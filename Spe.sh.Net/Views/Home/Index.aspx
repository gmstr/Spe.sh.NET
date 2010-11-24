<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Spe.sh - URL Shortener
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% using (Html.BeginForm())
       { %>
        <%=Html.TextBox("url") %>      
    <input type="submit" name="btnSubmit" value="Shorten" />
    <% } %>
    <%if(ViewData["token"] != null){ %> 
        <p>
            Your URL was created successfully (<%=Request.Url.ToString() + ViewData["token"] %>). Click <%=Html.ActionLink("here", "direct", "home", new { token = ViewData["token"] }, new { target = "_blank" }) %> to follow your link.
        </p>
    <%} %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
