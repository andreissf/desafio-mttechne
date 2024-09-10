using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using Lancamentos.Application.UseCases.Create;
using Lancamentos.Domain;
using Lancamentos.Infrastructure.Repository;
using MediatR;
using MessageBus;
using Moq;
using Xunit;

namespace Lancamentos.Application.Tests;

public class CreateLancamentoHandlerTests
{
    private readonly Mock<ILancamentoRepository> _lancamentoRepositoryMock;
    private readonly Mock<ITipoLancamentoRepository> _tipoLancamentoRepositoryMock;
    private readonly Mock<IMessageBus> _busMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateLancamentoHandler _handler;

    public CreateLancamentoHandlerTests()
    {
        _lancamentoRepositoryMock = new Mock<ILancamentoRepository>();
        _tipoLancamentoRepositoryMock = new Mock<ITipoLancamentoRepository>();
        _busMock = new Mock<IMessageBus>();
        _mapperMock = new Mock<IMapper>();
        _handler = new CreateLancamentoHandler(
            _lancamentoRepositoryMock.Object,
            _tipoLancamentoRepositoryMock.Object,
            _busMock.Object,
            _mapperMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldReturnValidationResult_WhenRequestIsValid()
    {
        // Arrange
        var command = new CreateLancamentoCommand(Guid.NewGuid(), "Teste", 100);
        var lancamento = new Lancamento();
        var tipoLancamento = new TipoLancamento();
        _mapperMock.Setup(m => m.Map<Lancamento>(command)).Returns(lancamento);
        _mapperMock.Setup(m => m.Map<TipoLancamento>(command)).Returns(tipoLancamento);
        _lancamentoRepositoryMock.Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
        _tipoLancamentoRepositoryMock.Setup(r => r.GetById(command.TipoLancamentoId)).Returns(new TipoLancamento() { Tipo = "D"});

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Handle_ShouldReturnValidationResultWithError_WhenCommitFails()
    {
        // Arrange
        var command = new CreateLancamentoCommand(Guid.NewGuid(), "Teste", 100);
        var lancamento = new Lancamento();
        _mapperMock.Setup(m => m.Map<Lancamento>(command)).Returns(lancamento);
        _lancamentoRepositoryMock.Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Erro ao persistir dados.");
    }

    [Fact]
    public async Task Handle_ShouldReturnValidationResultWithError_WhenTipoLancamentoNotFound()
    {
        // Arrange
        var command = new CreateLancamentoCommand(Guid.NewGuid(), "Teste", 100);
        var lancamento = new Lancamento();
        var tipoLancamento = new TipoLancamento();
        _mapperMock.Setup(m => m.Map<Lancamento>(command)).Returns(lancamento);
        _lancamentoRepositoryMock.Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
        _tipoLancamentoRepositoryMock.Setup(r => r.GetById(command.TipoLancamentoId)).Returns((TipoLancamento)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Tipo de lançamento não encontrado.");
    }
}