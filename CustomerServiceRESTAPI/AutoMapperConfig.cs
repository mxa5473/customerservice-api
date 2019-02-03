using System;
using System.Collections.Generic;
using System.Linq;
using CustomerServiceRESTAPI.Entities;
using CustomerServiceRESTAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerServiceRESTAPI
{
    public class AutoMapperConfig
    {
        public static void Config()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.Client, Models.ClientDto>()
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => CreateAddress(src)));
                cfg.CreateMap<Entities.Client, Models.ClientWithTicketsAndReviewsDto>()
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => CreateAddress(src)));
                cfg.CreateMap<Models.ClientForCreationDto, Entities.Client>();

                cfg.CreateMap<Entities.Ticket, Models.TicketDto>();
                cfg.CreateMap<Entities.Ticket, Models.TicketWithClientAndAgentDto>();
                cfg.CreateMap<Models.TicketForCreationDto, Entities.Ticket>();


                cfg.CreateMap<Entities.Review, Models.ReviewDto>();
                cfg.CreateMap<Entities.Review, Models.ReviewWithClientDto>();
                cfg.CreateMap<Models.ReviewDtoForCreation, Entities.Review>();


                cfg.CreateMap<Entities.Comment, Models.CommentDto>();
                cfg.CreateMap<Entities.Comment, Models.CommentWithTicketsDto>();
                cfg.CreateMap<Models.CommentForCreationDto, Entities.Comment>();
            });
        }

        static Address CreateAddress(Client client)
        {
            return new Address
            {
                Line1 = client.AddressLine1,
                Line2 = client.AddressLine2,
                City = client.AddressCity,
                State = client.AddressState,
                Zipcode = client.AddressZipcode,
                Country = client.AddressCountry
            };
        }
    }
}
