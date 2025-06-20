<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Formularios/Principal.Master" CodeBehind="catUsuarios.aspx.cs" Inherits="SACI_MEX.Formularios.catUsuarios" EnableEventValidation="true" ValidateRequest="false" EnableViewStateMac="false" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>



<asp:Content ID="contPrinc1" ContentPlaceHolderID="head" runat="server">
    <style>
        .negativeBar {
            background-color: #E8E8E8;
        }

        .pwdBlankBar .positiveBar {
            width: 0%;
        }

        .pwdBlankBar .negativeBar {
            width: 100%;
        }

        .pwdWeakBar .positiveBar {
            background-color: Red;
            width: 30%;
        }

        .pwdWeakBar .negativeBar {
            width: 70%;
        }

        .pwdFairBar .positiveBar {
            background-color: #FFCC33;
            width: 65%;
        }

        .pwdFairBar .negativeBar {
            width: 35%;
        }

        .pwdStrengthBar .positiveBar {
            background-color: Green;
            width: 100%;
        }

        .pwdStrengthBar .negativeBar {
            width: 0%;
        }
    </style>
    <script type="text/javascript">

       <%-- $(document).ready(function () {
            var TXTCVE = document.getElementById('<%= TXT_CVE_ACTIVIDAD.ClientID %>')
            TXTCVE.focus();
        });--%>

        function ShowInfo() {
            var x = document.getElementById("myDIV");
            if (x.style.display === "none") {
                x.style.display = "block";
            } else {
                x.style.display = "none";
            }
        }

        <%--   function ActivaCve() {

            $('#modalActividad').on('shown.bs.modal', function () {
                var TXTCVE = document.getElementById('<%= TXT_CVE_ACTIVIDAD.ClientID %>')
                TXTCVE.focus()
            });
        }


        function LimpiaControles() {
            var TXTCVE = document.getElementById('<%= TXT_CVE_ACTIVIDAD.ClientID %>')
        var TXTNOM = document.getElementById('<%= TXT_NOM_ACTIVIDAD.ClientID %>')
        TXTCVE.value = '';
        TXTNOM.value = '';
    }--%>


        //INICIO - Validacion de contraseña
        var minPwdLength = 10;
        var strongPwdLength = 10;

        function UpdateIndicator() {
            var strength = GetPasswordStrength(document.getElementById('MainContent_TXTPWD_USER').value);

            var className;
            var message;
            if (strength == -1) {
                className = 'pwdBlankBar';
                message = "Vacía";
            } else if (strength == 0) {
                className = 'pwdBlankBar';
                message = "Corta";
            } else if (strength <= 0.6666) {
                className = 'pwdWeakBar';
                message = "Débil";
            } else if (strength <= .9999) {
                className = 'pwdFairBar';
                message = "Media";
            } else {
                className = 'pwdStrengthBar';
                message = "Alta";
            }

            // update css and message
            var bar = document.getElementById("PasswordStrengthBar");
            bar.className = className;
            lbMessagePassword.SetValue(message);
        }
        function GetPasswordStrength(password) {
            if (password.length == 0) return -1;
            if (password.length < minPwdLength) return 0;

            var rate = 0;
            if (password.length >= strongPwdLength) rate++;
            if (password.match(/[0-9]/)) rate++;
            if (password.match(/[A-Z]/)) rate++;
            if (password.match(/[!,@,#,$,%,/,&,*,?,_,¿,\-,(,),¡,\[,\],+,=,\,,<,>,:,;]/)) rate++;
            return rate / 4;
        }

        function Valida() {
            var errores = false;

            //VALIDA CLAVE             
            if (document.getElementById('<%=TXT_CVE_USER.ClientID%>').value.length == 0) {
                var label = document.getElementById('<%=lblClave.ClientID%>');
                label.style.color = "red";
                errores = true;
            }
            else {
                var label = document.getElementById('<%=lblClave.ClientID%>');
                label.style.color = "black";
            }

            //VALIDA PWD             
            if (document.getElementById('<%=TXTPWD_USER.ClientID%>').value.length == 0) {
                var label = document.getElementById('<%=LBLpWD.ClientID%>');
                label.style.color = "red";
                errores = true;
            }
            else {
                var label = document.getElementById('<%=LBLpWD.ClientID%>');
                label.style.color = "black";
            }

            //VALIDA PERFIL             
            if (cmbPerfil.GetValue() == null) {
                var label = document.getElementById('<%=lblPerfil.ClientID%>');
                label.style.color = "red";
                errores = true;
            }
            else {
                var label = document.getElementById('<%=lblPerfil.ClientID%>');
                label.style.color = "black";
            }

            //VALIDA CHEK SMS
            var celular = TXT_TEL.GetValue();
            if (chk_SMS.GetChecked() && celular.toString().length == 0) {
                var label = document.getElementById('<%=lblCelular.ClientID%>');
                label.style.color = "red";
                errores = true;
            }
            else {
                var label = document.getElementById('<%=lblCelular.ClientID%>');
                label.style.color = "black";
            }



            //VALIDA CONTRASEÑA
            var validaPwd = 0;

            var strength = GetPasswordStrength(document.getElementById('MainContent_TXTPWD_USER').value);
            if (strength == 1) {
                validaPwd = 1;
            }
            else {
                validaPwd = 0;
            }

            //Se muestran errores si existen
            if (errores) {
                alert('Debe agregar los campos obligatorios.');
            }
            else {
                if (validaPwd == 1) {
                    $('#modalUsuario').modal('hide');
                    document.getElementById('MainContent_BtnValida').click();
                }
                else {
                    alert('La contraseña no cumple con las caracteristicas requeridas')
                }
            }

        }

        //FIN- Validacion de contraseña
        /*Ajustar el tamaño del grvRef al tamaño de la pantalla */
        function OnInitGridUsuarios(s, e) {
            var height = Math.max(0, document.documentElement.clientHeight * 0.70);

            gridUsuario.SetHeight(height);
        }


        var textSeparator = "|";
        function updateText() {
            var selectedItems = checkListBox.GetSelectedItems();
            checkComboBox.SetText(getSelectedItemsText(selectedItems));
        }
        function synchronizeListBoxValues(dropDown, args) {
            checkListBox.UnselectAll();
            var texts = dropDown.GetText().split(textSeparator);
            var values = getValuesByTexts(texts);
            checkListBox.SelectValues(values);
            updateText(); // for remove non-existing texts
        }
        function getSelectedItemsText(items) {
            var texts = [];
            for (var i = 0; i < items.length; i++)
                texts.push(items[i].text);
            return texts.join(textSeparator);
        }
        function getValuesByTexts(texts) {
            var actualValues = [];
            var item;
            for (var i = 0; i < texts.length; i++) {
                item = checkListBox.FindItemByText(texts[i]);
                if (item != null)
                    actualValues.push(item.value);
            }
            return actualValues;
        }

    </script>
</asp:Content>




<asp:Content ID="contPrinc2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="HfValidaCorreo" runat="server" />
    <div style="height: 40px"></div>
    <div class="container-fluid">
        <div class="container-fluid">
            <div class="panel-body bordes_curvos" style="background-color: #f8f8f8">

                <%--<h1 id="h1_titulo" runat="server" class="panel-title">Usuarios</h1>--%>
                <div class="divCard tcentrado">
                    <h1 id="h1_titulo" runat="server" class="panel-title">Usuarios</h1>
                </div>
                <asp:LinkButton ID="lkb_Nuevo" runat="server"  CssClass="btn btn-info btn-sm  btn-height" OnClick="lkb_Nuevo_Click">
                    <span class="fa fa-plus"></span>&nbsp;Agregar
                </asp:LinkButton>
                <asp:LinkButton ID="lkb_Editar" runat="server" CssClass="btn btn-info btn-sm btn-height" OnClick="lkb_Editar_Click">
                    <span class="fa fa-edit"></span>&nbsp;Editar
                </asp:LinkButton>
                <asp:LinkButton ID="lkb_Eliminar" runat="server" CssClass="btn btn-info btn-sm btn-width btn-height" OnClick="lkb_Eliminar_Click">
                    <span class="fa fa-times"></span>&nbsp;Borrar
                </asp:LinkButton>
                <asp:LinkButton ID="lkb_Excel" runat="server" CssClass="btn btn-info btn-sm btn-width btn-height" OnClick="lkb_Excel_Click">
                        <span class="fa fa-expand"></span>&nbsp;Excel
                </asp:LinkButton>
                <br />

                <dx:ASPxGridView ID="grvUsuarios" ClientInstanceName="gridUsuario" runat="server" KeyFieldName="PK_ID_USER"
                    Width="100%" AutoGenerateColumns="False" Settings-HorizontalScrollBarMode="Auto" Theme="DevEx" Styles-Cell-CssClass="grid_content"
                    EnableCallBacks="true" Styles-Header-ForeColor="#751473" Styles-Header-Font-Size="11px">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="PK_ID_USER" FieldName="PK_ID_USER" Visible="false" />
                        <dx:GridViewDataTextColumn FieldName="VIGENCIA" visible="false"/>

                        <dx:GridViewDataTextColumn Caption="Clave" Width="180px" FieldName="CVE_USER">
                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                            <CellStyle HorizontalAlign="Center"></CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="PWD_USER" Width="200px" FieldName="PWD_USER" Visible="false">
                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Nombre" Width="350px" FieldName="NOM_USER">
                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                            <CellStyle HorizontalAlign="Center"></CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="E-Mail" Width="350px" FieldName="DES_MAIL_USER">
                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Vigencia" Width="120px" FieldName="VIGENCIA_TXT">
                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                            <CellStyle HorizontalAlign="Center"></CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Autentifica Correo" FieldName="AC" Width="140px">
                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                            <CellStyle HorizontalAlign="Center"></CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Estatus" Width="290px" FieldName="STA_USER" Visible="false">
                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="FK_ID_PERFIL_USER" Width="130px" FieldName="FK_ID_PERFIL_USER" Visible="false">
                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="No. Celular" FieldName="NUMERO_CEL" Width="140px" Visible="true">
                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                            <CellStyle HorizontalAlign="Center"></CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="SMS" FieldName="SMS" Width="90px" Visible="true">
                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                            <CellStyle HorizontalAlign="Center"></CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Bloqueado" FieldName="BLOQUEADO" Width="120px" Visible="true">
                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                            <CellStyle HorizontalAlign="Center"></CellStyle>
                        </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="Plantas" FieldName="Plantas" Width="100%" Visible="true">
                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                            <CellStyle HorizontalAlign="Left"></CellStyle>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" HideDataCellsAtWindowInnerWidth="800"
                        AdaptiveDetailColumnCount="1" AllowOnlyOneAdaptiveDetailExpanded="True">
                        <AdaptiveDetailLayoutProperties ColCount="1">
                            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                        </AdaptiveDetailLayoutProperties>
                    </SettingsAdaptivity>
                    <SettingsResizing ColumnResizeMode="Control" />
                    <Settings ShowFilterRow="true" ShowFilterRowMenu="true" ShowGroupPanel="false" ShowFooter="true" />
                    <SettingsBehavior AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                    <Settings ShowFooter="False" ShowHeaderFilterButton="false" ShowFilterRowMenu="true" ShowFilterRow="true"
                        ShowGroupPanel="false" ShowVerticalScrollBar="true" />
                    <Styles>
                        <SelectedRow />
                        <Row Font-Size="11px" />
                        <AlternatingRow Enabled="True" />
                        <PagerTopPanel Paddings-PaddingBottom="3px"></PagerTopPanel>
                    </Styles>
                    <SettingsPager>
                        <PageSizeItemSettings Visible="true" Items="10, 20, 50" />
                    </SettingsPager>
                    <SettingsDataSecurity AllowInsert="false" AllowEdit="false" AllowDelete="false" />
                    <%--  <ClientSideEvents Init="grid_Init" BeginCallback="grid_BeginCallback" EndCallback="grid_EndCallback" />--%>
                    <ClientSideEvents Init="OnInitGridUsuarios" />
                    <GroupSummary>
                        <dx:ASPxSummaryItem SummaryType="Count" />
                    </GroupSummary>
                </dx:ASPxGridView>


                <dx:ASPxGridViewExporter ID="Exporter" GridViewID="grvUsuarios" runat="server" PaperKind="A5" Landscape="true" />





                <button id="btnNuevo" type="button" data-toggle="modal" data-target="#modalUsuario" style="display: none;"></button>
                <div class="modal fade" id="modalUsuario" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title" id="titNewCliente">Nuevo Usuario</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-sm-4 col-md-6">
                                        <asp:TextBox ID="TXT_PK_ID_USER" runat="server" Width="100%" Text="0" CssClass="form-control input-sm" Visible="false"></asp:TextBox>
                                        <div runat="server" id="divProv">
                                            <label id="lblClave" runat="server" class="form-text">Clave *</label>
                                            <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                <div class="input-group">
                                                    <asp:TextBox ID="TXT_CVE_USER" runat="server" Width="100%" CssClass="form-control input-sm" MaxLength="35"></asp:TextBox>
                                                </div>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TXT_CVE_USER" ValidationGroup="RequiedInfoGroup" ErrorMessage="***Falta Información" ForeColor="red" runat="Server">
                                    </asp:RequiredFieldValidator>--%>
                                                <i runat="server" id="ITXT_CVE_USER"></i>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 col-md-6">
                                        <div runat="server" id="Div1">
                                            <label id="lblNombe" runat="server" class="form-text">Nombre</label>
                                            <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                <div class="input-group">
                                                    <asp:TextBox ID="TXT_NOMBRE" runat="server" Width="150px" CssClass="form-control input-sm" MaxLength="100"></asp:TextBox>
                                                </div>
                                                <i runat="server" id="ITXT_NOMBRE"></i>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-4 col-md-6">
                                        <div runat="server">
                                            <label id="LBLpWD" runat="server" class="form-text">Contraseña *</label>
                                            <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                <div class="input-group">
                                                    <asp:TextBox ID="TXTPWD_USER" runat="server" Width="150px" CssClass="form-control input-sm" MaxLength="50" onblur="UpdateIndicator();" onKeyUp="javascript:UpdateIndicator();" ToolTip="10 caracteres mínimo. Debe contener: 1 mayúscula y 1 número" data-content="10 caracteres mínimo <br> Debe contener: <br> 1 mayúscula <br> 1 número" data-placement="bottom"></asp:TextBox>
                                                </div>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="TXTPWD_USER" ValidationGroup="RequiedInfoGroup" ErrorMessage="***Falta Información" ForeColor="red" runat="Server">
                                    </asp:RequiredFieldValidator>--%>
                                                <i runat="server" id="ITXTPWD_USER"></i>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 col-md-6">
                                        <div runat="server" id="DivColor" style="display: block;">
                                            <br />
                                            <dx:ASPxLabel ID="lbMessagePassword" ClientInstanceName="lbMessagePassword" runat="server"
                                                Text="Vacía">
                                            </dx:ASPxLabel>
                                            <table id="PasswordStrengthBar" class="pwdBlankBar" border="0" style="height: 4px; width: 195px; text-align: end">
                                                <tbody>
                                                    <tr>
                                                        <td id="PositiveBar" class="positiveBar"></td>
                                                        <td id="NegativeBar" class="negativeBar"></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-6">
                                        <div runat="server" id="Div_Vigencia">                                            
                                            <label id="Label6" runat="server" class="form-text">Vigencia Contraseña</label>                                       
                                            <dx:ASPxComboBox ID="cmbVigencia" Caption="" runat="server" Height="30px" NullText="Vigencia Contraseña" DataSecurityMode="Default" Width="100%" Font-Size="12px"
                                                Font-Names="Arial,Helvetica" CssClass="form-control input-sm" >
                                                <Items>
                                                    <dx:ListEditItem Value="0" Text="Sin vigencia" Selected="true" />
                                                    <dx:ListEditItem Value="-1" Text="Usuario nuevo" />
                                                    <dx:ListEditItem Value="14" Text="2 semanas" />
                                                    <dx:ListEditItem Value="28" Text="4 semanas" />
                                                    <dx:ListEditItem Value="56" Text="8 semanas" />
                                                    <dx:ListEditItem Value="84" Text="12 semanas" />                                                
                                                </Items>
                                            </dx:ASPxComboBox>

                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div runat="server" id="Div7">
                                            <label id="Label4" runat="server" class="form-text">Plantas</label>
                                            <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                <div class="input-group">
                                                    <dx:ASPxDropDownEdit ClientInstanceName="checkComboBox" NullText="PLANTAS" ID="dde_filtros" ClientIDMode="Static" Width="285px" runat="server" AnimationType="None">
                                                        <DropDownWindowStyle CssClass="dropDownWindow" />
                                                        <DropDownWindowTemplate>
                                                            <dx:ASPxListBox Width="100%" ID="listBox" ClientInstanceName="checkListBox" SelectionMode="CheckColumn" CssClass="listBox"
                                                                runat="server" Height="200" EnableSelectAll="true">
                                                                <Border BorderStyle="None" />
                                                                <BorderBottom BorderStyle="Solid" BorderWidth="1px" />
                                                                <ClientSideEvents SelectedIndexChanged="updateText" Init="updateText" />
                                                            </dx:ASPxListBox>
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td style="padding: 4px">
                                                                        <dx:ASPxButton ID="ASPxButton1" AutoPostBack="False" runat="server" Text="Close" Style="float: right">
                                                                            <ClientSideEvents Click="function(s, e){ checkComboBox.HideDropDown(); }" />
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </DropDownWindowTemplate>
                                                        <ClientSideEvents TextChanged="synchronizeListBoxValues" DropDown="synchronizeListBoxValues" />
                                                    </dx:ASPxDropDownEdit>
                                                </div>
                                                <i runat="server" id="I6"></i>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">

                                    <div class="col-sm-8">
                                        <div runat="server" id="Div6">
                                            <label id="lblEmal" runat="server" class="form-text">E-Mail</label>
                                            <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                <div class="input-group">
                                                    <asp:TextBox ID="TXT_DES_MAIL_USER" runat="server" Width="150px" CssClass="form-control input-sm" MaxLength="100"></asp:TextBox>
                                                </div>
                                                <i runat="server" id="ITXT_DES_MAIL_USER"></i>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4">
                                        <div runat="server" id="Div5">
                                            <label id="Label3" runat="server" class="form-text">Verificación Correo</label>
                                            <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                <div class="input-group">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td style="text-align: center">
                                                                <dx:ASPxCheckBox ID="chkMail" ClientInstanceName="chkMail" runat="server"></dx:ASPxCheckBox>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>

                                                </div>
                                                <i runat="server" id="I5"></i>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-12 col-md-12">
                                        <div runat="server" id="Div2">
                                            <label id="lblPerfil" runat="server" class="form-text">Perfil *</label>
                                            <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                <div class="input-group">
                                                    <dx:ASPxComboBox ID="CMB_PERFILES" runat="server" Width="100%" CssClass="form-control input-sm" ValueField="PK_ID_PERFIL" TextField="NOM_PERFIL" ClientInstanceName="cmbPerfil">
                                                    </dx:ASPxComboBox>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="CMB_PERFILES" ValidationGroup="RequiedInfoGroup" ErrorMessage="***Falta Información" ForeColor="red" runat="Server">
                                        </asp:RequiredFieldValidator>--%>
                                                </div>
                                                <i runat="server" id="I1"></i>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-4" > <%--style="display:none"--%>
                                        <div runat="server" id="div3">
                                            <label id="lblCelular" runat="server" class="form-text">No. Celular</label>
                                            <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                <div class="input-group">
                                                    (52)+&nbsp
                                                    <dx:ASPxSpinEdit ID="TXT_TEL" runat="server" ClientInstanceName="TXT_TEL" Width="70%" CssClass="control-text" NumberType="Integer" MaxValue="9999999999999" MaxLength="10">
                                                        <SpinButtons ShowIncrementButtons="false" ShowLargeIncrementButtons="false" />
                                                    </dx:ASPxSpinEdit>
                                                </div>

                                                <i runat="server" id="I2"></i>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4"> 
                                        <div runat="server" id="Div4">
                                            <label id="Label2" runat="server" class="form-text">Verificación SMS</label>
                                            <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                <div class="input-group">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td style="text-align: center">
                                                                <dx:ASPxCheckBox ID="chk_SMS" ClientInstanceName="chk_SMS" runat="server"></dx:ASPxCheckBox>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <i runat="server" id="I3"></i>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div runat="server" id="DivBloqueo">
                                            <label id="Label1" runat="server" class="form-text">Bloqueado</label>
                                            <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                <div class="input-group">
                                                    <dx:ASPxCheckBox ID="chk_Bloqueo" ClientInstanceName="chk_Bloqueo" runat="server">
                                                    </dx:ASPxCheckBox>
                                                </div>
                                                <i runat="server" id="I4"></i>
                                            </div>
                                        </div>
                                    </div>


                                </div>
                                



                                <%--                    <div class="row">
                        <div class="col-sm-6 col-md-12">
                            <div runat="server" id="Div2">
                                <label id="lbl_Direccion2" runat="server" class="form-text">Direccion 2</label>
                                <div class="form-group" style="position: relative; width: 100%; float: left;">
                                    <div class="input-group">
                                        <asp:TextBox ID="TXT_Direccion2" runat="server" Width="150px" CssClass="form-control input-sm" MaxLength="100"></asp:TextBox>
                                    </div>
                                    <i runat="server" id="ITXT_Direccion2"></i>
                                </div>
                            </div>
                        </div>
                    </div>--%>

                                <%--                        <div class="col-sm-6 col-md-6">
                            <div runat="server" id="Div3">
                                <label id="LBL_Patente" runat="server" class="form-text">Patente *</label>
                                <div class="form-group" style="position: relative; width: 100%; float: left;">
                                    <div class="input-group">                                        
                                         <dx:ASPxTextBox ID="TXT_Patente" runat="server" MaxLength="4" Width="100%" Height="30px" >
                                            <ClientSideEvents LostFocus="function(s,e){if(s.GetText().length<4){alert('este campo debe ser de 4 digitos'); s.Focus() }}" />                                            
                                        </dx:ASPxTextBox>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="TXT_Patente" ValidationGroup="RequiedInfoGroup" ErrorMessage="***Falta Información" ForeColor="red" runat="Server">
                                    </asp:RequiredFieldValidator>
                                    <i runat="server" id="ITXT_Patente"></i>
                                </div>
                            </div>
                        </div>--%>


                                <div class="modal-footer">
                                    <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-primary btn-sm" Text="Guardar" OnClientClick="Valida();return false;" ValidationGroup="RequiedInfoGroup" CausesValidation="true">
                                <span class="glyphicon glyphicon-ok"></span>&nbsp;&nbsp;Guardar</asp:LinkButton>
                                    <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal" onclick="LimpiaControles()">Cancelar</button>
                                    <asp:Button ID="BtnValida" runat="server" Style="display: none" OnClick="btnGuardar_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <button id="btnSucces" type="button" data-toggle="modal" data-target="#modalSucces" style="display: none;"></button>
                <!-- Central Modal Medium Success -->
                <div class="modal fade" id="modalSucces" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-notify modal-success" role="document">
                        <!--Content-->
                        <div class="modal-content">
                            <!--Header-->
                            <%--    <div class="modal-header">
                    <p class="heading lead">Aviso</p>

                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true" class="white-text">&times;</span>
                    </button>
                </div>--%>

                            <!--Body-->
                            <div class="modal-body">
                                <div class="text-center">
                                    <i class="fa fa-check fa-4x mb-3 animated rotateIn"></i>
                                    <p id="pSucces" runat="server">
                                    </p>
                                </div>
                            </div>

                            <!--Footer-->
                            <div class="modal-footer justify-content-center">
                                <a type="button" class="btn btn-success btn-sm" data-dismiss="modal">Aceptar<i class="fa ml-1 text-white"></i></a>
                            </div>
                        </div>
                        <!--/.Content-->
                    </div>
                </div>
                <!-- Central Modal Medium Success-->

                <button id="btnError" type="button" data-toggle="modal" data-target="#AlertError" style="display: none;"></button>
                <div class="modal fade bd-example-modal-lg" id="AlertError" tabindex="-1" role="dialog">
                    <div class="modal-dialog modal-lg" role="document" style="top: 12%; outline: none;">
                        <div class="modal-content">
                            <div class=" alert  alert-warning text-center" style="-webkit-box-shadow: 0 5px 15px rgba(0, 0, 0, .5); box-shadow: 0 5px 15px rgba(0, 0, 0, .5);">
                                <img src="../img/warning.PNG" width="70" height="65" class="imagen-logo-warning" />
                                <br />
                                <p id="titError" runat="server">Error inesperado.</p>
                                <br />
                                <p id="p1" runat="server" class="alert-title">Se identificó un error en el sistema, favor de contactar al administrador para más información da clic en el siguiente link.</p>
                                <dx:ASPxHyperLink ID="lnkMasInfo" runat="server" Text="Más información acerca del error" NavigateUrl="javascript:ShowInfo();"></dx:ASPxHyperLink>
                                <p id="pModal" runat="server" class="alert-title" visible="false">
                                </p>
                                <hr />
                                <div id="myDIV" style="display: none">
                                    <div class="form-group">
                                        <label for="exampleFormControlTextarea1">Detalle de Error!</label>
                                        <textarea runat="server" class="form-control" id="txtArea" rows="3" style="resize: none">                                                     
                        </textarea>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal" onclick="location.href='../Formularios/default.aspx';">Cerrar</button>
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <button id="btnErrorUser" type="button" data-toggle="modal" data-target="#AlertErrorUser" style="display: none;"></button>
                <div class="modal fade" id="AlertErrorUser" tabindex="-1" role="dialog">
                    <div class="modal-dialog modal-sm" role="document" style="top: 25%; outline: none;">
                        <div class="alert alert-danger text-center" style="-webkit-box-shadow: 0 5px 15px rgba(0, 0, 0, .5); box-shadow: 0 5px 15px rgba(0, 0, 0, .5);">
                            <span class="glyphicon glyphicon-alert ico"></span>
                            <br />
                            <br />
                            <p id="p2" runat="server" class="alert-title">
                            </p>
                            <hr />
                            <button type="button" class="btn btn-danger txt-sm" data-dismiss="modal">Aceptar</button>
                        </div>
                    </div>
                </div>


                <button id="btnQuestion" type="button" data-toggle="modal" data-target="#AlertQuestion" style="display: none;"></button>
                <div class="modal fade  modal-warning" id="AlertQuestion" tabindex="-1" role="dialog" style="top: 25%; outline: none;">
                    <div class="modal-dialog  modal-sm " role="document">
                        <div class="modal-content" style="height: 90px">
                            <div class="alert alert-warning text-center" style="-webkit-box-shadow: 0 5px 15px rgba(0, 0, 0, .5); box-shadow: 0 5px 15px rgba(0, 0, 0, .5);">

                                <span class="glyphicon glyphicon-question-sign ico"></span>

                                <br />

                                <br />

                                <p id="pModalQuestion" runat="server" class="alert-title">
                                </p>

                                <hr />
                                <asp:LinkButton ID="btnAceptarDel" runat="server" CssClass="btn btn-primary btn-sm txt-sm" OnClick="btnAceptarDel_Click">
                    <span class="glyphicon glyphicon-plus"></span>&nbsp;&nbsp;Aceptar
                                </asp:LinkButton>
                                <button id="btnCancel" runat="server" class="btn btn-secondary btn-sm" data-dismiss="modal">Cancelar</button>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <div style="height: 100px"></div>
</asp:Content>
