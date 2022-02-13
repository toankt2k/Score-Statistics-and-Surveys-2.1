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
    public class SemesterController : Controller
    {
        private XLT_TLU.Models.DTSTLUModels dbSet = new XLT_TLU.Models.DTSTLUModels();

        private DBIO data = new DBIO();

        // GET: Semester
        public ActionResult Index(List<XLT_TLU.Models.tbl_subject> ss)
        {
            //if (Session[Constants.ROLE_EXAM_MANAGERMENT].ToString() == Constants.ROLE_EXAM_MANAGERMENT)
            //{
                SelectList semester = new SelectList((from s in dbSet.tbl_semester select s), "id", "semester_name");
                SelectList course_year = new SelectList((from c_y in dbSet.tbl_course_year select c_y), "id", "name");
                SelectList subject = new SelectList((from s in dbSet.tbl_subject select s), "id", "subject_name");
                SelectList semester_register_period = new SelectList((from s_r_p in dbSet.tbl_semester_register_period select s_r_p), "id", "name");

                //SelectList cateList = new SelectList(cate, "ID", "THELOAI_NAME");

                ViewBag.semester = semester;
                ViewBag.course_year = course_year;
                ViewBag.subject = subject;
                ViewBag.semester_regitster_period = semester_register_period;
                ViewBag.data = dbSet.tbl_subject.Where(s => s.id < 100).ToList();
                return View();
            //}
            //return RedirectToAction("Error", "Login");
        }

        public JsonResult getSubject(List<string> semesterId, string periodId, string courseId)
        {
            long period_id = 0;
            long course_id = 0;
            if (periodId != "") period_id = long.Parse(periodId);
            if (courseId != "") course_id = long.Parse(courseId);

            try
            {
                var list_subject = (from s in dbSet.tbl_subject
                                    join s1 in dbSet.tbl_semester_subject
                                    on s.id equals s1.subject_id
                                    where semesterId.Count == 0 ?true: semesterId.Contains(s1.semester_id.ToString())
                                    && (periodId == "" ?true:s1.register_period_id == period_id)
                                    && (courseId == "" ?true:s1.course_year_id == course_id )
                                    select new { 
                                        s.id,
                                        s.subject_name
                                    }).ToList();

                return Json(new { code = 200, data = list_subject }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { code = 500, data = "Không có dữ liệu!!" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult getClass(List<string> semester, long khoaHoc, long dotHoc, long monHoc, string type)
        {
            try
            {   //danh sách các nhóm môn học
                var subject = dbSet.tbl_semester_subject.Where(s => s.register_period_id == dotHoc
                                     && s.subject_id == monHoc
                                     && s.course_year_id == khoaHoc
                                     && semester.Contains(s.semester_id.ToString())).FirstOrDefault();
                dynamic list;
                if (type == "1") {
                    list = (from s in dbSet.tbl_course_subject
                            join class_student in dbSet.tbl_student_course_subject
                            on s.id equals class_student.course_subject_id
                            join student in dbSet.tbl_student
                            on class_student.student_id equals student.id
                            where s.semester_subject_id == subject.id
                            select new
                            {
                                id = s.id,
                                name = s.display_name
                            }).Distinct().ToList();

                }
                else
                {
                    list = (from cs in dbSet.tbl_course_subject
                                             join scs in dbSet.tbl_student_course_subject
                                             on cs.id equals scs.course_subject_id
                                             join student in dbSet.tbl_student
                                             on scs.student_id equals student.id
                                             join enroll in dbSet.tbl_enrollment_class
                                             on student.class_id equals enroll.id
                                             where cs.semester_subject_id == subject.id
                                             select new
                                             {
                                                 id = enroll.id,
                                                 name = enroll.className
                                             }).Distinct().ToList();
                }

                return Json(new { code = 200, data =  list, name = type}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { code = 500, data = "Không có dữ liệu!!" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ExportAll(string type, string subject, List<string> semester, List<string> data, string mark)
        {
            try
            {
                var tp = type;
                var subject_id = long.Parse(subject);

                var subj = dbSet.tbl_subject.Find(subject_id);

                ExcelExport export = new ExcelExport();
                var list_gr = new List<tbl_course_subject>();
                var list_class = new List<tbl_enrollment_class>();
                if (tp == "1")
                {
                    foreach(var id in data)
                    {
                        list_gr.Add(dbSet.tbl_course_subject.Find(int.Parse(id)));
                    }
                    var result = export.ExportBySemester(list_gr, semester, subj, mark);
                    //return File(result, "xlsx/xls", "Phổ điểm" + semes.semester_name + ".xlsx");
                    return Json(new { code = 200, name = "Phổ điểm Tlu.xlsx", data = Convert.ToBase64String(result, 0, result.Length) }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    foreach (var id in data)
                    {
                        list_class.Add(dbSet.tbl_enrollment_class.Find(int.Parse(id)));
                    }
                    var result = export.ExportByClass(list_class, semester, subj, mark);
                    //return File(result, "xlsx/xls", "Phổ điểm" + semes.semester_name + ".xlsx");
                    return Json(new { code = 200, name = "Phổ điểm Tlu.xlsx", data = Convert.ToBase64String(result, 0, result.Length) }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new { code = 500, mgs = e.ToString()  }, JsonRequestBehavior.AllowGet);
            }
        }

        //public JsonResult Export(string type, string subject, string semester, string data)//xuat 1 bang
        //{
        //    try
        //    {
        //        long _id = long.Parse(data);
        //        var tp = type;
        //        var subject_id = long.Parse(subject);
        //        var semester_id = long.Parse(semester);

        //        var semes = dbSet.tbl_semester.Find(semester_id);
        //        var subj = dbSet.tbl_subject.Find(subject_id);

        //        ExcelExport export = new ExcelExport();
        //        var list_gr = new List<tbl_course_subject>();
        //        var list_class = new List<tbl_enrollment_class>();
        //        if (tp == "1")
        //        {
        //            list_gr = dbSet.tbl_course_subject.Where(s => s.id == _id).ToList();//tim 1 nhom
        //            var result = export.ExportBySemester(list_gr, semes, subj);
        //            return Json(new { code = 200, name = "Phổ điểm" + semes.semester_name + ".xlsx", data = Convert.ToBase64String(result, 0, result.Length) }, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            list_class = dbSet.tbl_enrollment_class.Where(s => s.id == _id).ToList();
        //            var result = export.ExportByClass(list_class, semes, subj);
        //            return Json(new { code = 200, name = "Phổ điểm" + semes.semester_name + ".xlsx", data = Convert.ToBase64String(result, 0, result.Length) }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(new { code = 500, mgs = e.ToString() }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        public JsonResult GetMark(List<string> listId, string type, string subject, List<string> semester)
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

                        list_result.AddRange(data.GetMarkBySemester(listId, _subject, semester));
                    }
                    else
                    {
                        long _subject = long.Parse(subject);

                        list_result.AddRange(data.GetMarkByClass(listId, _subject, semester));
                    }
                }
                var resultExam = new List<MarkStatiticBySemester>();
                var resultFinal = new List<MarkStatiticBySemester>();
                var resultQt = new List<MarkStatiticBySemester>();

                int stt = 0;
                int tt = 0;
                foreach (var cl in list_result)
                {
                    int[] list_mark = { 0, 0, 0, 0, 0 };
                    int[] list_mark_final = { 0, 0, 0, 0, 0 };
                    int[] list_mark_QT = { 0, 0, 0, 0, 0 };
                    int total = cl.Where(ss => ss.status == 0).ToList().Count;
                    for(int i = 0; i < 5; i++)
                    {
                        list_mark[i] = cl.Where(ss =>ss.status == 0 && getCharMark(ss.mark_exam) == i).ToList().Count;
                        list_mark_final[i] = cl.Where(ss => ss.status == 0 && getCharMark(ss.mark_final) == i).ToList().Count;
                        list_mark_QT[i] = cl.Where(ss => ss.status == 0 && getCharMark(ss.mark) == i).ToList().Count;
                        listMark[i] += list_mark[i];
                        listMarkFinal[i] += list_mark_final[i];
                        listMarkQT[i] += list_mark_QT[i];
                    }
                    stt++;
                    resultExam.Add(new MarkStatiticBySemester(stt,cl.Key.className.ToString(),cl.Key.teacherName.ToString(), list_mark[4], list_mark[3], list_mark[2], list_mark[1], list_mark[0], total, cl.Key.subject.ToString()));
                    resultFinal.Add(new MarkStatiticBySemester(stt,cl.Key.className.ToString(), cl.Key.teacherName.ToString(), list_mark_final[4], list_mark_final[3], list_mark_final[2], list_mark_final[1], list_mark_final[0], total, cl.Key.subject.ToString()));
                    resultQt.Add(new MarkStatiticBySemester(stt,cl.Key.className.ToString(), cl.Key.teacherName.ToString(), list_mark_QT[4], list_mark_QT[3], list_mark_QT[2], list_mark_QT[1], list_mark_QT[0], total, cl.Key.subject.ToString()));
                    tt += total;
                }
                double[] listMark1 = { 0, 0, 0, 0, 0 };
                double[] listMarkFinal1 = { 0, 0, 0, 0, 0 };
                double[] listMarkQT1 = { 0, 0, 0, 0, 0 };
                for (int i = 0; i < 5; i++)
                {
                    listMark1[i] = tt == 0 ? 0 : Math.Round((double)(listMark[i] * 100) / tt, 2);
                    listMarkFinal1[i] = tt == 0 ? 0 : Math.Round((double)(listMarkFinal[i] * 100) / tt, 2);
                    listMarkQT1[i] = tt == 0 ? 0 : Math.Round((double)(listMarkQT[i] * 100) / tt, 2);
                }

                return Json(new { code = 200, dataQt = resultQt, dataFinal = resultFinal, dataExam = resultExam, chart_mark = new { qt = listMarkQT1, exam = listMark1, final = listMarkFinal1 } }, JsonRequestBehavior.AllowGet);
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
                if (m.Length > 5) return -1;
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