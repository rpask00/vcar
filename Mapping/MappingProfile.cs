using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vcar.Models;
using vcar.Controllers.Resources;
using AutoMapper;

namespace vcar.Mapping
{
    public class MappingProfile :Profile
    {

        public MappingProfile()
        {
            CreateMap<Model, ModelResource>();
            CreateMap<Make, MakeResource>();
            CreateMap<Car, CarResource>();
        }
    }
}
