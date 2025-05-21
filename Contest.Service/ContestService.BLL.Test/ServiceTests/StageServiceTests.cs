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
public class StageServiceTests
{
    private readonly Mock<IRepositoryBase<Stage>> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly StageService _stageService;

    public StageServiceTests()
    {
        _repositoryMock = new Mock<IRepositoryBase<Stage>>();
        _mapperMock = new Mock<IMapper>();
        _stageService = new StageService(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateStage_WhenValid()
    {
        // Arrange
        var model = new StageModel { Id = Guid.NewGuid(), Name = "Stage 1", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(1) };
        var entity = new Stage { Id = model.Id, Name = model.Name, StartDate = model.StartDate, EndDate = model.EndDate };

        _mapperMock.Setup(m => m.Map<Stage>(model)).Returns(entity);
        _repositoryMock.Setup(r => r.CreateAsync(entity, It.IsAny<CancellationToken>())).ReturnsAsync(entity);
        _mapperMock.Setup(m => m.Map<StageModel>(entity)).Returns(model);

        // Act
        var result = await _stageService.CreateAsync(model, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(model);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteStage_WhenExists()
    {
        //Arrange
        var id = Guid.NewGuid();
        var entity = new Stage { Id = id, Name = "Stage 1", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(1) };

        _repositoryMock
            .Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Stage, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([entity]);

        // Act
        await _stageService.DeleteAsync(id, CancellationToken.None);

        // Assert
        _repositoryMock.Verify(r => r.DeleteAsync(entity, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowNotFoundException_WhenNotExists()
    {
        //Arrange
        var id = Guid.NewGuid();

        _repositoryMock
            .Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Stage, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _stageService.DeleteAsync(id, CancellationToken.None));
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllStages()
    {
        //Arrange
        var entities = new List<Stage>
        {
            new() { Id = Guid.NewGuid(), Name = "Stage 1", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(1) }
        };
        var models = new List<StageModel>
        {
            new() { Id = entities[0].Id, Name = entities[0].Name, StartDate = entities[0].StartDate, EndDate = entities[0].EndDate }
        };

        _repositoryMock.Setup(r => r.GetAllToListAsync(It.IsAny<CancellationToken>())).ReturnsAsync(entities);
        _mapperMock.Setup(m => m.Map<List<StageModel>>(entities)).Returns(models);

        //Act
        var result = await _stageService.GetAllAsync(CancellationToken.None);

        //Assert
        result.Should().BeEquivalentTo(models);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnStage_WhenExists()
    {
        //Arrange
        var id = Guid.NewGuid();
        var entity = new Stage { Id = id, Name = "Stage 1", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(1) };
        var model = new StageModel { Id = id, Name = entity.Name, StartDate = entity.StartDate, EndDate = entity.EndDate };

        _repositoryMock
            .Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Stage, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([entity]);
        _mapperMock.Setup(m => m.Map<StageModel>(entity)).Returns(model);

        //Act
        var result = await _stageService.GetAsync(id, CancellationToken.None);

        //Assert
        result.Should().BeEquivalentTo(model);
    }

    [Fact]
    public async Task GetAsync_ShouldThrowNotFoundException_WhenNotExists()
    {
        //Arrange
        var id = Guid.NewGuid();

        _repositoryMock
            .Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Stage, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        //Act&Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _stageService.GetAsync(id, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateStage_WhenExists()
    {
        //Arrange
        var model = new StageModel { Id = Guid.NewGuid(), Name = "Updated Stage", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(1) };
        var entity = new Stage { Id = model.Id, Name = "Old Stage", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow };

        _repositoryMock
            .Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Stage, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([entity]);

        _mapperMock.Setup(m => m.Map(model, entity));
        _repositoryMock.Setup(r => r.UpdateAsync(entity, It.IsAny<CancellationToken>())).ReturnsAsync(entity);
        _mapperMock.Setup(m => m.Map<StageModel>(entity)).Returns(model);

        //Act
        var result = await _stageService.UpdateAsync(model, CancellationToken.None);

        //Assert
        result.Should().BeEquivalentTo(model);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowNotFoundException_WhenNotExists()
    {
        //Arrange
        var model = new StageModel { Id = Guid.NewGuid(), Name = "Stage", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow };

        _repositoryMock
            .Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Stage, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        //Act&Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _stageService.UpdateAsync(model, CancellationToken.None));
    }

}
