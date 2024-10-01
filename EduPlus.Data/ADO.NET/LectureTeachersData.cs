using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EduPlus.Models;

namespace EduPlus.Data.ADO.Net
{
    public static class LectureTeachersData
    {
        public static List<Teacher> GetNonAssociatedTeacherList(int lectureId, SqlConnection cn)
        {
            var result = new List<Teacher>();
            using (var cmd = new SqlCommand("SELECT * FROM Teachers " +
                                            "WHERE TeacherId NOT IN ( " +
                                            "   SELECT DISTINCT TeacherId " +
                                            "   FROM TeacherLectures " +
                                            "   WHERE LectureId=@LectureId" +
                                            ")", cn))
            {
                if (cn.State == ConnectionState.Closed) cn.Open();
                cmd.Parameters.AddWithValue("LectureId", lectureId);
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
                }
            }
            return result;
        }

        public static List<Teacher> GetAssociatedTeacherList(int lectureId, SqlConnection cn)
        {
            var result = new List<Teacher>();
            using (var cmd = new SqlCommand("SELECT * FROM Teachers " +
                                            "WHERE TeacherId IN ( " +
                                            "   SELECT DISTINCT TeacherId " +
                                            "   FROM TeacherLectures " +
                                            "   WHERE LectureId=@LectureId" +
                                            ")", cn))
            {
                if (cn.State == ConnectionState.Closed) cn.Open();
                cmd.Parameters.AddWithValue("LectureId", lectureId);
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
                }
            }
            return result;
        }

        public static List<Lecture> GetNonAssociatedLectureList(int teacherId, SqlConnection cn)
        {
            var result = new List<Lecture>();
            using (var cmd = new SqlCommand("SELECT * FROM Lectures " +
                                            "WHERE LectureId NOT IN ( " +
                                            "   SELECT DISTINCT LectureId " +
                                            "   FROM TeacherLectures " +
                                            "   WHERE TeacherId=@TeacherId" +
                                            ")", cn))
            {
                if (cn.State == ConnectionState.Closed) cn.Open();
                cmd.Parameters.AddWithValue("TeacherId", teacherId);
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
                }
            }
            return result;
        }

        public static List<Lecture> GetAssociatedLectureList(int teacherId, SqlConnection cn)
        {
            var result = new List<Lecture>();
            using (var cmd = new SqlCommand("SELECT * FROM Lectures " +
                                            "WHERE LectureId IN ( " +
                                            "   SELECT DISTINCT LectureId " +
                                            "   FROM TeacherLectures " +
                                            "   WHERE TeacherId=@TeacherId" +
                                            ")", cn))
            {
                if (cn.State == ConnectionState.Closed) cn.Open();
                cmd.Parameters.AddWithValue("TeacherId", teacherId);
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
                }
            }
            return result;
        }

        public static void Insert(int teacherId, int lectureId, SqlConnection cn)
        {
            using (var cmd = new SqlCommand("INSERT INTO TeacherLectures (TeacherId, LectureId) VALUES (@TeacherId, @LectureId)", cn))
            {
                cmd.Parameters.AddWithValue("TeacherId", teacherId);
                cmd.Parameters.AddWithValue("LectureId", lectureId);
                if (cn.State == ConnectionState.Closed) cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void Delete(int teacherId, int lectureId, SqlConnection cn)
        {
            using (var cmd = new SqlCommand("DELETE TeacherLectures WHERE TeacherId=@TeacherId AND LectureId=@LectureId", cn))
            {
                cmd.Parameters.AddWithValue("TeacherId", teacherId);
                cmd.Parameters.AddWithValue("LectureId", lectureId);
                if (cn.State == ConnectionState.Closed) cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
