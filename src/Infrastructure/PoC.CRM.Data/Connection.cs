using System;
using System.Data.Common;
using MySqlConnector;

namespace PoC.CRM.Data;

public class Connection
{
    private static DbConnection? _instance;
    private static object _lock = new object();

    public static DbConnection GetInstance() {
        lock (_lock) {
            _instance ??= new MySqlConnection("Server=localhost;Database=poc_crm;User ID=root;Password=senha123;");

            if (_instance.State != System.Data.ConnectionState.Open)
                _instance.Open();
        }
        
        return _instance;
    }
}
