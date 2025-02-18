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
        private Dictionary<string, string> ColumnLabelsForGridView = new Dictionary<string, string>()
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
            var connection = new SqlConnection(@"server=(localdb)\v11.0");
            using (connection)
            {
                connection.Open();

                var sql = string.Format(File.ReadAllText("CreateDatabase.sql").Replace("[Cards]", $"[Cards{sufix}]"), Directory.GetCurrentDirectory());

                var command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();

                connection.Close();
            }

            connection = new SqlConnection(DefaultConnectionString);
            using (connection)
            {
                connection.Open();
                var command = new SqlCommand(File.ReadAllText("CreateTable.sql"), connection);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        ///     Create database if not exist.
        /// </summary>
        /// <returns>
        ///     The erreor message if fails.
        /// </returns>
        public string CreateDataBaseIfNotExist()
        {
            try
            {
                this.CreateDatabase();
                return null;
            }
            catch (Exception ex)
            {
                if (!File.Exists($"{Directory.GetCurrentDirectory()}\\Cards.mdf"))
                {
                    try
                    {
                        this.CreateDatabase(new SimpleGeneratorId().GenerateUniqueId());
                    }
                    catch (Exception ex2)
                    {
                        return ex2.Message;
                    }

                    if (!ex.Message.Contains("already exists. Choose a different database name."))
                    {
                        return ex.Message;
                    }
                }

                return null;
            }
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
        /// <returns>
        ///     Returrns data from database based on arguments.
        /// </returns>
        public DataTable GetData(string id, string serialNumber, string accountNumber)
        {
            var dt = new DataTable();

            SqlConnection connection = null;

            try
            {
                using (connection = new SqlConnection(DefaultConnectionString))
                {
                    using (var command = new SqlCommand(FilteredSelectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@id", $"%{id}%");
                        command.Parameters.AddWithValue("@serialNumber", $"%{serialNumber}%");
                        command.Parameters.AddWithValue("@accountNumber", $"%{accountNumber}%");

                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }

                ColumnLabelsForGridView.Keys.ToList().ForEach(
                    columnName => dt.Columns[columnName].ColumnName = ColumnLabelsForGridView[columnName]
                );
            }
            catch (Exception)
            {

            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return dt;
        }

        /// <summary>
        ///     Delete row by id.
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        public void DeleteRowById(string id)
        {
            SqlConnection connection = null;

            try
            {
                using (connection = new SqlConnection(DefaultConnectionString))
                {
                    using (var command = new SqlCommand(DeleteQueryById, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@id", id));

                        connection.Open();
                        command.ExecuteReader();
                    }
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
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
        public void AddCard(string id, string pin, string serialNumber, string accountNumber)
        {
            SqlConnection connection = null;

            try
            {
                using (connection = new SqlConnection(DefaultConnectionString))
                {
                    using (var command = new SqlCommand(DeleteQueryById, connection))
                    {
                        command.CommandText = InsertQueryString;

                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@pin", pin);
                        command.Parameters.AddWithValue("@serialNumber", serialNumber);
                        command.Parameters.AddWithValue("@accountNumber", accountNumber);

                        connection.Open();
                        command.ExecuteReader();
                    }
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
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
        /// <returns>
        ///     The list of duplicated values.
        /// </returns>
        public DataTable GetSelectedData(string id, string serialNumber, string accountNumber)
        {
            var dt = new DataTable();

            SqlConnection connection = null;

            try
            {
                using (connection = new SqlConnection(DefaultConnectionString))
                {
                    using (var command = new SqlCommand(ValidationSelectQuery, connection))
                    {
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
            }
            catch (Exception)
            {

            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return dt;
        }
    }
}
