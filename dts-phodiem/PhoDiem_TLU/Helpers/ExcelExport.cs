﻿using OfficeOpenXml;
using OfficeOpenXml.Style;
using PhoDiem_TLU.DatabaseIO;
using PhoDiem_TLU.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using XLT_TLU.Models;

namespace PhoDiem_TLU.Helpers
{
    public class ExcelExport
    {
        private DBIO db = new DBIO();

        public Byte[] ExportBySemester(List<tbl_course_subject> list_gr, tbl_semester semester, tbl_subject subject)
        {
            using (var excelPackage = new ExcelPackage(new FileInfo("C:\\Users\\toank\\toan2k.xlsx")))
            {
                // Tạo author cho file Excel
                // Tạo title cho file Excel
                excelPackage.Workbook.Properties.Title = "Phổ điểm TLU";
                int count_ws = 0;
                var listResult = db.GetMarkBySemester(list_gr.Select(s=>s.id.ToString()).ToList(), subject.id, semester.id);

                excelPackage.Workbook.Worksheets.Add("Tổng hợp");
                ExcelWorksheet workSheetDefault = excelPackage.Workbook.Worksheets[count_ws];
                //sheet tổng hợp
                #region sheet tổng hợp
                workSheetDefault.Cells["A1:J1"].Merge = true;
                workSheetDefault.Cells["A1:J1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                workSheetDefault.Cells["A1:J1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheetDefault.Cells["A1"].Value = "TRƯỜNG ĐẠI HỌC THỦY LỢI";

                workSheetDefault.Cells["A2:J2"].Merge = true;
                workSheetDefault.Cells["A2:J2"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                workSheetDefault.Cells["A2:J2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheetDefault.Cells["A2"].Value = "PHÒNG KHẢO THÍ VÀ ĐẢM BẢO CHẤT LƯỢNG";

                workSheetDefault.Cells["A4:J4"].Merge = true;
                workSheetDefault.Cells["A4:J4"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                workSheetDefault.Cells["A4:J4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheetDefault.Cells["A4"].Value = "PHIẾU BÁO ĐIỂM - " + subject.subject_name;

                workSheetDefault.Cells["A5:J5"].Merge = true;
                workSheetDefault.Cells["A5:J5"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                workSheetDefault.Cells["A5:J5"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheetDefault.Cells["A5"].Value = "Học kỳ: " + semester.semester_name;

                workSheetDefault.Cells["A6:C6"].Merge = true;
                workSheetDefault.Cells["A6"].Value = "Môn: " + subject.subject_name;

                workSheetDefault.Cells["A7:B7"].Merge = true;
                workSheetDefault.Cells["A7"].Value = "Số tín chỉ: " + subject.number_of_credit;

                workSheetDefault.Cells["A9"].Value = "STT";
                workSheetDefault.Cells["B9"].Value = "Mã sinh viên";
                workSheetDefault.Cells["C9:D9"].Merge = true;
                workSheetDefault.Cells["C9"].Value = "Họ và tên";
                workSheetDefault.Cells["E9"].Value = "DQT";
                workSheetDefault.Cells["F9"].Value = "THI";
                workSheetDefault.Cells["G9"].Value = "TKHP";
                workSheetDefault.Cells["H9"].Value = "Chữ";
                workSheetDefault.Cells["I9"].Value = "Hệ 4";
                workSheetDefault.Cells["J9"].Value = "Ghi chú";

                workSheetDefault.Row(9).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheetDefault.Row(6).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                workSheetDefault.Row(7).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                workSheetDefault.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheetDefault.Column(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheetDefault.Column(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheetDefault.Column(4).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheetDefault.Column(5).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheetDefault.Column(6).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheetDefault.Column(7).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheetDefault.Column(8).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheetDefault.Column(9).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                workSheetDefault.Cells["A1:J9"].Style.Font.Bold = true;
                #endregion
                int index = 10;
                int tcA = 0;
                int tcB = 0;
                int tcC = 0;
                int tcD = 0;
                int tcF = 0;
                int max = index+2;
                int total = 0;
                //thêm các sheet chi tiết
                foreach (var item in listResult)
                {
                    excelPackage.Workbook.Worksheets.Add((count_ws + 1) + "-" + item.Key.className);
                    ExcelWorksheet workSheet = excelPackage.Workbook.Worksheets[count_ws+1];
                    List<MarkBySemester> temp = item.ToList();
                    #region sheet chi tiết
                    workSheet.Cells["A1:J1"].Merge = true;
                    workSheet.Cells["A1:J1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    workSheet.Cells["A1:J1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells["A1"].Value = "TRƯỜNG ĐẠI HỌC THỦY LỢI";

                    workSheet.Cells["A2:J2"].Merge = true;
                    workSheet.Cells["A2:J2"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    workSheet.Cells["A2:J2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells["A2"].Value = "PHÒNG KHẢO THÍ VÀ ĐẢM BẢO CHẤT LƯỢNG";

                    workSheet.Cells["A4:J4"].Merge = true;
                    workSheet.Cells["A4:J4"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    workSheet.Cells["A4:J4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells["A4"].Value = "PHIẾU BÁO ĐIỂM - " + item.Key.className;

                    workSheet.Cells["A5:J5"].Merge = true;
                    workSheet.Cells["A5:J5"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    workSheet.Cells["A5:J5"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells["A5"].Value = "Học kỳ: " + semester.semester_name;

                    workSheet.Cells["A6:C6"].Merge = true;
                    workSheet.Cells["A6"].Value = "Môn: " + subject.subject_name;

                    workSheet.Cells["A7:B7"].Merge = true;
                    workSheet.Cells["A7"].Value = "Số tín chỉ: " + subject.number_of_credit;

                    workSheet.Cells["A9"].Value = "STT";
                    workSheet.Cells["B9"].Value = "Mã sinh viên";
                    workSheet.Cells["C9:D9"].Merge = true;
                    workSheet.Cells["C9"].Value = "Họ và tên";
                    workSheet.Cells["E9"].Value = "DQT";
                    workSheet.Cells["F9"].Value = "THI";
                    workSheet.Cells["G9"].Value = "TKHP";
                    workSheet.Cells["H9"].Value = "Chữ";
                    workSheet.Cells["I9"].Value = "Hệ 4";
                    workSheet.Cells["J9"].Value = "Ghi chú";

                    workSheet.Row(9).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Row(6).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    workSheet.Row(7).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    workSheet.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Column(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Column(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Column(4).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Column(5).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Column(6).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Column(7).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Column(8).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Column(9).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    workSheet.Cells["A1:J9"].Style.Font.Bold = true;
                    #endregion
                    max += temp.Count;
                    for (int i = 1; i <= temp.Count; i++)
                    {
                        #region chi tiết
                        int y = i + 9;
                        var name = temp.ElementAt(i - 1).student_name;
                        workSheet.Cells[y, 1].Value = i;
                        workSheet.Cells[y, 2].Value = temp.ElementAt(i - 1).student_code;
                        workSheet.Cells[y, 3].Value = name.Substring(0, name.LastIndexOf(" "));
                        workSheet.Cells[y, 4].Value = name.Substring(name.LastIndexOf(" ") + 1);
                        workSheet.Cells[y, 5].Value = temp.ElementAt(i - 1).mark;
                        workSheet.Cells[y, 6].Value = temp.ElementAt(i - 1).mark_exam;
                        workSheet.Cells[y, 7].Value = temp.ElementAt(i - 1).mark_final;
                        workSheet.Cells[y, 8].Value = temp.ElementAt(i - 1).gpa;
                        workSheet.Cells[y, 9].Value = temp.ElementAt(i - 1).mark_gpa;
                        workSheet.Cells[y, 10].Value = temp.ElementAt(i - 1).note;
                        #endregion
                        #region tổng hợp
                        index++;
                        name = temp.ElementAt(i - 1).student_name;
                        workSheetDefault.Cells[index, 1].Value = i;
                        workSheetDefault.Cells[index, 2].Value = temp.ElementAt(i - 1).student_code;
                        workSheetDefault.Cells[index, 3].Value = name.Substring(0, name.LastIndexOf(" "));
                        workSheetDefault.Cells[index, 4].Value = name.Substring(name.LastIndexOf(" ") + 1);
                        workSheetDefault.Cells[index, 5].Value = temp.ElementAt(i - 1).mark;
                        workSheetDefault.Cells[index, 6].Value = temp.ElementAt(i - 1).mark_exam;
                        workSheetDefault.Cells[index, 7].Value = temp.ElementAt(i - 1).mark_final;
                        workSheetDefault.Cells[index, 8].Value = temp.ElementAt(i - 1).gpa;
                        workSheetDefault.Cells[index, 9].Value = temp.ElementAt(i - 1).mark_gpa;
                        workSheetDefault.Cells[index, 10].Value = temp.ElementAt(i - 1).note;
                        #endregion

                    }

                    int row_max = temp.Count + 11;

                    workSheet.Cells["A" + (row_max + 1) + ":D" + (row_max + 1)].Merge = true;
                    workSheet.Cells["A" + (row_max + 1) + ":D" + (row_max + 1)].Value = "BẢNG THỐNG KÊ KÊT QUẢ THI";
                    workSheet.Cells["A" + (row_max + 2)].Value = "Điểm chữ";
                    workSheet.Cells["B" + (row_max + 2)].Value = "Điểm số";
                    workSheet.Cells["C" + (row_max + 2)].Value = "Số SV";
                    workSheet.Cells["D" + (row_max + 2)].Value = "Tỷ lệ %";
                    int cA = 0;
                    int cB = 0;
                    int cC = 0;
                    int cD = 0;
                    int cF = 0;
                    foreach (var s in temp)
                    {
                        if(s.status == 0)
                        {
                            total++;
                            if (double.Parse(s.mark_exam) <= 10 && double.Parse(s.mark_exam) >= 8.45) cA++;
                            if (double.Parse(s.mark_exam) <= 8.44 && double.Parse(s.mark_exam) >= 6.95) cB++;
                            if (double.Parse(s.mark_exam) <= 6.94 && double.Parse(s.mark_exam) >= 5.45) cC++;
                            if (double.Parse(s.mark_exam) <= 5.44 && double.Parse(s.mark_exam) >= 3.95) cD++;
                            if (double.Parse(s.mark_exam) < 3.95) cF++;
                        }
                    }
                    tcA += cA;
                    tcB += cB;
                    tcC += cC;
                    tcD += cD;
                    tcF += cF;
                    #region chi tiết
                    workSheet.Cells["A" + (row_max + 3)].Value = "A";
                    workSheet.Cells["B" + (row_max + 3)].Value = "8.45-10";
                    workSheet.Cells["C" + (row_max + 3)].Value = cA;
                    workSheet.Cells["D" + (row_max + 3)].Value = cA * 100 / temp.Count + " %";

                    workSheet.Cells["A" + (row_max + 4)].Value = "B";
                    workSheet.Cells["B" + (row_max + 4)].Value = "6.95-8.44";
                    workSheet.Cells["C" + (row_max + 4)].Value = cB;
                    workSheet.Cells["D" + (row_max + 4)].Value = cB * 100 / temp.Count + " %";

                    workSheet.Cells["A" + (row_max + 5)].Value = "C";
                    workSheet.Cells["B" + (row_max + 5)].Value = "5.45-6.94";
                    workSheet.Cells["C" + (row_max + 5)].Value = cC;
                    workSheet.Cells["D" + (row_max + 5)].Value = cC * 100 / temp.Count + " %";

                    workSheet.Cells["A" + (row_max + 6)].Value = "D";
                    workSheet.Cells["B" + (row_max + 6)].Value = "3.95-5.44";
                    workSheet.Cells["C" + (row_max + 6)].Value = cD;
                    workSheet.Cells["D" + (row_max + 6)].Value = cD * 100 / temp.Count + " %";

                    workSheet.Cells["A" + (row_max + 7)].Value = "F";
                    workSheet.Cells["B" + (row_max + 7)].Value = "0-3.94";
                    workSheet.Cells["C" + (row_max + 7)].Value = cF;
                    workSheet.Cells["D" + (row_max + 7)].Value = cF * 100 / temp.Count + " %";

                    var dd = DateTime.Today.ToString("dd");
                    var mm = DateTime.Today.ToString("MM");
                    var yyyy = DateTime.Today.ToString("yyyy");
                    workSheet.Cells["F" + (row_max + 10) + ":J" + (row_max + 10)].Merge = true;
                    workSheet.Cells["F" + (row_max + 10) + ":J" + (row_max + 10)].Value = "Hà Nội, ngày " + dd + " tháng " + mm + " năm " + yyyy;

                    workSheet.Cells["G" + (row_max + 11) + ":J" + (row_max + 11)].Merge = true;
                    workSheet.Cells["G" + (row_max + 11) + ":J" + (row_max + 11)].Style.Font.Bold = true;
                    workSheet.Cells["G" + (row_max + 11) + ":J" + (row_max + 11)].Value = "PHÒNG KHẢO THÍ & ĐBCL";
                    #endregion
                    count_ws++;
                    workSheet.Cells.AutoFitColumns();
                }
                #region Tổng hợp
                workSheetDefault.Cells["A" + (max + 1) + ":D" + (max + 1)].Merge = true;
                workSheetDefault.Cells["A" + (max + 1) + ":D" + (max + 1)].Value = "BẢNG THỐNG KÊ KÊT QUẢ THI";
                workSheetDefault.Cells["A" + (max + 2)].Value = "Điểm chữ";
                workSheetDefault.Cells["B" + (max + 2)].Value = "Điểm số";
                workSheetDefault.Cells["C" + (max + 2)].Value = "Số SV";
                workSheetDefault.Cells["D" + (max + 2)].Value = "Tỷ lệ %";
                workSheetDefault.Cells["A" + (max + 3)].Value = "A";
                workSheetDefault.Cells["B" + (max + 3)].Value = "8.45-10";
                workSheetDefault.Cells["C" + (max + 3)].Value = tcA;
                workSheetDefault.Cells["D" + (max + 3)].Value = tcA * 100 / total + " %";

                workSheetDefault.Cells["A" + (max + 4)].Value = "B";
                workSheetDefault.Cells["B" + (max + 4)].Value = "6.95-8.44";
                workSheetDefault.Cells["C" + (max + 4)].Value = tcB;
                workSheetDefault.Cells["D" + (max + 4)].Value = tcB * 100 / total + " %";

                workSheetDefault.Cells["A" + (max + 5)].Value = "C";
                workSheetDefault.Cells["B" + (max + 5)].Value = "5.45-6.94";
                workSheetDefault.Cells["C" + (max + 5)].Value = tcC;
                workSheetDefault.Cells["D" + (max + 5)].Value = tcC * 100 / total + " %";

                workSheetDefault.Cells["A" + (max + 6)].Value = "D";
                workSheetDefault.Cells["B" + (max + 6)].Value = "3.95-5.44";
                workSheetDefault.Cells["C" + (max + 6)].Value = tcD;
                workSheetDefault.Cells["D" + (max + 6)].Value = tcD * 100 / total + " %";

                workSheetDefault.Cells["A" + (max + 7)].Value = "F";
                workSheetDefault.Cells["B" + (max + 7)].Value = "0-3.94";
                workSheetDefault.Cells["C" + (max + 7)].Value = tcF;
                workSheetDefault.Cells["D" + (max + 7)].Value = tcF * 100 / total + " %";

                var d = DateTime.Today.ToString("dd");
                var m = DateTime.Today.ToString("MM");
                var yy = DateTime.Today.ToString("yyyy");
                workSheetDefault.Cells["F" + (max + 10) + ":J" + (max + 10)].Merge = true;
                workSheetDefault.Cells["F" + (max + 10) + ":J" + (max + 10)].Value = "Hà Nội, ngày " + d + " tháng " + m + " năm " + yy;

                workSheetDefault.Cells["G" + (max + 11) + ":J" + (max + 11)].Merge = true;
                workSheetDefault.Cells["G" + (max + 11) + ":J" + (max + 11)].Style.Font.Bold = true;
                workSheetDefault.Cells["G" + (max + 11) + ":J" + (max + 11)].Value = "PHÒNG KHẢO THÍ & ĐBCL";
                #endregion
                workSheetDefault.Cells.AutoFitColumns();
                var file = excelPackage.GetAsByteArray();
                excelPackage.Dispose();
                return file;
            }
        
        }

        public Byte[] ExportByClass(List<tbl_enrollment_class> list_gr, tbl_semester semester, tbl_subject subject)
        {
            using (var excelPackage = new ExcelPackage(new FileInfo("C:\\Users\\toank\\toan2k.xlsx")))
            {
                // Tạo author cho file Excel
                // Tạo title cho file Excel
                excelPackage.Workbook.Properties.Title = "Phổ điểm TLU";
                int count_ws = 0;
                var listResult = db.GetMarkByClass(list_gr.Select(s => s.id.ToString()).ToList(), subject.id, semester.id);

                excelPackage.Workbook.Worksheets.Add("Tổng hợp");
                ExcelWorksheet workSheetDefault = excelPackage.Workbook.Worksheets[count_ws];
                //sheet tổng hợp
                #region sheet tổng hợp
                workSheetDefault.Cells["A1:J1"].Merge = true;
                workSheetDefault.Cells["A1:J1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                workSheetDefault.Cells["A1:J1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheetDefault.Cells["A1"].Value = "TRƯỜNG ĐẠI HỌC THỦY LỢI";

                workSheetDefault.Cells["A2:J2"].Merge = true;
                workSheetDefault.Cells["A2:J2"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                workSheetDefault.Cells["A2:J2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheetDefault.Cells["A2"].Value = "PHÒNG KHẢO THÍ VÀ ĐẢM BẢO CHẤT LƯỢNG";

                workSheetDefault.Cells["A4:J4"].Merge = true;
                workSheetDefault.Cells["A4:J4"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                workSheetDefault.Cells["A4:J4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheetDefault.Cells["A4"].Value = "PHIẾU BÁO ĐIỂM - " + subject.subject_name;

                workSheetDefault.Cells["A5:J5"].Merge = true;
                workSheetDefault.Cells["A5:J5"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                workSheetDefault.Cells["A5:J5"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheetDefault.Cells["A5"].Value = "Học kỳ: " + semester.semester_name;

                workSheetDefault.Cells["A6:C6"].Merge = true;
                workSheetDefault.Cells["A6"].Value = "Môn: " + subject.subject_name;

                workSheetDefault.Cells["A7:B7"].Merge = true;
                workSheetDefault.Cells["A7"].Value = "Số tín chỉ: " + subject.number_of_credit;

                workSheetDefault.Cells["A9"].Value = "STT";
                workSheetDefault.Cells["B9"].Value = "Mã sinh viên";
                workSheetDefault.Cells["C9:D9"].Merge = true;
                workSheetDefault.Cells["C9"].Value = "Họ và tên";
                workSheetDefault.Cells["E9"].Value = "DQT";
                workSheetDefault.Cells["F9"].Value = "THI";
                workSheetDefault.Cells["G9"].Value = "TKHP";
                workSheetDefault.Cells["H9"].Value = "Chữ";
                workSheetDefault.Cells["I9"].Value = "Hệ 4";
                workSheetDefault.Cells["J9"].Value = "Ghi chú";

                workSheetDefault.Row(9).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheetDefault.Row(6).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                workSheetDefault.Row(7).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                workSheetDefault.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheetDefault.Column(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheetDefault.Column(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheetDefault.Column(4).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheetDefault.Column(5).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheetDefault.Column(6).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheetDefault.Column(7).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheetDefault.Column(8).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheetDefault.Column(9).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                workSheetDefault.Cells["A1:J9"].Style.Font.Bold = true;
                #endregion
                int index = 10;
                int tcA = 0;
                int tcB = 0;
                int tcC = 0;
                int tcD = 0;
                int tcF = 0;
                int max = index+2;
                int total = 0;
                //thêm các sheet chi tiết
                foreach (var item in listResult)
                {
                    excelPackage.Workbook.Worksheets.Add((count_ws + 1) + "-" + item.Key.className);
                    ExcelWorksheet workSheet = excelPackage.Workbook.Worksheets[count_ws + 1];
                    List<MarkBySemester> temp = item.ToList();
                    #region sheet chi tiết
                    workSheet.Cells["A1:J1"].Merge = true;
                    workSheet.Cells["A1:J1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    workSheet.Cells["A1:J1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells["A1"].Value = "TRƯỜNG ĐẠI HỌC THỦY LỢI";

                    workSheet.Cells["A2:J2"].Merge = true;
                    workSheet.Cells["A2:J2"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    workSheet.Cells["A2:J2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells["A2"].Value = "PHÒNG KHẢO THÍ VÀ ĐẢM BẢO CHẤT LƯỢNG";

                    workSheet.Cells["A4:J4"].Merge = true;
                    workSheet.Cells["A4:J4"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    workSheet.Cells["A4:J4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells["A4"].Value = "PHIẾU BÁO ĐIỂM - " + item.Key.className;

                    workSheet.Cells["A5:J5"].Merge = true;
                    workSheet.Cells["A5:J5"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    workSheet.Cells["A5:J5"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells["A5"].Value = "Học kỳ: " + semester.semester_name;

                    workSheet.Cells["A6:C6"].Merge = true;
                    workSheet.Cells["A6"].Value = "Môn: " + subject.subject_name;

                    workSheet.Cells["A7:B7"].Merge = true;
                    workSheet.Cells["A7"].Value = "Số tín chỉ: " + subject.number_of_credit;

                    workSheet.Cells["A9"].Value = "STT";
                    workSheet.Cells["B9"].Value = "Mã sinh viên";
                    workSheet.Cells["C9:D9"].Merge = true;
                    workSheet.Cells["C9"].Value = "Họ và tên";
                    workSheet.Cells["E9"].Value = "DQT";
                    workSheet.Cells["F9"].Value = "THI";
                    workSheet.Cells["G9"].Value = "TKHP";
                    workSheet.Cells["H9"].Value = "Chữ";
                    workSheet.Cells["I9"].Value = "Hệ 4";
                    workSheet.Cells["J9"].Value = "Ghi chú";

                    workSheet.Row(9).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Row(6).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    workSheet.Row(7).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    workSheet.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Column(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Column(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Column(4).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Column(5).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Column(6).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Column(7).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Column(8).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Column(9).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    workSheet.Cells["A1:J9"].Style.Font.Bold = true;
                    #endregion
                    max += temp.Count;
                    for (int i = 1; i <= temp.Count; i++)
                    {
                        #region chi tiết
                        int y = i + 9;
                        var name = temp.ElementAt(i - 1).student_name;
                        workSheet.Cells[y, 1].Value = i;
                        workSheet.Cells[y, 2].Value = temp.ElementAt(i - 1).student_code;
                        workSheet.Cells[y, 3].Value = name.Substring(0, name.LastIndexOf(" "));
                        workSheet.Cells[y, 4].Value = name.Substring(name.LastIndexOf(" ") + 1);
                        workSheet.Cells[y, 5].Value = temp.ElementAt(i - 1).mark;
                        workSheet.Cells[y, 6].Value = temp.ElementAt(i - 1).mark_exam;
                        workSheet.Cells[y, 7].Value = temp.ElementAt(i - 1).mark_final;
                        workSheet.Cells[y, 8].Value = temp.ElementAt(i - 1).gpa;
                        workSheet.Cells[y, 9].Value = temp.ElementAt(i - 1).mark_gpa;
                        workSheet.Cells[y, 10].Value = temp.ElementAt(i - 1).note;
                        #endregion
                        #region tổng hợp
                        index++;
                        name = temp.ElementAt(i - 1).student_name;
                        workSheetDefault.Cells[index, 1].Value = i;
                        workSheetDefault.Cells[index, 2].Value = temp.ElementAt(i - 1).student_code;
                        workSheetDefault.Cells[index, 3].Value = name.Substring(0, name.LastIndexOf(" "));
                        workSheetDefault.Cells[index, 4].Value = name.Substring(name.LastIndexOf(" ") + 1);
                        workSheetDefault.Cells[index, 5].Value = temp.ElementAt(i - 1).mark;
                        workSheetDefault.Cells[index, 6].Value = temp.ElementAt(i - 1).mark_exam;
                        workSheetDefault.Cells[index, 7].Value = temp.ElementAt(i - 1).mark_final;
                        workSheetDefault.Cells[index, 8].Value = temp.ElementAt(i - 1).gpa;
                        workSheetDefault.Cells[index, 9].Value = temp.ElementAt(i - 1).mark_gpa;
                        workSheetDefault.Cells[index, 10].Value = temp.ElementAt(i - 1).note;
                        #endregion

                    }

                    int row_max = temp.Count + 11;

                    workSheet.Cells["A" + (row_max + 1) + ":D" + (row_max + 1)].Merge = true;
                    workSheet.Cells["A" + (row_max + 1) + ":D" + (row_max + 1)].Value = "BẢNG THỐNG KÊ KÊT QUẢ THI";
                    workSheet.Cells["A" + (row_max + 2)].Value = "Điểm chữ";
                    workSheet.Cells["B" + (row_max + 2)].Value = "Điểm số";
                    workSheet.Cells["C" + (row_max + 2)].Value = "Số SV";
                    workSheet.Cells["D" + (row_max + 2)].Value = "Tỷ lệ %";
                    int cA = 0;
                    int cB = 0;
                    int cC = 0;
                    int cD = 0;
                    int cF = 0;
                    foreach (var s in temp)
                    {
                        if (s.status == 0)
                        {
                            total++;
                            if (double.Parse(s.mark_exam) <= 10 && double.Parse(s.mark_exam) >= 8.45) cA++;
                            if (double.Parse(s.mark_exam) <= 8.44 && double.Parse(s.mark_exam) >= 6.95) cB++;
                            if (double.Parse(s.mark_exam) <= 6.94 && double.Parse(s.mark_exam) >= 5.45) cC++;
                            if (double.Parse(s.mark_exam) <= 5.44 && double.Parse(s.mark_exam) >= 3.95) cD++;
                            if (double.Parse(s.mark_exam) < 3.95) cF++;
                        }
                    }
                    tcA += cA;
                    tcB += cB;
                    tcC += cC;
                    tcD += cD;
                    tcF += cF;
                    #region chi tiết
                    workSheet.Cells["A" + (row_max + 3)].Value = "A";
                    workSheet.Cells["B" + (row_max + 3)].Value = "8.45-10";
                    workSheet.Cells["C" + (row_max + 3)].Value = cA;
                    workSheet.Cells["D" + (row_max + 3)].Value = cA * 100 / temp.Count + " %";

                    workSheet.Cells["A" + (row_max + 4)].Value = "B";
                    workSheet.Cells["B" + (row_max + 4)].Value = "6.95-8.44";
                    workSheet.Cells["C" + (row_max + 4)].Value = cB;
                    workSheet.Cells["D" + (row_max + 4)].Value = cB * 100 / temp.Count + " %";

                    workSheet.Cells["A" + (row_max + 5)].Value = "C";
                    workSheet.Cells["B" + (row_max + 5)].Value = "5.45-6.94";
                    workSheet.Cells["C" + (row_max + 5)].Value = cC;
                    workSheet.Cells["D" + (row_max + 5)].Value = cC * 100 / temp.Count + " %";

                    workSheet.Cells["A" + (row_max + 6)].Value = "D";
                    workSheet.Cells["B" + (row_max + 6)].Value = "3.95-5.44";
                    workSheet.Cells["C" + (row_max + 6)].Value = cD;
                    workSheet.Cells["D" + (row_max + 6)].Value = cD * 100 / temp.Count + " %";

                    workSheet.Cells["A" + (row_max + 7)].Value = "F";
                    workSheet.Cells["B" + (row_max + 7)].Value = "0-3.94";
                    workSheet.Cells["C" + (row_max + 7)].Value = cF;
                    workSheet.Cells["D" + (row_max + 7)].Value = cF * 100 / temp.Count + " %";

                    var dd = DateTime.Today.ToString("dd");
                    var mm = DateTime.Today.ToString("MM");
                    var yyyy = DateTime.Today.ToString("yyyy");
                    workSheet.Cells["F" + (row_max + 10) + ":J" + (row_max + 10)].Merge = true;
                    workSheet.Cells["F" + (row_max + 10) + ":J" + (row_max + 10)].Value = "Hà Nội, ngày " + dd + " tháng " + mm + " năm " + yyyy;

                    workSheet.Cells["G" + (row_max + 11) + ":J" + (row_max + 11)].Merge = true;
                    workSheet.Cells["G" + (row_max + 11) + ":J" + (row_max + 11)].Style.Font.Bold = true;
                    workSheet.Cells["G" + (row_max + 11) + ":J" + (row_max + 11)].Value = "PHÒNG KHẢO THÍ & ĐBCL";
                    #endregion
                    count_ws++;
                    workSheet.Cells.AutoFitColumns();
                }
                #region Tổng hợp
                workSheetDefault.Cells["A" + (max + 1) + ":D" + (max + 1)].Merge = true;
                workSheetDefault.Cells["A" + (max + 1) + ":D" + (max + 1)].Value = "BẢNG THỐNG KÊ KÊT QUẢ THI";
                workSheetDefault.Cells["A" + (max + 2)].Value = "Điểm chữ";
                workSheetDefault.Cells["B" + (max + 2)].Value = "Điểm số";
                workSheetDefault.Cells["C" + (max + 2)].Value = "Số SV";
                workSheetDefault.Cells["D" + (max + 2)].Value = "Tỷ lệ %";
                workSheetDefault.Cells["A" + (max + 3)].Value = "A";
                workSheetDefault.Cells["B" + (max + 3)].Value = "8.45-10";
                workSheetDefault.Cells["C" + (max + 3)].Value = tcA;
                workSheetDefault.Cells["D" + (max + 3)].Value = tcA * 100 / total + " %";

                workSheetDefault.Cells["A" + (max + 4)].Value = "B";
                workSheetDefault.Cells["B" + (max + 4)].Value = "6.95-8.44";
                workSheetDefault.Cells["C" + (max + 4)].Value = tcB;
                workSheetDefault.Cells["D" + (max + 4)].Value = tcB * 100 / total + " %";

                workSheetDefault.Cells["A" + (max + 5)].Value = "C";
                workSheetDefault.Cells["B" + (max + 5)].Value = "5.45-6.94";
                workSheetDefault.Cells["C" + (max + 5)].Value = tcC;
                workSheetDefault.Cells["D" + (max + 5)].Value = tcC * 100 / total + " %";

                workSheetDefault.Cells["A" + (max + 6)].Value = "D";
                workSheetDefault.Cells["B" + (max + 6)].Value = "3.95-5.44";
                workSheetDefault.Cells["C" + (max + 6)].Value = tcD;
                workSheetDefault.Cells["D" + (max + 6)].Value = tcD * 100 / total + " %";

                workSheetDefault.Cells["A" + (max + 7)].Value = "F";
                workSheetDefault.Cells["B" + (max + 7)].Value = "0-3.94";
                workSheetDefault.Cells["C" + (max + 7)].Value = tcF;
                workSheetDefault.Cells["D" + (max + 7)].Value = tcF * 100 / total + " %";

                var d = DateTime.Today.ToString("dd");
                var m = DateTime.Today.ToString("MM");
                var yy = DateTime.Today.ToString("yyyy");
                workSheetDefault.Cells["F" + (max + 10) + ":J" + (max + 10)].Merge = true;
                workSheetDefault.Cells["F" + (max + 10) + ":J" + (max + 10)].Value = "Hà Nội, ngày " + d + " tháng " + m + " năm " + yy;

                workSheetDefault.Cells["G" + (max + 11) + ":J" + (max + 11)].Merge = true;
                workSheetDefault.Cells["G" + (max + 11) + ":J" + (max + 11)].Style.Font.Bold = true;
                workSheetDefault.Cells["G" + (max + 11) + ":J" + (max + 11)].Value = "PHÒNG KHẢO THÍ & ĐBCL";
                #endregion
                workSheetDefault.Cells.AutoFitColumns();
                var file = excelPackage.GetAsByteArray();
                excelPackage.Dispose();
                return file;
            }

        }

        public Byte[] ExportExcelDataCourseSubject(string markOption, string subjectName, long numberOfCredit, long startYear, long EndYear, List<Data> dataMark)
        {
            MemoryStream stream = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                package.Workbook.Properties.Title = "Phổ điểm TLU";
                var sheet = package.Workbook.Worksheets.Add($"{markOption}");

                sheet.Cells["A1:E1"].Merge = true;
                sheet.Cells["A1"].Value = "TRƯỜNG ĐẠI HỌC THỦY LỢI";

                sheet.Cells["A2:E2"].Merge = true;
                sheet.Cells["A2"].Value = "PHÒNG KHẢO THÍ VÀ ĐẢM BẢO CHẤT LƯỢNG";

                sheet.Cells["A4:O4"].Merge = true;
                sheet.Cells["A4"].Value = "MÔN: " + subjectName.ToUpper();

                sheet.Cells["A5:O5"].Merge = true;
                sheet.Cells["A5"].Value = markOption.ToUpper() + " " + startYear.ToString() + "-" + EndYear.ToString();

                sheet.Cells["A6:O6"].Merge = true;
                sheet.Cells["A6"].Value = "SỐ TÍN CHỈ: " + numberOfCredit.ToString();

                int num = 8;
                sheet.Cells["A" + num.ToString() + ":A" + (num + 1).ToString()].Merge = true;
                sheet.Cells["A" + (num).ToString()].Value = "STT";
                sheet.Cells["B" + num.ToString() + ":B" + (num + 1).ToString()].Merge = true;
                sheet.Cells["B" + (num).ToString()].Value = "Nhóm môn học";
                sheet.Cells["C" + num.ToString() + ":C" + (num + 1).ToString()].Merge = true;
                sheet.Cells["C" + (num).ToString()].Value = "Tên giáo viên";
                sheet.Cells["D" + num.ToString() + ":D" + (num + 1).ToString()].Merge = true;
                sheet.Cells["D" + (num).ToString()].Value = "Tổng số điểm";

                sheet.Cells["E" + num.ToString() + ":F" + (num).ToString()].Merge = true;
                sheet.Cells["E" + (num).ToString()].Value = "Điểm A";
                sheet.Cells["E" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["F" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm A";

                sheet.Cells["G" + num.ToString() + ":H" + (num).ToString()].Merge = true;
                sheet.Cells["G" + num.ToString()].Value = "Điểm B";
                sheet.Cells["G" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["H" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm B";

                sheet.Cells["I" + num.ToString() + ":J" + (num).ToString()].Merge = true;
                sheet.Cells["I" + num.ToString()].Value = "Điểm C";
                sheet.Cells["I" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["J" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm C";

                sheet.Cells["K" + num.ToString() + ":L" + (num).ToString()].Merge = true;
                sheet.Cells["K" + num.ToString()].Value = "Điểm D";
                sheet.Cells["K" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["L" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm D";

                sheet.Cells["M" + num.ToString() + ":N" + (num).ToString()].Merge = true;
                sheet.Cells["M" + num.ToString()].Value = "Điểm F";
                sheet.Cells["M" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["N" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm F";

                sheet.Cells["O" + num.ToString() + ":O" + (num + 1).ToString()].Merge = true;
                sheet.Cells["O" + num.ToString()].Value = "Ghi chú";

                sheet.Cells["A1:O" + (num + 1).ToString()].Style.Font.Bold = true;

                int rowInd = num + 2;
                foreach (var item in dataMark)
                {
                    sheet.Cells[rowInd, 1].Value = item.stt;
                    sheet.Cells[rowInd, 2].Value = item.courseSubjectName;
                    sheet.Cells[rowInd, 3].Value = item.teacherName;
                    sheet.Cells[rowInd, 4].Value = item.sum;
                    sheet.Cells[rowInd, 5].Value = item.A;
                    sheet.Cells[rowInd, 6].Value = item.rateA;
                    sheet.Cells[rowInd, 7].Value = item.B;
                    sheet.Cells[rowInd, 8].Value = item.rateB;
                    sheet.Cells[rowInd, 9].Value = item.C;
                    sheet.Cells[rowInd, 10].Value = item.rateC;
                    sheet.Cells[rowInd, 11].Value = item.D;
                    sheet.Cells[rowInd, 12].Value = item.rateD;
                    sheet.Cells[rowInd, 13].Value = item.F;
                    sheet.Cells[rowInd, 14].Value = item.rateF;
                    rowInd++;
                }
                rowInd += 1;
                sheet.Cells["K" + (rowInd).ToString() + ":L" + (rowInd).ToString()].Merge = true;
                sheet.Cells["K" + (rowInd).ToString() + ":L" + (rowInd).ToString()].Value = "KÝ TÊN";
                sheet.Cells["K" + (rowInd + 1).ToString() + ":L" + (rowInd + 1).ToString()].Merge = true;
                sheet.Cells["K" + (rowInd + 1).ToString() + ":L" + (rowInd + 1).ToString()].Value = "PHÒNG KHẢO THÍ";
                sheet.Cells["K" + (rowInd).ToString() + ":L" + (rowInd + 1).ToString()].Style.Font.Bold = true;
                rowInd++;
                sheet.Cells["A1:P" + rowInd.ToString()].AutoFitColumns();
                sheet.Cells["A1:P" + rowInd.ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells["A1:P" + rowInd.ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                package.Save();
                var file = package.GetAsByteArray();
                package.Dispose();
                return file;
            }
        }

        public Byte[] ExportExcelDataTeacher(string markOption, string subjectName, long numberOfCredit, long startYear, long EndYear, List<MarkRate> dataMark,int val)
        {
            string column = "Môn học";
            if (val == 1)
            {
                column = "Môn học";
            }
            else column = "Năm học";
            MemoryStream stream = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                package.Workbook.Properties.Title = "Phổ điểm TLU";
                var sheet = package.Workbook.Worksheets.Add($"{markOption}");

                sheet.Cells["A1:E1"].Merge = true;
                sheet.Cells["A1"].Value = "TRƯỜNG ĐẠI HỌC THỦY LỢI";

                sheet.Cells["A2:E2"].Merge = true;
                sheet.Cells["A2"].Value = "PHÒNG KHẢO THÍ VÀ ĐẢM BẢO CHẤT LƯỢNG";

                sheet.Cells["A4:O4"].Merge = true;
                sheet.Cells["A4"].Value = "MÔN: " + subjectName.ToUpper();

                sheet.Cells["A5:O5"].Merge = true;
                sheet.Cells["A5"].Value = markOption.ToUpper() + " " + startYear.ToString() + "-" + EndYear.ToString();

                sheet.Cells["A6:O6"].Merge = true;
                sheet.Cells["A6"].Value = "SỐ TÍN CHỈ: " + numberOfCredit.ToString();

                int num = 8;
                sheet.Cells["A" + num.ToString() + ":A" + (num + 1).ToString()].Merge = true;
                sheet.Cells["A" + (num).ToString()].Value = "STT";

                sheet.Cells["B" + num.ToString() + ":B" + (num + 1).ToString()].Merge = true;
                sheet.Cells["B" + (num).ToString()].Value = column;

                sheet.Cells["C" + num.ToString() + ":C" + (num + 1).ToString()].Merge = true;
                sheet.Cells["C" + (num).ToString()].Value = "Tên giáo viên";

                sheet.Cells["D" + num.ToString() + ":D" + (num + 1).ToString()].Merge = true;
                sheet.Cells["D" + (num).ToString()].Value = "Tổng số điểm";

                sheet.Cells["E" + num.ToString() + ":F" + (num).ToString()].Merge = true;
                sheet.Cells["E" + (num).ToString()].Value = "Điểm A";
                sheet.Cells["E" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["F" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm A";

                sheet.Cells["G" + num.ToString() + ":H" + (num).ToString()].Merge = true;
                sheet.Cells["G" + num.ToString()].Value = "Điểm B";
                sheet.Cells["G" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["H" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm B";

                sheet.Cells["I" + num.ToString() + ":J" + (num).ToString()].Merge = true;
                sheet.Cells["I" + num.ToString()].Value = "Điểm C";
                sheet.Cells["I" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["J" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm C";

                sheet.Cells["K" + num.ToString() + ":L" + (num).ToString()].Merge = true;
                sheet.Cells["K" + num.ToString()].Value = "Điểm D";
                sheet.Cells["K" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["L" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm D";

                sheet.Cells["M" + num.ToString() + ":N" + (num).ToString()].Merge = true;
                sheet.Cells["M" + num.ToString()].Value = "Điểm F";
                sheet.Cells["M" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["N" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm F";

                sheet.Cells["O" + num.ToString() + ":O" + (num + 1).ToString()].Merge = true;
                sheet.Cells["O" + num.ToString()].Value = "Ghi chú";

                sheet.Cells["A1:O" + (num + 1).ToString()].Style.Font.Bold = true;

                int rowInd = num + 2;
                foreach (var item in dataMark)
                {
                    sheet.Cells[rowInd, 1].Value = item.stt;
                    if(val == 1)
                    {
                        sheet.Cells[rowInd, 2].Value = subjectName;
                    }
                    else sheet.Cells[rowInd, 2].Value = item.year;
                    sheet.Cells[rowInd, 3].Value = item.teacherName;
                    sheet.Cells[rowInd, 4].Value = item.sum;
                    sheet.Cells[rowInd, 5].Value = item.A;
                    sheet.Cells[rowInd, 6].Value = item.rateA;
                    sheet.Cells[rowInd, 7].Value = item.B;
                    sheet.Cells[rowInd, 8].Value = item.rateB;
                    sheet.Cells[rowInd, 9].Value = item.C;
                    sheet.Cells[rowInd, 10].Value = item.rateC;
                    sheet.Cells[rowInd, 11].Value = item.D;
                    sheet.Cells[rowInd, 12].Value = item.rateD;
                    sheet.Cells[rowInd, 13].Value = item.F;
                    sheet.Cells[rowInd, 14].Value = item.rateF;
                    rowInd++;
                }

                rowInd += 1;
                sheet.Cells["K" + (rowInd).ToString() + ":L" + (rowInd).ToString()].Merge = true;
                sheet.Cells["K" + (rowInd).ToString() + ":L" + (rowInd).ToString()].Value = "KÝ TÊN";
                sheet.Cells["K" + (rowInd + 1).ToString() + ":L" + (rowInd + 1).ToString()].Merge = true;
                sheet.Cells["K" + (rowInd + 1).ToString() + ":L" + (rowInd + 1).ToString()].Value = "PHÒNG KHẢO THÍ";
                sheet.Cells["K" + (rowInd).ToString() + ":L" + (rowInd + 1).ToString()].Style.Font.Bold = true;
                rowInd++;

                sheet.Cells["A1:P" + rowInd.ToString()].AutoFitColumns();
                sheet.Cells["A1:P" + rowInd.ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells["A1:P" + rowInd.ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                package.Save();
                var file = package.GetAsByteArray();
                package.Dispose();
                return file;
            }
        }
        public Byte[] ExportExcelDataTeacher(string markOption,string teacherName,string semesterName,string courseYearName, List<MarkRate> dataMark)
        {
            MemoryStream stream = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                package.Workbook.Properties.Title = "Phổ điểm TLU";
                var sheet = package.Workbook.Worksheets.Add($"{markOption}");

                sheet.Cells["A1:E1"].Merge = true;
                sheet.Cells["A1"].Value = "TRƯỜNG ĐẠI HỌC THỦY LỢI";

                sheet.Cells["A2:E2"].Merge = true;
                sheet.Cells["A2"].Value = "PHÒNG KHẢO THÍ VÀ ĐẢM BẢO CHẤT LƯỢNG";

                sheet.Cells["A4:O4"].Merge = true;
                sheet.Cells["A4"].Value = "GIÁO VIÊN: " + teacherName.ToUpper();

                sheet.Cells["A5:O5"].Merge = true;
                sheet.Cells["A5"].Value = markOption.ToUpper();

                sheet.Cells["A6:O6"].Merge = true;
                sheet.Cells["A6"].Value = "Học kỳ: " + semesterName.ToUpper();

                sheet.Cells["A7:O7"].Merge = true;
                sheet.Cells["A7"].Value = "Khóa học: " + courseYearName.ToUpper();

                int num = 9;
                sheet.Cells["A" + num.ToString() + ":A" + (num + 1).ToString()].Merge = true;
                sheet.Cells["A" + (num).ToString()].Value = "STT";

                sheet.Cells["B" + num.ToString() + ":B" + (num + 1).ToString()].Merge = true;
                sheet.Cells["B" + (num).ToString()].Value = "Tên môn học";

                sheet.Cells["C" + num.ToString() + ":C" + (num + 1).ToString()].Merge = true;
                sheet.Cells["C" + (num).ToString()].Value = "Lớp học phần";

                sheet.Cells["D" + num.ToString() + ":D" + (num + 1).ToString()].Merge = true;
                sheet.Cells["D" + (num).ToString()].Value = "Tổng số điểm";

                sheet.Cells["E" + num.ToString() + ":F" + (num).ToString()].Merge = true;
                sheet.Cells["E" + (num).ToString()].Value = "Điểm A";
                sheet.Cells["E" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["F" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm A";

                sheet.Cells["G" + num.ToString() + ":H" + (num).ToString()].Merge = true;
                sheet.Cells["G" + num.ToString()].Value = "Điểm B";
                sheet.Cells["G" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["H" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm B";

                sheet.Cells["I" + num.ToString() + ":J" + (num).ToString()].Merge = true;
                sheet.Cells["I" + num.ToString()].Value = "Điểm C";
                sheet.Cells["I" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["J" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm C";

                sheet.Cells["K" + num.ToString() + ":L" + (num).ToString()].Merge = true;
                sheet.Cells["K" + num.ToString()].Value = "Điểm D";
                sheet.Cells["K" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["L" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm D";

                sheet.Cells["M" + num.ToString() + ":N" + (num).ToString()].Merge = true;
                sheet.Cells["M" + num.ToString()].Value = "Điểm F";
                sheet.Cells["M" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["N" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm F";

                sheet.Cells["O" + num.ToString() + ":O" + (num + 1).ToString()].Merge = true;
                sheet.Cells["O" + num.ToString()].Value = "Ghi chú";

                sheet.Cells["A1:O" + (num + 1).ToString()].Style.Font.Bold = true;

                int rowInd = num + 2;
                foreach (MarkRate item in dataMark)
                {
                    sheet.Cells[rowInd, 1].Value = item.stt;
                    sheet.Cells[rowInd, 2].Value = item.subjectName;
                    sheet.Cells[rowInd, 3].Value = item.courseSubjectName;
                    sheet.Cells[rowInd, 4].Value = item.sum;
                    sheet.Cells[rowInd, 5].Value = item.A;
                    sheet.Cells[rowInd, 6].Value = item.rateA;
                    sheet.Cells[rowInd, 7].Value = item.B;
                    sheet.Cells[rowInd, 8].Value = item.rateB;
                    sheet.Cells[rowInd, 9].Value = item.C;
                    sheet.Cells[rowInd, 10].Value = item.rateC;
                    sheet.Cells[rowInd, 11].Value = item.D;
                    sheet.Cells[rowInd, 12].Value = item.rateD;
                    sheet.Cells[rowInd, 13].Value = item.F;
                    sheet.Cells[rowInd, 14].Value = item.rateF;
                    rowInd++;
                }

                rowInd += 1;
                sheet.Cells["K" + (rowInd).ToString() + ":L" + (rowInd).ToString()].Merge = true;
                sheet.Cells["K" + (rowInd).ToString() + ":L" + (rowInd).ToString()].Value = "KÝ TÊN";
                sheet.Cells["K" + (rowInd + 1).ToString() + ":L" + (rowInd + 1).ToString()].Merge = true;
                sheet.Cells["K" + (rowInd + 1).ToString() + ":L" + (rowInd + 1).ToString()].Value = "PHÒNG KHẢO THÍ";
                sheet.Cells["K" + (rowInd).ToString() + ":L" + (rowInd + 1).ToString()].Style.Font.Bold = true;
                rowInd++;

                sheet.Cells["A1:P" + rowInd.ToString()].AutoFitColumns();
                sheet.Cells["A1:P" + rowInd.ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells["A1:P" + rowInd.ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                package.Save();
                var file = package.GetAsByteArray();
                package.Dispose();
                return file;
            }
        }


        public Byte[] ExportExcelDataEnrollmentClass(string markOption, string subjectName, long numberOfCredit, long startYear, long EndYear, List<MarksByEnrollmentClass> dataMark)
        {
            MemoryStream stream = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                package.Workbook.Properties.Title = "Phổ điểm TLU";
                var sheet = package.Workbook.Worksheets.Add($"{markOption}");

                sheet.Cells["A1:E1"].Merge = true;
                sheet.Cells["A1"].Value = "TRƯỜNG ĐẠI HỌC THỦY LỢI";

                sheet.Cells["A2:E2"].Merge = true;
                sheet.Cells["A2"].Value = "PHÒNG KHẢO THÍ VÀ ĐẢM BẢO CHẤT LƯỢNG";

                sheet.Cells["A4:O4"].Merge = true;
                sheet.Cells["A4"].Value = "MÔN: " + subjectName.ToUpper();

                sheet.Cells["A5:O5"].Merge = true;
                sheet.Cells["A5"].Value = markOption.ToUpper() + " " + startYear.ToString() + "-" + EndYear.ToString();

                sheet.Cells["A6:O6"].Merge = true;
                sheet.Cells["A6"].Value = "SỐ TÍN CHỈ: " + numberOfCredit.ToString();

                int num = 8;
                sheet.Cells["A" + num.ToString() + ":A" + (num + 1).ToString()].Merge = true;
                sheet.Cells["A" + (num).ToString()].Value = "STT";

                sheet.Cells["B" + num.ToString() + ":B" + (num + 1).ToString()].Merge = true;
                sheet.Cells["B" + (num).ToString()].Value = "Môn học";

                sheet.Cells["C" + num.ToString() + ":C" + (num + 1).ToString()].Merge = true;
                sheet.Cells["C" + (num).ToString()].Value = "Lớp quản lý";

                sheet.Cells["D" + num.ToString() + ":D" + (num + 1).ToString()].Merge = true;
                sheet.Cells["D" + (num).ToString()].Value = "Tổng số điểm";

                sheet.Cells["E" + num.ToString() + ":F" + (num).ToString()].Merge = true;
                sheet.Cells["E" + (num).ToString()].Value = "Điểm A";
                sheet.Cells["E" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["F" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm A";

                sheet.Cells["G" + num.ToString() + ":H" + (num).ToString()].Merge = true;
                sheet.Cells["G" + num.ToString()].Value = "Điểm B";
                sheet.Cells["G" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["H" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm B";

                sheet.Cells["I" + num.ToString() + ":J" + (num).ToString()].Merge = true;
                sheet.Cells["I" + num.ToString()].Value = "Điểm C";
                sheet.Cells["I" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["J" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm C";

                sheet.Cells["K" + num.ToString() + ":L" + (num).ToString()].Merge = true;
                sheet.Cells["K" + num.ToString()].Value = "Điểm D";
                sheet.Cells["K" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["L" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm D";

                sheet.Cells["M" + num.ToString() + ":N" + (num).ToString()].Merge = true;
                sheet.Cells["M" + num.ToString()].Value = "Điểm F";
                sheet.Cells["M" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["N" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm F";

                sheet.Cells["O" + num.ToString() + ":O" + (num + 1).ToString()].Merge = true;
                sheet.Cells["O" + num.ToString()].Value = "Ghi chú";

                sheet.Cells["A1:O" + (num + 1).ToString()].Style.Font.Bold = true;

                int rowInd = num + 2;
                foreach (var item in dataMark)
                {
                    sheet.Cells[rowInd, 1].Value = item.stt;
                    sheet.Cells[rowInd, 2].Value = item.subjectName;
                    sheet.Cells[rowInd, 3].Value = item.enrollmentClassName;
                    sheet.Cells[rowInd, 4].Value = item.sum;
                    sheet.Cells[rowInd, 5].Value = item.A;
                    sheet.Cells[rowInd, 6].Value = item.rateA;
                    sheet.Cells[rowInd, 7].Value = item.B;
                    sheet.Cells[rowInd, 8].Value = item.rateB;
                    sheet.Cells[rowInd, 9].Value = item.C;
                    sheet.Cells[rowInd, 10].Value = item.rateC;
                    sheet.Cells[rowInd, 11].Value = item.D;
                    sheet.Cells[rowInd, 12].Value = item.rateD;
                    sheet.Cells[rowInd, 13].Value = item.F;
                    sheet.Cells[rowInd, 14].Value = item.rateF;
                    rowInd++;
                }

                rowInd += 1;
                sheet.Cells["K" + (rowInd).ToString() + ":L" + (rowInd).ToString()].Merge = true;
                sheet.Cells["K" + (rowInd).ToString() + ":L" + (rowInd).ToString()].Value = "KÝ TÊN";
                sheet.Cells["K" + (rowInd + 1).ToString() + ":L" + (rowInd + 1).ToString()].Merge = true;
                sheet.Cells["K" + (rowInd + 1).ToString() + ":L" + (rowInd + 1).ToString()].Value = "PHÒNG KHẢO THÍ";
                sheet.Cells["K" + (rowInd).ToString() + ":L" + (rowInd + 1).ToString()].Style.Font.Bold = true;
                rowInd++;

                sheet.Cells["A1:P" + rowInd.ToString()].AutoFitColumns();
                sheet.Cells["A1:P" + rowInd.ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells["A1:P" + rowInd.ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                package.Save();
                var file = package.GetAsByteArray();
                package.Dispose();
                return file;
            }
        }
        public Byte[] ExportExcelDataDepartment(string markOption, string subjectName, long numberOfCredit,string departmentName, long startYear, long EndYear, List<MarkByDepartment> dataMark)
        {
            MemoryStream stream = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                package.Workbook.Properties.Title = "Phổ điểm TLU";
                var sheet = package.Workbook.Worksheets.Add($"{markOption}");

                sheet.Cells["A1:E1"].Merge = true;
                sheet.Cells["A1"].Value = "TRƯỜNG ĐẠI HỌC THỦY LỢI";

                sheet.Cells["A2:E2"].Merge = true;
                sheet.Cells["A2"].Value = "PHÒNG KHẢO THÍ VÀ ĐẢM BẢO CHẤT LƯỢNG";

                sheet.Cells["A4:O4"].Merge = true;
                sheet.Cells["A4"].Value = "MÔN: " + subjectName.ToUpper();

                sheet.Cells["A5:O5"].Merge = true;
                sheet.Cells["A5"].Value = markOption.ToUpper() + " " + startYear.ToString() + "-" + EndYear.ToString();

                sheet.Cells["A6:O6"].Merge = true;
                sheet.Cells["A6"].Value = "SỐ TÍN CHỈ: " + numberOfCredit.ToString();

                int num = 8;
                sheet.Cells["A" + num.ToString() + ":A" + (num + 1).ToString()].Merge = true;
                sheet.Cells["A" + (num).ToString()].Value = "STT";

                sheet.Cells["B" + num.ToString() + ":B" + (num + 1).ToString()].Merge = true;
                sheet.Cells["B" + (num).ToString()].Value = "Năm học";

                sheet.Cells["C" + num.ToString() + ":C" + (num + 1).ToString()].Merge = true;
                sheet.Cells["C" + (num).ToString()].Value = "Khoa";

                sheet.Cells["D" + num.ToString() + ":D" + (num + 1).ToString()].Merge = true;
                sheet.Cells["D" + (num).ToString()].Value = "Tổng số điểm";

                sheet.Cells["E" + num.ToString() + ":F" + (num).ToString()].Merge = true;
                sheet.Cells["E" + (num).ToString()].Value = "Điểm A";
                sheet.Cells["E" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["F" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm A";

                sheet.Cells["G" + num.ToString() + ":H" + (num).ToString()].Merge = true;
                sheet.Cells["G" + num.ToString()].Value = "Điểm B";
                sheet.Cells["G" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["H" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm B";

                sheet.Cells["I" + num.ToString() + ":J" + (num).ToString()].Merge = true;
                sheet.Cells["I" + num.ToString()].Value = "Điểm C";
                sheet.Cells["I" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["J" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm C";

                sheet.Cells["K" + num.ToString() + ":L" + (num).ToString()].Merge = true;
                sheet.Cells["K" + num.ToString()].Value = "Điểm D";
                sheet.Cells["K" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["L" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm D";

                sheet.Cells["M" + num.ToString() + ":N" + (num).ToString()].Merge = true;
                sheet.Cells["M" + num.ToString()].Value = "Điểm F";
                sheet.Cells["M" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["N" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm F";

                sheet.Cells["O" + num.ToString() + ":O" + (num + 1).ToString()].Merge = true;
                sheet.Cells["O" + num.ToString()].Value = "Ghi chú";

                sheet.Cells["A1:O" + (num + 1).ToString()].Style.Font.Bold = true;

                int rowInd = num + 2;
                foreach (var item in dataMark)
                {
                    sheet.Cells[rowInd, 1].Value = item.stt;
                    sheet.Cells[rowInd, 2].Value = item.year;
                    sheet.Cells[rowInd, 3].Value = item.departmentName;
                    sheet.Cells[rowInd, 4].Value = item.sum;
                    sheet.Cells[rowInd, 5].Value = item.A;
                    sheet.Cells[rowInd, 6].Value = item.rateA;
                    sheet.Cells[rowInd, 7].Value = item.B;
                    sheet.Cells[rowInd, 8].Value = item.rateB;
                    sheet.Cells[rowInd, 9].Value = item.C;
                    sheet.Cells[rowInd, 10].Value = item.rateC;
                    sheet.Cells[rowInd, 11].Value = item.D;
                    sheet.Cells[rowInd, 12].Value = item.rateD;
                    sheet.Cells[rowInd, 13].Value = item.F;
                    sheet.Cells[rowInd, 14].Value = item.rateF;
                    rowInd++;
                }

                rowInd += 1;
                sheet.Cells["K" + (rowInd).ToString() + ":L" + (rowInd).ToString()].Merge = true;
                sheet.Cells["K" + (rowInd).ToString() + ":L" + (rowInd).ToString()].Value = "KÝ TÊN";
                sheet.Cells["K" + (rowInd + 1).ToString() + ":L" + (rowInd + 1).ToString()].Merge = true;
                sheet.Cells["K" + (rowInd + 1).ToString() + ":L" + (rowInd + 1).ToString()].Value = "PHÒNG KHẢO THÍ";
                sheet.Cells["K" + (rowInd).ToString() + ":L" + (rowInd + 1).ToString()].Style.Font.Bold = true;
                rowInd++;

                sheet.Cells["A1:P" + rowInd.ToString()].AutoFitColumns();
                sheet.Cells["A1:P" + rowInd.ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells["A1:P" + rowInd.ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                package.Save();
                var file = package.GetAsByteArray();
                package.Dispose();
                return file;
            }
        }
        public Byte[] ExportExcelDataDepartment(string markOption, string subjectName, long numberOfCredit, long startYear, long EndYear, List<MarkByDepartment> dataMark)
        {
            MemoryStream stream = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                package.Workbook.Properties.Title = "Phổ điểm TLU";
                var sheet = package.Workbook.Worksheets.Add($"{markOption}");

                sheet.Cells["A1:E1"].Merge = true;
                sheet.Cells["A1"].Value = "TRƯỜNG ĐẠI HỌC THỦY LỢI";

                sheet.Cells["A2:E2"].Merge = true;
                sheet.Cells["A2"].Value = "PHÒNG KHẢO THÍ VÀ ĐẢM BẢO CHẤT LƯỢNG";

                sheet.Cells["A4:O4"].Merge = true;
                sheet.Cells["A4"].Value = "MÔN: " + subjectName.ToUpper();

                sheet.Cells["A5:O5"].Merge = true;
                sheet.Cells["A5"].Value = markOption.ToUpper() + " " + startYear.ToString() + "-" + EndYear.ToString();

                sheet.Cells["A6:O6"].Merge = true;
                sheet.Cells["A6"].Value = "SỐ TÍN CHỈ: " + numberOfCredit.ToString();

                int num = 8;
                sheet.Cells["A" + num.ToString() + ":A" + (num + 1).ToString()].Merge = true;
                sheet.Cells["A" + (num).ToString()].Value = "STT";

                sheet.Cells["B" + num.ToString() + ":B" + (num + 1).ToString()].Merge = true;
                sheet.Cells["B" + (num).ToString()].Value = "Môn học";

                sheet.Cells["C" + num.ToString() + ":C" + (num + 1).ToString()].Merge = true;
                sheet.Cells["C" + (num).ToString()].Value = "Khoa";

                sheet.Cells["D" + num.ToString() + ":D" + (num + 1).ToString()].Merge = true;
                sheet.Cells["D" + (num).ToString()].Value = "Tổng số điểm";

                sheet.Cells["E" + num.ToString() + ":F" + (num).ToString()].Merge = true;
                sheet.Cells["E" + (num).ToString()].Value = "Điểm A";
                sheet.Cells["E" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["F" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm A";

                sheet.Cells["G" + num.ToString() + ":H" + (num).ToString()].Merge = true;
                sheet.Cells["G" + num.ToString()].Value = "Điểm B";
                sheet.Cells["G" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["H" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm B";

                sheet.Cells["I" + num.ToString() + ":J" + (num).ToString()].Merge = true;
                sheet.Cells["I" + num.ToString()].Value = "Điểm C";
                sheet.Cells["I" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["J" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm C";

                sheet.Cells["K" + num.ToString() + ":L" + (num).ToString()].Merge = true;
                sheet.Cells["K" + num.ToString()].Value = "Điểm D";
                sheet.Cells["K" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["L" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm D";

                sheet.Cells["M" + num.ToString() + ":N" + (num).ToString()].Merge = true;
                sheet.Cells["M" + num.ToString()].Value = "Điểm F";
                sheet.Cells["M" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["N" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm F";

                sheet.Cells["O" + num.ToString() + ":O" + (num + 1).ToString()].Merge = true;
                sheet.Cells["O" + num.ToString()].Value = "Ghi chú";

                sheet.Cells["A1:O" + (num + 1).ToString()].Style.Font.Bold = true;

                int rowInd = num + 2;
                foreach (var item in dataMark)
                {
                    sheet.Cells[rowInd, 1].Value = item.stt;
                    sheet.Cells[rowInd, 2].Value = item.subjectName;
                    sheet.Cells[rowInd, 3].Value = item.departmentName;
                    sheet.Cells[rowInd, 4].Value = item.sum;
                    sheet.Cells[rowInd, 5].Value = item.A;
                    sheet.Cells[rowInd, 6].Value = item.rateA;
                    sheet.Cells[rowInd, 7].Value = item.B;
                    sheet.Cells[rowInd, 8].Value = item.rateB;
                    sheet.Cells[rowInd, 9].Value = item.C;
                    sheet.Cells[rowInd, 10].Value = item.rateC;
                    sheet.Cells[rowInd, 11].Value = item.D;
                    sheet.Cells[rowInd, 12].Value = item.rateD;
                    sheet.Cells[rowInd, 13].Value = item.F;
                    sheet.Cells[rowInd, 14].Value = item.rateF;
                    rowInd++;
                }

                rowInd += 1;
                sheet.Cells["K" + (rowInd).ToString() + ":L" + (rowInd).ToString()].Merge = true;
                sheet.Cells["K" + (rowInd).ToString() + ":L" + (rowInd).ToString()].Value = "KÝ TÊN";
                sheet.Cells["K" + (rowInd + 1).ToString() + ":L" + (rowInd + 1).ToString()].Merge = true;
                sheet.Cells["K" + (rowInd + 1).ToString() + ":L" + (rowInd + 1).ToString()].Value = "PHÒNG KHẢO THÍ";
                sheet.Cells["K" + (rowInd).ToString() + ":L" + (rowInd + 1).ToString()].Style.Font.Bold = true;
                rowInd++;

                sheet.Cells["A1:P" + rowInd.ToString()].AutoFitColumns();
                sheet.Cells["A1:P" + rowInd.ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells["A1:P" + rowInd.ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                package.Save();
                var file = package.GetAsByteArray();
                package.Dispose();
                return file;
            }
        }

        //xuat du lieu cua mot lop quan ly
        public Byte[] ExportExcelDataEnrollmentClass(string markOption, string subjectName, long numberOfCredit,string enrollmentClassName, long startYear, long EndYear, List<MarksByEnrollmentClass> dataMark)
        {
            MemoryStream stream = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                package.Workbook.Properties.Title = "Phổ điểm TLU";
                var sheet = package.Workbook.Worksheets.Add($"{markOption}");

                sheet.Cells["A1:E1"].Merge = true;
                sheet.Cells["A1"].Value = "TRƯỜNG ĐẠI HỌC THỦY LỢI";

                sheet.Cells["A2:E2"].Merge = true;
                sheet.Cells["A2"].Value = "PHÒNG KHẢO THÍ VÀ ĐẢM BẢO CHẤT LƯỢNG";

                sheet.Cells["A4:O4"].Merge = true;
                sheet.Cells["A4"].Value = "MÔN: " + subjectName.ToUpper();

                sheet.Cells["A5:O5"].Merge = true;
                sheet.Cells["A5"].Value = "Lớp quản lý: " + enrollmentClassName.ToUpper();

                sheet.Cells["A6:O6"].Merge = true;
                sheet.Cells["A6"].Value = markOption.ToUpper() + " " + startYear.ToString() + "-" + EndYear.ToString();

                sheet.Cells["A7:O7"].Merge = true;
                sheet.Cells["A7"].Value = "SỐ TÍN CHỈ: " + numberOfCredit.ToString();

                int num = 9;
                sheet.Cells["A" + num.ToString() + ":A" + (num + 1).ToString()].Merge = true;
                sheet.Cells["A" + (num).ToString()].Value = "STT";

                sheet.Cells["B" + num.ToString() + ":B" + (num + 1).ToString()].Merge = true;
                sheet.Cells["B" + (num).ToString()].Value = "Năm";

                sheet.Cells["C" + num.ToString() + ":C" + (num + 1).ToString()].Merge = true;
                sheet.Cells["C" + (num).ToString()].Value = "Lớp quản lý";

                sheet.Cells["D" + num.ToString() + ":D" + (num + 1).ToString()].Merge = true;
                sheet.Cells["D" + (num).ToString()].Value = "Tổng số điểm";

                sheet.Cells["E" + num.ToString() + ":F" + (num).ToString()].Merge = true;
                sheet.Cells["E" + (num).ToString()].Value = "Điểm A";
                sheet.Cells["E" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["F" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm A";

                sheet.Cells["G" + num.ToString() + ":H" + (num).ToString()].Merge = true;
                sheet.Cells["G" + num.ToString()].Value = "Điểm B";
                sheet.Cells["G" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["H" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm B";

                sheet.Cells["I" + num.ToString() + ":J" + (num).ToString()].Merge = true;
                sheet.Cells["I" + num.ToString()].Value = "Điểm C";
                sheet.Cells["I" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["J" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm C";

                sheet.Cells["K" + num.ToString() + ":L" + (num).ToString()].Merge = true;
                sheet.Cells["K" + num.ToString()].Value = "Điểm D";
                sheet.Cells["K" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["L" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm D";

                sheet.Cells["M" + num.ToString() + ":N" + (num).ToString()].Merge = true;
                sheet.Cells["M" + num.ToString()].Value = "Điểm F";
                sheet.Cells["M" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["N" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm F";

                sheet.Cells["O" + num.ToString() + ":O" + (num + 1).ToString()].Merge = true;
                sheet.Cells["O" + num.ToString()].Value = "Ghi chú";

                sheet.Cells["A1:O" + (num + 1).ToString()].Style.Font.Bold = true;

                int rowInd = num + 2;
                foreach (var item in dataMark)
                {
                    sheet.Cells[rowInd, 1].Value = item.stt;
                    sheet.Cells[rowInd, 2].Value = item.year;
                    sheet.Cells[rowInd, 3].Value = item.enrollmentClassName;
                    sheet.Cells[rowInd, 4].Value = item.sum;
                    sheet.Cells[rowInd, 5].Value = item.A;
                    sheet.Cells[rowInd, 6].Value = item.rateA;
                    sheet.Cells[rowInd, 7].Value = item.B;
                    sheet.Cells[rowInd, 8].Value = item.rateB;
                    sheet.Cells[rowInd, 9].Value = item.C;
                    sheet.Cells[rowInd, 10].Value = item.rateC;
                    sheet.Cells[rowInd, 11].Value = item.D;
                    sheet.Cells[rowInd, 12].Value = item.rateD;
                    sheet.Cells[rowInd, 13].Value = item.F;
                    sheet.Cells[rowInd, 14].Value = item.rateF;
                    rowInd++;
                }

                rowInd += 1;
                sheet.Cells["K" + (rowInd).ToString() + ":L" + (rowInd).ToString()].Merge = true;
                sheet.Cells["K" + (rowInd).ToString() + ":L" + (rowInd).ToString()].Value = "KÝ TÊN";
                sheet.Cells["K" + (rowInd + 1).ToString() + ":L" + (rowInd + 1).ToString()].Merge = true;
                sheet.Cells["K" + (rowInd + 1).ToString() + ":L" + (rowInd + 1).ToString()].Value = "PHÒNG KHẢO THÍ";
                sheet.Cells["K" + (rowInd).ToString() + ":L" + (rowInd + 1).ToString()].Style.Font.Bold = true;
                rowInd++;

                sheet.Cells["A1:P" + rowInd.ToString()].AutoFitColumns();
                sheet.Cells["A1:P" + rowInd.ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells["A1:P" + rowInd.ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                package.Save();
                var file = package.GetAsByteArray();
                package.Dispose();
                return file;
            }
        }
        public Byte[] ExportExcelDataEnrollmentClass(string markOption, string subjectName, long numberOfCredit, string semesterName, List<MarksByEnrollmentClass> dataMark)
        {
            MemoryStream stream = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                package.Workbook.Properties.Title = "Phổ điểm TLU";
                var sheet = package.Workbook.Worksheets.Add($"{markOption}");

                sheet.Cells["A1:E1"].Merge = true;
                sheet.Cells["A1"].Value = "TRƯỜNG ĐẠI HỌC THỦY LỢI";

                sheet.Cells["A2:E2"].Merge = true;
                sheet.Cells["A2"].Value = "PHÒNG KHẢO THÍ VÀ ĐẢM BẢO CHẤT LƯỢNG";

                sheet.Cells["A4:O4"].Merge = true;
                sheet.Cells["A4"].Value = "MÔN: " + subjectName.ToUpper();

                sheet.Cells["A5:O5"].Merge = true;
                sheet.Cells["A5"].Value = markOption.ToUpper() + " " + semesterName;

                sheet.Cells["A6:O6"].Merge = true;
                sheet.Cells["A6"].Value = "SỐ TÍN CHỈ: " + numberOfCredit.ToString();

                int num = 8;
                sheet.Cells["A" + num.ToString() + ":A" + (num + 1).ToString()].Merge = true;
                sheet.Cells["A" + (num).ToString()].Value = "STT";

                sheet.Cells["B" + num.ToString() + ":B" + (num + 1).ToString()].Merge = true;
                sheet.Cells["B" + (num).ToString()].Value = "Môn học";

                sheet.Cells["C" + num.ToString() + ":C" + (num + 1).ToString()].Merge = true;
                sheet.Cells["C" + (num).ToString()].Value = "Lớp quản lý";

                sheet.Cells["D" + num.ToString() + ":D" + (num + 1).ToString()].Merge = true;
                sheet.Cells["D" + (num).ToString()].Value = "Tổng số điểm";

                sheet.Cells["E" + num.ToString() + ":F" + (num).ToString()].Merge = true;
                sheet.Cells["E" + (num).ToString()].Value = "Điểm A";
                sheet.Cells["E" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["F" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm A";

                sheet.Cells["G" + num.ToString() + ":H" + (num).ToString()].Merge = true;
                sheet.Cells["G" + num.ToString()].Value = "Điểm B";
                sheet.Cells["G" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["H" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm B";

                sheet.Cells["I" + num.ToString() + ":J" + (num).ToString()].Merge = true;
                sheet.Cells["I" + num.ToString()].Value = "Điểm C";
                sheet.Cells["I" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["J" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm C";

                sheet.Cells["K" + num.ToString() + ":L" + (num).ToString()].Merge = true;
                sheet.Cells["K" + num.ToString()].Value = "Điểm D";
                sheet.Cells["K" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["L" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm D";

                sheet.Cells["M" + num.ToString() + ":N" + (num).ToString()].Merge = true;
                sheet.Cells["M" + num.ToString()].Value = "Điểm F";
                sheet.Cells["M" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["N" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm F";

                sheet.Cells["O" + num.ToString() + ":O" + (num + 1).ToString()].Merge = true;
                sheet.Cells["O" + num.ToString()].Value = "Ghi chú";

                sheet.Cells["A1:O" + (num + 1).ToString()].Style.Font.Bold = true;

                int rowInd = num + 2;
                foreach (var item in dataMark)
                {
                    sheet.Cells[rowInd, 1].Value = item.stt;
                    sheet.Cells[rowInd, 2].Value = item.subjectName;
                    sheet.Cells[rowInd, 3].Value = item.enrollmentClassName;
                    sheet.Cells[rowInd, 4].Value = item.sum;
                    sheet.Cells[rowInd, 5].Value = item.A;
                    sheet.Cells[rowInd, 6].Value = item.rateA;
                    sheet.Cells[rowInd, 7].Value = item.B;
                    sheet.Cells[rowInd, 8].Value = item.rateB;
                    sheet.Cells[rowInd, 9].Value = item.C;
                    sheet.Cells[rowInd, 10].Value = item.rateC;
                    sheet.Cells[rowInd, 11].Value = item.D;
                    sheet.Cells[rowInd, 12].Value = item.rateD;
                    sheet.Cells[rowInd, 13].Value = item.F;
                    sheet.Cells[rowInd, 14].Value = item.rateF;
                    rowInd++;
                }

                rowInd += 1;
                sheet.Cells["K" + (rowInd).ToString() + ":L" + (rowInd).ToString()].Merge = true;
                sheet.Cells["K" + (rowInd).ToString() + ":L" + (rowInd).ToString()].Value = "KÝ TÊN";
                sheet.Cells["K" + (rowInd + 1).ToString() + ":L" + (rowInd + 1).ToString()].Merge = true;
                sheet.Cells["K" + (rowInd + 1).ToString() + ":L" + (rowInd + 1).ToString()].Value = "PHÒNG KHẢO THÍ";
                sheet.Cells["K" + (rowInd).ToString() + ":L" + (rowInd + 1).ToString()].Style.Font.Bold = true;
                rowInd++;

                sheet.Cells["A1:P" + rowInd.ToString()].AutoFitColumns();
                sheet.Cells["A1:P" + rowInd.ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells["A1:P" + rowInd.ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                package.Save();
                var file = package.GetAsByteArray();
                package.Dispose();
                return file;
            }
        }

        public Byte[] ExportExcelDataCourseSubject2(string markOption, string subjectName, long numberOfCredit, string semesterName, List<Data> dataMark)
        {
            MemoryStream stream = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                package.Workbook.Properties.Title = "Phổ điểm TLU";
                var sheet = package.Workbook.Worksheets.Add($"{markOption}");

                sheet.Cells["A1:E1"].Merge = true;
                sheet.Cells["A1"].Value = "TRƯỜNG ĐẠI HỌC THỦY LỢI";

                sheet.Cells["A2:E2"].Merge = true;
                sheet.Cells["A2"].Value = "PHÒNG KHẢO THÍ VÀ ĐẢM BẢO CHẤT LƯỢNG";

                sheet.Cells["A4:O4"].Merge = true;
                sheet.Cells["A4"].Value = "MÔN: " + subjectName.ToUpper();

                sheet.Cells["A5:O5"].Merge = true;
                sheet.Cells["A5"].Value = markOption.ToUpper() + " HỌC KỲ:" + semesterName;

                sheet.Cells["A6:O6"].Merge = true;
                sheet.Cells["A6"].Value = "SỐ TÍN CHỈ: " + numberOfCredit.ToString();

                int num = 8;
                sheet.Cells["A" + num.ToString() + ":A" + (num + 1).ToString()].Merge = true;
                sheet.Cells["A" + (num).ToString()].Value = "STT";
                sheet.Cells["B" + num.ToString() + ":B" + (num + 1).ToString()].Merge = true;
                sheet.Cells["B" + (num).ToString()].Value = "Nhóm môn học";
                sheet.Cells["C" + num.ToString() + ":C" + (num + 1).ToString()].Merge = true;
                sheet.Cells["C" + (num).ToString()].Value = "Tên giáo viên";
                sheet.Cells["D" + num.ToString() + ":D" + (num + 1).ToString()].Merge = true;
                sheet.Cells["D" + (num).ToString()].Value = "Tổng số điểm";

                sheet.Cells["E" + num.ToString() + ":F" + (num).ToString()].Merge = true;
                sheet.Cells["E" + (num).ToString()].Value = "Điểm A";
                sheet.Cells["E" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["F" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm A";

                sheet.Cells["G" + num.ToString() + ":H" + (num).ToString()].Merge = true;
                sheet.Cells["G" + num.ToString()].Value = "Điểm B";
                sheet.Cells["G" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["H" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm B";

                sheet.Cells["I" + num.ToString() + ":J" + (num).ToString()].Merge = true;
                sheet.Cells["I" + num.ToString()].Value = "Điểm C";
                sheet.Cells["I" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["J" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm C";

                sheet.Cells["K" + num.ToString() + ":L" + (num).ToString()].Merge = true;
                sheet.Cells["K" + num.ToString()].Value = "Điểm D";
                sheet.Cells["K" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["L" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm D";

                sheet.Cells["M" + num.ToString() + ":N" + (num).ToString()].Merge = true;
                sheet.Cells["M" + num.ToString()].Value = "Điểm F";
                sheet.Cells["M" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["N" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm F";

                sheet.Cells["O" + num.ToString() + ":O" + (num + 1).ToString()].Merge = true;
                sheet.Cells["O" + num.ToString()].Value = "Ghi chú";

                sheet.Cells["A1:O" + (num + 1).ToString()].Style.Font.Bold = true;

                int rowInd = num + 2;
                foreach (var item in dataMark)
                {
                    sheet.Cells[rowInd, 1].Value = item.stt;
                    sheet.Cells[rowInd, 2].Value = item.courseSubjectName;
                    sheet.Cells[rowInd, 3].Value = item.teacherName;
                    sheet.Cells[rowInd, 4].Value = item.sum;
                    sheet.Cells[rowInd, 5].Value = item.A;
                    sheet.Cells[rowInd, 6].Value = item.rateA;
                    sheet.Cells[rowInd, 7].Value = item.B;
                    sheet.Cells[rowInd, 8].Value = item.rateB;
                    sheet.Cells[rowInd, 9].Value = item.C;
                    sheet.Cells[rowInd, 10].Value = item.rateC;
                    sheet.Cells[rowInd, 11].Value = item.D;
                    sheet.Cells[rowInd, 12].Value = item.rateD;
                    sheet.Cells[rowInd, 13].Value = item.F;
                    sheet.Cells[rowInd, 14].Value = item.rateF;
                    rowInd++;
                }
                rowInd += 1;
                sheet.Cells["K" + (rowInd).ToString() + ":L" + (rowInd).ToString()].Merge = true;
                sheet.Cells["K" + (rowInd).ToString() + ":L" + (rowInd).ToString()].Value = "KÝ TÊN";
                sheet.Cells["K" + (rowInd + 1).ToString() + ":L" + (rowInd + 1).ToString()].Merge = true;
                sheet.Cells["K" + (rowInd + 1).ToString() + ":L" + (rowInd + 1).ToString()].Value = "PHÒNG KHẢO THÍ";
                sheet.Cells["K" + (rowInd).ToString() + ":L" + (rowInd + 1).ToString()].Style.Font.Bold = true;
                rowInd++;
                sheet.Cells["A1:P" + rowInd.ToString()].AutoFitColumns();
                sheet.Cells["A1:P" + rowInd.ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells["A1:P" + rowInd.ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                package.Save();
                var file = package.GetAsByteArray();
                package.Dispose();
                return file;
            }
        }

        public Byte[] ExportExcelDataTeacher2(string markOption, string subjectName, long numberOfCredit, string semesterName, List<MarkRate> dataMark)
        {
            MemoryStream stream = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                package.Workbook.Properties.Title = "Phổ điểm TLU";
                var sheet = package.Workbook.Worksheets.Add($"{markOption}");

                sheet.Cells["A1:E1"].Merge = true;
                sheet.Cells["A1"].Value = "TRƯỜNG ĐẠI HỌC THỦY LỢI";

                sheet.Cells["A2:E2"].Merge = true;
                sheet.Cells["A2"].Value = "PHÒNG KHẢO THÍ VÀ ĐẢM BẢO CHẤT LƯỢNG";

                sheet.Cells["A4:O4"].Merge = true;
                sheet.Cells["A4"].Value = "MÔN: " + subjectName.ToUpper();

                sheet.Cells["A5:O5"].Merge = true;
                sheet.Cells["A5"].Value = markOption.ToUpper() + " " + semesterName;

                sheet.Cells["A6:O6"].Merge = true;
                sheet.Cells["A6"].Value = "SỐ TÍN CHỈ: " + numberOfCredit.ToString();

                int num = 8;
                sheet.Cells["A" + num.ToString() + ":A" + (num + 1).ToString()].Merge = true;
                sheet.Cells["A" + (num).ToString()].Value = "STT";

                sheet.Cells["B" + num.ToString() + ":B" + (num + 1).ToString()].Merge = true;
                sheet.Cells["B" + (num).ToString()].Value = "Môn học";

                sheet.Cells["C" + num.ToString() + ":C" + (num + 1).ToString()].Merge = true;
                sheet.Cells["C" + (num).ToString()].Value = "Tên giáo viên";

                sheet.Cells["D" + num.ToString() + ":D" + (num + 1).ToString()].Merge = true;
                sheet.Cells["D" + (num).ToString()].Value = "Tổng số điểm";

                sheet.Cells["E" + num.ToString() + ":F" + (num).ToString()].Merge = true;
                sheet.Cells["E" + (num).ToString()].Value = "Điểm A";
                sheet.Cells["E" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["F" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm A";

                sheet.Cells["G" + num.ToString() + ":H" + (num).ToString()].Merge = true;
                sheet.Cells["G" + num.ToString()].Value = "Điểm B";
                sheet.Cells["G" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["H" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm B";

                sheet.Cells["I" + num.ToString() + ":J" + (num).ToString()].Merge = true;
                sheet.Cells["I" + num.ToString()].Value = "Điểm C";
                sheet.Cells["I" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["J" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm C";

                sheet.Cells["K" + num.ToString() + ":L" + (num).ToString()].Merge = true;
                sheet.Cells["K" + num.ToString()].Value = "Điểm D";
                sheet.Cells["K" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["L" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm D";

                sheet.Cells["M" + num.ToString() + ":N" + (num).ToString()].Merge = true;
                sheet.Cells["M" + num.ToString()].Value = "Điểm F";
                sheet.Cells["M" + (num + 1).ToString()].Value = "Tổng số điểm";
                sheet.Cells["N" + (num + 1).ToString()].Value = "Tỉ lệ(%) điểm F";

                sheet.Cells["O" + num.ToString() + ":O" + (num + 1).ToString()].Merge = true;
                sheet.Cells["O" + num.ToString()].Value = "Ghi chú";

                sheet.Cells["A1:O" + (num + 1).ToString()].Style.Font.Bold = true;

                int rowInd = num + 2;
                foreach (var item in dataMark)
                {
                    sheet.Cells[rowInd, 1].Value = item.stt;
                    sheet.Cells[rowInd, 2].Value = item.subjectName;
                    sheet.Cells[rowInd, 3].Value = item.teacherName;
                    sheet.Cells[rowInd, 4].Value = item.sum;
                    sheet.Cells[rowInd, 5].Value = item.A;
                    sheet.Cells[rowInd, 6].Value = item.rateA;
                    sheet.Cells[rowInd, 7].Value = item.B;
                    sheet.Cells[rowInd, 8].Value = item.rateB;
                    sheet.Cells[rowInd, 9].Value = item.C;
                    sheet.Cells[rowInd, 10].Value = item.rateC;
                    sheet.Cells[rowInd, 11].Value = item.D;
                    sheet.Cells[rowInd, 12].Value = item.rateD;
                    sheet.Cells[rowInd, 13].Value = item.F;
                    sheet.Cells[rowInd, 14].Value = item.rateF;
                    rowInd++;
                }

                rowInd += 1;
                sheet.Cells["K" + (rowInd).ToString() + ":L" + (rowInd).ToString()].Merge = true;
                sheet.Cells["K" + (rowInd).ToString() + ":L" + (rowInd).ToString()].Value = "KÝ TÊN";
                sheet.Cells["K" + (rowInd + 1).ToString() + ":L" + (rowInd + 1).ToString()].Merge = true;
                sheet.Cells["K" + (rowInd + 1).ToString() + ":L" + (rowInd + 1).ToString()].Value = "PHÒNG KHẢO THÍ";
                sheet.Cells["K" + (rowInd).ToString() + ":L" + (rowInd + 1).ToString()].Style.Font.Bold = true;
                rowInd++;

                sheet.Cells["A1:P" + rowInd.ToString()].AutoFitColumns();
                sheet.Cells["A1:P" + rowInd.ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells["A1:P" + rowInd.ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                package.Save();
                var file = package.GetAsByteArray();
                package.Dispose();
                return file;
            }
        }
    }
}