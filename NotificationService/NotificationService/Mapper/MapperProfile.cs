using AutoMapper;
using MongoDB.Bson;
using NotificationService.Data.Dtos;
using NotificationService.Data.Entities;
using NotificationService.Data.Models;

namespace NotificationService.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<NotificationEntity, NotificationModel>().ReverseMap();
            CreateMap<NotificationDto, NotificationModel>();
                //.ForMember(d => d.UserId, u => u.MapFrom(i => ObjectId.Parse(i.UserId)));
            CreateMap<NotificationModel, NotificationDto>();
                //.ForMember(d => d.UserId, u => u.MapFrom(i => i.UserId.ToString()));

            CreateMap<EditNotificationDto, EditNotificationModel>().ReverseMap();


        }
    }
}
