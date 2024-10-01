using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
using EduPlus.Models;
using EduPlus.Data.ADO.Net;
using EduPlus.WebUI.Controllers;

namespace EduPlus.WebUI.Controllers
{
    // [Authorize]
    public class ManageLectureController : CommonBaseClass
    {
       
        public ActionResult Index()
        {
            var listOfLectures = new List<Lecture>();
            try
            {
                using (var cn = new SqlConnection(ConnectionString))
                {
                    listOfLectures = LectureData.GetList(cn);
                }
            }
            catch (Exception ex)
            {
                TempData["DangerMessage"] = ex.Message;
            }
            return View(listOfLectures);
        }

        
        public ActionResult AddOrUpdate(int? id)
        {
            if (id == null) return View();

            Lecture lecture = null;
            try
            {
                using (var cn = new SqlConnection(ConnectionString))
                {
                    lecture = LectureData.GetLecture((int)id, cn);
                    if (lecture == null)
                        return RedirectToAction("Index", "NotFound", new { entity = "Lecture", backUrl = "/ManageLecture/" });
                }
            }
            catch (Exception ex)
            {
                TempData["DangerMessage"] = ex.Message;
            }
            return View(lecture);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrUpdate(Lecture lecture)
        {
            if (!ModelState.IsValid)
                return (lecture.LectureId == 0) ? View() : View(lecture);

            try
            {
                using (var cn = new SqlConnection(ConnectionString))
                {
                    if (lecture.LectureId == 0)
                        LectureData.Insert(lecture, cn);
                    else
                        LectureData.Update(lecture, cn);
                }
            }
            catch (Exception ex)
            {
                TempData["DangerMessage"] = ex.Message;
                return (lecture.LectureId == 0) ? View() : View(lecture);
            }
            return RedirectToAction(nameof(Index));
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(Lecture lecture)
        {
            try
            {
                using (var cn = new SqlConnection(ConnectionString))
                {
                    if (LectureData.HasTeachers(lecture.LectureId, cn))
                    {
                        throw new Exception("This Lecture cannot be removed because it is associated with a teacher. Remove all associations first.");
                    }
                    else
                    {
                        LectureData.Delete(lecture.LectureId, cn);
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
