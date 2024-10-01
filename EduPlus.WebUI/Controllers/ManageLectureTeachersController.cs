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
    public class ManageLectureTeachersController : CommonBaseClass
    {
        
        public ActionResult Index(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "NotFound", new { entity = "Lecture", backUrl = "/ManageLectures/" });

            Lecture lecture = null;
            List<Teacher> listOfAssociatedTeachers = new List<Teacher>();
            List<Teacher> listOfNonAssociatedTeachers = new List<Teacher>();

            try
            {
                using (var cn = new SqlConnection(ConnectionString))
                {
                    lecture = LectureData.GetLecture((int)id, cn);
                    if (lecture == null)
                        return RedirectToAction("Index", "NotFound", new { entity = "Lecture", backUrl = "/ManageLectures/" });

                    listOfAssociatedTeachers = TeacherLecturesData.GetAssociatedTeacherList((int)id, cn);
                    listOfNonAssociatedTeachers = TeacherLecturesData.GetNonAssociatedTeacherList((int)id, cn);
                }
            }
            catch (Exception ex)
            {
                TempData["DangerMessage"] = ex.Message;
            }

            var myViewModel = new LectureTeachersViewModel
            {
                Lecture = lecture,
                AssociatedTeachers = listOfAssociatedTeachers,
                NonAssociatedTeachers = listOfNonAssociatedTeachers
            };

            return View(myViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrUpdate(int lectureId, int teacherId)
        {
            try
            {
                using (var cn = new SqlConnection(ConnectionString))
                {
                    LectureTeachersData.Insert(teacherId, lectureId, cn);
                }
            }
            catch (Exception ex)
            {
                TempData["DangerMessage"] = ex.Message;
            }
            return RedirectToAction(nameof(Index), new { id = lectureId });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(int lectureId, int teacherId)
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
            return RedirectToAction(nameof(Index), new { id = lectureId });
        }
    }
}
