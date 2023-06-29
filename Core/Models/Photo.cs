using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace vcar.Core.Models
{
    [Table("Photos")]
    public class Photo
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public int CarId { get; set; }
    }
}
