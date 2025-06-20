<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Formularios/Principal.Master" CodeBehind="catProveedores.aspx.cs" Inherits="SACI_MEX.Formularios.catProveedores" EnableEventValidation="true" ValidateRequest="false" EnableViewStateMac="false" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>



<asp:Content ID="contPrinc1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
         <%--   var TXTCVE = document.getElementById('<%= TXT_CVE_ACTIVIDAD.ClientID %>')
            TXTCVE.focus();--%>
        });

        function ShowInfo() {
            var x = document.getElementById("myDIV");
            if (x.style.display === "none") {
                x.style.display = "block";
            } else {
                x.style.display = "none";
            }
        }

        function fn_AllowonlyNumeric(s, e) {
            var theEvent = e.htmlEvent || window.event;
            var key = theEvent.keyCode || theEvent.which;
            key = String.fromCharCode(key);
            var regex = /[0-9]/;

            if (!regex.test(key)) {
                theEvent.returnValue = false;
                if (theEvent.preventDefault)
                    theEvent.preventDefault();
            }
        }



        function Valida(s, e) {
            var errores = false;

            //VALIDA        
            if (TXT_Clave.GetValue() == null) {
                var label = document.getElementById('<%=LBL_Clave.ClientID%>');
                label.style.color = "red";
                errores = true;
            }
            else {
                var label = document.getElementById('<%=LBL_Clave.ClientID%>');
                label.style.color = "black";
            }


            if (TXT_NOMBRE.GetValue() == null) {
                var label = document.getElementById('<%=LBL_NOMBRE.ClientID%>');
                label.style.color = "red";
                errores = true;
            }
            else {
                var label = document.getElementById('<%=LBL_NOMBRE.ClientID%>');
                label.style.color = "black";
            }


            if (TXT_Tipone.GetValue() == null) {
                var label = document.getElementById('<%=LBL_Tipone.ClientID%>');
                label.style.color = "red";
                errores = true;
            }
            else {
                var label = document.getElementById('<%=LBL_Tipone.ClientID%>');
                label.style.color = "black";
            }
            //Calle y numero exterior
            if (TXT_CalleNumero.GetValue() == null) {
                var label = document.getElementById('<%=LBL_CalleNumero.ClientID%>');
                label.style.color = "red";
                errores = true;
            }
            else {
                var label = document.getElementById('<%=LBL_CalleNumero.ClientID%>');
                label.style.color = "black";
            }



            if (errores) {
                alert('Debe agregar los campos obligatorios.');
            }
            else {
                $('#modalProvedores').modal('hide');
                cbpcatProv.PerformCallback('GuardarP');
            }
        }

        /*Ajustar el tamaño del grvRef al tamaño de la pantalla */
        function OnInitGridProv(s, e) {
            var height = Math.max(0, document.documentElement.clientHeight * 0.68);

            GridPrOV.SetHeight(height);
        }
    </script>
</asp:Content>




<asp:Content ID="contPrinc2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="height: 40px"></div>
    <div class="container">
        <div class="container-fluid">
            <div class="panel-body bordes_curvos" style="background-color: #f8f8f8">

                <div class="divCard tcentrado">
                    <h1 id="h2" runat="server" class="panel-title">&nbsp;&nbsp;Catálogos</h1>
                    <h1 id="h1_titulo" runat="server" class="panel-title">&nbsp;&nbsp;Proveedores</h1>
                </div>
                <br />

                <dx:ASPxCallbackPanel ID="cbpcatProv" runat="server" ClientInstanceName="cbpcatProv" OnCallback="cbpcatProv_Callback">
                    <PanelCollection>
                        <dx:PanelContent runat="server">

                            <asp:HiddenField ID="hdnGuardar" runat="server" ClientIDMode="Static" Value="0" />
                            <asp:HiddenField ID="PKEY" Value="0" runat="server" />
                            <%--<asp:HiddenField ID="hf_Usuario" Value="0" runat="server" />--%>


                            <%--<table style="width: 100%">
                                <tr>
                                    <td style="width: 15%">
                                        <dx:ASPxGridLookup ID="cmbPlantas" runat="server" NullText="Plantas" CssClass="control-text"
                                            KeyFieldName="nombre" MultiTextSeparator="|" ToolTip="" data-toggle="tooltip" Width="100%"
                                            OnDataBound="cmbPlantas_DataBound" SelectionMode="Multiple" TextFormatString="{0}">
                                            <Columns>
                                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" ShowClearFilterButton="true" SelectAllCheckboxMode="AllPages" Width="50px" />
                                                <dx:GridViewDataTextColumn Caption="Planta" FieldName="nombre" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <GridViewProperties>
                                                <SettingsPager PageSize="8" Mode="ShowPager"></SettingsPager>
                                            </GridViewProperties>
                                        </dx:ASPxGridLookup>
                                    </td>
                                    <td style="width: 85%">
                                        <asp:LinkButton ID="lkb_Buscar" runat="server" CssClass="btn btn-info btn-sm  btn-height" OnClientClick="EventosProv('BuscarPlantas')">
                                                <span class="fa fa-search"></span>&nbsp;Buscar
                                        </asp:LinkButton>--%>

                                        <asp:LinkButton ID="lkb_Nuevo" runat="server" CssClass="btn btn-info btn-sm  btn-height" OnClientClick="EventosProv('NuevoP')">
                                            <span class="fa fa-plus"></span>&nbsp;Agregar
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lkb_Editar" runat="server" CssClass="btn btn-info btn-sm btn-height" OnClientClick="EventosProv('EditarP')">
                                            <span class="fa fa-edit"></span>&nbsp;Editar
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lkb_Eliminar" runat="server" CssClass="btn btn-info btn-sm btn-width btn-height"
                                            OnClientClick="document.getElementById('btnQuestion').setAttribute('data-whatever', ''); document.getElementById('pModalQuestion').innerHTML  = '¿Estas seguro de eliminar el registro?';  document.getElementById('btnQuestion').click(); return false">
                                                <span class="fa fa-times"></span>&nbsp;Borrar
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lkb_Excel" runat="server" CssClass="btn btn-info btn-sm btn-width btn-height" OnClick="lkb_Excel_Click">
                                            <span class="fa fa-expand"></span>&nbsp;Excel
                                        </asp:LinkButton>


                                   <%-- </td>
                                </tr>
                            </table>--%>



                            <br />

                            <dx:ASPxGridView ID="grvProveedores" ClientInstanceName="GridPrOV" runat="server" KeyFieldName="PKey"
                                Width="100%" AutoGenerateColumns="False" Settings-HorizontalScrollBarMode="Auto"
                                EnableCallBacks="true" Styles-Header-ForeColor="#751473" Styles-Header-Font-Size="11px"
                                Theme="DevEx" Styles-Cell-CssClass="grid_content">
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="ID" FieldName="PKey" Visible="false" />
                                    <dx:GridViewDataTextColumn Caption="Almacén" Width="130px" FieldName="ALMACEN" >
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Clave" Width="100px" FieldName="Clave">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Nombre" Width="350px" FieldName="Nombre" >
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Id Fiscal" Width="100px" FieldName="Idfiscal" >
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Tipo NE" Width="120px" FieldName="Tipone" >
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Programa" Width="130px" FieldName="Programa" >
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Calle número exterior" Width="170px" FieldName="CalleNumero" >
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Calle y número interior" Width="170px" FieldName="callenumerointerior" >
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Colonia" Width="130px" FieldName="Colonia" >
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Código" Width="130px" FieldName="Codigo" >
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Localidad" Width="130px" FieldName="localidad" >
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Municipio" Width="130px" FieldName="municipio">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>

                                    <dx:GridViewDataTextColumn Caption="Entidad" Width="130px" FieldName="Entidad">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>

                                    <dx:GridViewDataTextColumn Caption="País" Width="130px" FieldName="Pais" >
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Teléfono" Width="130px" FieldName="Telefono" >
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Correo" Width="130px" FieldName="Correo" >
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Fax" Width="130px" FieldName="Fax" >
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="ALMACENKEY" Width="130px" FieldName="ALMACENKEY" Visible="false">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    
                                    <dx:GridViewDataTextColumn Caption="Apellido paterno" Width="130px" FieldName="ApellidoPaterno" Visible="false">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Apellido materno" Width="130px" FieldName="ApellidoMaterno" Visible="false">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Calle" Width="130px" FieldName="calle" Visible="false">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>


                                    <dx:GridViewDataTextColumn Caption="Referencia" Width="130px" FieldName="referencia" Visible="false">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>

                                    <dx:GridViewDataTextColumn Caption="Tipo identificador" Width="130px" FieldName="tipoidentificador" Visible="false">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Código postal" Width="130px" FieldName="codigopostal" Visible="false">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
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
                                <SettingsBehavior AllowEllipsisInText="true" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                                <Settings ShowFooter="False" ShowHeaderFilterButton="false" ShowFilterRowMenu="true" ShowFilterRow="true"
                                    ShowGroupPanel="false" ShowVerticalScrollBar="true" />
                                <Styles>
                                    <SelectedRow />
                                    <Row Font-Size="11px" />
                                    <AlternatingRow Enabled="True" />
                                    <PagerTopPanel Paddings-PaddingBottom="3px"></PagerTopPanel>
                                </Styles>
                                <SettingsPager>
                                    <PageSizeItemSettings Visible="true" Items="15, 20, 50" />
                                </SettingsPager>
                                <SettingsDataSecurity AllowInsert="false" AllowEdit="false" AllowDelete="false" />
                                <%--  <ClientSideEvents Init="grid_Init" BeginCallback="grid_BeginCallback" EndCallback="grid_EndCallback" />--%>
                                <ClientSideEvents Init="OnInitGridProv" />
                                <GroupSummary>
                                    <dx:ASPxSummaryItem SummaryType="Count" />
                                </GroupSummary>
                            </dx:ASPxGridView>


                            <dx:ASPxGridViewExporter ID="Exporter" GridViewID="grvProveedores" runat="server" PaperKind="A5" Landscape="true" />
                            <%--</div>--%>


                            <button id="btnNuevo" type="button" data-toggle="modal" data-target="#modalProvedores" style="display: none;"></button>
                            <div class="modal fade" id="modalProvedores" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                                <div class="modal-dialog modal-xl" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h4 class="modal-title" runat="server" id="titNewProveedor">Nuevo Proveedor</h4>
                                        </div>
                                        <div class="modal-body">

                                            <div class="form-group row" style="height: 43px; padding-bottom: 65px">
                                                <div class="col-sm-2 col-md-2">
                                                    <asp:TextBox ID="TXT_PKey" runat="server" Width="100%" Text="0" CssClass="form-control input-sm" Visible="false"></asp:TextBox>
                                                    <div runat="server" id="divProv">
                                                        <label id="LBL_Clave" runat="server" class="form-text" style="font-size: 13px">Clave *</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_Clave" ClientInstanceName="TXT_Clave" runat="server" Width="100%" MaxLength="35" CssClass="control-text">
                                                                    <%--<ValidationSettings SetFocusOnError="false" Display="Dynamic" ErrorText="" ErrorDisplayMode="None">
                                                        </ValidationSettings>
                                                        <InvalidStyle BackColor="LightPink" />--%>
                                                                </dx:ASPxTextBox>
                                                                <%--<dx:ASPxTextBox ID="TXT_Clave" runat="server" Width="100%" CssClass="control-text" MaxLength="35"></dx:ASPxTextBox>--%>
                                                            </div>
                                                            <%--<asp:RequiredFieldValidator ID="ValidateGroup1" ControlToValidate="TXT_Clave" ValidationGroup="RequiedInfoGroup" ErrorMessage="***Falta Información" ForeColor="red" runat="Server">
                                                </asp:RequiredFieldValidator>--%>
                                                            <i runat="server" id="ITXT_Clave"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4 col-md-4">
                                                    <div runat="server" id="DivNombreAcc">
                                                        <label id="LBL_NOMBRE" runat="server" class="form-text" style="font-size: 13px">Nombre *</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_NOMBRE" ClientInstanceName="TXT_NOMBRE" runat="server" Width="100%" MaxLength="100" CssClass="control-text">
                                                                    <%--<ValidationSettings SetFocusOnError="false" Display="Dynamic" ErrorText="" ErrorDisplayMode="None">
                                                        </ValidationSettings>
                                                        <InvalidStyle BackColor="LightPink" />--%>
                                                                </dx:ASPxTextBox>
                                                                <%--<dx:ASPxTextBox ID="TXT_NOMBRE" runat="server" Width="100%" CssClass="control-text" MaxLength="100"></dx:ASPxTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TXT_NOMBRE" ValidationGroup="RequiedInfoGroup" ErrorMessage="***Falta Información" ForeColor="red" runat="Server">
                                        </asp:RequiredFieldValidator>--%>
                                                            </div>

                                                            <i runat="server" id="ITXT_NOMBRE"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2 col-md-2">
                                                    <div runat="server" id="Div1">
                                                        <label id="LBL_Idfiscal" runat="server" class="form-text" style="font-size: 13px">Id físcal</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_Idfiscal" runat="server" Width="100%" CssClass="control-text" MaxLength="100"></dx:ASPxTextBox>
                                                            </div>
                                                            <i runat="server" id="ITXT_Idfiscal"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2 col-md-2">
                                                    <div runat="server" id="Div2">
                                                        <label id="LBL_Tipone" runat="server" class="form-text" style="font-size: 13px">Tipo NE *</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxComboBox ID="TXT_Tipone" ClientInstanceName="TXT_Tipone" runat="server" CssClass="control-text" Width="100%">
                                                                    <Items>
                                                                        <dx:ListEditItem Value="NA" Text="NACIONAL" />
                                                                        <dx:ListEditItem Value="EX" Text="EXTRANJERO" />
                                                                    </Items>
                                                                    <%--<ValidationSettings SetFocusOnError="false" Display="Dynamic" ErrorText="" ErrorDisplayMode="None">
                                                        </ValidationSettings>
                                                        <InvalidStyle BackColor="LightPink" />--%>
                                                                </dx:ASPxComboBox>
                                                            </div>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="TXT_Tipone" ValidationGroup="RequiedInfoGroup" ErrorMessage="***Falta Información" ForeColor="red" runat="Server">
                                    </asp:RequiredFieldValidator>--%>
                                                            <i runat="server" id="ITXT_Tipone"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2 col-md-2">
                                                    <div runat="server" id="Div3">
                                                        <label id="LBL_Programa" runat="server" class="form-text" style="font-size: 13px">Programa</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_Programa" runat="server" Width="100%" CssClass="control-text" MaxLength="100"></dx:ASPxTextBox>
                                                            </div>
                                                            <i runat="server" id="ITXT_Programa"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <label id="Label1" runat="server" class="form-text" style="font-size: 13px">Dirección</label>
                                            <hr />
                                            <div class="form-group row" style="height: 43px">
                                                <div class="col-sm-4 col-md-4">
                                                    <div runat="server" id="Div6">
                                                        <label id="LBL_CalleNumero" runat="server" class="form-text" style="font-size: 13px">Calle y Número Exterior *</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_CalleNumero" ClientInstanceName="TXT_CalleNumero" runat="server" Width="100%" CssClass="control-text" MaxLength="100"></dx:ASPxTextBox>
                                                            </div>
                                                            <i runat="server" id="ITXT_CalleNumero"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-1 col-md-1">
                                                    <div runat="server" id="Div16">
                                                        <label id="LBL_callenumerointerior" runat="server" class="form-text" style="font-size: 13px">No. Int.</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxSpinEdit ID="TXT_callenumerointerior" runat="server" Width="100%" CssClass="control-text" MaxLength="100">
                                                                </dx:ASPxSpinEdit>
                                                                <%--<asp:TextBox ID="TXT_callenumerointerior" runat="server" Width="150px" CssClass="form-control input-sm" MaxLength="100">                                            
                                        </asp:TextBox>--%>
                                                            </div>
                                                            <i runat="server" id="ITXT_callenumerointerior"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4 col-md-4">
                                                    <div runat="server" id="Div4">
                                                        <label id="LBL_Colonia" runat="server" class="form-text" style="font-size: 13px">Colonia</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_Colonia" runat="server" Width="100%" CssClass="control-text" MaxLength="100"></dx:ASPxTextBox>
                                                            </div>
                                                            <i runat="server" id="ITXT_Colonia"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-1 col-md-1">
                                                    <div runat="server" id="Div5">
                                                        <label id="LBL_Codigo" runat="server" class="form-text" style="font-size: 13px">Código</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_Codigo" runat="server" Width="100%" CssClass="control-text" MaxLength="100"></dx:ASPxTextBox>
                                                            </div>
                                                            <i runat="server" id="ITXT_Codigo"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2 col-md-2">
                                                    <div runat="server" id="Div17">
                                                        <label id="LBL_localidad" runat="server" class="form-text" style="font-size: 13px">Localidad</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_localidad" runat="server" Width="100%" CssClass="control-text" MaxLength="100"></dx:ASPxTextBox>
                                                            </div>
                                                            <i runat="server" id="ITXT_localidad"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row" style="height: 43px; padding-bottom: 65px">
                                                <div class="col-sm-3 col-md-3">
                                                    <div runat="server" id="Div19">
                                                        <label id="LBL_municipio" runat="server" class="form-text" style="font-size: 13px">Municipio</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_municipio" runat="server" Width="100%" CssClass="control-text" MaxLength="100"></dx:ASPxTextBox>
                                                            </div>
                                                            <i runat="server" id="ITXT_municipio"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2 col-md-2">
                                                    <div runat="server" id="Div9">
                                                        <label id="LBL_Entidad" runat="server" class="form-text" style="font-size: 13px">Entidad</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_Entidad" runat="server" Width="100%" CssClass="control-text" MaxLength="100"></dx:ASPxTextBox>
                                                            </div>
                                                            <i runat="server" id="ITXT_Entidad"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2 col-md-2">
                                                    <div runat="server" id="Div7">
                                                        <label id="LBL_Pais" runat="server" class="form-text" style="font-size: 13px">País</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_Pais" runat="server" Width="100%" CssClass="control-text" MaxLength="100"></dx:ASPxTextBox>
                                                            </div>
                                                            <i runat="server" id="ITXT_Pais"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <label id="Label2" runat="server" class="form-text" style="font-size: 13px">Contacto</label>
                                            <hr />

                                            <div class="form-group row" style="height: 43px">
                                                <div class="col-sm-4 col-md-4">
                                                    <div runat="server" id="Div8">
                                                        <label id="LBL_Telefono" runat="server" class="form-text" style="font-size: 13px">Teléfono</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_Telefono" runat="server" Width="100%" CssClass="control-text" MaxLength="100"></dx:ASPxTextBox>
                                                            </div>
                                                            <i runat="server" id="ITXT_Telefono"></i>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-4 col-md-4">
                                                    <div runat="server" id="Div12">
                                                        <label id="LBL_Correo" runat="server" class="form-text">Correo</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_Correo" runat="server" Width="100%" CssClass="control-text" MaxLength="100"></dx:ASPxTextBox>
                                                            </div>
                                                            <i runat="server" id="ITXT_Correo"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4 col-md-4">
                                                    <div runat="server" id="Div10">
                                                        <label id="LBL_Fax" runat="server" class="form-text" style="font-size: 13px">Fax</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_Fax" runat="server" Width="100%" CssClass="control-text" MaxLength="100"></dx:ASPxTextBox>
                                                            </div>
                                                            <i runat="server" id="ITXT_Fax"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4 col-md-2">                                                    
                                                    <div runat="server" id="div11">
                                                        <label id="Label3" runat="server" class="form-text">Almacén</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxComboBox ID="CMB_ALMACEN" runat="server" CssClass="control-text" Width="100%" ValueField="ALMACENKEY" TextField="ALMACEN" ClientInstanceName="txtAlmacen">
                                                                </dx:ASPxComboBox>
                                                            </div>
                                                            <i runat="server" id="I1"></i>
                                                        </div>
                                                    </div>
                                                </div>



                                                <%--<div class="col-sm-4 col-md-4" style="visibility: hidden">
                            <div runat="server" id="DivALMACEN">
                                <label id="LBL_ALMACEN" runat="server" class="form-text" style="font-size:13px">Almacén</label>
                                <div class="form-group" style="position: relative; width: 100%; float: left;">
                                    <div class="input-group">
                                        <dx:ASPxComboBox ID="CMB_ALMACEN" runat="server" CssClass="control-text" ValueField="ALMACENKEY" TextField="ALMACEN" Width="100%">
                                        </dx:ASPxComboBox>
                                    </div>
                                    <i runat="server" id="ICMB_ALMACEN"></i>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4" style="visibility: hidden">
                            <div runat="server" id="Div13">
                                <label id="LBL_ApellidoPaterno" runat="server" class="form-text">Apellido paterno</label>
                                <div class="form-group" style="position: relative; width: 100%; float: left;">
                                    <div class="input-group">
                                        <dx:ASPxTextBox ID="TXT_ApellidoPaterno" runat="server" Width="100%" CssClass="control-text" MaxLength="100"></dx:ASPxTextBox>
                                    </div>
                                    <i runat="server" id="ITXT_ApellidoPaterno"></i>
                                </div>
                            </div>
                        </div>

                    </div>


                    <div class="form-group row" style="visibility: hidden; height: 43px" >
                        <div class="col-sm-4 col-md-4" style="visibility: hidden">
                            <div runat="server" id="Div14">
                                <label id="LBL_ApellidoMaterno" runat="server" class="form-text" style="font-size:13px">Apellido materno</label>
                                <div class="form-group" style="position: relative; width: 100%; float: left;">
                                    <div class="input-group">
                                        <dx:ASPxTextBox ID="TXT_ApellidoMaterno" runat="server" Width="100%" CssClass="control-text" MaxLength="100"></dx:ASPxTextBox>
                                    </div>
                                    <i runat="server" id="ITXT_ApellidoMaterno"></i>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4" style="visibility: hidden">
                            <div runat="server" id="Div15">
                                <label id="LBL_calle" runat="server" class="form-text" style="font-size:13px">Calle</label>
                                <div class="form-group" style="position: relative; width: 100%; float: left;">
                                    <div class="input-group">
                                        <dx:ASPxTextBox ID="TXT_calle" runat="server" Width="100%" CssClass="control-text" MaxLength="100"></dx:ASPxTextBox>
                                    </div>
                                    <i runat="server" id="ITXT_calle"></i>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-4 col-md-4" style="visibility: hidden">
                            <div runat="server" id="Div18">
                                <label id="LBL_referencia" runat="server" class="form-text" style="font-size:13px">Referencia</label>
                                <div class="form-group" style="position: relative; width: 100%; float: left;">
                                    <div class="input-group">
                                        <dx:ASPxTextBox ID="TXT_referencia" runat="server" Width="100%" CssClass="control-text" MaxLength="100"></dx:ASPxTextBox>
                                    </div>
                                    <i runat="server" id="ITXT_referencia"></i>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="form-group row" style="visibility: hidden; height: 43px">

                        <div class="col-sm-4 col-md-4">
                            <div runat="server" id="Div20">
                                <label id="LBL_tipoidentificador" runat="server" class="form-text" style="font-size:13px">Tipo identificador</label>
                                <div class="form-group" style="position: relative; width: 100%; float: left;">
                                    <div class="input-group">
                                        <dx:ASPxTextBox ID="TXT_tipoidentificador" runat="server" Width="100%" CssClass="control-text" MaxLength="100"></dx:ASPxTextBox>
                                    </div>
                                    <i runat="server" id="ITXT_tipoidentificador"></i>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <div runat="server" id="Div21">
                                <label id="LBL_codigopostal" runat="server" class="form-text" style="font-size:13px">Código postal</label>
                                <div class="form-group" style="position: relative; width: 100%; float: left;">
                                    <div class="input-group">
                                        <dx:ASPxTextBox ID="TXT_codigopostal" runat="server" Width="100%" CssClass="control-text" MaxLength="100"></dx:ASPxTextBox>
                                    </div>
                                    <i runat="server" id="ITXT_codigopostal"></i>
                                </div>
                            </div>
                        </div>
                    </div>--%>
                                            </div>
                                            <div class="modal-footer">
                                                <dx:ASPxLabel ID="lblRepetido" runat="server" Text="El registro a guardar ya existe" Visible="false" ForeColor="Red" Font-Size="12px"></dx:ASPxLabel>
                                                <dx:ASPxLabel ID="lblExisteProveedor" runat="server" Text="No existe el Proveedor" Visible="false" ForeColor="Red" Font-Size="12px"></dx:ASPxLabel>
                                                <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-primary btn-sm" Text="Guardar" OnClientClick="Valida()" ValidationGroup="RequiedInfoGroup" CausesValidation="true">
                                <span class="glyphicon glyphicon-ok"></span>&nbsp;&nbsp;Guardar</asp:LinkButton>
                                                <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal" >Cancelar</button>
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
                            <%--<div class="modal fade" id="AlertErrorUser" tabindex="-1" role="dialog">
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
    </div>--%>
                            <div class="modal fade" id="AlertErrorUser" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                                <div class="modal-dialog modal-notify" role="document">
                                    <div class="modal-content " style="height: 250px">
                                        <div class="alert alert-danger text-center" style="-webkit-box-shadow: 0 5px 15px rgba(0, 0, 0, .5); box-shadow: 0 5px 15px rgba(0, 0, 0, .5);">
                                            <div class="modal-body">
                                                <div class="text-center">
                                                    <i class="fa fa-window-close fa-4x mb-3 animated rotateIn"></i>
                                                    <p id="p2" runat="server">
                                                    </p>
                                                </div>
                                            </div>
                                            <div class="modal-footer justify-content-center">
                                                <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal" onclick="LimpiarError()">Aceptar<i class="fa ml-1 text-white"></i></button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <button id="btnQuestion" type="button" data-toggle="modal" data-target="#AlertQuestion" style="display: none;"></button>
                            <div class="modal fade modal-warning" id="AlertQuestion" tabindex="-1" role="dialog" style="top: 26%; outline: none;">
                                <div class="modal-dialog  modal-sm " role="document">
                                    <div class="modal-content" style="height: 90px">
                                        <div class="alert alert-warning text-center" style="-webkit-box-shadow: 0 5px 15px rgba(0, 0, 0, .5); box-shadow: 0 5px 15px rgba(0, 0, 0, .5);">

                                            <span class="glyphicon glyphicon-question-sign ico"></span>

                                            <br />

                                            <br />

                                            <p id="pModalQuestion" class="alert-title">
                                            </p>

                                            <hr />
                                            <asp:LinkButton ID="btnAceptarDel" runat="server" CssClass="btn btn-primary btn-sm txt-sm" data-dismiss="modal" OnClientClick="EventosProv('BorrarP')">
                                    <span class="glyphicon glyphicon-plus"></span>&nbsp;&nbsp;Aceptar
                                            </asp:LinkButton>
                                            <button id="btnCancel" runat="server" class="btn btn-secondary btn-sm" data-dismiss="modal">Cancelar</button>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </dx:PanelContent>
                    </PanelCollection>
                    <ClientSideEvents EndCallback="function(s, e){ UpdateProveedores(); }"></ClientSideEvents>
                </dx:ASPxCallbackPanel>
                <script src="../ScriptsSaci/Catalogos.js"></script>
            </div>
        </div>
    </div>
    <div style="height: 100px"></div>
</asp:Content>

