$('#world-map').vectorMap({
	map: 'world_mill_en',
	backgroundColor: 'transparent',
	hoverOpacity: .5,
	regionStyle:{
		initial:{fill:"#e8efff"}
	},
	markerStyle:{
		initial:{
			r:9,
			fill:"#4c84ff",
			stroke:"#fff","stroke-width":7,"stroke-opacity":.4
		},
		hover:{
			stroke:"#fff","fill-opacity":1,"stroke-width":1.5
		}
	},
	markers:[
		{latLng:[45.26,19.83],name:"Novi Sad"},
		{latLng:[43.73,7.41],name:"Monaco"},
		{latLng:[19.4326, -99.1332],name:"Mexico City"},
		{latLng:[-12.0464, -77.0428],name:"Lima"},
		{latLng:[43.93,12.46],name:"San Marino"},
		{latLng:[39.904, 116.407],name:"Beijing"},
		{latLng:[44.7489, 19.6908],name:"Sabac"},
		{latLng:[30.0444, 31.235],name:"Cairo"},
		{latLng:[41.3851, -2.1734],name:"Barcelona"},
		{latLng:[37.7749, -122.4194],name:"San Francisco"},
		{latLng:[-37.8136, 144.9631],name:"Melbourne"},
		{latLng:[1.3,103.8],name:"Singapore"},
		]
});

