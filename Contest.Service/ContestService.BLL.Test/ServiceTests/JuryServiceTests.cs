using AutoMapper;
using ContestService.BLL.Exceptions;
using ContestService.BLL.Mapper;
using ContestService.BLL.Models;
using ContestService.BLL.Services;
using ContestService.BLL.Tests.Customizations;
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

        [Theory]
        [CustomAutoData]
        public async Task CreateAsync_ShouldCreateJurySuccessfully(JuryModel juryModel)
        {
            // Arrange
            var juryEntity = _mapper.Map<Jury>(juryModel);
            _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<Jury>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(juryEntity);

            var cancellationToken = CancellationToken.None;

            // Act
            var result = await _juryService.CreateAsync(juryModel, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(juryModel);
            _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<Jury>(), cancellationToken), Times.Once);
        }

        [Theory]
        [CustomAutoData]
        public async Task DeleteAsync_ShouldDeleteJury_WhenJuryExists(Jury jury)
        {
            //Arrange
            var juryId = Guid.NewGuid();
            var cancellationToken = CancellationToken.None;

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

        [Theory]
        [CustomAutoData]
        public async Task GetAllAsync_ReturnsListSuccessfully(Jury jury)
        {
            //Arrange
            var cancellationToken = CancellationToken.None;
            var juriesEntity = new List<Jury>
            {
                jury, jury
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

        [Theory]
        [CustomAutoData]
        public async Task GetAsync_ReturnsJury_WhenJuryExist(Jury jury)
        {
            //Arrange
            var cancellationToken = CancellationToken.None;
            var juryId = Guid.NewGuid();
            jury.Id = juryId;
            
            var juryModel = _mapper.Map<JuryModel>(jury);

            _repositoryMock.Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Jury, bool>>>(), cancellationToken))
                .ReturnsAsync(new List<Jury> { jury});

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

        [Theory]
        [CustomAutoData]
        public async Task UpdateAsync_UpdateSuccessfully(JuryModel model)
        {
            // Arrange
            var cancellationToken = CancellationToken.None;

            var updatedEntity = _mapper.Map<Jury>(model);

            var existingEntity = new Jury
            {
                Id = updatedEntity.Id,
                Name = "Old",
                Surname = updatedEntity.Surname,
                Birthday = updatedEntity.Birthday,
                UserId = updatedEntity.UserId,
                CreatedAt = DateTime.UtcNow.AddDays(-5),
                UpdatedAt = DateTime.UtcNow.AddDays(-5),
            };

            _repositoryMock
                .Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Jury, bool>>>(), cancellationToken))
                .ReturnsAsync(new List<Jury> { existingEntity });

            _repositoryMock
                .Setup(r => r.UpdateAsync(existingEntity, cancellationToken))
                .ReturnsAsync(updatedEntity);

            // Act
            var result = await _juryService.UpdateAsync(model, cancellationToken);

            // Assert
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
