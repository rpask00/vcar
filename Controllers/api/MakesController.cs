using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAnnotations;
using vcar.Controllers.Resources;
using AutoMapper;
using vcar.Core.Models;

namespace vcar.Controllers.api
{
    [Route("api/makes")]
    public class MakesController : Controller
    {
        private readonly VcarContext _context;
        private readonly IMapper _mapper;   

        public MakesController(VcarContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            var makes = await _context.Makes.Include(m => m.Models).ToListAsync();
            return _mapper.Map<List<Make>, List<MakeResource>>(makes);
        }

    }
}
