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

namespace ContestService.BLL.Tests.ServiceTests
{
    public class JuryServiceTests
    {
        private readonly Mock<IRepositoryBase<Jury>> _repositoryMock;
        private readonly IMapper _mapper;
        private readonly JuryService _juryService;

        public JuryServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>(); // та же конфигурация, что в BLL
            });
            _mapper = config.CreateMapper();

            _repositoryMock = new Mock<IRepositoryBase<Jury>>();
            _juryService = new JuryService(_repositoryMock.Object, _mapper);
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

            var juryEntity = _mapper.Map<Jury>(juryModel);

            _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<Jury>(), It.IsAny<CancellationToken>())).ReturnsAsync(juryEntity);
            var cancellationToken = CancellationToken.None;

            //Act
            var result = await _juryService.CreateAsync(juryModel, cancellationToken);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(juryModel);

            _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<Jury>(), cancellationToken), Times.Once);
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

            var juriesModels = _mapper.Map<List<JuryModel>>(juriesEntity);

            _repositoryMock.Setup(r => r.GetAllToListAsync(cancellationToken)).ReturnsAsync(juriesEntity);

            //Act 
            var result = await _juryService.GetAllAsync(cancellationToken);

            //Assert
            Assert.NotNull(result);
            result.Should().BeEquivalentTo(juriesModels);

            _repositoryMock.Verify(r => r.GetAllToListAsync(cancellationToken), Times.Once);
        }

        [Fact]
        public async Task GetAsync_ReturnsJury_WhenJuryExist()
        {
            //Arrange
            var cancellationToken = CancellationToken.None;
            var juryId = Guid.NewGuid();

            var juryEntity = new Jury
            {
                Id = juryId,
                Name = "Test",
                Surname = "Test",
                Birthday = new DateOnly(1986, 12, 12),
                UserId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var juryModel = _mapper.Map<JuryModel>(juryEntity);

            _repositoryMock.Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Jury, bool>>>(), cancellationToken))
                .ReturnsAsync(new List<Jury> { juryEntity});

            //Act
            var result = await _juryService.GetAsync(juryId, cancellationToken);

            //Assert
            result.Should().BeEquivalentTo(juryModel);

            _repositoryMock.Verify(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Jury, bool>>>(), cancellationToken), Times.Once);
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

            var model = _mapper.Map<JuryModel>(updatedEntity);

            _repositoryMock.Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Jury, bool>>>(), cancellationToken)).ReturnsAsync(new List<Jury> { existingEntity });
            _repositoryMock.Setup(r => r.UpdateAsync(existingEntity, cancellationToken)).ReturnsAsync(updatedEntity);

            //Act 
            var result = await _juryService.UpdateAsync(model, cancellationToken);

            //Assert
            result.Should().BeEquivalentTo(model);

            _repositoryMock.Verify(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Jury, bool>>>(), cancellationToken), Times.Once);
            _repositoryMock.Verify(r => r.UpdateAsync(existingEntity, cancellationToken), Times.Once);
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
