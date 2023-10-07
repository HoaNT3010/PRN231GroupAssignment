using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Domain.Enums;

namespace Domain.Entities
{
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Precision(11, 2)]
        public decimal TotalPrice { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime CreateDate { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string? Description { get; set; }
        public InvoiceStatus Status { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer Customer { get; set; } = null!;
        public int StaffId { get; set; }
        [ForeignKey(nameof(StaffId))]
        public virtual Staff Staff { get; set; } = null!;

        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
