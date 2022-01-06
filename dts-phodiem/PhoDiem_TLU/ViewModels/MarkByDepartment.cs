using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoDiem_TLU.ViewModels
{
    public class MarkByDepartment:MarkRate
    {
        public MarkByDepartment(long stt, long? subjectID, string subjectName, long? departmentID, string departmentName,
            long? startYearID, long? endYearID,
            long sum, long A, double rateA, long B, double rateB, long C, double rateC, long D, double rateD,
            long F, double rateF)
        {
            this.stt = stt;
            this.subjectID = subjectID;
            this.subjectName = subjectName;
            this.departmentID = departmentID;
            this.departmentName = departmentName;
            this.startYearID = startYearID;
            this.endYearID = endYearID;
            this.sum = sum;
            this.A = A;
            this.rateA = rateA;
            this.B = B;
            this.rateB = rateB;
            this.C = C;
            this.rateC = rateC;
            this.D = D;
            this.rateD = rateD;
            this.F = F;
            this.rateF = rateF;
        }
        public MarkByDepartment(long stt, long? subjectID, string subjectName, long? departmentID, string departmentName,
            long? year,
            long sum, long A, double rateA, long B, double rateB, long C, double rateC, long D, double rateD,
            long F, double rateF)
        {
            this.stt = stt;
            this.subjectID = subjectID;
            this.subjectName = subjectName;
            this.departmentID = departmentID;
            this.departmentName = departmentName;
            this.year = year;
            this.sum = sum;
            this.A = A;
            this.rateA = rateA;
            this.B = B;
            this.rateB = rateB;
            this.C = C;
            this.rateC = rateC;
            this.D = D;
            this.rateD = rateD;
            this.F = F;
            this.rateF = rateF;
        }
        public long? departmentID { get; set; }
        public string departmentName { get; set; }
    }
}