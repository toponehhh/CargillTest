using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargillTest.DAL.Models
{
    public class ReconcillationReport
    {
        public required CPMapping CPMappingData { get; set; }
        public required ARAPJDE ArApData { get; set; }
        public Insurance? InsuranceData { get; set; }
        public decimal ExpectedLoss { get; set; }
        public bool HasInsurance
        {
            get
            {
                return InsuranceData != null;
            }
        }
        public decimal InsuranceLimit { get; set; }
        public decimal NetExposure { get; set; }

        [SetsRequiredMembers]
        public ReconcillationReport(CPMapping cPMappingData, ARAPJDE arApData, Insurance? insuranceData)
        {
            CPMappingData = cPMappingData;
            ArApData = arApData;
            InsuranceData = insuranceData;
        }
    }
}
