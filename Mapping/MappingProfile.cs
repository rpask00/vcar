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
            CreateMap<Car, CarResource>()
            .ForPath(vr => vr.contact.name, opt => opt.MapFrom(v => v.ContactName))
            .ForPath(vr => vr.contact.email, opt => opt.MapFrom(v => v.Email))
            .ForPath(vr => vr.contact.phone, opt => opt.MapFrom(v => 0))
            .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(f => f.FeatureId)));

            CreateMap<CarResource, Car>()
            .ForMember(v => v.Id, opt => opt.Ignore())
            .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.contact.name))
            .ForMember(v => v.Email, opt => opt.MapFrom(vr => vr.contact.email))
            .ForMember(v => v.Features, opt => opt.Ignore())
            .AfterMap((vr, v) =>
            {
                var removedFeatures = v.Features.Where(f => !vr.Features.Contains(f.FeatureId));
                foreach (var f in removedFeatures)
                    v.Features.Remove(f);

                var addFeatures = vr.Features.Where(fid => !v.Features.Any(f => f.FeatureId == fid))
                .Select(fid => new CarFeature { FeatureId = fid });

                foreach (var fid in addFeatures)
                    v.Features.Add(fid);
            });
        }
    }
}
