using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoDiem_TLU.ViewModels
{
    public class StudentCourseSubject
    {
        public StudentCourseSubject(long? studentId, double? mark, long subjectId, long couresSubjectID, string courseSubjectName, string teacherName, string subjectName, long? numberOfCredit, long? semesterID, string semesterName, long? subjectExamID, long? studentSubjectMarkID)
        {
            this.studentId = studentId;
            this.mark = mark;
            this.subjectId = subjectId;
            this.couresSubjectID = couresSubjectID;
            this.courseSubjectName = courseSubjectName;
            this.teacherName = teacherName;
            this.subjectName = subjectName;
            this.numberOfCredit = numberOfCredit;
            this.semesterID = semesterID;
            this.semesterName = semesterName;
            this.subjectExamID = subjectExamID;
            this.studentSubjectMarkID = studentSubjectMarkID;
        }
        public StudentCourseSubject(long? studentId, double? mark, long subjectId, long couresSubjectID, string courseSubjectName,
            long? teacherID,string teacherName, string subjectName, long? numberOfCredit, long? semesterID, string semesterName,
            long? subjectExamID, long? studentSubjectMarkID,long? startYearID,long? endYearID)
        {
            this.studentId = studentId;
            this.mark = mark;
            this.subjectId = subjectId;
            this.couresSubjectID = couresSubjectID;
            this.courseSubjectName = courseSubjectName;
            this.teacherID  = teacherID;
            this.teacherName = teacherName;
            this.subjectName = subjectName;
            this.numberOfCredit = numberOfCredit;
            this.semesterID = semesterID;
            this.semesterName = semesterName;
            this.subjectExamID = subjectExamID;
            this.studentSubjectMarkID = studentSubjectMarkID;
            this.startYearID = startYearID;
            this.endYearID = endYearID;
        }
        public StudentCourseSubject(long? studentId, double? mark, long subjectId, long couresSubjectID, string courseSubjectName,
            long? teacherID, string teacherName, string subjectName, long? numberOfCredit, long? semesterID, string semesterName,
            long? subjectExamID, long? studentSubjectMarkID, long? year)
        {
            this.studentId = studentId;
            this.mark = mark;
            this.subjectId = subjectId;
            this.couresSubjectID = couresSubjectID;
            this.courseSubjectName = courseSubjectName;
            this.teacherID = teacherID;
            this.teacherName = teacherName;
            this.subjectName = subjectName;
            this.numberOfCredit = numberOfCredit;
            this.semesterID = semesterID;
            this.semesterName = semesterName;
            this.subjectExamID = subjectExamID;
            this.studentSubjectMarkID = studentSubjectMarkID;
            this.year = year;
        }
        public long? studentId { get; set; }
        public double? mark { get; set; }
        public long subjectId { get; set; }
        public long couresSubjectID { get; set; }
        public string courseSubjectName { get; set; }
        public long? teacherID { get; set; }
        public string teacherName { get; set; }
        public string subjectName { get; set; }
        public long? numberOfCredit { get; set; }

        public long? semesterID { get; set; }
        public string semesterName { get; set; }
        public long? subjectExamID { get; set; }
        public long? studentSubjectMarkID { get; set; }
        public long? startYearID { get; set; }
        public long? endYearID { get; set; }
        public long? year { get; set; }
    }
}