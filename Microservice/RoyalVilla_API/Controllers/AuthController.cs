using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalVilla_API.Data;
using RoyalVilla_API.Models;
using RoyalVilla_API.Models.DTO;
using RoyalVilla_API.Services;

namespace RoyalVilla_API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("Register")]
        [ProducesResponseType(typeof(ActionResult<UserDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ActionResult<object>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ActionResult<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ActionResult<object>), StatusCodes.Status409Conflict)]

        public async Task<ActionResult<ApiResponse<UserDTO>>> Register([FromBody]RegisterationRequestDTO registrationRequestDTO)
        {
            try
            {
                if (registrationRequestDTO == null)
                {

                    return BadRequest(ApiResponse<object>.BadRequest("Registeration data is required"));
                }
                if (await _authService.IsEmailExixtsAsync(registrationRequestDTO.Email))
                {
                    return Conflict(ApiResponse<object>.Conflict($"Email {registrationRequestDTO.Email} is already in use."));
                }

                var user = await _authService.RegisterAsync(registrationRequestDTO);
                if (user == null)
                {
                    return BadRequest(ApiResponse<object>.BadRequest("Registration failed"));
                }

                var response = ApiResponse<UserDTO>.CreatedAt(user, "user created successfully");
                return CreatedAtAction(nameof(Register), response);
                //return Ok(await _db.Villa.ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<object>.Error(StatusCodes.Status500InternalServerError, "An error occurred while registering.", ex.Message));
            }
        }



        [HttpPost("Login")]
        [ProducesResponseType(typeof(ActionResult<IEnumerable<LoginResponseDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ActionResult<object>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ActionResult<object>), StatusCodes.Status404NotFound)]
      

        public async Task<ActionResult<ApiResponse<LoginResponseDTO>>> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            try
            {
                if (loginRequestDTO == null)
                {

                    return BadRequest(ApiResponse<object>.BadRequest("Login data is required"));
                }
                

                var loginResponse = await _authService.LoginAsync(loginRequestDTO);
                if (loginResponse == null)
                {
                    return BadRequest(ApiResponse<object>.BadRequest("Login failed"));
                }

                var response = ApiResponse<LoginResponseDTO>.OK(loginResponse, "Login successful");
                return Ok(response);
                //return Ok(await _db.Villa.ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<object>.Error(StatusCodes.Status500InternalServerError, "An error occurred while logging in.", ex.Message));
            }
        }






    }
}
