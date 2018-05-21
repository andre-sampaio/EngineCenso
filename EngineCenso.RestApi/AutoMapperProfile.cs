using AutoMapper;
using EngineCenso.DataAccess;
using EngineCenso.RestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngineCenso.RestApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CensoMappingInsertModel, CensoMapping>()
                .ForMember(x => x.InternalId, config => config.Ignore())
                .ReverseMap();

            CreateMap<CensoMappingUpdateModel, CensoMapping>()
                .ForMember(x => x.InternalId, config => config.Ignore())
                .ForMember(x => x.Name, config => config.Ignore())
                .ReverseMap();
        }
    }
}
