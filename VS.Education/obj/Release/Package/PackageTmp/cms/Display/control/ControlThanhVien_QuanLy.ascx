<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlThanhVien_QuanLy.ascx.cs" Inherits="VS.E_Commerce.cms.Display.control.ControlThanhVien_QuanLy" %>
<%@ Register Src="~/cms/Display/Nav_conten.ascx" TagPrefix="uc1" TagName="Nav_conten" %>
<%@ Register Src="~/cms/Display/Lefmenu_ThanhVien.ascx" TagPrefix="uc1" TagName="Lefmenu_ThanhVien" %>

<uc1:Nav_conten runat="server" id="Nav_conten" />
 <script type="text/javascript">
     function confirmDelete(spanChk)
     {
         var oItem = spanChk.children;
         var theBox= (spanChk.type=="checkbox")?spanChk : spanChk.children.item[0];
         var elm=<%=checkboxs.ClientID %>.getElementsByTagName("input");
                var haveChoose = new Boolean();
                haveChoose = true;
                var ok = new Boolean();
                ok == false;
                for(i=0;i<elm.length;i++)                   
                    if(elm[i].type=="checkbox")                         
                        if(elm[i].checked == haveChoose) ok = true;
                if(ok == haveChoose)
                    return confirm('Bạn muốn xóa những thông tin này ?');
                else
                    return confirm('Bạn chưa chọn thông tin nào để xóa !');                       
            }     
            function changeColor(CheckBoxObj,color) 
            {         
                if (CheckBoxObj.checked == true) 
                {
                    CheckBoxObj.parentNode.parentNode.style.backgroundColor='#e6f5de'; 
                }else
                    if (CheckBoxObj.checked == false) 
                    {
                        CheckBoxObj.parentNode.parentNode.style.backgroundColor='#FAF9FA'; 
                    }
            }    
            function SelectAllCheckboxes(spanChk){
                // Added as ASPX uses SPAN for checkbox
                var oItem = spanChk.children;
                var theBox= (spanChk.type=="checkbox") ? 
                    spanChk : spanChk.children.item[0];
                xState=theBox.checked;
                elm=theBox.form.elements;

                for(i=0;i<elm.length;i++)
                    if(elm[i].type=="checkbox" && 
                             elm[i].id!=theBox.id)
                    {
                        //elm[i].click();
                        if(elm[i].checked!=xState)
                            elm[i].click();
                        //elm[i].checked=xState;
                    } 
            } 
        </script>
      

<div class="container chitiet">
<div class="row">
    
<section class="right-content col-md-9 col-md-push-3">
<div id="checkboxs" runat="server">
<asp:PlaceHolder ID="phcontrol" runat="server"></asp:PlaceHolder>
</div>
</section>
    <uc1:Lefmenu_ThanhVien runat="server" id="Lefmenu_ThanhVien" />
</div>
</div>