<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebAnalytics.ascx.cs" Inherits="VS.E_Commerce.cms.Admin.WebAnalytics.WebAnalytics" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
<table style="BORDER-COLLAPSE: collapse" cellPadding="0" width="100%" border="0">	
	<tr>
		<td width="5"></td>
		<td>
			<TABLE style="BORDER-COLLAPSE: collapse" cellPadding="0" width="100%" border="0">
				<TR>
					<TD></TD>
				</TR>
				<TR>
					<TD></TD>
				</TR>
				<TR>
					<TD>
						<%=label("l_reporttype")%>
						<asp:RadioButton id="rd_bydate" runat="server" GroupName="reporttype" Text="By Date" AutoPostBack="True" oncheckedchanged="rd_bydate_CheckedChanged"></asp:RadioButton>
						<asp:RadioButton id="rd_bymonth" runat="server" GroupName="reporttype" Text="By Month" AutoPostBack="True" oncheckedchanged="rd_bymonth_CheckedChanged"></asp:RadioButton>
						<asp:RadioButton id="rd_byyear" runat="server" GroupName="reporttype" Text="By Year" AutoPostBack="True" Visible="False" oncheckedchanged="rd_byyear_CheckedChanged"></asp:RadioButton></TD>
				</TR>
				<TR>
					<TD></TD>
				</TR>
				<TR>
					<TD>
						<asp:Panel id="pn_bydate" runat="server" Visible="False">
							<TABLE style="BORDER-COLLAPSE: collapse" cellPadding="0" width="100%" border="0">
								<TR>
									<TD><STRONG>
                                        <%=label("l_dayreport")%>
                                        </STRONG></TD>
								</TR>
								<TR>
									<TD>
                                        <%=label("l_choosemonth")%>
										<asp:DropDownList id="ddl_month" runat="server">
											<asp:ListItem Value="1">1</asp:ListItem>
											<asp:ListItem Value="2">2</asp:ListItem>
											<asp:ListItem Value="3">3</asp:ListItem>
											<asp:ListItem Value="4">4</asp:ListItem>
											<asp:ListItem Value="5">5</asp:ListItem>
											<asp:ListItem Value="6">6</asp:ListItem>
											<asp:ListItem Value="7">7</asp:ListItem>
											<asp:ListItem Value="8">8</asp:ListItem>
											<asp:ListItem Value="9">9</asp:ListItem>
											<asp:ListItem Value="10">10</asp:ListItem>
											<asp:ListItem Value="11">11</asp:ListItem>
											<asp:ListItem Value="12">12</asp:ListItem>
										</asp:DropDownList><%=label("lt_year")%>:
										<asp:DropDownList id="ddl_yearbyday" runat="server">
											<asp:ListItem Value="2010">2010</asp:ListItem>
										</asp:DropDownList>
										<asp:Button id="btn_showbydate" Font-Bold="True" Text="Show Report" runat="server" BackColor="#E0E0E0" onclick="btn_showbydate_Click"></asp:Button></TD>
								</TR>
								<TR>
									<TD></TD>
								</TR>
							</TABLE>
                            <asp:Literal id="lt_reportbydate" runat="server"></asp:Literal>
						</asp:Panel>
						<asp:Panel id="pn_bymonth" runat="server" Visible="False">
							<TABLE style="BORDER-COLLAPSE: collapse" cellPadding="0" width="100%" border="0">
								<TR>
									<TD><STRONG><%=label("l_monthreport")%></STRONG></TD>
								</TR>
								<TR>
									<TD>
                                        <%=label("l_chooseyear")%>
                                        
										<asp:DropDownList id="ddl_yearbymonth" runat="server">
											<asp:ListItem Value="2003">2003</asp:ListItem>
											<asp:ListItem Value="2004">2004</asp:ListItem>
											<asp:ListItem Value="2005">2005</asp:ListItem>
											<asp:ListItem Value="2006">2006</asp:ListItem>
											<asp:ListItem Value="2007">2007</asp:ListItem>
											<asp:ListItem Value="2008">2008</asp:ListItem>
											<asp:ListItem Value="2009">2009</asp:ListItem>
											<asp:ListItem Value="2010">2010</asp:ListItem>
										</asp:DropDownList>
										<asp:Button id="btn_showbymonth" Font-Bold="True" Text="Show Report" runat="server" BackColor="#E0E0E0" onclick="btn_showbymonth_Click"></asp:Button></TD>
								</TR>
								<TR>
									<TD>
                                        <br />
										<asp:Literal id="lt_reportbymonth" runat="server"></asp:Literal></TD>
								</TR>
							</TABLE>
						</asp:Panel>
						<asp:Panel id="pn_byyear" runat="server" Visible="False">
							<TABLE  style="BORDER-COLLAPSE: collapse" cellPadding="0" width="100%" border="0">
								<TR>
									<TD><STRONG>
                                        <%=label("l_yearreport")%>
                                        </STRONG></TD>
								</TR>
								<TR>
									<TD></TD>
								</TR>
								<TR>
									<TD>
										<asp:Literal id="lt_reportbyyear" runat="server"></asp:Literal></TD>
								</TR>
							</TABLE>
						</asp:Panel></TD>
				</TR>
			</TABLE>
		</td>
		<td width="5"></td>
	</tr>
</table>
    </ContentTemplate>
</asp:UpdatePanel>
