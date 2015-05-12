namespace BBDM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblDam")]
    public partial class tblDam
    {
        public tblDam()
        {
            tblCalves = new HashSet<tblCalf>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Dam_SN { get; set; }

        public int DamHerd_SN { get; set; }

        [Required]
        [StringLength(1)]
        public string DamYr_Code { get; set; }

        [Required]
        [StringLength(10)]
        public string DamTag_Str { get; set; }

        public int? DamTag_Num { get; set; }

        public short DamBirthYr_Num { get; set; }

        public short? DamDispYr_Num { get; set; }

        public int? DamDisp_SN { get; set; }

        public DateTime? DamBirth_Date { get; set; }

        public DateTime? DamWean_Date { get; set; }

        public DateTime? DamYlg_Date { get; set; }

        public DateTime? DamMonth18_Date { get; set; }

        public short? DamBirth_Wt { get; set; }

        public short? DamWean_Wt { get; set; }

        public short? DamYlg_Wt { get; set; }

        public short? DamMonth18_Wt { get; set; }

        [StringLength(10)]
        public string MetalTag_Str { get; set; }

        [StringLength(10)]
        public string BangsTag_Str { get; set; }

        [StringLength(10)]
        public string PreviousDamTag_Str { get; set; }

        [Column(TypeName = "text")]
        public string ActivityLog_Txt { get; set; }

        public int? Calf_SN { get; set; }

        [StringLength(2)]
        public string zDamHerd_Code { get; set; }

        public virtual ICollection<tblCalf> tblCalves { get; set; }
    }
}
