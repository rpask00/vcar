using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataAnnotations;
using vcar.Core.Models;


namespace vcar.Core
{
    public interface IUnitOfWork
    {
        Task Complete();
    }
}
