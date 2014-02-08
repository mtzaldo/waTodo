<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="waTodo.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TODO - ASP .NET MVP example</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>todo</h1>
        <asp:TextBox runat="server" ID="txtDescription" placeholder="description" />
        <asp:Button runat="server" ID="btnAdd" Text="add" OnClick="bntAdd_Click"/>
        <br />
        <br />
        <asp:GridView runat="server" ID="grvTodo" AutoGenerateColumns="false"  DataKeyNames="Id" ShowHeader="false" GridLines="None"
            OnPreRender="grvTodo_PreRender"
            OnRowDeleting="grvTodo_RowDeleting" 
            OnRowDataBound="grvTodo_RowDataBound"
            OnRowEditing="grvTodo_RowEditing"
            OnRowUpdating="grvTodo_RowUpdating">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="cbDone" AutoPostBack="true" OnCheckedChanged="cbDone_CheckedChanged" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Description" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button runat="server" ID="btnEdit" Text="edit" CommandName="edit" />
                        <asp:Button runat="server" ID="btnSave" Text="save" CommandName="update" />
                        <asp:Button runat="server" ID="btnDelete" Text="delete" CommandName="delete" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                No items
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
    </form>
    <br />
    author: aldo martinez (martinez.aldo@gmail.com)
</body>
</html>
