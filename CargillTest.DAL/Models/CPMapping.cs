using SQLite;

namespace CargillTest.DAL.Models
{
    [Table("CP_Mappings")]
    public class CPMapping
    {
        [PrimaryKey]
        [Column("abcode_number")]
        public int AbcodeNumber { get; set; }
        [Column("SalesForce_CP_Name")]
        public string? SalesForceCPName { get; set; }
        [Column("JDE_CP_NAME")]
        public string? JDECPName { get; set; }
        [Column("PD_Rate")]
        public decimal PDRate { get; set; }
    }
}
