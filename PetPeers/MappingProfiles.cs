using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace PetPeers
{
    [ExcludeFromCodeCoverage]
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //CreateMap<SiteDetailDto, Sitelist_By_Clientid>().ReverseMap();
            //CreateMap<List<SiteDetailDto>, List<Sitelist_By_Clientid>>().ReverseMap();
        }
    }
}
