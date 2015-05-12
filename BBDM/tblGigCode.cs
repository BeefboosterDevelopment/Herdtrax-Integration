namespace BBDM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblGigCode")]
    public partial class tblGigCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GIG_SN { get; set; }

        [StringLength(50)]
        public string GIG_Name { get; set; }

        public bool Disabled_Flag { get; set; }

        public short? HeiferAge_Num { get; set; }

        public short? DefaultBirth_Wt { get; set; }

        public float? WeanWtOverNight_Pct { get; set; }

        public float? DamWtOverNight_Pct { get; set; }
    }
}
