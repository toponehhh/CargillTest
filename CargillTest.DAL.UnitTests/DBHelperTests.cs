using CargillTest.DAL.Models;

namespace CargillTest.DAL.UnitTests
{
    public class DBHelperTests
    {
        ~DBHelperTests() 
        {
            try
            {
                var testDBFiles = Directory.GetFiles(Environment.CurrentDirectory, "*.db");
                if (testDBFiles.Length > 0)
                {
                    foreach (var testDBFile in testDBFiles)
                    {
                        File.Delete(testDBFile);
                    }
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
        }

        [Fact]
        public void CreateDBTest()
        {
            const string testDBFileName = "UnitTestDB.db";
            var fakeInsuranceDAO = new DBHelper<Insurance>(testDBFileName);
            Assert.True(File.Exists(Path.Combine(Environment.CurrentDirectory, testDBFileName)));
        }

        [Fact]
        public void InsertEntityTest()
        {
            const string testDBFileName = "InsertUnitTestDB.db";
            var fakeCPMappingDAO = new DBHelper<CPMapping>(testDBFileName);
            var insertedLineCount = fakeCPMappingDAO.Insert(new CPMapping { AbcodeNumber = 1234, PDRate = 0.152m, JDECPName = "TestCPJDEName", SalesForceCPName = "TestSFCPName" });
            Assert.Equal(1, insertedLineCount);
        }

        [Fact]
        public void QueryAllEntityTest()
        {
            const string testDBFileName = "QueryAllUnitTestDB.db";
            var fakeARAPJDEDAO = new DBHelper<ARAPJDE>(testDBFileName);
            fakeARAPJDEDAO.Insert(new ARAPJDE { ACCode = "62189.1", Description = "Test DESC 1", SupplierCode = "1003", SupplierName = "Test Supplier 1", ContractNo = "P0798", DueDate = new DateTime(2033, 1, 30), AmountInJDE = 12088.19m });
            fakeARAPJDEDAO.Insert(new ARAPJDE { ACCode = "6189.2", Description = "Test DESC 2", SupplierCode = "2003", SupplierName = "Test Supplier 2", ContractNo = "P1805", DueDate = new DateTime(2043, 2, 3), AmountInJDE = 3596.33m });
            fakeARAPJDEDAO.Insert(new ARAPJDE { ACCode = "6119.3", Description = "Test DESC 3", SupplierCode = "3003", SupplierName = "Test Supplier 3", ContractNo = "P2806", DueDate = new DateTime(2053, 3, 30), AmountInJDE = 2615.09m });
            fakeARAPJDEDAO.Insert(new ARAPJDE { ACCode = "629.4", Description = "Test DESC 4", SupplierCode = "4003", SupplierName = "Test Supplier 4", ContractNo = "P9802", DueDate = new DateTime(2063, 4, 5), AmountInJDE = 9559.50m });

            var queryResult = fakeARAPJDEDAO.QueryAll();

            Assert.Equal(4, queryResult.Count);
        }

        [Fact]
        public void UpdateEntityTest()
        {
            const string testDBFileName = "UpdateUnitTestDB.db";
            var fakeInsuranceDAO = new DBHelper<Insurance>(testDBFileName);
            fakeInsuranceDAO.Insert(new Insurance { BizDate = new DateTime(2033, 11, 30), CPMasterID = "CP00012", CPName = "Racking Test", LimitC = 2126m, PDRate = 0.55m, InsuranceRate = 0.95m });
            var fakeInsurances = fakeInsuranceDAO.QueryAll();
            var updateInsurance = fakeInsurances.FirstOrDefault();
            int updateCount = 0;
            if (updateInsurance != null)
            {
                updateInsurance.InsuranceRate = 0.85m;
                updateCount = fakeInsuranceDAO.Update(updateInsurance);
            }

            Assert.Equal(1, updateCount);
        }
    }
}