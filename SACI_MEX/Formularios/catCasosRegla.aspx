<%@ Page Title="" Language="C#" MasterPageFile="~/Formularios/Principal.Master" AutoEventWireup="true" CodeBehind="catCasosRegla.aspx.cs" Inherits="SACI_MEX.Formularios.catCasosRegla" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function EventosCR(s, e) {
            cbpCasosRegla.PerformCallback(s, '');
        }

        function UpdatePager(s, e) {

            if (document.getElementById("hdnGuardar").value == "1") {
                document.getElementById('btnSucces').setAttribute('data-whatever', 'gagdas'); document.getElementById('btnSucces').click();
            }
            else if (document.getElementById("hdnGuardar").value == "2") {
                document.getElementById('btnErrorUser').setAttribute('data-whatever', 'gagdas'); document.getElementById('btnErrorUser').click();
            }
            else if (document.getElementById("hdnGuardar").value == "3") {
                document.getElementById('btnNuevo').setAttribute('data-whatever', 'gagdas'); document.getElementById('btnNuevo').click();
            }
            else if (document.getElementById("hdnGuardar").value == "4") {
                document.getElementById('btnError').setAttribute('data-whatever', 'gagdas'); document.getElementById('btnError').click();
            }
        }

        function Valida(s, e) {
            var errores = false;

            //VALIDA REGLA          
            if (txtReglaLink.GetValue() == null) {
                var label = document.getElementById('<%=lblReglaLink.ClientID%>');
                label.style.color = "red";
                errores = true;
            }
            else {
                var label = document.getElementById('<%=lblReglaLink.ClientID%>');
                label.style.color = "black";
            }

            //VALIDA CONDICION           
            if (txtCondicionCaso.GetValue() == null) {
                var label = document.getElementById('<%=lblCondicionCaso.ClientID%>');
                label.style.color = "red";
                errores = true;
            }
            else {
                var label = document.getElementById('<%=lblCondicionCaso.ClientID%>');
                label.style.color = "black";
            }

            //VALIDA CONDICION             
            if (txtTextoCaso.GetValue() == null) {
                var label = document.getElementById('<%=lblTextoCaso.ClientID%>');
                label.style.color = "red";
                errores = true;
            }
            else {
                var label = document.getElementById('<%=lblTextoCaso.ClientID%>');
                label.style.color = "black";
            }
            
            if (errores) {
                alert('Debe agregar los campos obligatorios.');
            }
            else {
                $('#modalCR').modal('hide');
                cbpCasosRegla.PerformCallback('guardar');
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="height: 40px"></div>
    <div class="container">
        <div class="container-fluid">
            <div class="panel-body bordes_curvos" style="background-color: #f8f8f8">
                <div class="divCard tcentrado">
                    <h1 id="h1_titulo" runat="server" class="panel-title">&nbsp;&nbsp;Catálogo de Casos de Regla</h1>
                </div>
                <dx:ASPxCallbackPanel ID="cbpCasosRegla" runat="server" ClientInstanceName="cbpCasosRegla" OnCallback="cbpCasosRegla_Callback">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <asp:HiddenField ID="hdnGuardar" runat="server" ClientIDMode="Static" Value="0" />
                            <asp:HiddenField ID="hdnKey" runat="server" ClientIDMode="Static" />
                            <asp:LinkButton ID="lkb_Nuevo" runat="server" CssClass="btn btn-info btn-sm  btn-height" OnClientClick="EventosCR('nuevo');return false;">
                                <span class="fa fa-plus"></span>&nbsp;Agregar
                            </asp:LinkButton>
                            <asp:LinkButton ID="lkb_Editar" runat="server" CssClass="btn btn-info btn-sm btn-height" OnClientClick="EventosCR('editar');return false;">
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

                            <dx:ASPxGridView ID="grvCasosRegla" ClientInstanceName="gridT" runat="server" KeyFieldName="CASO_KEY"
                                Width="100%" AutoGenerateColumns="False" Settings-HorizontalScrollBarMode="Auto" Theme="DevEx" Styles-Cell-CssClass="grid_content"
                                EnableCallBacks="True" Styles-Header-ForeColor="#751473" Styles-Header-Font-Size="11px">
                                <SettingsBehavior AllowEllipsisInText="true" AllowSort="false" />
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="ID" FieldName="CASO_KEY" Visible="false" />
                                    <dx:GridViewDataTextColumn Caption="Regla" Width="30%" FieldName="REGLA_LINK">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Condicion Caso" Width="70%" FieldName="CONDICION_CASO">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                     <dx:GridViewDataTextColumn Caption="Texto Caso" Width="70%" FieldName="TEXTO_CASO">
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
                                    <PageSizeItemSettings Visible="true" Items="10, 20, 50" />
                                </SettingsPager>
                                <SettingsDataSecurity AllowInsert="false" AllowEdit="false" AllowDelete="false" />
                                <GroupSummary>
                                    <dx:ASPxSummaryItem SummaryType="Count" />
                                </GroupSummary>
                            </dx:ASPxGridView>

                            <dx:ASPxGridViewExporter ID="Exporter" GridViewID="grvCasosRegla" runat="server" PaperKind="A5" Landscape="true" />
                            <button id="btnNuevo" type="button" data-toggle="modal" data-target="#modalCR" style="display: none;"></button>
                            <div class="modal fade" id="modalCR" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                                <div class="modal-dialog modal-md" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h4 class="modal-title" runat="server" id="titNewTratado">Nuevo Caso de Regla</h4>
                                        </div>
                                        <div class="modal-body">
                                            <div class="form-group row">
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="TXT_CRKey" runat="server" Width="100%" Text="0" CssClass="control-text" Visible="false"></asp:TextBox>
                                                    <div runat="server" id="divProv">
                                                    <label id="lblReglaLink" runat="server" class="form-text">Regla *</label>
                                                    <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                        <div class="input-group">
                                                            
                                                            <dx:ASPxComboBox ID="CMB_REGLA_LINK" CssClass="control-text" runat="server" Width="100%" ClientInstanceName="txtReglaLink"
                                                            DropDownStyle="DropDownList" ValueField="Regla_Key" TextFormatString="{0}"
                                                            EnableCallbackMode="true" IncrementalFilteringMode="Contains" AutoPostBack="false" CallbackPageSize="500">
                                                            <Columns>
                                                                <dx:ListBoxColumn FieldName="Regla_Key" Visible="false" />
                                                                <dx:ListBoxColumn FieldName="Tratado" />
                                                                <dx:ListBoxColumn FieldName="Capitulo" />
                                                                <dx:ListBoxColumn FieldName="Condicion" />
                                                                <dx:ListBoxColumn FieldName="Regla" />
                                                                <dx:ListBoxColumn FieldName="Nota_Id" />

                                                            </Columns>
                                                            </dx:ASPxComboBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                </div>
                                                <div class="col-sm-6">
                                                    <div runat="server" id="DivNombreAcc">
                                                        <label id="lblCondicionCaso" runat="server" class="form-text">Condicion del caso *</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_CONDICION_CASO" runat="server" Width="100%" CssClass="control-text" MaxLength="20" ClientInstanceName="txtCondicionCaso"></dx:ASPxTextBox>
                                                                
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-8">
                                                    <div runat="server" id="Div1">
                                                        <label id="lblTextoCaso" runat="server" class="form-text">Caso *</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_TEXTO_CASO" runat="server" Width="100%" CssClass="control-text" MaxLength="25" ClientInstanceName="txtTextoCaso"></dx:ASPxTextBox>
                                                                
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="modal-footer">
                                                <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-primary btn-sm" Text="Guardar" OnClientClick="Valida();return false;" CausesValidation="true">
                                                    <span class="glyphicon glyphicon-ok"></span>&nbsp;&nbsp;Guardar</asp:LinkButton>
                                                <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal">Cancelar</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <button id="btnSucces" type="button" data-toggle="modal" data-target="#modalSucces" style="display: none;"></button>
                            <div class="modal fade" id="modalSucces" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                                <div class="modal-dialog modal-notify modal-success" role="document">
                                    <div class="modal-content">                                      
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
                            <div class="modal fade  modal-warning" id="AlertQuestion" tabindex="-1" role="dialog" style="top: 25%; outline: none;">
                                <div class="modal-dialog  modal-sm " role="document">
                                    <div class="modal-content" style="height: 90px">
                                        <div class="alert alert-warning text-center" style="-webkit-box-shadow: 0 5px 15px rgba(0, 0, 0, .5); box-shadow: 0 5px 15px rgba(0, 0, 0, .5);">
                                            <span class="glyphicon glyphicon-question-sign ico"></span>

                                            <br />

                                            <br />

                                            <p id="pModalQuestion" class="alert-title"> </p>                                       
                                            <hr />
                                            <asp:LinkButton ID="btnAceptarDel" runat="server" CssClass="btn btn-primary btn-sm txt-sm" data-dismiss="modal" OnClientClick="EventosCR('borrar');return false;">
                                                <span class="glyphicon glyphicon-plus"></span>&nbsp;&nbsp;Aceptar
                                            </asp:LinkButton>
                                            <button id="btnCancel" runat="server" class="btn btn-secondary btn-sm" data-dismiss="modal">Cancelar</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </dx:PanelContent>
                    </PanelCollection>
                    <ClientSideEvents EndCallback="function(s, e){UpdatePager(); }"></ClientSideEvents>
                </dx:ASPxCallbackPanel>
            </div>
        </div>
    </div>
</asp:Content>

