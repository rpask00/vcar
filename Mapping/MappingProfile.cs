using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vcar.Models;
using vcar.Controllers.Resources;
using AutoMapper;

namespace vcar.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Model, ModelResource>();
            CreateMap<Make, MakeResource>();
            CreateMap<Car, SaveCarResource>()
            .ForPath(cr => cr.Contact.name, opt => opt.MapFrom(c => c.ContactName))
            .ForPath(cr => cr.Contact.email, opt => opt.MapFrom(c => c.Email))
            .ForPath(cr => cr.Contact.phone, opt => opt.MapFrom(c => 0))
            .ForMember(cr => cr.Features, opt => opt.MapFrom(c => c.Features.Select(f => f.FeatureId)));

            CreateMap<SaveCarResource, Car>()
            .ForMember(c => c.Id, opt => opt.Ignore())
            .ForMember(c => c.ContactName, opt => opt.MapFrom(cr => cr.Contact.name))
            .ForMember(c => c.Email, opt => opt.MapFrom(cr => cr.Contact.email))
            .ForMember(c => c.Features, opt => opt.Ignore())
            .AfterMap((cr, c) =>
            {
                var removedFeatures = c.Features.Where(f => !cr.Features.Contains(f.FeatureId));
                foreach (var f in removedFeatures)
                    c.Features.Remove(f);

                var addFeatures = cr.Features.Where(fid => !c.Features.Any(f => f.FeatureId == fid))
                .Select(fid => new CarFeature { FeatureId = fid });

                foreach (var fid in addFeatures)
                    c.Features.Add(fid);
            });
        }
    }
}
