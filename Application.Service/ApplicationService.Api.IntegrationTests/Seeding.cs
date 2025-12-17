using ApplicationService.BLL.Integrations.Contracts.Instruments.DTOs;
using ApplicationService.BLL.Integrations.Contracts.Participants.DTOs;
using ApplicationService.BLL.Models;
using ApplicationService.DAL.Entities;
using ApplicationService.DAL.Enum;

namespace ApplicationService.Api.IntegrationTests
{
    public class Seeding
    {
        public static List<ApplicationEntity> GetApplications()
        {
            var videos = GetVideos();
            return new List<ApplicationEntity>()
            {
                new ApplicationEntity(){Id = Guid.NewGuid(), ParticipantId = Guid.NewGuid(), InstrumentId = Guid.NewGuid(),
                    NominationId = Guid.NewGuid(), VideoId = videos[0].Id, Status = ApplicationStatus.RendingReview, Video = videos[0]},

                new ApplicationEntity(){Id = Guid.NewGuid(), ParticipantId = Guid.NewGuid(), InstrumentId = Guid.NewGuid(),
                    NominationId = Guid.NewGuid(), VideoId = videos[1].Id, Status = ApplicationStatus.RendingReview, Video = videos[1]},

                new ApplicationEntity(){Id = Guid.NewGuid(), ParticipantId = Guid.NewGuid(), InstrumentId = Guid.NewGuid(),
                    NominationId = Guid.NewGuid(), VideoId = videos[2].Id, Status = ApplicationStatus.RendingReview, Video = videos[2]},
            };
        }

        public static List<VideoEntity> GetVideos()
        {
            return new List<VideoEntity>()
            {
                new VideoEntity(){Id = Guid.NewGuid(), VideoUrl = "url1" },
                new VideoEntity(){Id = Guid.NewGuid(), VideoUrl = "url2" },
                new VideoEntity(){Id = Guid.NewGuid(), VideoUrl = "url3" },
            };
        }

        public static async Task<VideoModel> GetVideoModel()
        {
            return new VideoModel()
            {
                Id = Guid.NewGuid(),
                VideoUrl = "Video",
            };
        }

        public static ParticipantResponse GetParticipant(Guid Id)
        {
            return new ParticipantResponse(Id, "Name", "Surname", new DateOnly(2005, 10, 9));
        }
        public static InstrumentResponse GetInstrument(Guid Id)
        {
            return new InstrumentResponse(Id, "InstrumentName", Guid.NewGuid());
        }

        public static ApplicationEntity GetApplication()
        {
            var video = new VideoEntity() { Id = Guid.NewGuid(), VideoUrl = "video1" };
            return new ApplicationEntity()
            {
                Id = Guid.NewGuid(),
                ParticipantId = Guid.NewGuid(),
                InstrumentId = Guid.NewGuid(),
                NominationId = Guid.NewGuid(),
                VideoId = video.Id,
                Status = ApplicationStatus.RendingReview,
                Video = video
            };
        }
    }
}
