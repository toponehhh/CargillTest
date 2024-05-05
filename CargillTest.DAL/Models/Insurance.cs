using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargillTest.DAL.Models
{
    public class Insurance
    {
        [PrimaryKey, AutoIncrement]
        [Column("Insurance_ID")]
        public int InsuranceID { get; set; }
        [Column("bizdate")]
        public DateTime BizDate { get; set; }
        [Column("cp_master_id")]
        public string? CPMasterID { get; set; }
        [Column("cp_name")]
        public string? CPName { get; set; }
        [Column("limit_c_usd")]
        public decimal LimitC { get; set; }
        [Column("PD_Rate")]
        public decimal PDRate { get; set; }
        [Column("Insurance_Rate")]
        public decimal InsuranceRate { get; set; }
    }
}
