using AutoMapper;
using ServerAnime.Model.ModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServerAnime.Model.Utils
{
    public class AutoMaperProfiles : Profile
    {
        public AutoMaperProfiles()
        {
            CreateMap<Categorium, CategoriaDto>();
            CreateMap<Role, RolDto>();
        }
    }
}
