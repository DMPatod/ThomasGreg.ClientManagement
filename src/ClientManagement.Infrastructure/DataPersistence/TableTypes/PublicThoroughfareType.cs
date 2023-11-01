using ClientManagement.Domain.Clients.Entities;
using System.Data;

namespace ClientManagement.Infrastructure.DataPersistence.TableTypes
{
    public static class PublicThoroughfareType
    {
        private static void ConfigureColumns(this DataTable table)
        {
            table.Columns.Add("Id", typeof(string));
            table.Columns.Add("Street", typeof(string));
            table.Columns.Add("Number", typeof(int));
            table.Columns.Add("City", typeof(string));
            table.Columns.Add("State", typeof(string));
            table.Columns.Add("AditionalInformation", typeof(string));
        }
        public static DataTable ToDataTable(this PublicThoroughfare publicThoroughfare)
        {
            var table = new DataTable();

            table.ConfigureColumns();

            table.Rows.Add(publicThoroughfare.Id.ToString(), publicThoroughfare.Street, publicThoroughfare.Number, publicThoroughfare.City, publicThoroughfare.State, publicThoroughfare.AditionalInformation);
            return table;
        }

        public static DataTable ToDataTable(this IReadOnlyCollection<PublicThoroughfare> publicThoroughfares)
        {
            var table = new DataTable();

            table.ConfigureColumns();

            foreach (var item in publicThoroughfares)
            {
                table.Rows.Add(item.Id.ToString(), item.Street, item.Number, item.City, item.State, item.AditionalInformation);
            }

            return table;
        }
    }
}
