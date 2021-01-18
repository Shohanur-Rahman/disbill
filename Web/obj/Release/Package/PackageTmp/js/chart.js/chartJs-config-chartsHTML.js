//chart.html Configuration 
var randomScalingFactor = function(){ return Math.round(Math.random()*100)};

var barChartData = {
    labels : ["January","February","March","April","May","June","July"],
    datasets : [
        {
            label: 'Dataset 1',
            borderWidth: 1,
            backgroundColor: "#4c84ff",
            borderColor: "#4c84ff",
            pointBorderColor: "#4c84ff",
            pointBackgroundColor: "#4c84ff",
            data : [randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor()]
        },
        {
            label: 'Dataset 2',
            borderWidth: 1,
            backgroundColor: "rgba(215,227,255, 0.6)",
            borderColor: "#d7e3ff",
            pointBorderColor: "#d7e3ff",
            pointHighlightStroke: "#d7e3ff",
            data : [randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor()]
        }
    ]
}

var barChartData2 = {
    labels : ["January","February","March","April","May","June","July"],
    datasets : [
        {
            label: 'Dataset 1',
            borderWidth: 1,
            backgroundColor: "#4c84ff",
            borderColor: "#4c84ff",
            pointBorderColor: "#4c84ff",
            pointBackgroundColor: "#4c84ff",
            data : [randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor()]
        },
        {
            label: 'Dataset 2',
            borderWidth: 1,
            backgroundColor: "rgba(215,227,255, 0.6)",
            borderColor: "#d7e3ff",
            pointBorderColor: "#d7e3ff",
            pointHighlightStroke: "#d7e3ff",
            data : [randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor()]
        }
    ]
}

var barChartData3 = {
    labels : ["January","February","March","April","May","June","July"],
    datasets : [
        {
            label: 'Dataset 1',
            borderWidth: 1,
            backgroundColor: "rgba(76,132,255, 0.3)",
            borderColor: "#4c84ff",
            pointBorderColor: "#4c84ff",
            pointBackgroundColor: "#4c84ff",
            data : [randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor()]
        },
        {
            label: 'Dataset 2',
            borderWidth: 1,
            backgroundColor: "rgba(215,227,255, 0.6)",
            borderColor: "#d7e3ff",
            pointBorderColor: "#d7e3ff",
            pointHighlightStroke: "#d7e3ff",
            data : [randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor()]
        }
    ]
}

var barChartData4 = {
    labels : [],
    datasets : [
        {
            label: 'Dataset 1',
            borderWidth: 1,
            backgroundColor: "rgba(76,132,255, 0.3)",
            borderColor: "#4c84ff",
            pointBorderColor: "#4c84ff",
            pointBackgroundColor: "#4c84ff",
            data : [randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor()]
        },
        {
            label: 'Dataset 2',
            borderWidth: 1,
            backgroundColor: "rgba(215,227,255, 0.6)",
            borderColor: "#d7e3ff",
            pointBorderColor: "#d7e3ff",
            pointHighlightStroke: "#d7e3ff",
            data : [randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor()]
        }
    ]
}

var barChartData5 = {
    labels : ["January","February","March","April","May","June","July"],
    datasets : [
        {
            label: 'Dataset 1',
            borderWidth: 1,
            backgroundColor: "rgba(76,132,255, 0.3)",
            borderColor: "#4c84ff",
            pointBorderColor: "#4c84ff",
            pointBackgroundColor: "#4c84ff",
            data : [randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor()]
        },
        {
            label: 'Dataset 2',
            borderWidth: 1,
            backgroundColor: "rgba(215,227,255, 0.6)",
            borderColor: "#d7e3ff",
            pointBorderColor: "#d7e3ff",
            pointHighlightStroke: "#d7e3ff",
            data : [randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor()]
        }
    ]
}

var barChartData6 = {
    labels : ["January","February","March","April","May","June","July"],
    datasets: [{
        type: 'line',
        label: 'Dataset 1',
        borderWidth: 1,
        scaleBeginAtZero : true,
        backgroundColor: "rgba(76,132,255, 0.3)",
        borderColor: "#4c84ff",
        pointBorderColor: "#4c84ff",
        pointBackgroundColor: "#4c84ff",
        data : [0,randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor()]
    }, {
        type: 'bar',
        label: 'Dataset 2',
        borderWidth: 1,
        backgroundColor: "rgba(215,227,255, 0.6)",
        borderColor: "#d7e3ff",
        pointBorderColor: "#d7e3ff",
        pointHighlightStroke: "#d7e3ff",
        data : [randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor(),randomScalingFactor()]
    }]
}

window.onload = function(){
    "use strict";
    var ctx1 = document.getElementById("canvas-vertical").getContext("2d");
    window.myBar = new Chart(ctx1, {
        type: 'bar',
        data: barChartData,
        options: {
            responsive: true,
            legend: {
                display: false,
            },
            title: {
                display: false,
            },
            scales: {
            yAxes: [{
                gridLineColor: 'transparent',
                stacked: true,
                ticks: {
                    display: false,
                },
                gridLines: {
                    lineWidth: 0,
                    zeroLineColor: "rgba(255,255,255,0)",
                    color: "rgba(255,255,255,0)"
                },
            }],
            xAxes: [{
                gridLineColor: 'transparent',
                stacked: true,
                gridLines: {
                    display: false,
                    color: "rgba(255,255,255,0)"
            },
                ticks: {
                    display: false,
                },
            }]
        },
        }
    });
    var ctx2 = document.getElementById("canvas-horizontal").getContext("2d");
    window.myHorizontalBar = new Chart(ctx2, {
        type: 'horizontalBar',
        data: barChartData2,
        options: {
            responsive: true,
            legend: {
                display: false,
            },
            title: {
                display: false,
            },
            scales: {
            yAxes: [{
                gridLineColor: 'transparent',
                stacked: true,
                ticks: {
                    display: false,
                },
                gridLines: {
                    lineWidth: 0,
                    zeroLineColor: "rgba(255,255,255,0)",
                    color: "rgba(255,255,255,0)"
                },
            }],
            xAxes: [{
                gridLineColor: 'transparent',
                stacked: true,
                gridLines: {
                    display: false,
                    color: "rgba(255,255,255,0)"
            },
                ticks: {
                    display: false,
                },
            }]
        },
        }
    });
    var ctx3 = document.getElementById("canvas-radar").getContext("2d");
    window.myLine = new Chart(ctx3,  {
        type: 'radar',
        data: barChartData3,
        options: {
            title: {
                display: false,
            },
            scale: {
              ticks: {
                beginAtZero: true
              }
            },
            responsive: true,
            legend: {
                display: false,
            },
            title: {
                display: false,
            },
            scales: {
            yAxes: [{
                gridLineColor: 'transparent',
                stacked: true,
                ticks: {
                    display: false,
                },
                gridLines: {
                        lineWidth: 0,
                        zeroLineColor: "rgba(255,255,255,0)",
                        color: "rgba(255,255,255,0)"
                },
            }],
            xAxes: [{
                gridLineColor: 'transparent',
                stacked: true,
                gridLines: {
                    display: false,
                    color: "rgba(255,255,255,0)"
                },
                ticks: {
                    display: false,
                },
            }]
        },
        }
    });
    var ctx4 = document.getElementById("canvas-polar").getContext("2d");
    window.myPolarArea = Chart.PolarArea(ctx4,  {
        data: barChartData4,
        options: {
            responsive: true,
            legend : {
                display: false,
            },
            title: {
                display: false,
            },
            scales: {
            yAxes: [{
                gridLineColor: 'transparent',
                stacked: true,
                ticks: {
                    display: false,
                },
            gridLines: {
                        lineWidth: 0,
                        zeroLineColor: "rgba(255,255,255,0)",
                        color: "rgba(255,255,255,0)"
                },
            }],
            xAxes: [{
                gridLineColor: 'transparent',
                stacked: true,
                gridLines: {
                    display: false,
                    color: "rgba(255,255,255,0)"
            },
                ticks: {
                    display: false,
                },
            }]
        },
        }
    });
    var ctx5 = document.getElementById("canvas-basic").getContext("2d");
    window.myLine = new Chart(ctx5,  {
        type: 'line',
        data: barChartData5,
        options: {
            responsive: true,
            legend: {
                display: false,
            },
            title: {
                display: false,
            },
            scales: {
            yAxes: [{
                gridLineColor: 'transparent',
                stacked: true,
                ticks: {
                    display: false,

                },
                gridLines: {
                        lineWidth: 0,
                        zeroLineColor: "rgba(255,255,255,0)",
                        color: "rgba(255,255,255,0)"
                },
            }],
            xAxes: [{
                gridLineColor: 'transparent',
                stacked: true,
                gridLines: {
                    display: false,
                    color: "rgba(255,255,255,0)"
            },
                ticks: {
                    display: false,
                },
            }]
        },
        }
    });
    var ctx6 = document.getElementById("canvas-combo").getContext("2d");
    window.myLine = new Chart(ctx6,  {
        type: 'bar',
        data: barChartData6,
        options: {
            responsive: true,
            legend: {
                display: false,
            },
            title: {
                display: false,
            },
            scales: {
            yAxes: [{
                gridLineColor: 'transparent',
                stacked: true,
                ticks: {
                    display: false,                
                },
                gridLines: {
                        lineWidth: 0,
                        zeroLineColor: "rgba(255,255,255,0)",
                        color: "rgba(255,255,255,0)"
                },
            }],
            xAxes: [{
                gridLineColor: 'transparent',
                stacked: false,
                gridLines: {
                    display: false,
                    color: "rgba(255,255,255,0)"
            },
                ticks: {
                    display: false,      
                },
            }],
            },
        }
    });
}






