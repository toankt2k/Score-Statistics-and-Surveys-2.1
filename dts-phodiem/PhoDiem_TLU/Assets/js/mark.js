﻿$(document).ready(function () {
    var ok = false;
    var dataMark = null;
    handlefilter();
    var table = $("#table_id_department").DataTable({
        "processing": true,
    });
        
})
const Marks = (function (){
    var data = []
    return {
        getAllData() {
            return data
        },
        setData(marks) {
            data = [...marks]
        },
        getData(stt) {
            return data[stt-1]
        }
    }
}())
const DataExcel = (function () {
    var data = []
    return {
        setData(marks) {
            data = [...marks]
        }
    }
}())
const Selected = (function () {
    var subjectID;
    var semesterIDStart;
    var semesterIDEnd;
    var showOption;
    var markOption;
    return {
        setData(s1, s2, s3, s4, s5) {
            subjectID = s1
            semesterIDStart = s2
            semesterIDEnd = s3
            showOption = s4
            markOption = s5
        },
        getData() {
            return {
                subjectID, semesterIDStart, semesterIDEnd, showOption, markOption
            }
        }
    }
}())
function handlefilter() {
    var butt = document.getElementById('show__btn-id');
    butt.addEventListener('click', function (event) {

});
    $('form').submit(function (even) {
        event.preventDefault(); // <- avoid reloading
        console.log("oke")
        if (even.originalEvent.submitter.value == 'Hiển thị') {
        $.ajax({
            method: $(this).attr('method'),
            url: $(this).attr('action'),
            data: $(this).serialize(),
            datatype: 'json',
            type: 'GET',
            // other AJAX settings goes here
            // ..

            error: function (xhr, ajaxOptions, thrownError) {
                console.log(xhr);
                console.log(ajaxOptions);
                console.log(thrownError);
            }
        }).done(function (response) {
            if (response != null) {
                if (response.code == 200) {
                    

                    console.log(response)
                    /*DataExcel.setData(response.dataExcel)*/
                    dataMark = null
                    
                    Marks.setData(response.dataTable)
                    Selected.setData(response.subjectID, response.semesterIDStart, response.semesterIDEnd, response.showoption, response.markOption);
                    console.log(Marks.getAllData())
                    fillDataToChart(response.dataTable, response.showoption);
                    fillDataToChartPie(response.sumMark);
                    ok = true;
                    
                    showHandler(response.showoption, response.markOption)
                    handlerExport()
                }
                else {
                    console.log(response);
                }
            }
            else {
                console.log("Kết nối thất bại!");
                alert('Không có dữ liệu');
            }
        });
    
        }
            
    })
}
function showHandler(showOption, markOption) {
    
    if (true) {
        
        if (showOption === "HTK") {
            var marks = Marks.getAllData()
            var data = marks.map(dataItem => {
                var { stt, departmentID, departmentName, sum, A, rateA, B, rateB, C, rateC, D, rateD, F, rateF } = dataItem
                return {
                    
                    stt, departmentName, sum, A, rateA, B, rateB, C, rateC, D, rateD, F, rateF,
                    'checkbox': `<div class="form-check">
                                  <input class="form-check-input" type="checkbox" name="checkboxExport[]" value="${departmentID}">
                                  
                                </div>`,
                }
            })
            
            data = data.map(Object.values)
            console.log(data)
            $("#table_id_department").DataTable().clear();
            $("#table_id_department").DataTable().rows.add(data);
            $("#table_id_department").DataTable().draw();
            $("#table_id_department").show();
            $("#table_id_teacher_wrapper").hide();
            $("#table_id_enrollmentClass_wrapper").hide();
        }
        else if (showOption === "HTGV") {
            var marks = Marks.getAllData()
            var data = marks.map(data => {
                var { stt, teacherName, sum, A, rateA, B, rateB, C, rateC, D, rateD, F, rateF } = data
                return {
                    stt, teacherName, sum, A, rateA, B, rateB, C, rateC, D, rateD, F, rateF,
                    'button': `<button onclick= "handleExportTeacher(${stt},'${markOption}')" class="download export"><i class="fa fa-download" aria-hidden="true"></i></button>`
                }
            })

            data = data.map(Object.values)
            $("#table_id_teacher").DataTable().clear();
            $("#table_id_teacher").DataTable().rows.add(data);
            $("#table_id_teacher").DataTable().draw();
            $("#table_id_teacher").show();
            $("#table_id_department_wrapper").hide();
            $("#table_id_enrollmentClass_wrapper").hide();
        }
        else {
            var marks = Marks.getAllData()
            var data = marks.map(data => {
                var { stt, subjectName, enrollmentClassName, sum, A,rateA,B,rateB,C,rateC,D,rateD,F,rateF} = data
                return {
                    stt, subjectName, enrollmentClassName, sum, A, rateA, B, rateB, C, rateC, D, rateD, F, rateF,
                    'button': `<button onclick= "handleExportEnrollmentClass(${stt},'${markOption}')" class="download export"><i class="fa fa-download" aria-hidden="true"></i></button>`
                }
            })
           
            data = data.map(Object.values)
            $("#table_id_enrollmentClass").DataTable().clear();
            $("#table_id_enrollmentClass").DataTable().rows.add(data);
            $("#table_id_enrollmentClass").DataTable().draw();
            $("#table_id_teacher_wrapper").hide();
            $("#table_id_department_wrapper").hide();
            $("#table_id_enrollmentClass").show();
        }

    }
}
function handlerExport() {
    selected = Selected.getData()
    var { subjectID, semesterIDStart, semesterIDEnd, showOption, markOption, } = selected
    var submitBtn = $('#export__btn-id')
    var checkboxIDs = []
    submitBtn.click(function (e) {
        e.preventDefault()

        $('input[name="checkboxExport[]"]:checked').each(function () {
            checkboxIDs.push(
                $(this).val()
            )
        });
        console.log(checkboxIDs);
        if (checkboxIDs.length > 0) {

            $.post("../Mark/Export", {
                subjectID,
                semesterIDStart,
                semesterIDEnd,
                showOption,
                markOption,
                checkboxIDs
            }, function (data) {
                if (data.code == 200) {
                    var bytes = Base64ToBytes(data.result)
                    var a = window.document.createElement('a')

                    a.href = window.URL.createObjectURL(new Blob([bytes], { type: 'application/xlsx' }))

                    a.download = `${data.fileName}`

                    document.body.appendChild(a)
                    a.click();
                    document.body.removeChild(a)
                }
                else alert("Lỗi khi xuất file")
            })
        }
    })
}
function handleExportDepartment(stt, markOption) {
    var mark = Marks.getData(stt)
    console.log(mark.subjectID, mark.startYearID, mark.endYearID, mark.departmentID, markOption)
    $.post("../Mark/ExportFileDepartment", {
        subject_id: mark.subjectID,
        school_year_id_start: mark.startYearID,
        school_year_id_end: mark.endYearID,
        departmentID: mark.departmentID,
        markOption: markOption
    }, function (data) {
        if (data.code == 200) {
            var bytes = Base64ToBytes(data.result)
            var a = window.document.createElement('a')

            a.href = window.URL.createObjectURL(new Blob([bytes], { type: 'application/xlsx' }))

            a.download = `${data.fileName}`

            document.body.appendChild(a)
            a.click();
            document.body.removeChild(a)
        }
        else alert("Lỗi khi xuất file")
    })
}
function handleExportTeacher(stt, markOption) {
    var mark = Marks.getData(stt)
    console.log(mark.subjectID, mark.startYearID, mark.endYearID, mark.teacherID, markOption)
    $.post("../Mark/ExportFileTeacher", {
        subject_id: mark.subjectID,
        school_year_id_start: mark.startYearID,
        school_year_id_end: mark.endYearID,
        teacherID: mark.teacherID,
        markOption: markOption
    }, function (data) {
        if (data.code == 200) {
            var bytes = Base64ToBytes(data.result)
            var a = window.document.createElement('a')

            a.href = window.URL.createObjectURL(new Blob([bytes], { type: 'application/xlsx' }))

            a.download = `${data.fileName}`

            document.body.appendChild(a)
            a.click();
            document.body.removeChild(a)
        }
        else alert("Lỗi khi xuất file")
    })
}
function handleExportEnrollmentClass(stt,markOption) {
    
    var mark = Marks.getData(stt)
    console.log(mark)
    $.post("../Mark/ExportFileEnrollmentClass", {
        subject_id: mark.subjectID,
        school_year_id_start: mark.startYear,
        school_year_id_end: mark.endYear,
        enrollmentClassID: mark.enrollmentClassID,
        markOption: markOption
    }, function (data) {
        if (data.code == 200) {
            var bytes = Base64ToBytes(data.result)
            var a = window.document.createElement('a')

            a.href = window.URL.createObjectURL(new Blob([bytes], { type: 'application/xlsx' }))

            a.download = `${data.fileName}`

            document.body.appendChild(a)
            a.click();
            document.body.removeChild(a)
        }
        else alert("Lỗi khi xuất file")
    })
}
function Base64ToBytes(base64) {
    var s = window.atob(base64)
    var bytes = new Uint8Array(s.length)
    for (var i = 0; i < s.length; ++i) {
        bytes[i] = s.charCodeAt(i);
    }
    return bytes;
}
function fillDataToChart(list,type) {
    let typeChart = 'horizontalBar';
    let istypeBar = false;
    let labels;
    if (type == 'HTK') {
        labels = list.map(function (data) {
            return data.departmentName;

        })
    }
    else if (type == 'HTGV') {
        labels = list.map(function (data) {
            return data.teacherName;

        })
            istypeBar = true;
    typeChart = 'bar';
        }
    else {
        labels = list.map(function (data) {
            return data.enrollmentClassName;

        })
    }
    cleardata('myChart', istypeBar);
    var chartBar = document.getElementById('myChart').getContext('2d');
    const chartMark = {
        type: typeChart,
    data: {
        labels: labels,
    datasets: [
    {
        label: "Điểm A",
    data: list.map(function (mark) {
                            return mark.A;
                        }),
    backgroundColor: '#333',
                    },
    {
        label: "Điểm B",
    data: list.map(function (mark) {
                            return mark.B;
                        }),
    backgroundColor: '#7FBA00'
                    },
    {
        label: "Điểm C",
    data: list.map(function (mark) {
                            return mark.C;
                        }),
    backgroundColor: '#DD5246'
                    },
    {
        label: "Điểm D",
    data: list.map(function (mark) {
                            return mark.D;
                        }),
    backgroundColor: '#4B8BF4'
                    },
    {
        label: "Điểm F",
    data: list.map(function (mark) {
                            return mark.F;
                        }),
    backgroundColor: '#FFCD42'
                    }


    ]

            },
    options: {
        responsive: true,
    legend: {
        display: true
                },
    title: {
        display: true,
    text: 'Tỉ lệ điểm A,B,C,D',
    fontSize:20
                    
                },
                
               
            },
        }
    var chart = new Chart(chartBar, chartMark);
    chart.update();
}
function fillDataToChartPie(list) {
    cleardata('chartPie', true);
    var chartPie = document.getElementById('chartPie').getContext('2d');
    let title = 'Tỉ lệ điểm A,B,C,D';
    var labels = ["Điểm A", "Điểm B", "Điểm C", "Điểm D", "Điểm F"];

    var barColors = [
    "#b91d47",
    "#00aba9",
    "#2b5797",
    "#e8c3b9",
    "#1e7145"
    ];

    var chart = new Chart('chartPie', {
        type: 'pie',
    data: {
        labels: labels,
    datasets: [
    {
        data: list,
    backgroundColor: barColors,
                    },


    ]

            },
    options: {
        title: {
        display: true,
    text: title,
    fontSize: 20
                },
    legend: {
        display: true,
                }
            }
        });
    chart.update();
}
function showChart(name, nameSwap) {
    if (ok) {
        $(`#${nameSwap}`).show();
        $(`#${name}`).hide();
    }
}
function cleardata(name,istypeBar) {
    var element = document.getElementById(name);
    var parentElement = element.parentElement;
    element.remove();
    if (istypeBar) {
        parentElement.innerHTML = `<canvas id="${name}" width="1000"></canvas>`;
        }
    else {
        parentElement.innerHTML = `<canvas id="${name}" height="800"></canvas>`;
    }
        
}
