using CargillTest.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace CargillTest.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReconcillationReportController : ControllerBase
    {
        private readonly ILogger<ReconcillationReportController> _logger;
        private IEnumerable<ReconcillationReport> _reconcillations;

        public ReconcillationReportController(ILogger<ReconcillationReportController> logger, IEnumerable<ReconcillationReport> reports)
        {
            _logger = logger;
            _reconcillations = reports;
        }

        [HttpGet(Name = "GetReconcillationReport")]
        public IEnumerable<ReconcillationReport> Get()
        {
            foreach (var reconcillation in _reconcillations)
            {
                reconcillation.ExpectedLoss = reconcillation.ArApData.AmountInJDE * reconcillation.CPMappingData.PDRate;
                if (reconcillation.HasInsurance)
                {
                    reconcillation.InsuranceLimit = reconcillation.InsuranceData!.LimitC / _reconcillations.Count(re => !string.IsNullOrWhiteSpace(re.CPMappingData.SalesForceCPName) && re.CPMappingData.SalesForceCPName.Equals(reconcillation.CPMappingData.SalesForceCPName));
                    var JDESummary = _reconcillations.Where(re => !string.IsNullOrWhiteSpace(re.ArApData.SupplierCode) && re.ArApData.SupplierCode.Equals(reconcillation.ArApData.SupplierCode, StringComparison.InvariantCultureIgnoreCase)).Sum(re => re.ArApData.AmountInJDE);
                    if (JDESummary * reconcillation.InsuranceData.InsuranceRate < reconcillation.InsuranceData.LimitC)
                    {
                        reconcillation.NetExposure = reconcillation.ArApData.AmountInJDE - reconcillation.ArApData.AmountInJDE * reconcillation.InsuranceData.InsuranceRate;
                    }
                    else
                    {
                        reconcillation.NetExposure = reconcillation.InsuranceLimit;
                    }
                }
                else
                {
                    reconcillation.InsuranceLimit = 0;
                    reconcillation.NetExposure = reconcillation.ArApData.AmountInJDE;
                }
            }
            return _reconcillations;
        }
    }
}
