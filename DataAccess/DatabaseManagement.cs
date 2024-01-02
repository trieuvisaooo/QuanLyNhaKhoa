using Microsoft.Win32;
using QuanLyNhaKhoa.Models;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace QuanLyNhaKhoa.DataAccess
{
    public class DatabaseManagement
    {
        private static string serverName = "localhost";
        private static string connectionName = "localhost";
        private static string databaseName = "BOOKSTORE";
        private static string connectionString = $"Data Source={connectionName};Integrated Security=True;TrustServerCertificate=True;Connect Timeout=2;";

        public DatabaseManagement()
        {
            try
            {
                TryConnection();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
            }
        }

        public void TryConnection()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    Debug.WriteLine($"Attempting to connect to '{connectionName}'...");
                    // set the timer for the command below before throwing some exception to terminate it
                    connection.Open();
                    if (!DatabaseExists(connection, databaseName))
                    {
                        // If not, create the database
                        CreateDatabase(connection, databaseName);
                        Debug.WriteLine($"Database '{databaseName}' created successfully.");
                    }
                    else
                    {
                        DropDatabase(connection, databaseName);
                        CreateDatabase(connection, databaseName);
                        Debug.WriteLine($"Database '{databaseName}' already exists.");
                    }
                    Reconnect();
                    connection.Close();
                    //Reconnect();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\nConnection error: {ex.Message} with serverName {connectionName}.\n");
                if (serverName == "localhost")
                {
                    GetServerName();
                    TryConnection();
                }
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
                        connectionName = serverName + "\\" + instanceName;
                        connectionString = $"Data Source={connectionName};Integrated Security=True;TrustServerCertificate=True;Connect Timeout=2;";
                    }
                }
            }
        }

        private void Reconnect()
        {
            // Attempt to reconnect using the new database
            connectionString = $"Data Source={connectionName};Initial Catalog={databaseName};Integrated Security=True;TrustServerCertificate=True;Connect Timeout=2;";

            try
            {
                using (SqlConnection newConnection = new SqlConnection(connectionString))
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

        bool DatabaseExists(SqlConnection connection, string databaseName)
        {
            string query = $"SELECT COUNT(*) FROM sys.databases WHERE name = '{databaseName}'";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        void CreateDatabase(SqlConnection connection, string databaseName)
        {
            try
            {
                // create database
                string createDatabaseQuery = $"CREATE DATABASE {databaseName}";

                using (connection)
                {
                    using (SqlCommand command = new SqlCommand(createDatabaseQuery, connection))
                    {
                        command.ExecuteNonQuery();
                        Debug.WriteLine("Create Database executed successfully. Now creating tables.");
                        CreateTable(connection, databaseName);
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error executing script: {ex.Message}");
            }

        }

        void CreateTable(SqlConnection connection, string databaseName)
        {
            try
            {
                Assembly ass = Assembly.GetEntryAssembly();
                string dir = Path.GetDirectoryName(ass.Location);
                Debug.WriteLine("dis is: " + dir);
                string script = File.ReadAllText(Path.Combine(dir, "DataAccess/database.sql"));
                Debug.WriteLine(script.Substring(0, 100));

                using (SqlConnection tableConnection = new SqlConnection(connectionString + $"Initial Catalog={databaseName}"))
                {
                    tableConnection.Open(); // Open the connection explicitly for the table creation

                    using (SqlCommand command = new SqlCommand(script, tableConnection))
                    {
                        command.ExecuteNonQuery();
                    }
                    tableConnection.Close();
                }

                Debug.WriteLine("Script for tables executed successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error executing script: {ex.Message}");
            }
        }

        void DropDatabase(SqlConnection connection, string databaseName)
        {
            string dropDatabaseQuery = $"DROP DATABASE {databaseName}";

            using (SqlCommand command = new SqlCommand(dropDatabaseQuery, connection))
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
