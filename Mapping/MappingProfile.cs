using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vcar.Core.Models;
using vcar.Controllers.Resources;
using AutoMapper;

namespace vcar.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));

            CreateMap<Model, ModelResource>();
            CreateMap<Make, MakeResource>();
            CreateMap<Photo, PhotoResource>();
            CreateMap<CarQueryResource, CarQuery>();
            CreateMap<Car, SaveCarResource>()
            .ForPath(cr => cr.Contact.Name, opt => opt.MapFrom(c => c.ContactName))
            .ForPath(cr => cr.Contact.Email, opt => opt.MapFrom(c => c.Email))
            .ForPath(cr => cr.Contact.Phone, opt => opt.MapFrom(c => 0))
            .ForMember(cr => cr.Features, opt => opt.MapFrom(c => c.Features.Select(f => f.FeatureId)));

            CreateMap<SaveCarResource, Car>()
            .ForMember(c => c.Id, opt => opt.Ignore())
            .ForMember(c => c.ContactName, opt => opt.MapFrom(cr => cr.Contact.Name))
            .ForMember(c => c.Email, opt => opt.MapFrom(cr => cr.Contact.Email))
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


            CreateMap<Car, CarResource>()
            .ForPath(cr => cr.Contact.Name, opt => opt.MapFrom(c => c.ContactName))
            .ForPath(cr => cr.Contact.Email, opt => opt.MapFrom(c => c.Email))
            .ForPath(cr => cr.Contact.Phone, opt => opt.MapFrom(c => 0))
            .ForMember(cr => cr.Make, opt => opt.MapFrom(c => c.Model.Make))
            .ForMember(cr => cr.Features, opt => opt.MapFrom(c =>
                c.Features.Select(f => new FeatureResource { Id = f.FeatureId, Name = f.Feature.Name })
            ));


        }
    }
}
