<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Formularios/Principal.Master" CodeBehind="default.aspx.cs" Inherits="SACI_MEX._default" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>



<asp:Content ID="contPrinc1" ContentPlaceHolderID="head" runat="server">
    <style>
        @media only screen and (max-width: 766px) {
            .collapsing, .in {
                background-color: #f7f7f7;
                color: #555 !important;
            }

                .collapsing ul li a, .in ul li a {
                    color: #555 !important;
                }

                    .collapsing ul li a:hover, .in ul li a:hover {
                        color: #000 !important;
                        background-color: #E7E7E7 !important;
                    }


            /*Se usa para poder ver el limite inferior del dx:ASPxSplitter */
            /*.padding-bottom-sm {
                padding-bottom: 30%;
            }*/
        }
    </style>
    <script type="text/javascript">
        function ShowInfo() {
            var x = document.getElementById("myDIV");
            if (x.style.display === "none") {
                x.style.display = "block";
            } else {
                x.style.display = "none";
            }
        }




        /*Ajustar el tamaño del PageControl1 al tamaño de la pantalla */
        function OnInitPageControl1(s, e) {
            var height = Math.max(0, document.documentElement.clientHeight * 0.79);

            pagePrincipal.SetHeight(height);

        }
    </script>



</asp:Content>



<asp:Content ID="contPrinc2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="height: 40px"></div>

    <div class="container-fluid">
        <div class="panel-body bordes_curvos" style="background-color: #f8f8f8">
            <dx:ASPxCallbackPanel ID="cbpDefault" runat="server" Width="100%" ClientInstanceName="cbpDefault" OnCallback="cbpDefault_Callback">
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <asp:HiddenField ID="hdnGuardar" runat="server" ClientIDMode="Static" Value="0" />



                        <dx:ASPxPageControl runat="server" ID="PageControlDefault" Height="100%" Width="100%" ContentStyle-Border-BorderWidth="3px" ClientInstanceName="pagePrincipal"
                            TabAlign="Justify" EnableCallBacks="false" EnableHierarchyRecreation="true" EnableTabScrolling="true" Theme="SoftOrange" Font-Size="12px"
                            ContentStyle-VerticalAlign="Top" TabStyle-BackColor="#751473" ActiveTabStyle-BackColor="#751473" AutoPostBack="false" ActiveTabIndex="0" Visible="false">
                            <Paddings Padding="0px" />
                            <TabStyle Paddings-PaddingLeft="50px" Paddings-PaddingRight="50px" ForeColor="#751473">
                                <Paddings PaddingLeft="50px" PaddingRight="50px"></Paddings>
                            </TabStyle>
                            <ActiveTabStyle BackColor="#751473" ForeColor="#751473" />
                            <ContentStyle>
                                <Paddings PaddingLeft="20px" PaddingRight="20px" PaddingTop="5px" />
                            </ContentStyle>
                            <ClientSideEvents Init="OnInitPageControl1" />
                            <TabPages>
                                <dx:TabPage Text="Información general">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl9" runat="server">
                                            <asp:Panel ID="Panel9" runat="server">
                                                <div class="form row" style="height: 500px; width: 100%">
                                                    <div class="col-sm-12 col-lg-12">
                                                        <br />
                                                        <div>
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 50%">
                                                                        <dx:ASPxLabel ID="lblTituloGral" runat="server" Font-Size="Medium" Text="Vencimientos"></dx:ASPxLabel>
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:LinkButton ID="lkb_rptSaldos" runat="server" PostBackUrl="~/Formularios/rptSaldos.aspx" ForeColor="Navy" Font-Size="Small">
                                                    <img src="../img/report.png"/>
                                                    Ver Reporte
                                                                </asp:LinkButton>
                                                                    </td>
                                                                    <td style="width: 50%; text-align: right">
                                                                        <dx:ASPxLabel ID="lblDiasRestantes" runat="server" Font-Size="Small" ForeColor="IndianRed"></dx:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>

                                                        <hr />
                                                        <dx:ASPxGridView ID="grvVencimientos" ClientInstanceName="gridVenc" runat="server" KeyFieldName="MENSAJE"
                                                            Width="100%" AutoGenerateColumns="False" Settings-HorizontalScrollBarMode="Auto" Theme="DevEx" Styles-Cell-CssClass="grid_content"
                                                            EnableCallBacks="True" Styles-Header-ForeColor="#751473" Styles-Header-Font-Size="11px" Settings-VerticalScrollBarMode="Auto" Settings-VerticalScrollableHeight="100">
                                                            <SettingsBehavior AllowEllipsisInText="true" AllowSort="false" />
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn Caption="Mensaje" FieldName="MENSAJE" Width="60%" VisibleIndex="0">
                                                                    <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Cantidad" FieldName="CANTIDAD" Width="40%" VisibleIndex="0">
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
                                                            <Settings ShowFilterRow="true" ShowFilterRowMenu="true" ShowGroupPanel="false" ShowFooter="false"
                                                                ShowHeaderFilterButton="false" HorizontalScrollBarMode="Auto" />
                                                            <SettingsBehavior AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                                                            <Styles>
                                                                <SelectedRow />
                                                                <Row Font-Size="11px" />
                                                                <AlternatingRow Enabled="True" />
                                                                <PagerTopPanel Paddings-PaddingBottom="3px"></PagerTopPanel>
                                                            </Styles>
                                                            <SettingsPager>
                                                                <PageSizeItemSettings Visible="false" Items="10, 20, 50" />
                                                            </SettingsPager>
                                                            <SettingsDataSecurity AllowInsert="false" AllowEdit="false" AllowDelete="false" />
                                                        </dx:ASPxGridView>
                                                        <br />
                                                        <br />
                                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Size="Medium" Text="Mensajes"></dx:ASPxLabel>
                                                        <hr />
                                                        <dx:ASPxGridView ID="grvWarnings" ClientInstanceName="gridWarning" runat="server" KeyFieldName="MENSAJE"
                                                            Width="100%" AutoGenerateColumns="False" Settings-HorizontalScrollBarMode="Auto" Theme="DevEx" Styles-Cell-CssClass="grid_content"
                                                            EnableCallBacks="True" Styles-Header-ForeColor="#751473" Styles-Header-Font-Size="11px" Settings-VerticalScrollBarMode="Auto" Settings-VerticalScrollableHeight="100">
                                                            <SettingsBehavior AllowEllipsisInText="true" AllowSort="false" />
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn Caption="Mensaje" FieldName="MENSAJE" Width="60%" VisibleIndex="0">
                                                                    <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Cantidad" FieldName="CANTIDAD" Width="40%" VisibleIndex="0">
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
                                                            <Settings ShowFilterRow="true" ShowFilterRowMenu="true" ShowGroupPanel="false" ShowFooter="false"
                                                                ShowHeaderFilterButton="false" HorizontalScrollBarMode="Auto" />
                                                            <SettingsBehavior AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                                                            <Styles>
                                                                <SelectedRow />
                                                                <Row Font-Size="11px" />
                                                                <AlternatingRow Enabled="True" />
                                                                <PagerTopPanel Paddings-PaddingBottom="3px"></PagerTopPanel>
                                                            </Styles>
                                                            <SettingsPager>
                                                                <PageSizeItemSettings Visible="false" Items="10, 20, 50" />
                                                            </SettingsPager>
                                                            <SettingsDataSecurity AllowInsert="false" AllowEdit="false" AllowDelete="false" />
                                                        </dx:ASPxGridView>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Text="Detalle errores">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl1" runat="server">
                                            <asp:Panel ID="Panel1" runat="server">
                                                <div class="form row" style="height: 500px; width: 100%">
                                                    <div class="col-sm-12 col-lg-12">
                                                        <asp:LinkButton ID="lkb_verDetalle" runat="server" Width="130px" CssClass="btn btn-info btn-sm btn-width btn-height" OnClientClick="EventosInicio('GenerarDetalle')">
                        <span class="fa fa-bars"></span>&nbsp;ver Detalle
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lkb_Excel" runat="server" CssClass="btn btn-info btn-sm btn-width btn-height" OnClick="lkb_Excel_Click">
                        <span class="fa fa-expand"></span>&nbsp;Excel
                                                        </asp:LinkButton>
                                                        <br />
                                                        <dx:ASPxGridView ID="grvDetWarnings" ClientInstanceName="gridDetWarning" runat="server" KeyFieldName="TIPO"
                                                            Width="100%" AutoGenerateColumns="False" Settings-HorizontalScrollBarMode="Auto" Theme="DevEx" Styles-Cell-CssClass="grid_content"
                                                            EnableCallBacks="True" Styles-Header-ForeColor="#751473" Styles-Header-Font-Size="11px" Settings-VerticalScrollBarMode="Auto" Settings-VerticalScrollableHeight="300">
                                                            <SettingsBehavior AllowEllipsisInText="true" AllowSort="false" />
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn Caption="Detalle" FieldName="TIPO" Width="100%" VisibleIndex="0">
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
                                                            <Settings ShowFilterRow="true" ShowFilterRowMenu="true" ShowGroupPanel="false" ShowFooter="false"
                                                                ShowHeaderFilterButton="false" HorizontalScrollBarMode="Auto" />
                                                            <SettingsBehavior AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                                                            <Styles>
                                                                <SelectedRow />
                                                                <Row Font-Size="11px" />
                                                                <AlternatingRow Enabled="True" />
                                                                <PagerTopPanel Paddings-PaddingBottom="3px"></PagerTopPanel>
                                                            </Styles>
                                                            <SettingsPager>
                                                                <PageSizeItemSettings Visible="false" Items="10, 20, 50" />
                                                            </SettingsPager>
                                                            <SettingsDataSecurity AllowInsert="false" AllowEdit="false" AllowDelete="false" />
                                                        </dx:ASPxGridView>
                                                        <dx:ASPxGridViewExporter ID="Exporter" GridViewID="grvDetWarnings" runat="server" PaperKind="A5" Landscape="true" />
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                            </TabPages>
                        </dx:ASPxPageControl>

                        <button id="btnError" type="button" data-toggle="modal" data-target="#AlertError" style="display: none;"></button>
                        <div class="modal fade bd-example-modal-lg" id="AlertError" tabindex="-1" role="dialog">
                            <div class="modal-dialog modal-lg" role="document" style="top: 12%; outline: none;">
                                <div class=" alert  alert-warning text-center" style="-webkit-box-shadow: 0 5px 15px rgba(0, 0, 0, .5); box-shadow: 0 5px 15px rgba(0, 0, 0, .5);">
                                    <img src="img/warning.PNG" class="imagen-logo-warning" />
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



        </div>
    </div>
    <div style="height: 100px"></div>

    <script src="../ScriptsSaci/pagInicio.js"></script>
</asp:Content>

