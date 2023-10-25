using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities
{
    public class Wallet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string? Name { get; set; }
        [Precision(11, 2)]
        public decimal Balance { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime CreateDate { get; set; }
        public WalletStatus Status { get; set; }
        public bool IsDefaultWallet { get; set; }

        public int CardId { get; set; }
        [ForeignKey(nameof(CardId))]
        public virtual Card Card { get; set; } = null!;

        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
