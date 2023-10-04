using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public CustomerStatus Status { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public required string FirstName { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public required string LastName { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public required string PhoneNumber { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string? Email { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string? Address { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime CreateDate { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime UpdateDate { get; set; }

        public virtual ICollection<Card> Cards { get; set; } = new List<Card>();
        public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}
