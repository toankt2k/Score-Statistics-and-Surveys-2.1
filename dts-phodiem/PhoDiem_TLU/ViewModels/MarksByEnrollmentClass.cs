using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoDiem_TLU.ViewModels
{
    public class MarksByEnrollmentClass:MarkRate
    {
        public MarksByEnrollmentClass(long stt, string subjectName, string enrollmentClassName, long sumMark, long a, double rateA, long b, double rateB, long c, double rateC, long d, double rateD, long f, double rateF)
        {
            this.stt = stt;
            this.subjectName = subjectName;
            this.enrollmentClassName = enrollmentClassName;
            this.sum = sumMark;
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
        }
        public MarksByEnrollmentClass(long stt,long? subjectID, string subjectName,long? enrollmentClassID,
            string enrollmentClassName,long? startYear,long? endYear,
            long sumMark, long a, double rateA, long b, double rateB, long c, double rateC, long d, double rateD, long f, double rateF)
        {
            this.stt = stt;
            this.subjectID = subjectID;
            this.subjectName = subjectName;
            this.enrollmentClassID = enrollmentClassID;
            this.enrollmentClassName = enrollmentClassName;
            this.startYearID = startYear;
            this.endYearID = endYear;
            this.sum = sumMark;
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
        }
        
        public MarksByEnrollmentClass(long stt, long? subjectID, string subjectName, long? enrollmentClassID,
            string enrollmentClassName,long? year,
            long sumMark, long a, double rateA, long b, double rateB, long c, double rateC, long d, double rateD, long f, double rateF)
        {
            this.stt = stt;
            this.subjectID = subjectID;
            this.subjectName = subjectName;
            this.enrollmentClassID = enrollmentClassID;
            this.enrollmentClassName = enrollmentClassName;
            this.year = year;
            this.sum = sumMark;
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
        }
        public long? enrollmentClassID { get; set; }
        public string enrollmentClassName { get; set; }
        





    }
}