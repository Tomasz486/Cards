using System.Data;

namespace Cards.Tools
{
    public class FormValidation
    {
        /// <summary>
        ///     Compares data from form and database.
        /// </summary>
        /// <param name="dataTable">
        ///     The data from database.
        /// </param>
        /// <param name="serialNumber">
        ///     The serial number.
        /// </param>
        /// <param name="accountNumber">
        ///     The account number.
        /// </param>
        /// <returns>
        ///     Returns duplicated field name or empty string if no fieldb is duplicated.
        /// </returns>
        public string CompareDataFromFormAndDatabase(DataTable dataTable, string serialNumber, string accountNumber)
        {
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (serialNumber == dataTable.Rows[i]["SerialNumber"].ToString())
                    {
                        return "Numer seryjny";
                    }

                    if (accountNumber == dataTable.Rows[i]["AccountNumber"].ToString())
                    {
                        return "Numer konta";
                    }
                }
            }

            return null;
        }
    }
}
