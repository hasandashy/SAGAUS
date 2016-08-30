<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlNAGraph.ascx.cs" Inherits="SGA.controls.ctrlNAGraph" %>
<script type="text/javascript" src="https://www.google.com/jsapi"></script>
<script type="text/javascript">
    google.load("visualization", "1", { packages: ["corechart"] });
    google.setOnLoadCallback(drawChart);
    function drawChart() {
        var data = google.visualization.arrayToDataTable([
	    ['Topic Name', '', { role: 'style'}],
        ['<%= topic1name %>', <%= topic1mark %>, '#F89F5A'],            // RGB value
	    ['<%= topic2name %>', <%= topic2mark %>, '#7B7C7F'],            // English color name
	    ['<%= topic3name %>', <%= topic3mark %>, '#F89F5A'],
	    ['<%= topic4name %>', <%= topic4mark %>, '#7B7C7F'], // CSS-style declaration
        ['<%= topic5name %>', <%= topic5mark %>, '#F89F5A'], // CSS-style declaration
        ['<%= topic6name %>', <%= topic6mark %>, '#7B7C7F'], // CSS-style declaration
        ['<%= topic7name %>', <%= topic7mark %>, '#F89F5A'], // CSS-style declaration
        ['<%= topic8name %>', <%= topic8mark %>, '#7B7C7F'] // CSS-style declaration
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