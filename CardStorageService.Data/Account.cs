using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardStorageService.Data
{
    [Table("Accounts")]
    public class Account
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int AccountId { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(100)]
        public string PasswordSalt { get; set; }

        [StringLength(100)]

        public string PasswordHash { get; set; }

        public bool Locked { get; set; }

        [StringLength(255)]
        public string Firstname { get; set; }

        [StringLength(255)]
        public string Lastname { get; set; }

        [StringLength(255)]
        public string Secondname { get; set; }

        [InverseProperty(nameof(AccountSession.Account))]
        public virtual ICollection<AccountSession> Sessions { get; set; } = new HashSet<AccountSession>();
    }
}
