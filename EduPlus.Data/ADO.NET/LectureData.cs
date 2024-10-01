using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EduPlus.Models;

namespace EduPlus.Data.ADO.Net
{
    public static class LectureData
    {
        public static void Insert(Lecture lecture, SqlConnection cn)
        {
            if (cn.State == ConnectionState.Closed) cn.Open();

            using (var cmd = new SqlCommand("INSERT INTO Lectures (Subject, MinMinutes, MaxMinutes) VALUES (@Subject, @MinMinutes, @MaxMinutes)", cn))
            {
                cmd.Parameters.AddWithValue("Subject", lecture.Subject);
                cmd.Parameters.AddWithValue("MinMinutes", lecture.MinMinutes);
                cmd.Parameters.AddWithValue("MaxMinutes", lecture.MaxMinutes);
                cmd.ExecuteNonQuery();
            }
        }

        public static void Update(Lecture lecture, SqlConnection cn)
        {
            if (cn.State == ConnectionState.Closed) cn.Open();

            using (var cmd = new SqlCommand("UPDATE Lectures SET Subject=@Subject, MinMinutes=@MinMinutes, MaxMinutes=@MaxMinutes WHERE LectureId=@LectureId", cn))
            {
                cmd.Parameters.AddWithValue("LectureId", lecture.LectureId);
                cmd.Parameters.AddWithValue("Subject", lecture.Subject);
                cmd.Parameters.AddWithValue("MinMinutes", lecture.MinMinutes);
                cmd.Parameters.AddWithValue("MaxMinutes", lecture.MaxMinutes);
                cmd.ExecuteNonQuery();
            }
        }

        public static Lecture GetLecture(int lectureId, SqlConnection cn)
        {
            Lecture result = null;
            if (cn.State == ConnectionState.Closed) cn.Open();

            using (var cmd = new SqlCommand("SELECT * FROM Lectures WHERE LectureId=@LectureId", cn))
            {
                cmd.Parameters.AddWithValue("LectureId", lectureId);
                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        result = new Lecture()
                        {
                            LectureId = Convert.ToInt32(dr["LectureId"]),
                            Subject = Convert.ToString(dr["Subject"]),
                            MinMinutes = Convert.ToInt32(dr["MinMinutes"]),
                            MaxMinutes = Convert.ToInt32(dr["MaxMinutes"])
                        };
                    }
                    dr.Close();
                }
            }
            return result;
        }

        public static List<Lecture> GetList(SqlConnection cn)
        {
            var result = new List<Lecture>();
            if (cn.State == ConnectionState.Closed) cn.Open();

            using (var cmd = new SqlCommand("SELECT * FROM Lectures", cn))
            {
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var lecture = new Lecture()
                        {
                            LectureId = Convert.ToInt32(dr["LectureId"]),
                            Subject = Convert.ToString(dr["Subject"]),
                            MinMinutes = Convert.ToInt32(dr["MinMinutes"]),
                            MaxMinutes = Convert.ToInt32(dr["MaxMinutes"])
                        };
                        result.Add(lecture);
                    }
                    dr.Close();
                }
            }
            return result;
        }

        public static void Delete(int lectureId, SqlConnection cn)
        {
            using (var cmd = new SqlCommand("DELETE FROM Lectures WHERE LectureId = @LectureId", cn))
            {
                cmd.Parameters.AddWithValue("LectureId", lectureId);
                if (cn.State == ConnectionState.Closed) cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static bool HasTeachers(int lectureId, SqlConnection cn)
        {
            if (cn.State == ConnectionState.Closed) cn.Open();

            using (var cmd = new SqlCommand("SELECT COUNT(*) FROM TeacherLectures WHERE LectureId=@LectureId", cn))
            {
                cmd.Parameters.AddWithValue("LectureId", lectureId);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

    }
}
