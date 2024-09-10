using AutoMapper;
using Lancamentos.Application.UseCases.Update;

namespace Lancamentos.Api.UseCases.Update;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UpdateLancamentoRequest, UpdateLancamentoCommand>();
    }
}