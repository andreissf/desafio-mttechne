using AutoMapper;
using Lancamentos.Application.UseCases.Create;
using Lancamentos.Application.UseCases.Update;
using Lancamentos.Domain;

namespace Lancamentos.Application.UseCases;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateLancamentoCommand, Lancamento>().ReverseMap();
        CreateMap<UpdateLancamentoCommand, Lancamento>().ReverseMap();
    }
}