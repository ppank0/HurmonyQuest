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
public class ParticipantServiceTests
{
    private readonly Mock<IParticipantRepository> _repositoryMock = new();
    private readonly Mock<INominationRepository> _nominationRepoMock = new();
    private readonly IMapper _mapper;
    private readonly ParticipantService _service;

    public ParticipantServiceTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>(); // та же конфигурация, что в BLL
        });
        _mapper = config.CreateMapper();

        _service = new ParticipantService(_repositoryMock.Object, _nominationRepoMock.Object, _mapper);
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

        var entity = _mapper.Map<Participant>(model);

        _nominationRepoMock.Setup(x => x.IsMusicalInstrumentInNomination(model.NominationId, model.MusicalInstrumentId)).Returns(true);
        _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<Participant>(), It.IsAny<CancellationToken>())).ReturnsAsync(entity);

        // Act
        var result = await _service.CreateAsync(model, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(model, options => options
            .Excluding(p => p.NominationId));
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
        var models = _mapper.Map<List<ParticipantModel>>(entities);

        _repositoryMock.Setup(r => r.GetAllWithRelationsAsync(It.IsAny<CancellationToken>())).ReturnsAsync(entities);

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

        var model = _mapper.Map<ParticipantModel>(entity);

        _repositoryMock.Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Participant, bool>>>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync([entity]);

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
        var oldEntity = new Participant
        {
            Id = model.Id,
            Name = "jdfhfd",
            Surname = "ifdsjfodsp",
            Birthday = model.Birthday,
            UserId = model.UserId,
            MusicalInstrumentId = model.MusicalInstrumentId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        var updatedEntity = _mapper.Map<Participant>(model);

        _repositoryMock.Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Participant, bool>>>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(new List<Participant>{oldEntity});
        _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Participant>(), It.IsAny<CancellationToken>())).ReturnsAsync(updatedEntity);

        //Act
        var result = await _service.UpdateAsync(model, CancellationToken.None);

        //Assert
        result.Should().BeEquivalentTo(model, options => options
    .Excluding(p => p.NominationId));
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
