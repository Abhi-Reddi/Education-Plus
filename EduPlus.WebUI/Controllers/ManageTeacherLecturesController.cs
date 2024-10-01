using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
using EduPlus.Data.ADO.Net;
using EduPlus.Models;
using EduPlus.WebUI.Models;


namespace EduPlus.WebUI.Controllers
{
    // [Authorize]
    public class ManageTeacherLecturesController : CommonBaseClass
    {
        
        public ActionResult Index(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "NotFound", new { entity = "Teacher", backUrl = "/ManageTeachers/" });

            Teacher teacher = null;
            List<Lecture> listOfAssociatedLectures = new List<Lecture>();
            List<Lecture> listOfNonAssociatedLectures = new List<Lecture>();

            try
            {
                using (var cn = new SqlConnection(ConnectionString))
                {
                    teacher = TeacherData.GetTeacher((int)id, cn);
                    if (teacher == null)
                        return RedirectToAction("Index", "NotFound", new { entity = "Teacher", backUrl = "/ManageTeachers/" });

                    listOfAssociatedLectures = TeacherLecturesData.GetAssociatedLectureList((int)id, cn);
                    listOfNonAssociatedLectures = TeacherLecturesData.GetNonAssociatedLectureList((int)id, cn);
                }
            }
            catch (Exception ex)
            {
                TempData["DangerMessage"] = ex.Message;
            }

            var myViewModel = new TeacherLecturesViewModel
            {
                Teacher = teacher,
                AssociatedLectures = listOfAssociatedLectures,
                NonAssociatedLectures = listOfNonAssociatedLectures
            };

            return View(myViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(int teacherId, int lectureId)
        {
            try
            {
                using (var cn = new SqlConnection(ConnectionString))
                {
                    TeacherLecturesData.Insert(teacherId, lectureId, cn);
                }
            }
            catch (Exception ex)
            {
                TempData["DangerMessage"] = ex.Message;
            }
            return RedirectToAction(nameof(Index), new { id = teacherId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(int teacherId, int lectureId)
        {
            try
            {
                using (var cn = new SqlConnection(ConnectionString))
                {
                    TeacherLecturesData.Delete(teacherId, lectureId, cn);
                }
            }
            catch (Exception ex)
            {
                TempData["DangerMessage"] = ex.Message;
            }
            return RedirectToAction(nameof(Index), new { id = teacherId });
        }
    }
}
