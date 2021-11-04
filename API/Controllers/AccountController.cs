using System.Threading.Tasks;
using API.ErrorHandling;
using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public AccountController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, ITokenService tokenService, 
            IMapper mapper, IDoctorService doctorService, IPatientService patientService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
            _doctorService = doctorService;
            _patientService = patientService;
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

            var roleResult = await _userManager.AddToRoleAsync(user, "Patient");

            if (!roleResult.Succeeded) return BadRequest(result.Errors);

            await _patientService.CreatePatient1(user.Id, user.LastName, user.FirstName, registerDto.DateOfBirth);


            return new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                Email = user.Email,
                RoleName = await _doctorService.GetRoleName(user.Id),
                UserId = user.Id            };
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


        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}