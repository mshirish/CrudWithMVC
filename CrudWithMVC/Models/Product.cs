using CrudWithMVC.Enums;
using System.ComponentModel.DataAnnotations;

namespace CrudWithMVC.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
        public Categories Category { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateOnly ExpiryDate { get; set; }
    }
}
