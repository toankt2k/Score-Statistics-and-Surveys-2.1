using OfficeOpenXml;
using OfficeOpenXml.Style;
using PhoDiem_TLU.Core;
using PhoDiem_TLU.DatabaseIO;
using PhoDiem_TLU.Helpers;
using PhoDiem_TLU.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace PhoDiem_TLU.Controllers
{
    public class MarkController : Controller
    {
        // GET: Mark
        private DBIO dBIO = new DBIO();
        private ExcelExport ex = new ExcelExport();

        //Khai báo log
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(MarkController));
        public ActionResult Index()
        {
            setViewbagMarkOption();
            return View();
        }
        private void setViewbagMarkOption()
        {
            var subject = dBIO.getSubject();
            var semesters = dBIO.GetSemesters();
            List<SelectListItem> provinces = new List<SelectListItem>();
            provinces.Add(new SelectListItem() { Text = "Hiển thị theo khoa", Value = "HTK" });
            provinces.Add(new SelectListItem() { Text = "Hiển thị theo giáo viên", Value = "HTGV" });
            provinces.Add(new SelectListItem() { Text = "Hiển thị theo lớp quản lý", Value = "HTL" });

            List<SelectListItem> listOption = new List<SelectListItem>();
            listOption.Add(new SelectListItem() { Text = "Điểm quá trình", Value = "Điểm quá trình" });
            listOption.Add(new SelectListItem() { Text = "Điểm thi", Value = "Điểm thi" });
            listOption.Add(new SelectListItem() { Text = "Điểm tổng kết", Value = "Điểm tổng kết" });
            SelectList showOption = new SelectList(provinces, "Value", "Text");
            SelectList markOption = new SelectList(listOption, "Value", "Text");
            SelectList s = new SelectList(subject, "id", "subject_name");
            SelectList y = new SelectList(semesters, "id", "semester_name");
            ViewBag.subjects = s;
            ViewBag.semesters = y;
            ViewBag.showOption = showOption;
            ViewBag.markOption = markOption;
        }
        [HttpPost]

        public JsonResult MarkResult(long? subjectID, long? semesterIDStart, long? semesterIDEnd, string showoption, string markOption)
        {

            try
            {

                if (subjectID != null && semesterIDStart != null && semesterIDEnd != null && showoption != null && markOption != null)
                {

                    if (showoption == "HTK")
                    {
                        if (markOption == "Điểm quá trình")
                        {
                            var studentMarks = dBIO.getStudentMark(subjectID, semesterIDStart, semesterIDEnd, 2);
                            //var dataExcel = dBIO.groupMarkByDepartment(studentMarks);
                            var dataTable = dBIO.getMarkByDepartment(studentMarks);
                            var sumMark = dBIO.getSumMarks(dataTable);
                            return Json(new
                            {
                                code = 200,
                                //studentMarks,
                                //dataExcel,
                                dataTable,
                                sumMark,
                                subjectID,
                                semesterIDStart,
                                semesterIDEnd,
                                showoption,
                                markOption

                            }, JsonRequestBehavior.AllowGet);
                        }
                        else if (markOption == "Điểm thi")
                        {
                            var studentMarks = dBIO.getStudentMark(subjectID, semesterIDStart, semesterIDEnd, 3);
                            //var dataExcel = dBIO.groupMarkByDepartment(studentMarks);
                            var dataTable = dBIO.getMarkByDepartment(studentMarks);
                            var sumMark = dBIO.getSumMarks(dataTable);
                            return Json(new
                            {
                                code = 200,
                                //studentMarks,
                                //dataExcel,
                                dataTable,
                                sumMark,
                                subjectID,
                                semesterIDStart,
                                semesterIDEnd,
                                showoption,
                                markOption

                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            //Diem tong ket
                            var studentMarks = dBIO.getStudentMark(subjectID, semesterIDStart, semesterIDEnd);
                            //var dataExcel = dBIO.groupMarkByDepartment(studentMarks);
                            var dataTable = dBIO.getMarkByDepartment(studentMarks);
                            var sumMark = dBIO.getSumMarks(dataTable);
                            return Json(new
                            {
                                code = 200,
                                //studentMarks,
                                //dataExcel,
                                dataTable,
                                sumMark,
                                subjectID,
                                semesterIDStart,
                                semesterIDEnd,
                                showoption,
                                markOption

                            }, JsonRequestBehavior.AllowGet);

                        }
                    }
                    else if (showoption == "HTGV")
                    {
                        if (markOption == "Điểm quá trình")
                        {

                            var studentMarks = dBIO.getStudentMark(subjectID, semesterIDStart, semesterIDEnd, 2);
                            //var dataExcel = dBIO.groupMarkByTeacher(studentMarks);
                            var dataTable = dBIO.getMarkByTeacher(studentMarks);
                            var sumMark = dBIO.getSumMarks(dataTable);
                            return Json(new
                            {
                                code = 200,
                                //studentMarks,
                                //dataExcel,
                                dataTable,
                                sumMark,
                                subjectID,
                                semesterIDStart,
                                semesterIDEnd,
                                showoption,
                                markOption

                            }, JsonRequestBehavior.AllowGet);
                        }
                        else if (markOption == "Điểm thi")
                        {
                            var studentMarks = dBIO.getStudentMark(subjectID, semesterIDStart, semesterIDEnd,3);
                            //var dataExcel = dBIO.groupMarkByTeacher(studentMarks);
                            var dataTable = dBIO.getMarkByTeacher(studentMarks);
                            var sumMark = dBIO.getSumMarks(dataTable);
                            return Json(new
                            {
                                code = 200,
                                //studentMarks,
                                //dataExcel,
                                dataTable,
                                sumMark,
                                subjectID,
                                semesterIDStart,
                                semesterIDEnd,
                                showoption,
                                markOption

                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            var studentMarks = dBIO.getStudentMark(subjectID, semesterIDStart, semesterIDEnd);
                            //var dataExcel = dBIO.groupMarkByTeacher(studentMarks);
                            var dataTable = dBIO.getMarkByTeacher(studentMarks);
                            var sumMark = dBIO.getSumMarks(dataTable);
                            return Json(new
                            {
                                code = 200,
                                //studentMarks,
                                //dataExcel,
                                dataTable,
                                sumMark,
                                subjectID,
                                semesterIDStart,
                                semesterIDEnd,
                                showoption,
                                markOption

                            }, JsonRequestBehavior.AllowGet);

                        }
                    }
                    else //Diem theo lop quan ly
                    {
                        if (markOption == "Điểm quá trình")
                        {
                            var studentMarks = dBIO.getStudentMark(subjectID, semesterIDStart, semesterIDEnd, 2);
                            //var dataExcel = dBIO.groupMarkByDepartment(studentMarks);
                            var dataTable = dBIO.getMarksEnrollmentClass(studentMarks);
                            var sumMark = dBIO.getSumMarks(dataTable);
                            return Json(new
                            {
                                code = 200,
                                //studentMarks,
                                //dataExcel,
                                dataTable,
                                sumMark,
                                subjectID,
                                semesterIDStart,
                                semesterIDEnd,
                                showoption,
                                markOption

                            }, JsonRequestBehavior.AllowGet);
                        }
                        else if (markOption == "Điểm thi")
                        {
                            var studentMarks = dBIO.getStudentMark(subjectID, semesterIDStart, semesterIDEnd, 3);
                            //var dataExcel = dBIO.groupMarkByDepartment(studentMarks);
                            var dataTable = dBIO.getMarksEnrollmentClass(studentMarks);
                            var sumMark = dBIO.getSumMarks(dataTable);
                            return Json(new
                            {
                                code = 200,
                                //studentMarks,
                                //dataExcel,
                                dataTable,
                                sumMark,
                                subjectID,
                                semesterIDStart,
                                semesterIDEnd,
                                showoption,
                                markOption

                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            //Diem tong ket
                            var studentMarks = dBIO.getStudentMark(subjectID, semesterIDStart, semesterIDEnd);
                            //var dataExcel = dBIO.groupMarkByDepartment(studentMarks);
                            var dataTable = dBIO.getMarksEnrollmentClass(studentMarks);
                            var sumMark = dBIO.getSumMarks(dataTable);
                            return Json(new
                            {
                                code = 200,
                                //studentMarks,
                                //dataExcel,
                                dataTable,
                                sumMark,
                                subjectID,
                                semesterIDStart,
                                semesterIDEnd,
                                showoption,
                                markOption

                            }, JsonRequestBehavior.AllowGet);

                        }
                    }
                }
                return Json(new { code = 404 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                return Json(new { code = 404, msg = e.Message }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { code = 404 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Export(long? subjectID, long? semesterIDStart, long? semesterIDEnd, string showoption, 
            string markOption, List<long?> checkboxIDs)

        {
            try
            {

                if (subjectID != null && semesterIDStart != null && semesterIDEnd != null && showoption != null 
                    && markOption != null)
                {
                    
                    if (showoption == "HTK")
                    {
                        string subjectName = dBIO.getSubject(subjectID);
                        long numberOfCredit = dBIO.getNumberOfCredit(subjectID);
                        string semesterNameStart = dBIO.getSemeterName(semesterIDStart);
                        string semesterNameEnd = dBIO.getSemeterName(semesterIDEnd);
                        List<StudentMarkViewModel> studentMarks;
                        List<MarkByDepartment> dataTable;
                        if (markOption == "Điểm quá trình")
                        {
                            studentMarks = dBIO.getStudentMarkExcelDepartment(subjectID, semesterIDStart, semesterIDEnd, 2,checkboxIDs);
                            dataTable = dBIO.getMarkByDepartment(studentMarks);

                        }
                        else if (markOption == "Điểm thi")
                        {
                             studentMarks = dBIO.getStudentMarkExcelDepartment(subjectID, semesterIDStart, semesterIDEnd, 3,checkboxIDs);
                             dataTable = dBIO.getMarkByDepartment(studentMarks);
                        }
                        else
                        {
                            //Diem tong ket
                            studentMarks = dBIO.getStudentMarkExcelDepartment(subjectID, semesterIDStart, semesterIDEnd, checkboxIDs);
                            dataTable = dBIO.getMarkByDepartment(studentMarks);
                        }
                        var fileName = $"{markOption}_{subjectName}_{semesterNameStart}_{semesterNameEnd}.xlsx";
                        var data = ex.ExportExcelDataDepartment(markOption, subjectName, numberOfCredit,semesterNameStart,semesterNameEnd,studentMarks,dataTable);
                        return Json(new { code = 200, fileName, result = Convert.ToBase64String(data) }, JsonRequestBehavior.AllowGet);


                    }
                    else if (showoption == "HTGV")
                    {
                        string subjectName = dBIO.getSubject(subjectID);
                        long numberOfCredit = dBIO.getNumberOfCredit(subjectID);
                        string semesterNameStart = dBIO.getSemeterName(semesterIDStart);
                        string semesterNameEnd = dBIO.getSemeterName(semesterIDEnd);
                        List<StudentMarkViewModel> studentMarks;
                        List<MarkRate> dataTable;
                        if (markOption == "Điểm quá trình")
                        {
                            studentMarks = dBIO.getStudentMarkExcelTeacher(subjectID, semesterIDStart, semesterIDEnd, 2, checkboxIDs);
                            dataTable = dBIO.getMarkByTeacher(studentMarks);

                        }
                        else if (markOption == "Điểm thi")
                        {
                            studentMarks = dBIO.getStudentMarkExcelTeacher(subjectID, semesterIDStart, semesterIDEnd, 3, checkboxIDs);
                            dataTable = dBIO.getMarkByTeacher(studentMarks);
                        }
                        else
                        {
                            //Diem tong ket
                            studentMarks = dBIO.getStudentMarkExcelTeacher(subjectID, semesterIDStart, semesterIDEnd, checkboxIDs);
                            dataTable = dBIO.getMarkByTeacher(studentMarks);
                        }
                        var fileName = $"{markOption}_{subjectName}_{semesterNameStart}_{semesterNameEnd}.xlsx";
                        var data = ex.ExportExcelDataTeacher(markOption, subjectName, numberOfCredit, semesterNameStart, semesterNameEnd, studentMarks, dataTable);
                        return Json(new { code = 200, fileName, result = Convert.ToBase64String(data) }, JsonRequestBehavior.AllowGet);
                    }
                    else //Diem theo lop quan ly
                    {
                        string subjectName = dBIO.getSubject(subjectID);
                        long numberOfCredit = dBIO.getNumberOfCredit(subjectID);
                        string semesterNameStart = dBIO.getSemeterName(semesterIDStart);
                        string semesterNameEnd = dBIO.getSemeterName(semesterIDEnd);
                        List<StudentMarkViewModel> studentMarks;
                        List<MarksByEnrollmentClass> dataTable;
                        if (markOption == "Điểm quá trình")
                        {
                            studentMarks = dBIO.getStudentMarkExcelEnrollmentClass(subjectID, semesterIDStart, semesterIDEnd, 2, checkboxIDs);
                            dataTable = dBIO.getMarksEnrollmentClass(studentMarks);

                        }
                        else if (markOption == "Điểm thi")
                        {
                            studentMarks = dBIO.getStudentMarkExcelEnrollmentClass(subjectID, semesterIDStart, semesterIDEnd, 3, checkboxIDs);
                            dataTable = dBIO.getMarksEnrollmentClass(studentMarks);
                        }
                        else
                        {
                            //Diem tong ket
                            studentMarks = dBIO.getStudentMarkExcelEnrollmentClass(subjectID, semesterIDStart, semesterIDEnd, checkboxIDs);
                            dataTable = dBIO.getMarksEnrollmentClass(studentMarks);
                        }
                        var fileName = $"{markOption}_{subjectName}_{semesterNameStart}_{semesterNameEnd}.xlsx";
                        var data = ex.ExportExcelDataEnrollmentClass(markOption, subjectName, numberOfCredit, semesterNameStart, semesterNameEnd, studentMarks, dataTable);
                        return Json(new { code = 200, fileName, result = Convert.ToBase64String(data) }, JsonRequestBehavior.AllowGet);
                    }

                    
                }
                return Json(new { code = 404 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                return Json(new { code = 404, msg = e.Message }, JsonRequestBehavior.AllowGet);
            }
            
        }
        

        
        public ActionResult MarkByTeacher()
        {
            log.Info("Info");
            setViewBagTeacher();
            setViewBagYear();
            setViewBagMarkOption();
            setViewBagCourseYear();
            return View();
        }
        [HttpPost]
        public JsonResult MarkByTeacher(int teacher, int semester,int courseYear,string markOption)
        {
            try
            {
                List<MarkRate> list ;
                if(markOption == "DTK") list = dBIO.getRateMarkByTeacher(teacher, courseYear, semester);
                else list = dBIO.getRateMarkByTeacher(teacher, courseYear, semester,markOption);

                var dataChart = dBIO.getSumMarks(list);
                return Json(new { code = 200,list,dataChart }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return Json(new {code = 500}, JsonRequestBehavior.AllowGet);
            }
        }
        private void setViewBagYear()
        {
            var years = dBIO.getYear();
            SelectList y = new SelectList(years, "year", "year");
            ViewBag.years = y;
        }
        private void setViewBagTeacher()
        {
            var teacherName = dBIO.getListNameTeachers();
            
            SelectList y = new SelectList(teacherName, "id", "name");
            ViewBag.teacherName = y;
        }
        private void setViewBagMarkOption()
        {
            List<SelectListItem> listOption = new List<SelectListItem>();
            listOption.Add(new SelectListItem() { Text = "Điểm quá trình", Value = "DQT" });
            listOption.Add(new SelectListItem() { Text = "Điểm thi", Value = "DT" });
            listOption.Add(new SelectListItem() { Text = "Điểm tổng kết", Value = "DTK" });
            SelectList markOption = new SelectList(listOption, "Value", "Text");
            ViewBag.markOption = markOption;
        }
        private void setViewBagCourseYear()
        {
            var courseYear = dBIO.getCourseYear();
            SelectList s = new SelectList(courseYear, "id", "name");
            ViewBag.courseYear = s;
        }
        public JsonResult getSemester(int startYear,int endYear)
        {
            try
            {
                var semester = dBIO.getSemester(startYear,endYear);
                return Json(new {code = 200,semester},JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return Json(new {code = 500},JsonRequestBehavior.AllowGet);
            }
        }
    }
    
}