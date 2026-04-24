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
            CreateMap<NotificationDto, NotificationModel>().ReverseMap();
        }
    }
}
