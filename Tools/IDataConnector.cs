using System.Data;

namespace Cards
{
    public interface IDataConnector
    {
        void AddCard(string id, string pin, string serialNumber, string accountNumber);
        void DeleteRowById(string id);
        DataTable GetData(string id, string serialNumber, string accountNumber);
        DataTable GetSelectedData(string id, string serialNumber, string accountNumber);
    }
}