namespace BBDM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblHerdCode")]
    public partial class tblHerdCode
    {
        public tblHerdCode()
        {
            tblCalves = new HashSet<tblCalf>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Herd_SN { get; set; }

        public int GeneticHerd_SN { get; set; }

        [StringLength(20)]
        public string Herd_Code { get; set; }

        [StringLength(50)]
        public string Brand_Str { get; set; }

        [StringLength(50)]
        public string TagColor_Str { get; set; }

        [StringLength(5)]
        public string TagColorShort_Str { get; set; }

        public DateTime LastChangedOn { get; set; }

        public bool Disabled_Flag { get; set; }

        public bool Trial_Flag { get; set; }

        public virtual ICollection<tblCalf> tblCalves { get; set; }
    }
}
