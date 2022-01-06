using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoDiem_TLU.ViewModels
{
    public class MarkStatiticBySemester
    {
        public MarkStatiticBySemester()
        {

        }
        public MarkStatiticBySemester(int stt, string _className, string _teacherName, long a, long b, long c, long d, long f, long total, string subjectName)
        {
            this.stt = stt;
            this.className = _className;
            this.teacherName = _teacherName;
            A = a;
            this.rateA = Math.Round((double)a * 100 / total,1) + " %";
            B = b;
            this.rateB = Math.Round((double)b * 100 / total,1) + " %";
            C = c;
            this.rateC = Math.Round((double)c * 100 / total,1) + " %";
            D = d;
            this.rateD = Math.Round((double)d * 100 / total,1) + " %";
            F = f;
            this.rateF = Math.Round((double)f * 100 / total,1) + " %";
            this.subjectName = subjectName;
        }
        public long stt { set; get; }
        public string className { set; get; }
        public string teacherName { set; get; }
        public string subjectName { get; set; }
        public long A { set; get; }
        public string rateA { set; get; }
        public long B { set; get; }
        public string rateB { set; get; }
        public long C { set; get; }
        public string rateC { set; get; }
        public long D { set; get; }
        public string rateD { set; get; }
        public long F { set; get; }
        public string rateF { set; get; }
        

    }
}