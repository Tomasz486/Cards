using System.Data;

namespace Cards
{
    public interface IDataConnector
    {
        void AddCard(string id, string pin, string serialNumber, string accountNumber, out string errorMessage);
        bool CreateDataBaseIfNotExist(out string errorMessage);
        void DeleteRowById(string id, out string erroMessage);
        DataTable GetData(string id, string serialNumber, string accountNumber, out string errorMessage);
        DataTable GetSelectedData(string id, string serialNumber, string accountNumber, out string errorMessage);
    }
}