using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Zajecia2.Models
{
    public class StudentDbService : IStudentDbService
    {
        private string SqlConn ="Data Source=db-mssql;Initial Catalog=s17579;Integrated Security=True";

        public IEnumerable<Student> GetStudents()
        {
            var output = new List<Student>();
            using (var client = new SqlConnection(SqlConn))
            using (var command = new SqlCommand())
                {
                    command.Connection = client;
                    command.CommandText = " Select * From Student";
                
                    client.Open();
                    var dr = command.ExecuteReader();

                    while(dr.Read())
                    {
                        output.Add(new Student
                        {
                            IdEnrollment = int.Parse(dr["IdEnrollment"].ToString()),
                            FirstName = dr["FirstName"].ToString(),
                            LastName = dr["LastName"].ToString(),
                            BirthDate = dr["IndexNumber"].ToString(),
                            IndexNumber = dr["IndexNumber"].ToString()
                        });
                    }
                }
            
            return output;
        }
        public IEnumerable<Enrollment> GetStudentEnrollmentInfo(String IndexNumber)          
        {
            int Index = Convert.ToInt32(IndexNumber);
            var output = new List<Enrollment>();
            using (var client = new SqlConnection(SqlConn))
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = client;
                    command.CommandText = "Select Enrollment.IdEnrollment, Semester, IdStudy, StartDate  from Enrollment , Student where Student.IndexNumber = @Index AND Enrollment.IdEnrollment = Student.IdEnrollment";
                    command.Parameters.AddWithValue("Index", Index);

                    client.Open();
                    var dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        output.Add(new Enrollment
                        {
                            IdEnrollment = int.Parse(dr["IdEnrollment"].ToString()),
                            Semester = int.Parse(dr["Semester"].ToString()),
                            IdStudy = int.Parse(dr["IdStudy"].ToString()),
                            StartDate = dr["StartDate"].ToString()                        
                        });
                    }
                }
            }
            return output;
        }

        public Student GetStudent(string IndexNumber)
        {
            throw new NotImplementedException();
        }
    }
}
