function show(name) {
    chart_draw();
    $('#chart_qt').hide();
    $('#chart_exam').hide();
    $('#chart_final').hide();
    if (name != '') $(`#${name}`).show();
}
var value = { value: '', subject: '', semester: '', course: '', period: '', schoolYear:'', courseYear:'', subject_id:'' };
var listClass = new Set();
var chart_data = {
    qt: [],
    exam: [],
    final: []
};
$('.dropdown-container')
    .on('click', '.dropdown-button', function () {
        $(this).siblings('.dropdown-list').toggle();
    })
    .on('input', '.dropdown-search', function () {
        var target = $(this);
        var dropdownList = target.closest('.dropdown-list');
        var search = target.val().toLowerCase();

        if (!search) {
            dropdownList.find('li').show();
            return false;
        }

        dropdownList.find('li').each(function () {
            var text = $(this).text().toLowerCase();
            var match = text.indexOf(search) > -1;
            $(this).toggle(match);
        });
    });

function unDisableButton() {
    document.getElementById('1000001').disabled = false
    document.getElementById('1000002').disabled = false
}

function getListEnrollmentClass() {
    listClass = new Set();
    value.type = "2";
    getClass(2);
    document.getElementById('1000002').disabled = true
    document.getElementById('1000001').disabled = false
    
}
function getListCourseSubject() {
    value.type = "1";
    listClass = new Set();
    getClass(1);
    document.getElementById('1000001').disabled = true
    document.getElementById('1000002').disabled = false
    console.log('button is disabled')
}
function getClass(type) {
    form = $('#form');
    datas = { data: form.serialize() + "&type=" + type };
    $.ajax({
        method: form.attr('method'),
        url: form.attr('action'),
        data: form.serialize() + "&type=" + type,
        type: 'GET',
        // other AJAX settings goes here
        // ..
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr);
            console.log(ajaxOptions);
            console.log(thrownError);
            //alert("Vui lòng kiểm tra lại các lựa chọn!")
        }
    }).done(function (response) {
        if (response != null) {
            if (response.code == 500) {
                alert("Không có dữ liệu được nhận!")
            }
            else {
                if (response.data != null) {
                    if (response.name == "1") {
                        $("#class_label").empty();
                        $("#class_label").append("Chọn lớp học phần");
                    } else {
                        $("#class_label").empty();
                        $("#class_label").append("Chọn lớp quản Lý");
                    }
                    let temp = $("#listClass")
                    temp.empty();
                    for (let i = 0; i < response.data.length; i++) {
                        let k = `<li>
                                    <label class="checkbox-wrap">
                                        <input id="${response.data[i].id}" onclick="oncheck(this)" type="checkbox">
                                        <span for="AL">${response.data[i].name}</span> <span class="checkmark"></span>
                                    </label>
                                 </li>`;
                        temp.append(k)
                    }
                } else {
                    alert("Không có nhóm phù hợp!");
                }
            }
        }
        else {
            alert("Đã có lỗi xảy ra!");
        }
        // Process the response here
    });
    event.preventDefault(); // <- avoid reloading
}

function oncheck(checkbox) {
    if (checkbox.checked) {
        listClass.add(checkbox.id);
    }
    else {
        listClass.delete(checkbox.id);
    }
    console.log(listClass)
    var container = $('.dropdown-container');
    var numChecked = container.find('[type="checkbox"]:checked').length;
    container.find('.quantity').text(numChecked || 'Chọn nhiều');
}
function showClass(show) {
    let value = show.getAttribute('value');
    let type = show.getAttribute('type');
    change(value, type);
    $('.dropdown-list').toggle();
}
function download(download) {
    let value = download.getAttribute('value');
    let type = download.getAttribute('type');
    listClass = new Set();
    xuat1(value, type);
}


function change(res, type) {
    show('');
    $("#_loading").show();
    $("#main_content").hide();
    $.ajax({
        url: '/StatisticByYear/GetMark',
        dataType: "json",
        type: 'POST',
        data: { listId: [...listClass], type: value.type, subject: value.subject_id, year: value.schoolYear },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr);
            console.log(ajaxOptions);
            alert('Chưa có dữ liệu');
            $("#_loading").hide();
            $("#main_content").show()
        }
    }).done(function (response) {
        if (response.data != null) {
            console.log(response.data);
            if (response.code == 200) {
                let data1 = response.data.map(Object.values);;
                chart_data = response.chart_mark;
                $("#table_id").DataTable().clear();
                $("#table_id").DataTable().rows.add(data1);
                $("#table_id").DataTable().draw();
                $("#table_id>thead>tr .headTb").css('width', "10%");
                document.getElementById('1000001').disabled = false
                document.getElementById('1000002').disabled = false
                $("#_loading").hide();
                $("#main_content").show()
            }
            else {
                alert(response.data);               
            }
        }
        else {
            alert("Kết nối thất bại!")
        }
        $("#_loading").hide();
        $("#main_content").show()
    });
    event.preventDefault(); // <- avoid reloading
}

function setCourseYear(res) {
    value.schoolYear = res.value;
    getCourseYear(); //call ajax
    unDisableButton();
}
function setSubject(res) {
    value.courseYear = res.value;
    getSubject(); //call ajax
    unDisableButton();
}
function setSubjectId(res) {
    value.subject_id = res.value;
    unDisableButton();
}
//lấy danh sách các khóa học (k61,k62) có trong năm đó
function getCourseYear() {
    $.ajax({
        url: '/StatisticByYear/getCourseYear',
        dataType: "json",
        type: 'POST',
        data: { schoolYearId: value.schoolYear },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr);
            console.log(ajaxOptions);
        }
    }).done(function (response) {
        if (response.data != null) {
            if (response.code == 200) {
                let temp = $("#courseYear")
                temp.empty();
                temp.append('<option value="">Khóa học</option>');
                for (let i = 0; i < response.data.length; i++) {
                    let k = '<option value=' + response.data[i].id + '>' + response.data[i].name + '</option>';
                    temp.append(k)
                }
            }
            else {
                alert(response.data);
            }
        }
        else {
            alert("Kết nối thất bại!")
        }
    });
    event.preventDefault(); // <- avoid reloading
}
function getSubject() {
    $.ajax({
        url: '/StatisticByYear/getSubject',
        dataType: "json",
        type: 'POST',
        data: { schoolYearId: value.schoolYear, courseYearId: value.courseYear, subjectId: value.subject_id },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr);
            console.log(ajaxOptions);
        }
    }).done(function (response) {
        if (response.data != null) {
            if (response.code == 200) {
                let temp = $("#subject")
                temp.empty();
                temp.append('<option value="">Môn học</option>');
                for (let i = 0; i < response.data.length; i++) {
                    let k = '<option value=' + response.data[i].id + '>' + response.data[i].subject_name + '</option>';
                    temp.append(k)
                }
            }
            else {
                alert(response.data);
            }
        }
        else {
            alert("Kết nối thất bại!")
        }
    });
    event.preventDefault(); // <- avoid reloading
}
//lấy danh sách các kỳ học (chính phụ tăng cường...)
function setSemesterRegistorPeriod() {
    $.ajax({
        url: '/StatisticByYear/getSemesterRegistorPeriod',
        dataType: "json",
        type: 'POST',
        data: { courseId: value.course, semesterId: value.semester, periodId: value.period },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr);
            console.log(ajaxOptions);
        }
    }).done(function (response) {
        if (response.data != null) {
            if (response.code == 200) {
                let temp = $("#dotHoc")
                temp.empty();
                temp.append('<option value="">Đợt học</option>');
                for (let i = 0; i < response.data.length; i++) {
                    let k = '<option value=' + response.data[i].id + '>' + response.data[i].name + '</option>';
                    temp.append(k)
                }
            }
            else {
                alert(response.data);
            }
        }
        else {
            alert("Kết nối thất bại!")
        }
    });
    event.preventDefault(); // <- avoid reloading
}
function xuat() {
    $.ajax({
        type: "POST",
        url: "/StatisticByYear/ExportAll",
        data: {
            type: value.type,
            subject: value.subject_id,
            year: value.schoolYear,
            data: [...listClass]
        },
        error: function (a, b, c) {
            console.log(a);
            console.log(b);
        },
    }).done(function (r) {
        if (r.code == 200) {
            //Convert Base64 string to Byte Array.
            var bytes = Base64ToBytes(r.data);

            //Convert Byte Array to BLOB.
            var blob = new Blob([bytes], { type: "application/octetstream/xlsx" });

            //Check the Browser type and download the File.
            var isIE = false || !!document.documentMode;
            if (isIE) {
                window.navigator.msSaveBlob(blob, r.name);
            } else {
                var url = window.URL || window.webkitURL;
                link = url.createObjectURL(blob);
                var a = $("<a />");
                a.attr("download", r.name);
                a.attr("href", link);
                $("body").append(a);
                a[0].click();
                $("body").remove(a);
            }

        }
        else console.log(r.mgs);
    });
};
function Base64ToBytes(base64) {
    var s = window.atob(base64);
    var bytes = new Uint8Array(s.length);
    for (var i = 0; i < s.length; i++) {
        bytes[i] = s.charCodeAt(i);
    }
    return bytes;
};
function chart_draw() {
    massPopChart1.config.data.datasets[0].data = chart_data.exam;
    massPopChart1.update();
    massPopChart2.config.data.datasets[0].data = chart_data.final;
    massPopChart2.update();
    massPopChart.config.data.datasets[0].data = chart_data.qt;
    massPopChart.update();
}

var mychart1 = document.getElementById('myChart1').getContext('2d');
var myChart = document.getElementById('myChart').getContext('2d');
var myChart2 = document.getElementById('myChart2').getContext('2d');

var dataLabel = ['F', 'D', 'C', 'B', 'A'];

var massPopChart1 = new Chart(myChart1, setChart('Biểu đồ phân bố điểm thi', chart_data.exam, 'Điểm thi'));
var massPopChart = new Chart(myChart, setChart('Biểu đồ phân bố điểm quá trình', chart_data.qt, 'Điểm quá trình'));
var massPopChart2 = new Chart(myChart2, setChart('Biểu đồ phân bố điểm tổng kết', chart_data.final, 'Điểm tổng kết'));
$(document).ready(function () {
    var table = $("#table_id").DataTable();
    document.getElementById('1000001').disabled = true
    document.getElementById('1000002').disabled = true
})
function setChart(title, data, label) {
    return {
        type: 'bar', // bar, horizontalBar, pie, line, doughnut, radar, polarArea
        data: {
            labels: dataLabel,
            datasets: [{
                label: label,
                data: data,
                //backgroundColor:'green',
                backgroundColor: '#198002',
                borderColor: '#7C7AFD',
                hoverBorderWidth: 1,
                hoverBorderColor: '#7C7AFD'
            }]
        },
        options: {
            title: {
                display: true,
                text: title,
                fontSize: 20
            },
            legend: {
                display: true,
                position: 'right',
                labels: {
                    fontColor: '#000'
                }
            },
            layout: {
                padding: {
                    left: 50,
                    right: 0,
                    bottom: 0,
                    top: 0
                }
            },
            tooltips: {
                enabled: true
            }
        }
    }
}