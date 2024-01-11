using QuanLyNhaKhoa.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

namespace QuanLyNhaKhoa.DataAccess
{
    public class MedicinesAndServices
    {
        string connectionString;
        public MedicinesAndServices(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Medicine> GetMedicines(string byName = null)
        {
            List<Medicine> meds = new();
            // query the database to get all orders
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM THUOC ";
                if (byName is not null)
                {
                    query += $"WHERE TENTHUOC LIKE N'%{byName}%' ";
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
                            string id = reader.GetString(reader.GetOrdinal("MATHUOC"));
                            string name = reader.GetString(reader.GetOrdinal("TENTHUOC"));
                            string unit = reader.GetString(reader.GetOrdinal("DONVITINH"));
                            int quantity = reader.GetInt32(reader.GetOrdinal("SOLUONGTON"));
                            string desc = reader.GetString(reader.GetOrdinal("CHIDINH"));
                            int price = reader.GetInt32(reader.GetOrdinal("DONGIA"));
                            DateTime expiredDate = reader.GetDateTime(reader.GetOrdinal("NGAYHETHAN"));
                            meds.Add(new Medicine()
                            {
                                ID = id,
                                Name = name,
                                Unit = unit,
                                Count = quantity,
                                Description = desc,
                                ExpiredDate = expiredDate,
                                Price = price
                            });
                        }
                    }
                }
            }
            return meds;
        }

        public Medicine GetMedicine(Medicine medicine)
        {
            // query the database to get all orders
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = $"SELECT * FROM THUOC WHERE MATHUOC = '{medicine.ID}'";

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
                            string id = reader.GetString(reader.GetOrdinal("MATHUOC"));
                            string name = reader.GetString(reader.GetOrdinal("TENTHUOC"));
                            string unit = reader.GetString(reader.GetOrdinal("DONVITINH"));
                            int quantity = reader.GetInt32(reader.GetOrdinal("SOLUONGTON"));
                            string desc = reader.GetString(reader.GetOrdinal("CHIDINH"));
                            int price = reader.GetInt32(reader.GetOrdinal("DONGIA"));
                            DateTime expiredDate = reader.GetDateTime(reader.GetOrdinal("NGAYHETHAN"));
                            return new Medicine()
                            {
                                ID = id,
                                Name = name,
                                Unit = unit,
                                Count = quantity,
                                Description = desc,
                                ExpiredDate = expiredDate,
                                Price = price
                            };
                        }
                    }
                }
            }
            return null;
        }

        public bool RemoveMedicine(Models.Medicine medicine)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = $"DELETE FROM THUOC WHERE MATHUOC = '{medicine.ID}'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {

                        Debug.WriteLine("Cannot remove. Medicine is in used.");
                        return false;
                    }
                }
            }
            return true;
        }

        public bool AddMedicine(Models.Medicine medicine)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = $"INSERT INTO THUOC(MATHUOC, TENTHUOC, DONVITINH, SOLUONGTON, CHIDINH, DONGIA, NGAYHETHAN) VALUES ('{medicine.ID}', N'{medicine.Name}', N'{medicine.Unit}', {medicine.Count}, N'{medicine.Description}', {medicine.Price}, '{medicine.ExpiredDate.ToString("yyyy-MM-dd")}')";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {

                        Debug.WriteLine("Cannot add medicine.");
                        return false;
                    }
                }
            }
            return true;
        }

        public bool EditMedicine(Models.Medicine medicine)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = $"UPDATE THUOC SET TENTHUOC = N'{medicine.Name}', DONVITINH = N'{medicine.Unit}', SOLUONGTON = {medicine.Count}, CHIDINH = N'{medicine.Description}', DONGIA = {medicine.Price}, NGAYHETHAN = '{medicine.ExpiredDate.ToString("yyyy-MM-dd")}' WHERE MATHUOC = '{medicine.ID}'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {

                        Debug.WriteLine("Cannot edit medicine.");
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
