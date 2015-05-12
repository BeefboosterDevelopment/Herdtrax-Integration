using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBDM
{
    [Table("stblSN")]
    public class SN
    {
        [Key]
       // [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Last_SN { get; set; }
    }
}