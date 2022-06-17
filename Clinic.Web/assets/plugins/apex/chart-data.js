'use strict';

$(document).ready(function() {

	if($('#call-chart').length > 0) {
		var options = {
			series: [{
			name: 'Video Call',
			data: [20,50,20,60,20,50,45,40,20,60,20,80]
		  }, {
			name: 'Audio Call',
			data: [30,60,30,80,40,70,20,60,50,80,20,90]
		  }],
			chart: {
			height: 350,
			type: 'area'
		  },
		  dataLabels: {
			enabled: false
		  },
		  colors: ['#0CE0FF', '#0C4F8A'],
		  stroke: {
			curve: 'smooth'
		  },
		  legend: {
			position: 'top',
			horizontalAlign: 'right',
		  },
		 
		  xaxis: {
			type: 'datetime',
			categories: ["Jan 2022", "Feb 2022","Mar 2022","April 2022" ,"May 2022","June 2022" , "July 2022","Aug 2022","Sep 2022","Oct 2022","Nov 2022","Dec 2022"]
		  },
		  yaxis: {
			title: {
				text: 'No of Appointmment',
			  },
			axisBorder: {
				show: true,
			},
		},
		  tooltip: {
			x: {
			  format: 'dd/MM/yy HH:mm'
			},
		  },
		  };
  
		  var chart = new ApexCharts(document.querySelector("#call-chart"), options);
		  chart.render();
	}

	if($('#earnings-chart').length > 0) {
		var options = {
			series: [{
			name: 'Total Earnings',
			data: [88,88.1,88.5,88.6,88.8,89,90]
		  }],
			chart: {
			height: 315,
			type: 'area'
		  },
		  dataLabels: {
			enabled: false
		  },
		  colors: ['#0C4F8A'],
		  stroke: {
			curve: 'smooth'
		  },
		  xaxis: {
			type: 'month',
			categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
		  },
		  yaxis:{
			labels: {
				formatter: function (val) {
					return val + "k"
				}
			},
		  },
		  tooltip: {
			x: {
			  format: 'dd/MM/yy HH:mm'
			},
			y: {
				formatter: function (val) {
					return "$" + val + "k"
				}
			}
		  },
		  };
  
		  var chart = new ApexCharts(document.querySelector("#earnings-chart"), options);
		  chart.render();
		  
	}
});