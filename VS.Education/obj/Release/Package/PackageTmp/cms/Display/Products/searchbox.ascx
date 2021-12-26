<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="searchbox.ascx.cs" Inherits="VS.E_Commerce.cms.Display.Products.searchbox" %>
<link href="/Resources/Timkiem/jquery-ui.css" rel="stylesheet" type="text/css" />
<script src="/Resources/Timkiem/jquery-ui.min.js" type="text/javascript"></script>
<asp:Panel ID="SearchBox" runat="server" DefaultButton="lnksearch">
<asp:TextBox OnLoad="Text_Load" ID="txtkeyword" class="txt" runat="server"></asp:TextBox>
<asp:Button ID="lnksearch" class="bt-search" runat="server"  onclick="lnksearch_Click" />
</asp:Panel>

 <asp:LinkButton ID="lnkVIE" runat="server" OnClick="lnkVIE_Click"><img src="/Resources/img/vn.jpg" style="margin-right:5px;" /></asp:LinkButton>
 <asp:LinkButton ID="lnkEnglish" runat="server" OnClick="lnkEnglish_Click"><img src="/Resources/img/en.jpg" /></asp:LinkButton>
  <%--<script>
      $(function () {
          $("[id$=txtkeyword]").autocomplete({
              source: function (request, response) {
                  $.ajax({
                      url: '<%=ResolveUrl("~/index.aspx/GetCustomers") %>',
                      data: "{ 'prefix': '" + request.term + "','condition': '' }",
                      dataType: "json",
                      type: "POST",
                      contentType: "application/json; charset=utf-8",
                      success: function (data) {
                          response($.map(data.d, function (item) {
                              return {
                                  label: item.split('-')[0],
                                  val: item.split('-')[1]
                              }
                          }))
                      },
                      error: function (response) {
                          //  alert(response.responseText);
                      },
                      failure: function (response) {
                          // alert(response.responseText);
                      }
                  });
              },
              select: function (e, i) {
                  $("[id$=hfCustomerId]").val(i.item.val);
              },
              minLength: 1
          });
      });
</script>--%>


<script>
    $(function () {
        $("[id$=txtkeyword]").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: '<%=ResolveUrl("~/index.aspx/GetAutocomplete") %>',
                    data: "{ 'prefix': '" + request.term + "','condition': '' }",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                label: item.Name,
                                val: item.TangName,
                                image: item.ImagesSmall,
                                price: item.Price,
                                oldprice: item.OldPrice
                            }
                        }))
                    },
                    error: function (response) {
                        // alert(response.responseText);
                    },
                    failure: function (response) {
                        // alert(response.responseText);
                    }
                });
            },
            select: function (e, i) {

            },
            minLength: 1
        })
        .autocomplete("instance")._renderItem = function (ul, item) {
            return $("<li>")
                   .append("<div><a href='" + item.val + ".html' title='" + item.label + "'><div class='auto-img'><img src='" + item.image + "' alt='" + item.label + "' /></div><div class='auto-name'><h3>"
                            + item.label + "</h3></div><br><span class='auto-price'>" + item.price + "</span><span class='auto-oldprice'>" + item.oldprice + "</span></a></div>")
                   .appendTo(ul);
        };
    });
</script>