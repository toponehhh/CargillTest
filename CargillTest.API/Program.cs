
using CargillTest.DAL;
using CargillTest.DAL.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Linq;

namespace CargillTest.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            InitData();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton<IEnumerable<ReconcillationReport>>(provider => { return FillReconcillationReportData(); });

            builder.Services.AddCors(options=>
            {
                options.AddDefaultPolicy(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.UseCors();

            app.MapControllers();

            app.Run();
        }

        public static IEnumerable<ReconcillationReport> FillReconcillationReportData()
        {
            var cpMappingDAO = new DBHelper<CPMapping>();
            var cpMappings = cpMappingDAO.QueryAll();
            var ARAPJDEDAO = new DBHelper<ARAPJDE>();
            var ARAPs = ARAPJDEDAO.QueryAll();
            var InsuranceDAO = new DBHelper<Insurance>();
            var Insurances = InsuranceDAO.QueryAll();

            List<ReconcillationReport> reports = new List<ReconcillationReport>();
            foreach (var arapline in ARAPs)
            {
                var relatedCP = cpMappings.FirstOrDefault(cp =>!string.IsNullOrWhiteSpace(cp.SalesForceCPName) && cp.SalesForceCPName.Equals(arapline.SupplierName, StringComparison.InvariantCultureIgnoreCase));
                var relatedInsurance = Insurances.FirstOrDefault(ins => !string.IsNullOrWhiteSpace(ins.CPName) && ins.CPName.Equals(arapline.SupplierName, StringComparison.InvariantCultureIgnoreCase));
                if (relatedCP != null)
                {
                    reports.Add(new ReconcillationReport(relatedCP, arapline, relatedInsurance));
                }
            }
            return reports;
        }

        public static void InitData()
        {
            var cpMappingDAO = new DBHelper<CPMapping>();
            var cpMappingLines = cpMappingDAO.QueryAll();
            if (cpMappingLines.Count < 2)
            {
                cpMappingDAO.Insert(new CPMapping { AbcodeNumber = 12356, SalesForceCPName = "Steel Limited", JDECPName = "Steel Limited Test", PDRate = 0.257m });
                cpMappingDAO.Insert(new CPMapping { AbcodeNumber = 3003, SalesForceCPName = "CHINA Global", JDECPName = "CHINA Global", PDRate = 0.36m });
            }

            var ARAPJDEDAO = new DBHelper<ARAPJDE>();
            var ARAPLines = ARAPJDEDAO.QueryAll();
            if (ARAPLines.Count < 10)
            {
                ARAPJDEDAO.Insert(new ARAPJDE { ACCode = "6211189.1", Description = "A/R Trade-FLAT", SupplierCode = "3003", SupplierName = "CHINA Global", ContractNo = "P0798", DueDate = new DateTime(2023, 11, 30), AmountInJDE = 142088.19m });
                ARAPJDEDAO.Insert(new ARAPJDE { ACCode = "6211189.1", Description = "A/R Trade-FLAT", SupplierCode = "3003", SupplierName = "CHINA Global", ContractNo = "P0805", DueDate = new DateTime(2023, 11, 30), AmountInJDE = 30596.33m });
                ARAPJDEDAO.Insert(new ARAPJDE { ACCode = "6211189.1", Description = "A/R Trade-FLAT", SupplierCode = "3003", SupplierName = "CHINA Global", ContractNo = "P0806", DueDate = new DateTime(2023, 11, 30), AmountInJDE = 26015.09m });
                ARAPJDEDAO.Insert(new ARAPJDE { ACCode = "6211189.1", Description = "A/R Trade-FLAT", SupplierCode = "3003", SupplierName = "CHINA Global", ContractNo = "P0802", DueDate = new DateTime(2023, 11, 30), AmountInJDE = 95599.50m });

                ARAPJDEDAO.Insert(new ARAPJDE { ACCode = "6211189.1", Description = "A/R Trade-FLAT", SupplierCode = "12356", SupplierName = "Steel Limited", ContractNo = "S1169", DueDate = new DateTime(2023, 12, 20), AmountInCTRM = 1205113.35m, AmountInJDE = 12051.35m });
                ARAPJDEDAO.Insert(new ARAPJDE { ACCode = "6211189.1", Description = "A/R Trade-FLAT", SupplierCode = "12356", SupplierName = "Steel Limited", ContractNo = "S1168", DueDate = new DateTime(2023, 12, 20), AmountInCTRM = 928527.25m, AmountInJDE = 9227.25m });
                ARAPJDEDAO.Insert(new ARAPJDE { ACCode = "6211189.1", Description = "A/R Trade-FLAT", SupplierCode = "12356", SupplierName = "Steel Limited", ContractNo = "S1160", DueDate = new DateTime(2023, 12, 20), AmountInCTRM = 1002061.90m, AmountInJDE = 12061.90m });
                ARAPJDEDAO.Insert(new ARAPJDE { ACCode = "6211189.1", Description = "A/R Trade-FLAT", SupplierCode = "12356", SupplierName = "Steel Limited", ContractNo = "S1269", DueDate = new DateTime(2023, 12, 20), AmountInCTRM = 1032969.01m, AmountInJDE = 10369.01m });
                ARAPJDEDAO.Insert(new ARAPJDE { ACCode = "6211189.1", Description = "A/R Trade-FLAT", SupplierCode = "12356", SupplierName = "Steel Limited", ContractNo = "S1369", DueDate = new DateTime(2023, 12, 20), AmountInCTRM = 946199.18m, AmountInJDE = 9469.18m });
                ARAPJDEDAO.Insert(new ARAPJDE { ACCode = "6211189.1", Description = "A/R Trade-FLAT", SupplierCode = "12356", SupplierName = "Steel Limited", ContractNo = "S4169", DueDate = new DateTime(2023, 12, 20), AmountInCTRM = 760206.15m, AmountInJDE = 7606.15m });
            }

            var InsuranceDAO = new DBHelper<Insurance>();
            var InsuranceLines = InsuranceDAO.QueryAll();
            if (InsuranceLines.Count < 2)
            {
                InsuranceDAO.Insert(new Insurance { BizDate = new DateTime(2023, 11, 30), CPMasterID = "CP00002", CPName = "CRM Racking Test", LimitC = 21286m, PDRate = 0.25m, InsuranceRate = 0.9m });
                InsuranceDAO.Insert(new Insurance { BizDate = new DateTime(2023, 11, 30), CPMasterID = "CP00002", CPName = "CHINA Global", LimitC = 53215m, PDRate = 0.36m, InsuranceRate = 0.9m });
            }
        }
    }
}
