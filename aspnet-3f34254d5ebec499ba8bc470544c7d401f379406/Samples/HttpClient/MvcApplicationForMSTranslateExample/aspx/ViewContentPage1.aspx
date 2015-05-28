<%@ Page Language="C#" MasterPageFile="~/aspx/Shared/Layout.Master" AutoEventWireup="true" CodeBehind="ViewContentPage1.aspx.cs" Inherits="MvcApplicationForMSTranslateExample.ViewContentPage1" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
                <h2>(old) style aspx under pinnings</h2>
            </hgroup>
            <p>
            </p>
        </div>
    </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    this is my special page
</asp:Content>
