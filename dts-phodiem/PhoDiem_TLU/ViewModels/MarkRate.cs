using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoDiem_TLU.ViewModels
{
    public class MarkRate
    {
        public MarkRate()
        {

        }
        public MarkRate(long stt, string subjectName, string teacherName, long sum, long a, double rateA, long b, double rateB, long c, double rateC, long d, double rateD, long f, double rateF, long? numberOfCredit)
        {
            this.stt = stt;
            this.subjectName = subjectName;
            this.teacherName = teacherName;
            this.sum = sum;
            A = a;
            this.rateA = rateA;
            B = b;
            this.rateB = rateB;
            C = c;
            this.rateC = rateC;
            D = d;
            this.rateD = rateD;
            F = f;
            this.rateF = rateF;
            this.numberOfCredit = numberOfCredit;
        }
        public MarkRate(long stt,long? subjectID, string subjectName,long? teacherID, string teacherName,
            long? startYeadID,long? endYearID,
            long sum, long a, double rateA, long b, double rateB, long C, double rateC, long d, double rateD,
            long F, double rateF)
        {
            this.stt = stt;
            this.subjectID = subjectID;
            this.subjectName = subjectName;
            this.teacherID = teacherID;
            this.teacherName = teacherName;
            this.startYearID = startYeadID;
            this.endYearID = endYearID;
            this.sum = sum;
            A = a;
            this.rateA = rateA;
            B = b;
            this.rateB = rateB;
            this.C = C;
            this.rateC = rateC;
            D = d;
            this.rateD = rateD;
            this.F = F;
            this.rateF = rateF;
        }
        public MarkRate(long stt, string subjectName,string courseSubjectName, string teacherName, long sum, long a, double rateA, long b, double rateB, long c, double rateC, long d, double rateD, long f, double rateF, long? numberOfCredit)
        {
            this.stt = stt;
            this.subjectName = subjectName;
            this.courseSubjectName = courseSubjectName;
            this.teacherName = teacherName;
            this.sum = sum;
            A = a;
            this.rateA = rateA;
            B = b;
            this.rateB = rateB;
            C = c;
            this.rateC = rateC;
            D = d;
            this.rateD = rateD;
            F = f;
            this.rateF = rateF;
            this.numberOfCredit = numberOfCredit;
        }
        public MarkRate(long stt, long? year, string teacherName, long sum, long a, double rateA, long b, double rateB, long c, double rateC, long d, double rateD, long f, double rateF, long? numberOfCredit)
        {
            this.stt = stt;
            this.year = year;
            this.teacherName = teacherName;
            this.sum = sum;
            A = a;
            this.rateA = rateA;
            B = b;
            this.rateB = rateB;
            C = c;
            this.rateC = rateC;
            D = d;
            this.rateD = rateD;
            F = f;
            this.rateF = rateF;
            this.numberOfCredit = numberOfCredit;
        }

        public long stt { set; get; }
        public long? subjectID { get; set; }
        public string subjectName { get; set; }
        public long? year { get; set; }
        public string courseSubjectName { get; set; }
        public long? teacherID { get; set; }    
        public string teacherName { set; get; }
        public long? startYearID { get; set; }
        public long? endYearID { get; set; }
        public long sum { set; get; }
        public long A { set; get; }
        public double rateA { set; get; }
        public long B { set; get; }
        public double rateB { set; get; }
        public long C { set; get; }
        public double rateC { set; get; }
        public long D { set; get; }
        public double rateD { set; get; }
        public long F { set; get; }
        public double rateF { set; get; }
        public long? numberOfCredit { get; set; }
    }
}