using Microsoft.Win32;
using QuanLyNhaKhoa.Models;
using System;
using System.Management;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data;

namespace QuanLyNhaKhoa.DataAccess
{
    public class DatabaseManagement
    {
        private static string serverName = "localhost";
        private static string databaseName = "QLPK";
        private static string connectionString = $"Data Source={serverName};Integrated Security=True";

        public DatabaseManagement()
        {
            GetServerName();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    // Check if the database exists
                    if (!DatabaseExists(connection, databaseName))
                    {
                        // If not, create the database
                        CreateDatabase(connection, databaseName);
                        Debug.WriteLine($"Database '{databaseName}' created successfully.");
                        connection.Close();
                    }
                    else
                    {
                        Debug.WriteLine($"Database '{databaseName}' already exists.");
                        connection.Close();
                    }
                    Reconnect();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
            }
        }
        public static void GetServerName()
        {
            serverName = Environment.MachineName;
            RegistryView registryView = Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32;
            using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView))
            {
                RegistryKey instanceKey = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL", false);
                if (instanceKey != null)
                {
                    foreach (var instanceName in instanceKey.GetValueNames())
                    {
                        Debug.WriteLine("HELLO: " + serverName + "\\" + instanceName);
                        serverName = serverName + "\\" + instanceName;
                        connectionString = $"Data Source={serverName};Integrated Security=True";
                        break;
                    }
                }
            }
        }

        private void Reconnect()
        {
            // Attempt to reconnect using the new database
            string newConnectionString = $"Data Source={serverName};Initial Catalog={databaseName};Integrated Security=True";

            try
            {
                using (SqlConnection newConnection = new SqlConnection(newConnectionString))
                {
                    newConnection.Open();
                    Debug.WriteLine($"Successfully reconnected to '{databaseName}'.");
                    // Continue with additional operations if needed
                    newConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Reconnection error: {ex.Message}");
            }
        }

        static bool DatabaseExists(SqlConnection connection, string databaseName)
        {
            string query = $"SELECT COUNT(*) FROM sys.databases WHERE name = '{databaseName}'";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        static void CreateDatabase(SqlConnection connection, string databaseName)
        {
            string createDatabaseQuery = $"CREATE DATABASE {databaseName}";

            using (SqlCommand command = new SqlCommand(createDatabaseQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        static bool Login(Interfaces.Account account)
        {
            string accountType = "KHACH_HANG";
            if (account is AdministratorAccount)
            {
                accountType = "QUAN_TRI_VIEN";
            }
            string query = $"SELECT COUNT(*) FROM {accountType} WHERE Username = '{account.PhoneNumber}' AND Password = '{account.Password}'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }

        }
    }
}
