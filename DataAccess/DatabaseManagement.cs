using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

namespace QuanLyNhaKhoa.DataAccess
{
    public class DatabaseManagement
    {
        private static string serverName = Environment.MachineName;
        private static string connectionName = serverName;
        private static string databaseName = "QLPK";
        private static string connectionString = $"Data Source={connectionName};Integrated Security=True;TrustServerCertificate=True;Connect Timeout=2;";
        public string ConnectionString { get => connectionString; private set { connectionString = value; } }

        public DatabaseManagement()
        {
            foreach (var connectionName in GetServerNames())
            {
                try
                {
                    TryConnection();
                    break;
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"No database found in {connectionName}");
                }
            }
        }

        public void TryConnection()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Debug.WriteLine($"Attempting to connect to '{connectionName}'...");
                // set the timer for the command below before throwing some exception to terminate it
                connection.Open();
                if (!DatabaseExists(connection, databaseName))
                {
                    Debug.WriteLine($"Not found '{databaseName}' on {connectionName}.");
                    throw new Exception();
                }
                else
                {
                    Debug.WriteLine($"Database '{databaseName}' already exists.");
                }
                Reconnect();
                connection.Close();
                //Reconnect();
            }
        }
        public static IEnumerable<string> GetServerNames()
        {
            yield return connectionName;
            RegistryView registryView = Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32;

            using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView))
            {
                RegistryKey instanceKey = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL", false);

                if (instanceKey != null)
                {
                    string[] valueNames = instanceKey.GetValueNames();
                    foreach (var value in (instanceKey.GetValueNames()))
                    {
                        string instanceName = value;
                        connectionName = $"{serverName}\\{instanceName}";
                        connectionString = $"Data Source={connectionName};Integrated Security=True;TrustServerCertificate=True;Connect Timeout=2;";
                        yield return connectionName;

                    }
                    if (valueNames.Length == 0)
                    {
                        throw new Exception("No more database servers found.");
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
            throw new NotImplementedException();
        }

        void DropDatabase(SqlConnection connection, string databaseName)
        {
            string dropDatabaseQuery = $"DROP DATABASE {databaseName}";

            using (SqlCommand command = new SqlCommand(dropDatabaseQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        //public ObservableCollection<CustomerAppointmentViewModel> GetAppointments(string connectionString, string cusID)
        //{
        //    //string CusID = "KH0002";
        //    string GetAppointmentQuery = "select LH.MALICHHEN, NS.HOTEN, NS.MANS, LH.GIOKHAM, LH.NGAYKHAM from LICH_HEN LH JOIN NHA_SI NS ON LH.NHASI = NS.MANS " +
        //                                        "where LH.MAKH = " + "'" + cusID + "' order by LH.NGAYKHAM asc, LH.GIOKHAM asc";

        //    var appointments = new ObservableCollection<CustomerAppointmentViewModel>();
        //    try
        //    {
        //        using (var conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();
        //            if (conn.State == System.Data.ConnectionState.Open)
        //            {
        //                using (SqlCommand cmd = conn.CreateCommand())
        //                {
        //                    cmd.CommandText = GetAppointmentQuery;
        //                    using (SqlDataReader reader = cmd.ExecuteReader())
        //                    {
        //                        while (reader.Read())
        //                        {
        //                            var CustomerAppointment = new CustomerAppointmentViewModel();
        //                            CustomerAppointment.AppoID = reader.GetString(0);
        //                            CustomerAppointment.DenName = reader.GetString(1);
        //                            CustomerAppointment.DenID = reader.GetString(2);
        //                            TimeSpan time = reader.GetTimeSpan(3);
        //                            CustomerAppointment.AppoTime = TimeOnly.FromTimeSpan(time);
        //                            DateTime date = reader.GetDateTime(4);
        //                            CustomerAppointment.AppoDate = DateOnly.FromDateTime(date);


        //                            appointments.Add(CustomerAppointment);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        return appointments;
        //    }
        //    catch (Exception eSql)
        //    {
        //        Debug.WriteLine($"Exception: {eSql.Message}");
        //    }
        //    return null;
        //}
    }
}
