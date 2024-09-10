using AutoMapper;
using Lancamentos.Application.UseCases.Create;

namespace Lancamentos.Api.UseCases.Create;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateLancamentoRequest, CreateLancamentoCommand>();
    }
}