using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FullSearchSample.Entity
{
    [Table("Documents")]
    public class Document
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Content { get; set; }

    }
}
