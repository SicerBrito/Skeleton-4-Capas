using API.Dtos;
using API.Dtos.Generic;
using AutoMapper;
using Dominio.Entities;

namespace API.Profiles;
    public class MappingProfile : Profile{
        public MappingProfile(){

            CreateMap<Rol, RolDto>()
                .ReverseMap();

                
            //TODO: Ejemplo
            // CreateMap<NombreDto1, NombreEntidad1>()
            //     .ReverseMap();

            // CreateMap<NombreDto2, NombreEntidad2>()
            //     .ReverseMap();

        }
    }
