using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAnnotations;
using vcar.Core.Models;

namespace vcar.Controllers.api
{
    [Route("api/Features")]
    public class FeaturesController : Controller
    {
        private readonly VcarContext _context;

        public FeaturesController(VcarContext context)
        {
            _context = context;
        }

        // GET: Features
        [HttpGet]
        public async Task<ICollection<Feature>> GetFeatures()
        {
            return await _context.Features.ToListAsync();
        }


    }
}
