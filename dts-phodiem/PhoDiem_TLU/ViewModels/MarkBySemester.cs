using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoDiem_TLU.ViewModels
{
    public class MarkBySemester
    {
        public MarkBySemester()
        {

        }
        public MarkBySemester(string _class_name, string _student_code, string _student_name, string _teacherName, string _subject, double? _mark, double? _mark_exam, double? _mark_final, int? _gpa, double? _mark_gpa, string _note)
        {
            this.class_name = _class_name;
            this.student_code = _student_code;
            this.student_name = _student_name;
            this.mark = _mark<0||_mark==null?"Chưa nhập điểm":_mark.ToString();
            this.mark_exam = _mark_exam<0||_mark_exam==null?"Chưa nhập điểm":_mark_exam.ToString();
            this.mark_final = _mark_final<0||_mark_final==null?"Chưa nhập điểm":_mark_final.ToString();
            this.teacher_name = _teacherName;
            this.subject = _subject;
            this.gpa = 'F';
            if (_gpa == 4) this.gpa = 'A';
            else if (_gpa == 3) this.gpa = 'B';
            else if (_gpa == 2) this.gpa = 'C';
            else if (_gpa == 1) this.gpa = 'D';
            this.mark_gpa = _mark_gpa;
            this.note = _note;
        }

        public string class_name { get; set; }
        public string student_code { get; set; }
        public string student_name { get; set; }
        public string subject { get; set; }

        public string teacher_name { get; set; }
        public string mark { get; set; }
        public string mark_exam { get; set; }
        public string mark_final { get; set; }
        public char gpa { get; set; }
        public double? mark_gpa { get; set; }
        public string note { get; set; }
        public long? status { get; set; }

    }
}
