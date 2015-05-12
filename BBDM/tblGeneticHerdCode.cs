namespace BBDM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblGeneticHerdCode")]
    public partial class tblGeneticHerdCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GeneticHerd_SN { get; set; }

        [Required]
        [StringLength(50)]
        public string GeneticHerd_Name { get; set; }

        public int GIG_SN { get; set; }

        [Required]
        [StringLength(2)]
        public string Strain_Code { get; set; }

        public int? BreederPerson_SN { get; set; }

        public DateTime? RecCreate_Date { get; set; }

        public DateTime? RecChange_Date { get; set; }

        [StringLength(25)]
        public string RecChange_Name { get; set; }

        [StringLength(20)]
        public string AccountNo { get; set; }

        [StringLength(6)]
        public string CCIA_ProducerPIN_Str { get; set; }

        [StringLength(20)]
        public string CCIA_ProducerAccountNo_Str { get; set; }

        [StringLength(20)]
        public string CCIA_ProducerPremise_Str { get; set; }
    }
}
