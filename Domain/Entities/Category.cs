using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public required string Name { get; set; }
        public CategoryStatus Status { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
