
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DAL
{
    public class StudenHelper
    {
        //[JsonIgnore]
        public AppDb Db { get; set; }

        public StudenHelper(AppDb db = null)
        {
            Db = db;
        }

        public async Task<List<ClassTeacherDto>> GetClassDataAsync()
        {
            List<ClassTeacherDto> dt = new List<ClassTeacherDto>();
            SqlConnection nwindConn = new SqlConnection("Data Source=DESKTOP-JV14P7J\\SQLEXPRESS;Initial Catalog=Portal;Integrated Security=true");
            SqlCommand selectCMD = new SqlCommand("SELECT Id, Name, Location, Teacher FROM ClassTeacher", nwindConn);
            selectCMD.CommandTimeout = 30;
            SqlDataAdapter customerDA = new SqlDataAdapter();
            customerDA.SelectCommand = selectCMD;
            nwindConn.Open();
            DataSet customerDS = new DataSet();
            customerDA.Fill(customerDS, "ClassTeacher");
            foreach (DataRow tt in customerDS.Tables[0].Rows)
            {
                dt.Add(new ClassTeacherDto()
                {
                    Id = Convert.ToInt32(tt["Id"]),
                    Name = Convert.ToString(tt["Name"]),
                    Location = Convert.ToString(tt["Location"]),
                    Teacher = Convert.ToString(tt["Teacher"])
                });
            }
            return dt;
        }

        public async Task<bool> UpdateClassRecord(ClassTeacherDto dto)
        {
            try
            {
                SqlConnection nwindConn = new SqlConnection("Data Source=DESKTOP-JV14P7J\\SQLEXPRESS;Initial Catalog=Portal;Integrated Security=true");
                SqlCommand cmd = new SqlCommand();
                if (dto.Id != 0)
                    cmd = new SqlCommand(string.Format("UPDATE ClassTeacher SET Name ='{0}', Location = '{1}', Teacher = '{2}' Where Id = {3}", dto.Name, dto.Location, dto.Teacher, dto.Id), nwindConn);
                else
                    cmd = new SqlCommand(string.Format("insert into ClassTeacher (Name, Location, Teacher) values('{0}', '{1}', '{2}')", dto.Name, dto.Location, dto.Teacher), nwindConn);
                nwindConn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            return true;
        }

        public async Task<bool> DeleteClassRecord(int id)
        {
            SqlConnection nwindConn = new SqlConnection("Data Source=DESKTOP-JV14P7J\\SQLEXPRESS;Initial Catalog=Portal;Integrated Security=true");
            SqlCommand cmd = new SqlCommand("sp_deleteClass", nwindConn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

            nwindConn.Open();
            cmd.ExecuteNonQuery();
            return true;
        }


        public async Task<List<StudentRowDto>> GetStudentDataAsync(int classId)
        {
            List<StudentRowDto> dt = new List<StudentRowDto>();
            SqlConnection nwindConn = new SqlConnection("Data Source=DESKTOP-JV14P7J\\SQLEXPRESS;Initial Catalog=Portal;Integrated Security=true");
            SqlCommand selectCMD = new SqlCommand(string.Format("SELECT Id, FirstName, LastName, Age, Gpa FROM StudentRow where ClassId={0}", classId), nwindConn);
            selectCMD.CommandTimeout = 30;
            SqlDataAdapter customerDA = new SqlDataAdapter();
            customerDA.SelectCommand = selectCMD;
            nwindConn.Open();
            DataSet customerDS = new DataSet();
            customerDA.Fill(customerDS, "StudentRow");
            foreach (DataRow tt in customerDS.Tables[0].Rows)
            {
                dt.Add(new StudentRowDto()
                {
                    Id = Convert.ToInt32(tt["Id"]),
                    FirstName = Convert.ToString(tt["FirstName"]),
                    LastName = Convert.ToString(tt["LastName"]),
                    Age = Convert.ToInt32(tt["Age"]),
                    Gpa = Convert.ToDouble(tt["Gpa"])
                });
            }
            return dt;
            //nwindConn.Close();
            //using (SqlConnection connection = new SqlConnection("Data Source =DESKTOP-JV14P7J\\SQLEXPRESS;Initial Catalog = Portal;Integrated Security=true;"))
            //{
            //    try
            //    {
            //        //connection.Open();
            //        //var cmd = connection.CreateCommand() as SqlCommand;
            //        //cmd.CommandText = @"SELECT * FROM Student";

            //        //var data = await cmd.ExecuteScalarAsync();
            //        List<StudentDto> dt = new List<StudentDto>();
            //        connection.Open();
            //        //SqlDataAdapter da = new SqlDataAdapter();
            //        SqlCommand cmd = connection.CreateCommand();
            //        cmd.CommandText = @"SELECT Id, Name FROM dbo.Student";
            //        //da.SelectCommand = cmd;
            //        //DataSet ds = new DataSet();

            //        // Get the data reader
            //        //SqlDataReader reader = cmd.ExecuteReader();

            //        // Process each result
            //        SqlDataReader reader = cmd.ExecuteReader();
            //        DataTable schemaTable = reader.getta();

            //        //DataTable table = new DataTable();
            //        foreach (DataRow row in schemaTable.Rows)
            //        {
            //            foreach (DataColumn col in schemaTable.Columns)
            //            {
            //                object value = row[col.ColumnName];
            //            }
            //        }

            //        //foreach (DataRow row in schemaTable.Rows)
            //        //{
            //        //    foreach (DataColumn column in schemaTable.Columns)
            //        //    {
            //        //        Console.WriteLine(String.Format("{0} = {1}",
            //        //           column.ColumnName, row[column]));
            //        //    }
            //        //}
            //    }
            //    catch (Exception ex)
            //    {

            //    }
            //}

        }
    }
}
