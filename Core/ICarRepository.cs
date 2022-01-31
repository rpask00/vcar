using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataAnnotations;
using vcar.Core.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace vcar.Core
{
    public interface ICarRepository
    {
        Task<QueryResult<Car>> GetAll(CarQuery carQuery, bool loadExternal = false);
        Task<Car> Get(int id, bool loadExternal = false);
        void Add(Car Car);
        void Remove(Car car);
    }
}