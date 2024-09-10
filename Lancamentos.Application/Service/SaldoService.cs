using Refit;

namespace Lancamentos.Application.Service;

public interface ISaldoService
{
    [Get("/health")]
    Task<string> GetHealth();
}
