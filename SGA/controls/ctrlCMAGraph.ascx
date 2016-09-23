<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlCMAGraph.ascx.cs" Inherits="SGA.controls.ctrlCMAGraph" %>
<script type="text/javascript" src="https://www.google.com/jsapi"></script>
<script type="text/javascript">
    google.load("visualization", "1", { packages: ["corechart"] });
    google.setOnLoadCallback(drawChart);
    function drawChart() {
        var data = google.visualization.arrayToDataTable([
	      ['Topic Name', '', { role: 'style' }, { role: 'tooltip' }],
    ['<%= topic1name %>', <%= topic1mark %>, '#F89F5A', '<%= topic1name %>' + "\r\n" + <%= topic1mark %> +"%"],            // RGB value
	    ['<%= topic2name %>', <%= topic2mark %>, '#7B7C7F', '<%= topic2name %>' + "\r\n" + <%= topic2mark %> +"%"],            // English color name
	    ['<%= topic3name %>', <%= topic3mark %>, '#F89F5A', '<%= topic3name %>' + "\r\n" + <%= topic3mark %> +"%"],
	    ['<%= topic4name %>', <%= topic4mark %>, '#7B7C7F', '<%= topic4name %>' + "\r\n" + <%= topic4mark %> +"%"], // CSS-style declaration
        ['<%= topic5name %>', <%= topic5mark %>, '#F89F5A', '<%= topic5name %>' + "\r\n" + <%= topic5mark %> +"%"], // CSS-style declaration 
           ['<%= topic6name %>', <%= topic6mark %>, '#7B7C7F', '<%= topic6name %>' + "\r\n" + <%= topic6mark %> +"%"],
	    ['<%= topic7name %>', <%= topic7mark %>, '#F89F5A', '<%= topic7name %>' + "\r\n" + <%= topic7mark %> +"%"], // CSS-style declaration
        ['<%= topic8name %>', <%= topic8mark %>, '#7B7C7F', '<%= topic8name %>' + "\r\n" + <%= topic8mark %> +"%"] // CSS-style declaration 
      ]);
        var options = {
            title: ' ',
            hAxis: { title: ' ', titleTextStyle: { color: 'red'} },
            legend: {position: 'none'}
        };
       
        var chart = new google.visualization.ColumnChart(document.getElementById('chart_div'));
        chart.draw(data, options);
    }
    </script>
<div id="chart_div" style="width: 725px; height: 500px;"></div>