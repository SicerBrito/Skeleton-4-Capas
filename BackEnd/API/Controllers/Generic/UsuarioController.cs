using API.Dtos;
using API.Services;
using AutoMapper;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
    public class UsuariosController : BaseApiController
    {
        private readonly IUserService _UserService;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IMapper _Mapper;

        public UsuariosController(IUserService userService, IUnitOfWork UnitOfWork, IMapper mapper)
        {
            _UserService = userService;
            _UnitOfWork = UnitOfWork;
            _Mapper = mapper;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> RegisterAsync(RegisterDto model)
        {
            var result = await _UserService.RegisterAsync(model);
            return Ok(result);
        }

        //METODO POST PARA ONTENER EL TOKEN CON SU REFRESHTOKEN
        [HttpPost("token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTokenAsync(LoginDto model)
        {
            var result = await _UserService.GetTokenAsync(model);
            SetRefreshTokenInCookie(result.RefreshToken!); //activar la cookie con el refreshToken
            return Ok(result);
        }

        //METODO PARA AÑADIR UN ROL 
        [HttpPost("addrole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddRoleAsync(AddRoleDto model)
        {
            var result = await _UserService.AddRoleAsync(model);
            return Ok(result);
        }

        //METODO POST PARA OBTENER EL REFRESTOKEN Y ACTUALIZARLO
        [HttpPost("refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _UserService.RefreshTokenAsync(refreshToken!);
            
            if (!string.IsNullOrEmpty(response.RefreshToken))
                SetRefreshTokenInCookie(response.RefreshToken);

            return Ok(response);
        }
        private void SetRefreshTokenInCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(10),
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }


}
