using System.Text;
using System.Threading.Tasks;
using API.ErrorHandling;
using API.Extensions;
using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
     public class AccountController : BaseApiController
    {
        private readonly ITokenService _tokenService;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        public AccountController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, ITokenService tokenService, 
            IMapper mapper, IDoctorService doctorService, IPatientService patientService, 
            IEmailService emailService, IConfiguration config, IUserService userService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
            _doctorService = doctorService;
            _patientService = patientService;
            _emailService = emailService;
            _config = config;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");

            var user = _mapper.Map<ApplicationUser>(registerDto);

            user.UserName = registerDto.Username.ToLower();
            user.PhoneNumber = registerDto.PhoneNumber;

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(new ServerResponse(400));

            var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
            var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

            string url = 
                    $"{_config["ApiAppUrl"]}/api/account/confirmemail?email={user.Email}&token={validEmailToken}";

            await _emailService.SendEmailAsync(user.Email, 
                "Confirm your email", $"<h1>Welcome to Auth Demo</h1>" +
                $"<p>Please confirm your email by <a href='{url}'>Clicking here</a></p>");

            var roleResult = await _userManager.AddToRoleAsync(user, "Patient");

            if (!roleResult.Succeeded) return BadRequest(result.Errors);

            await _patientService.CreatePatient1(user, registerDto);

            return Ok();
        }

        [HttpGet("confirmemail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] string token)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(token))
                return NotFound();

            await _userService.ConfirmEmailAsync(email, token);

            return Redirect($"{_config["AngularAppUrl"]}/account/email-confirmation");
        }

        
        [HttpPost("registerdoctor")]
        public async Task<ActionResult<DoctorToReturnDto>> RegisterDoctor(RegisterDoctorDto registerDoctorDto)
        {
            var user = _mapper.Map<ApplicationUser>(registerDoctorDto);

            var result = await _userManager.CreateAsync(user, registerDoctorDto.Password);
            
            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "Doctor");

            if (!roleResult.Succeeded) return BadRequest(result.Errors);

            await _doctorService.CreateDoctor(user.Id, registerDoctorDto, user.FirstName, user.LastName);

            return new DoctorToReturnDto
            {
                FirstName = user.FirstName,
                Token = await _tokenService.CreateToken(user)
            };
        }

        // ovo koristiš za sada!
        [HttpPost("registerdoctor1")]
        public async Task<ActionResult<UserDto>> RegisterDoctor1(RegisterDoctorDto1 registerDto)
        {
            if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");

            var user = _mapper.Map<ApplicationUser>(registerDto);
            
            user.UserName = registerDto.Username.ToLower();

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            
            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "Doctor");

            if (!roleResult.Succeeded) return BadRequest(result.Errors);

            await _doctorService.CreateDoctor1(user.Id, user.LastName, user.FirstName, registerDto);

           return NoContent(); /* new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                Email = user.Email
            }; */
        }

        // new method, attempting to implement multipleselectormodel
        [HttpPost("registerdoctor2")]
        public async Task<ActionResult<UserDto>> RegisterDoctor2(RegisterDoctorDto1 registerDto)
        {
            if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");

            var user = _mapper.Map<ApplicationUser>(registerDto);
            
            user.UserName = registerDto.Username.ToLower();
            user.EmailConfirmed = true;

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            
            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "Doctor");

            if (!roleResult.Succeeded) return BadRequest(result.Errors);

            var doctor = _mapper.Map<Doctor1>(registerDto);

            await _doctorService.CreateDoctor2(user.Id, doctor, user.LastName, user.FirstName);

            return NoContent();
        }

    
        [HttpPost("registeremployee")]
        public async Task<ActionResult<DoctorToReturnDto>> RegisterEmployee(RegisterEmployeeDto registerEmployeeDto)
        {
            var user = _mapper.Map<ApplicationUser>(registerEmployeeDto);

            var result = await _userManager.CreateAsync(user, registerEmployeeDto.Password);
            
            if (!result.Succeeded) return BadRequest(new ServerResponse(400));

            var roleResult = await _userManager.AddToRoleAsync(user, "Employee");

            if (!roleResult.Succeeded) return BadRequest(result.Errors);

            return new DoctorToReturnDto
            {
                FirstName = user.FirstName,
                Token = await _tokenService.CreateToken(user)
            };
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
           
            var user = await _userManager.Users
                .FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            if (user == null) return BadRequest("Invalid request");

            if (!await _userManager.IsEmailConfirmedAsync(user))
            return Unauthorized("Email is not confirmed");

            var result = await _signInManager
                .CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized(new ServerResponse(401));

            return new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                Email = user.Email,
                RoleName = await _doctorService.GetRoleName(user.Id),
                UserId = user.Id
            };
        }

        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            
            if (string.IsNullOrEmpty(dto.Email)) return NotFound(new ServerResponse(404));

            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null) return BadRequest(new ApiResponse(400));

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            string url = $"{_config["AngularAppUrl"]}/account/reset-password?email={dto.Email}&token={validToken}";

            await _emailService.SendEmailAsync(dto.Email, "Reset Password", "<h1>Follow the instructions to reset your password</h1>" +
                $"<p>To reset your password <a href='{url}'>Click here</a></p>");   

            return Ok();
        }

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null) return NotFound(new ServerResponse(404));

            var decodedToken = WebEncoders.Base64UrlDecode(dto.Token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ResetPasswordAsync(user, normalToken, dto.Password);

            if (result.Succeeded) return Ok();

            return BadRequest(new ServerResponse(400));
        }


        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}