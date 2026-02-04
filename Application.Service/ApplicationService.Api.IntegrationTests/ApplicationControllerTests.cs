using Application.Service.Dtos;
using ApplicationService.BLL.Integrations.Contracts.Participants.DTOs;
using ApplicationService.DAL.Context;
using ApplicationService.DAL.Entities;
using ApplicationService.DAL.Enum;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace ApplicationService.Api.IntegrationTests
{
    public class ApplicationControllerTests(AppWebApplicationFactory _factory) : IClassFixture<AppWebApplicationFactory>, IAsyncLifetime
    {
        private HttpClient _client;
        private AppDbContext _dbContext;
        private IServiceScope _scope;

        public async Task InitializeAsync()
        {
            _client = _factory.CreateClient();
            _scope = _factory.Services.CreateScope();
            _dbContext = _scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await ClearDatabaseAsync();

            _factory.InstrumentMock.Reset();
            _factory.ParticipantMock.Reset();
            _factory.VideoServiceMock.Reset();
        }

        protected async Task ClearDatabaseAsync()
        {
            _dbContext.Applications.RemoveRange(_dbContext.Applications);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DisposeAsync()
        {
            await ClearDatabaseAsync();
            _client.Dispose();
            await _dbContext.DisposeAsync();
            _scope.Dispose();
        }

        [Fact]
        public async Task GetAll_AppsExists_SuccessfulResponse()
        {
            //Arrange
            _dbContext.AddRange(Seeding.GetApplications());
            await _dbContext.SaveChangesAsync();

            //Act
            var response = await _client.GetAsync("/api/application");

            //Assert
            var result = await response.Content.ReadFromJsonAsync<List<ApplicationDto>>();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            result!.Count.Should().Be(3);
        }

        [Fact]
        public async Task GetAll_NoApps_NotFoundException()
        {
            //Act
            var response = await _client.GetAsync("/api/application");

            //Assert
            var result = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
            result.Should().Contain("NotFoundException");
        }

        [Fact]
        public async Task UpdateStatusAsync_AppExists_StatusChangedSuccessfully()
        {
            //Arrange
            var app = _dbContext.Add(Seeding.GetApplication());
            await _dbContext.SaveChangesAsync();

            _factory.ParticipantMock.Setup(x => x.GetAsync(app.Entity.ParticipantId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Seeding.GetParticipant(app.Entity.ParticipantId));

            _factory.InstrumentMock.Setup(x => x.GetByIdAsync(app.Entity.InstrumentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Seeding.GetInstrument(app.Entity.InstrumentId));

            var content = JsonContent.Create(ApplicationStatus.Rejected);

            //Act
            var response = await _client.PatchAsync($"/api/application/{app.Entity.Id}", content);

            //Assert
            var result = await response.Content.ReadFromJsonAsync<ApplicationDto>();

            result.As<ApplicationDto>().Should().NotBeNull();
            result!.Status.Should().Be(ApplicationStatus.Rejected);
        }

        [Fact]
        public async Task UpdateStatusAsync_AppNotExists_ThrowNotFoundException()
        {
            //Arrange
            var app = Seeding.GetApplication();
            var content = JsonContent.Create(ApplicationStatus.Rejected);

            //Act
            var response = await _client.PatchAsync($"/api/application/{app.Id}", content);

            //Assert
            var result = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
            result.Should().Contain("NotFoundException");
        }

        [Fact]
        public async Task GetById_NotFound_ReturnException()
        {
            //Arrange
            var app = Seeding.GetApplication();

            //Act 
            var response = await _client.GetAsync($"api/application/{app.Id}");

            //Assert
            var result = await response.Content.ReadAsStringAsync();
            result.Should().Contain("NotFoundException");
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task GetById_AppExists_ReturnAppDto()
        {
            //Arrange
            var app = _dbContext.Applications.Add(Seeding.GetApplication());
            _dbContext.SaveChanges();

            _factory.ParticipantMock.Setup(x => x.GetAsync(app.Entity.ParticipantId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Seeding.GetParticipant(app.Entity.ParticipantId));

            _factory.InstrumentMock.Setup(x => x.GetByIdAsync(app.Entity.InstrumentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Seeding.GetInstrument(app.Entity.InstrumentId));

            //Act 
            var response = await _client.GetAsync($"api/application/{app.Entity.Id}");

            //Assert
            var result = await response.Content.ReadFromJsonAsync<ApplicationDto>();
            result.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Delete_NotFound_ReturnException()
        {
            //Arrange
            var app = Seeding.GetApplication();

            //Act 
            var response = await _client.DeleteAsync($"api/application/{app.Id}");

            //Assert
            var result = await response.Content.ReadAsStringAsync();
            result.Should().Contain("NotFoundException");
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task Delete_AppExists_ReturnOk()
        {
            //Arrange
            var app = _dbContext.Applications.Add(Seeding.GetApplication());
            _dbContext.SaveChanges();

            _factory.ParticipantMock.Setup(x => x.GetAsync(app.Entity.ParticipantId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Seeding.GetParticipant(app.Entity.ParticipantId));

            _factory.InstrumentMock.Setup(x => x.GetByIdAsync(app.Entity.InstrumentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Seeding.GetInstrument(app.Entity.InstrumentId));

            //Act 
            var response = await _client.DeleteAsync($"api/application/{app.Entity.Id}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CreateApplication_CorrectData_ReturnCreatedDto()
        {
            //Arrange
            var participantId = Guid.NewGuid();
            var videoModel = await Seeding.GetVideoModel();
            _dbContext.Videos.Add(new VideoEntity() { Id = videoModel.Id, VideoUrl = videoModel.VideoUrl });
            _dbContext.SaveChanges();

            _factory.VideoServiceMock.Setup(x => x.PutAsync(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<Stream>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(videoModel);

            _factory.ParticipantMock.Setup(x => x.CreateAsync(It.IsAny<ParticipantCreateRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Seeding.GetParticipant(Guid.NewGuid()));

            _factory.InstrumentMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Seeding.GetInstrument(Guid.NewGuid()));

            var fileBytes = Encoding.UTF8.GetBytes("fake video content");
            using var fileStream = new MemoryStream(fileBytes);

            var multipartContent = new MultipartFormDataContent
            {
                { new StringContent("John"), "name" },
                { new StringContent("Doe"), "surname" },
                { new StringContent("2000-01-01"), "birthday" },
                { new StringContent(Guid.NewGuid().ToString()), "musicalInstrumentId" },
                { new StringContent(Guid.NewGuid().ToString()), "nominationId" },
            };

            var fileContent = new StreamContent(fileStream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("video/mp4");
            multipartContent.Add(fileContent, "file", "test_video.mp4");

            //Act
            var response = await _client.PostAsync($"api/application", multipartContent);

            //Assert
            var result = await response.Content.ReadFromJsonAsync<ApplicationDto>();

            result.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.InstrumentName.Should().BeEquivalentTo("instrumentName");
        }
    }
}
