using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EduPlus.Models;

namespace EduPlus.Data.ADO.Net
{
    public static class TeacherData
    {
        public static void Insert(Teacher teacher, SqlConnection cn)
        {
            if (cn.State == ConnectionState.Closed) cn.Open();

            using (var cmd = new SqlCommand("INSERT INTO Teachers (FullName, Email) VALUES (@FullName, @Email)", cn))
            {
                cmd.Parameters.AddWithValue("FullName", teacher.FullName);
                cmd.Parameters.AddWithValue("Email", teacher.Email);
                cmd.ExecuteNonQuery();
            }
        }

        public static void Update(Teacher teacher, SqlConnection cn)
        {
            if (cn.State == ConnectionState.Closed) cn.Open();

            using (var cmd = new SqlCommand("UPDATE Teachers SET FullName=@FullName, Email=@Email WHERE TeacherId=@TeacherId", cn))
            {
                cmd.Parameters.AddWithValue("TeacherId", teacher.TeacherId);
                cmd.Parameters.AddWithValue("FullName", teacher.FullName);
                cmd.Parameters.AddWithValue("Email", teacher.Email);
                cmd.ExecuteNonQuery();
            }
        }

        public static Teacher GetTeacher(int teacherId, SqlConnection cn)
        {
            Teacher result = null;

            if (cn.State == ConnectionState.Closed)
                cn.Open();

            using (var cmd = new SqlCommand("SELECT * FROM Teachers WHERE TeacherId = @TeacherId", cn))
            {
                cmd.Parameters.AddWithValue("TeacherId", teacherId);

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        result = new Teacher
                        {
                            TeacherId = Convert.ToInt32(dr["TeacherId"]),
                            FullName = Convert.ToString(dr["FullName"]),
                            Email = Convert.ToString(dr["Email"])
                        };
                    }
                }
            }

            return result;
        }


        public static List<Teacher> GetList(SqlConnection cn)
        {
            var result = new List<Teacher>();
            if (cn.State == ConnectionState.Closed) cn.Open();

            using (var cmd = new SqlCommand("SELECT * FROM Teachers", cn))
            {
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var teacher = new Teacher()
                        {
                            TeacherId = Convert.ToInt32(dr["TeacherId"]),
                            FullName = Convert.ToString(dr["FullName"]),
                            Email = Convert.ToString(dr["Email"])
                        };
                        result.Add(teacher);
                    }
                    dr.Close();
                }
            }
            return result;
        }

        public static void Delete(Teacher teacher, SqlConnection cn)
        {
            using (var cmd = new SqlCommand("DELETE FROM Teachers WHERE TeacherId=@TeacherId", cn))
            {
                cmd.Parameters.AddWithValue("TeacherId", teacher.TeacherId);
                if (cn.State == ConnectionState.Closed) cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static bool HasLectures(Teacher teacher, SqlConnection cn)
        {
            if (cn.State == ConnectionState.Closed) cn.Open();

            using (var cmd = new SqlCommand("SELECT COUNT(*) FROM TeacherLectures WHERE TeacherId=@TeacherId", cn))
            {
                cmd.Parameters.AddWithValue("TeacherId", teacher.TeacherId);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }
    }
}
