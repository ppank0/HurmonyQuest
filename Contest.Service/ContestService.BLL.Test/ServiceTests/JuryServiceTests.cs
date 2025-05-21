using AutoMapper;
using ContestService.BLL.Exceptions;
using ContestService.BLL.Models;
using ContestService.BLL.Services;
using ContestService.DAL.Entities;
using ContestService.DAL.Repositories.Interfaces;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;

namespace ContestService.BLL.Tests.ServiceTests
{
    public class JuryServiceTests
    {
        private readonly Mock<IRepositoryBase<Jury>> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly JuryService _juryService;

        public JuryServiceTests()
        {
            _repositoryMock = new Mock<IRepositoryBase<Jury>>();
            _mapperMock = new Mock<IMapper>();
            _juryService = new JuryService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateJurySuccessfully()
        {
            //Arrange
            var juryModel = new JuryModel
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Surname = "Test",
                Birthday = new DateOnly(1986, 12, 5),
                UserId = Guid.NewGuid(),
            };

            var juryEntity = new Jury
            {
                Id = juryModel.Id,
                Name = juryModel.Name,
                Surname = juryModel.Surname,
                Birthday = juryModel.Birthday,
                UserId = juryModel.UserId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _mapperMock.Setup(m => m.Map<Jury>(juryModel)).Returns(juryEntity);
            _repositoryMock.Setup(r => r.CreateAsync(juryEntity, It.IsAny<CancellationToken>())).ReturnsAsync(juryEntity);
            _mapperMock.Setup(m => m.Map<JuryModel>(juryEntity)).Returns(juryModel);
            var cancellationToken = CancellationToken.None;

            //Act
            var result = await _juryService.CreateAsync(juryModel, cancellationToken);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(juryModel);

            _mapperMock.Verify(m => m.Map<Jury>(juryModel), Times.Once);
            _repositoryMock.Verify(r => r.CreateAsync(juryEntity, cancellationToken), Times.Once);
            _mapperMock.Verify(m => m.Map<JuryModel>(juryEntity), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteJury_WhenJuryExists()
        {
            //Arrange
            var juryId = Guid.NewGuid();
            var cancellationToken = CancellationToken.None;

            var jury = new Jury
            {
                Id = juryId,
                Name = "Test",
                Surname = "Test",
                Birthday = new DateOnly(1986, 12, 12),
                UserId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _repositoryMock.Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Jury, bool>>>(), cancellationToken))
                                                                                        .ReturnsAsync(new List<Jury> { jury });

            //Act
            await _juryService.DeleteAsync(juryId, cancellationToken);

            // Assert
            _repositoryMock.Verify(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Jury, bool>>>(), cancellationToken), Times.Once);
            _repositoryMock.Verify(r => r.DeleteAsync(jury, cancellationToken), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowNotFoundException_WhenJuryNotFound()
        {
            //Arrange
            var juryId = Guid.NewGuid();
            var cancellationToken = CancellationToken.None;

            _repositoryMock.Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Jury, bool>>>(), cancellationToken))
                                                                                            .ReturnsAsync(new List<Jury>());

            //Act
            await Assert.ThrowsAsync<NotFoundException>(() => _juryService.DeleteAsync(juryId, cancellationToken));

            // Assert
            _repositoryMock.Verify(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Jury, bool>>>(), cancellationToken), Times.Once);
            _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Jury>(), cancellationToken), Times.Never);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsListSuccessfully()
        {
            //Arrange
            var cancellationToken = CancellationToken.None;
            var juriesEntity = new List<Jury>
            {
                new Jury{Id = Guid.NewGuid(), Name = "Test", Surname = "Test", Birthday = new DateOnly(1986, 12, 12), UserId = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow},
                new Jury{Id = Guid.NewGuid(), Name = "Test2", Surname = "Test2", Birthday = new DateOnly(1986, 6, 2), UserId = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow},
            };

            var juriesModels = new List<JuryModel>
            {
                new JuryModel{Id = juriesEntity[0].Id, Name = juriesEntity[0].Name, Surname = juriesEntity[0].Surname, Birthday = juriesEntity[0].Birthday, UserId = juriesEntity[0].UserId},
                new JuryModel{Id = juriesEntity[1].Id, Name = juriesEntity[1].Name, Surname = juriesEntity[1].Surname, Birthday = juriesEntity[1].Birthday, UserId = juriesEntity[1].UserId},
            };

            _repositoryMock.Setup(r => r.GetAllToListAsync(cancellationToken)).ReturnsAsync(juriesEntity);
            _mapperMock.Setup(m => m.Map<List<JuryModel>>(juriesEntity)).Returns(juriesModels);

            //Act 
            var result = await _juryService.GetAllAsync(cancellationToken);

            //Assert
            Assert.NotNull(result);
            result.Should().BeEquivalentTo(juriesModels);

            _repositoryMock.Verify(r => r.GetAllToListAsync(cancellationToken), Times.Once);
            _mapperMock.Verify(m => m.Map<List<JuryModel>>(juriesEntity), Times.Once);
        }

        [Fact]
        public async Task GetAsync_ReturnsJury_WhenJuryExist()
        {
            //Arrange
            var cancellationToken = CancellationToken.None;
            var juryId = Guid.NewGuid();


            var juryEntites = new List<Jury>
            {
                new Jury { Id = juryId, Name = "Test", Surname = "Test", Birthday = new DateOnly(1986, 12, 12), UserId = Guid.NewGuid(),
                        CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow}
            };

            var juryModel = new JuryModel
            {
                Id = juryId,
                Name = juryEntites[0].Name,
                Surname = juryEntites[0].Surname,
                Birthday = juryEntites[0].Birthday,
                UserId = juryEntites[0].UserId
            };

            _mapperMock.Setup(m => m.Map<JuryModel>(juryEntites[0])).Returns(juryModel);

            _repositoryMock.Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Jury, bool>>>(), cancellationToken))
                .ReturnsAsync(juryEntites);

            //Act
            var result = await _juryService.GetAsync(juryId, cancellationToken);

            //Assert
            result.Should().BeEquivalentTo(juryModel);

            _repositoryMock.Verify(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Jury, bool>>>(), cancellationToken), Times.Once);
            _mapperMock.Verify(m => m.Map<JuryModel>(juryEntites[0]), Times.Once);
        }

        [Fact]
        public async Task GetAsync_ThrowNotFoundException_WhenJuryNotFound()
        {
            //Arrange
            var cancellationToken = CancellationToken.None;
            var juryId = Guid.NewGuid();

            _repositoryMock.Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Jury, bool>>>(), cancellationToken))
                .ReturnsAsync(new List<Jury>());

            //Act
            await Assert.ThrowsAsync<NotFoundException>(() => _juryService.GetAsync(juryId, cancellationToken));

            //Assert
            _repositoryMock.Verify(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Jury, bool>>>(), cancellationToken), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_UpdateSuccessfully()
        {
            //Arrange
            var cancellationToken = CancellationToken.None;
            var juryId = Guid.NewGuid();
            var existingEntity = new Jury
            {
                Id = juryId,
                Name = "Old",
                Surname = "Name",
                Birthday = new DateOnly(1986, 12, 12),
                UserId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var updatedEntity = new Jury
            {
                Id = juryId,
                Name = "New",
                Surname = "Name",
                Birthday = new DateOnly(1986, 12, 12),
                UserId = existingEntity.UserId,
                CreatedAt = existingEntity.CreatedAt,
                UpdatedAt = DateTime.UtcNow
            };

            var model = new JuryModel
            {
                Id = juryId,
                Name = "New",
                Surname = "Name",
                Birthday = updatedEntity.Birthday,
                UserId = updatedEntity.UserId
            };

            _repositoryMock.Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Jury, bool>>>(), cancellationToken)).ReturnsAsync(new List<Jury> { existingEntity });
            _mapperMock.Setup(m => m.Map(model, existingEntity));
            _repositoryMock.Setup(r => r.UpdateAsync(existingEntity, cancellationToken)).ReturnsAsync(updatedEntity);
            _mapperMock.Setup(m => m.Map<JuryModel>(updatedEntity)).Returns(model);

            //Act 
            var result = await _juryService.UpdateAsync(model, cancellationToken);

            //Assert
            result.Should().BeEquivalentTo(model);

            _repositoryMock.Verify(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Jury, bool>>>(), cancellationToken), Times.Once);
            _mapperMock.Verify(m => m.Map(model, existingEntity), Times.Once);
            _repositoryMock.Verify(r => r.UpdateAsync(existingEntity, cancellationToken), Times.Once);
            _mapperMock.Verify(m => m.Map<JuryModel>(updatedEntity), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ThrowNotFoundException()
        {
            //Arrange
            var cancellationToken = CancellationToken.None;
            var juryId = Guid.NewGuid();

            _repositoryMock.Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Jury, bool>>>(), cancellationToken))
                .ReturnsAsync(new List<Jury>());

            //Act
            await Assert.ThrowsAsync<NotFoundException>(() => _juryService.GetAsync(juryId, cancellationToken));

            //Assert
            _repositoryMock.Verify(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Jury, bool>>>(), cancellationToken), Times.Once);
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Jury>(), cancellationToken), Times.Never);

        }
    }
}
