using AutoMapper;
using Saldo.Application.UseCases.Get;

namespace Saldo.Api.UseCases.Get;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Request, GetConsolidadoDiarioCommand>();
    }
}