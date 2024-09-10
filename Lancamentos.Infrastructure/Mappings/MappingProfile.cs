using AutoMapper;

namespace Lancamentos.Infrastructure.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Lancamento, Entities.Lancamento>().ReverseMap();
        CreateMap<Domain.TipoLancamento, Entities.TipoLancamento>().ReverseMap();
    }
}