using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Zajecia2.DTO;
using Zajecia2.DTO.Request;
using Zajecia2.DTO.Response;
using Zajecia2.Models;

namespace Zajecia2.Services
{
    public class EnrollmentDbServices : IEnrollmentDbServices
    {
        private string SqlConn = " Data Source=db-mssql;Initial Catalog=s17579;Integrated Security=True";

        public string ConnString { get; private set; }

        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest request)
        {
            EnrollStudentResponse response = null;
            Enrollment respEnrollment = new Enrollment();
            SqlTransaction tran = null;

            using (var con = new SqlConnection(SqlConn))
            using (var com = new SqlCommand())
            {
                com.CommandText = "SELECT * FROM Studies WHERE Name = @StudyName";
                com.Parameters.AddWithValue("StudyName", request.Studies);

                com.Connection = con;
                con.Open();
                tran = con.BeginTransaction();

                com.Transaction = tran;
                SqlDataReader dr = com.ExecuteReader();
                if (!dr.Read())
                {

                    dr.Close();
                    tran.Rollback();
                    dr.Dispose();
                    throw new ArgumentException("Studies not found");
                }
                int idStudy = (int)dr["IdStudy"]; // needed for 3.
                dr.Close();


         
                int idEnrollment = 0;

                com.CommandText = "SELECT * FROM Enrollment WHERE Semester = 1 AND IdStudy = @IdStudy";
                com.Parameters.AddWithValue("IdStudy", idStudy);
                com.Transaction = tran;
                dr = com.ExecuteReader();
                if (dr.Read())
                {
                    idEnrollment = (int)dr["IdEnrollment"];
                }
                else
                {
                    dr.Close();

                    com.CommandText = "SELECT IdEnrollment FROM Enrollment";
                    com.Transaction = tran;
                    dr = com.ExecuteReader();
                    int newIdEnrollment = 0;
                    while (dr.Read())
                    {
                        int maxIdEnrollment = (int)dr["IdEnrollment"];
                        if (maxIdEnrollment > newIdEnrollment) newIdEnrollment = maxIdEnrollment;
                    }
                    newIdEnrollment++;
                    dr.Close();


                    com.CommandText = "INSERT INTO Enrollment (IdEnrollment, Semester, IdStudy, StartDate) VALUES (@NewIdEnrollment, 1, @IdStudy, convert(varchar, getdate(), 110))";

                    com.Parameters.AddWithValue("NewIdEnrollment", newIdEnrollment);
                    com.Transaction = tran;
                    com.ExecuteNonQuery();
                    idEnrollment = newIdEnrollment;
                }
                dr.Close();

                com.CommandText = "SELECT * FROM Student WHERE IndexNumber = @IndexNumber";
                com.Parameters.AddWithValue("IndexNumber", request.IndexNumber);
                com.Transaction = tran;
                dr = com.ExecuteReader();
                if (dr.Read())
                {
                    dr.Close();
                    tran.Rollback();
                    dr.Dispose();
                    throw new InvalidOperationException("Student with given index number already exists");
                }
                dr.Close();

                com.CommandText = "INSERT INTO Student (IndexNumber, FirstName, LastName, BirthDate, IdEnrollment) VALUES (@IndexNumber, @Firstname, @LastName, convert(datetime, @BirthDate, 104), @IdEnrollment)";
                com.Parameters.AddWithValue("FirstName", request.FirstName);
                com.Parameters.AddWithValue("LastName", request.LastName);
                com.Parameters.AddWithValue("BirthDate", request.BirthDate);
                com.Parameters.AddWithValue("IdEnrollment", idEnrollment);
                com.Transaction = tran;
                com.ExecuteNonQuery();
                dr.Close();

                com.CommandText = "SELECT * FROM Enrollment WHERE IdEnrollment = @IdEnrollment";
                com.Transaction = tran;
                dr = com.ExecuteReader();
                dr.Read();

                response = new EnrollStudentResponse();
                response.Semester = 1;
                response.Enrollment = respEnrollment;
                respEnrollment.IdEnrollment = (int)dr["IdEnrollment"];
                respEnrollment.Semester = (int)dr["Semester"];
                respEnrollment.IdStudy = (int)dr["IdStudy"];
                respEnrollment.StartDate = dr["StartDate"].ToString();

                dr.Dispose();
                tran.Commit();
            }
            return response;
        }


        public PromoteStudentsResponse PromoteStudents(PromoteStudentsRequest request)
        {
            if (request.Studies == null || request.Semester == 0)
            {
                throw new ArgumentNullException("Incorrect request");
            }
            PromoteStudentsResponse response = null;
            Enrollment respEnrollment = new Enrollment();



            using (SqlConnection conn = new SqlConnection(SqlConn))
            {
                conn.Open();


                SqlCommand cmd = new SqlCommand("PromoteStudents", conn);


                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@studies", request.Studies));
                cmd.Parameters.Add(new SqlParameter("@semester", request.Semester));

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (!dr.Read())
                    {
                        cmd.Dispose();
                        throw new ArgumentException("Nothing to be read by SqlDataReader");
                    }
                    response = new PromoteStudentsResponse();
                    response.Enrollment = respEnrollment;
                    respEnrollment.IdEnrollment = (int)dr["IdEnrollment"];
                    respEnrollment.Semester = (int)dr["Semester"];
                    respEnrollment.IdStudy = (int)dr["IdStudy"];
                    respEnrollment.StartDate = dr["StartDate"].ToString();
                }
                cmd.Dispose();
            }
            return response;
        }
    }
}