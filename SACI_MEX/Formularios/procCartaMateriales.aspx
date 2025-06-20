<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Formularios/Principal.Master" CodeBehind="procCartaMateriales.aspx.cs" Inherits="SACI_MEX.Formularios.procCartaMateriales" EnableEventValidation="true" ValidateRequest="false" EnableViewStateMac="false" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="contPrinc1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        /*Ajustar el tamaño del PageControl1 al tamaño de la pantalla */
        function OnInitPageControl1(s, e) {
            var height = Math.max(0, document.documentElement.clientHeight * 0.67);

            pagePrincipal.SetHeight(height);

        }

    </script>

</asp:Content>


<asp:Content ID="contPrinc2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="height: 40px"></div>

    <div class="container-fluid">
        <div class="panel-body bordes_curvos" style="background-color: #f8f8f8">


            <div class="divCard tcentrado">
                <h1 id="h2" runat="server" class="panel-title">&nbsp;&nbsp;Carta Materiales</h1>
                <%--<h1 id="h1_titulo_Impo" runat="server" class="panel-title">&nbsp;&nbsp;1. Modulo de información aduanera de entradas</h1>--%>
            </div>
            <br />

            <dx:ASPxCallbackPanel ID="panelPrinc" runat="server" ClientInstanceName="cp2"
                OnCallback="panelPrinc_Callback">
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <asp:HiddenField ID="hdnGuardar" runat="server" ClientIDMode="Static" Value="0" />
                        <div class="row" id="divBuscar" runat="server">
                            <div class="col-sm-3 col-lg-3" style="padding-top: 7px">
                                <label id="lblPeriodo" runat="server" class="form-text" visible="false">Periodo</label>
                                <dx:ASPxUploadControl ID="UploadControl" ClientInstanceName="uploadControl" runat="server" ShowProgressPanel="True" NullText=" " OnFileUploadComplete="UploadControl_FileUploadComplete"
                                    UploadMode="Advanced" Width="100%" BrowseButtonStyle-CssClass="btn-info btnCurvo" CssClass="control-text btnCurvo" AutoStartUpload="false" BrowseButtonStyle-ForeColor="#FFFFFF">
                                    <AdvancedModeSettings EnableFileList="True" EnableMultiSelect="True" EnableDragAndDrop="true"></AdvancedModeSettings>
                                    <BrowseButton Text="&nbsp;Explorar"></BrowseButton>
                                    <ValidationSettings AllowedFileExtensions=".xls,.xlsx"></ValidationSettings>
                                    <ClientSideEvents FileUploadComplete="function(s, e) { OnUploadCompleteExcel(); }" />
                                    <BrowseButtonStyle CssClass="btn-info btnCurvo" ForeColor="White"></BrowseButtonStyle>
                                </dx:ASPxUploadControl>
                            </div>
                            <div class="col-sm-4 col-lg-4">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 35%">
                                            <asp:LinkButton ID="lnk_Cargar" runat="server" CssClass="btn btn-info btn-sm" OnClientClick="OnBtnUploadClick()">
                                        <span class="fa fa-plus"></span>&nbsp;Cargar                
                                            </asp:LinkButton>
                                        </td>
                                        <td style="width: 35%">
                                            <asp:LinkButton ID="lnk_Guardar" runat="server" CssClass="btn btn-info btn-sm" OnClientClick="CargaGrid('Guardar')">
                                        <span class="fa fa-plus"></span>&nbsp;Procesar                
                                            </asp:LinkButton>
                                        </td>
                                        <td style="width: 30%">
                                            <asp:LinkButton ID="lnk_Limpiar" runat="server" CssClass="btn btn-info btn-sm" OnClientClick="CargaGrid('Limpia')">
                                        <span class="fa fa-plus"></span>&nbsp;Limpiar                
                                            </asp:LinkButton>
                                        </td>
                                        <td style="width: 30%">
                                            <asp:LinkButton ID="lnk_DescargarLy" runat="server" CssClass="btn btn-info btn-sm" OnClick="lnk_DescargarLy_Click" Width="120px">
                                        <span class="fa fa-download"></span>&nbsp;Descargar                
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>


                            </div>
                            <div class="col-sm-4 col-lg-4"></div>
                        </div>
                        <br />

                        <dx:ASPxPageControl runat="server" ID="ASPxPageControlCM" Height="100%" Width="100%" ContentStyle-Border-BorderWidth="3px" ClientInstanceName="pagePrincipal"
                            TabAlign="Justify" EnableCallBacks="false" EnableHierarchyRecreation="true" EnableTabScrolling="true" Theme="SoftOrange" Font-Size="12px"
                            ContentStyle-VerticalAlign="Top" TabStyle-BackColor="#751473" ActiveTabStyle-BackColor="#751473" AutoPostBack="false" ActiveTabIndex="0">
                            <TabStyle Paddings-PaddingLeft="50px" Paddings-PaddingRight="50px" ForeColor="#751473">
                                <Paddings PaddingLeft="50px" PaddingRight="50px"></Paddings>
                            </TabStyle>
                            <ActiveTabStyle BackColor="#751473" ForeColor="#751473" />
                            <ContentStyle>
                                <Paddings PaddingLeft="20px" PaddingRight="20px" PaddingTop="5px" />
                            </ContentStyle>
                            <ClientSideEvents Init="OnInitPageControl1" />
                            <TabPages>
                                <dx:TabPage Text="Carta Materiales">
                                    <ActiveTabStyle Height="200px"></ActiveTabStyle>
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl1" runat="server">
                                            <asp:Panel ID="Panel9" runat="server">
                                                <div class="form-row" style="height: 350px">
                                                    <div class="col-sm-12 col-lg-12">
                                                        <div style="width: 100%">

                                                            <dx:ASPxGridView ID="grvCm" ClientInstanceName="grvCm_cte" runat="server" KeyFieldName="Id"
                                                                Width="100%" AutoGenerateColumns="False" Settings-HorizontalScrollBarMode="Auto" Theme="DevEx"
                                                                EnableCallBacks="True" Styles-Header-ForeColor="#751473" Styles-Header-Font-Size="11px">
                                                                <Columns>
                                                                    <dx:GridViewDataTextColumn FieldName="Id" VisibleIndex="1" Visible="false" />
                                                                    <dx:GridViewDataTextColumn FieldName="IdCarga" VisibleIndex="2" Visible="false" />
                                                                    <dx:GridViewDataTextColumn Caption="Código Producto" Width="130px" FieldName="codigodeproducto" VisibleIndex="3">
                                                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                                        <CellStyle CssClass="grid_content"></CellStyle>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Código Material 1" Width="130px" FieldName="CODIGODEMATERIAL1" VisibleIndex="4">
                                                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                                        <CellStyle HorizontalAlign="Center" CssClass="grid_content"></CellStyle>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Código Material 2" Width="130px" FieldName="CODIGODEMATERIAL2" VisibleIndex="5">
                                                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                                        <CellStyle HorizontalAlign="Center" CssClass="grid_content"></CellStyle>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Código Material 3" Width="130px" FieldName="CODIGODEMATERIAL3" VisibleIndex="6">
                                                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                                        <CellStyle HorizontalAlign="Center" CssClass="grid_content"></CellStyle>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Código Material 4" Width="130px" FieldName="CODIGODEMATERIAL4" VisibleIndex="6">
                                                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                                        <CellStyle HorizontalAlign="Center" CssClass="grid_content"></CellStyle>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataDateColumn Caption="Inicio Vigencia" Width="120px" FieldName="iniciovigencia" VisibleIndex="7">
                                                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                                        <CellStyle HorizontalAlign="Center" CssClass="grid_content"></CellStyle>
                                                                    </dx:GridViewDataDateColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Cantidad UMC" Width="130px" FieldName="cantidadumc" VisibleIndex="8">
                                                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                                        <CellStyle CssClass="grid_content"></CellStyle>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="UMC" Width="130px" FieldName="umc" VisibleIndex="9">
                                                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                                        <CellStyle HorizontalAlign="Center" CssClass="grid_content"></CellStyle>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Desperdicio" Width="130px" FieldName="desperdicio" VisibleIndex="10">
                                                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                                        <PropertiesTextEdit DisplayFormatString="{0:n7}" />
                                                                        <CellStyle CssClass="grid_content"></CellStyle>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Tipo Material" Width="130px" FieldName="tipodematerial" VisibleIndex="11">
                                                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                                        <PropertiesTextEdit DisplayFormatString="{0:n7}" />
                                                                        <CellStyle CssClass="grid_content"></CellStyle>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="MERMA" Width="130px" FieldName="merma" VisibleIndex="12">
                                                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                                        <PropertiesTextEdit DisplayFormatString="{0:n7}" />
                                                                        <CellStyle CssClass="grid_content"></CellStyle>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="División" Width="130px" FieldName="DIVISION" VisibleIndex="13">
                                                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                                        <CellStyle HorizontalAlign="Center" CssClass="grid_content"></CellStyle>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Orden de trabajo" Width="130px" FieldName="ORDEN" VisibleIndex="13">
                                                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                                        <CellStyle HorizontalAlign="Center" CssClass="grid_content"></CellStyle>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Pedimento" Width="130px" FieldName="PEDIMENTO" VisibleIndex="13">
                                                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                                        <CellStyle HorizontalAlign="Center" CssClass="grid_content"></CellStyle>
                                                                    </dx:GridViewDataTextColumn>
                                                                </Columns>
                                                                <ClientSideEvents BeginCallback="" />
                                                                <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" HideDataCellsAtWindowInnerWidth="800"
                                                                    AdaptiveDetailColumnCount="1" AllowOnlyOneAdaptiveDetailExpanded="True">
                                                                    <AdaptiveDetailLayoutProperties ColCount="1">
                                                                        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                                                                    </AdaptiveDetailLayoutProperties>
                                                                </SettingsAdaptivity>
                                                                <SettingsResizing ColumnResizeMode="Control" />
                                                                <Settings ShowFilterRow="true" ShowFilterRowMenu="true" ShowGroupPanel="false" ShowFooter="false"
                                                                    ShowHeaderFilterButton="false" HorizontalScrollBarMode="Auto" />
                                                                <SettingsBehavior AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                                                                <Styles>
                                                                    <SelectedRow />
                                                                    <Header Font-Size="11px" ForeColor="#751473"></Header>

                                                                    <Row Font-Size="11px" />
                                                                    <AlternatingRow Enabled="True" />
                                                                    <PagerTopPanel Paddings-PaddingBottom="3px">
                                                                        <Paddings PaddingBottom="3px"></Paddings>
                                                                    </PagerTopPanel>
                                                                </Styles>
                                                                <SettingsPager>
                                                                    <PageSizeItemSettings Visible="false" Items="10, 20, 50" />
                                                                </SettingsPager>
                                                                <SettingsDataSecurity AllowInsert="false" AllowEdit="false" AllowDelete="false" />
                                                            </dx:ASPxGridView>

                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Text="Resultado">
                                    <ActiveTabStyle Height="200px"></ActiveTabStyle>
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl2" runat="server">
                                            <asp:Panel ID="Panel1" runat="server">
                                                <div class="form-row" style="height: 350px">
                                                    <div class="col-sm-12 col-lg-12">
                                                        <asp:LinkButton ID="lnkExcel" runat="server" CssClass="btn btn-info btn-sm" OnClick="lnkExcel_Click" Visible="true">
                                                        <span class="fa fa-expand"></span>&nbsp;Excel                
                                                        </asp:LinkButton>
                                                        <div style="width: 100%">
                                                            <dx:ASPxGridView ID="grvRes" ClientInstanceName="grvRes_cte" runat="server" SettingsPager-Mode="ShowAllRecords"
                                                                Width="100%" AutoGenerateColumns="False" Settings-HorizontalScrollBarMode="Auto" Theme="DevEx"
                                                                EnableCallBacks="True" Styles-Header-ForeColor="#751473" Styles-Header-Font-Size="11px">
                                                                <Columns>
                                                                    <dx:GridViewDataTextColumn Caption="Id Carga" Width="21%" FieldName="Id" VisibleIndex="1">
                                                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                                        <CellStyle CssClass="grid_content"></CellStyle>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Código Producto" Width="18%" FieldName="codigoProducto" VisibleIndex="2">
                                                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                                        <CellStyle HorizontalAlign="Center" CssClass="grid_content"></CellStyle>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Código Material" Width="15%" FieldName="codigoMaterial" VisibleIndex="3">
                                                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                                        <CellStyle HorizontalAlign="Center" CssClass="grid_content"></CellStyle>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Error" Width="46%" FieldName="error" VisibleIndex="4">
                                                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                                        <CellStyle HorizontalAlign="Center" CssClass="grid_content"></CellStyle>
                                                                    </dx:GridViewDataTextColumn>
                                                                </Columns>
                                                                <ClientSideEvents BeginCallback="" />
                                                                <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" HideDataCellsAtWindowInnerWidth="800"
                                                                    AdaptiveDetailColumnCount="1" AllowOnlyOneAdaptiveDetailExpanded="True">
                                                                    <AdaptiveDetailLayoutProperties ColCount="1">
                                                                        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                                                                    </AdaptiveDetailLayoutProperties>
                                                                </SettingsAdaptivity>
                                                                <SettingsResizing ColumnResizeMode="Control" />
                                                                <Settings ShowFilterRow="true" ShowFilterRowMenu="true" ShowGroupPanel="false" ShowFooter="false"
                                                                    ShowHeaderFilterButton="false" HorizontalScrollBarMode="Auto" />
                                                                <SettingsBehavior AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                                                                <Styles>
                                                                    <SelectedRow />
                                                                    <Header Font-Size="11px" ForeColor="#751473"></Header>

                                                                    <Row Font-Size="11px" />
                                                                    <AlternatingRow Enabled="True" />
                                                                    <PagerTopPanel Paddings-PaddingBottom="3px">
                                                                        <Paddings PaddingBottom="3px"></Paddings>
                                                                    </PagerTopPanel>
                                                                </Styles>
                                                                <SettingsPager>
                                                                    <PageSizeItemSettings Visible="false" Items="10, 20, 50" />
                                                                </SettingsPager>
                                                                <SettingsDataSecurity AllowInsert="false" AllowEdit="false" AllowDelete="false" />
                                                            </dx:ASPxGridView>
                                                            <dx:ASPxGridViewExporter ID="Exporter" GridViewID="grvRes" runat="server" PaperKind="Letter" Landscape="false" EnableViewState="true" />
                                                            <div id="divSucces" visible="false" runat="server" class="alert alert-success" role="alert">
                                                                <strong>Éxito!</strong>El registro se guardo correctamente.
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                            </TabPages>
                        </dx:ASPxPageControl>


                        <button id="btnSucces" type="button" data-toggle="modal" data-target="#modalSucces" style="display: none;"></button>
                        <div class="modal fade" id="modalSucces" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                            <div class="modal-dialog modal-notify modal-success" role="document">
                                <div class="modal-content">
                                    <div class="modal-body">
                                        <div class="text-center">
                                            <i class="fa fa-check fa-4x mb-3 animated rotateIn"></i>
                                            <p id="pSucces" runat="server">
                                            </p>
                                        </div>
                                    </div>
                                    <div class="modal-footer justify-content-center">
                                        <a type="button" class="btn btn-success btn-sm" data-dismiss="modal">Aceptar<i class="fa ml-1 text-white"></i></a>
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
                                        <asp:LinkButton ID="btnAceptarDel" runat="server" CssClass="btn btn-primary btn-sm txt-sm" data-dismiss="modal" OnClientClick="CargaGrid('DelImpo')">
                                    <span class="glyphicon glyphicon-plus"></span>&nbsp;&nbsp;Aceptar
                                        </asp:LinkButton>
                                        <button id="btnCancel" runat="server" class="btn btn-secondary btn-sm" data-dismiss="modal">Cancelar</button>
                                    </div>
                                </div>
                            </div>
                        </div>


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


                    </dx:PanelContent>
                </PanelCollection>
                <ClientSideEvents EndCallback="function(s, e){ UpdatePager(); }"></ClientSideEvents>
            </dx:ASPxCallbackPanel>

            <script src="../ScriptsSaci/CargaCartaMateriales.js"></script>
        </div>
    </div>

    <div style="height: 100px"></div>

</asp:Content>
