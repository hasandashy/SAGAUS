<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlCMCGraph.ascx.cs" Inherits="SGA.controls.ctrlCMCGraph" %>
<script type="text/javascript" src="https://www.google.com/jsapi"></script>
<script type="text/javascript">
    google.load("visualization", "1", { packages: ["corechart"] });
    //google.setOnLoadCallback(drawChart);
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
                hAxis: { title: ' ', titleTextStyle: { color: 'red' } },
                legend: { position: 'none' }
            };

            var chart = new google.visualization.ColumnChart(document.getElementById('chart_div'));
            chart.draw(data, options);
        }

        function drawChartAverage() {

            var data = google.visualization.arrayToDataTable([
              ['Topic', 'My Score', '<%= median %>'],
          ['<%= topic1name %>', <%= topic1mark %>, <%= medain1 %>],
          ['<%= topic2name %>', <%= topic2mark %>, <%= medain2 %>],
          ['<%= topic3name %>', <%= topic3mark %>, <%= medain3 %>],
          ['<%= topic4name %>', <%= topic4mark %>, <%= medain4 %>],
          ['<%= topic5name %>', <%= topic5mark %>, <%= medain5 %>],
          ['<%= topic6name %>', <%= topic6mark %>, <%= medain6 %>],
          ['<%= topic7name %>', <%= topic7mark %>, <%= medain7 %>],
          ['<%= topic8name %>', <%= topic8mark %>, <%= medain8 %>]
        ]
        );
        var options = {
            title: ' ',
            hAxis: { title: ' ', titleTextStyle: { color: 'red' } },
            seriesType: "bars",
            series: { 5: { type: "line" } },
            legend: { position: 'right' },
            colors: ['#EA4320', '#7B7C7F']
        };

        var chart = new google.visualization.ColumnChart(document.getElementById('chart_div'));
        chart.draw(data, options);
    }
</script>
<table width="100%" border="0" cellspacing="1" cellpadding="1" id="tblCompare" runat="server" class="tform">
    <tr>
        <td width="30%" class="txtrht">Compare results againest:
        </td>
        <td width="45%">
            <asp:RadioButtonList ID="rdlQuartile" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Average" Value="4"></asp:ListItem>
                <asp:ListItem Text="Lower Quartile" Value="1"></asp:ListItem>
                <asp:ListItem Text="Median" Selected="True" Value="2"></asp:ListItem>
                <asp:ListItem Text="Upper Quartile" Value="3"></asp:ListItem>
            </asp:RadioButtonList>

        </td>
        <td width="25%" class="txtlht">
            <asp:LinkButton ID="lnkCompare" runat="server" CausesValidation="false" Text="Compare"
                CssClass="rdbut" OnClick="lnkCompare_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td colspan="3">&nbsp;</td>
    </tr>
    <tr>
        <td colspan="3"></td>
    </tr>
</table>
<div id="chart_div" style="width: 725px; height: 500px;">
</div>
