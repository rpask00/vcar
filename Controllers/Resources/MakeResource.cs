﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace vcar.Controllers.Resources
{
    public class MakeResource

    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ModelResource> Models { get; set; }

        public MakeResource()
        {
            Models = new Collection<ModelResource>();
        }
    }
}
