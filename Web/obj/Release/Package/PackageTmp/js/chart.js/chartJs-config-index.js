/* Users statistics */
var ctx = document.getElementById('users').getContext('2d');
var data = {
	labels: ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"],
	datasets: [
	{
		label: "Total users",
		backgroundColor: "rgba(255, 255, 255, 0.0)",
		borderColor: "rgba(76, 132 ,255 ,1)",
		pointBorderColor: "rgba(255, 255, 255, 0.0)",
		pointBackgroundColor: "rgba(76, 132, 255, 1)",
		pointRadius: 0,
		data: [15352, 15796, 15219, 15516, 15219, 15633]
	},

	]
};
		
var areaChart = new Chart(ctx, {
	type:"line",

	data:data,
			
	options: {
		layout: {
            padding: {
                left: -10,
                right: 0,
                top: 10,
                bottom: 0
            }
        },
		tooltips: {
			mode:"label"
		},
		scaleLineColor: 'transparent',
		elements:{
			point: {
				hitRadius:90
			},
		},
				
		scales: {

		yAxes: [{
			gridLineColor: 'transparent',
			gridLines: {
                        lineWidth: 0,
                        zeroLineColor: "rgba(255,255,255,0)",
                        color: "rgba(255,255,255,0)"
            },
        	ticks: {
                 display: false
            },
		}],
			xAxes: [{
				stacked: true,
				gridLines: {
					display: false,
					color: "rgba(255,255,255,0)"
				},
				ticks: {
					display: false
				},
			}]
		},
		animation: {
			duration: 1500
		},
		responsive: true,
		legend: {
			display: false,
		},
		tooltips: {
			backgroundColor:"rgba(76, 132 ,255 ,0.6)",
			cornerRadius:0,
			footerFontFamily:"Roboto",
			cornerRadius: 3,
		},		
	}
});

/* Total Sales */
var ctx = document.getElementById('sales').getContext('2d');
var data = {
	labels: ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"],
	datasets: [
	{
		label: "Number of sales",
		backgroundColor: "rgba(255, 255, 255, 0.0)",
		borderColor: "#1ce3bb",
		pointBorderColor: "rgba(255, 255, 255, 0.0)",
		pointBackgroundColor: "#1ce3bb",
		pointRadius: 0,
		data: [13852, 13496, 13619, 13416, 13719, 13703]
	},

	]
};
		
var areaChart = new Chart(ctx, {
	type:"line",

	data:data,
			
	options: {
		layout: {
            padding: {
                left: -10,
                right: 0,
                top: 10,
                bottom: 0
            }
        },
		tooltips: {
			mode:"label"
		},
		scaleLineColor: 'transparent',
		elements:{
			point: {
				hitRadius:90
			},
		},
				
		scales: {

		yAxes: [{
			gridLineColor: 'transparent',
			gridLines: {
                        lineWidth: 0,
                        zeroLineColor: "rgba(255,255,255,0)",
                        color: "rgba(255,255,255,0)"
            },
        	ticks: {
                 display: false
            },
		}],
			xAxes: [{
				stacked: true,
				gridLines: {
					display: false,
					color: "rgba(255,255,255,0)"
				},
				ticks: {
					display: false
				},
			}]
		},
		animation: {
			duration: 1500
		},
		responsive: true,
		legend: {
			display: false,
		},
		tooltips: {
			backgroundColor:"rgba(24, 206, 169, 0.8)",
			cornerRadius:0,
			footerFontFamily:"Roboto",
			cornerRadius: 3,
		},		
	}
});

/* Avg. Rating */
var ratingGrowthData = {
    labels: [
        "Happy", "Sad"
    ],
    datasets: [
        {
            data: [86, 14],
            backgroundColor: [
                "rgba(76, 132 ,255 ,1)",
                "rgba(191, 197, 209, 0.2)"
            ],
            borderWidth: [
                0, 0
            ]
    }]
};

var ratingGrowthDataOpt= {
    circumference: Math.PI,
    rotation: 1.0 * Math.PI,
    cutoutPercentage: 94,
    animation: {
        animationRotate: true,
        duration: 2000
    },
    legend: {
        display: false
    },
    tooltips: {
		enabled: false
    },
    responsive: true,
};

var chart = new Chart($('#ratingGrowth'), {
    type: 'doughnut',
    data: ratingGrowthData,
    options: ratingGrowthDataOpt
});

/* Productivity member */
var ctx = document.getElementById('productivity').getContext('2d');
var data = {
	labels: ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"],
	datasets: [
	{
		label: "Total users",
		backgroundColor: "rgba(255, 255, 255, 0.0)",
		borderColor: "rgba(76, 132 ,255 ,1)",
		pointBorderColor: "rgba(0, 0, 0, 0.0)",
		pointBackgroundColor: "rgba(0, 0, 0, 0.0)",
		pointRadius: 0,
		data: [15352, 15796, 15219, 15516, 15219, 15833]
	},

	]
};
		
var areaChart = new Chart(ctx, {
	type:"line",

	data:data,
			
	options: {
		layout: {
            padding: {
                left: -10,
                right: 0,
                top: 10,
                bottom: 0
            }
        },
		tooltips: {
			mode:"label"
		},
		scaleLineColor: 'transparent',
		elements:{
			point: {
				hitRadius:90
			},
		},
				
		scales: {

		yAxes: [{
			gridLineColor: 'transparent',
			gridLines: {
                        lineWidth: 0,
                        zeroLineColor: "rgba(255,255,255,0)",
                        color: "rgba(255,255,255,0)"
            },
        	ticks: {
                 display: false
            },
		}],
			xAxes: [{
				stacked: true,
				gridLines: {
					display: false,
					color: "rgba(255,255,255,0)"
				},
				ticks: {
					display: false
				},
			}]
		},
		animation: {
			duration: 1500
		},
		responsive: true,
		legend: {
			display: false,
		},
		tooltips: {
			backgroundColor:"rgba(76, 132 ,255 ,0.6)",
			cornerRadius:0,
			footerFontFamily:"Roboto",
			cornerRadius: 3,
		},		
	}
});

/* Completed tasks */
var tasksCompletedData = {
    labels: [
        "Finished", "Unfinished"
    ],
    datasets: [
        {
            data: [45, 55],
            backgroundColor: [
                "rgba(76, 132 ,255 ,1)",
                "rgba(191, 197, 209, 0.2)"
            ],
            borderWidth: [
                0, 0
            ]
    }]
};

var tasksCompletedOpt= {
    circumference: Math.PI,
    rotation: 1.0 * Math.PI,
    cutoutPercentage: 94,
    animation: {
        animationRotate: true,
        duration: 2000
    },
    legend: {
        display: false
    },
    tooltips: {
		enabled: true
    },
    responsive: false,
};

var chart = new Chart($('#tasksCompleted'), {
    type: 'doughnut',
    data: tasksCompletedData,
    options: tasksCompletedOpt
});

/* Completed tasks - DUE */
var tasksDueData = {
    labels: [
        "Finished", "Unfinished"
    ],
    datasets: [
        {
            data: [73, 27],
            backgroundColor: [
                "rgba(28, 227, 187 ,1)",
                "rgba(191, 197, 209, 0.2)"
            ],
            borderWidth: [
                0, 0
            ]
    }]
};

var tasksDueOpt= {
    circumference: Math.PI,
    rotation: 1.0 * Math.PI,
    cutoutPercentage: 94,
    animation: {
        animationRotate: true,
        duration: 2000
    },
    legend: {
        display: false
    },
    tooltips: {
		enabled: true
    },
    responsive: false,
};

var chart = new Chart($('#tasksDue'), {
    type: 'doughnut',
    data: tasksDueData,
    options: tasksDueOpt
});

/* Completed tasks - Assigned */
var tasksAssignedData = {
    labels: [
        "Finished", "Unfinished"
    ],
    datasets: [
        {
            data: [100, 0],
            backgroundColor: [
                "rgba(255, 70, 131 ,1)",
                "rgba(191, 197, 209, 0.2)"
            ],
            borderWidth: [
                0, 0
            ]
    }]
};

var tasksAssignedOpt= {
    circumference: Math.PI,
    rotation: 1.0 * Math.PI,
    cutoutPercentage: 94,
    animation: {
        animationRotate: true,
        duration: 2000
    },
    legend: {
        display: false
    },
    tooltips: {
		enabled: true
    },
    responsive: false,
};

var chart = new Chart($('#tasksAssigned'), {
    type: 'doughnut',
    data: tasksAssignedData,
    options: tasksAssignedOpt,
});

/* tasksProductivity-chart */
var ctx1 = document.getElementById('tasksProductivity').getContext('2d');
var data2 = {
	labels: ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"],
	datasets: [
	{
		label: "Total users",
		backgroundColor: "rgba(255, 255, 255, 0.0)",
		borderColor: "rgba(76, 132 ,255 ,1)",
		pointBorderColor: "rgba(0, 0, 0, 0.0)",
		pointBackgroundColor: "rgba(0, 0, 0, 0.0)",
		pointRadius: 0,
		data: [15352, 15496, 15619, 15516, 15219, 15833]
	},

	]
};
		
var areaChart = new Chart(ctx1, {
	type:"line",

	data:data,
			
	options: {
		layout: {
            padding: {
                left: -10,
                right: 0,
                top: 10,
                bottom: 0
            }
        },
		tooltips: {
			mode:"label"
		},
		scaleLineColor: 'transparent',
		elements:{
			point: {
				hitRadius:90
			},
		},
				
		scales: {

		yAxes: [{
			gridLineColor: 'transparent',
			gridLines: {
                        lineWidth: 0,
                        zeroLineColor: "rgba(255,255,255,0)",
                        color: "rgba(255,255,255,0)"
            },
        	ticks: {
                 display: false
            },
		}],
			xAxes: [{
				stacked: true,
				gridLines: {
					display: false,
					color: "rgba(255,255,255,0)"
				},
				ticks: {
					display: false
				},
			}]
		},
		animation: {
			duration: 1500
		},
		responsive: true,
		legend: {
			display: false,
		},
		tooltips: {
			backgroundColor:"rgba(76, 132 ,255 ,0.6)",
			cornerRadius:0,
			footerFontFamily:"Roboto",
			cornerRadius: 3,
		},		
	}
});

