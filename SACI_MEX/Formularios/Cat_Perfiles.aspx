<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Formularios/Principal.Master" CodeBehind="Cat_Perfiles.aspx.cs" Inherits="SACI_MEX.Formularios.Cat_Perfiles" EnableEventValidation="true" ValidateRequest="false" EnableViewStateMac="false" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v17.1, Version=17.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>



<asp:Content ID="contPrinc1" ContentPlaceHolderID="head" runat="server">
      
    
    <script type="text/javascript">
        <%-- $(document).ready(function () {
            var TXTCVE = document.getElementById('<%= TXT_CVE_PERFIL.ClientID %>')
            TXTCVE.focus();
        });--%>

        /*Ajustar el tamaño del grvRef al tamaño de la pantalla */
        function OnInitGridPerfil(s, e) {
            var height = Math.max(0, document.documentElement.clientHeight * 0.70);

            GridPerfil.SetHeight(height);
        }

        function rowClick(s, e) {
            window.setTimeout(function () {
                var currentRow = s.GetFocusedRowIndex();
                document.getElementById("<%=HiddenField1.ClientID%>").value = e.visibleIndex;
            }, 100);
        }

        function rowClick2(s, e) {
            window.setTimeout(function () {
                var currentRow = s.GetFocusedRowIndex();
                document.getElementById("<%=IndexGridAdd.ClientID%>").value = e.visibleIndex;
            }, 100);
        }

    </script>
</asp:Content>



<asp:Content ID="contPrinc2" ContentPlaceHolderID="MainContent" runat="server">

    <div style="height: 40px"></div>
    <div class="container">
        <div class="container-fluid">
            <div class="panel-body bordes_curvos" style="background-color: #f8f8f8">

                <%--<h1 id="h1_titulo" runat="server" class="panel-title">Perfiles</h1>
        <hr />--%>
                <div class="divCard tcentrado">
                    <h1 id="h1_titulo" runat="server" class="panel-title">Perfiles</h1>
                </div>

                <%--     <asp:LinkButton ID="lkb_Nuevo" runat="server" OnClientClick="ActivaCve('<%= TXT_CVE_PERFIL.ClientID %>')" data-toggle="modal" data-target="#modalPerfil" CssClass="btn btn-info btn-sm  btn-height">
                    <span class="fa fa-plus"></span>&nbsp;Agregar
        </asp:LinkButton>--%>
                <asp:LinkButton ID="lkb_Nuevo" runat="server" OnClick="lkb_Nuevo_Click" CssClass="btn btn-info btn-sm  btn-height">
                    <span class="fa fa-plus"></span>&nbsp;Agregar
                </asp:LinkButton>
                <asp:LinkButton ID="lkb_Editar" runat="server" CssClass="btn btn-info btn-sm btn-height" OnClick="lkb_Editar_Click">
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
                <dx:ASPxGridView ID="grvPerfiles" ClientInstanceName="GridPerfil" runat="server" KeyFieldName="PK_ID_PERFIL"
                    Width="100%" AutoGenerateColumns="False" Settings-HorizontalScrollBarMode="Auto" Theme="DevEx" Styles-Cell-CssClass="grid_content"
                    EnableCallBacks="true" Styles-Header-ForeColor="#751473" Styles-Header-Font-Size="11px">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="ID" FieldName="PK_ID_PERFIL" Visible="false" />
                        <dx:GridViewDataTextColumn Caption="Clave" Width="50%" FieldName="CVE_PERFIL">
                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn Caption="Nombre" FieldName="NOM_PERFIL" Width="100%">
                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                        </dx:GridViewDataDateColumn>
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
                      <ClientSideEvents Init="OnInitGridPerfil" />
                    <GroupSummary>
                        <dx:ASPxSummaryItem SummaryType="Count" />
                    </GroupSummary>
                </dx:ASPxGridView>
                <dx:ASPxGridViewExporter ID="Exporter" GridViewID="grvPerfiles" runat="server" PaperKind="A5" Landscape="true" />
                <div id="divSucces" visible="false" runat="server" class="alert alert-success" role="alert">
                    <strong>Éxito!</strong>El registro se guardo correctamente.
                </div>


                <button id="btnNuevo" type="button" data-toggle="modal" data-target="#modalPerfil" style="display: none;"></button>
                <div class="modal fade" id="modalPerfil" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-lg" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title" id="titModalPerfil" runat="server"></h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-sm-4 col-md-4">
                                        <asp:TextBox ID="TXT_ID_PERFIL" runat="server" Width="100%" Text="0" CssClass="form-control input-sm" MaxLength="35" Visible="false"></asp:TextBox>
                                        <div runat="server" id="DivPerfil">
                                            <label id="LBLCVE_Perfil" runat="server" class="form-text">Clave</label>
                                            <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                <div class="input-group">
                                                    <asp:TextBox ID="TXT_CVE_PERFIL" runat="server" Width="100%" CssClass="form-control input-sm" MaxLength="35"></asp:TextBox>
                                                </div>
                                                <i runat="server" id="ITXT_CVE_PERFIL"></i>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 col-md-8">
                                        <div runat="server" id="DivNombrePerfil">
                                            <label id="LBLNOM_Perfil" runat="server" class="form-text">Nombre</label>
                                            <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                <div class="input-group">
                                                    <asp:TextBox ID="OBL_TXT_NOM_PERFIL" runat="server" Width="150px" CssClass="form-control input-sm" MaxLength="100"></asp:TextBox>
                                                </div>
                                                <i runat="server" id="ITXT_NOM_PERFIL"></i>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="container horizontal-center-aligned">
                                    <div class="contentEditors">
                                        <div class="row">
                                            <div class="col-sm-5 col-md-5">
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                                <dx:ASPxLabel ID="lblAcciones" Text="Actividades" runat="server" Font-Size="14px"></dx:ASPxLabel>
                                                <dx:ASPxGridView ID="GRIDACTIVITIES" runat="server" EnableTheming="True" Theme="MetropolisBlue"
                                                    EnableCallBacks="false" ClientInstanceName="grid2" AutoGenerateColumns="False" Width="100%"
                                                    Settings-VerticalScrollBarMode="Visible" KeyFieldName="PK_ID_ACTIVIDAD" SettingsPager-PageSize="400">
                                                    <SettingsResizing ColumnResizeMode="Control" />
                                                    <Settings ShowFooter="False" ShowFilterRow="false" ShowFilterRowMenu="true" VerticalScrollableHeight="224" ShowGroupPanel="false" />
                                                    <SettingsBehavior AllowSelectByRowClick="true" AllowSelectSingleRowOnly="false" AllowHeaderFilter="true" FilterRowMode="Auto" AllowAutoFilter="true"
                                                        ProcessSelectionChangedOnServer="false" AllowSort="false" AllowDragDrop="false" AllowGroup="false" />
                                                    <Styles>
                                                        <Header BackColor="#F4F4F4" ForeColor="#000000" Font-Overline="false"
                                                            Font-Underline="false" Font-Bold="false" Font-Size="12px" />
                                                        <SelectedRow BackColor="#9E9E9E" ForeColor="#FFFFFF" />
                                                        <Row />
                                                        <AlternatingRow Enabled="True" />
                                                    </Styles>
                                                    <SettingsDataSecurity AllowInsert="false" AllowEdit="false" AllowDelete="false" />
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn FieldName="PK_ID_ACTIVIDAD" ReadOnly="True" Visible="false">
                                                        </dx:GridViewDataTextColumn>


                                                        <dx:GridViewDataTextColumn Caption="CLAVE" FieldName="CVE_ACTIVIDAD" ReadOnly="True" VisibleIndex="1" Width="70px">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DESCRIPCIÓN" FieldName="NOM_ACTIVIDAD" ReadOnly="True" VisibleIndex="2" Width="200px">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <ClientSideEvents RowClick="rowClick" />
                                                </dx:ASPxGridView>
                                            </div>
                                            <div class="col-sm-2 col-md-2">
                                                <br />
                                                <br />
                                                <div class="contentButtons">
                                                    <div align="center">
                                                        <dx:BootstrapButton ID="bs_Add" runat="server" OnClick="bs_Add_Click" CssClasses-Control="btn btn-primarys btn-sm txt-sm margin-top-bottom-button"
                                                            SettingsBootstrap-RenderOption="Info" AutoPostBack="false" SettingsBootstrap-Sizing="Small" CssClasses-Text="txt-sm" Width="70px" ToolTip="Asignar Seleccionados">
                                                            <ClientSideEvents Click="function(s, e) { GRIDACTIVITIESADD.AddNewRow(); }" />
                                                            <CssClasses Icon="fa fa-forward fa-lg" />
                                                        </dx:BootstrapButton>                                            
                                                    </div>
                                                    <br />
                                                    <div class="TopPadding" align="center">
                                                        <dx:BootstrapButton ID="bs_AddAll" runat="server" OnClick="bs_AddAll_Click" CssClasses-Control="btn-primarys btn-sm txt-sm margin-top-bottom-button"
                                                            SettingsBootstrap-RenderOption="Info" AutoPostBack="false" SettingsBootstrap-Sizing="Small" CssClasses-Text="txt-sm" Width="70px" ToolTip="Asignar Todos">
                                                            <ClientSideEvents Click="function(s, e) { Callback.PerformCallback(); LoadingPanel1.Show(); }"
                                                                Init="function(s, e) {LoadingPanel1.Hide();}" />
                                                            <CssClasses Icon="fa fa-fast-forward fa-lg" />
                                                        </dx:BootstrapButton>                                            
                                                    </div>
                                                    <br />                                       
                                                    <div align="center">
                                                        <dx:BootstrapButton ID="bs_DelAll" runat="server" OnClick="bs_DelAll_Click" CssClasses-Control="btn btn-primarys btn-sm txt-sm margin-top-bottom-button"
                                                            SettingsBootstrap-RenderOption="Info" AutoPostBack="false" SettingsBootstrap-Sizing="Small" CssClasses-Text="txt-sm" Width="70px" ToolTip="Quitar Todos">
                                                            <ClientSideEvents Click="function(s, e) { Callback.PerformCallback(); LoadingPanel1.Show(); }"
                                                                Init="function(s, e) {LoadingPanel1.Hide();}" />
                                                            <CssClasses Icon="fa fa-fast-backward fa-lg" />
                                                        </dx:BootstrapButton>                                           
                                                    </div>
                                                    <br />
                                                    <div class="TopPadding" align="center">
                                                        <dx:BootstrapButton ID="bs_Del" runat="server" OnClick="bs_Del_Click" CssClasses-Control="btn-primarys btn-sm txt-sm margin-top-bottom-button"
                                                            SettingsBootstrap-RenderOption="Info" AutoPostBack="false" SettingsBootstrap-Sizing="Small" CssClasses-Text="txt-sm" Width="70px" ToolTip="Quitar Asignados">
                                                            <ClientSideEvents Click="function(s, e) { Callback.PerformCallback(); LoadingPanel1.Show(); }"
                                                                Init="function(s, e) {LoadingPanel1.Hide();}" />
                                                            <CssClasses Icon="fa fa-backward fa-lg" />
                                                        </dx:BootstrapButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-5 col-md-5">
                                                <div class="contentEditors">
                                                    <asp:HiddenField ID="IndexGridAdd" runat="server" />
                                                    <dx:ASPxLabel ID="lblAsignadas" Text="Asignadas" runat="server" Font-Size="14px"></dx:ASPxLabel>
                                                    <dx:ASPxGridView ID="GRIDACTIVITIESADD" runat="server" EnableTheming="True" Theme="MetropolisBlue"
                                                        EnableCallBacks="false" ClientInstanceName="grid3" AutoGenerateColumns="False" Width="100%"
                                                        Settings-VerticalScrollBarMode="Visible" KeyFieldName="ACTIVIDAD_ID" SettingsPager-PageSize="400">
                                                        <SettingsResizing ColumnResizeMode="Control" />
                                                        <Settings ShowFooter="False" ShowFilterRow="false" ShowFilterRowMenu="true" VerticalScrollableHeight="224" ShowGroupPanel="false" />
                                                        <SettingsBehavior AllowSelectByRowClick="true" AllowSelectSingleRowOnly="false" AllowHeaderFilter="true" FilterRowMode="Auto" AllowAutoFilter="true"
                                                            ProcessSelectionChangedOnServer="false" AllowSort="false" AllowDragDrop="false" AllowGroup="false" />
                                                        <Styles>
                                                            <Header BackColor="#F4F4F4" ForeColor="#000000" Font-Overline="false"
                                                                Font-Underline="false" Font-Bold="false" Font-Size="12px" />
                                                            <SelectedRow BackColor="#9E9E9E" ForeColor="#FFFFFF" />
                                                            <Row />
                                                            <AlternatingRow Enabled="True" />
                                                        </Styles>
                                                        <SettingsDataSecurity AllowInsert="false" AllowEdit="false" AllowDelete="false" />
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn FieldName="ACTIVIDAD_ID" ReadOnly="True" Visible="false">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="CLAVE" FieldName="CVE_ACTIVIDAD" ReadOnly="True" VisibleIndex="1" Width="70px">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="DESCRIPCIÓN" FieldName="NOM_ACTIVIDAD" ReadOnly="True" VisibleIndex="1" Width="200px">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                        <ClientSideEvents RowClick="rowClick2" />
                                                    </dx:ASPxGridView>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <dx:ASPxCallback ID="ASPxCallback1" runat="server" ClientInstanceName="Callback" >
                                        <ClientSideEvents CallbackComplete="function(s, e) { System.Threading.Thread.Sleep(3000); LoadingPanel1.Hide(); }" />
                                    </dx:ASPxCallback>
                                    <dx:ASPxLoadingPanel ID="LoadingPanel1" runat="server" ClientInstanceName="LoadingPanel1"
                                        Modal="True" ViewStateMode="Enabled" Theme="MaterialCompact" >
                                    </dx:ASPxLoadingPanel>

                                </div>


                                <div class="modal-footer">
                                    <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-primary btn-sm" Text="Guardar" OnClick="btnGuardar_Click">
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



