function show(name) {
    chart_draw();
    $('#chart_qt').hide();
    $('#chart_exam').hide();
    $('#chart_final').hide();
    if (name != '') $(`#${name}`).show();
}
var value = { value: '', subject: '', semester: '', course: '', period: '' };
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

    




function ql() {
    listClass = new Set();
    $('.dropdown-container').find('.quantity').text('Chọn nhiều');
    value.type = "2";
    getClass(2);
}
function hp() {
    value.type = "1";
    listClass = new Set();
    $('.dropdown-container').find('.quantity').text('Chọn nhiều');
    getClass(1);
}
function getClass(type) {
    form = $('#form');
    datas = { data : form.serialize()+"&type=" + type };
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

function checkAll(res) {
    let list = $("#listClass > li input");
    if (!res.checked) {
        listClass.clear();
        list.prop('checked', false);
    } else {
        list.prop('checked', true);
        listClass.clear();
        list.toArray().forEach(val => {
            listClass.add(val.id);
        })
    }
    $('.dropdown-container').find('.quantity').text(listClass.size || 'Chọn nhiều');
}

function oncheck(checkbox) {
    if (checkbox.checked) {
        listClass.add(checkbox.id);
    }
    else {
        listClass.delete(checkbox.id);
    }
    var container = $('.dropdown-container');
    var numChecked = listClass.size;
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
    xuat1(value,type);
}


function change(res, type) {
    show('');
    $("#_loading").show();
    $("#main_content").hide();
    $.ajax({
        url: '/Semester/GetMark',
        dataType: "json",
        type: 'POST',
        data: { listId: [...listClass], type: value.type, subject: value.subject, semester: value.semester },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr);
            console.log(ajaxOptions);
        }
    }).done(function (response) {
        if (response.data != null) {
            if (response.code == 200 && response.data.length > 0) {
                let data1 = response.data.map(Object.values);;
                chart_data = response.chart_mark;
                $("#table_id").DataTable().clear();
                $("#table_id").DataTable().rows.add(data1);
                $("#table_id").DataTable().draw();
                $("#table_id>thead>tr .headTb").css('width', "10%");
            }
            else {
                alert(response.data || "Không có dữ liệu");
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

function setSemester(res) {
    value.semester = res.value;
    setFillter();
}
function setSubject(res) {
    value.subject = res.value;
}
function setCourse(res) {
    value.course = res.value;
    setFillter();
}
function setPeriod(res) {
    value.period = res.value;
    setFillter();
}

function setFillter() {
    $.ajax({
        url: '/Semester/getSubject',
        dataType: "json",
        type: 'POST',
        data: { courseId: value.course, semesterId:value.semester, periodId:value.period },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr);
            console.log(ajaxOptions);
        }
    }).done(function (response) {
        if (response.data != null) {
            if (response.code == 200) {
                response.data = response.data.sort((a,b) => { return (a.subject_name >= b.subject_name?1:-1)});
                let temp = $("#monHoc")
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

function xuat() {
    $.ajax({
        type: "POST",
        url: "/Semester/ExportAll",
        data: {
            type: value.type,
            subject: value.subject,
            semester: value.semester,
            data: [...listClass]
        },
        error: function (a, b,c) {
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
    //var data = [
    //    [1, 'Giải tích 2-1-19 (61CTM+KTO_01)', 'teacher', 3, 5.172413793103448, 1, 1.7241379310344827, 0, 0, 10, 17.24137931034483, 44, 75.86206896551724, 'môn học'],
    //    [1, 'Giải tích 2-1-19 (61CTM+KTO_01)', 'teacher', 3, 5.172413793103448, 1, 1.7241379310344827, 0, 0, 10, 17.24137931034483, 44, 75.86206896551724, 'môn học'],
    //    [1, 'Giải tích 2-1-19 (61CTM+KTO_01)', 'teacher', 3, 5.172413793103448, 1, 1.7241379310344827, 0, 0, 10, 17.24137931034483, 44, 75.86206896551724, 'môn học'],

    //];
    //console.log(data);
    //$("#table_id").DataTable().clear();
    //$("#table_id").DataTable().rows.add(data);
    //$("#table_id").DataTable().draw();
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