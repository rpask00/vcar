using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAnnotations;
using AutoMapper;
using vcar.Controllers.Resources;
using vcar.Persistance;
using vcar.Core.Models;
using vcar.Core;

namespace vcar.Controllers.api
{
    [Route("api/cars")]
    public class CarsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly ICarRepository _CarRepository;

        public CarsController(IMapper mapper, ICarRepository CarRepository, IUnitOfWork UnitOfWork)
        {
            _mapper = mapper;
            _CarRepository = CarRepository;
            _UnitOfWork = UnitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetCars(FilterResource filterResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var filter = _mapper.Map<FilterResource, Filter>(filterResource);
            var cars = await _CarRepository.GetAll(filter, loadExternal: true);

            if (cars == null)
                return NotFound();

            var carsResource = cars.Select(c => _mapper.Map<Car, CarResource>(c));
            return Ok(carsResource);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCar(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var car = await _CarRepository.Get(id, loadExternal: true);

            if (car == null)
                return NotFound();

            return Ok(_mapper.Map<Car, CarResource>(car));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SaveCarResource scr)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var car = _mapper.Map<SaveCarResource, Car>(scr);
            car.lasUpdate = DateTime.Now;

            _CarRepository.Add(car);
            await _UnitOfWork.Complete();
            car = await _CarRepository.Get(car.Id, loadExternal: true);

            return Ok(_mapper.Map<Car, CarResource>(car));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SaveCarResource carResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var car = await _CarRepository.Get(id, loadExternal: true);
            _mapper.Map<SaveCarResource, Car>(carResource, car);
            car.lasUpdate = DateTime.Now;

            await _UnitOfWork.Complete();
            car = await _CarRepository.Get(car.Id, loadExternal: true);

            return Ok(_mapper.Map<Car, CarResource>(car));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var car = await _CarRepository.Get(id);

            if (car == null)
                return NotFound();

            _CarRepository.Remove(car);
            await _UnitOfWork.Complete();

            return Ok(car);
        }



    }
}
