using ClientManagement.Domain.Clients;
using System.Data;

namespace ClientManagement.Infrastructure.DataPersistence.Converter
{
    public static class ClientType
    {
        public static DataTable ToDataTable(this Client client)
        {
            var table = new DataTable();

            table.Columns.Add("Id", typeof(string));
            table.Columns.Add("FirstName", typeof(string));
            table.Columns.Add("LastName", typeof(string));
            table.Columns.Add("Email", typeof(string));
            table.Columns.Add("Logo", typeof(string));

            table.Rows.Add(client.Id.ToString(), client.Name.FirstName, client.Name.LastName, client.Email, client.Logo);

            return table;
        }
    }
}
