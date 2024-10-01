using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Linq; 
using System.Web.Mvc;
using EduPlus.Models;
using EduPlus.Data.ADO.Net;
using EduPlus.WebUI.Controllers;

namespace EduPlus.WebUI.Controllers
{
    // [Authorize]
    public class ManageTeachersController : CommonBaseClass
    {
        
        public ActionResult Index()
        {
            var listOfTeachers = new List<Teacher>();
            try
            {
                using (var cn = new SqlConnection(ConnectionString))
                {
                    listOfTeachers = TeacherData.GetList(cn);
                }
            }
            catch (Exception ex)
            {
                TempData["DangerMessage"] = ex.Message;
            }
            return View(listOfTeachers);
        }

       
        public ActionResult AddOrUpdate(int? id)
        {
            if (id == null) return View();

            Teacher teacher = null;
            try
            {
                using (var cn = new SqlConnection(ConnectionString))
                {
                    teacher = TeacherData.GetTeacher((int)id, cn);
                    if (teacher == null)
                        return RedirectToAction("Index", "NotFound", new { entity = "Teacher", backUrl = "/ManageTeachers/" });
                }
            }
            catch (Exception ex)
            {
                TempData["DangerMessage"] = ex.Message;
            }
            return View(teacher);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrUpdate(Teacher teacher)
        {
            if (!ModelState.IsValid)
                return (teacher.TeacherId == 0) ? View() : View(teacher);

            try
            {
                using (var cn = new SqlConnection(ConnectionString))
                {
                    if (teacher.TeacherId == 0)
                        TeacherData.Insert(teacher, cn);
                    else
                        TeacherData.Update(teacher, cn);
                }
            }
            catch (Exception ex)
            {
                TempData["DangerMessage"] = ex.Message;
                return (teacher.TeacherId == 0) ? View() : View(teacher);
            }
            return RedirectToAction(nameof(Index));
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(Teacher teacher)
        {
            try
            {
                using (var cn = new SqlConnection(ConnectionString))
                {
                    if (TeacherData.HasLectures(teacher, cn))
                    {
                        var associatedLectures = TeacherLecturesData.GetAssociatedLectureList(teacher.TeacherId, cn);
                        var lectureSubjects = string.Join(", ", associatedLectures.Select(l => l.Subject)); 

                        throw new Exception($"This Teacher cannot be removed because they are associated with these lectures: {lectureSubjects}. Remove all associations first.");
                    }
                    else
                    {
                        TeacherData.Delete(teacher, cn);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["DangerMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
