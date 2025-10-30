using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GymSystemBLL.ViewModels.SessionViewModel;
using GymSystemG2AL.Entities;
using Microsoft.Extensions.Options;

namespace GymSystemBLL
{
    public class MappingProfiles : Profile
    {
        // Profile Must be in CTOR

        public MappingProfiles()
        {
            CreateMap<Session, SessionViewModel>()
            .ForMember(dest => dest.CategoryName, Options => Options.MapFrom(Src => Src.SessionCategory.CategoryName))
            .ForMember(dest => dest.TrainerName, Options => Options.MapFrom(Src => Src.SessionTrainer.Name))
            .ForMember(dest => dest.AvailableSlots, Options => Options.Ignore());
        }
    }
}