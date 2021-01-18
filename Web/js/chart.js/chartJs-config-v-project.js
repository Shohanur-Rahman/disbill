/* Users statistics */
var ctx = document.getElementById('v-project').getContext('2d');
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
		data: [15219, 15633, 15352, 15796, 15719, 15851,]
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

