using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CardStorageService.Data
{
    [Table("AccountSessions")]
    public class AccountSession
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SessionId { get; set; }

        [Required]
        [StringLength(384)]
        public string SessionToken { get; set; }

        [ForeignKey(nameof(Account))]
        public int AccountId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime TimeCreate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime TimeLastRequest { get; set; }

        public bool IsClosed { get; set; }

        [Column(TypeName = "datetime2")]

        public DateTime TimeClosed { get; set; }

        public virtual Account Account { get; set; }
    }
}
