using PhoDiem_TLU.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XLT_TLU.Models;

namespace PhoDiem_TLU.DatabaseIO
{
    
    public class DBIO
    {
        DTSTLUModels models = new DTSTLUModels();
        //Khai báo log
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(DBIO));
        //Lấy thông tin danh sách sinh viên của một môn học trong nhiều năm
        public List<Mark> getMarks(long? subject_id, long? school_year_id_start, long? school_year_id_end)
        {

            var list = (
                // lay hoc ki
                from school in models.tbl_shool_year
                join semester in models.tbl_semester
                on school.id equals semester.school_year_id

                //Lay hoc ki mon hoc
                join semesterSubject in models.tbl_semester_subject
                on semester.id equals semesterSubject.semester_id
                join subject in models.tbl_subject
                on semesterSubject.subject_id equals subject.id

                //tim nhom lop
                join couresSubject in models.tbl_course_subject
                on semesterSubject.id equals couresSubject.semester_subject_id

                // tim nhom lop sinh vien
                join studentCourseSubject in models.tbl_student_course_subject
                on couresSubject.id equals studentCourseSubject.course_subject_id

                //Tim ten giao vien
                join person in models.tbl_person
                on couresSubject.teacher_id equals person.id
                //tim diem tong ket
                from studentSubjectMark in models.tbl_student_subject_mark


                    //Tim nhom diem qua trinh
                from studentMark in models.tbl_student_mark


                    //
                where school.id >= school_year_id_start && school.id <= school_year_id_end && subject.id == subject_id


                //Tim diem
                && studentSubjectMark.semester_id == semester.id
                && studentSubjectMark.subject_id == subject.id
                && studentMark.student_course_subject_id == studentCourseSubject.id
                && studentMark.student_subject_mark_id == studentSubjectMark.id
                select new
                    {
                        semester = semester.id,
                        semesterName = semester.semester_name,
                        subjectID = subject.id,
                        subjectName = subject.subject_name,
                        numberOfCredit= subject.number_of_credit,
                        couresSubjectID = couresSubject.id,
                        courseSubjectName = couresSubject.display_name,
                        teacherID = couresSubject.teacher_id,
                        teacherName = person.display_name,
                        student_Mark = studentMark.mark,
                        student_Subject_Mark = studentSubjectMark.mark4,
                        startYearID = school_year_id_start,
                        endYearID = school_year_id_end,

                    }).ToList().Select(s=> new Mark(s.teacherID,s.teacherName,s.student_Mark,s.student_Subject_Mark,
                    s.subjectID,s.subjectName,s.numberOfCredit,s.startYearID,s.endYearID));


            return list.ToList();
        }
        // Lấy thông tin điểm tổng kết sinh viên của một giáo viên trong nhiều năm
        public List<Mark> getMarks(long? subject_id, long? school_year_id_start, long? school_year_id_end,long? teacherID)
        {

            var list = (
                // lay hoc ki
                from school in models.tbl_shool_year
                join semester in models.tbl_semester
                on school.id equals semester.school_year_id

                //Lay hoc ki mon hoc
                join semesterSubject in models.tbl_semester_subject
                on semester.id equals semesterSubject.semester_id
                join subject in models.tbl_subject
                on semesterSubject.subject_id equals subject.id

                //tim nhom lop
                join couresSubject in models.tbl_course_subject
                on semesterSubject.id equals couresSubject.semester_subject_id

                // tim nhom lop sinh vien
                join studentCourseSubject in models.tbl_student_course_subject
                on couresSubject.id equals studentCourseSubject.course_subject_id

                //Tim ten giao vien
                join person in models.tbl_person
                on couresSubject.teacher_id equals person.id
                //tim diem tong ket
                from studentSubjectMark in models.tbl_student_subject_mark


                    //Tim nhom diem qua trinh
                from studentMark in models.tbl_student_mark


                    //
                where school.id >= school_year_id_start && school.id <= school_year_id_end && subject.id == subject_id
                && couresSubject.teacher_id == teacherID

                //Tim diem
                && studentSubjectMark.semester_id == semester.id
                && studentSubjectMark.subject_id == subject.id
                && studentMark.student_course_subject_id == studentCourseSubject.id
                && studentMark.student_subject_mark_id == studentSubjectMark.id
                select new
                {
                    semester = semester.id,
                    semesterName = semester.semester_name,
                    subjectID = subject.id,
                    subjectName = subject.subject_name,
                    numberOfCredit = subject.number_of_credit,
                    couresSubjectID = couresSubject.id,
                    courseSubjectName = couresSubject.display_name,
                    teacherID = couresSubject.teacher_id,
                    teacherName = person.display_name,
                    student_Mark = studentMark.mark,
                    student_Subject_Mark = studentSubjectMark.mark4,
                    year = school.year

                }).ToList().Select(s => new Mark(s.teacherID, s.teacherName, s.student_Mark, s.student_Subject_Mark,
                s.subjectID, s.subjectName,s.year));


            return list.ToList();
        }
        //Lấy điểm tất cả sinh viên theo nhiều giáo viên
        public List<StudentCourseSubject> getMarks_2(long? subject_id, long? school_year_id_start, long? school_year_id_end)
        {

            var list = (
                // lay hoc ki
                from school in models.tbl_shool_year
                join semester in models.tbl_semester
                on school.id equals semester.school_year_id

                //Lay hoc ki mon hoc
                join semesterSubject in models.tbl_semester_subject
                on semester.id equals semesterSubject.semester_id
                join subject in models.tbl_subject
                on semesterSubject.subject_id equals subject.id

                //tim nhom lop
                join couresSubject in models.tbl_course_subject
                on semesterSubject.id equals couresSubject.semester_subject_id

                // tim nhom lop sinh vien
                join studentCourseSubject in models.tbl_student_course_subject
                on couresSubject.id equals studentCourseSubject.course_subject_id

                //Tim ten giao vien
                join person in models.tbl_person
                on couresSubject.teacher_id equals person.id
                
                //Tim nhom diem qua trinh
                from studentMark in models.tbl_student_mark     //
                where school.id >= school_year_id_start && school.id <= school_year_id_end && subject.id == subject_id
                && studentMark.student_id == studentCourseSubject.student_id
                && studentMark.subject_id == subject_id
                
                select new
                {
                    studentId = studentMark.student_id,
                    mark = studentMark.mark,
                    subjectId = subject.id,
                    couresSubjectID = couresSubject.id,
                    courseSubjectName = couresSubject.display_name,
                    teacherID = couresSubject.teacher_id,
                    teacherName = person.display_name,
                    subjectName = subject.subject_name,
                    numberOfCredit = subject.number_of_credit,
                    semesterID = semester.id,
                    semesterName = semester.semester_name,
                    subjectExamID = studentMark.subject_exam_id,
                    studentSubjectMarkID = studentMark.student_subject_mark_id,
                    startYearID = school_year_id_start,
                    endYearID = school_year_id_end

                }
                ).ToList().Select(x => new StudentCourseSubject(x.studentId,x.mark, x.subjectId,x.couresSubjectID,
                x.courseSubjectName,x.teacherID,
                x.teacherName,x.subjectName,x.numberOfCredit,x.semesterID, x.semesterName, x.subjectExamID,x.studentSubjectMarkID,
                x.startYearID,x.endYearID));


            return list.ToList();
        }
        //Lấy điểm tất cả sinh viên của một giáo viên
        public List<StudentCourseSubject> getMarks_2(long? subject_id, long? school_year_id_start, long? school_year_id_end,long? teacherID)
        {

            var list = (
                // lay hoc ki
                from school in models.tbl_shool_year
                join semester in models.tbl_semester
                on school.id equals semester.school_year_id

                //Lay hoc ki mon hoc
                join semesterSubject in models.tbl_semester_subject
                on semester.id equals semesterSubject.semester_id
                join subject in models.tbl_subject
                on semesterSubject.subject_id equals subject.id

                //tim nhom lop
                join couresSubject in models.tbl_course_subject
                on semesterSubject.id equals couresSubject.semester_subject_id

                // tim nhom lop sinh vien
                join studentCourseSubject in models.tbl_student_course_subject
                on couresSubject.id equals studentCourseSubject.course_subject_id

                //Tim ten giao vien
                join person in models.tbl_person
                on couresSubject.teacher_id equals person.id

                //Tim nhom diem qua trinh
                from studentMark in models.tbl_student_mark     //
                where school.id >= school_year_id_start && school.id <= school_year_id_end && subject.id == subject_id
                && studentMark.student_id == studentCourseSubject.student_id
                && studentMark.subject_id == subject_id
                && couresSubject.teacher_id == teacherID
                select new
                {
                    studentId = studentMark.student_id,
                    mark = studentMark.mark,
                    subjectId = subject.id,
                    couresSubjectID = couresSubject.id,
                    courseSubjectName = couresSubject.display_name,
                    teacherID = couresSubject.teacher_id,
                    teacherName = person.display_name,
                    subjectName = subject.subject_name,
                    numberOfCredit = subject.number_of_credit,
                    semesterID = semester.id,
                    semesterName = semester.semester_name,
                    subjectExamID = studentMark.subject_exam_id,
                    studentSubjectMarkID = studentMark.student_subject_mark_id,
                    year = school.year

                }
                ).ToList().Select(x => new StudentCourseSubject(x.studentId, x.mark, x.subjectId, x.couresSubjectID,
                x.courseSubjectName, x.teacherID,
                x.teacherName, x.subjectName, x.numberOfCredit, x.semesterID, x.semesterName, x.subjectExamID, x.studentSubjectMarkID,
                x.year));


            return list.ToList();
        }
        public List<MarksByEnrollmentClass> getMarksEnrollmentClass(long? subject_id, long? school_year_id_start, long? school_year_id_end, long subject_exam_type_id)
        {
            var listStudent = (from schoolYear in models.tbl_shool_year
                          join enrollmentClass in models.tbl_enrollment_class
                          on schoolYear.year equals enrollmentClass.schoolYear

                          join student in models.tbl_student
                          on enrollmentClass.id equals student.class_id

                          join studentMark in models.tbl_student_mark
                          on student.id equals studentMark.student_id

                          join subject in models.tbl_subject
                          on studentMark.subject_id equals subject.id

                          join subjectExam in models.tbl_subject_exam
                          on studentMark.subject_exam_id equals subjectExam.id

                          where schoolYear.id >= school_year_id_start && schoolYear.id <= school_year_id_end
                          && subject.id == subject_id
                          && subjectExam.subject_exam_type_id == subject_exam_type_id

                          select new
                          {
                                enrollmentClassID = enrollmentClass.id,
                                enrollmentClassName = enrollmentClass.className,
                                subjectID = subject.id,
                                subjectName = subject.subject_name,
                                mark = studentMark.mark
                          }).ToList();
            var result = (from list in listStudent
                          group list by list.enrollmentClassID into listGroup
                          from sublist in listStudent
                          where listGroup.Key == sublist.enrollmentClassID
                          select new
                          {
                              enrollmentClassID = listGroup.Key,
                              enrollmentClassName = sublist.enrollmentClassName,
                              subjectID = sublist.subjectID,
                              subjectName = sublist.subjectName,
                              Tong = listGroup.Count(),
                              A = listGroup.Count(x => x.mark >= 8.45 && x.mark <= 10),
                              B = listGroup.Count(x => x.mark >= 6.95 && x.mark < 8.45),
                              C = listGroup.Count(x => x.mark >= 5.45 && x.mark < 6.95),
                              D = listGroup.Count(x => x.mark >= 3.95 && x.mark < 5.45),
                              F = listGroup.Count(x => x.mark >= 0 && x.mark < 3.95)

                          }
                          ).Distinct().ToList().Select(x => new MarksByEnrollmentClass(0,x.subjectID,x.subjectName,x.enrollmentClassID,x.enrollmentClassName,
                              school_year_id_start,
                              school_year_id_end,
                              x.Tong,
                              x.A,
                              Math.Round((double)x.A * 100 / x.Tong, 2),
                              x.B, 
                              Math.Round((double)x.B * 100 / x.Tong, 2),
                              x.C, 
                              Math.Round((double)x.C * 100 / x.Tong, 2),
                              x.D, 
                              Math.Round((double)x.D * 100 / x.Tong, 2),
                              x.F,
                              Math.Round((double)x.F * 100 / x.Tong, 2)

                          )).ToList();

            int i = 1;
            foreach (MarksByEnrollmentClass item in result)
            {
                item.stt = i++;
            }
            return result;
        }
        
        //Lay diem cua mot lop quan ly theo tung nam
        public List<MarksByEnrollmentClass> getMarksEnrollmentClass(long? subject_id, long? school_year_id_start, long? school_year_id_end,
            long subject_exam_type_id,long? enrollmentClassID)
        {
            var listStudent = (from schoolYear in models.tbl_shool_year
                               join enrollmentClass in models.tbl_enrollment_class
                               on schoolYear.year equals enrollmentClass.schoolYear

                               join student in models.tbl_student
                               on enrollmentClass.id equals student.class_id

                               join studentMark in models.tbl_student_mark
                               on student.id equals studentMark.student_id

                               join subject in models.tbl_subject
                               on studentMark.subject_id equals subject.id

                               join subjectExam in models.tbl_subject_exam
                               on studentMark.subject_exam_id equals subjectExam.id

                               where schoolYear.id >= school_year_id_start && schoolYear.id <= school_year_id_end
                               && subject.id == subject_id
                               && subjectExam.subject_exam_type_id == subject_exam_type_id
                               && enrollmentClass.id == enrollmentClassID
                               select new
                               {
                                   enrollmentClassID = enrollmentClass.id,
                                   enrollmentClassName = enrollmentClass.className,
                                   subjectID = subject.id,
                                   subjectName = subject.subject_name,
                                   schoolYearID = schoolYear.id,
                                   year = schoolYear.year,
                                   mark = studentMark.mark
                               }).ToList();
            var result = (from list in listStudent
                          group list by list.schoolYearID into listGroup
                          from sublist in listStudent
                          where listGroup.Key == sublist.schoolYearID
                          select new
                          {
                              schoolYearID = listGroup.Key,
                              year = sublist.year,
                              enrollmentClassID = sublist.enrollmentClassID,
                              enrollmentClassName = sublist.enrollmentClassName,
                              subjectID = sublist.subjectID,
                              subjectName = sublist.subjectName,
                              Tong = listGroup.Count(),
                              A = listGroup.Count(x => x.mark >= 8.45 && x.mark <= 10),
                              B = listGroup.Count(x => x.mark >= 6.95 && x.mark < 8.45),
                              C = listGroup.Count(x => x.mark >= 5.45 && x.mark < 6.95),
                              D = listGroup.Count(x => x.mark >= 3.95 && x.mark < 5.45),
                              F = listGroup.Count(x => x.mark >= 0 && x.mark < 3.95)

                          }
                          ).Distinct().ToList().Select(x => new MarksByEnrollmentClass(0, x.subjectID, x.subjectName, x.enrollmentClassID, x.enrollmentClassName,
                              x.year,
                              x.Tong,
                              x.A,
                              Math.Round((double)x.A * 100 / x.Tong, 2),
                              x.B,
                              Math.Round((double)x.B * 100 / x.Tong, 2),
                              x.C,
                              Math.Round((double)x.C * 100 / x.Tong, 2),
                              x.D,
                              Math.Round((double)x.D * 100 / x.Tong, 2),
                              x.F,
                              Math.Round((double)x.F * 100 / x.Tong, 2)

                          )).ToList();

            int i = 1;
            foreach (MarksByEnrollmentClass item in result)
            {
                item.stt = i++;
            }
            return result;
        }

        //Lấy điểm tổng kết của một lớp quản lý trong nhiều năm
        public List<MarksByEnrollmentClass> getMarksEnrollmentClass(long? subject_id, long? school_year_id_start, long? school_year_id_end,
            long? enrollmentClassID)
        {
            var listStudent = (from schoolYear in models.tbl_shool_year
                               join enrollmentClass in models.tbl_enrollment_class
                               on schoolYear.year equals enrollmentClass.schoolYear

                               join student in models.tbl_student
                               on enrollmentClass.id equals student.class_id

                               join studentSubjectMark in models.tbl_student_subject_mark
                               on student.id equals studentSubjectMark.student_id

                               join subject in models.tbl_subject
                               on studentSubjectMark.subject_id equals subject.id



                               where schoolYear.id >= school_year_id_start && schoolYear.id <= school_year_id_end
                               && subject.id == subject_id
                               && enrollmentClass.id == enrollmentClassID
                               select new
                               {
                                   enrollmentClassID = enrollmentClass.id,
                                   enrollmentClassName = enrollmentClass.className,
                                   subjectID = subject.id,
                                   subjectName = subject.subject_name,
                                   schoolYearID = schoolYear.id,
                                   year = schoolYear.year,
                                   mark = studentSubjectMark.mark
                               }).ToList();
            var result = (from list in listStudent
                          group list by list.schoolYearID into listGroup
                          from sublist in listStudent
                          where listGroup.Key == sublist.schoolYearID
                          select new
                          {
                              schoolYearID = listGroup.Key,
                              year = sublist.year,
                              enrollmentClassID = sublist.enrollmentClassID,
                              enrollmentClassName = sublist.enrollmentClassName,
                              subjectID = sublist.subjectID,    
                              subjectName = sublist.subjectName,
                              Tong = listGroup.Count(),
                              A = listGroup.Count(x => x.mark >= 8.45 && x.mark <= 10),
                              B = listGroup.Count(x => x.mark >= 6.95 && x.mark < 8.45),
                              C = listGroup.Count(x => x.mark >= 5.45 && x.mark < 6.95),
                              D = listGroup.Count(x => x.mark >= 3.95 && x.mark < 5.45),
                              F = listGroup.Count(x => x.mark >= 0 && x.mark < 3.95)

                          }
                          ).Distinct().ToList().Select(x => new MarksByEnrollmentClass(0, x.subjectID, x.subjectName, x.enrollmentClassID, x.enrollmentClassName,
                              x.year,
                              x.Tong,
                              x.A,
                              Math.Round((double)x.A * 100 / x.Tong, 2),
                              x.B,
                              Math.Round((double)x.B * 100 / x.Tong, 2),
                              x.C,
                              Math.Round((double)x.C * 100 / x.Tong, 2),
                              x.D,
                              Math.Round((double)x.D * 100 / x.Tong, 2),
                              x.F,
                              Math.Round((double)x.F * 100 / x.Tong, 2)

                          )).ToList();
            int i = 1;
            foreach (MarksByEnrollmentClass item in result)
            {
                item.stt = i++;
            }
            return result;
        }
        // Lay diem tong ket cua nhieu lop quan ly 
        public List<MarksByEnrollmentClass> getMarksEnrollmentClass(long? subject_id, long? school_year_id_start, long? school_year_id_end)
        {
            var listStudent = (from schoolYear in models.tbl_shool_year
                               join enrollmentClass in models.tbl_enrollment_class
                               on schoolYear.year equals enrollmentClass.schoolYear

                               join student in models.tbl_student
                               on enrollmentClass.id equals student.class_id

                               join studentSubjectMark in models.tbl_student_subject_mark
                               on student.id equals studentSubjectMark.student_id

                               join subject in models.tbl_subject
                               on studentSubjectMark.subject_id equals subject.id

                              

                               where schoolYear.id >= school_year_id_start && schoolYear.id <= school_year_id_end
                               && subject.id == subject_id
                               select new
                               {
                                   enrollmentClassID = enrollmentClass.id,
                                   enrollmentClassName = enrollmentClass.className,
                                   subjectID = subject.id,
                                   subjectName = subject.subject_name,
                                   mark = studentSubjectMark.mark
                               }).ToList();
            var result = (from list in listStudent
                          group list by list.enrollmentClassID into listGroup
                          from sublist in listStudent
                          where listGroup.Key == sublist.enrollmentClassID
                          select new
                          {
                              enrollmentClassID = listGroup.Key,
                              enrollmentClassName = sublist.enrollmentClassName,
                              subjectID = sublist.subjectID,
                              subjectName = sublist.subjectName,
                              Tong = listGroup.Count(),
                              A = listGroup.Count(x => x.mark >= 8.45 && x.mark <= 10),
                              B = listGroup.Count(x => x.mark >= 6.95 && x.mark < 8.45),
                              C = listGroup.Count(x => x.mark >= 5.45 && x.mark < 6.95),
                              D = listGroup.Count(x => x.mark >= 3.95 && x.mark < 5.45),
                              F = listGroup.Count(x => x.mark >= 0 && x.mark < 3.95)

                          }
                          ).Distinct().ToList().Select(x => new MarksByEnrollmentClass(0,x.subjectID, x.subjectName,x.enrollmentClassID, 
                              x.enrollmentClassName, 
                              school_year_id_start,
                              school_year_id_end,
                              x.Tong,
                              x.A,
                              Math.Round((double)x.A * 100 / x.Tong, 2),
                              x.B,
                              Math.Round((double)x.B * 100 / x.Tong, 2),
                              x.C,
                              Math.Round((double)x.C * 100 / x.Tong, 2),
                              x.D,
                              Math.Round((double)x.D * 100 / x.Tong, 2),
                              x.F,
                              Math.Round((double)x.F * 100 / x.Tong, 2)

                          )).ToList();
            int i = 1;
            foreach (MarksByEnrollmentClass item in result)
            {
                item.stt = i++;
            }
            return result;
        }

        public List<MarksByEnrollmentClass> getMarksEnrollmentClass(long? hocKy, long? khoaHoc, long? dotHoc, long? monHoc, long subject_exam_type_id)
        {
            var listStudent = (from student in models.tbl_student
                               join enrollmentClass in models.tbl_enrollment_class
                               on student.class_id equals enrollmentClass.id

                              join studentMark in models.tbl_student_mark
                              on student.id equals studentMark.student_id

                              join subject in models.tbl_subject
                              on studentMark.subject_id equals subject.id

                              join subjectExam in models.tbl_subject_exam
                              on studentMark.subject_exam_id equals subjectExam.id

                              join semesterSubject in models.tbl_semester_subject
                              on studentMark.semester_subject_id equals semesterSubject.id
                              join semester in models.tbl_semester
                              on semesterSubject.semester_id equals semester.id

                              join srp in models.tbl_semester_register_period
                              on semesterSubject.semester_id equals srp.semeter_id                                                      

                              where
                              semester.id ==hocKy
                              && semesterSubject.course_year_id == khoaHoc
                              && srp.id == dotHoc
                              && studentMark.subject_id ==monHoc
                              && subjectExam.subject_exam_type_id == subject_exam_type_id

                              select new
                              {
                                  enrollmentClassID = enrollmentClass.id,
                                  enrollmentClassName = enrollmentClass.className,
                                  subjectName = subject.subject_name,
                                  mark = studentMark.mark
                              }).ToList();

            var result = (from list in listStudent
                          group list by list.enrollmentClassID into listGroup
                          from sublist in listStudent
                          where listGroup.Key == sublist.enrollmentClassID
                          select new
                          {
                              enrollmentClassID = listGroup.Key,
                              enrollmentClassName = sublist.enrollmentClassName,
                              subjectName = sublist.subjectName,
                              Tong = listGroup.Count(),
                              A = listGroup.Count(x => x.mark >= 8.45 && x.mark <= 10),
                              B = listGroup.Count(x => x.mark >= 6.95 && x.mark < 8.45),
                              C = listGroup.Count(x => x.mark >= 5.45 && x.mark < 6.95),
                              D = listGroup.Count(x => x.mark >= 3.95 && x.mark < 5.45),
                              F = listGroup.Count(x => x.mark >= 0 && x.mark < 3.95)

                          }
                              ).Distinct().ToList().Select(x => new MarksByEnrollmentClass(0, x.subjectName, x.enrollmentClassName, x.Tong,
                                  x.A,
                                  Math.Round((double)x.A * 100 / x.Tong, 2),
                                  x.B,
                                  Math.Round((double)x.B * 100 / x.Tong, 2),
                                  x.C,
                                  Math.Round((double)x.C * 100 / x.Tong, 2),
                                  x.D,
                                  Math.Round((double)x.D * 100 / x.Tong, 2),
                                  x.F,
                                  Math.Round((double)x.F * 100 / x.Tong, 2)

                              )).ToList();

            int i = 1;
            foreach (MarksByEnrollmentClass item in result)
            {
                item.stt = i++;
            }
            return result;


        }
        public List<MarksByEnrollmentClass> getMarksEnrollmentClasses(long? hocKy, long? khoaHoc, long? dotHoc, long? monHoc)
        {
            var listStudent = (from student in models.tbl_student
                               join enrollmentClass in models.tbl_enrollment_class
                               on student.class_id equals enrollmentClass.id

                               join studentMark in models.tbl_student_mark
                               on student.id equals studentMark.student_id

                               join subject in models.tbl_subject
                               on studentMark.subject_id equals subject.id

                               join subjectExam in models.tbl_subject_exam
                               on studentMark.subject_exam_id equals subjectExam.id

                               join semesterSubject in models.tbl_semester_subject
                               on studentMark.semester_subject_id equals semesterSubject.id
                               join semester in models.tbl_semester
                               on semesterSubject.semester_id equals semester.id

                               join srp in models.tbl_semester_register_period
                               on semesterSubject.semester_id equals srp.semeter_id

                               where
                               semester.id == hocKy
                               && semesterSubject.course_year_id == khoaHoc
                               && srp.id == dotHoc
                               && studentMark.subject_id == monHoc

                               select new
                               {
                                   enrollmentClassID = enrollmentClass.id,
                                   enrollmentClassName = enrollmentClass.className,
                                   subjectName = subject.subject_name,
                                   mark = studentMark.mark
                               }).ToList();

            var result = (from list in listStudent
                          group list by list.enrollmentClassID into listGroup
                          from sublist in listStudent
                          where listGroup.Key == sublist.enrollmentClassID
                          select new
                          {
                              enrollmentClassID = listGroup.Key,
                              enrollmentClassName = sublist.enrollmentClassName,
                              subjectName = sublist.subjectName,
                              Tong = listGroup.Count(),
                              A = listGroup.Count(x => x.mark >= 8.45 && x.mark <= 10),
                              B = listGroup.Count(x => x.mark >= 6.95 && x.mark < 8.45),
                              C = listGroup.Count(x => x.mark >= 5.45 && x.mark < 6.95),
                              D = listGroup.Count(x => x.mark >= 3.95 && x.mark < 5.45),
                              F = listGroup.Count(x => x.mark >= 0 && x.mark < 3.95)

                          }
                              ).Distinct().ToList().Select(x => new MarksByEnrollmentClass(0, x.subjectName, x.enrollmentClassName, x.Tong,
                                  x.A,
                                  Math.Round((double)x.A * 100 / x.Tong, 2),
                                  x.B,
                                  Math.Round((double)x.B * 100 / x.Tong, 2),
                                  x.C,
                                  Math.Round((double)x.C * 100 / x.Tong, 2),
                                  x.D,
                                  Math.Round((double)x.D * 100 / x.Tong, 2),
                                  x.F,
                                  Math.Round((double)x.F * 100 / x.Tong, 2)

                              )).ToList();

            int i = 1;
            foreach (MarksByEnrollmentClass item in result)
            {
                item.stt = i++;
            }
            return result;


        }
        public List<MarksByEnrollmentClass> getMarksEnrollmentClass(List<StudentMarkViewModel> studentMarkViewModels)
        {
            var result = (from s in studentMarkViewModels
                          group s by new { s.enrollmentClassID, s.enrollmentClassName } into list

                          select new
                          {
                              departmentID = list.Key.enrollmentClassID,
                              departmentName = list.Key.enrollmentClassName,
                              Tong = list.Count(),
                              A = list.Count(x => x.mark >= 8.45 && x.mark <= 10),
                              B = list.Count(x => x.mark >= 6.95 && x.mark < 8.45),
                              C = list.Count(x => x.mark >= 5.45 && x.mark < 6.95),
                              D = list.Count(x => x.mark >= 3.95 && x.mark < 5.45),
                              F = list.Count(x => x.mark >= 0 && x.mark < 3.95)

                          }).ToList().Select(x => new MarksByEnrollmentClass(0, x.departmentID, x.departmentName, x.Tong,
                              x.A,
                              Math.Round((double)x.A * 100 / x.Tong, 2),
                              x.B,
                              Math.Round((double)x.B * 100 / x.Tong, 2),
                              x.C,
                              Math.Round((double)x.C * 100 / x.Tong, 2),
                              x.D,
                              Math.Round((double)x.D * 100 / x.Tong, 2),
                              x.F,
                              Math.Round((double)x.F * 100 / x.Tong, 2)
                              )).ToList();
            int i = 1;
            foreach (MarksByEnrollmentClass item in result)
            {
                item.stt = i++;
            }
            return result;
        }

        public string getEnrollmentClassName(long? enrollmentClassID)
        {
            return models.tbl_enrollment_class.Where(e => e.id == enrollmentClassID).FirstOrDefault().className;
        }

        //Lấy điểm sinh viên theo nhiều phòng ban trong nhieu kỳ học
        public List<StudentMarkViewModel> getStudentMark(long? subjectID,long? semesterIDStart,long? semesterIDEnd,long? subject_exam_type_id)
        {
            var studentMarkViewModels = (from semeter in models.tbl_semester
                               join semesterSubject in models.tbl_semester_subject
                               on semeter.id equals semesterSubject.semester_id

                               join subject in models.tbl_subject
                               on semesterSubject.subject_id equals subject.id

                               join courseSubject in models.tbl_course_subject
                               on semesterSubject.id equals courseSubject.semester_subject_id

                               join studentCourseSubject in models.tbl_student_course_subject
                               on courseSubject.id equals studentCourseSubject.course_subject_id

                               join student in models.tbl_student
                               on studentCourseSubject.student_id equals student.id

                               join studentMark in models.tbl_student_mark
                               on student.id equals studentMark.student_id

                               join subjectExam in models.tbl_subject_exam
                               on studentMark.subject_exam_id equals subjectExam.id

                               join enrollmentClass in models.tbl_enrollment_class
                               on student.class_id equals enrollmentClass.id

                               join department in models.tbl_department
                               on enrollmentClass.department_id equals department.id

                               join person in models.tbl_person
                               on courseSubject.teacher_id equals person.id into coursePer
                               from p in coursePer.DefaultIfEmpty()

                               where semeter.id >= semesterIDStart && semeter.id <= semesterIDEnd
                               && subject.id == subjectID
                               && studentMark.subject_id == subjectID
                               && subjectExam.subject_exam_type_id == subject_exam_type_id
                               //&& (person.id == courseSubject.teacher_id || courseSubject.teacher_id == null)

                               select new
                               {
                                   studentID = student.id,
                                   studentCode = student.student_code,
                                   mark = studentMark.mark,
                                   courseSubjectID = courseSubject.id,
                                   courseSubjectName = courseSubject.display_name,
                                   teacherID = courseSubject.teacher_id,
                                   teacherName = p == null ? "Không xác định" : p.display_name,
                                   enrollmentClassID = enrollmentClass.id,
                                   enrollmentClassName = enrollmentClass.className,
                                   departmentID = department.id,
                                   departmentName = department.name,
                                   semesterID = semeter.id,
                                   semesterName = semeter.semester_name

                               }).ToList().Select(x => new StudentMarkViewModel(x.studentID, x.studentCode, 
                               x.mark, x.courseSubjectID,x.courseSubjectName, x.teacherID,x.teacherName,
                               x.enrollmentClassID, x.enrollmentClassName, x.departmentID, 
                               x.departmentName,x.semesterID,x.semesterName

                               )).ToList();

            return studentMarkViewModels;


            //var result = (from list in listStudent
            //              group list by list.departmentID into listGroup
            //              from sublist in listStudent
            //              where listGroup.Key == sublist.departmentID
            //              select new
            //              {
            //                  departmentID = listGroup.Key,
            //                  departmentName = sublist.departmentName,
            //                  subjectID = sublist.subjectID,
            //                  subjectName = sublist.subjectName,
            //                  Tong = listGroup.Count(),
            //                  A = listGroup.Count(x => x.mark >= 8.45 && x.mark <= 10),
            //                  B = listGroup.Count(x => x.mark >= 6.95 && x.mark < 8.45),
            //                  C = listGroup.Count(x => x.mark >= 5.45 && x.mark < 6.95),
            //                  D = listGroup.Count(x => x.mark >= 3.95 && x.mark < 5.45),
            //                  F = listGroup.Count(x => x.mark >= 0 && x.mark < 3.95)

            //              }
            //              ).Distinct().ToList().Select(x => new MarkByDepartment(0, x.subjectID, x.subjectName, x.departmentID, x.departmentName,
            //                  startYear,
            //                  endYear,
            //                  x.Tong,
            //                  x.A,
            //                  Math.Round((double)x.A * 100 / x.Tong, 2),
            //                  x.B,
            //                  Math.Round((double)x.B * 100 / x.Tong, 2),
            //                  x.C,
            //                  Math.Round((double)x.C * 100 / x.Tong, 2),
            //                  x.D,
            //                  Math.Round((double)x.D * 100 / x.Tong, 2),
            //                  x.F,
            //                  Math.Round((double)x.F * 100 / x.Tong, 2)

            //              )).ToList();

            //int i = 1;
            //foreach (MarkByDepartment item in result)
            //{
            //    item.stt = i++;
            //}
            //return result;
        }

        //Lấy điểm tổng kết của từng học sinh 
        public List<StudentMarkViewModel> getStudentMark(long? subjectID, long? semesterIDStart, long? semesterIDEnd)
        {
            var studentMarkViewModels = (from semeter in models.tbl_semester
                                         join semesterSubject in models.tbl_semester_subject
                                         on semeter.id equals semesterSubject.semester_id

                                         join subject in models.tbl_subject
                                         on semesterSubject.subject_id equals subject.id

                                         join courseSubject in models.tbl_course_subject
                                         on semesterSubject.id equals courseSubject.semester_subject_id

                                         join studentCourseSubject in models.tbl_student_course_subject
                                         on courseSubject.id equals studentCourseSubject.course_subject_id

                                         join student in models.tbl_student
                                         on studentCourseSubject.student_id equals student.id

                                         join studentSubjectMark in models.tbl_student_subject_mark
                                         on student.id equals studentSubjectMark.student_id

                                         join person in models.tbl_person
                                         on courseSubject.teacher_id equals person.id into coursePer
                                         from p in coursePer.DefaultIfEmpty()

                                         join enrollmentClass in models.tbl_enrollment_class
                                         on student.class_id equals enrollmentClass.id

                                         join department in models.tbl_department
                                         on enrollmentClass.department_id equals department.id

                                         where semeter.id >= semesterIDStart && semeter.id <= semesterIDEnd
                                         && subject.id == subjectID
                                         && studentSubjectMark.subject_id == subjectID
                                         && studentSubjectMark.semester_id == semeter.id

                                         select new
                                         {
                                             studentID = student.id,
                                             studentCode = student.student_code,
                                             mark = studentSubjectMark.mark,
                                             courseSubjectID = courseSubject.id,
                                             courseSubjectName = courseSubject.display_name,
                                             teacherID = courseSubject.teacher_id,
                                             teacherName = p == null ? "Không xác định" : p.display_name,
                                             enrollmentClassID = enrollmentClass.id,
                                             enrollmentClassName = enrollmentClass.className,
                                             departmentID = department.id,
                                             departmentName = department.name,
                                             semesterID = semeter.id,
                                             semesterName = semeter.semester_name

                                         }).ToList().Select(x => new StudentMarkViewModel(x.studentID,
                                            x.studentCode,
                                           x.mark, x.courseSubjectID, x.courseSubjectName, x.teacherID,
                                           x.teacherName,
                                           x.enrollmentClassID, x.enrollmentClassName, x.departmentID,
                                           x.departmentName, x.semesterID, x.semesterName

                                           )).ToList();

            return studentMarkViewModels;
        }
        public List<StudentMarkViewModel> getStudentMarkExcelDepartment(long? subjectID,long? semesterIDStart,long? semesterIDEnd, long? subject_exam_type_id, List<InputExport> iDCheckeds)
        {
            
            var studentMarkViewModels = (from semeter in models.tbl_semester
                                         join semesterSubject in models.tbl_semester_subject
                                         on semeter.id equals semesterSubject.semester_id

                                         join subject in models.tbl_subject
                                         on semesterSubject.subject_id equals subject.id

                                         join courseSubject in models.tbl_course_subject
                                         on semesterSubject.id equals courseSubject.semester_subject_id

                                         join studentCourseSubject in models.tbl_student_course_subject
                                         on courseSubject.id equals studentCourseSubject.course_subject_id

                                         join student in models.tbl_student
                                         on studentCourseSubject.student_id equals student.id

                                         join studentMark in models.tbl_student_mark
                                         on student.id equals studentMark.student_id

                                         join person in models.tbl_person
                                         on student.id equals person.id

                                         join subjectExam in models.tbl_subject_exam
                                         on studentMark.subject_exam_id equals subjectExam.id

                                         join enrollmentClass in models.tbl_enrollment_class
                                         on student.class_id equals enrollmentClass.id

                                         join department in models.tbl_department
                                         on enrollmentClass.department_id equals department.id

                                         where semeter.id >= semesterIDStart && semeter.id <= semesterIDEnd
                                         && subject.id == subjectID
                                         && studentMark.subject_id == subjectID
                                         && subjectExam.subject_exam_type_id == subject_exam_type_id

                                         select new
                                         {
                                             studentID = student.id,
                                             studentCode = student.student_code,
                                             mark = studentMark.mark,
                                             courseSubjectID = courseSubject.id,
                                             courseSubjectName = courseSubject.display_name,
                                             teacherID = courseSubject.teacher_id,
                                             studentName = person.display_name,
                                             enrollmentClassID = enrollmentClass.id,
                                             enrollmentClassName = enrollmentClass.className,
                                             departmentID = department.id,
                                             departmentName = department.name,
                                             semesterID = semeter.id,
                                             semesterName = semeter.semester_name

                                         }).ToList();
            var result = (from s in studentMarkViewModels
                          join i in iDCheckeds
                         on s.departmentID equals i.departmentId
                         //join person in models.tbl_person
                         //on s.studentID equals person.id
                         where s.enrollmentClassID == i.enrollmentClassId
                         && s.semesterID == i.semesterId
                         select new
                         {
                             studentID = s.studentID,
                             studentCode = s.studentCode,
                             mark = s.mark,
                             courseSubjectID = s.courseSubjectID,
                             courseSubjectName = s.courseSubjectName,
                             teacherID = s.teacherID,
                             studentName = s.studentName,
                             enrollmentClassID = s.enrollmentClassID,
                             enrollmentClassName = s.enrollmentClassName,
                             departmentID = s.departmentID,
                             departmentName = s.departmentName,
                             semesterID = s.semesterID,
                             semesterName = s.semesterName
                         }).ToList().Select(x => new StudentMarkViewModel(x.studentID,x.studentName,x.studentCode,
                                         x.mark, x.courseSubjectID, x.courseSubjectName, x.teacherID,
                                         x.enrollmentClassID, x.enrollmentClassName, x.departmentID,
                                         x.departmentName, x.semesterID, x.semesterName)).ToList();

            return result;
        }
        public List<StudentMarkViewModel> getStudentMarkExcelDepartment(long? subjectID, long? semesterIDStart, 
            long? semesterIDEnd, List<InputExport> iDCheckeds)
        {
            var studentMarkViewModels = (from semeter in models.tbl_semester
                                         join semesterSubject in models.tbl_semester_subject
                                         on semeter.id equals semesterSubject.semester_id

                                         join subject in models.tbl_subject
                                         on semesterSubject.subject_id equals subject.id

                                         join courseSubject in models.tbl_course_subject
                                         on semesterSubject.id equals courseSubject.semester_subject_id

                                         join studentCourseSubject in models.tbl_student_course_subject
                                         on courseSubject.id equals studentCourseSubject.course_subject_id

                                         join student in models.tbl_student
                                         on studentCourseSubject.student_id equals student.id

                                         join studentSubjectMark in models.tbl_student_subject_mark
                                         on student.id equals studentSubjectMark.student_id

                                         join person in models.tbl_person
                                         on student.id equals person.id

                                         join enrollmentClass in models.tbl_enrollment_class
                                         on student.class_id equals enrollmentClass.id

                                         join department in models.tbl_department
                                         on enrollmentClass.department_id equals department.id

                                         where semeter.id >= semesterIDStart && semeter.id <= semesterIDEnd
                                         && subject.id == subjectID
                                         && studentSubjectMark.subject_id == subjectID
                                         && studentSubjectMark.semester_id == semeter.id

                                         select new
                                         {
                                             studentID = student.id,
                                             studentName = person.display_name,
                                             studentCode = student.student_code,
                                             mark = studentSubjectMark.mark,
                                             courseSubjectID = courseSubject.id,
                                             courseSubjectName = courseSubject.display_name,
                                             teacherID = courseSubject.teacher_id,
                                             enrollmentClassID = enrollmentClass.id,
                                             enrollmentClassName = enrollmentClass.className,
                                             departmentID = department.id,
                                             departmentName = department.name,
                                             semesterID = semeter.id,
                                             semesterName = semeter.semester_name

                                         }).ToList();

            var result = (from s in studentMarkViewModels
                          join i in iDCheckeds
                         on s.departmentID equals i.departmentId
                          where s.enrollmentClassID == i.enrollmentClassId
                          && s.semesterID == i.semesterId
                          select new
                          {
                              studentID = s.studentID,
                              studentName = s.studentName,
                              studentCode = s.studentCode,
                              mark = s.mark,
                              courseSubjectID = s.courseSubjectID,
                              courseSubjectName = s.courseSubjectName,
                              teacherID = s.teacherID,
                              enrollmentClassID = s.enrollmentClassID,
                              enrollmentClassName = s.enrollmentClassName,
                              departmentID = s.departmentID,
                              departmentName = s.departmentName,
                              semesterID = s.semesterID,
                              semesterName = s.semesterName
                          }).ToList().Select(x => new StudentMarkViewModel(x.studentID,x.studentName, x.studentCode,
                                          x.mark, x.courseSubjectID, x.courseSubjectName, x.teacherID,
                                          x.enrollmentClassID, x.enrollmentClassName, x.departmentID,
                                          x.departmentName, x.semesterID, x.semesterName)).ToList();

            return result;
        }
        public List<StudentMarkViewModel> getStudentMarkExcelTeacher(long? subjectID, long? semesterIDStart,
            long? semesterIDEnd, long? subject_exam_type_id, List<InputExport> iDCheckeds)
        {
            var studentMarkViewModels = (from semeter in models.tbl_semester
                                         join semesterSubject in models.tbl_semester_subject
                                         on semeter.id equals semesterSubject.semester_id

                                         join subject in models.tbl_subject
                                         on semesterSubject.subject_id equals subject.id

                                         join courseSubject in models.tbl_course_subject
                                         on semesterSubject.id equals courseSubject.semester_subject_id

                                         join studentCourseSubject in models.tbl_student_course_subject
                                         on courseSubject.id equals studentCourseSubject.course_subject_id

                                         join student in models.tbl_student
                                         on studentCourseSubject.student_id equals student.id

                                         join studentMark in models.tbl_student_mark
                                         on student.id equals studentMark.student_id

                                         join person in models.tbl_person
                                         on student.id equals person.id into studentPer
                                         from ps in studentPer.DefaultIfEmpty()
                                         

                                         join personTeacher in models.tbl_person 
                                         on courseSubject.teacher_id equals personTeacher.id into coursePer

                                         from p in coursePer.DefaultIfEmpty()

                                         join subjectExam in models.tbl_subject_exam
                                         on studentMark.subject_exam_id equals subjectExam.id

                                         join enrollmentClass in models.tbl_enrollment_class
                                         on student.class_id equals enrollmentClass.id

                                         join department in models.tbl_department
                                         on enrollmentClass.department_id equals department.id

                                         where semeter.id >= semesterIDStart && semeter.id <= semesterIDEnd
                                         && subject.id == subjectID
                                         && studentMark.subject_id == subjectID
                                         && subjectExam.subject_exam_type_id == subject_exam_type_id

                                         select new
                                         {
                                             studentID = student.id,
                                             studentName = ps == null ? "Không xác định" : ps.display_name,
                                             studentCode = student.student_code,
                                             mark = studentMark.mark,
                                             courseSubjectID = courseSubject.id,
                                             courseSubjectName = courseSubject.display_name,
                                             teacherID = courseSubject.teacher_id,
                                             teacherName = p == null ? "Không xác định" : p.display_name,
                                             enrollmentClassID = enrollmentClass.id,
                                             enrollmentClassName = enrollmentClass.className,
                                             departmentID = department.id,
                                             departmentName = department.name,
                                             semesterID = semeter.id,
                                             semesterName = semeter.semester_name

                                         }).ToList();

            var result = (from i in iDCheckeds
                          join s in studentMarkViewModels
                          on i.teacherId equals s.teacherID

                          where s.semesterID == i.semesterId
                          && s.courseSubjectID == i.courseSubjectId

                          select new
                          {
                              studentID = s.studentID,
                              studentName = s.studentName,
                              studentCode = s.studentCode,
                              mark = s.mark,
                              courseSubjectID = s.courseSubjectID,
                              courseSubjectName = s.courseSubjectName,
                              teacherID = s.teacherID,
                              teacherName = s.teacherName,
                              enrollmentClassID = s.enrollmentClassID,
                              enrollmentClassName = s.enrollmentClassName,
                              departmentID = s.departmentID,
                              departmentName = s.departmentName,
                              semesterID = s.semesterID,
                              semesterName = s.semesterName
                          }
                          ).ToList().Select(x => new StudentMarkViewModel(x.studentID, x.studentName,
                                         x.studentCode, x.mark, x.courseSubjectID,
                                         x.courseSubjectName, x.teacherID, x.teacherName, x.enrollmentClassID,
                                         x.enrollmentClassName,
                                         x.semesterID,x.semesterName

                                         )).ToList();

            return result;
        }
        public List<StudentMarkViewModel> getStudentMarkExcelTeacher(long? subjectID, long? semesterIDStart,
            long? semesterIDEnd, List<InputExport> iDCheckeds)
        {
            var studentMarkViewModels = (from semeter in models.tbl_semester
                                         join semesterSubject in models.tbl_semester_subject
                                         on semeter.id equals semesterSubject.semester_id

                                         join subject in models.tbl_subject
                                         on semesterSubject.subject_id equals subject.id

                                         join courseSubject in models.tbl_course_subject
                                         on semesterSubject.id equals courseSubject.semester_subject_id

                                         join studentCourseSubject in models.tbl_student_course_subject
                                         on courseSubject.id equals studentCourseSubject.course_subject_id

                                         join student in models.tbl_student
                                         on studentCourseSubject.student_id equals student.id

                                         join studentSubjectMark in models.tbl_student_subject_mark
                                         on student.id equals studentSubjectMark.student_id

                                         join person in models.tbl_person
                                         on student.id equals person.id

                                         join personTeacher in models.tbl_person
                                         on courseSubject.teacher_id equals personTeacher.id into CoursePer
                                         from p in CoursePer.DefaultIfEmpty()

                                         join enrollmentClass in models.tbl_enrollment_class
                                         on student.class_id equals enrollmentClass.id

                                         join department in models.tbl_department
                                         on enrollmentClass.department_id equals department.id

                                         where semeter.id >= semesterIDStart && semeter.id <= semesterIDEnd
                                         && subject.id == subjectID
                                         && studentSubjectMark.subject_id == subjectID
                                         && studentSubjectMark.semester_id == semeter.id

                                         select new
                                         {
                                             studentID = student.id,
                                             studentName = person.display_name,
                                             studentCode = student.student_code,
                                             mark = studentSubjectMark.mark,
                                             courseSubjectID = courseSubject.id,
                                             courseSubjectName = courseSubject.display_name,
                                             teacherID = courseSubject.teacher_id,
                                             teacherName = p == null ? "Không xác định" : p.display_name,
                                             enrollmentClassID = enrollmentClass.id,
                                             enrollmentClassName = enrollmentClass.className,
                                             departmentID = department.id,
                                             departmentName = department.name,
                                             semesterID = semeter.id,
                                             semesterName = semeter.semester_name

                                         }).ToList();

            var result = (from i in iDCheckeds
                          join s in studentMarkViewModels
                          on i.teacherId equals s.teacherID

                          where s.semesterID == i.semesterId
                          && s.courseSubjectID == i.courseSubjectId

                          select new
                          {
                              studentID = s.studentID,
                              studentName = s.studentName,
                              studentCode = s.studentCode,
                              mark = s.mark,
                              courseSubjectID = s.courseSubjectID,
                              courseSubjectName = s.courseSubjectName,
                              teacherID = s.teacherID,
                              teacherName = s.teacherName,
                              enrollmentClassID = s.enrollmentClassID,
                              enrollmentClassName = s.enrollmentClassName,
                              departmentID = s.departmentID,
                              departmentName = s.departmentName,
                              semesterID = s.semesterID,
                              semesterName = s.semesterName
                          }
                          ).ToList().Select(x => new StudentMarkViewModel(x.studentID, x.studentName,
                                         x.studentCode, x.mark, x.courseSubjectID,
                                         x.courseSubjectName, x.teacherID, x.teacherName, x.enrollmentClassID,
                                         x.enrollmentClassName,
                                         x.semesterID, x.semesterName

                                         )).ToList();

            return result;
        }
        //public List<StudentMarkViewModel> getStudentMarkExcelEnrollmentClass(long? subjectID, long? semesterIDStart,
        //    long? semesterIDEnd, long? subject_exam_type_id, List<long?> iDCheckeds)
        //{
        //    var studentMarkViewModels = (from semeter in models.tbl_semester
        //                                 join semesterSubject in models.tbl_semester_subject
        //                                 on semeter.id equals semesterSubject.semester_id

        //                                 join subject in models.tbl_subject
        //                                 on semesterSubject.subject_id equals subject.id

        //                                 join courseSubject in models.tbl_course_subject
        //                                 on semesterSubject.id equals courseSubject.semester_subject_id

        //                                 join studentCourseSubject in models.tbl_student_course_subject
        //                                 on courseSubject.id equals studentCourseSubject.course_subject_id

        //                                 join student in models.tbl_student
        //                                 on studentCourseSubject.student_id equals student.id

        //                                 join studentMark in models.tbl_student_mark
        //                                 on student.id equals studentMark.student_id

        //                                 join person in models.tbl_person
        //                                 on student.id equals person.id

        //                                 join subjectExam in models.tbl_subject_exam
        //                                 on studentMark.subject_exam_id equals subjectExam.id

        //                                 join enrollmentClass in models.tbl_enrollment_class
        //                                 on student.class_id equals enrollmentClass.id

        //                                 join department in models.tbl_department
        //                                 on enrollmentClass.department_id equals department.id

        //                                 join long numcheckedList in iDCheckeds
        //                                 on enrollmentClass.id equals numcheckedList

        //                                 where semeter.id >= semesterIDStart && semeter.id <= semesterIDEnd
        //                                 && subject.id == subjectID
        //                                 && studentMark.subject_id == subjectID
        //                                 && subjectExam.subject_exam_type_id == subject_exam_type_id

        //                                 select new
        //                                 {
        //                                     studentID = student.id,
        //                                     studentName = person.display_name,
        //                                     studentCode = student.student_code,
        //                                     mark = studentMark.mark,
        //                                     courseSubjectID = courseSubject.id,
        //                                     courseSubjectName = courseSubject.display_name,
        //                                     teacherID = courseSubject.teacher_id,
        //                                     enrollmentClassID = enrollmentClass.id,
        //                                     enrollmentClassName = enrollmentClass.className,
        //                                     departmentID = department.id,
        //                                     departmentName = department.name

        //                                 }).ToList().Select(x => new StudentMarkViewModel(x.studentID, x.studentName,
        //                                 x.studentCode, x.mark, x.courseSubjectID,
        //                                 x.courseSubjectName, x.teacherID, x.enrollmentClassID,
        //                                 x.enrollmentClassName, x.departmentID, x.departmentName

        //                                 )).ToList();

        //    return studentMarkViewModels;
        //}
        //public List<StudentMarkViewModel> getStudentMarkExcelEnrollmentClass(long? subjectID, long? semesterIDStart,
        //    long? semesterIDEnd, List<long?> iDCheckeds)
        //{
        //    var studentMarkViewModels = (from semeter in models.tbl_semester
        //                                 join semesterSubject in models.tbl_semester_subject
        //                                 on semeter.id equals semesterSubject.semester_id

        //                                 join subject in models.tbl_subject
        //                                 on semesterSubject.subject_id equals subject.id

        //                                 join courseSubject in models.tbl_course_subject
        //                                 on semesterSubject.id equals courseSubject.semester_subject_id

        //                                 join studentCourseSubject in models.tbl_student_course_subject
        //                                 on courseSubject.id equals studentCourseSubject.course_subject_id

        //                                 join student in models.tbl_student
        //                                 on studentCourseSubject.student_id equals student.id

        //                                 join studentSubjectMark in models.tbl_student_subject_mark
        //                                 on student.id equals studentSubjectMark.student_id

        //                                 join person in models.tbl_person
        //                                 on student.id equals person.id


        //                                 join enrollmentClass in models.tbl_enrollment_class
        //                                 on student.class_id equals enrollmentClass.id

        //                                 join department in models.tbl_department
        //                                 on enrollmentClass.department_id equals department.id

        //                                 join long numcheckedList in iDCheckeds
        //                                 on enrollmentClass.id equals numcheckedList

        //                                 where semeter.id >= semesterIDStart && semeter.id <= semesterIDEnd
        //                                 && subject.id == subjectID
        //                                 && studentSubjectMark.subject_id == subjectID
        //                                 && studentSubjectMark.semester_id == semeter.id

        //                                 select new
        //                                 {
        //                                     studentID = student.id,
        //                                     studentName = person.display_name,
        //                                     studentCode = student.student_code,
        //                                     mark = studentSubjectMark.mark,
        //                                     courseSubjectID = courseSubject.id,
        //                                     courseSubjectName = courseSubject.display_name,
        //                                     teacherID = courseSubject.teacher_id,
        //                                     enrollmentClassID = enrollmentClass.id,
        //                                     enrollmentClassName = enrollmentClass.className,
        //                                     departmentID = department.id,
        //                                     departmentName = department.name

        //                                 }).ToList().Select(x => new StudentMarkViewModel(x.studentID, x.studentName,
        //                                 x.studentCode, x.mark, x.courseSubjectID,
        //                                 x.courseSubjectName, x.teacherID,
        //                                 x.enrollmentClassID, x.enrollmentClassName, x.departmentID, x.departmentName

        //                                 )).ToList();

        //    return studentMarkViewModels;
        //}

        ////Lấy điểm sinh viên theo một phòng ban
        //public List<MarkByDepartment> getMarksByDepartMent(long? subjectID, long? startYear, long? endYear, long? subject_exam_type_id,long? departmentID)
        //{
            
        //    var listStudent = (from schoolYear in models.tbl_shool_year
        //                        join enrollmentClass in models.tbl_enrollment_class
        //                        on schoolYear.year equals enrollmentClass.schoolYear

        //                        join student in models.tbl_student
        //                        on enrollmentClass.id equals student.class_id

        //                        join studentMark in models.tbl_student_mark
        //                        on student.id equals studentMark.student_id

        //                        join subject in models.tbl_subject
        //                        on studentMark.subject_id equals subject.id

        //                        join subjectExam in models.tbl_subject_exam
        //                        on studentMark.subject_exam_id equals subjectExam.id

        //                        join department in models.tbl_department
        //                        on enrollmentClass.department_id equals department.id

        //                        where schoolYear.id >= startYear && schoolYear.id <= endYear
        //                        && subject.id == subjectID
        //                        && subjectExam.subject_exam_type_id == subject_exam_type_id
        //                        && department.id == departmentID
        //                        select new
        //                        {
        //                            enrollmentClassID = enrollmentClass.id,
        //                            enrollmentClassName = enrollmentClass.className,
        //                            departmentID = department.id,
        //                            departmentName = department.name,
        //                            subjectID = subject.id,
        //                            subjectName = subject.subject_name,
        //                            year = schoolYear.year,
        //                            mark = studentMark.mark
        //                        }).ToList();
            
        //    var result = (from list in listStudent
        //                  group list by list.year into listGroup
        //                  from sublist in listStudent
        //                  where listGroup.Key == sublist.year
        //                  select new
        //                  {
        //                      departmentID = sublist.departmentID,
        //                      departmentName = sublist.departmentName,
        //                      subjectID = sublist.subjectID,
        //                      subjectName = sublist.subjectName,
        //                      year = listGroup.Key,
        //                      Tong = listGroup.Count(),
        //                      A = listGroup.Count(x => x.mark >= 8.45 && x.mark <= 10),
        //                      B = listGroup.Count(x => x.mark >= 6.95 && x.mark < 8.45),
        //                      C = listGroup.Count(x => x.mark >= 5.45 && x.mark < 6.95),
        //                      D = listGroup.Count(x => x.mark >= 3.95 && x.mark < 5.45),
        //                      F = listGroup.Count(x => x.mark >= 0 && x.mark < 3.95)

        //                  }
        //                  ).Distinct().ToList().Select(x => new MarkByDepartment(0, x.subjectID, x.subjectName, x.departmentID, x.departmentName,
        //                      x.year,
        //                      x.Tong,
        //                      x.A,
        //                      Math.Round((double)x.A * 100 / x.Tong, 2),
        //                      x.B,
        //                      Math.Round((double)x.B * 100 / x.Tong, 2),
        //                      x.C,
        //                      Math.Round((double)x.C * 100 / x.Tong, 2),
        //                      x.D,
        //                      Math.Round((double)x.D * 100 / x.Tong, 2),
        //                      x.F,
        //                      Math.Round((double)x.F * 100 / x.Tong, 2)

        //                  )).ToList();

        //    int i = 1;
        //    foreach (MarkByDepartment item in result)
        //    {
        //        item.stt = i++;
        //    }
        //    return result;
        //}
        //public List<MarkByDepartment> getStudentMarksByDepartMent(long? subjectID, long? startYear, long? endYear, long? departmentID)
        //{

        //    var listStudent = (from schoolYear in models.tbl_shool_year
        //                       join enrollmentClass in models.tbl_enrollment_class
        //                       on schoolYear.year equals enrollmentClass.schoolYear

        //                       join student in models.tbl_student
        //                       on enrollmentClass.id equals student.class_id

        //                       join studentSubjectMark in models.tbl_student_subject_mark
        //                       on student.id equals studentSubjectMark.student_id

        //                       join subject in models.tbl_subject
        //                       on studentSubjectMark.subject_id equals subject.id

        //                       join department in models.tbl_department
        //                       on enrollmentClass.department_id equals department.id

        //                       where schoolYear.id >= startYear && schoolYear.id <= endYear
        //                       && subject.id == subjectID
        //                       && department.id == departmentID
        //                       select new
        //                       {
        //                           enrollmentClassID = enrollmentClass.id,
        //                           enrollmentClassName = enrollmentClass.className,
        //                           departmentID = department.id,
        //                           departmentName = department.name,
        //                           subjectID = subject.id,
        //                           subjectName = subject.subject_name,
        //                           mark = studentSubjectMark.mark,
        //                           year = schoolYear.year
        //                       }).ToList();

        //    var result = (from list in listStudent
        //                  group list by list.year into listGroup
        //                  from sublist in listStudent
        //                  where listGroup.Key == sublist.year
        //                  select new
        //                  {
        //                      departmentID = sublist.departmentID,
        //                      departmentName = sublist.departmentName,
        //                      subjectID = sublist.subjectID,
        //                      subjectName = sublist.subjectName,
        //                      year = listGroup.Key,
        //                      Tong = listGroup.Count(),
        //                      A = listGroup.Count(x => x.mark >= 8.45 && x.mark <= 10),
        //                      B = listGroup.Count(x => x.mark >= 6.95 && x.mark < 8.45),
        //                      C = listGroup.Count(x => x.mark >= 5.45 && x.mark < 6.95),
        //                      D = listGroup.Count(x => x.mark >= 3.95 && x.mark < 5.45),
        //                      F = listGroup.Count(x => x.mark >= 0 && x.mark < 3.95)

        //                  }
        //                  ).Distinct().ToList().Select(x => new MarkByDepartment(0, x.subjectID, x.subjectName, x.departmentID, x.departmentName,
        //                      x.year,
        //                      x.Tong,
        //                      x.A,
        //                      Math.Round((double)x.A * 100 / x.Tong, 2),
        //                      x.B,
        //                      Math.Round((double)x.B * 100 / x.Tong, 2),
        //                      x.C,
        //                      Math.Round((double)x.C * 100 / x.Tong, 2),
        //                      x.D,
        //                      Math.Round((double)x.D * 100 / x.Tong, 2),
        //                      x.F,
        //                      Math.Round((double)x.F * 100 / x.Tong, 2)

        //                  )).ToList();

        //    int i = 1;
        //    foreach (MarkByDepartment item in result)
        //    {
        //        item.stt = i++;
        //    }
        //    return result;
        //}
        //// Lấy điểm tổng kết của sinh viên theo phòng ban
        //public List<MarkByDepartment> getMarksByDepartMent(long? subject_id, long? school_year_id_start, long? school_year_id_end)
        //{
        //    var listStudent = (from schoolYear in models.tbl_shool_year
        //                       join enrollmentClass in models.tbl_enrollment_class
        //                       on schoolYear.year equals enrollmentClass.schoolYear

        //                       join student in models.tbl_student
        //                       on enrollmentClass.id equals student.class_id

        //                       join studentSubjectMark in models.tbl_student_subject_mark
        //                       on student.id equals studentSubjectMark.student_id

        //                       join subject in models.tbl_subject
        //                       on studentSubjectMark.subject_id equals subject.id

        //                       join department in models.tbl_department
        //                       on enrollmentClass.department_id equals department.id

        //                       where schoolYear.id >= school_year_id_start && schoolYear.id <= school_year_id_end
        //                       && subject.id == subject_id
        //                       && department.level == 1
        //                       && (department.department_type == 2 || department.department_type == 0)
        //                       select new
        //                       {
        //                           enrollmentClassID = enrollmentClass.id,
        //                           enrollmentClassName = enrollmentClass.className,
        //                           departmentID = department.id,
        //                           departmentName = department.name,
        //                           subjectID = subject.id,
        //                           subjectName = subject.subject_name,
        //                           mark = studentSubjectMark.mark
        //                       }).ToList();
        //    var result = (from list in listStudent
        //                  group list by list.departmentID into listGroup
        //                  from sublist in listStudent
        //                  where listGroup.Key == sublist.departmentID
        //                  select new
        //                  {
        //                      departmentID = listGroup.Key,
        //                      departmentName = sublist.departmentName,
        //                      subjectID = sublist.subjectID,
        //                      subjectName = sublist.subjectName,
        //                      Tong = listGroup.Count(),
        //                      A = listGroup.Count(x => x.mark >= 8.45 && x.mark <= 10),
        //                      B = listGroup.Count(x => x.mark >= 6.95 && x.mark < 8.45),
        //                      C = listGroup.Count(x => x.mark >= 5.45 && x.mark < 6.95),
        //                      D = listGroup.Count(x => x.mark >= 3.95 && x.mark < 5.45),
        //                      F = listGroup.Count(x => x.mark >= 0 && x.mark < 3.95)

        //                  }
        //                  ).Distinct().ToList().Select(x => new MarkByDepartment(0, x.subjectID, x.subjectName, x.departmentID,
        //                      x.departmentName,
        //                      school_year_id_start,
        //                      school_year_id_end,
        //                      x.Tong,
        //                      x.A,
        //                      Math.Round((double)x.A * 100 / x.Tong, 2),
        //                      x.B,
        //                      Math.Round((double)x.B * 100 / x.Tong, 2),
        //                      x.C,
        //                      Math.Round((double)x.C * 100 / x.Tong, 2),
        //                      x.D,
        //                      Math.Round((double)x.D * 100 / x.Tong, 2),
        //                      x.F,
        //                      Math.Round((double)x.F * 100 / x.Tong, 2)

        //                  )).ToList();
        //    int i = 1;
        //    foreach (MarkByDepartment item in result)
        //    {
        //        item.stt = i++;
        //    }
        //    return result;
        //}

        public List<MarkByDepartment> getMarkByDepartment(List<StudentMarkViewModel> studentMarkViewModels)
        {
            var result = (from s in studentMarkViewModels
                          group s by new { s.departmentID, s.departmentName,
                          s.semesterId,s.semesterName,s.enrollmentClassID,s.enrollmentClassName} into list
                          orderby list.Key.departmentID
                          select new
                          {
                              departmentID = list.Key.departmentID,
                              departmentName = list.Key.departmentName,
                              semesterId = list.Key.semesterId,
                              semtesterName = list.Key.semesterName,
                              enrollmentClassID = list.Key.enrollmentClassID,
                              enrollmentClassName = list.Key.enrollmentClassName,
                              Tong = list.Count(),
                              A = list.Count(x => x.mark >= 8.45 && x.mark <= 10),
                              B = list.Count(x => x.mark >= 6.95 && x.mark < 8.45),
                              C = list.Count(x => x.mark >= 5.45 && x.mark < 6.95),
                              D = list.Count(x => x.mark >= 3.95 && x.mark < 5.45),
                              F = list.Count(x => x.mark >= 0 && x.mark < 3.95)

                          }).ToList().Select(x => new MarkByDepartment(0,x.departmentID,x.departmentName,
                                x.semesterId,x.semtesterName,x.enrollmentClassID,x.enrollmentClassName,
                              x.Tong,
                              x.A,
                              Math.Round((double)x.A * 100 / x.Tong, 2),
                              x.B,
                              Math.Round((double)x.B * 100 / x.Tong, 2),
                              x.C,
                              Math.Round((double)x.C * 100 / x.Tong, 2),
                              x.D,
                              Math.Round((double)x.D * 100 / x.Tong, 2),
                              x.F,
                              Math.Round((double)x.F * 100 / x.Tong, 2)
                              )).ToList();
            int i = 1;
            foreach (MarkByDepartment item in result)
            {
                item.stt = i++;
            }
            return result;
        }
        public List<MarkByDepartment> getDataChartByDepartment(List<StudentMarkViewModel> studentMarkViewModels)
        {
            var result = (from s in studentMarkViewModels
                          group s by new
                          {
                              s.departmentID,
                              s.departmentName,
                          } into list
                          orderby list.Key.departmentID
                          select new
                          {
                              departmentID = list.Key.departmentID,
                              departmentName = list.Key.departmentName,
                              Tong = list.Count(),
                              A = list.Count(x => x.mark >= 8.45 && x.mark <= 10),
                              B = list.Count(x => x.mark >= 6.95 && x.mark < 8.45),
                              C = list.Count(x => x.mark >= 5.45 && x.mark < 6.95),
                              D = list.Count(x => x.mark >= 3.95 && x.mark < 5.45),
                              F = list.Count(x => x.mark >= 0 && x.mark < 3.95)

                          }).ToList().Select(x => new MarkByDepartment(0, x.departmentID, x.departmentName,
                              x.Tong,
                              x.A,
                              Math.Round((double)x.A * 100 / x.Tong, 2),
                              x.B,
                              Math.Round((double)x.B * 100 / x.Tong, 2),
                              x.C,
                              Math.Round((double)x.C * 100 / x.Tong, 2),
                              x.D,
                              Math.Round((double)x.D * 100 / x.Tong, 2),
                              x.F,
                              Math.Round((double)x.F * 100 / x.Tong, 2)
                              )).ToList();
            int i = 1;
            foreach (MarkByDepartment item in result)
            {
                item.stt = i++;
            }
            return result;
        }

        public Object groupMarkByDepartment(List<StudentMarkViewModel> studentMarkViewModels)
        {
            var result = (from s in studentMarkViewModels
                          group new { 
                              s.departmentID, s.departmentName , s.studentcode, s.studentName, s.mark
                          
                          
                          } by s.departmentID into list
                          select list);
            return result;
        }
        public string getDepartmentName(long? departmentID)
        {
            if (departmentID == null) return "";
            else return models.tbl_department.Where(x => x.id == departmentID).FirstOrDefault().name;
        }
        //Lấy tong so diem A,B,C,D trong nhiều năm
        public Object getSumMarks(List<MarkRate> list)
        {
            long[] result = { 0, 0, 0, 0, 0 };
            foreach(var item in list)
            {
                if (item.A > 0) result[0] += item.A;
                if (item.B > 0) result[1] += item.B;
                if (item.C > 0) result[2] += item.C;
                if (item.D > 0) result[3] += item.D;
                if (item.F > 0) result[4] += item.F;
            }
            return result;
        }
        public Object getSumMarks(List<Data> list)
        {
            long[] result = { 0, 0, 0, 0, 0 };
            foreach (var item in list)
            {
                if (item.A > 0) result[0]+=item.A;
                if (item.B > 0) result[1]+=item.B;
                if (item.C > 0) result[2]+=item.C;
                if (item.D > 0) result[3]+=item.D;
                if (item.F > 0) result[4]+=item.F;
            }
            return result;
        }
        public Object getSumMarks(List<MarksByEnrollmentClass> list)
        {
            long[] result = { 0, 0, 0, 0, 0 };
            foreach (var item in list)
            {
                if (item.A > 0) result[0] += item.A;
                if (item.B > 0) result[1] += item.B;
                if (item.C > 0) result[2] += item.C;
                if (item.D > 0) result[3] += item.D;
                if (item.F > 0) result[4] += item.F;
            }
            return result;
        }
        public Object getSumMarks(List<MarkByDepartment> list)
        {
            long[] result = { 0, 0, 0, 0, 0 };
            foreach (var item in list)
            {
                if (item.A > 0) result[0] += item.A;
                if (item.B > 0) result[1] += item.B;
                if (item.C > 0) result[2] += item.C;
                if (item.D > 0) result[3] += item.D;
                if (item.F > 0) result[4] += item.F;
            }
            return result;
        }

        public List<tbl_subject> getSubject()
        {
            var list = (from s in models.tbl_subject
                        select s).ToList();
            return list;
        }
        public string getSubject(long? subjectId)
        {
            var subjectName = models.tbl_subject.Where(x => x.id == subjectId).Select(x => x.subject_name).FirstOrDefault();
            return subjectName;
        }
        public long getNumberOfCredit(long? subjectId)
        {
            var numberOfCredit = (long) models.tbl_subject.Where(x => x.id == subjectId).Select(x => x.number_of_credit).FirstOrDefault();
            return numberOfCredit;
        }
        public List<tbl_shool_year> getYear()
        {
            var list = (from s in models.tbl_shool_year
                        select s).ToList();
            return list;
        }
        public long getYear(long? yearID)
        {
            long year = (long)models.tbl_shool_year.Where(x => x.id == yearID).Select(x => x.year).FirstOrDefault();
            return year;
        }
        public List<Data> getCourseSubjectData(List<Mark> list)
        {
            var result = (from ls in list
                          group ls by ls.courseSubjectName into ls_group
                          from ls_2 in list
                          where ls_2.courseSubjectName == ls_group.Key
                          select new
                          {
                              semesterName = ls_2.semesterName,
                              subjectName = ls_2.subjectName,
                              numberOfCredit = ls_2.numberOfCredit,
                              courseSubjectName = ls_group.Key,
                              teacherName = ls_2.teacherName,
                              Tong = ls_group.Count(),
                              A = ls_group.Count(x => x.student_Subject_Mark == 4),
                              B = ls_group.Count(x => x.student_Subject_Mark == 3),
                              C = ls_group.Count(x => x.student_Subject_Mark == 2),
                              D = ls_group.Count(x => x.student_Subject_Mark == 1),
                              F = ls_group.Count(x => x.student_Subject_Mark == 0),
                          }).Distinct().ToList().Select(x => new Data(0, x.semesterName, x.courseSubjectName, x.teacherName, x.Tong,
                          x.A,
                          Math.Round((double)x.A * 100 / x.Tong, 2),
                          x.B,
                          Math.Round((double)x.B * 100 / x.Tong, 2),
                          x.C,
                          Math.Round((double)x.C * 100 / x.Tong, 2),
                          x.D,
                          Math.Round((double)x.D * 100 / x.Tong, 2),
                          x.F,
                          Math.Round((double)x.F * 100 / x.Tong, 2),
                          x.subjectName, x.numberOfCredit
                          )).ToList();
            int i = 1;
            foreach (Data item in result)
            {
                item.stt = i++;
            }
            return result;
        }
        
        public List<Data> getCourseSubjectData(List<StudentCourseSubject> studentCourseSubjects, long subject_exam_type_id )
        {
            var Marks = (from scs in studentCourseSubjects
                          join subjectExam in models.tbl_subject_exam
                          on scs.subjectExamID equals subjectExam.id

                          where subjectExam.subject_exam_type_id == subject_exam_type_id
                         select new
                          {
                              studentId = scs.studentId,
                              mark = scs.mark,
                              subjectId = scs.subjectId,
                              couresSubjectID = scs.couresSubjectID,
                              courseSubjectName = scs.courseSubjectName,
                              teacherName = scs.teacherName,
                              subjectName = scs.subjectName,
                              numberOfCredit = scs.numberOfCredit,
                              semesterName = scs.semesterName,
                              subjectExamID = scs.subjectExamID,
                              
                          }).ToList();
            var result = (from ls in Marks
                          group ls by ls.courseSubjectName into ls_group
                          from ls_2 in Marks
                          where ls_2.courseSubjectName == ls_group.Key
                          select new
                          {
                              semesterName = ls_2.semesterName,
                              subjectName = ls_2.subjectName,
                              numberOfCredit = ls_2.numberOfCredit,
                              courseSubjectName = ls_group.Key,
                              teacherName = ls_2.teacherName,
                              Tong = ls_group.Count(),
                              A = ls_group.Count(x => x.mark >= 8.45 && x.mark <= 10),
                              B = ls_group.Count(x => x.mark >= 6.95 && x.mark < 8.45),
                              C = ls_group.Count(x => x.mark >= 5.45 && x.mark < 6.95),
                              D = ls_group.Count(x => x.mark >= 3.95 && x.mark < 5.45),
                              F = ls_group.Count(x => x.mark >= 0 && x.mark < 3.95),
                          }).Distinct().ToList().Select(x => new Data(0, x.semesterName, x.courseSubjectName, x.teacherName, x.Tong,
                          x.A,
                          Math.Round((double)x.A * 100 / x.Tong, 2),
                          x.B,
                          Math.Round((double)x.B * 100 / x.Tong, 2),
                          x.C,
                          Math.Round((double)x.C * 100 / x.Tong, 2),
                          x.D,
                          Math.Round((double)x.D * 100 / x.Tong, 2),
                          x.F,
                          Math.Round((double)x.F * 100 / x.Tong, 2),
                          x.subjectName, x.numberOfCredit
                          )).ToList();
            int i = 1;
            foreach (Data item in result)
            {
                item.stt = i++;
            }
            return result;
           
        }
        
        //lấy điểm của một môn học do giáo viên đảm nhận trong nhiêu năm
        public List<MarkRate> getMarkByTeacher(List<StudentCourseSubject> studentCourseSubjects, long subject_exam_type_id)
        {
            var Marks = (from scs in studentCourseSubjects
                         join subjectExam in models.tbl_subject_exam
                         on scs.subjectExamID equals subjectExam.id

                         where subjectExam.subject_exam_type_id == subject_exam_type_id
                         select new
                         {
                             studentId = scs.studentId,
                             mark = scs.mark,
                             subjectId = scs.subjectId,
                             couresSubjectID = scs.couresSubjectID,
                             courseSubjectName = scs.courseSubjectName,
                             teacherID = scs.teacherID,
                             teacherName = scs.teacherName,
                             subjectID = scs.subjectId,
                             subjectName = scs.subjectName,
                             numberOfCredit = scs.numberOfCredit,
                             semesterName = scs.semesterName,
                             subjectExamID = scs.subjectExamID,
                             endYearID = scs.endYearID,
                             startYearID = scs.startYearID

                         }).ToList();
            var result = (from ls in Marks
                          group ls by ls.teacherID into ls_group
                          from ls_2 in Marks
                          where ls_group.Key == ls_2.teacherID
                          select new
                          {
                              subjectID = ls_2.subjectID,
                              subjectName = ls_2.subjectName,
                              teacherID = ls_group.Key,
                              teacherName = ls_2.teacherName,
                              startYeadID = ls_2.startYearID,
                              endYearID = ls_2.endYearID,
                              Tong = ls_group.Count(),
                              A = ls_group.Count(x => x.mark >= 8.45 && x.mark <= 10),
                              B = ls_group.Count(x => x.mark >= 6.95 && x.mark < 8.45),
                              C = ls_group.Count(x => x.mark >= 5.45 && x.mark < 6.95),
                              D = ls_group.Count(x => x.mark >= 3.95 && x.mark < 5.45),
                              F = ls_group.Count(x => x.mark >= 0 && x.mark < 3.95),
                              numberOfCredit = ls_2.numberOfCredit

                          }).Distinct().ToList().Select(x => new MarkRate(0,x.subjectID,x.subjectName,x.teacherID, x.teacherName,
                          x.startYeadID,x.endYearID,
                          x.Tong,
                          x.A,
                          Math.Round((double)x.A * 100 / x.Tong, 2),
                          x.B,
                          Math.Round((double)x.B * 100 / x.Tong, 2),
                          x.C,
                          Math.Round((double)x.C * 100 / x.Tong, 2),
                          x.D,
                          Math.Round((double)x.D * 100 / x.Tong, 2),
                          x.F,
                          Math.Round((double)x.F * 100 / x.Tong, 2)
                          )).ToList();
            int i = 1;
            foreach (var item in result)
            {
                item.stt = i++;
            }
            return result;
        }
        //lấy điểm của tất cả sinh viên của một giáo viên trong nhiều năm theo từng năm
        public List<MarkRate> getMarkByTeacher(List<StudentCourseSubject> studentCourseSubjects,long? teacherID, long subject_exam_type_id)
        {
            var Marks = (from scs in studentCourseSubjects
                         join subjectExam in models.tbl_subject_exam
                         on scs.subjectExamID equals subjectExam.id

                         where subjectExam.subject_exam_type_id == subject_exam_type_id
                         select new
                         {
                             studentId = scs.studentId,
                             mark = scs.mark,
                             subjectId = scs.subjectId,
                             couresSubjectID = scs.couresSubjectID,
                             courseSubjectName = scs.courseSubjectName,
                             teacherID = scs.teacherID,
                             teacherName = scs.teacherName,
                             subjectName = scs.subjectName,
                             numberOfCredit = scs.numberOfCredit,
                             semesterName = scs.semesterName,
                             subjectExamID = scs.subjectExamID,
                             year = scs.year,

                         }).ToList();
            var result = (from ls in Marks
                          group ls by ls.year into ls_group
                          from ls_2 in Marks
                          where ls_group.Key == ls_2.year
                          select new
                          {
                              subjectName = ls_2.subjectName,
                              year = ls_group.Key,
                              teacherName = ls_2.teacherName,
                              Tong = ls_group.Count(),
                              A = ls_group.Count(x => x.mark >= 8.45 && x.mark <= 10),
                              B = ls_group.Count(x => x.mark >= 6.95 && x.mark < 8.45),
                              C = ls_group.Count(x => x.mark >= 5.45 && x.mark < 6.95),
                              D = ls_group.Count(x => x.mark >= 3.95 && x.mark < 5.45),
                              F = ls_group.Count(x => x.mark >= 0 && x.mark < 3.95),
                              numberOfCredit = ls_2.numberOfCredit

                          }).Distinct().ToList().Select(x => new MarkRate(0,x.year, x.teacherName, x.Tong,
                          x.A,
                          Math.Round((double)x.A * 100 / x.Tong, 2),
                          x.B,
                          Math.Round((double)x.B * 100 / x.Tong, 2),
                          x.C,
                          Math.Round((double)x.C * 100 / x.Tong, 2),
                          x.D,
                          Math.Round((double)x.D * 100 / x.Tong, 2),
                          x.F,
                          Math.Round((double)x.F * 100 / x.Tong, 2),
                          x.numberOfCredit
                          )).ToList();
            int i = 1;
            foreach (var item in result)
            {
                item.stt = i++;
            }
            return result;
        }
        //lay ti le diem cua giao vien theo tên
        public List<MarkRate> getMarkByTeacher(List<Data> sublist)
        {
            var result = (from sl in sublist
                          group sl by sl.teacherName into sl_group
                          from sl_2 in sublist
                          where sl_group.Key == sl_2.teacherName
                          select new
                          {
                              subjectName = sl_2.subjectName,
                              teacherName = sl_group.Key,
                              Tong = sl_group.Sum(x => x.sum),
                              A = sl_group.Sum(x => x.A),
                              B = sl_group.Sum(x => x.B),
                              C = sl_group.Sum(x => x.C),
                              D = sl_group.Sum(x => x.D),
                              F = sl_group.Sum(x => x.F),
                              numberOfCredit = sl_2.numberOfCredit
                          }).Distinct().ToList().Select(x => new MarkRate(0, x.subjectName, x.teacherName, x.Tong,
                          x.A,
                          Math.Round((double)x.A * 100 / x.Tong, 2),
                          x.B,
                          Math.Round((double)x.B * 100 / x.Tong, 2),
                          x.C,
                          Math.Round((double)x.C * 100 / x.Tong, 2),
                          x.D,
                          Math.Round((double)x.D * 100 / x.Tong, 2),
                          x.F,
                          Math.Round((double)x.F * 100 / x.Tong, 2),
                          x.numberOfCredit
                          )).ToList();
            int i = 1;
            foreach (var item in result)
            {
                item.stt = i++;
            }
            return result; 
        }

        //Lấy tỉ lệ điểm tổng kết sinh viên của nhiều giáo viên trong nhiều năm
        public List<MarkRate> getMarkByTeacher(List<Mark> sublist)
        {
            var result = (from sl in sublist
                          group sl by sl.teacherID into sl_group
                          from sl_2 in sublist
                          where sl_group.Key == sl_2.teacherID
                          select new
                          {
                              subjectID = sl_2.subjectID,
                              subjectName = sl_2.subjectName,
                              teacherID = sl_group.Key,
                              teacherName = sl_2.teacherName,
                              startYearID = sl_2.startYearID,
                              endYearID = sl_2.endYearID,
                              Tong = sl_group.Count(),
                              A = sl_group.Count(x => x.student_Subject_Mark == 4),
                              B = sl_group.Count(x => x.student_Subject_Mark == 3),
                              C = sl_group.Count(x => x.student_Subject_Mark == 2),
                              D = sl_group.Count(x => x.student_Subject_Mark == 1),
                              F = sl_group.Count(x => x.student_Subject_Mark == 0),
                              numberOfCredit = sl_2.numberOfCredit
                          }).Distinct().ToList().Select(x => new MarkRate(0,x.subjectID, x.subjectName,x.teacherID,x.teacherName,
                          x.startYearID,x.endYearID,x.Tong,
                          x.A,
                          Math.Round((double)x.A * 100 / x.Tong, 2),
                          x.B,
                          Math.Round((double)x.B * 100 / x.Tong, 2),
                          x.C,
                          Math.Round((double)x.C * 100 / x.Tong, 2),
                          x.D,
                          Math.Round((double)x.D * 100 / x.Tong, 2),
                          x.F,
                          Math.Round((double)x.F * 100 / x.Tong, 2)
                          )).ToList();
            int i = 1;
            foreach (var item in result)
            {
                item.stt = i++;
            }
            return result;
        }
        public List<MarkRate> getMarkByTeacher(List<Mark> sublist,long? teacherID)
        {
            var result = (from sl in sublist
                          group sl by sl.year into sl_group
                          from sl_2 in sublist
                          where sl_group.Key == sl_2.year
                          select new
                          {
                              subjectID = sl_2.subjectID,
                              subjectName = sl_2.subjectName,
                              teacherID = sl_2.teacherID,
                              teacherName = sl_2.teacherName,
                              year = sl_2.year,
                              Tong = sl_group.Count(),
                              A = sl_group.Count(x => x.student_Subject_Mark == 4),
                              B = sl_group.Count(x => x.student_Subject_Mark == 3),
                              C = sl_group.Count(x => x.student_Subject_Mark == 2),
                              D = sl_group.Count(x => x.student_Subject_Mark == 1),
                              F = sl_group.Count(x => x.student_Subject_Mark == 0),
                              numberOfCredit = sl_2.numberOfCredit
                          }).Distinct().ToList().Select(x => new MarkRate(0, x.year, x.teacherName,
                          x.Tong,
                          x.A,
                          Math.Round((double)x.A * 100 / x.Tong, 2),
                          x.B,
                          Math.Round((double)x.B * 100 / x.Tong, 2),
                          x.C,
                          Math.Round((double)x.C * 100 / x.Tong, 2),
                          x.D,
                          Math.Round((double)x.D * 100 / x.Tong, 2),
                          x.F,
                          Math.Round((double)x.F * 100 / x.Tong, 2),
                          x.numberOfCredit
                          )).ToList();
            int i = 1;
            foreach (var item in result)
            {
                item.stt = i++;
            }
            return result;
        }
        public List<MarkRate> getMarkByTeacher(List<StudentMarkViewModel> studentMarkViewModels)
        {

            var result = (from s in studentMarkViewModels
                          group s by new { s.teacherID, s.teacherName,s.semesterId,s.semesterName,
                          s.courseSubjectID,s.courseSubjectName} into list
                          orderby list.Key.teacherID
                          select new
                          {
                              teacherID = list.Key.teacherID,
                              teacherName = list.Key.teacherName,
                              semesterId = list.Key.semesterId,
                              semesterName = list.Key.semesterName,
                              courseSubjectID = list.Key.courseSubjectID,
                              courseSubjectName = list.Key.courseSubjectName,
                              Tong = list.Count(),
                              A = list.Count(x => x.mark >= 8.45 && x.mark <= 10),
                              B = list.Count(x => x.mark >= 6.95 && x.mark < 8.45),
                              C = list.Count(x => x.mark >= 5.45 && x.mark < 6.95),
                              D = list.Count(x => x.mark >= 3.95 && x.mark < 5.45),
                              F = list.Count(x => x.mark >= 0 && x.mark < 3.95)

                          }).ToList().Select(x => new MarkRate(0,x.teacherID, x.teacherName,
                              x.semesterId,x.semesterName,x.courseSubjectID,x.courseSubjectName,
                              x.Tong,
                              x.A,
                              Math.Round((double)x.A * 100 / x.Tong, 2),
                              x.B,
                              Math.Round((double)x.B * 100 / x.Tong, 2),
                              x.C,
                              Math.Round((double)x.C * 100 / x.Tong, 2),
                              x.D,
                              Math.Round((double)x.D * 100 / x.Tong, 2),
                              x.F,
                              Math.Round((double)x.F * 100 / x.Tong, 2)
                              )).ToList();
            int i = 1;
            foreach (MarkRate item in result)
            {
                item.stt = i++;
            }
            return result;
        }
        public List<MarkRate> getDataChartByTeacher(List<StudentMarkViewModel> studentMarkViewModels)
        {

            var result = (from s in studentMarkViewModels
                          group s by new
                          {
                              s.teacherID,
                              s.teacherName,
                          } into list
                          orderby list.Key.teacherID
                          select new
                          {
                              teacherID = list.Key.teacherID,
                              teacherName = list.Key.teacherName,
                              Tong = list.Count(),
                              A = list.Count(x => x.mark >= 8.45 && x.mark <= 10),
                              B = list.Count(x => x.mark >= 6.95 && x.mark < 8.45),
                              C = list.Count(x => x.mark >= 5.45 && x.mark < 6.95),
                              D = list.Count(x => x.mark >= 3.95 && x.mark < 5.45),
                              F = list.Count(x => x.mark >= 0 && x.mark < 3.95)

                          }).ToList().Select(x => new MarkRate(0, x.teacherID, x.teacherName,
                              x.Tong,
                              x.A,
                              Math.Round((double)x.A * 100 / x.Tong, 2),
                              x.B,
                              Math.Round((double)x.B * 100 / x.Tong, 2),
                              x.C,
                              Math.Round((double)x.C * 100 / x.Tong, 2),
                              x.D,
                              Math.Round((double)x.D * 100 / x.Tong, 2),
                              x.F,
                              Math.Round((double)x.F * 100 / x.Tong, 2)
                              )).ToList();
            int i = 1;
            foreach (MarkRate item in result)
            {
                item.stt = i++;
            }
            return result;
        }

        public Object groupMarkByTeacher(List<StudentMarkViewModel> studentMarkViewModels)
        {
            var result = (from s in studentMarkViewModels
                          group new
                          {
                              s.teacherID,
                              s.studentID,
                              s.studentName,
                              s.mark


                          } by s.teacherID into list
                          select list);
            return result;
        }

        public IEnumerable<IGrouping<dynamic, MarkBySemester>> GetMarkByYear(List<string> listId, long subject_id, long year)
        {
            var subjectName = models.tbl_subject.Find(subject_id).subject_name;
            var listSemester = (from s in models.tbl_semester 
                               where s.school_year_id == year
                               select s.id).ToList();

            var list_result = new List<MarkBySemester>();

            var listMark2 = (from s1 in models.tbl_student_subject_mark
                             join s2 in models.tbl_student_mark
                             on s1.id equals s2.student_subject_mark_id

                             join s3 in models.tbl_student
                             on s1.student_id equals s3.id

                             join s5 in models.tbl_subject_exam
                             on s2.subject_exam_id equals s5.id
                             join s6 in models.tbl_semester
                            on s1.semester_id equals s6.id

                             join s7 in models.tbl_subject
                             on s1.subject_id equals s7.id

                             join s8 in models.tbl_department
                             on s7.department_id equals s8.id

                             where s1.subject_id == subject_id
                                      && listSemester.Contains((long)s1.semester_id)
                             group new { mark = s2.mark, type = s5.subject_exam_type_id } by new
                             {
                                 id = s3.id,
                                 code = s3.student_code,
                                 markFinal = s1.mark,
                                 mark4 = s1.mark4,
                                 note = s1.note,
                                 semesterName = s6.semester_name,
                                 departmentName = s8.name == null ? "" : s8.name
                             } into g

                             select new
                             {
                                 id = g.Key.id,
                                 code = g.Key.code,
                                 mark = g.ToList(),
                                 markFinal = g.Key.markFinal,
                                 mark4 = g.Key.mark4,
                                 note = g.Key.note,
                                 semester = g.Key.semesterName,
                                 department = g.Key.departmentName
                             }).ToList();
            var listStudent = (from s1 in models.tbl_course_subject
                               join s2 in models.tbl_student_course_subject
                               on s1.id equals s2.course_subject_id

                               join s3 in models.tbl_student
                               on s2.student_id equals s3.id

                               join s4 in models.tbl_student_semester_subject_exam_room
                               on s2.id equals s4.student_course_subject_id into status

                               join s5 in models.tbl_person
                               on s1.teacher_id equals s5.id into teacher

                               join s6 in models.tbl_person
                               on s3.id equals s6.id

                               where listId.Contains(s1.id.ToString())
                               from j in status.DefaultIfEmpty()
                               from jj in teacher.DefaultIfEmpty()
                               select new
                               {
                                   id = s3.id,
                                   code = s3.student_code,
                                   name = s6.display_name,
                                   teacherName = jj == null ? "" : jj.display_name,
                                   status = j==null?0:j.exam_status_id,
                                   className = s1.display_name
                               }).Distinct().ToList();

            list_result = (from s1 in listStudent
                           join s2 in listMark2
                           on s1.id equals s2.id into joined
                           from s2 in joined.DefaultIfEmpty()
                           where s1.status == 0 || s1.status == null
                           let mark = s2 == null ? null : s2.mark.Where(m => m.type == 2).FirstOrDefault()
                           let markExam = s2 == null ? null : s2.mark.Where(m => m.type == 3).FirstOrDefault()
                           select new MarkBySemester(
                              s1.className, s1.code, s1.name, s1.teacherName, subjectName,
                              (double)(mark == null ? -1 : mark.mark == null ? -1 : mark.mark),
                              (double)(markExam == null ? -1 : markExam.mark == null ? -1 : markExam.mark),
                              (double)(s2 == null ? -1 : s2.markFinal == null ? -1 : s2.markFinal == null ? -1 : s2.markFinal),
                              (int)(s2 == null ? -1 : s2.mark4 == null ? -1 : s2.mark4 == null ? 0 : s2.mark4),
                              (double)(s2 == null ? 0 : s2.mark4 == null ? 0 : s2.mark4 == null ? 0 : s2.mark4),
                              s2 == null ? "" : s2.note,
                              s2 == null ? "" : s2.department
                              )
                           { status = (long)(s2 == null ? -1 : s1.status == null ? 0 : s1.status) ,
                           semester = s2.semester}
                       ).ToList();

            var result = list_result.GroupBy(s => new { className = s.class_name, teacherName = s.teacher_name, subject = s.subject });

            return result;
        }

        public IEnumerable<IGrouping<dynamic, MarkBySemester>> GetMarkByClassYear(List<string> listId, long subject_id, long year)
        {
            var subjectName = models.tbl_subject.Find(subject_id).subject_name;

            var listSemester = (from s in models.tbl_semester
                                where s.school_year_id == year
                                select s.id).ToList();

            var list_result = new List<MarkBySemester>();

            var listMark2 = (from s1 in models.tbl_student_subject_mark
                             join s2 in models.tbl_student_mark
                             on s1.id equals s2.student_subject_mark_id

                             join s3 in models.tbl_student
                             on s1.student_id equals s3.id

                             join s5 in models.tbl_subject_exam
                             on s2.subject_exam_id equals s5.id

                             join s6 in models.tbl_semester
                            on s1.semester_id equals s6.id

                             join s7 in models.tbl_subject
                             on s1.subject_id equals s7.id

                             join s8 in models.tbl_department
                             on s7.department_id equals s8.id
                             where s1.subject_id == subject_id
                                      && listSemester.Contains((long)s1.semester_id)
                             group new { mark = s2.mark, type = s5.subject_exam_type_id } by new
                             {
                                 id = s3.id,
                                 code = s3.student_code,
                                 markFinal = s1.mark,
                                 mark4 = s1.mark4,
                                 note = s1.note,
                                 semesterName = s6.semester_name,
                                 departmentName = s8.name == null ? "" : s8.name
                             } into g

                             select new
                             {
                                 id = g.Key.id,
                                 code = g.Key.code,
                                 mark = g.ToList(),
                                 markFinal = g.Key.markFinal,
                                 mark4 = g.Key.mark4,
                                 note = g.Key.note,
                                 semester = g.Key.semesterName,
                                 department = g.Key.departmentName
                             }).ToList();


            var listStudent = (from s1 in models.tbl_enrollment_class
                               join s2 in models.tbl_student
                               on s1.id equals s2.class_id

                               join s3 in models.tbl_student_course_subject
                               on s2.id equals s3.student_id

                               join s4 in models.tbl_course_subject
                               on s3.course_subject_id equals s4.id

                               join s5 in models.tbl_semester_subject
                               on s4.semester_subject_id equals s5.id

                               join s6 in models.tbl_person
                               on s1.staff_id equals s6.id into teacher

                               join s7 in models.tbl_student_semester_subject_exam_room
                               on s3.id equals s7.student_course_subject_id into status

                               join s8 in models.tbl_person
                               on s2.id equals s8.id

                               where listId.Contains(s1.id.ToString())
                               && s5.subject_id == subject_id
                               && listSemester.Contains((long)s5.semester_id)

                               from j in status.DefaultIfEmpty()
                               from jj in teacher.DefaultIfEmpty()
                               select new
                               {
                                   id = s2.id,
                                   name = s8.display_name,
                                   code = s2.student_code,
                                   teacherName = jj == null ? "" : jj.display_name,
                                   status = j==null?0:j.exam_status_id,
                                   className = s1.className
                               }).ToList();
            list_result = (from s1 in listStudent
                           join s2 in listMark2
                           on s1.id equals s2.id into joined
                           from s2 in joined.DefaultIfEmpty()
                           where s1.status == 0 || s1.status == null
                           let mark = s2 == null ? null : s2.mark.Where(m => m.type == 2).FirstOrDefault()
                           let markExam = s2 == null ? null : s2.mark.Where(m => m.type == 3).FirstOrDefault()
                           select new MarkBySemester(
                              s1.className, s1.code, s1.name, s1.teacherName, subjectName,
                              (double)(mark == null ? -1 : mark.mark == null ? -1 : mark.mark),
                              (double)(markExam == null ? -1 : markExam.mark == null ? -1 : markExam.mark),
                              (double)(s2 == null ? -1 : s2.markFinal == null ? -1 : s2.markFinal == null ? -1 : s2.markFinal),
                              (int)(s2 == null ? -1 : s2.mark4 == null ? -1 : s2.mark4 == null ? 0 : s2.mark4),
                              (double)(s2 == null ? 0 : s2.mark4 == null ? 0 : s2.mark4 == null ? 0 : s2.mark4),
                              s2 == null ? "" : s2.note,
                              s2 == null ? "" : s2.department
                              )
                           { status = (long)(s2 == null ? -1 : s1.status == null ? 0 : s1.status) ,
                           semester = s2.semester}
                       ).ToList();

            var result = list_result.GroupBy(s => new { className = s.class_name, teacherName = s.teacher_name, subject = s.subject });

            return result;
        }


        public IEnumerable<IGrouping<dynamic,MarkBySemester>> GetMarkBySemester(List<string> listId, long subject_id, List<string> semester_id)
        {
            var subjectName = models.tbl_subject.Find(subject_id).subject_name;

            var list_result = new List<MarkBySemester>();
            
            var listMark2 = (from s1 in models.tbl_student_subject_mark
                            join s2 in models.tbl_student_mark
                            on s1.id equals s2.student_subject_mark_id

                            join s3 in models.tbl_student
                            on s1.student_id equals s3.id

                            join s5 in models.tbl_subject_exam
                            on s2.subject_exam_id equals s5.id

                            join s6 in models.tbl_semester
                            on s1.semester_id equals s6.id

                            join s7 in models.tbl_subject
                            on s1.subject_id equals s7.id

                             join s8 in models.tbl_department
                             on s7.department_id equals s8.id
                             where s1.subject_id == subject_id
                                     && semester_id.Contains(s1.semester_id.ToString())

                            group new {mark = s2.mark, type = s5.subject_exam_type_id} by new
                            {
                                id = s3.id,
                                code = s3.student_code,
                                markFinal = s1.mark,
                                mark4 = s1.mark4,
                                note = s1.note,
                                semesterName = s6.semester_name,
                                departmentName = s8.name==null?"": s8.name
                            } into g

                            select new {
                                id = g.Key.id,
                                code = g.Key.code,
                                mark = g.ToList(),
                                markFinal = g.Key.markFinal,
                                mark4 = g.Key.mark4,
                                note = g.Key.note,
                                semesteName = g.Key.semesterName,
                                department = g.Key.departmentName
                            }).ToList();

            #region 
            var listStudent = (from s1 in models.tbl_course_subject
                                join s2 in models.tbl_student_course_subject
                                on s1.id equals s2.course_subject_id

                                join s3 in models.tbl_student
                                on s2.student_id equals s3.id

                                join s4 in models.tbl_student_semester_subject_exam_room
                                on s2.id equals s4.student_course_subject_id into status

                                join s5 in models.tbl_person
                                on s1.teacher_id equals s5.id into teacher

                                join s6 in models.tbl_person
                                on s3.id equals s6.id

                                where listId.Contains(s1.id.ToString())
                                from j in status.DefaultIfEmpty()
                                from jj in teacher.DefaultIfEmpty()
                                select new
                                {
                                    id = s3.id,
                                    code = s3.student_code,
                                    name = s6.display_name,
                                    teacherName = jj == null ? "" : jj.display_name,
                                    status = j == null ? 0 : j.exam_status_id,
                                    className = s1.display_name
                                }).Distinct().ToList();
            #endregion
            list_result = (from s1 in listStudent
                            join s2 in listMark2
                            on s1.id equals s2.id into joined
                            from s2 in joined.DefaultIfEmpty()
                            where s1.status==0||s1.status==null
                            let mark = s2 == null ? null : s2.mark.Where(m => m.type == 2).FirstOrDefault()
                            let markExam = s2 == null ? null : s2.mark.Where(m => m.type == 3).FirstOrDefault()
                            select new MarkBySemester(
                               s1.className, s1.code, s1.name, s1.teacherName, subjectName,
                               (double)(mark == null ? -1 : mark.mark == null ? -1 : mark.mark),
                               (double)(markExam == null ? -1 : markExam.mark == null ? -1 : markExam.mark),
                               (double)(s2==null?-1:s2.markFinal == null ? -1 : s2.markFinal == null ? -1 : s2.markFinal),
                               (int)(s2==null?-1:s2.mark4 == null ? -1 : s2.mark4 == null ? 0 : s2.mark4),
                               (double)(s2 == null ? 0 : s2.mark4 == null ? 0 : s2.mark4 == null ? 0 : s2.mark4),
                               s2==null?"":s2.note,
                                s2 == null ? "" : s2.department)
                            { 
                                status = (long)(s2==null?-1:s1.status == null ? 0 : s1.status),
                                semester = (string)(s2 == null ? "" : s2.semesteName == null ? "" : s2.semesteName+"")
                            }
                       ).ToList();

            var result = list_result.GroupBy(s => new {
                className = s.class_name,
                teacherName = s.teacher_name,
                subject = s.subject,
                department = s.department,
                semester = s.semester
            });
            return result;
        }
        public IEnumerable<IGrouping<dynamic, MarkBySemester>> GetMarkByClass(List<string> listId, long subject_id, List<string> semester_id)
        {
            var subjectName = models.tbl_subject.Find(subject_id).subject_name;

            var list_result = new List<MarkBySemester>();

            var listMark2 = (from s1 in models.tbl_student_subject_mark
                             join s2 in models.tbl_student_mark
                             on s1.id equals s2.student_subject_mark_id

                             join s3 in models.tbl_student
                             on s1.student_id equals s3.id

                             join s5 in models.tbl_subject_exam
                             on s2.subject_exam_id equals s5.id
                             join s6 in models.tbl_semester
                            on s1.semester_id equals s6.id

                             join s7 in models.tbl_subject
                             on s1.subject_id equals s7.id

                             join s8 in models.tbl_department
                             on s7.department_id equals s8.id
                             where s1.subject_id == subject_id
                                      && semester_id.Contains(s1.semester_id.ToString())
                             group new { mark = s2.mark, type = s5.subject_exam_type_id } by new
                             {
                                 id = s3.id,
                                 code = s3.student_code,
                                 markFinal = s1.mark,
                                 mark4 = s1.mark4,
                                 note = s1.note,
                                 semesterName = s6.semester_name,
                                 departmentName = s8.name == null ? "" : s8.name
                             } into g

                             select new
                             {
                                 id = g.Key.id,
                                 code = g.Key.code,
                                 mark = g.ToList(),
                                 markFinal = g.Key.markFinal,
                                 mark4 = g.Key.mark4,
                                 note = g.Key.note,
                                 semester = g.Key.semesterName,
                                 department=g.Key.departmentName
                             }).ToList();


            var listStudent = (from s1 in models.tbl_enrollment_class
                               join s2 in models.tbl_student
                               on s1.id equals s2.class_id

                               join s3 in models.tbl_student_course_subject
                               on s2.id equals s3.student_id

                               join s4 in models.tbl_course_subject
                               on s3.course_subject_id equals s4.id

                               join s5 in models.tbl_semester_subject
                               on s4.semester_subject_id equals s5.id

                               join s6 in models.tbl_person
                               on s1.staff_id equals s6.id into teacher

                               join s7 in models.tbl_student_semester_subject_exam_room
                               on s3.id equals s7.student_course_subject_id into status

                               join s8 in models.tbl_person
                               on s2.id equals s8.id

                               where listId.Contains(s1.id.ToString()) 
                               && s5.subject_id == subject_id
                               && semester_id.Contains(s5.semester_id.ToString())

                               from j in status.DefaultIfEmpty()
                               from jj in teacher.DefaultIfEmpty()
                               select new
                               {
                                   id = s2.id,
                                   name = s8.display_name,
                                   code = s2.student_code,
                                   teacherName = jj == null ? "" : jj.display_name,
                                   status = j==null?0:j.exam_status_id,
                                   className = s1.className
                               }).ToList();
            list_result = (from s1 in listStudent
                           join s2 in listMark2
                           on s1.id equals s2.id into joined
                           from s2 in joined.DefaultIfEmpty()
                           where s1.status == 0 || s1.status == null
                           let mark = s2 == null ? null : s2.mark.Where(m => m.type == 2).FirstOrDefault()
                           let markExam = s2 == null ? null : s2.mark.Where(m => m.type == 3).FirstOrDefault()
                           select new MarkBySemester(
                              s1.className, s1.code, s1.name, s1.teacherName, subjectName,
                              (double)(mark == null ? -1 : mark.mark == null ? -1 : mark.mark),
                              (double)(markExam == null ? -1 : markExam.mark == null ? -1 : markExam.mark),
                              (double)(s2 == null ? -1 : s2.markFinal == null ? -1 : s2.markFinal == null ? -1 : s2.markFinal),
                              (int)(s2 == null ? -1 : s2.mark4 == null ? -1 : s2.mark4 == null ? 0 : s2.mark4),
                              (double)(s2 == null ? 0 : s2.mark4 == null ? 0 : s2.mark4 == null ? 0 : s2.mark4),
                              s2 == null ? "" : s2.note,
                              s2 == null ? "" : s2.department
                              )
                           { status = (long)(s2 == null ? -1 : s1.status == null ? 0 : s1.status),
                            semester = s2.semester
                           }
                       ).ToList();

            var result = list_result.GroupBy(s => new { className = s.class_name, teacherName = s.teacher_name,
                subject = s.subject,
                department = s.department, semester = s.semester });

            return result;
        }

        //lấy điểm của một môn học theo học phần từng năm
        public List<StudentCourseSubject> getMarks_3(long? hocKy, long? khoaHoc, long? dotHoc, long? monHoc )
        {
            var list2 =( from sm in models.tbl_student_mark
                        join ss in models.tbl_semester_subject on sm.semester_subject_id equals ss.id
                        join s in models.tbl_subject on ss.subject_id equals s.id
                        join semester in models.tbl_semester on ss.semester_id equals semester.id
                        join srp in models.tbl_semester_register_period on ss.semester_id equals srp.semeter_id
                        join cs in models.tbl_course_subject on ss.id equals cs.semester_subject_id
                        join scs in models.tbl_student_course_subject on cs.id equals scs.course_subject_id
                        join p in models.tbl_person on cs.teacher_id equals p.id

                        where sm.subject_id ==monHoc
                        && ss.semester_id==hocKy
                        && srp.id==dotHoc
                        && ss.course_year_id ==khoaHoc
                        && sm.student_id == scs.student_id
                        select new
                        {
                            studentId = sm.student_id,
                            mark = sm.mark,
                            subjectId = s.id,
                            courseSubjectID = cs.id,
                            courseSubjectName = cs.display_name,
                            csName = cs.display_name,
                            teacherName = p.display_name,
                            subjectName = s.subject_name,
                            numberOfCredit = s.number_of_credit,
                            semesterID = semester.id,
                            semesterName  = semester.semester_name,
                            subjectExamId = sm.subject_exam_id,
                            studentSubjectMarkID = sm.student_subject_mark_id
                        }).ToList().Select(x => new StudentCourseSubject(x.studentId, x.mark, x.subjectId, x.courseSubjectID,
                x.courseSubjectName, x.teacherName, x.subjectName, x.numberOfCredit, x.semesterID, x.semesterName, x.subjectExamId, x.studentSubjectMarkID));


            return list2.ToList();
        }

        public List<Mark> getListMark(long? hocKy, long? khoaHoc, long? dotHoc, long? monHoc)
        {
            var list_result = (from cs in models.tbl_course_subject
                               join scs in models.tbl_student_course_subject on cs.id equals scs.course_subject_id
                               join sm in models.tbl_student_mark on scs.id equals sm.student_course_subject_id
                               join sub in models.tbl_subject on sm.subject_id equals sub.id
                               join ssm in models.tbl_student_subject_mark on sm.student_subject_mark_id equals ssm.id
                               join ss in models.tbl_semester_subject on sm.semester_subject_id equals ss.id
                               join s in models.tbl_semester on ss.semester_id equals s.id
                               join srp in models.tbl_semester_register_period on s.id equals srp.semeter_id
                               //loai diem
                               join se in models.tbl_subject_exam on sm.subject_exam_id equals se.id
                               //ten giao vien
                               join p in models.tbl_person on cs.teacher_id equals p.id

                               where (srp.id == dotHoc) && (sub.id == monHoc) && (s.id == hocKy) && (ss.course_year_id == khoaHoc)
                               select new
                               {
                                   idhocphan = cs.id, //id học phần
                                   tenhocphan = cs.display_name,//tên học phần
                                   idgiangvien = cs.teacher_id,//giảng viên
                                   tenhocky = s.semester_name,//tên học kỳ
                                   diemtongket = ssm.mark, //điểm tổng kết                           
                                   ssm.mark4,//điểm hệ 4
                                   se.code, //loai diem
                                   semester = s.id,
                                   semesterName = s.semester_name,
                                   subjectName = sub.subject_name,
                                   numberOfCredit = sub.number_of_credit,
                                   couresSubjectID = cs.id,
                                   courseSubjectName = cs.display_name,
                                   teacherName = p.display_name,
                                   student_Mark = sm.mark,
                                   student_Subject_Mark = ssm.mark4
                               }).ToList().Select(s => new Mark(s.semesterName, s.couresSubjectID, s.courseSubjectName,
            s.teacherName, s.student_Mark, s.student_Subject_Mark, s.subjectName, s.numberOfCredit)); 
            
            return list_result.ToList();
        }
        public List<Mark> getListMarkByTeacher(int teacherID,int courseYearID,int semesterID,string code)
        {
            try
            {
                List<Mark> result = (
                        from semesterSubject in models.tbl_semester_subject

                        //lay lop hoc phan
                        join courseSubject in models.tbl_course_subject
                        on semesterSubject.id equals courseSubject.semester_subject_id
                        
                        join studentCourseSubject in models.tbl_student_course_subject
                        on courseSubject.id equals studentCourseSubject.course_subject_id

                        join studentSubjectMark in models.tbl_student_subject_mark
                        on studentCourseSubject.student_id equals studentSubjectMark.student_id

                        join studentMark in models.tbl_student_mark
                        on studentSubjectMark.id equals studentMark.student_subject_mark_id

                        join subjectExam in models.tbl_subject_exam
                        on studentMark.subject_exam_id equals subjectExam.id

                        join subject in models.tbl_subject
                        on semesterSubject.subject_id equals subject.id

                        join semester in models.tbl_semester
                        on semesterSubject.semester_id equals semester.id

                        join courseYear in models.tbl_course_year
                        on semesterSubject.course_year_id equals courseYear.id

                        join person in models.tbl_person
                        on courseSubject.teacher_id equals person.user_id 
                        where semester.id == semesterID
                        && courseYear.id == courseYearID
                        && courseSubject.teacher_id == teacherID
                        && studentSubjectMark.semester_id == semesterID
                        && semesterSubject.subject_id == studentSubjectMark.subject_id
                        && subjectExam.code == code
                        select new
                        {
                            subjectName = subject.subject_name,
                            semesterName = semester.semester_name,
                            courseYearName = courseYear.name,
                            teacherID = teacherID,
                            teacherName = person.display_name,
                            courseSubjectID = courseSubject.id,
                            courseSubjectName = courseSubject.display_name,
                            numberOfCredit = subject.number_of_credit,
                            student_mark = studentMark.mark,
                            student_subject_mark = studentSubjectMark.mark,
                            code = subjectExam.code
                        }
                    ).ToList().Select(x => new Mark(x.subjectName, x.semesterName, x.courseYearName, x.teacherID, x.teacherName, x.courseSubjectID,
                    x.courseSubjectName, x.numberOfCredit, x.student_mark, x.student_subject_mark, x.code)).ToList();
                return result;

            }
            catch(Exception ex)
            {
                return new List<Mark>();
                log.Error(ex.Message);
            }
        }

        public List<MarkRate> getRateMarkByTeacher(int teacherID, int courseYearID, int semesterID,string markOption)
        {
            try
            {
                List<Mark> marks;
                if (markOption == "DT") 
                    marks = getListMarkByTeacher(teacherID, courseYearID, semesterID,"THI");
                else marks = getListMarkByTeacher(teacherID, courseYearID, semesterID, "DQT");
                var result = (from m in marks
                              group m by m.couresSubjectID into m_group
                              from m_i in marks
                              where m_group.Key == m_i.couresSubjectID
                              select new
                              {
                                  subjectName = m_i.subjectName,
                                  courseSubjectName = m_i.courseSubjectName,
                                  teacherName = m_i.teacherName,
                                  Tong = m_group.Count(),
                                  A = m_group.Count(x => x.student_Mark >= 8.45 && x.student_Mark <= 10),
                                  B = m_group.Count(x => x.student_Mark >= 6.95 && x.student_Mark < 8.45),
                                  C = m_group.Count(x => x.student_Mark >= 5.45 && x.student_Mark < 6.95),
                                  D = m_group.Count(x => x.student_Mark >= 3.95 && x.student_Mark < 5.45),
                                  F = m_group.Count(x => x.student_Mark >= 0 && x.student_Mark < 3.95),
                                  numberOfCredit = m_i.numberOfCredit
                              }

                    ).Distinct().ToList().Select(x => new MarkRate(0, x.subjectName,x.courseSubjectName, x.teacherName, x.Tong,
                          x.A,
                          Math.Round((double)x.A * 100 / x.Tong, 2),
                          x.B,
                          Math.Round((double)x.B * 100 / x.Tong, 2),
                          x.C,
                          Math.Round((double)x.C * 100 / x.Tong, 2),
                          x.D,
                          Math.Round((double)x.D * 100 / x.Tong, 2),
                          x.F,
                          Math.Round((double)x.F * 100 / x.Tong, 2),
                          x.numberOfCredit
                          )).ToList();
                int i = 1;
                foreach (var item in result)
                {
                    item.stt = i++;
                }
                return result;
            }
            catch(Exception e)
            {
                log.Error(e.Message);
                return new List<MarkRate>();
            }
        }
        public List<MarkRate> getRateMarkByTeacher(int teacherID, int courseYearID, int semesterID)
        {
            try
            {
                List<Mark> marks = getListMarkByTeacher(teacherID, courseYearID, semesterID, "THI");
                var result = (from m in marks
                              group m by m.couresSubjectID into m_group
                              from m_i in marks
                              where m_group.Key == m_i.couresSubjectID
                              select new
                              {
                                  subjectName = m_i.subjectName,
                                  courseSubjectName = m_i.courseSubjectName,
                                  teacherName = m_i.teacherName,
                                  Tong = m_group.Count(),
                                  A = m_group.Count(x => x.student_Subject_Mark >= 8.45 && x.student_Subject_Mark <= 10),
                                  B = m_group.Count(x => x.student_Subject_Mark >= 6.95 && x.student_Subject_Mark < 8.45),
                                  C = m_group.Count(x => x.student_Subject_Mark >= 5.45 && x.student_Subject_Mark < 6.95),
                                  D = m_group.Count(x => x.student_Subject_Mark >= 3.95 && x.student_Subject_Mark < 5.45),
                                  F = m_group.Count(x => x.student_Subject_Mark >= 0 && x.student_Subject_Mark < 3.95),
                                  numberOfCredit = m_i.numberOfCredit
                              }

                    ).Distinct().ToList().Select(x => new MarkRate(0, x.subjectName, x.courseSubjectName, x.teacherName, x.Tong,
                          x.A,
                          Math.Round((double)x.A * 100 / x.Tong, 2),
                          x.B,
                          Math.Round((double)x.B * 100 / x.Tong, 2),
                          x.C,
                          Math.Round((double)x.C * 100 / x.Tong, 2),
                          x.D,
                          Math.Round((double)x.D * 100 / x.Tong, 2),
                          x.F,
                          Math.Round((double)x.F * 100 / x.Tong, 2),
                          x.numberOfCredit
                          )).ToList();
                int i = 1;
                foreach (var item in result)
                {
                    item.stt = i++;
                }
                return result;
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                return new List<MarkRate>();
            }
        }
        public dynamic getListNameTeachers()
        {
            try
            {
                var role = models.tbl_role.Where(r => r.id == 13).FirstOrDefault();
                var user = role.tbl_user;
                var person = models.tbl_person.ToList();
                var list = (from p in person
                            join u in user
                            on p.user_id equals u.id
                            select new
                            {
                                id = u.id,
                                name = p.display_name,
                            }).ToList();
                return list;
            }
            catch(Exception ex)
            {
                log.Error(ex.Message);
                return new List<dynamic>();
            }
        }
        // Semester
        public string getSemester(long? hocKy)
        {
            var semesterName = models.tbl_semester.Where(x => x.id == hocKy).Select(x => x.semester_name).FirstOrDefault();
            return semesterName;
        }
        public dynamic getSemester(int startYear, int endYear)
        {
            try
            {
                var list = (
                        from s in models.tbl_semester
                        join c in models.tbl_shool_year
                        on s.school_year_id equals c.id
                        where c.year >= startYear && c.year <= endYear
                        select new
                        {
                            id = s.id,
                            name = s.semester_name
                        }


                    ).ToList();
                return list;
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                return new List<dynamic>();
            }
        }

        public List<tbl_semester> GetSemesters()
        {
            return models.tbl_semester.ToList();
        }
        public string getSemeterName(long? id)
        {
            return models.tbl_semester.Where(x => x.id == id).FirstOrDefault().semester_name;
        }

        // Course Year
        public List<tbl_course_year> getCourseYear()
        {
            try
            {
                return models.tbl_course_year.ToList();
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                return new List<tbl_course_year>();
            }
        }
        public string getCourseYearName(int id)
        {
            var courserName = models.tbl_course_year.Where(x => x.id == id).Select(x => x.name).FirstOrDefault();
            return courserName;
        }

        // TeacherName
        public string getSemesterName(List<string> semes)
        {
            var sem = models.tbl_semester.Where(s => semes.Contains(s.id.ToString()));
            string str = "";
            foreach(var s in sem)
            {
                str += s.semester_name + ", ";
            }
            if (str.Length > 2) return str.Substring(0, str.Length - 2);
            else return "";
        }
        public string getTeacherName(long? teacherID)
        {
            try
            {
                var teacher = models.tbl_person.Where(x => x.id == teacherID).FirstOrDefault();
                return teacher.display_name;
            }
            catch(Exception e)
            {
                log.Error(e.Message);
                return "";
            }
        }

    }
}