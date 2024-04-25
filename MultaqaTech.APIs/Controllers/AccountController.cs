namespace MultaqaTech.APIs.Controllers;

public class AccountController : BaseApiController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IAuthService _authService;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IUnitOfWork _unitOfWork;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
        IAuthService authService,
        RoleManager<IdentityRole> roleManager,
        IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _authService = authService;
        _roleManager = roleManager;
        _unitOfWork= unitOfWork;
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

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

            if (!result.Succeeded)
                return Unauthorized(new ApiResponse(401));

            return Ok(new UserDto
            {
                UserName = user.UserName ?? string.Empty,
                Email = user.Email??string.Empty,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            }); ;
        }

        return Unauthorized(new ApiResponse(401));
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
                var resetPasswordLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, Request.Scheme);
                var email = new Email()
                {
                    Title = "Reset Password",
                    To = model.Email,
                    Body = resetPasswordLink??string.Empty
                };
                EmailSettings.SendEmail(email);
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

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto model)
    {
        if (CheckEmailExists(model.Email).Result.Value)
            return BadRequest(new ApiValidationErrorResponse() { Errors = new string[] { "This email already exists!" } });

        if (CheckUserNameExists(model.UserName).Result.Value)
            return BadRequest(new ApiValidationErrorResponse() { Errors = new string[] { "This user name already exists!" } });

        if (CheckPhoneNumberExists(model.PhoneNumber).Result.Value)
            return BadRequest(new ApiValidationErrorResponse() { Errors = new string[] { "This phone number already exists!" } });

        Student? student = new();   
        await _unitOfWork.Repository<Student>().AddAsync(student);

        var user = new AppUser
        {
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            UserName = model.UserName,
            PhoneNumber = model.PhoneNumber,
            RegistrationDate = DateTime.Now,
            Student=student,
            StudentId=student.Id
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            string errors = string.Join(", ", result.Errors.Select(error => error.Description));
            _unitOfWork.Repository<Student>().Delete(student);
            return BadRequest(new ApiResponse(400, errors));
        }

        student.AppUser=user;
        student.AppUserId=user.Id;
        _unitOfWork.Repository<Student>().Update(student);


        return Ok(new UserDto
        {
            UserName = user.UserName,
            Email = user.Email,
            Token = await _authService.CreateTokenAsync(user, _userManager)
        });

    }


    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);

        var user = await _userManager.FindByEmailAsync(email);

        return Ok(new UserDto()
        {
            UserName = user.UserName??string.Empty,
            Email = user.Email ?? string.Empty,
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
    public async Task<ActionResult> CreateToken(string? name)
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
            Log.Error(ex,ex.Message);
            return BadRequest(new ApiResponse(400));
        }
    }


    [HttpPost("GoogleSignIn")]
    public async Task<ActionResult<UserDto>> GoogleSignIn(GoogleSignInVM model)
    {
        try
        {
            var result = await _authService.SignInWithGoogle(model);
            if (result != null)
            {
                UserDto userDto = new UserDto()
                {
                    UserName = result.UserName ?? string.Empty,
                    ProfilePictureUrl = result.ProfilePictureUrl,
                    Email = result.Email ?? string.Empty,
                    Token = await _authService.CreateTokenAsync(result, _userManager)
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
    public async Task<IActionResult> FacebookSignIn(FacebookSignInVM model)
    {
        try
        {
            var result = (IActionResult)await _authService.SignInWithFacebook(model);
            if (result != null)
                return Ok(result);
            else
                return BadRequest(new ApiResponse(400));
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return BadRequest(new ApiResponse(400));
        }
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout(string dummyString)
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