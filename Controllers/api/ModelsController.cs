using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAnnotations;
using vcar.Controllers.Resources;
using AutoMapper;
using vcar.Core.Models;

namespace vcar.Controllers.api
{
    [Route("api/Models")]
    public class ModelsController : Controller
    {
        private readonly VcarContext _context;
        private readonly IMapper _mapper;

        public ModelsController(VcarContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Models
        [HttpGet]
        public async Task<IEnumerable<ModelResource>> GetModels()
        {
            var models = await _context.Models.Include(m => m.Make).ToListAsync();
            return _mapper.Map<List<Model>, List<ModelResource>>(models);
        }
    }
}