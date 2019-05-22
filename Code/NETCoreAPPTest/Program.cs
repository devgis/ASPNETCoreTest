using System;
using System.Data.SqlClient;

namespace NETCoreAPPTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string constr = "Data Source = 192.168.0.200;Initial Catalog = test;User Id = sa;Password = 123456;";
            SqlConnection con = new SqlConnection(constr);
            string sql = "SELECT * FROM t_person";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            //var fac = new System.Data.SqlClient.SqlClientFactory();
            

            while (reader.Read())
            {
                Console.WriteLine(reader["name"].ToString());
            }
            con.Close();


            Console.ReadLine();
            Console.WriteLine("Hello World!");
        }
    }
}