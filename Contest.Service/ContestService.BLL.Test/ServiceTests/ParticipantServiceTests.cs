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
public class ParticipantServiceTests
{
    private readonly Mock<IRepositoryBase<Participant>> _repositoryMock = new();
    private readonly Mock<INominationRepository> _nominationRepoMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly ParticipantService _service;

    public ParticipantServiceTests()
    {
        _service = new ParticipantService(_repositoryMock.Object, _nominationRepoMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateParticipant_WhenInstrumentMatchesNomination()
    {
        // Arrange
        var model = new ParticipantModel
        {
            Id = Guid.NewGuid(),
            Name = "Anna",
            Surname = "Smith",
            Birthday = new DateOnly(2000, 1, 1),
            MusicalInstrumentId = Guid.NewGuid(),
            NominationId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        var entity = new Participant { Id = model.Id, Name = model.Name, Surname = model.Surname, Birthday = model.Birthday,
            UserId = model.UserId,  MusicalInstrumentId = model.MusicalInstrumentId, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow};

        _nominationRepoMock.Setup(x => x.IsMusicalInstrumentInNomination(model.NominationId, model.MusicalInstrumentId)).Returns(true);
        _mapperMock.Setup(m => m.Map<Participant>(model)).Returns(entity);
        _repositoryMock.Setup(r => r.CreateAsync(entity, It.IsAny<CancellationToken>())).ReturnsAsync(entity);
        _mapperMock.Setup(m => m.Map<ParticipantModel>(entity)).Returns(model);

        // Act
        var result = await _service.CreateAsync(model, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(model);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrowBadRequest_WhenInstrumentDoesNotMatchNomination()
    {
        var model = new ParticipantModel
        {
            Id = Guid.NewGuid(),
            Name = "Anna",
            Surname = "Smith",
            Birthday = new DateOnly(2000, 1, 1),
            NominationId = Guid.NewGuid(),
            MusicalInstrumentId = Guid.NewGuid()
        };

        _nominationRepoMock.Setup(x => x.IsMusicalInstrumentInNomination(model.NominationId, model.MusicalInstrumentId)).Returns(false);

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() => _service.CreateAsync(model, CancellationToken.None));
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteParticipant_WhenExists()
    {
        var id = Guid.NewGuid();
        var entity = new Participant
        {
            Id = Guid.NewGuid(),
            Name = "Anna",
            Surname = "Smith",
            Birthday = new DateOnly(2000, 1, 1),
            MusicalInstrumentId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _repositoryMock.Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Participant, bool>>>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(new List<Participant> { entity });

        // Act
        await _service.DeleteAsync(id, CancellationToken.None);

        // Assert
        _repositoryMock.Verify(r => r.DeleteAsync(entity, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowNotFound_WhenParticipantNotExists()
    {
        var id = Guid.NewGuid();
        _repositoryMock.Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Participant, bool>>>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync([]);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteAsync(id, CancellationToken.None));
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnListOfParticipants()
    {
        //Arrange
        var entities = new List<Participant> { new Participant
        {
            Id = Guid.NewGuid(),
            Name = "Anna",
            Surname = "Smith",
            Birthday = new DateOnly(2000, 1, 1),
            MusicalInstrumentId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        } };
        var models = new List<ParticipantModel> { new ParticipantModel 
        { 
            Id = entities[0].Id,
            Name = entities[0].Name,
            Surname = entities[0].Surname,
            Birthday = entities[0].Birthday,
            MusicalInstrumentId = entities[0].MusicalInstrumentId,
        }};

        _repositoryMock.Setup(r => r.GetAllToListAsync(It.IsAny<CancellationToken>())).ReturnsAsync(entities);
        _mapperMock.Setup(m => m.Map<List<ParticipantModel>>(entities)).Returns(models);

        //Act
        var result = await _service.GetAllAsync(CancellationToken.None);

        //Assert
        result.Should().BeEquivalentTo(models);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnParticipant_WhenExists()
    {
        //Arrange
        var id = Guid.NewGuid();
        var entity = new Participant
        {
            Id = Guid.NewGuid(),
            Name = "Anna",
            Surname = "Smith",
            Birthday = new DateOnly(2000, 1, 1),
            MusicalInstrumentId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var model = new ParticipantModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Surname = entity.Surname,
            Birthday = entity.Birthday,
            NominationId = Guid.NewGuid(),
            MusicalInstrumentId = entity.MusicalInstrumentId,
        };

        _repositoryMock.Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Participant, bool>>>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync([entity]);
        _mapperMock.Setup(m => m.Map<ParticipantModel>(entity)).Returns(model);

        //Act
        var result = await _service.GetAsync(id, CancellationToken.None);

        //Assert
        result.Should().BeEquivalentTo(model);
    }

    [Fact]
    public async Task GetAsync_ShouldThrowNotFound_WhenParticipantDoesNotExist()
    {
        //Arrange
        var id = Guid.NewGuid();
        _repositoryMock.Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Participant, bool>>>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync([]);

        //Act&Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetAsync(id, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateParticipant_WhenExists()
    {
        //Arrange
        var model = new ParticipantModel
        {
            Id = Guid.NewGuid(),
            Name = "Anna",
            Surname = "Smith",
            Birthday = new DateOnly(2000, 1, 1),
            NominationId = Guid.NewGuid(),
            MusicalInstrumentId = Guid.NewGuid()
        };

        var entity = new Participant { Id = model.Id, Name = model.Name, Surname = model.Surname, Birthday = model.Birthday,
            UserId = model.UserId, MusicalInstrumentId = model.MusicalInstrumentId, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow};

        _repositoryMock.Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Participant, bool>>>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync([entity]);
        _mapperMock.Setup(m => m.Map(model, entity));
        _repositoryMock.Setup(r => r.UpdateAsync(entity, It.IsAny<CancellationToken>())).ReturnsAsync(entity);
        _mapperMock.Setup(m => m.Map<ParticipantModel>(entity)).Returns(model);

        //Act
        var result = await _service.UpdateAsync(model, CancellationToken.None);

        //Assert
        result.Should().BeEquivalentTo(model);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowNotFound_WhenParticipantDoesNotExist()
    {
        //Arrange
        var model = new ParticipantModel
        {
            Id = Guid.NewGuid(),
            Name = "Anna",
            Surname = "Smith",
            Birthday = new DateOnly(2000, 1, 1),
            NominationId = Guid.NewGuid(),
            MusicalInstrumentId = Guid.NewGuid()
        };

        _repositoryMock.Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Participant, bool>>>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync([]);
        //Act&Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _service.UpdateAsync(model, CancellationToken.None));
    }


}
