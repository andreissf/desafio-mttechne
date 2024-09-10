using Core.Data;

namespace Lancamentos.Domain;

public class TipoLancamento : Entity
{
    private const string DEBITO = "D";
    public string Tipo { get; set; }

    public bool EhDebito()
    {
        return Tipo.ToUpper() == DEBITO;
    }
}