using QuanLyNhaKhoa.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QuanLyNhaKhoa.DataAccess
{
    public class AccountData
    {
        string connectionString;
        public Interfaces.Account StoredAccount { get; set; } = null;
        public AccountData(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public bool Login(Interfaces.Account account)
        {
            string accountType = "UNKNOWN";
            if (account is AdministratorAccount)
            {
                accountType = "QTV";
            }
            string query = $"SELECT COUNT(*) FROM {accountType} WHERE SDT = '{account.PhoneNumber}' AND MATKHAU = '{account.Password}'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    int count = (int)command.ExecuteScalar();

                    if (StoredAccount is null)
                    {
                        StoredAccount = GetAccountInfo(account);
                    }
                    return count > 0;
                }
            }

        }

        public Interfaces.Account GetAccountInfo(Interfaces.Account account)
        {
            if (account is AdministratorAccount)
            {
                return GetAdministratorAccount(account.PhoneNumber);
            }

            return null;
        }

        private AdministratorAccount GetAdministratorAccount(string PhoneNumber)
        {
            AdministratorAccount account = new AdministratorAccount();
            string query = $"SELECT * FROM QTV WHERE SDT={PhoneNumber}";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                account.Id = reader.GetString(reader.GetOrdinal("MAQTV"));
                                account.PhoneNumber = reader.GetString(reader.GetOrdinal("SDT"));
                                account.Name = reader.GetString(reader.GetOrdinal("HOTEN"));
                                account.Birthday = reader.GetDateTime(reader.GetOrdinal("NGAYSINH"));
                                account.Address = reader.GetString(reader.GetOrdinal("DIACHI"));
                                account.Password = reader.GetString(reader.GetOrdinal("MATKHAU"));
                                account.Status = reader.GetInt32(reader.GetOrdinal("TRANGTHAI"));
                                return account;
                            }
                        }
                    }
                    catch (Exception)
                    {

                        return null;
                    }
                }
            }
            return null;
        }

        bool SignUpCustomer(Interfaces.Account account)
        {
            string query = "INSERT INTO KHACH_HANG (MAKH, SDT, HOTEN, NGAYSINH, DIACHI, MATKHAU, TRANGTHAI)"
                + "VALUES (@MAKH, @SDT, @HOTEN, @NGAYSINH, @DIACHI, @MATKHAU, @TRANGTHAI)";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MAKH", account.Id);
                        command.Parameters.AddWithValue("@SDT", account.PhoneNumber);
                        command.Parameters.AddWithValue("@HOTEN", account.Name);
                        command.Parameters.AddWithValue("@NGAYSINH", account.Birthday);
                        command.Parameters.AddWithValue("@DIACHI", account.Address);
                        command.Parameters.AddWithValue("@MATKHAU", account.Password);
                        command.Parameters.AddWithValue("@TRANGTHAI", account.Status);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        bool SignUpDentist(Interfaces.Account account)
        {
            throw new NotImplementedException();
        }

        bool SignUpReceptionist(Interfaces.Account account)
        {
            throw new NotImplementedException();
        }

        bool SignUpAdministrator(Interfaces.Account account)
        {
            throw new NotImplementedException();
        }

        public List<ReceptionistView> GetReceptionists(string byName = null)
        {
            List<ReceptionistView> receps = new();
            // query the database to get all orders
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM NHAN_VIEN ";
                if (byName is not null)
                {
                    query += $"WHERE HOTEN LIKE '%{byName}%'";
                }


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            // check if there are result
                            if (!reader.HasRows)
                            {
                                return null;
                            }
                            // Access columns from the result set
                            string id = reader.GetString(reader.GetOrdinal("MANV"));
                            string name = reader.GetString(reader.GetOrdinal("HOTEN"));
                            receps.Add(new ReceptionistView()
                            {
                                Id = id,
                                Name = name,
                            });
                        }
                    }
                }
            }
            return receps;
        }
    }
}
