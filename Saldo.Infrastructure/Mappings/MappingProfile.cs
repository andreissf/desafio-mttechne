using AutoMapper;

namespace Saldo.Infrastructure.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Saldo, Entities.Saldo>().ReverseMap();
    }
}