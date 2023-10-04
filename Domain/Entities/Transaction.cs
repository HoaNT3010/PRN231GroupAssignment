using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Precision(11, 2)]
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public TransactionMethod TransactionMethod { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string? Description { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string? EWalletTransaction { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime CreateDate { get; set; }
        public TransactionStatus Status { get; set; }

        public int WalletId { get; set; }
        [ForeignKey(nameof(WalletId))]
        public virtual Wallet Wallet { get; set; } = null!;
        public int? StaffId { get; set; }
        [ForeignKey(nameof(StaffId))]
        public virtual Staff? Staff { get; set; }
        public int? InvoiceId { get; set; }
        [ForeignKey(nameof(InvoiceId))]
        public virtual Invoice? Invoice { get; set; }
    }
}
