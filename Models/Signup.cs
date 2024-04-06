﻿using Humanizer;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace project.Models
{
    public class Signup
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Rentify_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;");

        public int Id { get; set; }

        [Required]
        public string Uname { get; set; }

        [Required]

        public string email { get; set; }

        [Required]
        public int phone { get; set; }
        [Required]
        public string Passwrd { get; set; }

        
            public List<Signup> getData()
            {
                List<Signup> lstEmp = new List<Signup>();
                SqlDataAdapter apt = new SqlDataAdapter("select * from signup", con);
                DataSet ds = new DataSet();
                apt.Fill(ds);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lstEmp.Add(new Signup
                    {
                        Id = Convert.ToInt32(dr["Id"].ToString()),
                        Uname = dr["Uname"].ToString(),
                        email = dr["email"].ToString(),
                        phone = Convert.ToInt32(dr["phone"].ToString()),
                        Passwrd = dr["Passwrd"].ToString(),

                    });
                }
                return lstEmp;
            }

            public Signup getData(string Id)
            {
                Signup emp1 = new Signup();
                SqlCommand cmd = new SqlCommand("select * from signup where id='" + Id +
               "'", con);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        emp1.Id = Convert.ToInt32(dr["Id"].ToString());
                        emp1.Uname = dr["Uname"].ToString();
                        emp1.email = dr["email"].ToString();
                        emp1.phone = Convert.ToInt32(dr["phone"].ToString());
                        emp1.Passwrd = dr["Passwrd"].ToString();

                    }
                }
                con.Close();
                return emp1;
            }

            public bool insert(Signup Emp)
            {
                // Assuming "Id" is auto-generated by the database, if not, you need to add it as a parameter.
                string query = "INSERT INTO signup (Uname,email,phone, Passwrd) VALUES (@Uname,@email,@phone, @Passwrd)";

                // Use "using" statement for automatic disposal of SqlConnection
                using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Rentify_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
                {
                    // Use "using" statement for SqlCommand to ensure it's disposed properly
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Uname", Emp.Uname);
                    cmd.Parameters.AddWithValue("@email", Emp.email);
                    cmd.Parameters.AddWithValue("@phone", Emp.phone);
                    cmd.Parameters.AddWithValue("@Passwrd", Emp.Passwrd);

                        con.Open(); // Open connection
                        int i = cmd.ExecuteNonQuery(); // Execute the non-query command
                        con.Close(); // Close connection explicitly here because of the return statement below (though using will ensure closure)

                        return i >= 1;
                    }
                }

                // Assuming "Id" is auto-generated by the database, if not, you need to add it as a parameter.


            }
        }
    }

