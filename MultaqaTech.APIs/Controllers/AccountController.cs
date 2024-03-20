using MultaqaTech.APIs.Dtos.AccountDtos;

namespace MultaqaTech.APIs.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : BaseApiController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,
        IAuthService authService,
        IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _authService = authService;
        _mapper = mapper;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto model )
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.UserNameOrEmail);

            if (user == null) 
                user = await _userManager.FindByNameAsync(model.UserNameOrEmail);
           
            if(user is null)
                return Unauthorized(new ApiResponse(401));

            var result= await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe,false );

            if(!result.Succeeded)
                return Unauthorized(new ApiResponse(401));

            return Ok(new UserDto
            {
                UserName = user.UserName,
                Email = user.Email,
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
                var resetPasswordLink = Url.Action("ResetPassword","Account",new {Email=model.Email,Token=token}, Request.Scheme);
                var email = new Email()
                {
                    Title = "Reset Password",
                    To = model.Email,
                    Body = resetPasswordLink
                };
                EmailSettings.SendEmail(email);
                return Ok(model);
            }
            return Unauthorized(new ApiResponse(401));
        }

        return Ok(model) ;
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
            return BadRequest(new ApiValidationErrorResponse() { Errors= new string[] {"This email already exists!"} });

        if (CheckUserNameExists(model.UserName).Result.Value)
            return BadRequest(new ApiValidationErrorResponse() { Errors = new string[] { "This user name already exists!" } });

        if (CheckPhoneNumberExists(model.PhoneNumber).Result.Value)
            return BadRequest(new ApiValidationErrorResponse() { Errors = new string[] { "This phone number already exists!" } });

        var user = new AppUser
        {   Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            UserName = model.UserName,
            PhoneNumber = model.PhoneNumber,
            RegistrationDate= DateTime.Now
        };

        var result=await _userManager.CreateAsync(user,model.Password);

        if (!result.Succeeded)
        {
            string errors = string.Join(", ", result.Errors.Select(error => error.Description));
            return BadRequest(new ApiResponse(400, errors));
        }

        return Ok(new UserDto
        {
            UserName = user.UserName,
            Email = user.Email,
            Token = await _authService.CreateTokenAsync(user, _userManager)
        });

    }


    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserDto>>GetCurrentUser()
    {
        var email = User.FindFirstValue(ClaimTypes.Email); 

        var user= await _userManager.FindByEmailAsync(email);

        return Ok(new UserDto()
        {
            UserName= user.UserName,
            Email = user.Email,
            Token= await _authService.CreateTokenAsync(user, _userManager)
        });
    }

    [HttpGet("emailexists")]
    public async Task<ActionResult<bool>> CheckEmailExists(string email)
        =>await _userManager.FindByEmailAsync(email) is not null;

    [HttpGet("phonenumberexists")]
    public async Task<ActionResult<bool>> CheckPhoneNumberExists(string phoneNumber)
       =>await _userManager.Users.FirstOrDefaultAsync(U => U.PhoneNumber == phoneNumber) is not null;

    [HttpGet("usernameexists")]
    public async Task<ActionResult<bool>> CheckUserNameExists(string userName)
        => await _userManager.FindByNameAsync(userName) is not null;


}
