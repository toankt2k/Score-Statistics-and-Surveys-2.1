$(document).ready(function () {
    var table = $("#table_id_course-subject").DataTable({
        "processing": true,
    })
    handleChanged()
    handleSubmit()
    //(function () {
    //    $.ajax({
    //        url: '../Mark/test',
    //        datatype: 'json',
    //        type: 'get',
    //        // other AJAX settings goes here
    //        // ..
    //        success: function (data) {
    //            console.log(data)
    //            Marks.setData(data.list)
    //            showDataSubmit("a")
    //        },
    //        error: function (xhr, ajaxOptions, thrownError) {
    //            console.log(xhr);
    //            console.log(ajaxOptions);
    //            console.log(thrownError);
    //        }
    //    })
    //}())
})
var ok = false
const Marks = (function () {
    var data = []
    return {
        setData(mark) {
            data = [...mark]
        },
        getData() {
            return data
        }
    }
}())
const Charts = (function () {
    var data = []
    return {
        setData(mark) {
            data = [...mark]
        },
        getData() {
            return data
        }
    }
}())
function handleChanged() {
    $('#startYear').on('change', function() {
        const startYear = this.value
        const endYear = document.querySelector('#endYear').value
        
        if (startYear && endYear){
            setSemester(startYear, endYear)
        }
        else setDefaultSemester()
    })
    $('#endYear').on('change', function () {
        const endYear = this.value
        const startYear = document.querySelector('#startYear').value
        
        if (startYear && endYear) {
            setSemester(startYear, endYear)
        }
        else setDefaultSemester()
    })
}
function setSemester(startYear, endYear) {
    $.ajax({
        url: '../Mark/getSemester',
        type: 'post',
        dataType: 'json',
        data: {
            startYear: startYear,
            endYear: endYear
        },
        success: function (data) {
            if (data.code === 200) {
                setDefaultSemester()
                let element = data.semester.map(item => {
                    return `<option value = ${item.id}> ${item.name} </option>`
                })
                element.join('')
                $('#semester').append(element)
            }
        }
    })
}
function setDefaultSemester() {
    var semester = document.querySelector('#semester')
    semester.innerHTML = `<option value> Kì học </option>`
}

function handleSubmit() {
    $('form').submit(function (even) {
        if (even.originalEvent.submitter.value == 'Hiển thị') {
            $.ajax({
                method: $(this).attr('method'),
                url: $(this).attr('action'),
                data: $(this).serialize(),
                datatype: 'json',
                type: 'post',
                // other AJAX settings goes here
                // ..

                error: function (xhr, ajaxOptions, thrownError) {
                    console.log(xhr);
                    console.log(ajaxOptions);
                    console.log(thrownError);
                    alert('Không có dữ liệu');
                }
            }).done(function (response) {

                if (response.list != null) {
                    if (response.code == 200) {
                        Marks.setData(response.list)
                        Charts.setData(response.dataChart)
                        fillDataToChart(response.list)
                        fillDataToChartPie(response.dataChart)
                        ok = true;
                        showDataSubmit()
                    }
                    else {
                        console.log(reponse.data);
                        alert('Không có dữ liệu');
                    }
                }
                else {
                    console.log("Kết nối thất bại!");
                    alert('Không có dữ liệu');
                }
            });
            event.preventDefault(); // <- avoid reloading
        }

    })
}
function showDataSubmit() {
    var dataMark = Marks.getData()
    if (dataMark != null) {
        var data = dataMark.map(data => {
            var { teacherName, ...newData } = data
            return newData
        })

        data = data.map(Object.values)
        console.log(data)
        $("#table_id_course-subject").DataTable().clear();
        $("#table_id_course-subject").DataTable().rows.add(data);
        $("#table_id_course-subject").DataTable().draw();
        $("#table_id_course-subject").show();
        $("#table_id_teacher_wrapper").hide();
        $("#table_id_enrollmentClass_wrapper").hide();

    }
}
function fillDataToChart(list) {
    let typeChart = 'horizontalBar';
    let istypeBar = false;
    let labels;
    labels = list.map(function (data) {
        return data.courseSubjectName;

    })
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
                text: 'Tỉ lệ điểm A,B,C,D'

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
function cleardata(name, istypeBar) {
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
