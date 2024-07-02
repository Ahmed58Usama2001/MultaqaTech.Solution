namespace MultaqaTech.APIs.Controllers;

public class AccountController : BaseApiController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IAuthService _authService;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMaillingService _mailService;
    private readonly IConfiguration _configuration;


    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
        IAuthService authService,
        RoleManager<IdentityRole> roleManager,
        IUnitOfWork unitOfWork,
        IMaillingService maillingService,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _authService = authService;
        _roleManager = roleManager;
        _unitOfWork = unitOfWork;
        _mailService = maillingService;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto model)
    {
        if (CheckEmailExists(model.Email).Result.Value)
            return BadRequest(new ApiValidationErrorResponse() { Errors = new string[] { "This email already exists!" } });

        if (CheckUserNameExists(model.UserName).Result.Value)
            return BadRequest(new ApiValidationErrorResponse() { Errors = new string[] { "This user name already exists!" } });

        if (CheckPhoneNumberExists(model.PhoneNumber).Result.Value)
            return BadRequest(new ApiValidationErrorResponse() { Errors = new string[] { "This phone number already exists!" } });


        var user = new AppUser
        {
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            UserName = model.UserName,
            PhoneNumber = model.PhoneNumber,
            RegistrationDate = DateTime.Now,
            EmailConfirmed = false

        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            string errors = string.Join(", ", result.Errors.Select(error => error.Description));
            return BadRequest(new ApiResponse(400, errors));
        }

        Student? student = new()
        {
            AppUser = user,
            AppUserId = user.Id
        };
        await _unitOfWork.Repository<Student>().AddAsync(student);

        var studentResult = await _unitOfWork.CompleteAsync();
        if (studentResult <= 0) return BadRequest(false);

        user.Student = student;
        user.StudentId = student.Id;

        result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            string errors = string.Join(", ", result.Errors.Select(error => error.Description));
            return BadRequest(new ApiResponse(400, errors));
        }

        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        //var callbackUrl = Request.Scheme + "://" + Request.Host + Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = UrlEncoder.Default.Encode(code) });

        //this will be used right before deployment
        var callbackUrl =Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = UrlEncoder.Default.Encode(code) }, "http", _configuration["AngularBaseUrl"]);

        var bodyUrl = $"{Directory.GetCurrentDirectory()}\\wwwroot\\TempleteHtml\\2-StepVerificationTemplete.html";
        var body = new StreamReader(bodyUrl);
        var mailText = body.ReadToEnd();
        body.Close();

        mailText = mailText.Replace("[username]", user.UserName).Replace("[LinkHere]",
            HtmlEncoder.Default.Encode(callbackUrl));

        var emailResult = await _mailService.SendEmailAsync(model.Email, "Confirm Email", mailText);
        if (!emailResult)
            return BadRequest(new ApiResponse(400));

        return Ok(true);

    }

    [HttpGet("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail(string userId, string code)
    {
        if (userId == null || code == null)
            return BadRequest(new ApiResponse(400));

        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            Unauthorized(new ApiResponse(401));

        var decodedCode = System.Web.HttpUtility.UrlDecode(code);
        var result = await _userManager.ConfirmEmailAsync(user, decodedCode);

        var status = result.Succeeded;

        return Ok(status);
    }

    [HttpPost("BecomeInstructor")]
    public async Task<IActionResult> BecomeInstructor(BecomeInstructorDto model)
    {
        if (model is null) return BadRequest(new ApiResponse(400));

        string? userEmail = User.FindFirstValue(ClaimTypes.Email);
        if (userEmail is null) return NotFound(new ApiResponse(401));

        AppUser? storedUser = await _userManager.FindByEmailAsync(userEmail);
        if (storedUser is null) return NotFound(new ApiResponse(401));

        if (model.ProfilePicture is null && storedUser.ProfilePictureUrl is null)
            return BadRequest(new ApiResponse(400,"Profile Picture is required"));

        Instructor? instructor = new()
        {
            AppUser = storedUser,
            AppUserId = storedUser.Id,
            Bio=model.Bio,
            JobTitle=model.JobTitle,
        };

        await _unitOfWork.Repository<Instructor>().AddAsync(instructor);

        var instructorResult = await _unitOfWork.CompleteAsync();
        if (instructorResult <= 0) return BadRequest(false);

        storedUser.Instructor = instructor;
        storedUser.InstructorId = instructor.Id;
        storedUser.ProfilePictureUrl = storedUser.ProfilePictureUrl?? DocumentSetting.UploadFile(model?.ProfilePicture, "Users\\ProfilePictures");
        storedUser.IsInstructor = true;

        var userResult = await _userManager.UpdateAsync(storedUser);
        var status = userResult.Succeeded;

        return Ok(status);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.UserNameOrEmail);

            if (user == null)
                user = await _userManager.FindByNameAsync(model.UserNameOrEmail);

            if (user is null)
                return Unauthorized(new ApiResponse(401));

            if (!user.EmailConfirmed)
                return Unauthorized(new ApiResponse(401, "Email Needs to be confirmed"));

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

            if (!result.Succeeded)
                return Unauthorized(new ApiResponse(401));

            return Ok(new UserDto
            {
                UserName = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                IsInstructor = user?.IsInstructor ?? false,
                InstructorId= user?.InstructorId ?? 0,
                StudentId= user?.StudentId ?? 0,
                ProfilePictureUrl= !string.IsNullOrEmpty(user.ProfilePictureUrl)? $"{_configuration["ApiBaseUrl"]}/{user?.ProfilePictureUrl}":string.Empty,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            });
        }

        return Unauthorized(new ApiResponse(401));
    }

    [Authorize]
    [HttpGet("GetCurrentUser")]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);

        var user = await _userManager.FindByEmailAsync(email);

        return Ok(new UserDto()
        {
            UserName = user.UserName ?? string.Empty,
            Email = user.Email ?? string.Empty,
            IsInstructor = user?.IsInstructor ?? false,
            InstructorId = user?.InstructorId ?? 0,
            StudentId = user?.StudentId ?? 0,
            ProfilePictureUrl = !string.IsNullOrEmpty(user.ProfilePictureUrl) ? $"{_configuration["ApiBaseUrl"]}/{user?.ProfilePictureUrl}" : string.Empty,
            Token = await _authService.CreateTokenAsync(user, _userManager)
        });
    }

    [HttpGet("emailexists")]
    public async Task<ActionResult<bool>> CheckEmailExists(string email)
        => await _userManager.FindByEmailAsync(email) is not null;

    [HttpGet("phonenumberexists")]
    public async Task<ActionResult<bool>> CheckPhoneNumberExists(string phoneNumber)
       => await _userManager.Users.FirstOrDefaultAsync(U => U.PhoneNumber == phoneNumber) is not null;

    [HttpGet("usernameexists")]
    public async Task<ActionResult<bool>> CheckUserNameExists(string userName)
        => await _userManager.FindByNameAsync(userName) is not null;

    [HttpPost("CreateRole")]
    public async Task<ActionResult> CreateRole(string? name)
    {
        try
        {
            if (string.IsNullOrEmpty(name)) return BadRequest(new ApiResponse(400, "Role cannot be Empty !!"));

            bool isRoleAlreadyExists = await _roleManager.RoleExistsAsync(name);
            if (isRoleAlreadyExists) return BadRequest(new ApiResponse(400, $"Role: {name} Already Exists !!"));

            await _roleManager.CreateAsync(new IdentityRole(name));
            return Ok(name);
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            return BadRequest(new ApiResponse(400));
        }
    }


    [HttpPost("GoogleSignIn")]
    public async Task<ActionResult<UserDto>> GoogleSignIn(GoogleSignInVM model)
    {
        try
        {
            var googleUser = await _authService.SignInWithGoogle(model);

            if (googleUser.StudentId is null)
            {
                Student? student = new()
                {
                    AppUser = googleUser,
                    AppUserId = googleUser.Id
                };
                await _unitOfWork.Repository<Student>().AddAsync(student);

                var studentResult = await _unitOfWork.CompleteAsync();
                if (studentResult <= 0) return BadRequest(false);

                googleUser.Student = student;
                googleUser.StudentId = student.Id;

                var result = await _userManager.UpdateAsync(googleUser);
                if (!result.Succeeded)
                {
                    string errors = string.Join(", ", result.Errors.Select(error => error.Description));
                    return BadRequest(new ApiResponse(400, errors));
                }
            }


            if (googleUser != null)
            {
                UserDto userDto = new UserDto()
                {
                    UserName = googleUser.UserName ?? string.Empty,
                    IsInstructor = googleUser?.IsInstructor ?? false,
                    InstructorId = googleUser?.InstructorId ?? 0,
                    StudentId = googleUser?.StudentId ?? 0,
                    ProfilePictureUrl = !string.IsNullOrEmpty(googleUser.ProfilePictureUrl) ? $"{_configuration["ApiBaseUrl"]}/{googleUser?.ProfilePictureUrl}" : string.Empty,
                    Email = googleUser?.Email ?? string.Empty,
                    Token = await _authService.CreateTokenAsync(googleUser, _userManager)
                };

                return Ok(userDto);
            }
            else
                return BadRequest(new ApiResponse(400));
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return BadRequest(new ApiResponse(400));
        }
    }

    [HttpPost("FacebookSignIn")]
    public async Task<ActionResult<UserDto>> FacebookSignIn(FacebookSignInVM model)
    {
        try
        {
            var facebookUser = await _authService.SignInWithFacebook(model);


            if (facebookUser.StudentId is null)
            {
                Student? student = new()
                {
                    AppUser = facebookUser,
                    AppUserId = facebookUser.Id
                };
                await _unitOfWork.Repository<Student>().AddAsync(student);

                var studentResult = await _unitOfWork.CompleteAsync();
                if (studentResult <= 0) return BadRequest(false);

                facebookUser.Student = student;
                facebookUser.StudentId = student.Id;

                var result = await _userManager.UpdateAsync(facebookUser);
                if (!result.Succeeded)
                {
                    string errors = string.Join(", ", result.Errors.Select(error => error.Description));
                    return BadRequest(new ApiResponse(400, errors));
                }
            }

            if (facebookUser != null)
            {
                UserDto userDto = new UserDto()
                {
                    UserName = facebookUser.UserName ?? string.Empty,
                    IsInstructor = facebookUser?.IsInstructor ?? false,
                    InstructorId = facebookUser?.InstructorId ?? 0,
                    StudentId = facebookUser?.StudentId ?? 0,
                    ProfilePictureUrl = !string.IsNullOrEmpty(facebookUser.ProfilePictureUrl) ? $"{_configuration["ApiBaseUrl"]}/{facebookUser?.ProfilePictureUrl}" : string.Empty,
                    Email = facebookUser?.Email ?? string.Empty,
                    Token = await _authService.CreateTokenAsync(facebookUser, _userManager)
                };

                return Ok(userDto);
            }
            else
                return BadRequest(new ApiResponse(400));
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return BadRequest(new ApiResponse(400));
        }
    }

    [HttpPost("forgetPassword")]
    public async Task<ActionResult<UserDto>> ForgetPassword(ForgetPasswordDto model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);


            if (user is not null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var resetPasswordLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, "http", _configuration["AngularBaseUrl"]);
                var bodyUrl = $"{Directory.GetCurrentDirectory()}\\wwwroot\\TempleteHtml\\ForgetPasswordTemplete.html";
                var body = new StreamReader(bodyUrl);
                var mailText = body.ReadToEnd();
                body.Close();

                mailText = mailText.Replace("[username]", user.UserName).Replace("[LinkHere]", resetPasswordLink);

                var result = await _mailService.SendEmailAsync(model.Email, "Reset Password", mailText);
                if (!result)
                    return BadRequest(new ApiResponse(400, "No Internet Connection"));


                return Ok(model);
            }
            return Unauthorized(new ApiResponse(401));
        }

        return Ok(model);
    }

    [HttpPost("ResetPassword")]
    public async Task<ActionResult<UserDto>> ResetPassword(ResetPasswordDto model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);


            if (user is not null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
                if (result.Succeeded)
                    return Ok(model);
                string errors = string.Join(", ", result.Errors.Select(error => error.Description));
                return BadRequest(new ApiResponse(400, errors));

            }
        }

        return Ok(model);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
        {
            try
            {
                await _authService.InvalidateSignedInTokenAsync(token); // Call your AuthService method
                return Ok(new { message = "Logged out successfully" });
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message); // Log the error
                return BadRequest(new { message = "Error during logout" });
            }
        }

        return BadRequest(new { message = "Unable to logout" });
    }
}