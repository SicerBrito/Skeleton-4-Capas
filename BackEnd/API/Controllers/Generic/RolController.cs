using API.Dtos;
using API.Dtos.Generic;
using API.Helpers;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

    [ApiVersion("1.0")] //obtener solo las areas de incidencias
    [ApiVersion("1.1")] //obtener solo las areas de incidencias
    public class RolController : BaseApiController
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IMapper _Mapper;

        //constructor de la classe
        public RolController(IUnitOfWork UnitOfWork, IMapper mapper)
        {
            _UnitOfWork = UnitOfWork;
            _Mapper = mapper;
        }

        //METODO GET obtener todos los registros
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<List<RolDto>>> Get()
        {
            var roles = await _UnitOfWork.Roles!.GetAllAsync();
            return _Mapper.Map<List<RolDto>>(roles);

        }

        //METODO GET inplemetacion de la paginacion, busquedas
        [HttpGet("Paginacion")]
        [MapToApiVersion("1.1")]
        [Authorize(Roles = "Administrador, Gerente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Pager<RolDto>>> Get1A([FromQuery] Params rolParams)
        {
            var role = await _UnitOfWork.Roles!.GetAllAsync(rolParams.PageIndex, rolParams.PageSize, rolParams.Search);
            var lstRolesDto = _Mapper.Map<List<RolDto>>(role.registros);

            return new Pager<RolDto>(lstRolesDto , role.totalRegistros, rolParams.PageIndex, rolParams.PageSize, rolParams.Search);
        }

        //METODO GET POR ID (buscar un registro por id)
        [HttpGet("{id}")]
        //[MapToApiVersion("1.0")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RolDto>> Get(int id)
        {
            var rol = await _UnitOfWork.Roles!.GetByIdAsync(id);
            if (rol == null)
            {
                return NotFound();
            }
            return _Mapper.Map<RolDto>(rol);
        }

        //METODO POST (para enviar iformacion a la Db)
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RolDto>> Post(RolDto rolDto)
        {
            var rol = _Mapper.Map<Rol>(rolDto);
            _UnitOfWork.Roles!.Add(rol);
            await _UnitOfWork.SaveAsync();

            if (rol == null)
            {
                return BadRequest();
            }
            return _Mapper.Map<RolDto>(rol);

        }

        //METODO PUT (para editar un registro de la Db)
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RolDto>> Put(int id, [FromBody]RolDto rolDto)
        {
            var rol = _Mapper.Map<Rol>(rolDto);
            if (rol == null)
            {
                return NotFound();
            }
            rol.Id = id;
            _UnitOfWork.Roles!.Update(rol);
            await _UnitOfWork.SaveAsync();
            return _Mapper.Map<RolDto>(rol);
        }

        //METODO DELETE (Eliminar un regidtro de la Db)
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var rol = await _UnitOfWork.Roles!.GetByIdAsync(id);
            if (rol == null)
            {
                return NotFound();
            }
            _UnitOfWork.Roles.Remove(rol);
            await _UnitOfWork.SaveAsync();
            return NoContent();
        }

}
