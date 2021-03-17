using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAnnotations;
using vcar.Models;
using AutoMapper;
using vcar.Controllers.Resources;

namespace vcar.Controllers.api
{
    [Route("api/cars")]
    public class CarsController : Controller
    {
        private readonly VcarContext _context;
        private readonly IMapper _mapper;

        public CarsController(VcarContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CarResource carResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var car = _mapper.Map<CarResource, Car>(carResource);

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            carResource = _mapper.Map<Car, CarResource>(car);

            return Ok(carResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CarResource carResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var car = await _context.Cars.Include(c => c.Features).SingleAsync(c => c.Id == id);
            _mapper.Map<CarResource, Car>(carResource, car);

            await _context.SaveChangesAsync();

            carResource = _mapper.Map<Car, CarResource>(car);

            return Ok(carResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var car = await _context.Cars.FindAsync(id);

            if (car == null)
                return NotFound();

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return Ok(car);
        }
    }
}
