using System;
using System.Data.SqlClient;

namespace Zajecia2.Services
{
    internal class CheckIndexDbService
    {
            public bool CheckIndex(string IndexNumber)
            {
            int index = Convert.ToInt32(IndexNumber);      
                using (SqlConnection con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s17579;Integrated Security=True"))
                using (SqlCommand com = new SqlCommand())
                {
                    com.Connection = con;
                    com.CommandText = "select FirstName from Student where IndexNumber=@index";
                    com.Parameters.AddWithValue("index", index);
                    con.Open();
                    SqlDataReader sqlDataReader = com.ExecuteReader();
                    if (sqlDataReader.Read())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                con.Close();

                }
            }
        
    }
}
