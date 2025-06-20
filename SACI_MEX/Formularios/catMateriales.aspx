<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Formularios/Principal.Master" CodeBehind="catMateriales.aspx.cs" Inherits="SACI_MEX.Formularios.catMateriales" EnableEventValidation="true" ValidateRequest="false" EnableViewStateMac="false" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="contPrinc1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function grid_CustomButtonClick(s, e) {
            var index = e.visibleIndex
            if (index != null) {
                $("#HiddenField1").val(index)
            };



            //GridMat.GetRowValues(e.visibleIndex, 'clave', gvTest_GetRowValues);    
        }


        function gvTest_GetRowValues(values) {
            var TestID = values[0];
            //var TestName = values[1]; 


        }



        function Valida(s, e) {
            var errores = false;

            //VALIDA        
            if (TXT_CVE_MATERIAL.GetValue() == null) {
                var label = document.getElementById('<%=LBLCVE_MATERIAL.ClientID%>');
                label.style.color = "red";
                errores = true;
            }
            else {
                var label = document.getElementById('<%=LBLCVE_MATERIAL.ClientID%>');
                label.style.color = "black";
            }


            if (TXT_DESCRIPCION.GetValue() == null) {
                var label = document.getElementById('<%=LBL_DESCRIPCION.ClientID%>');
                label.style.color = "red";
                errores = true;
            }
            else {
                var label = document.getElementById('<%=LBL_DESCRIPCION.ClientID%>');
                label.style.color = "black";
            }


            if (TXT_FRACCION.GetValue() == null) {
                var label = document.getElementById('<%=lblFracc.ClientID%>');
                label.style.color = "red";
                errores = true;
            }
            else {
                var label = document.getElementById('<%=lblFracc.ClientID%>');
                label.style.color = "black";
            }


            if (CMB_UNIDAD.GetValue() == null) {
                var label = document.getElementById('<%=LBL_UNIDAD.ClientID%>');
                label.style.color = "red";
                errores = true;
            }
            else {
                var label = document.getElementById('<%=LBL_UNIDAD.ClientID%>');
                label.style.color = "black";
            }


            if (CMB_UNIDADT.GetValue() == null) {
                var label = document.getElementById('<%=LBL_UNIDADT.ClientID%>');
                label.style.color = "red";
                errores = true;
            }
            else {
                var label = document.getElementById('<%=LBL_UNIDADT.ClientID%>');
                label.style.color = "black";
            }


            if (errores) {
                alert('Debe agregar los campos obligatorios.');
            }
            else {
                $('#modalMateriales').modal('hide');
                cbpcatMateriales.PerformCallback('GuardarM');
            }
        }

        /*Ajustar el tamaño del grvRef al tamaño de la pantalla */
        function OnInitGridMat(s, e) {
            var height = Math.max(0, document.documentElement.clientHeight * 0.68);

            GridMat.SetHeight(height);
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
                    <h1 id="h1_titulo" runat="server" class="panel-title">&nbsp;&nbsp;Materiales</h1>
                </div>
                <br />
                <dx:ASPxCallbackPanel ID="cbpcatMateriales" runat="server" ClientInstanceName="cbpcatMateriales" OnCallback="cbpcatMateriales_Callback">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <div class="container">
                                <asp:HiddenField ID="hdnMaterialKey" runat="server" Value="0" />
                                <asp:HiddenField ID="hdnClaveSel" runat="server" Value="0" />
                                <asp:HiddenField ID="hdnUnidadSel" runat="server" Value="0" />
                                <asp:HiddenField ID="hdnGuardar" runat="server" ClientIDMode="Static" Value="0" />
                                <asp:HiddenField ID="IDActividad" Value="0" runat="server" />


                                <asp:LinkButton ID="lkb_Nuevo" runat="server" CssClass="btn btn-info btn-sm  btn-height" OnClientClick="EventosMateriales('NuevoM')">
                                    <span class="fa fa-plus"></span>&nbsp;Agregar
                                </asp:LinkButton>
                                <asp:LinkButton ID="lkb_Editar" runat="server" CssClass="btn btn-info btn-sm btn-height" OnClientClick="EventosMateriales('EditarM')">
                                    <span class="fa fa-edit"></span>&nbsp;Editar
                                </asp:LinkButton>
                                <asp:LinkButton ID="lkb_Eliminar" runat="server" CssClass="btn btn-info btn-sm btn-width btn-height"
                                    OnClientClick="document.getElementById('btnQuestion').setAttribute('data-whatever', ''); document.getElementById('pModalQuestion').innerHTML  = '¿Estas seguro de eliminar el registro?';  document.getElementById('btnQuestion').click(); return false">
                                    <span class="fa fa-times"></span>&nbsp;Borrar
                                </asp:LinkButton>
                                <asp:LinkButton ID="lkb_Excel" runat="server" CssClass="btn btn-info btn-sm btn-width btn-height" OnClick="lkb_Excel_Click">
                                    <span class="fa fa-expand"></span>&nbsp;Excel
                                </asp:LinkButton>

                                <br />
                                <dx:ASPxGridView ID="grvMateriales" ClientInstanceName="GridMat" runat="server" KeyFieldName="materialkey"
                                    Width="100%" AutoGenerateColumns="False" Settings-HorizontalScrollBarMode="Auto" Theme="DevEx" Styles-Cell-CssClass="grid_content"
                                    EnableCallBacks="true" Styles-Header-ForeColor="#751473" Styles-Header-Font-Size="11px">
                                    <Settings HorizontalScrollBarMode="Auto" VerticalScrollBarMode="Visible"></Settings>
                                    <SettingsBehavior AllowEllipsisInText="true" AllowSort="true" AllowSelectByRowClick="true" />
                                    <Columns>
                                        <dx:GridViewDataTextColumn ReadOnly="True" Width="100px" VisibleIndex="0">
                                            <CellStyle HorizontalAlign="Center"></CellStyle>
                                            <DataItemTemplate>
                                                <dx:ASPxButton ID="btn" runat="server" Text="Factor" OnClick="btn_Click" RenderMode="Link" AutoPostBack="false">
                                                    <%--<Image IconID="Images/file.gif" Url="../img/ver.png" AlternateText="download"></Image>--%>
                                                    <ClientSideEvents Click="function(s,e){ grid_CustomButtonClick(s,e); } " />
                                                </dx:ASPxButton>
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="División / Almacén" Width="130px" FieldName="ALMACEN">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="PK_ID_MATERIAL" FieldName="materialkey" Visible="false" />
                                        <dx:GridViewDataTextColumn Caption="ALMACEN" FieldName="ALMACENKEY" Visible="false" />
                                        <dx:GridViewDataTextColumn Caption="Código MP compañÍa" Width="180px" FieldName="clave">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Código MP proveedor" Width="180px" FieldName="Clave_Proveedor">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Descripción comercial mercancía" Width="210px" FieldName="descripcion">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Fracción" Width="100px" FieldName="fraccion">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Nico" Width="100px" FieldName="NICO">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Unidad" Width="100px" FieldName="unidad">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Unidad tarifa" Width="100px" FieldName="unidadt">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Tipo nacional / Importado" Width="170px" FieldName="TIPOM">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Tipo de material" Width="250px" FieldName="tipomaterial">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Familia" Width="160px" FieldName="FAMILIA">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>


                                        <dx:GridViewDataTextColumn Caption="KG" Width="250px" FieldName="KG" Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="GR" Width="250px" FieldName="GR" Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="ML" Width="250px" FieldName="ML" Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="MCUA" Width="250px" FieldName="MCUA" Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="MCUB" Width="250px" FieldName="MCUB" Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="PZA" Width="250px" FieldName="PZA" Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="CAB" Width="250px" FieldName="CAB" Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="LT" Width="250px" FieldName="LT" Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="PAR" Width="250px" FieldName="PAR" Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="KW" Width="250px" FieldName="KW" Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="MI" Width="250px" FieldName="MI" Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="JGO" Width="250px" FieldName="JGO" Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="KWH" Width="250px" FieldName="KWH" Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="TON" Width="250px" FieldName="TON" Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="BAR" Width="250px" FieldName="BAR" Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="GRN" Width="250px" FieldName="GRN" Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="DECE" Width="250px" FieldName="DECE" Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="CIEN" Width="250px" FieldName="CIEN" Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="DOCE" Width="250px" FieldName="DOCE" Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="CAJA" Width="250px" FieldName="CAJA" Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="BOT" Width="250px" FieldName="BOT" Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>

                                        <dx:GridViewDataTextColumn Caption="Unidad americana" Width="150px" FieldName="UM_AMERICANA">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Factor unidad americana" Width="180px" FieldName="FACTOR_UM_AMERICANA">
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
                                        <PageSizeItemSettings Visible="true" Items="15, 20, 50" />
                                    </SettingsPager>
                                    <SettingsDataSecurity AllowInsert="false" AllowEdit="false" AllowDelete="false" />
                                    <%--  <ClientSideEvents Init="grid_Init" BeginCallback="grid_BeginCallback" EndCallback="grid_EndCallback" />--%>
                                    <ClientSideEvents RowClick="grid_CustomButtonClick" />
                                    <GroupSummary>
                                        <dx:ASPxSummaryItem SummaryType="Count" />
                                    </GroupSummary>
                                    <ClientSideEvents Init="OnInitGridMat" />
                                </dx:ASPxGridView>


                                <dx:ASPxGridViewExporter ID="Exporter" GridViewID="grvMateriales" runat="server">
                                </dx:ASPxGridViewExporter>
                                <div id="divSucces" visible="false" runat="server" class="alert alert-success" role="alert">
                                    <strong>Éxito!</strong>El registro se guardo correctamente.
                                </div>
                            </div>


                            <button id="btnNuevo" type="button" data-toggle="modal" data-target="#modalMateriales" style="display: none;"></button>
                            <div class="modal fade" id="modalMateriales" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                <div class="modal-dialog modal-lg" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h4 class="modal-title" runat="server" id="titNewActividad">Nuevo Material</h4>
                                        </div>
                                        <div class="modal-body">
                                            <div class="form-group row" style="height: 43px">
                                                <div class="col-sm-4 col-md-4">
                                                    <asp:TextBox ID="TXT_materialkey" runat="server" Width="100%" Text="0" CssClass="control-text" MaxLength="35" Visible="false"></asp:TextBox>
                                                    <div runat="server" id="DivMaterial">
                                                        <label id="LBLCVE_MATERIAL" runat="server" class="form-text">Código MP compañÍa *</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_CVE_MATERIAL" ClientInstanceName="TXT_CVE_MATERIAL" runat="server" Width="100%" MaxLength="50">
                                                                    <%--<ValidationSettings SetFocusOnError="false" Display="Dynamic" ErrorText="" ErrorDisplayMode="None">
                                                        </ValidationSettings>
                                                        <InvalidStyle BackColor="LightPink" />--%>
                                                                </dx:ASPxTextBox>
                                                                <%--<asp:RequiredFieldValidator ID="ValidateGroup1" ControlToValidate="TXT_CVE_MATERIAL" ValidationGroup="RequiedInfoGroup" ErrorMessage="***Falta Información" ForeColor="red" runat="Server">
                                                    </asp:RequiredFieldValidator>--%>
                                                            </div>
                                                            <i runat="server" id="ITXT_CVE_MATERIAL"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4 col-md-4">
                                                    <div runat="server" id="Div2">
                                                        <label id="lblCveProv" runat="server" class="form-text">Código MP proveedor</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_MP_PROVEEDOR" runat="server" Width="100%" MaxLength="50"></dx:ASPxTextBox>
                                                            </div>
                                                            <i runat="server" id="I1_TXT_MP_PROVEEDOR"></i>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="form-group row" style="height: 43px">
                                                <div class="col-sm-8 col-md-8">
                                                    <div runat="server" id="DivDescripcion">
                                                        <label id="LBL_DESCRIPCION" runat="server" class="form-text">Descripción comercial mercancía *</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_DESCRIPCION" ClientInstanceName="TXT_DESCRIPCION" runat="server" Width="100%" MaxLength="250">
                                                                    <%--<ValidationSettings SetFocusOnError="false" Display="Dynamic" ErrorText="" ErrorDisplayMode="None">
                                                        </ValidationSettings>
                                                        <InvalidStyle BackColor="LightPink" />--%>
                                                                </dx:ASPxTextBox>
                                                            </div>
                                                            <%--<asp:RequiredFieldValidator ID="ValidateGrou2" ControlToValidate="TXT_DESCRIPCION" ValidationGroup="RequiedInfoGroup" ErrorMessage="***Falta Información" ForeColor="red" runat="Server">
                                                </asp:RequiredFieldValidator>--%>
                                                            <i runat="server" id="ITXT_DESCRIPCION"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row" style="height: 43px">
                                                <div class="col-sm-4 col-md-4">
                                                    <div runat="server" id="Div3">
                                                        <label id="lblFracc" runat="server" class="form-text">Fracción *</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <%--<asp:TextBox ID="TXT_FRACCION" runat="server" Width="100%" CssClass="control-text" Height="30px" MaxLength="50" BorderColor="Red"></asp:TextBox>--%>
                                                                <dx:ASPxTextBox ID="TXT_FRACCION" ClientInstanceName="TXT_FRACCION" runat="server" MaxLength="8" Width="100%" Height="20px">
                                                                    <%--<ValidationSettings SetFocusOnError="false" Display="Dynamic" ErrorText="" ErrorDisplayMode="None">
                                                        </ValidationSettings>
                                                        <InvalidStyle BackColor="LightPink" />--%>
                                                                    <ClientSideEvents LostFocus="function(s,e){if(s.GetText().length<8){alert('este campo debe ser de 8 digitos'); s.Focus() }}" />
                                                                </dx:ASPxTextBox>
                                                                <%--<asp:RequiredFieldValidator ID="ValidateGrou3" ControlToValidate="TXT_FRACCION" ValidationGroup="RequiedInfoGroup" ErrorMessage="***Falta Información" ForeColor="red" runat="Server">
                                                    </asp:RequiredFieldValidator>--%>
                                                            </div>
                                                            <i runat="server" id="I1_TXT_FRACCION"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2 col-md-2">
                                                    <div runat="server" id="Div5">
                                                    </div>
                                                    <div runat="server" id="Div6">
                                                        <label id="lblNico" runat="server" class="form-text">Nico</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_NICO" ClientInstanceName="TXT_NICO" runat="server" MaxLength="5" Width="100%" Height="20px"></dx:ASPxTextBox>
                                                            </div>
                                                            <i runat="server" id="ITXT_NICO"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3 col-md-3">
                                                    <div runat="server" id="Div7">
                                                    </div>
                                                    <div runat="server" id="Div8">
                                                        <label id="Label1" runat="server" class="form-text">Familia</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_FAMILIA" ClientInstanceName="TXT_FAMILIA" runat="server" MaxLength="20" Width="100%" Height="20px"></dx:ASPxTextBox>
                                                            </div>
                                                            <i runat="server" id="I1"></i>
                                                        </div>
                                                    </div>
                                                </div>



                                            </div>
                                            <div class="form-group row" style="height: 43px">

                                                <div class="col-sm-3 col-md-3">
                                                    <div runat="server" id="DivUNIDAD">
                                                        <label id="LBL_UNIDAD" runat="server" class="form-text">Unidad *</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxComboBox ID="CMB_UNIDAD" ClientInstanceName="CMB_UNIDAD" runat="server" CssClass="control-text" ValueField="CVE_UNIDAD" TextField="CVE_UNIDAD">
                                                                    <%--<ValidationSettings SetFocusOnError="false" Display="Dynamic" ErrorText="" ErrorDisplayMode="None">
                                                        </ValidationSettings>
                                                        <InvalidStyle BackColor="LightPink" /> --%>
                                                                </dx:ASPxComboBox>
                                                            </div>
                                                            <%--<asp:RequiredFieldValidator ID="ValidateGrou4" ControlToValidate="CMB_UNIDAD" ValidationGroup="RequiedInfoGroup" ErrorMessage="*Falta Información" ForeColor="red" runat="Server">
                                                </asp:RequiredFieldValidator>--%>
                                                            <i runat="server" id="ITXT_UNIDAD"></i>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="col-sm-3 col-md-3">
                                                    <div runat="server" id="DivUNIDADT">
                                                        <label id="LBL_UNIDADT" runat="server" class="form-text">Unidad tarifaria *</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxComboBox ID="CMB_UNIDADT" ClientInstanceName="CMB_UNIDADT" runat="server" CssClass="control-text" ValueField="CVE_UNIDAD" TextField="CVE_UNIDAD">
                                                                    <%--<ValidationSettings SetFocusOnError="false" Display="Dynamic" ErrorText="" ErrorDisplayMode="None">
                                                        </ValidationSettings>
                                                        <InvalidStyle BackColor="LightPink" />--%>
                                                                </dx:ASPxComboBox>
                                                            </div>
                                                            <%--<asp:RequiredFieldValidator ID="ValidateGrou5" ControlToValidate="CMB_UNIDADT" ValidationGroup="RequiedInfoGroup" ErrorMessage="***Falta Información" ForeColor="red" runat="Server">
                                                </asp:RequiredFieldValidator>--%>
                                                            <i runat="server" id="ICMB_UNIDADT"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3 col-md-3">
                                                    <div runat="server" id="DivVACIO">
                                                    </div>
                                                    <div runat="server" id="DivALMACEN">
                                                        <label id="LBL_ALMACEN" runat="server" class="form-text">División / Almacén</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxComboBox ID="CMB_ALMACEN" runat="server" CssClass="control-text" ValueField="ALMACENKEY" TextField="ALMACEN">
                                                                </dx:ASPxComboBox>
                                                            </div>
                                                            <i runat="server" id="ICMB_ALMACEN"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3 col-md-3">
                                                    <div runat="server" id="Div4">
                                                        <label id="lblNacImpo" runat="server" class="form-text">Tipo nacional / Importado</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxComboBox ID="CMB_NAC_IMPORTADO" runat="server" CssClass="control-text" ValueField="NOM_TIP" TextField="NOM_TIP">
                                                                </dx:ASPxComboBox>
                                                            </div>
                                                            <i runat="server" id="I1_CMB_NAC_IMPORTADO"></i>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="form-group row" style="height: 43px">


                                                <div class="col-sm-12 col-md-12">
                                                    <div runat="server" id="DivFAMILIA">
                                                        <label id="LBL_FAMILIA" runat="server" class="form-text">Tipo de material</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxComboBox ID="CMB_FAMILIA" Width="100%" runat="server" CssClass="control-text" ValueField="TipoMaterial" TextField="TipoMaterial">
                                                                </dx:ASPxComboBox>
                                                            </div>
                                                            <i runat="server" id="ITCMB_FAMILIA"></i>
                                                        </div>
                                                    </div>
                                                </div>




                                            </div>

                                            <div class="form-group row" style="height: 43px">
                                                <div class="col-sm-3 col-md-3">
                                                    <div runat="server" id="Div9">
                                                    </div>
                                                    <div runat="server" id="Div10">
                                                        <label id="Label2" runat="server" class="form-text">Unidad americana</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_UM_AMERICANA" ClientInstanceName="TXT_UM_AMERICANA" runat="server" MaxLength="50" Width="100%" Height="20px"></dx:ASPxTextBox>
                                                            </div>
                                                            <i runat="server" id="I2"></i>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-3 col-md-3">
                                                    <div runat="server" id="Div11">
                                                    </div>
                                                    <div runat="server" id="Div12">
                                                        <label id="Label3" runat="server" class="form-text">Factor unidad americana</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxSpinEdit ID="TXT_FACTOR_UM_AMERICANA" runat="server" Number="0">
                                                                </dx:ASPxSpinEdit>
                                                            </div>
                                                            <i runat="server" id="I3"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="modal-footer">
                                                <dx:ASPxLabel ID="lblRepetido" runat="server" Text="El registro a guardar ya existe" Visible="false" ForeColor="Red" Font-Size="12px"></dx:ASPxLabel>
                                                <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-primary btn-sm" Text="Guardar" OnClientClick="Valida()" ValidationGroup="RequiedInfoGroup" CausesValidation="true">
                                <span class="glyphicon glyphicon-ok"></span>&nbsp;&nbsp;Guardar</asp:LinkButton>
                                                <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal">Cancelar</button>
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
                            <%--
                                <div class="modal fade" id="AlertErrorUser" tabindex="-1" role="dialog">
                                                <div class="modal-dialog modal-sm" role="document" style="top: 25%; outline: none;">
                                                <div class="alert alert-danger text-center" style="-webkit-box-shadow: 0 5px 15px rgba(0, 0, 0, .5); box-shadow: 0 5px 15px rgba(0, 0, 0, .5);">
                                            <span class="glyphicon glyphicon-alert ico"></span>
                                            <br />
                                            <br />
                                            <p id="p2" runat="server" class="alert-title">
                                            </p>
                                            <hr />
                                            <button type="button" class="btn btn-danger" data-dismiss="modal">Aceptar</button>
                                        </div>
                                    </div>
                                </div>
                            --%>
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
                                            <asp:LinkButton ID="btnAceptarDel" runat="server" CssClass="btn btn-primary btn-sm txt-sm" data-dismiss="modal" OnClientClick="EventosMateriales('BorrarM')">
                    <span class="glyphicon glyphicon-plus"></span>&nbsp;&nbsp;Aceptar
                                            </asp:LinkButton>
                                            <button id="btnCancel" runat="server" class="btn btn-secondary btn-sm" data-dismiss="modal">Cancelar</button>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <button id="btnDetMaterial" type="button" data-toggle="modal" data-target="#modalDetalleMat" style="display: none;"></button>
                            <div class="modal fade " id="modalDetalleMat" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" data-backdrop="static">
                                <div class="modal-dialog " role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h4 class="modal-title" id="titDetalleMat" runat="server"></h4>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:HiddenField ID="HiddenField1" runat="server" ClientIDMode="Static" />

                                                    <br />
                                                    <dx:ASPxGridView ID="grvDetMaterial" ClientInstanceName="GridDetMate" runat="server" KeyFieldName="FactoresMPKey" OnInitNewRow="grvDetMaterial_InitNewRow" OnCustomErrorText="grvDetMaterial_CustomErrorText"
                                                        Width="100%" AutoGenerateColumns="False" Styles-Header-Font-Size="11px" OnCellEditorInitialize="grvDetMaterial_CellEditorInitialize" OnRowUpdating="grvDetMaterial_RowUpdating" OnRowDeleting="grvDetMaterial_RowDeleting"
                                                        OnRowInserting="grvDetMaterial_RowInserting" Theme="DevEx" Styles-Cell-CssClass="grid_content" OnCommandButtonInitialize="grvDetMaterial_CommandButtonInitialize">
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn Caption="detKey" FieldName="FactoresMPKey" Visible="false" />
                                                            <dx:GridViewCommandColumn ShowNewButtonInHeader="true" ShowEditButton="true" ShowDeleteButton="true" VisibleIndex="0" />
                                                            <dx:GridViewDataTextColumn Caption="Unidad" Width="38%" FieldName="Unidad" VisibleIndex="1" ReadOnly="true">
                                                                <EditFormSettings VisibleIndex="0" />
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataComboBoxColumn Caption="Unidad a Convertir" Width="35%" FieldName="UnidadConvertir" VisibleIndex="2" PropertiesComboBox-ValidationSettings-RequiredField-IsRequired="true" PropertiesComboBox-ValidationSettings-RequiredField-ErrorText="Falta información">
                                                                <EditFormSettings VisibleIndex="1" />
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                            </dx:GridViewDataComboBoxColumn>
                                                            <dx:GridViewDataSpinEditColumn Caption="Factor" Width="33%" FieldName="Factor" VisibleIndex="3" PropertiesSpinEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesSpinEdit-ValidationSettings-RequiredField-ErrorText="Falta información">
                                                                <EditFormSettings VisibleIndex="2" />
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                            </dx:GridViewDataSpinEditColumn>

                                                        </Columns>
                                                        <SettingsCommandButton CancelButton-Text="Cancelar" NewButton-Text="Nuevo" UpdateButton-Text="Guardar" EditButton-Text="Editar" DeleteButton-Text="Eliminar"></SettingsCommandButton>
                                                        <EditFormLayoutProperties ColCount="3">
                                                            <Items>
                                                                <dx:GridViewColumnLayoutItem ColumnName="Unidad" Width="100%" />
                                                                <dx:GridViewColumnLayoutItem ColumnName="UnidadConvertir" Width="60%" RequiredMarkDisplayMode="Required" />
                                                                <dx:GridViewColumnLayoutItem ColumnName="Factor" Width="60%" RequiredMarkDisplayMode="Required" />
                                                                <dx:EditModeCommandLayoutItem Width="100%" HorizontalAlign="Right" RequiredMarkDisplayMode="Required" />
                                                            </Items>
                                                        </EditFormLayoutProperties>
                                                        <EditFormLayoutProperties>
                                                            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                                                        </EditFormLayoutProperties>


                                                        <SettingsBehavior AllowSelectSingleRowOnly="True" AllowFocusedRow="True" ConfirmDelete="true" AllowSort="false" />

                                                    </dx:ASPxGridView>


                                                    <dx:ASPxGridViewExporter ID="grvExporterDetMat" GridViewID="grvDetMaterial" runat="server" PaperKind="A5" Landscape="true" />
                                                    <div id="div1" visible="false" runat="server" class="alert alert-success" role="alert">
                                                        <strong>Éxito!</strong>El registro se guardo correctamente.
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>


                        </dx:PanelContent>
                    </PanelCollection>
                    <ClientSideEvents EndCallback="function(s, e){ UpdateMateriales(); }"></ClientSideEvents>
                </dx:ASPxCallbackPanel>
                <script src="../ScriptsSaci/Catalogos.js"></script>
            </div>
        </div>
    </div>
    <div style="height: 100px"></div>
</asp:Content>



