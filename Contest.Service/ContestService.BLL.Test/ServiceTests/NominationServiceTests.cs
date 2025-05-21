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
public class NominationServiceTests
{
    private readonly NominationService _nominationService;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IRepositoryBase<Nomination>> _repositoryMock;

    public NominationServiceTests()
    {
        _mapperMock = new Mock<IMapper>();
        _repositoryMock = new Mock<IRepositoryBase<Nomination>>();
        _nominationService = new NominationService(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateNominationSuccessfully()
    {
        //Arrange
        var cancellationToken = CancellationToken.None;
        var model = new NominationModel
        {
            Id = Guid.NewGuid(),
            Name = "Test"
        };

        var entity = new Nomination
        {
            Id = model.Id,
            Name = model.Name,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _mapperMock.Setup(m => m.Map<Nomination>(model)).Returns(entity);
        _repositoryMock.Setup(r => r.CreateAsync(entity, cancellationToken)).ReturnsAsync(entity);
        _mapperMock.Setup(m => m.Map<NominationModel>(entity)).Returns(model);

        //Act
        var result = await _nominationService.CreateAsync(model, cancellationToken);

        //Assert
        result.Should().BeEquivalentTo(model);

        _repositoryMock.Verify(r => r.CreateAsync(entity, cancellationToken), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteNominationSuccessfully()
    {
        //Arrange
        var cancellationToken = CancellationToken.None;
        var nominationId = Guid.NewGuid();

        var entity = new Nomination
        {
            Id = nominationId,
            Name = "nomination Name",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _repositoryMock.Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Nomination, bool>>>(), cancellationToken))
                                                                .ReturnsAsync(new List<Nomination> { entity });
        _repositoryMock.Setup(r => r.DeleteAsync(entity, cancellationToken));

        //Act
        await _nominationService.DeleteAsync(nominationId, cancellationToken);

        //Assert
        _repositoryMock.Verify(r => r.DeleteAsync(entity, cancellationToken), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowNotFoundException_WhenNominationNotFound()
    {
        //Arrange
        var cancellationToken = CancellationToken.None;
        var nominationId = Guid.NewGuid();

        _repositoryMock.Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Nomination, bool>>>(), cancellationToken))
                                                                .ReturnsAsync(new List<Nomination>());
        //Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _nominationService.DeleteAsync(nominationId, cancellationToken));
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnListOfNominations()
    {
        // Arrange
        var ct = CancellationToken.None;
        var entities = new List<Nomination>
    {
        new() { Id = Guid.NewGuid(), Name = "Solo", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        new() { Id = Guid.NewGuid(), Name = "Duet", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
    };

        var models = entities.Select(e => new NominationModel { Id = e.Id, Name = e.Name }).ToList();

        _repositoryMock.Setup(r => r.GetAllToListAsync(ct)).ReturnsAsync(entities);
        _mapperMock.Setup(m => m.Map<List<NominationModel>>(entities)).Returns(models);

        // Act
        var result = await _nominationService.GetAllAsync(ct);

        // Assert
        result.Should().BeEquivalentTo(models);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnNomination_WhenExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity = new Nomination { Id = id, Name = "Group", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };

        _repositoryMock.Setup(r =>
            r.FindByConditionAsync(It.IsAny<Expression<Func<Nomination, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Nomination> { entity });

        // Act
        var result = await _nominationService.GetAsync(id, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(new NominationModel { Id = id, Name = "Group" });
    }

    [Fact]
    public async Task GetAsync_ShouldThrowNotFoundException_WhenNominationNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();

        _repositoryMock.Setup(r =>
            r.FindByConditionAsync(It.IsAny<Expression<Func<Nomination, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Nomination>());

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() =>
            _nominationService.GetAsync(id, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateAndReturnNomination()
    {
        // Arrange
        var id = Guid.NewGuid();
        var model = new NominationModel { Id = id, Name = "Updated Name" };
        var entity = new Nomination { Id = id, Name = "Old Name", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };

        _repositoryMock.Setup(r =>
            r.FindByConditionAsync(It.IsAny<Expression<Func<Nomination, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Nomination> { entity });

        _mapperMock.Setup(m => m.Map(model, entity));
        _repositoryMock.Setup(r => r.UpdateAsync(entity, It.IsAny<CancellationToken>()))
            .ReturnsAsync(entity);

        _mapperMock.Setup(m => m.Map<NominationModel>(entity)).Returns(model);

        // Act
        var result = await _nominationService.UpdateAsync(model, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(model);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowNotFoundException_WhenNominationNotFound()
    {
        // Arrange
        var model = new NominationModel { Id = Guid.NewGuid(), Name = "Test" };

        _repositoryMock.Setup(r =>
            r.FindByConditionAsync(It.IsAny<Expression<Func<Nomination, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Nomination>());

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() =>
            _nominationService.UpdateAsync(model, CancellationToken.None));
    }

}
