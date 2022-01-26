using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoDiem_TLU.ViewModels
{
    public class StudentMarkViewModel
    {
        public StudentMarkViewModel(long? studentID, string studentName, string studentcode, double? mark, long? courseSubjectID,
            string courseSubjectName, long? teacherID, long? enrollmentClassID, string enrollmentClassName, long? departmentID,
            string departmentName)
        {
            this.studentID = studentID;
            this.studentName = studentName;
            this.studentcode = studentcode;
            this.mark = mark;
            this.courseSubjectID = courseSubjectID;
            this.courseSubjectName = courseSubjectName;
            this.teacherID = teacherID;
            this.enrollmentClassID = enrollmentClassID;
            this.enrollmentClassName = enrollmentClassName;
            this.departmentID = departmentID;
            this.departmentName = departmentName;
        }
        public StudentMarkViewModel(long? studentID, string studentcode, double? mark, long? courseSubjectID,
            string courseSubjectName, long? teacherID,string teacherName, long? enrollmentClassID, string enrollmentClassName, long? departmentID,
            string departmentName)
        {
            this.studentID = studentID;
            this.studentcode = studentcode;
            this.mark = mark;
            this.courseSubjectID = courseSubjectID;
            this.courseSubjectName = courseSubjectName;
            this.teacherID = teacherID;
            this.teacherName = teacherName;
            this.enrollmentClassID = enrollmentClassID;
            this.enrollmentClassName = enrollmentClassName;
            this.departmentID = departmentID;
            this.departmentName = departmentName;
        }

        public long? studentID { get; set; }
        public string studentName { get; set; }
        public string studentcode { get; set; }
        public double? mark { get; set; }
        public long? courseSubjectID { get; set; }
        public string courseSubjectName { get; set; }
        public long? teacherID { get; set; }
        public string teacherName { get; set; }
        public long? enrollmentClassID { get; set; }
        public string enrollmentClassName { get; set; }
        public long? departmentID { get; set; }
        public string departmentName { get; set; }
    }
}