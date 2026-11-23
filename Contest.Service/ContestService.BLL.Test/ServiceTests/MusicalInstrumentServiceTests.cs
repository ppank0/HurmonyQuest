using AutoMapper;
using ContestService.BLL.Exceptions;
using ContestService.BLL.Mapper;
using ContestService.BLL.Services;
using ContestService.DAL.Entities;
using ContestService.DAL.Repositories.Interfaces;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;
using ContestService.BLL.Models.MusicalInstrument;

namespace ContestService.BLL.Tests.ServiceTests;
public class MusicalInstrumentServiceTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IMusicalInstrumentRepository> _instrumentRepoMock;
    private readonly MusicalInstrumentService _instrumentService;

    public MusicalInstrumentServiceTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>(); // та же конфигурация, что в BLL
        });
        _mapper = config.CreateMapper();

        _instrumentRepoMock = new Mock<IMusicalInstrumentRepository>();
        _instrumentService = new  MusicalInstrumentService(_mapper, _instrumentRepoMock.Object);
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateInstrumentSuccessfully()
    {
        // Arrange
        var nomination = new Nomination { Id = Guid.NewGuid(), Name = "nomination", MusicalInstruments = new List<MusicalInstrument>() };
        var editModel = new MusicalInstrumentEditModel
        {
            Name = "Violin",
            NominationId = nomination.Id,
        };

        var entity = _mapper.Map<MusicalInstrument>(editModel);
        entity.Id = Guid.NewGuid();
        var model = _mapper.Map<MusicalInstrumentModel>(entity);

        _instrumentRepoMock.Setup(r => r.CreateAsync(It.IsAny<MusicalInstrument>(), It.IsAny<CancellationToken>())).ReturnsAsync(entity);

        // Act
        var result = await _instrumentService.CreateAsync(editModel, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(model);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteInstrumentSuccessfully()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity = new MusicalInstrument { Id = id, Name = "Flute", NominationId = Guid.NewGuid() };

        _instrumentRepoMock
            .Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<MusicalInstrument, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<MusicalInstrument> { entity });

        _instrumentRepoMock.Setup(r => r.DeleteAsync(entity, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        // Act
        await _instrumentService.DeleteAsync(id, CancellationToken.None);

        // Assert
        _instrumentRepoMock.Verify(r => r.DeleteAsync(entity, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowNotFoundException_WhenInstrumentNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        _instrumentRepoMock
            .Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<MusicalInstrument, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<MusicalInstrument>());

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _instrumentService.DeleteAsync(id, CancellationToken.None));
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnInstrumentList()
    {
        var nomination = new Nomination { Id = Guid.NewGuid(), Name = "nomination", MusicalInstruments = new List<MusicalInstrument>() };
        // Arrange
        var entities = new List<MusicalInstrument>
        {
            new() { Id = Guid.NewGuid(), Name = "Piano", NominationId = nomination.Id, Nomination = nomination},
            new() { Id = Guid.NewGuid(), Name = "Guitar", NominationId = nomination.Id, Nomination = nomination }
        };

        var models = _mapper.Map<List<MusicalInstrumentModel>>(entities);

        _instrumentRepoMock.Setup(r => r.GetAllToListAsync(It.IsAny<CancellationToken>())).ReturnsAsync(entities);

        // Act
        var result = await _instrumentService.GetAllAsync(CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(models);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnInstrument()
    {
        // Arrange
        var instrumentId = Guid.NewGuid();
        var nominationId = Guid.NewGuid();

        var entity = new MusicalInstrument { Id = instrumentId, Name = "Saxophone", NominationId = nominationId,
            Nomination = new Nomination { Id = nominationId, Name = "Nomination", MusicalInstruments = new List<MusicalInstrument>() } };
        
        _instrumentRepoMock
            .Setup(r => r.GetByIdWithNominationAsync(instrumentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync( entity );

        var model = _mapper.Map<MusicalInstrumentModel>(entity);

        // Act
        var result = await _instrumentService.GetAsync(instrumentId, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(model);
    }

    [Fact]
    public async Task GetAsync_ShouldThrowNotFoundException_WhenInstrumentNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();

        _instrumentRepoMock
            .Setup(r => r.GetByIdWithNominationAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((MusicalInstrument?)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _instrumentService.GetAsync(id, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowNotFoundException_WhenInstrumentNotFound()
    {
        // Arrange
        var model = new MusicalInstrumentModel { Id = Guid.NewGuid(), Name = "Oboe", NominationId = Guid.NewGuid(), NominationName = "nomination" };

        _instrumentRepoMock
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

        _instrumentRepoMock
            .Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<MusicalInstrument, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<MusicalInstrument> { oldEntity });

        _instrumentRepoMock
            .Setup(r => r.UpdateAsync(oldEntity, It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedEntity); 

        // Act
        var result = await _instrumentService.UpdateAsync(model, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(model);
    }

}
