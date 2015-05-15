using System;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace BBDM
{
    public class BBModel : DbContext
    {
        public BBModel()
            : base("name=BBModel")
        {
        }

        public virtual DbSet<SN> SN { get; set; }
        public virtual DbSet<tblCalf> tblCalves { get; set; }
        public virtual DbSet<tblDam> tblDams { get; set; }
        public virtual DbSet<tblGeneticHerdCode> tblGeneticHerdCodes { get; set; }
        public virtual DbSet<tblGigCode> tblGigCodes { get; set; }
        public virtual DbSet<tblHerdCode> tblHerdCodes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblCalf>()
                .Property(e => e.CalfYr_Code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tblCalf>()
                .Property(e => e.Sex_Code)
                .IsFixedLength()
                .IsUnicode(false);

/*            modelBuilder.Entity<tblCalf>()
                .Property(e => e.Comment_Txt)
                .IsUnicode(false);
 */

            modelBuilder.Entity<tblCalf>()
                .Property(e => e.Sire_Str)
                .IsUnicode(false);

            modelBuilder.Entity<tblDam>()
                .Property(e => e.DamYr_Code)
                .IsUnicode(false);

            modelBuilder.Entity<tblDam>()
                .Property(e => e.DamTag_Str)
                .IsUnicode(false);

            modelBuilder.Entity<tblDam>()
                .Property(e => e.MetalTag_Str)
                .IsUnicode(false);

            modelBuilder.Entity<tblDam>()
                .Property(e => e.BangsTag_Str)
                .IsUnicode(false);

            modelBuilder.Entity<tblDam>()
                .Property(e => e.PreviousDamTag_Str)
                .IsUnicode(false);

            modelBuilder.Entity<tblDam>()
                .Property(e => e.ActivityLog_Txt)
                .IsUnicode(false);

            modelBuilder.Entity<tblDam>()
                .Property(e => e.zDamHerd_Code)
                .IsUnicode(false);

            modelBuilder.Entity<tblDam>()
                .HasMany(e => e.tblCalves)
                .WithRequired(e => e.tblDam)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblGeneticHerdCode>()
                .Property(e => e.GeneticHerd_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tblGeneticHerdCode>()
                .Property(e => e.Strain_Code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tblGeneticHerdCode>()
                .Property(e => e.RecChange_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tblGeneticHerdCode>()
                .Property(e => e.AccountNo)
                .IsUnicode(false);

            modelBuilder.Entity<tblGigCode>()
                .Property(e => e.GIG_Name)
                .IsUnicode(false);

            modelBuilder.Entity<tblHerdCode>()
                .Property(e => e.Herd_Code)
                .IsUnicode(false);

            modelBuilder.Entity<tblHerdCode>()
                .Property(e => e.Brand_Str)
                .IsUnicode(false);

            modelBuilder.Entity<tblHerdCode>()
                .Property(e => e.TagColor_Str)
                .IsUnicode(false);

            modelBuilder.Entity<tblHerdCode>()
                .Property(e => e.TagColorShort_Str)
                .IsUnicode(false);

            modelBuilder.Entity<tblHerdCode>()
                .HasMany(e => e.tblCalves)
                .WithRequired(e => e.tblHerdCode)
                .HasForeignKey(e => e.CalfHerd_SN)
                .WillCascadeOnDelete(false);
        }

        public int LookupDamHerd(int damSN)
        {
            if (damSN <= 0) return 0;
            var dam = tblDams.Find(damSN);
            return dam != null ? dam.DamHerd_SN : 0;
        }

        public int GetLastSN()
        {
            return SN.First().Last_SN;
        }

        // return the first available SN in the reserved range
        public int ReserveSNRange(int numberToReserve)
        {
            //const string sql = @"UPDATE [stblSN] SET [Last_SN] = [Last_SN] + @num";
            /*
             CREATE PROCEDURE [dbo].[ReserveSN] @numToReserve int, @lastSN int OUTPUT
                AS
                BEGIN
                UPDATE stblSN SET Last_SN = Last_SN + @numToReserve
                SELECT @lastSN = Last_SN FROM stblSN
                END
             */

            var lastSNReservedParam = new SqlParameter
            {
                ParameterName = "lastSN",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            Database.ExecuteSqlCommand("EXEC ReserveSN @numToReserve, @lastSN OUTPUT",
                new SqlParameter("@numToReserve", numberToReserve), lastSNReservedParam);


            var newLastSN = Convert.ToInt32(lastSNReservedParam.Value);
            return newLastSN - numberToReserve + 1;
        }
    }
}