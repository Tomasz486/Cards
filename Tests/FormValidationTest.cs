using System.Data;
using Cards.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cards.Tests
{
    [TestClass]
    public class FormValidationTest
    {
        /// <summary>
        ///     Init dataTable for tests.
        /// </summary>
        /// <param name="serialNumber">
        ///     The serial number.
        /// </param>
        /// <param name="accountNumber">
        ///     The account number.
        /// </param>
        /// <returns>
        ///     The data table for tests.
        /// </returns>
        private DataTable InitDataTable(string serialNumber, string accountNumber)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("SerialNumber");
            dataTable.Columns.Add("AccountNumber");

            DataRow row = dataTable.NewRow();
            row["SerialNumber"] = serialNumber;
            row["AccountNumber"] = accountNumber;

            dataTable.Rows.Add(row);

            return dataTable;
        }


        [TestMethod]
        [DataRow("0123456789", "0987654321", "0234567890", "1987654321")]
        public void CompareDataFromFormAndDatabaseIfAreNotTheSameTest(string serialNumberFromDb, string accountNumberFromFormDb, string serialNumberFromForm, string accountNumberFromForm)
        {
            Assert.IsNull(new FormValidation().CompareDataFromFormAndDatabase(InitDataTable(serialNumberFromDb, accountNumberFromFormDb), serialNumberFromForm, accountNumberFromForm));
        }

        [TestMethod]
        [DataRow("0000000000", "0987654321", "0000000000", "1987654321")]
        public void CompareDataFromFormAndDatabaseIfSerialNumberIsTheSameTest(string serialNumberFromDb, string accountNumberFromFormDb, string serialNumberFromForm, string accountNumberFromForm)
        {
            Assert.IsNotNull(new FormValidation().CompareDataFromFormAndDatabase(InitDataTable(serialNumberFromDb, accountNumberFromFormDb), serialNumberFromForm, accountNumberFromForm));
        }

        [TestMethod]
        [DataRow("0987654321", "0000000000", "1987654321", "0000000000")]
        public void CompareDataFromFormAndDatabaseIfAccountNumberIsTheSameTest(string serialNumberFromDb, string accountNumberFromFormDb, string serialNumberFromForm, string accountNumberFromForm)
        {
            Assert.IsNotNull(new FormValidation().CompareDataFromFormAndDatabase(InitDataTable(serialNumberFromDb, accountNumberFromFormDb), serialNumberFromForm, accountNumberFromForm));
        }
    }
}
