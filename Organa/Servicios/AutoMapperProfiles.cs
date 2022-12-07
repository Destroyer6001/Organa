using AutoMapper;
using Organa.Models;

namespace Organa.Servicios
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<IngredienteViewModel, IngredienteCreacionViewModel>();

            CreateMap<PlatoViewModel, PlatoCreacionViewModel>();
        }
    }
}
