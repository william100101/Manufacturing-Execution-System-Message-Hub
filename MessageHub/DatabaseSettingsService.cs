using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageHub
{
    public class DatabaseSettingsService
    {
        public event Action OnChange;
        private string _ConnectionString;

        public string ConnectionString
        {
            get => _ConnectionString;
            set
            {
                if (_ConnectionString != value)
                {
                    _ConnectionString = value;
                    NotifyStateChanged();
                }
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

        public List<string> ParseConnectionString(string connectionString)
        {
            List<string> DatabaseSettingsInfo = new List<string>();
            if (connectionString != null)
            {
                string server;
                string database;

                int firstEqualSign = connectionString.IndexOf("=");
                int firstSemiColon = connectionString.IndexOf(";");

                server = connectionString.Substring(firstEqualSign + 1, firstSemiColon - (firstEqualSign + 1));
                int databaseEqualSign = connectionString.IndexOf("Database=", StringComparison.CurrentCultureIgnoreCase);
                databaseEqualSign = databaseEqualSign + 8;
                int secondSemiColon = connectionString.IndexOf(";", databaseEqualSign);

                database = connectionString.Substring(databaseEqualSign + 1, secondSemiColon - (databaseEqualSign + 1));
                
                DatabaseSettingsInfo.Add(server);
                DatabaseSettingsInfo.Add(database);
            }
            else
            {
                string dummy1 = "";
                string dummy2 = "";

                DatabaseSettingsInfo.Add(dummy1);
                DatabaseSettingsInfo.Add(dummy2);
            }
            return DatabaseSettingsInfo;
        }


    }
}
