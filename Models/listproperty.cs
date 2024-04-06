using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace project.Models
{
    public class ListProperty
    {
        private SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Rentify_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;");

        public int Id { get; set; }
        public string OwnerName { get; set; }
        public string Address { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string HouseType { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal Price { get; set; }
        public string Type { get; set; }
        public string Availability { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }

        public List<ListProperty> GetAllProperties()
        {
            List<ListProperty> properties = new List<ListProperty>();
            string query = "SELECT * FROM Properties"; // Assuming the table is named Properties
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                properties.Add(new ListProperty
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    OwnerName = dr["OwnerName"].ToString(),
                    Address = dr["Address"].ToString(),
                    Area = dr["Area"].ToString(),
                    City = dr["City"].ToString(),
                    State = dr["State"].ToString(),
                    HouseType = dr["HouseType"].ToString(),
                    Price = Convert.ToDecimal(dr["Price"]),
                    Type = dr["Type"].ToString(),
                    Availability = dr["Availability"].ToString(),
                    Description = dr["Description"].ToString(),
                    PhotoUrl = dr["PhotoUrl"].ToString()
                });
            }
            return properties;
        }

        public ListProperty GetPropertyById(int id)
        {
            ListProperty property = null;
            string query = "SELECT * FROM Properties WHERE Id = @Id"; // Adjusted to parameterized query for security
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", id);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read()) // Assuming Id is unique, we use if instead of while
            {
                property = new ListProperty
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    OwnerName = dr["OwnerName"].ToString(),
                    Address = dr["Address"].ToString(),
                    Area = dr["Area"].ToString(),
                    City = dr["City"].ToString(),
                    State = dr["State"].ToString(),
                    HouseType = dr["HouseType"].ToString(),
                    Price = Convert.ToDecimal(dr["Price"]),
                    Type = dr["Type"].ToString(),
                    Availability = dr["Availability"].ToString(),
                    Description = dr["Description"].ToString(),
                    PhotoUrl = dr["PhotoUrl"].ToString()
                };
            }
            con.Close();
            return property;
        }

        public bool InsertProperty(ListProperty property)
        {
            string query = @"INSERT INTO Properties (OwnerName, Address, Area, City, State, HouseType, Price, Type, Availability, Description, PhotoUrl) 
                             VALUES (@OwnerName, @Address, @Area, @City, @State, @HouseType, @Price, @Type, @Availability, @Description, @PhotoUrl)";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@OwnerName", property.OwnerName);
                cmd.Parameters.AddWithValue("@Address", property.Address);
                cmd.Parameters.AddWithValue("@Area", property.Area);
                cmd.Parameters.AddWithValue("@City", property.City);
                cmd.Parameters.AddWithValue("@State", property.State);
                cmd.Parameters.AddWithValue("@HouseType", property.HouseType);
                cmd.Parameters.AddWithValue("@Price", property.Price);
                cmd.Parameters.AddWithValue("@Type", property.Type);
                cmd.Parameters.AddWithValue("@Availability", property.Availability);
                cmd.Parameters.AddWithValue("@Description", property.Description);
                cmd.Parameters.AddWithValue("@PhotoUrl", property.PhotoUrl);

                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();

                return i >= 1;
            }
        }
    }
}
