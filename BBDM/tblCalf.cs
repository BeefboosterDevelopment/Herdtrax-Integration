namespace BBDM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblCalf")]
    public partial class tblCalf
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Calf_SN { get; set; }

        public int Dam_SN { get; set; }

        public int CalfHerd_SN { get; set; }

        public short CalfBirthYr_Num { get; set; }

        public int CalfTag_Num { get; set; }

        [StringLength(1)]
        public string CalfYr_Code { get; set; }

        [Required]
        [StringLength(1)]
        public string Sex_Code { get; set; }

        public bool AssistedBirth_Flag { get; set; }

        public bool Grafted_Flag { get; set; }

        public bool Twin_Flag { get; set; }

        public bool Cull_BirthWt_Flag { get; set; }

        public bool Cull_BirthDate_Flag { get; set; }

        public bool Cull_ADG_Flag { get; set; }

        public bool Cull_Sex_Flag { get; set; }

        public bool Cull_Dam_Flag { get; set; }

        public bool Cull_Pull_Flag { get; set; }

        public bool Cull_WeanWt_Flag { get; set; }

/*
        public bool? zzBit3_Flag { get; set; }

        public bool? zzBit4_Flag { get; set; }

        public bool? zzBit5_Flag { get; set; }

        public bool? zzBit6_Flag { get; set; }

        public bool? zzBit7_Flag { get; set; }

        public bool? zzBit8_Flag { get; set; }
*/

        public int? GraftedFromDam_SN { get; set; }

        public int? CalfTwin_SN { get; set; }

        public int? CalfDisp_SN { get; set; }

        public DateTime? Birth_Date { get; set; }

/*
        public short? zzDaysFromMedianBirthDate_Num { get; set; }
*/

        public short? Birth_Wt { get; set; }

/*
        public short? zzBirthAdj_Wt { get; set; }

        public float? zzBirthAdjWt_Idx { get; set; }

        public short? zzBirthAdjWt_Rnk { get; set; }
*/

        public DateTime? Wean_Date { get; set; }

        public short? Wean_Wt { get; set; }

/*
        public short? zzDay180_Wt { get; set; }

        public float? zzDay180Wt_Idx { get; set; }

        public short? zzDay180Wt_Rnk { get; set; }

        public short? zzDay180Adj_Wt { get; set; }

        public float? zzDay180AdjWt_Idx { get; set; }

        public short? zzDay180AdjWt_Rnk { get; set; }

        public short? zzDay205_Wt { get; set; }

        public float? zzzDay205Wt_Idx { get; set; }

        public short? zzDay205Adj_Wt { get; set; }

        public float? zzWean_WPDA { get; set; }

        public float? zzWeanWPDA_Idx { get; set; }

        public short? zzWeanWPDA_Rnk { get; set; }

        public float? zzBirthToWean_ADG { get; set; }

        public float? zzBirthToWeanAdj_ADG { get; set; }

        public float? zzBirthToWeanAdjADG_Idx { get; set; }

        public short? zzBirthToWeanAdjADG_Rnk { get; set; }

        public double? zzPreTest_Idx { get; set; }

        public short? zzPreTest_Rnk { get; set; }
*/

/*
        [Column(TypeName = "text")]
        public string Comment_Txt { get; set; }
*/

        [StringLength(10)]
        public string Sire_Str { get; set; }

/*
        public float? zzYlgHeiferSel_Idx { get; set; }

        public short? zzYlgHeiferSel_Rnk { get; set; }
*/

        public byte? Teat_Score { get; set; }

        public byte? Udder_Score { get; set; }

        public int? CCID { get; set; }

        public int? DNA_Tag { get; set; }

        public bool Polled_Flag { get; set; }
        
        public int? HerdtraxAnimalId { get; set; }
        
        [StringLength(10)]
        public string HerdtraxTwinType { get; set; }

        public virtual tblDam tblDam { get; set; }

        public virtual tblHerdCode tblHerdCode { get; set; }
    }
}
