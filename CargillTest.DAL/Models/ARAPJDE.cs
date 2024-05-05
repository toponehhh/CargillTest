using SQLite;

namespace CargillTest.DAL.Models
{
    [Table("ARAPJDE")]
    public class ARAPJDE
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }
        [Column("AC_Code")]
        public string? ACCode {  get; set; }
        public string? Description { get; set; }
        [Column("Supplier_Code")]
        public string? SupplierCode { get; set; }
        [Column("Supplier_Name")]
        public string? SupplierName { get; set; }
        [Column("Contract_No")]
        public string? ContractNo { get; set; }
        [Column("Due_Date")]
        public DateTime DueDate { get; set; }
        [Column("Amount_In_CTRM")]
        public decimal? AmountInCTRM { get; set; }
        [Column("Amount_In_JDE")]
        public decimal AmountInJDE { get; set; }
    }
}
