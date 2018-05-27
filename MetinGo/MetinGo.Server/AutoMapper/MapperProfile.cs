using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MetinGo.Server.Entities;

namespace MetinGo.Server.AutoMapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Monster, ApiModel.Monster.Monster>();
            CreateMap<Character, ApiModel.Character.Character>();
            CreateMap<Item, ApiModel.Item.Item>();
            CreateMap<CharacterItem, ApiModel.Item.CharacterItem>();

            CreateMap<Character, Common.Character>();
            CreateMap<Monster, Fight.Model.Monster>();
        }
    }
}
