using AutoMapper;
using ContestService.BLL.Exceptions;
using ContestService.BLL.Mapper;
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
    private readonly IMapper _mapper;
    private readonly MusicalInstrumentService _instrumentService;

    public MusicalInstrumentServiceTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>(); // та же конфигурация, что в BLL
        });
        _mapper = config.CreateMapper();

        _repositoryMock = new Mock<IRepositoryBase<MusicalInstrument>>();
        _instrumentService = new  MusicalInstrumentService(_repositoryMock.Object, _mapper);
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

        var entity = _mapper.Map<MusicalInstrument>(model);

        _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<MusicalInstrument>(), It.IsAny<CancellationToken>())).ReturnsAsync(entity);

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

        var models = _mapper.Map<List<MusicalInstrumentModel>>(entities);

        _repositoryMock.Setup(r => r.GetAllToListAsync(It.IsAny<CancellationToken>())).ReturnsAsync(entities);

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
        var model = _mapper.Map<MusicalInstrumentModel>(entity);

        _repositoryMock
            .Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<MusicalInstrument, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<MusicalInstrument> { entity });

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
        var oldEntity = new MusicalInstrument
        {
            Id = id,
            Name = "Old",
            NominationId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var updatedEntity = new MusicalInstrument
        {
            Id = id,
            Name = "Trumpet",
            NominationId = oldEntity.NominationId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var model = _mapper.Map<MusicalInstrumentModel>(updatedEntity);

        _repositoryMock
            .Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<MusicalInstrument, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<MusicalInstrument> { oldEntity });

        _repositoryMock
            .Setup(r => r.UpdateAsync(oldEntity, It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedEntity); 

        // Act
        var result = await _instrumentService.UpdateAsync(model, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(model);
    }

}
