using AutoMapper;
using ContestService.BLL.Exceptions;
using ContestService.BLL.Models;
using ContestService.BLL.Services;
using ContestService.DAL.Entities;
using ContestService.DAL.Repositories.Interfaces;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;

namespace ContestService.BLL.Tests.ServiceTests;
public class MusicalInstrumentServiceTests
{
    private readonly Mock<IRepositoryBase<MusicalInstrument>> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly MusicalInstrumentService _instrumentService;

    public MusicalInstrumentServiceTests()
    {
        _repositoryMock = new Mock<IRepositoryBase<MusicalInstrument>>();
        _mapperMock = new Mock<IMapper>();
        _instrumentService = new  MusicalInstrumentService(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateInstrumentSuccessfully()
    {
        // Arrange
        var model = new MusicalInstrumentModel
        {
            Id = Guid.NewGuid(),
            Name = "Violin",
            NominationId = Guid.NewGuid()
        };

        var entity = new MusicalInstrument
        {
            Id = model.Id,
            Name = model.Name,
            NominationId = model.NominationId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _mapperMock.Setup(m => m.Map<MusicalInstrument>(model)).Returns(entity);
        _repositoryMock.Setup(r => r.CreateAsync(entity, It.IsAny<CancellationToken>())).ReturnsAsync(entity);
        _mapperMock.Setup(m => m.Map<MusicalInstrumentModel>(entity)).Returns(model);

        // Act
        var result = await _instrumentService.CreateAsync(model, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(model);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteInstrumentSuccessfully()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity = new MusicalInstrument { Id = id, Name = "Flute", NominationId = Guid.NewGuid() };

        _repositoryMock
            .Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<MusicalInstrument, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<MusicalInstrument> { entity });

        _repositoryMock.Setup(r => r.DeleteAsync(entity, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        // Act
        await _instrumentService.DeleteAsync(id, CancellationToken.None);

        // Assert
        _repositoryMock.Verify(r => r.DeleteAsync(entity, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowNotFoundException_WhenInstrumentNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        _repositoryMock
            .Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<MusicalInstrument, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<MusicalInstrument>());

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _instrumentService.DeleteAsync(id, CancellationToken.None));
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnInstrumentList()
    {
        // Arrange
        var entities = new List<MusicalInstrument>
    {
        new() { Id = Guid.NewGuid(), Name = "Piano", NominationId = Guid.NewGuid() },
        new() { Id = Guid.NewGuid(), Name = "Guitar", NominationId = Guid.NewGuid() }
    };

        var models = entities.Select(e => new MusicalInstrumentModel
        {
            Id = e.Id,
            Name = e.Name,
            NominationId = e.NominationId
        }).ToList();

        _repositoryMock.Setup(r => r.GetAllToListAsync(It.IsAny<CancellationToken>())).ReturnsAsync(entities);
        _mapperMock.Setup(m => m.Map<List<MusicalInstrumentModel>>(entities)).Returns(models);

        // Act
        var result = await _instrumentService.GetAllAsync(CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(models);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnInstrument()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity = new MusicalInstrument { Id = id, Name = "Saxophone", NominationId = Guid.NewGuid() };
        var model = new MusicalInstrumentModel { Id = id, Name = entity.Name, NominationId = entity.NominationId };

        _repositoryMock
            .Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<MusicalInstrument, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<MusicalInstrument> { entity });

        _mapperMock.Setup(m => m.Map<MusicalInstrumentModel>(entity)).Returns(model);

        // Act
        var result = await _instrumentService.GetAsync(id, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(model);
    }

    [Fact]
    public async Task GetAsync_ShouldThrowNotFoundException_WhenInstrumentNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();

        _repositoryMock
            .Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<MusicalInstrument, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<MusicalInstrument>());

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _instrumentService.GetAsync(id, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowNotFoundException_WhenInstrumentNotFound()
    {
        // Arrange
        var model = new MusicalInstrumentModel { Id = Guid.NewGuid(), Name = "Oboe", NominationId = Guid.NewGuid() };

        _repositoryMock
            .Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<MusicalInstrument, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<MusicalInstrument>());

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _instrumentService.UpdateAsync(model, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateInstrumentSuccessfully()
    {
        // Arrange
        var id = Guid.NewGuid();
        var model = new MusicalInstrumentModel
        {
            Id = id,
            Name = "Trumpet",
            NominationId = Guid.NewGuid()
        };

        var entity = new MusicalInstrument
        {
            Id = id,
            Name = "Old",
            NominationId = model.NominationId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _repositoryMock
            .Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<MusicalInstrument, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<MusicalInstrument> { entity });

        _mapperMock.Setup(m => m.Map(model, entity));
        _repositoryMock
            .Setup(r => r.UpdateAsync(entity, It.IsAny<CancellationToken>()))
            .ReturnsAsync(entity); 
        _mapperMock.Setup(m => m.Map<MusicalInstrumentModel>(entity)).Returns(model);

        // Act
        var result = await _instrumentService.UpdateAsync(model, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(model);
    }

}
