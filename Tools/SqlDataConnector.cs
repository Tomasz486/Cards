using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace Cards
{
    /// <summary>
    ///     The connector to SQL.
    /// </summary>
    public class SqlDataConnector : IDataConnector
    {
        /// <summary>
        ///     The default connection string.
        /// </summary>
        private const string DefaultConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Cards.mdf;Integrated Security=True";

        /// <summary>
        ///     The filtered select query string.
        /// </summary>
        private const string InsertQueryString = "INSERT INTO Cards (Id, Pin, SerialNumber, AccountNumber) VALUES (@id, @pin, @serialNumber, @accountNumber)";

        /// <summary>
        ///     The validation select query.
        /// </summary>
        private const string ValidationSelectQuery = "SELECT Id, SerialNumber, AccountNumber FROM [Cards] WHERE " +
            "Id = @id " +
            "OR SerialNumber = @serialNumber " +
            "OR AccountNumber = @accountNumber";

        /// <summary>
        ///     The filtered select query.
        /// </summary>
        private const string FilteredSelectQuery = "SELECT Id, SerialNumber, AccountNumber FROM [Cards] WHERE " +
            "Id LIKE @id " +
            "AND SerialNumber LIKE @serialNumber " +
            "AND AccountNumber LIKE @accountNumber";

        /// <summary>
        ///     The delete query,
        /// </summary>
        private const string DeleteQueryById = "DELETE FROM [Cards] WHERE Id = @id";

        /// <summary>
        ///     column labels for gridview.
        /// </summary>
        private readonly Dictionary<string, string> ColumnLabelsForGridView = new Dictionary<string, string>()
        {
            { "Id", "Identyfikator"},
            { "SerialNumber", "Numer seryjny" },
            { "AccountNumber", "Numer konta" }
        };

        /// <summary>
        ///     Create database.
        /// </summary>
        /// <param name="sufix">
        ///     The optional sufix.
        /// </param>
        private void CreateDatabase(string sufix = "")
        {
            using (var connection = new SqlConnection(@"server=(localdb)\v11.0"))
            {
                connection.Open();

                var sql = string.Format(File.ReadAllText("CreateDatabase.sql").Replace("[Cards]", $"[Cards{sufix}]"), Directory.GetCurrentDirectory());

                var command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
            }

            this.CreateTable();
        }

        /// <summary>
        ///     Creates table.
        /// </summary>
        private void CreateTable()
        {
            using (var connection = new SqlConnection(DefaultConnectionString))
            {
                connection.Open();
                var command = new SqlCommand(File.ReadAllText("CreateTable.sql"), connection);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        ///     Create database if not exist.
        /// </summary>
        /// <param name="errorMessage">
        ///     The error message.
        /// </param>
        /// <returns>
        ///     The error message if fails.
        /// </returns>
        public bool CreateDataBaseIfNotExist(out string errorMessage)
        {
            errorMessage = null;

            try
            {
                this.CreateDatabase();

                return true;
            }
            catch (Exception ex)
            {
                return this.TrySolvingProblemsWithDatabase(ex, out errorMessage);
            }
        }

        /// <summary>
        ///     Tries solivng problems with database.
        /// </summary>
        /// <param name="exception">
        ///     The exception to be analyzed.
        /// </param>
        /// <param name="errorMessage">
        ///     The error message.
        /// </param>
        /// <returns>
        ///     Returns true if there is no problems with database.
        /// </returns>
        private bool TrySolvingProblemsWithDatabase(Exception exception, out string errorMessage)
        {
            errorMessage = null;

            if (!File.Exists($"{Directory.GetCurrentDirectory()}\\Cards.mdf"))
            {
                try
                {
                    this.CreateDatabase(new SimpleGeneratorId().GenerateUniqueId());
                }
                catch (Exception ex2)
                {
                    errorMessage = ex2.Message;
                    return false;
                }

                if (!exception.Message.Contains("already exists. Choose a different database name."))
                {
                    errorMessage = exception.Message;
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///     Get Data.
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <param name="serialNumber">
        ///     The serial number.
        /// </param>
        /// <param name="accountNumber">
        ///     The account number.
        /// </param>
        /// <param name="errorMessage">
        ///     The error message.
        /// </param>
        /// <returns>
        ///     Returrns data from database based on arguments.
        /// </returns>
        public DataTable GetData(string id, string serialNumber, string accountNumber, out string errorMessage)
        {
            var dt = new DataTable();

            errorMessage = null;

            try
            {
                using (var connection = new SqlConnection(DefaultConnectionString))
                {
                    var command = new SqlCommand(FilteredSelectQuery, connection);
                    command.Parameters.AddWithValue("@id", $"%{id}%");
                    command.Parameters.AddWithValue("@serialNumber", $"%{serialNumber}%");
                    command.Parameters.AddWithValue("@accountNumber", $"%{accountNumber}%");

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }

                ColumnLabelsForGridView.Keys.ToList().ForEach(
                    columnName => dt.Columns[columnName].ColumnName = ColumnLabelsForGridView[columnName]
                );
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return dt;
        }

        /// <summary>
        ///     Delete row by id.
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <param name="errorMessage">
        ///     The error message.
        /// </param>
        public void DeleteRowById(string id, out string erroMessage)
        {
            SqlConnection connection = null;

            erroMessage = null;

            try
            {
                using (connection = new SqlConnection(DefaultConnectionString))
                {
                    var command = new SqlCommand(DeleteQueryById, connection);
                    command.Parameters.Add(new SqlParameter("@id", id));

                    connection.Open();
                    command.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                erroMessage = ex.Message;
            }
        }

        /// <summary>
        ///     Add card.
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <param name="pin">
        ///     The pin.
        /// </param>
        /// <param name="serialNumber">
        ///     The serial number.
        /// </param>
        /// <param name="accountNumber">
        ///     The account number.
        /// </param>
        /// <param name="errorMessage">
        ///     The error message.
        /// </param>
        public void AddCard(string id, string pin, string serialNumber, string accountNumber, out string errorMessage)
        {
            errorMessage = null;

            try
            {
                using (var connection = new SqlConnection(DefaultConnectionString))
                {
                    var command = new SqlCommand(DeleteQueryById, connection);
                    command.CommandText = InsertQueryString;

                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@pin", pin);
                    command.Parameters.AddWithValue("@serialNumber", serialNumber);
                    command.Parameters.AddWithValue("@accountNumber", accountNumber);

                    connection.Open();
                    command.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        /// <summary>
        ///     Gets selected data database.
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <param name="serialNumber">
        ///     The serial number.
        /// </param>
        /// <param name="accountNumber">
        ///     The account number.
        /// </param>
        /// <param name="errorMessage">
        ///     The error message.
        /// </param>
        /// <returns>
        ///     The list of duplicated values.
        /// </returns>
        public DataTable GetSelectedData(string id, string serialNumber, string accountNumber, out string errorMessage)
        {
            var dt = new DataTable();

            errorMessage = null;

            try
            {
                using (var connection = new SqlConnection(DefaultConnectionString))
                {
                    var command = new SqlCommand(ValidationSelectQuery, connection);
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@serialNumber", serialNumber);
                    command.Parameters.AddWithValue("@accountNumber", accountNumber);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return dt;
        }
    }
}
