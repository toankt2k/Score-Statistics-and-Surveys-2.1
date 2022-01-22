using PhoDiem_TLU.Commons;
using PhoDiem_TLU.Core;
using PhoDiem_TLU.DatabaseIO;
using PhoDiem_TLU.Helpers;
using PhoDiem_TLU.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using XLT_TLU.Models;

namespace PhoDiem_TLU.Controllers
{
    public class StatisticByYearController : Controller
    {
        private XLT_TLU.Models.DTSTLUModels dbSet = new XLT_TLU.Models.DTSTLUModels();

        private DBIO data = new DBIO();

        // GET: Semester
        public ActionResult Index(List<XLT_TLU.Models.tbl_subject> ss)
        {
            //if (Session[Constants.ROLE_EXAM_MANAGERMENT].ToString() == Constants.ROLE_EXAM_MANAGERMENT)
            //{
            SelectList year = new SelectList((from sy in dbSet.tbl_shool_year select sy), "id", "year");
            SelectList semester = new SelectList((from s in dbSet.tbl_semester select s), "id", "semester_name");
            SelectList course_year = new SelectList((from c_y in dbSet.tbl_course_year select c_y), "id", "name");
            SelectList subject = new SelectList((from s in dbSet.tbl_subject select s), "id", "subject_name");
            SelectList semester_register_period = new SelectList((from s_r_p in dbSet.tbl_semester_register_period select s_r_p), "id", "name");

            //SelectList cateList = new SelectList(cate, "ID", "THELOAI_NAME");
            ViewBag.year = year;
            ViewBag.semester = semester; 
            ViewBag.course_year = course_year;
            ViewBag.subject = subject;
            ViewBag.semester_regitster_period = semester_register_period;
            ViewBag.data = dbSet.tbl_subject.Where(s => s.id < 100).ToList();
            return View();
            //}
            //return RedirectToAction("Error", "Login");
        }

        public JsonResult getSubject(string schoolYearId, string courseYearId, string subjectId)
        {
            long school_year_id = 0;
            long course_year_id = 0;
            long subject_id = 0;
            if (schoolYearId != "") school_year_id = long.Parse(schoolYearId);
            if (courseYearId != "") course_year_id = long.Parse(courseYearId);
            if (subjectId != "") subject_id = long.Parse(subjectId);

            try
            {
                var listSubject = (from sub in dbSet.tbl_subject
                                   join ss in dbSet.tbl_semester_subject
                                   on sub.id equals ss.subject_id
                                   join sem in dbSet.tbl_semester
                                   on ss.semester_id equals sem.id
                                   join cy in dbSet.tbl_course_year
                                   on ss.course_year_id equals cy.id
                                   join sy in dbSet.tbl_shool_year
                                   on sem.school_year_id equals sy.id
                                   where (
                                       sy.id == school_year_id && cy.id == course_year_id
                                   )
                                   select new
                                   {
                                       sub.id,
                                       sub.subject_name
                                   }).Distinct().ToList();

                return Json(new { code = 200, data = listSubject }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { code = 500, data = "Không có dữ liệu!!" }, JsonRequestBehavior.AllowGet);
            }
        }
      
        public JsonResult getSemesterRegistorPeriod(string semesterId, string courseId)
        {
            long semester_id = 0;            
            long course_id = 0;            
            if (semesterId != "") semester_id = long.Parse(semesterId);
            if (courseId != "") course_id = long.Parse(courseId);

            try
            {   //lấy danh sách các đợt học (chính - phụ -  tăng cường...)
                var listSemesterRegistorPeriod = (from sem in dbSet.tbl_semester
                                    join srp in dbSet.tbl_semester_register_period
                                    on sem.id equals srp.semeter_id   
                                    where sem.id == semester_id
                                    select new
                                    {
                                        srp.id,
                                        srp.name
                                    }).ToList();

                return Json(new { code = 200, data = listSemesterRegistorPeriod }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { code = 500, data = "Không có dữ liệu!!" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getCourseYear(string schoolYearId)
        {
            long school_year_id = 0;
            if (schoolYearId != "") school_year_id = long.Parse(schoolYearId);

            try
            {   //lấy danh sách các khóa học trong năm bằng schoolYearId
                var listCourseYear = (from sem in dbSet.tbl_semester
                                                  join ss in dbSet.tbl_semester_subject
                                                  on sem.id equals ss.semester_id
                                                  join sy in dbSet.tbl_shool_year
                                                  on sem.school_year_id equals sy.id                                                  
                                                  join cy in dbSet.tbl_course_year
                                                  on ss.course_year_id equals cy.id
                                                  where sy.id == school_year_id
                                                   select new
                                                  {
                                                     cy.id,
                                                     cy.name,
                                                  }).Distinct().ToList();

                return Json(new { code = 200, data = listCourseYear }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { code = 500, data = "Không có dữ liệu!!" }, JsonRequestBehavior.AllowGet);
            }
        }
        
        public JsonResult getClass(long schoolYear, long courseYear, long subject, string type)
        {            
            try
            {   //danh sách các semesterSubject
                var listSemesterSubject = (from ss in dbSet.tbl_semester_subject
                                           join sem in dbSet.tbl_semester
                                           on ss.semester_id equals sem.id
                                           join sy in dbSet.tbl_shool_year
                                           on sem.school_year_id equals sy.id
                                           join sub in dbSet.tbl_subject
                                           on ss.subject_id equals sub.id
                                           join cy in dbSet.tbl_course_year
                                           on ss.course_year_id equals cy.id
                                           where (sy.id == schoolYear && cy.id == courseYear && sub.id == subject)
                                           select new
                                           {
                                               id = ss.id,
                                           }
                                           ).ToList().FirstOrDefault();
                dynamic list;
                //lấy danh sách nhóm theo lớp học phần
                if (type == "1")
                {
                    list = (from s in dbSet.tbl_course_subject
                            join class_student in dbSet.tbl_student_course_subject
                            on s.id equals class_student.course_subject_id
                            join student in dbSet.tbl_student
                            on class_student.student_id equals student.id
                            where s.semester_subject_id == listSemesterSubject.id
                            select new
                            {
                                id = s.id,
                                name = s.display_name
                            }).Distinct().ToList();

                }
                //lấy danh sách theo lớp quản lý
                else
                {
                    list = (from cs in dbSet.tbl_course_subject
                            join scs in dbSet.tbl_student_course_subject
                            on cs.id equals scs.course_subject_id
                            join student in dbSet.tbl_student
                            on scs.student_id equals student.id
                            join enroll in dbSet.tbl_enrollment_class
                            on student.class_id equals enroll.id
                            where cs.semester_subject_id == listSemesterSubject.id
                            select new
                            {
                                id = enroll.id,
                                name = enroll.className
                            }).Distinct().ToList();
                }

                return Json(new { code = 200, data = list, name = type }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { code = 500, data = "Không có dữ liệu!!" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ExportAll(string type, string subject, string semester, List<string> data)
        {
            try
            {
                var tp = type;
                var subject_id = long.Parse(subject);
                var semester_id = long.Parse(semester);

                var semes = dbSet.tbl_semester.Find(semester_id);
                var subj = dbSet.tbl_subject.Find(subject_id);

                ExcelExport export = new ExcelExport();
                var list_gr = new List<tbl_course_subject>();
                var list_class = new List<tbl_enrollment_class>();
                if (tp == "1")
                {
                    foreach (var id in data)
                    {
                        list_gr.Add(dbSet.tbl_course_subject.Find(int.Parse(id)));
                    }
                    var result = export.ExportBySemester(list_gr, semes, subj);
                    //return File(result, "xlsx/xls", "Phổ điểm" + semes.semester_name + ".xlsx");
                    return Json(new { code = 200, name = "Phổ điểm" + semes.semester_name + ".xlsx", data = Convert.ToBase64String(result, 0, result.Length) }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    foreach (var id in data)
                    {
                        list_class.Add(dbSet.tbl_enrollment_class.Find(int.Parse(id)));
                    }
                    var result = export.ExportByClass(list_class, semes, subj);
                    //return File(result, "xlsx/xls", "Phổ điểm" + semes.semester_name + ".xlsx");
                    return Json(new { code = 200, name = "Phổ điểm" + semes.semester_name + ".xlsx", data = Convert.ToBase64String(result, 0, result.Length) }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new { code = 500, mgs = e.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }
        
        public JsonResult Export(string type, string subject, string semester, string data)//xuat 1 bang
        {
            try
            {
                long _id = long.Parse(data);
                var tp = type;
                var subject_id = long.Parse(subject);
                var semester_id = long.Parse(semester);

                var semes = dbSet.tbl_semester.Find(semester_id);
                var subj = dbSet.tbl_subject.Find(subject_id);

                ExcelExport export = new ExcelExport();
                var list_gr = new List<tbl_course_subject>();
                var list_class = new List<tbl_enrollment_class>();
                if (tp == "1")
                {
                    list_gr = dbSet.tbl_course_subject.Where(s => s.id == _id).ToList();//tim 1 nhom
                    var result = export.ExportBySemester(list_gr, semes, subj);
                    return Json(new { code = 200, name = "Phổ điểm" + semes.semester_name + ".xlsx", data = Convert.ToBase64String(result, 0, result.Length) }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    list_class = dbSet.tbl_enrollment_class.Where(s => s.id == _id).ToList();
                    var result = export.ExportByClass(list_class, semes, subj);
                    return Json(new { code = 200, name = "Phổ điểm" + semes.semester_name + ".xlsx", data = Convert.ToBase64String(result, 0, result.Length) }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new { code = 500, mgs = e.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetMark(List<string> listId, string type, string subject, string semester)
        {
            try
            {
                int[] listMark = { 0, 0, 0, 0, 0 };
                int[] listMarkFinal = { 0, 0, 0, 0, 0 };
                int[] listMarkQT = { 0, 0, 0, 0, 0 };

                var list_result = new List<IGrouping<dynamic, MarkBySemester>>();
                if (listId != null && listId.Count != 0)
                {
                    if (type == "1")
                    {
                        long _subject = long.Parse(subject);
                        long _semester = long.Parse(semester);

                        list_result.AddRange(data.GetMarkBySemester(listId, _subject, _semester));
                    }
                    else
                    {
                        long _subject = long.Parse(subject);
                        long _semester = long.Parse(semester);

                        list_result.AddRange(data.GetMarkByClass(listId, _subject, _semester));
                    }
                }
                var resultExam = new List<MarkStatiticBySemester>();
                var resultFinal = new List<MarkStatiticBySemester>();
                var resultQt = new List<MarkStatiticBySemester>();

                int stt = 0;
                foreach (var cl in list_result)
                {
                    int[] list_mark = { 0, 0, 0, 0, 0 };
                    int[] list_mark_final = { 0, 0, 0, 0, 0 };
                    int[] list_mark_QT = { 0, 0, 0, 0, 0 };
                    int total = cl.Where(ss => ss.status == 0).ToList().Count;
                    for (int i = 0; i < 5; i++)
                    {
                        list_mark[i] = cl.Where(ss => ss.status == 0 && getCharMark(ss.mark_exam) == i).ToList().Count;
                        list_mark_final[i] = cl.Where(ss => ss.status == 0 && getCharMark(ss.mark_final) == i).ToList().Count;
                        list_mark_QT[i] = cl.Where(ss => ss.status == 0 && getCharMark(ss.mark) == i).ToList().Count;
                        listMark[i] += list_mark[i];
                        listMarkFinal[i] += list_mark_final[i];
                        listMarkQT[i] += list_mark_QT[i];
                    }
                    stt++;
                    resultExam.Add(new MarkStatiticBySemester(stt, cl.Key.className.ToString(), cl.Key.teacherName.ToString(), list_mark[4], list_mark[3], list_mark[2], list_mark[1], list_mark[0], total, cl.Key.subject.ToString()));
                    resultFinal.Add(new MarkStatiticBySemester(stt, cl.Key.ToString(), cl.Key.teacherName.ToString(), list_mark_final[4], list_mark_final[3], list_mark_final[2], list_mark_final[1], list_mark_final[0], total, cl.Key.subject.ToString()));
                    resultQt.Add(new MarkStatiticBySemester(stt, cl.Key.ToString(), cl.Key.teacherName.ToString(), list_mark_QT[4], list_mark_QT[3], list_mark_QT[2], list_mark_QT[1], list_mark_QT[0], total, cl.Key.subject.ToString()));

                }
                return Json(new { code = 200, data = resultExam, chart_mark = new { qt = listMarkQT, exam = listMark, final = listMarkFinal } }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, data = "Không có dữ liệu!" }, JsonRequestBehavior.AllowGet);
            }
        }

        public int getCharMark(string m)
        {
            try
            {
                var mark = double.Parse(m);
                if (mark <= 10 && mark >= 8.45) return 4;
                if (mark <= 8.44 && mark >= 6.95) return 3;
                if (mark <= 6.94 && mark >= 5.45) return 2;
                if (mark <= 5.44 && mark >= 3.95) return 1;
                return 0;
            }
            catch
            {
                return -1;
            }
        }
    }
}