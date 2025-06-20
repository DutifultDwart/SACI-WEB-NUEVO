<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Formularios/Principal.Master" CodeBehind="catReglaOrigen.aspx.cs" Inherits="SACI_MEX.Formularios.catReglaOrigen" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="contPrinc1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function EventosRO(s, e) {
            cbpReglaorigen.PerformCallback(s, '');
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

            //VALIDA TRATADO             
            if (txtTratado.GetValue() == null) {
                var label = document.getElementById('<%=lblTratado.ClientID%>');
                label.style.color = "red";
                errores = true;
            }
            else {
                var label = document.getElementById('<%=lblTratado.ClientID%>');
                label.style.color = "black";
            }

            //VALIDA CAPITULO             
            if (txtCapitulo.GetValue() == null) {
                var label = document.getElementById('<%=lblCapitulo.ClientID%>');
                label.style.color = "red";
                errores = true;
            }
            else {
                var label = document.getElementById('<%=lblCapitulo.ClientID%>');
                label.style.color = "black";
            }

            //VALIDA CONDICION             
            if (txtCondicion.GetValue() == null) {
                var label = document.getElementById('<%=lblCondicion.ClientID%>');
                label.style.color = "red";
                errores = true;
            }
            else {
                var label = document.getElementById('<%=lblCondicion.ClientID%>');
                label.style.color = "black";
            }

            //VALIDA NOTA ID             
            if (txtNotaId.GetValue() == null) {
                var label = document.getElementById('<%=lblNota.ClientID%>');
                label.style.color = "red";
                errores = true;
            }
            else {
                var label = document.getElementById('<%=lblNota.ClientID%>');
                label.style.color = "black";
            }

            //VALIDA REGLA             
            if (memoRegla.GetValue() == null) {
                var label = document.getElementById('<%=lblRegla.ClientID%>');
                label.style.color = "red";
                errores = true;
            }
            else {
                var label = document.getElementById('<%=lblRegla.ClientID%>');
                label.style.color = "black";
            }


            if (errores) {
                alert('Debe agregar los campos obligatorios.');
            }
            else {
                $('#modalRO').modal('hide');
                cbpReglaorigen.PerformCallback('guardar');
            }
        }

    </script>
</asp:Content>

<asp:Content ID="contPrinc2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="height: 40px"></div>
    <div class="container">
        <div class="container-fluid">
            <div class="panel-body bordes_curvos" style="background-color: #f8f8f8">

                <div class="divCard tcentrado">
                    <h1 id="h1_titulo" runat="server" class="panel-title">&nbsp;&nbsp;Catálogo de Reglas de Origen</h1>
                </div>



                <dx:ASPxCallbackPanel ID="cbpReglaorigen" runat="server" ClientInstanceName="cbpReglaorigen" OnCallback="cbpReglaorigen_Callback">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <asp:HiddenField ID="hdnGuardar" runat="server" ClientIDMode="Static" Value="0" />
                            <asp:HiddenField ID="hdnKey" runat="server" ClientIDMode="Static" />
                            <div class="row">
                                <div class="col-sm-6">
                                    <asp:LinkButton ID="lkb_Nuevo" runat="server" CssClass="btn btn-info btn-sm  btn-height" OnClientClick="EventosRO('nuevo');return false;">
                                <span class="fa fa-plus"></span>&nbsp;Agregar
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lkb_Editar" runat="server" CssClass="btn btn-info btn-sm btn-height" OnClientClick="EventosRO('editar');return false;">
                                <span class="fa fa-edit"></span>&nbsp;Editar
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lkb_Eliminar" runat="server" CssClass="btn btn-info btn-sm btn-width btn-height"
                                        OnClientClick="document.getElementById('btnQuestion').setAttribute('data-whatever', ''); document.getElementById('pModalQuestion').innerHTML  = '¿Estas seguro de eliminar el registro?';  document.getElementById('btnQuestion').click(); return false">
                                <span class="fa fa-times"></span>&nbsp;Borrar
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lkb_Excel" runat="server" CssClass="btn btn-info btn-sm btn-width btn-height" OnClick="lkb_Excel_Click">
                                <span class="fa fa-expand"></span>&nbsp;Excel
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-2">
                                    <br />
                                    <dx:ASPxTextBox ID="TXT_TRATADOB" runat="server" CssClass="control-text" MaxLength="25" ClientInstanceName="txtReglaB" NullText="Tratado"></dx:ASPxTextBox>
                                </div>
                                <div class="col-sm-2">
                                    <br />
                                    <dx:ASPxTextBox ID="TXT_REGLAB" runat="server" CssClass="control-text" MaxLength="25" ClientInstanceName="txtReglaB" NullText="Fracción"></dx:ASPxTextBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:LinkButton ID="LkbRegla" runat="server" CssClass="btn btn-info btn-sm btn-height" OnClientClick="EventosRO('buscar');return false;">
                                <span class="fa fa-search"></span>&nbsp;Buscar
                                    </asp:LinkButton>
                                </div>
                            </div>

                            <br />

                            <dx:ASPxGridView ID="grvReglasOrigen" ClientInstanceName="gridT" runat="server" KeyFieldName="Regla_Key"
                                Width="100%" AutoGenerateColumns="False" Settings-HorizontalScrollBarMode="Auto" Theme="DevEx" Styles-Cell-CssClass="grid_content"
                                EnableCallBacks="True" Styles-Header-ForeColor="#751473" Styles-Header-Font-Size="11px">
                                <SettingsBehavior AllowEllipsisInText="true" AllowSort="false" />
                                <Columns>

                                    <dx:GridViewDataTextColumn Caption="ID" FieldName="Regla_Key" Visible="false" />
                                    <dx:GridViewDataTextColumn Caption="Tratado" Width="10%" FieldName="Tratado">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Capitulo" Width="10%" FieldName="Capitulo">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Condición" Width="20%" FieldName="Condicion">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Regla" Width="50%" FieldName="Regla">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Nota Id" Width="10%" FieldName="Nota_Id">
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
                                <%--<ClientSideEvents Init="OnInitGridTratados" />--%>
                                <GroupSummary>
                                    <dx:ASPxSummaryItem SummaryType="Count" />
                                </GroupSummary>
                            </dx:ASPxGridView>


                            <dx:ASPxGridViewExporter ID="Exporter" GridViewID="grvReglasOrigen" runat="server" PaperKind="A5" Landscape="true" />
                            <%--</div>--%>

                            <button id="btnNuevo" type="button" data-toggle="modal" data-target="#modalRO" style="display: none;"></button>
                            <div class="modal fade" id="modalRO" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                                <div class="modal-dialog modal-md" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h4 class="modal-title" runat="server" id="titNewTratado">Nuevo Tratado</h4>
                                        </div>
                                        <div class="modal-body">
                                            <div class="form-group row">

                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="TXT_RKey" runat="server" Width="100%" Text="0" CssClass="control-text" Visible="false"></asp:TextBox>
                                                    <div runat="server" id="divProv">
                                                        <label id="lblTratado" runat="server" class="form-text">Tratado *</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_TRATADO" runat="server" Width="100%" CssClass="control-text" MaxLength="20" ClientInstanceName="txtTratado"></dx:ASPxTextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6">
                                                    <div runat="server" id="DivNombreAcc">
                                                        <label id="lblCapitulo" runat="server" class="form-text">Capítulo *</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_CAPITULO" runat="server" Width="100%" CssClass="control-text" MaxLength="20" ClientInstanceName="txtCapitulo"></dx:ASPxTextBox>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-8">
                                                    <div runat="server" id="Div1">
                                                        <label id="lblCondicion" runat="server" class="form-text">Condición *</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_CONDICION" runat="server" Width="100%" CssClass="control-text" MaxLength="25" ClientInstanceName="txtCondicion"></dx:ASPxTextBox>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-4">
                                                    <div runat="server" id="Div3">
                                                        <label id="lblNota" runat="server" class="form-text">Nota Id *</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">

                                                                <dx:ASPxTextBox ID="TXT_NOTAID" runat="server" Width="100%" CssClass="control-text" MaxLength="20" ClientInstanceName="txtNotaId"></dx:ASPxTextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12">
                                                    <div runat="server" id="Div2">
                                                        <label id="lblRegla" runat="server" class="form-text">Regla *</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">

                                                                <dx:ASPxMemo ID="TXT_REGLA" runat="server" Width="100%" Height="200px" CssClass="control-text" ClientInstanceName="memoRegla"></dx:ASPxMemo>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>



                                            </div>
                                            <div class="modal-footer">
                                                <%--<dx:ASPxLabel ID="lblRepetido" runat="server" Text="El registro a guardar ya existe." Visible="false" ForeColor="Red" Font-Size="12px"></dx:ASPxLabel>--%>
                                                <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-primary btn-sm" Text="Guardar" OnClientClick="Valida();return false;" CausesValidation="true">
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

                                            <p id="pModalQuestion" class="alert-title">
                                            </p>

                                            <hr />
                                            <asp:LinkButton ID="btnAceptarDel" runat="server" CssClass="btn btn-primary btn-sm txt-sm" data-dismiss="modal" OnClientClick="EventosRO('borrar');return false;">
                    <span class="glyphicon glyphicon-plus"></span>&nbsp;&nbsp;Aceptar
                                            </asp:LinkButton>
                                            <button id="btnCancel" runat="server" class="btn btn-secondary btn-sm" data-dismiss="modal">Cancelar</button>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </dx:PanelContent>
                    </PanelCollection>

                    <ClientSideEvents EndCallback="function(s, e){ UpdatePager(); }"></ClientSideEvents>
                </dx:ASPxCallbackPanel>
            </div>
        </div>
    </div>
</asp:Content>
