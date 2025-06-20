<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Formularios/Principal.Master" CodeBehind="catProductos.aspx.cs" Inherits="SACI_MEX.Formularios.catProductos" EnableEventValidation="true" ValidateRequest="false" EnableViewStateMac="false" %>

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

        function grid_CustomButtonClick(s, e) {
            var index = e.visibleIndex
            if (index != null) {
                $("#HiddenField1").val(index)
            };
        }


        function gvTest_GetRowValues(values) {
            var TestID = values[0];
        }


        function grid2_CustomButtonClick(s, e) {
            var index = e.visibleIndex
            if (index != null) {
                $("#HiddenAlterna").val(index)
            };

            //EventosProductos('verAlternativo');

        }


        function grid3_CustomButtonClick(s, e) {
            var index = e.visibleIndex
            if (index != null) {
                $("#hiddenFechaEstruct").val(index)
            };

            //__doPostBack('ss', 'sel');
            EventosProductos('seleccionaMateriales');
        }

        function Valida(s, e) {
            var errores = false;

            //VALIDA        
            if (txtClave.GetValue() == null) {
                var label = document.getElementById('<%=LBL_CVE_PRODUCTO.ClientID%>');
                label.style.color = "red";
                errores = true;
            }
            else {
                var label = document.getElementById('<%=LBL_CVE_PRODUCTO.ClientID%>');
                label.style.color = "black";
            }


            if (txtDescripcion.GetValue() == null) {
                var label = document.getElementById('<%=LBL_NOMBRE.ClientID%>');
                label.style.color = "red";
                errores = true;
            }
            else {
                var label = document.getElementById('<%=LBL_NOMBRE.ClientID%>');
                label.style.color = "black";
            }


            if (txtFraccion.GetValue() == null) {
                var label = document.getElementById('<%=LBL_fraccion.ClientID%>');
                label.style.color = "red";
                errores = true;
            }
            else {
                var label = document.getElementById('<%=LBL_fraccion.ClientID%>');
                label.style.color = "black";
            }


            if (cmbUnidad.GetValue() == null) {
                var label = document.getElementById('<%=LBL_UNIDAD.ClientID%>');
                label.style.color = "red";
                errores = true;
            }
            else {
                var label = document.getElementById('<%=LBL_UNIDAD.ClientID%>');
                label.style.color = "black";
            }

            if (errores) {
                alert('Debe agregar los campos obligatorios.');
            }
            else {
                $('#modalProductos').modal('hide');
                cbpCatProductos.PerformCallback('guardarProducto');
            }
        }

        function OnNewClick(s, e) {
            cteGrvFecVig.AddNewRow();
        }

        /*Ajustar el tamaño del grvRef al tamaño de la pantalla */
        function OnInitGridProduc(s, e) {
            var height = Math.max(0, document.documentElement.clientHeight * 0.68);

            GridProuctos.SetHeight(height);
        }

        function OnEndCallbackVig(s, e) {
            if (s.cpRefreshLstMat) {
                delete s.cpRefreshLstMat;
                //alert('Deleted!');
                grvLstMateriales.PerformCallback();
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
                    <h1 id="h2" runat="server" class="panel-title">&nbsp;&nbsp;Catálogos</h1>
                    <h1 id="h1_titulo" runat="server" class="panel-title">&nbsp;&nbsp;Productos</h1>
                </div>
                <br />
                <dx:ASPxCallbackPanel ID="cbpCatProductos" runat="server" ClientInstanceName="cbpCatProductos" OnCallback="cbpCatProductos_Callback">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <asp:HiddenField ID="hdnGuardar" runat="server" ClientIDMode="Static" Value="0" />


                            <asp:LinkButton ID="lkb_Nuevo" runat="server" CssClass="btn btn-info btn-sm  btn-height" OnClick="lkb_Nuevo_Click">
                                <span class="fa fa-plus"></span>&nbsp;Agregar
                            </asp:LinkButton>
                            <asp:LinkButton ID="lkb_Editar" runat="server" CssClass="btn btn-info btn-sm btn-height" OnClientClick="EventosProductos('editarProducto')">
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
                            <asp:HiddenField ID="HiddenField1" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="HiddenFieldFactor" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="hdnProductoKey" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="hdnTituloEstructura" runat="server" ClientIDMode="Static" Value="" />
                            <asp:HiddenField ID="hdnFechaSel" runat="server" ClientIDMode="Static" Value="" />
                            <dx:ASPxGridView ID="grvProductos" ClientInstanceName="GridProuctos" runat="server" KeyFieldName="PRODUCTOKEY"
                                Width="100%" AutoGenerateColumns="False" Settings-HorizontalScrollBarMode="Auto" Theme="DevEx"
                                EnableCallBacks="True" Styles-Header-ForeColor="#751473" Styles-Header-Font-Size="11px"
                                Styles-Cell-CssClass="grid_content" OnCustomColumnSort="grvProductos_CustomColumnSort">
                                <Columns>
                                    <dx:GridViewDataTextColumn ReadOnly="True" Width="100px" VisibleIndex="0" Name="Factor">
                                        <CellStyle HorizontalAlign="Center"></CellStyle>
                                        <DataItemTemplate>
                                            <dx:ASPxButton ID="btnFact" runat="server" Text="Factor" OnClick="btnFact_Click" RenderMode="Link" AutoPostBack="false">
                                                <ClientSideEvents Click="function(s,e){ grid_CustomButtonClick(s,e); } " />
                                            </dx:ASPxButton>
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataColumn Width="140px" VisibleIndex="1" Caption="Lista de Materiales" Name="Estructuras" FieldName="TIENEESTRUCTURA" Settings-ShowFilterRowMenu="False" Settings-AllowAutoFilter="False" Settings-AllowAutoFilterTextInputTimer="False" Settings-AllowHeaderFilter="False">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        <CellStyle HorizontalAlign="Center"></CellStyle>
                                        <DataItemTemplate>
                                            <dx:ASPxButton ID="btn" runat="server" OnClick="btn_Click" RenderMode="Link" AutoPostBack="false" OnInit="btn_Init">
                                                <ClientSideEvents Click="function(s,e){ grid_CustomButtonClick(s,e); } " />
                                            </dx:ASPxButton>
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn Caption="División / Almacén" Width="130px" FieldName="ALMACEN">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="ID" FieldName="PRODUCTOKEY" Visible="false" />
                                    <dx:GridViewDataTextColumn Caption="Clave de identificación" Width="160px" FieldName="CVE_PRODUCTO">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Codigo cliente" Width="130px" FieldName="CVE_PRODUCTO_CLIENTE">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Planta" Width="130px" FieldName="Planta">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Descripción" Width="350px" FieldName="NOMBRE">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Unidad de medida" Width="130px" FieldName="UNIDAD">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Unidad tarifa" Width="130px" FieldName="UNIDADT">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Fracción TIGIE" Width="120px" FieldName="fraccion">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Nico" Width="130px" FieldName="NICO">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Tipo" Width="130px" FieldName="TIPO">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="AMLACENKEY" FieldName="ALMACENKEY">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>

                                    <dx:GridViewDataTextColumn Caption="Auxiliar" Width="130px" FieldName="AUXILIAR">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>

                                    <dx:GridViewDataTextColumn Caption="Valor" Width="130px" FieldName="VALOR">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>

<%--                                    <dx:GridViewDataTextColumn Caption="Moneda" Width="130px" FieldName="MONEDA">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>--%>

                                    <%--<dx:GridViewDataTextColumn Caption="Uso" Width="130px" FieldName="USO">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>--%>

                                    <%--<dx:GridViewDataTextColumn Caption="Cliente" Width="130px" FieldName="CLIENTE">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>--%>

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

                                    <%--<dx:GridViewDataTextColumn Caption="Valor transaccion" Width="180px" FieldName="VALOR_TRANSACCION">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>--%>


                                    <dx:GridViewDataTextColumn Caption="Moneda" Width="180px" FieldName="MONEDA">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Uso" Width="180px" FieldName="USO">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Cliente" Width="180px" FieldName="CLIENTE">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Pais" Width="180px" FieldName="PAIS">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Costo Neto" Width="180px" FieldName="COSTO_NETO">
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
                                <SettingsBehavior AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" AllowSort="true" />
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
                                <%--  <ClientSideEvents Init="grid_Init" BeginCallback="grid_BeginCallback" EndCallback="grid_EndCallback"  RowClick="gridFac_CustomButtonClick" />--%>
                                <ClientSideEvents RowClick="grid_CustomButtonClick" Init="OnInitGridProduc" />
                                <GroupSummary>
                                    <dx:ASPxSummaryItem SummaryType="Count" />
                                </GroupSummary>
                            </dx:ASPxGridView>


                            <dx:ASPxGridViewExporter ID="Exporter" GridViewID="grvProductos" runat="server" PaperKind="A5" Landscape="true" />
                            <%--</div>--%>





                            <div class="container">

                                <button id="btnEstructura" type="button" data-toggle="modal" data-target="#modalEstructura" style="display: none;"></button>
                                <div class="modal fade" id="modalEstructura" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                                    <div class="modal-dialog modal-xl" role="document">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <h4 class="modal-title" id="titEstructuras">Nuevo Producto</h4>
                                            </div>
                                            <div class="modal-body">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <dx:ASPxPopupControl ID="popUpEstructura" ClientInstanceName="popUpEstructura" runat="server" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" Width="1350px" Height="600">
                                    <ModalBackgroundStyle BackColor="#E0E0E0" Opacity="50">
                                    </ModalBackgroundStyle>
                                    <ContentCollection>
                                        <dx:PopupControlContentControl runat="server">
                                            <div class="row">
                                                <div class="col-md-4 col-lg-4">
                                                    <asp:HiddenField ID="hiddenFechaEstruct" runat="server" ClientIDMode="Static" Value="" />
                                                    <label id="Label2" runat="server" class="form-text">Fecha de inicio de aplicación</label>
                                                    <dx:ASPxGridView ID="grvFechaVig" ClientInstanceName="cteGrvFecVig" runat="server" KeyFieldName="estructurakey" OnCustomErrorText="grvFechaVig_CustomErrorText"
                                                        Width="106%" AutoGenerateColumns="False" Styles-Header-Font-Size="11px" OnRowUpdating="grvFechaVig_RowUpdating"
                                                        OnRowDeleting="grvFechaVig_RowDeleting" OnRowInserting="grvFechaVig_RowInserting"
                                                        Styles-Header-ForeColor="#751473" Theme="DevEx" Styles-Cell-CssClass="grid_content" EnableCallBacks="True"
                                                        SettingsPager-Mode="ShowAllRecords" Settings-VerticalScrollableHeight="480" Settings-VerticalScrollBarMode="Visible"
                                                        SettingsText-ConfirmDelete="¿Esta seguro de eliminar este registro?">
                                                        <%-- SettingsPager-Summary-Visible="false"   --%>
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn Caption="estrucKey" FieldName="estructurakey" Visible="false" />
                                                            <%--<dx:GridViewDataTextColumn Width="35" VisibleIndex="0" Name="BorrarEstructura" Visible="false">
                                                                <DataItemTemplate>
                                                                    <dx:ASPxButton ID="btnBorrarEst" runat="server" OnClick="btnBorrarEst_Click" RenderMode="Button" AutoPostBack="true" BackColor="Transparent" Border-BorderStyle="None">
                                                                        <Image Url="../img/del.png" ToolTip="Eliminar"></Image>
                                                                    </dx:ASPxButton>
                                                                </DataItemTemplate>
                                                                <CellStyle HorizontalAlign="Left" />
                                                            </dx:GridViewDataTextColumn>--%>
                                                            <dx:GridViewCommandColumn ShowNewButtonInHeader="true" ShowEditButton="true" ShowDeleteButton="true" VisibleIndex="1" Width="50" Name="comandEstructura">
                                                            </dx:GridViewCommandColumn>
                                                            <dx:GridViewDataDateColumn Caption="Fecha inicio" Width="150" FieldName="inicio" VisibleIndex="2">
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                                                                    <ValidationSettings>
                                                                        <RequiredField IsRequired="true" ErrorText="Falta información" />
                                                                    </ValidationSettings>
                                                                </PropertiesDateEdit>
                                                            </dx:GridViewDataDateColumn>
                                                            <dx:GridViewDataTextColumn Caption="Orden Fab." Width="200" FieldName="ORDEN" VisibleIndex="3" ReadOnly="false" PropertiesTextEdit-MaxLength="50">
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                        <ClientSideEvents RowClick="grid3_CustomButtonClick" EndCallback="OnEndCallbackVig" />
                                                        <SettingsEditing Mode="Inline"></SettingsEditing>
                                                        <SettingsCommandButton>
                                                            <EditButton Text="" RenderMode="Image">
                                                                <Image ToolTip="Editar" Url="../img/edit.png" />
                                                            </EditButton>
                                                            <DeleteButton Text="" RenderMode="Image">
                                                                <Image ToolTip="Eliminar" Url="../img/del.png" />
                                                            </DeleteButton>
                                                            <UpdateButton RenderMode="Image" Text="">
                                                                <Image ToolTip="Guardar" Url="../img/x_paloma.png" />
                                                            </UpdateButton>
                                                            <CancelButton RenderMode="Image" Text="">
                                                                <Image ToolTip="Cancelar" Url="../img/x_exis.png" />
                                                            </CancelButton>
                                                            <NewButton RenderMode="Image" Text="">
                                                                <Image ToolTip="Nuevo" Url="../img/ico_nuevo.png" />
                                                            </NewButton>
                                                        </SettingsCommandButton>
                                                        <EditFormLayoutProperties ColCount="1">
                                                            <Items>
                                                                <dx:GridViewColumnLayoutItem ColumnName="inicio" Width="10%" RequiredMarkDisplayMode="Required" />
                                                                <dx:EditModeCommandLayoutItem Width="10%" HorizontalAlign="Right" RequiredMarkDisplayMode="Required" />
                                                            </Items>
                                                        </EditFormLayoutProperties>
                                                        <EditFormLayoutProperties>
                                                            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                                                        </EditFormLayoutProperties>
                                                        <SettingsBehavior AllowSelectSingleRowOnly="True" AllowFocusedRow="True" ConfirmDelete="true" AllowSort="true" AllowEllipsisInText="true" />
                                                        <SettingsResizing ColumnResizeMode="Control" />
                                                    </dx:ASPxGridView>
                                                    <div id="div6" visible="false" runat="server" class="alert alert-success" role="alert">
                                                        <strong>Éxito!</strong>El registro se guardo correctamente.
                                                    </div>
                                                </div>
                                                <div class="col-sm-8 col-lg-8">
                                                    <asp:HiddenField ID="HiddenAlterna" runat="server" ClientIDMode="Static" />
                                                    <label id="lblTitLstMat" runat="server" class="form-text">Lista de materiales</label>
                                                    <dx:ASPxGridView ID="grvLstMateriales" runat="server" KeyFieldName="PRODMATKEY" Settings-HorizontalScrollBarMode="Visible" Width="100%"
                                                        Settings-VerticalScrollBarMode="Visible" ClientInstanceName="grvLstMateriales"
                                                        Theme="DevEx" Styles-Cell-CssClass="grid_content" Styles-Header-Font-Size="11px" EnableCallBacks="true"
                                                        OnCustomErrorText="grvLstMateriales_CustomErrorText" OnRowUpdating="grvLstMateriales_RowUpdating"
                                                        OnRowInserting="grvLstMateriales_RowInserting" OnRowDeleting="grvLstMateriales_RowDeleting"
                                                        OnInit="grvLstMateriales_Init" Settings-VerticalScrollableHeight="480"
                                                        OnCustomCallback="grvLstMateriales_CustomCallback">
                                                        <%--SettingsPager-Mode="ShowAllRecords" --%>
                                                        <SettingsBehavior AllowEllipsisInText="true" AllowSort="false" />
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn Caption="PRODMATKEY" FieldName="PRODMATKEY" Visible="false" />
                                                            <dx:GridViewCommandColumn ShowNewButtonInHeader="true" ShowEditButton="true" ShowDeleteButton="true" VisibleIndex="0" Name="CommandColumnLista" Visible="false" Width="50" />
                                                            <dx:GridViewDataComboBoxColumn Caption="Clave material" Width="25%" FieldName="CVE_MATERIAL" VisibleIndex="1">
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                                <PropertiesComboBox TextField="nombre" ValueField="clave">
                                                                    <ValidationSettings Display="Dynamic" RequiredField-IsRequired="true" ErrorText="Falta información" />
                                                                </PropertiesComboBox>
                                                            </dx:GridViewDataComboBoxColumn>
                                                            <dx:GridViewDataTextColumn Width="8%" VisibleIndex="2" Caption="Unidad" FieldName="UNIDAD">
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataSpinEditColumn Caption="Utilizado" Width="14%" FieldName="CANT_UTILIZADA" VisibleIndex="3">
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                                <PropertiesSpinEdit DisplayFormatString="n6" DecimalPlaces="6">
                                                                    <ValidationSettings>
                                                                        <RequiredField IsRequired="true" ErrorText="Falta información" />
                                                                    </ValidationSettings>
                                                                </PropertiesSpinEdit>
                                                            </dx:GridViewDataSpinEditColumn>
                                                            <dx:GridViewDataSpinEditColumn Caption="Mermado" Width="14%" FieldName="CANT_MERMADA" VisibleIndex="4">
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                                <PropertiesSpinEdit DisplayFormatString="n6" DecimalPlaces="6">
                                                                    <ValidationSettings>
                                                                        <RequiredField IsRequired="true" ErrorText="Falta información" />
                                                                    </ValidationSettings>
                                                                </PropertiesSpinEdit>
                                                            </dx:GridViewDataSpinEditColumn>
                                                            <dx:GridViewDataSpinEditColumn Caption="Desperdiciado" Width="14%" FieldName="CANT_DESPERDICIADA" VisibleIndex="5">
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                                <PropertiesSpinEdit DisplayFormatString="n6" DecimalPlaces="6">
                                                                    <ValidationSettings>
                                                                        <RequiredField IsRequired="true" ErrorText="Falta información" />
                                                                    </ValidationSettings>
                                                                </PropertiesSpinEdit>
                                                            </dx:GridViewDataSpinEditColumn>
                                                            <dx:GridViewDataTextColumn Width="14%" VisibleIndex="6" Caption="Pedimento" FieldName="AUXILIAR" PropertiesTextEdit-MaxLength="50">
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn ReadOnly="True" Width="11%" VisibleIndex="6" Caption="Alternativo">
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                                <CellStyle HorizontalAlign="Center" />
                                                                <DataItemTemplate>
                                                                    <dx:ASPxButton ID="btnAlternativo" runat="server" Text="" OnClick="btnAlternativo_Click" Width="20px" BackColor="Transparent" Border-BorderStyle="None">
                                                                        <Image IconID="Images/file.gif" Url="../img/ver.png" AlternateText="download"></Image>
                                                                        <ClientSideEvents Click="function(s,e){ grid2_CustomButtonClick(s,e); } " />
                                                                    </dx:ASPxButton>
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                        <ClientSideEvents RowClick="grid2_CustomButtonClick" />
                                                        <SettingsEditing Mode="Inline">
                                                        </SettingsEditing>
                                                        <SettingsCommandButton>
                                                            <EditButton Text="" RenderMode="Image">
                                                                <Image ToolTip="Editar" Url="../img/edit.png" />
                                                            </EditButton>
                                                            <DeleteButton Text="" RenderMode="Image">
                                                                <Image ToolTip="Eliminar" Url="../img/del.png" />
                                                            </DeleteButton>
                                                            <UpdateButton RenderMode="Image" Text="">
                                                                <Image ToolTip="Guardar" Url="../img/x_paloma.png" />
                                                            </UpdateButton>
                                                            <CancelButton RenderMode="Image" Text="">
                                                                <Image ToolTip="Cancelar" Url="../img/x_exis.png" />
                                                            </CancelButton>
                                                            <NewButton RenderMode="Image" Text="">
                                                                <Image ToolTip="Nuevo" Url="../img/ico_nuevo.png" />
                                                            </NewButton>
                                                        </SettingsCommandButton>

                                                        <SettingsBehavior AllowSelectSingleRowOnly="True" AllowFocusedRow="True" ConfirmDelete="true" AllowSort="false" />
                                                    </dx:ASPxGridView>
                                                </div>
                                            </div>
                                            <br />
                                            <%--   <div class="row">
                        <div class="col-12">
                           
                            <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Cancel" CssClass="float-right" OnClick="ASPxButton1_Click">
                            </dx:ASPxButton>
                        </div>
                    </div>--%>
                                        </dx:PopupControlContentControl>
                                    </ContentCollection>
                                </dx:ASPxPopupControl>



                                <dx:ASPxPopupControl ID="popupAlternativo" ClientInstanceName="popupAlternativoCte" runat="server" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above">
                                    <ModalBackgroundStyle BackColor="#E0E0E0" Opacity="50">
                                    </ModalBackgroundStyle>
                                    <ContentCollection>
                                        <dx:PopupControlContentControl runat="server">
                                            <div class="row">
                                                <div class="col-md-12 col-lg-12">
                                                    <asp:HiddenField ID="hiddenAlternativo" runat="server" ClientIDMode="Static" />
                                                    <label id="Label3" runat="server" class="form-text">Codigos alternativos</label>
                                                    <dx:ASPxGridView ID="grvAlternativo" ClientInstanceName="ctegrvAlternativo" runat="server" KeyFieldName="alternativokey" OnCustomErrorText="grvAlternativo_CustomErrorText"
                                                        Width="100%" AutoGenerateColumns="False" Styles-Header-Font-Size="11px" OnRowUpdating="grvAlternativo_RowUpdating"
                                                        OnRowDeleting="grvAlternativo_RowDeleting" OnRowInserting="grvAlternativo_RowInserting"
                                                        Theme="DevEx" Styles-Cell-CssClass="grid_content">
                                                        <%--OnInit="grvAlternativo_Init"--%>
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn Caption="alternativokey" FieldName="alternativokey" Visible="false" />
                                                            <dx:GridViewCommandColumn ShowNewButtonInHeader="true" ShowEditButton="true" ShowDeleteButton="true" VisibleIndex="0" Width="100px" Name="comandAlternativo" />
                                                            <dx:GridViewDataComboBoxColumn Caption="Clave material" Width="200px" FieldName="CVE_MATERIAL" VisibleIndex="1">
                                                                <EditFormSettings VisibleIndex="0" />
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                                <PropertiesComboBox TextField="nombre" ValueField="clave">
                                                                    <ValidationSettings Display="Dynamic" RequiredField-IsRequired="true" ErrorText="Falta información" />
                                                                </PropertiesComboBox>
                                                            </dx:GridViewDataComboBoxColumn>
                                                            <dx:GridViewDataTextColumn Caption="productokey" FieldName="productokey" Visible="false" />
                                                            <dx:GridViewDataTextColumn Caption="prodmatkey" FieldName="prodmatkey" Visible="false" />
                                                        </Columns>
                                                        <SettingsEditing Mode="Inline"></SettingsEditing>
                                                        <SettingsCommandButton>
                                                            <EditButton Text="" RenderMode="Image">
                                                                <Image ToolTip="Editar" Url="../img/edit.png" />
                                                            </EditButton>
                                                            <DeleteButton Text="" RenderMode="Image">
                                                                <Image ToolTip="Eliminar" Url="../img/del.png" />
                                                            </DeleteButton>
                                                            <UpdateButton RenderMode="Image" Text="">
                                                                <Image ToolTip="Guardar" Url="../img/x_paloma.png" />
                                                            </UpdateButton>
                                                            <CancelButton RenderMode="Image" Text="">
                                                                <Image ToolTip="Cancelar" Url="../img/x_exis.png" />
                                                            </CancelButton>
                                                            <NewButton RenderMode="Image" Text="">
                                                                <Image ToolTip="Nuevo" Url="../img/ico_nuevo.png" />
                                                            </NewButton>
                                                        </SettingsCommandButton>
                                                        <EditFormLayoutProperties ColCount="1">
                                                            <Items>
                                                                <dx:GridViewColumnLayoutItem ColumnName="inicio" Width="10%" RequiredMarkDisplayMode="Required" />
                                                                <dx:EditModeCommandLayoutItem Width="10%" HorizontalAlign="Right" RequiredMarkDisplayMode="Required" />
                                                            </Items>
                                                        </EditFormLayoutProperties>
                                                        <EditFormLayoutProperties>
                                                            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                                                        </EditFormLayoutProperties>
                                                        <SettingsBehavior AllowSelectSingleRowOnly="True" AllowFocusedRow="True" ConfirmDelete="true" AllowSort="false" />
                                                    </dx:ASPxGridView>
                                                    <div id="div7" visible="false" runat="server" class="alert alert-success" role="alert">
                                                        <strong>Éxito!</strong>El registro se guardo correctamente.
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <%--   <div class="row">
                        <div class="col-12">
                           
                            <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Cancel" CssClass="float-right" OnClick="ASPxButton1_Click">
                            </dx:ASPxButton>
                        </div>
                    </div>--%>
                                        </dx:PopupControlContentControl>
                                    </ContentCollection>
                                </dx:ASPxPopupControl>



                                <dx:ASPxPopupControl ID="popFactor" ClientInstanceName="popFactor" runat="server" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" Width="500px">
                                    <ModalBackgroundStyle BackColor="#E0E0E0" Opacity="50">
                                    </ModalBackgroundStyle>
                                    <ContentCollection>
                                        <dx:PopupControlContentControl runat="server">
                                            <div class="row">
                                                <div class="col-md-12 col-lg-12">
                                                    <asp:HiddenField ID="hiddFactorGrid" runat="server" ClientIDMode="Static" />
                                                    <asp:HiddenField ID="hdnUnidadSel" runat="server" ClientIDMode="Static" Value="" />
                                                    <asp:HiddenField ID="hdnClaveProductoSel" runat="server" ClientIDMode="Static" Value="" />
                                                    <asp:HiddenField ID="hdnClaveProdClienteSel" runat="server" ClientIDMode="Static" Value="" />
                                                    <br />
                                                    <dx:ASPxGridView ID="grvFactorProd" ClientInstanceName="ctegrvFactorProd" runat="server" KeyFieldName="FactoresMPKey"
                                                        Width="100%" AutoGenerateColumns="False" Styles-Header-Font-Size="11px"
                                                        OnInitNewRow="grvFactorProd_InitNewRow" OnCustomErrorText="grvFactorProd_CustomErrorText"
                                                        OnRowUpdating="grvFactorProd_RowUpdating" OnRowDeleting="grvFactorProd_RowDeleting"
                                                        OnRowInserting="grvFactorProd_RowInserting" Theme="DevEx" Styles-Cell-CssClass="grid_content">
                                                        <%--OnCellEditorInitialize="grvFactorProd_CellEditorInitialize" --%>
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn Caption="detKey" FieldName="FactoresMPKey" Visible="false" />
                                                            <dx:GridViewCommandColumn VisibleIndex="0" ShowNewButtonInHeader="true" ShowEditButton="true" ShowDeleteButton="true" Name="comandFactor" />
                                                            <dx:GridViewDataTextColumn Caption="Unidad" Width="30%" FieldName="Unidad" VisibleIndex="1" ReadOnly="true">
                                                                <EditFormSettings VisibleIndex="0" />
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataComboBoxColumn Caption="Unidad a convertir" Width="35%" FieldName="UnidadConvertir" VisibleIndex="2">
                                                                <EditFormSettings VisibleIndex="1" />
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                                <PropertiesComboBox TextField="CVE_UNIDAD" ValueField="CVE_UNIDAD">
                                                                    <ValidationSettings Display="Dynamic" RequiredField-IsRequired="true" ErrorText="Falta información" />
                                                                </PropertiesComboBox>
                                                            </dx:GridViewDataComboBoxColumn>
                                                            <dx:GridViewDataSpinEditColumn Caption="Factor" Width="33%" FieldName="Factor" VisibleIndex="3" PropertiesSpinEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesSpinEdit-ValidationSettings-RequiredField-ErrorText="Falta información">
                                                                <EditFormSettings VisibleIndex="2" />
                                                                <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                            </dx:GridViewDataSpinEditColumn>
                                                        </Columns>
                                                        <SettingsCommandButton CancelButton-Text="Cancelar" NewButton-Text="Nuevo" UpdateButton-Text="Guardar" EditButton-Text="Editar" DeleteButton-Text="Eliminar"></SettingsCommandButton>
                                                        <EditFormLayoutProperties ColCount="1">
                                                            <Items>
                                                                <dx:GridViewColumnLayoutItem ColumnName="Unidad" Width="35%" />
                                                                <dx:GridViewColumnLayoutItem ColumnName="UnidadConvertir" Width="35%" RequiredMarkDisplayMode="Required" />
                                                                <dx:GridViewColumnLayoutItem ColumnName="Factor" Width="30%" RequiredMarkDisplayMode="Required" />
                                                                <dx:EditModeCommandLayoutItem Width="100%" HorizontalAlign="Right" />
                                                            </Items>
                                                        </EditFormLayoutProperties>
                                                        <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" HideDataCellsAtWindowInnerWidth="800"
                                                            AdaptiveDetailColumnCount="1" AllowOnlyOneAdaptiveDetailExpanded="True">
                                                            <AdaptiveDetailLayoutProperties ColCount="1">
                                                                <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                                                            </AdaptiveDetailLayoutProperties>
                                                        </SettingsAdaptivity>
                                                        <SettingsBehavior AllowSelectSingleRowOnly="True" AllowFocusedRow="True" ConfirmDelete="true" AllowSort="false" />
                                                        <Styles>
                                                            <SelectedRow />
                                                            <Row Font-Size="11px" />
                                                            <AlternatingRow Enabled="True" />
                                                            <PagerTopPanel Paddings-PaddingBottom="3px"></PagerTopPanel>
                                                        </Styles>
                                                    </dx:ASPxGridView>

                                                    <div id="div8" visible="false" runat="server" class="alert alert-success" role="alert">
                                                        <strong>Éxito!</strong>El registro se guardo correctamente.
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                        </dx:PopupControlContentControl>
                                    </ContentCollection>
                                </dx:ASPxPopupControl>


                            </div>

                            <button id="btnNuevo" type="button" data-toggle="modal" data-target="#modalProductos" style="display: none;"></button>
                            <div class="modal fade" id="modalProductos" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                                <div class="modal-dialog modal-lg" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h4 class="modal-title" runat="server" id="titProducto">Nuevo Producto</h4>
                                        </div>
                                        <div class="modal-body">
                                            <div class="form-group row" style="height: 43px">
                                                <div class="col-sm-6 col-md-6">
                                                    <asp:TextBox ID="TXT_PRODUCTOKEY" runat="server" Width="100%" Text="0" CssClass="control-text" Visible="false"></asp:TextBox>
                                                    <div runat="server" id="DivActividad">
                                                        <label id="LBL_CVE_PRODUCTO" runat="server" class="form-text">Clave de identificación *</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_CVE_PRODUCTO" runat="server" Width="100%" CssClass="control-text" MaxLength="50" ClientInstanceName="txtClave"></dx:ASPxTextBox>
                                                            </div>
                                                            <i runat="server" id="ITXT_CVE_PRODUCTO"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 col-md-6">
                                                    <div runat="server" id="Div3">
                                                        <label id="LBL_CVE_PRODUCTO_CLIENTE" runat="server" class="form-text">Código cliente</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_CVE_PRODUCTO_CLIENTE" runat="server" Width="100%" CssClass="control-text" MaxLength="30"></dx:ASPxTextBox>
                                                            </div>
                                                            <i runat="server" id="ITXT_CVE_PRODUCTO_CLIENTE"></i>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="form-group row" style="height: 43px">
                                                <div class="col-sm-12 col-md-12">
                                                    <div runat="server" id="DivNombreAcc">
                                                        <label id="LBL_NOMBRE" runat="server" class="form-text">Descripción *</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_NOMBRE" runat="server" Width="100%" CssClass="control-text" MaxLength="100" ClientInstanceName="txtDescripcion"></dx:ASPxTextBox>
                                                            </div>
                                                            <i runat="server" id="ITXT_NOMBRE"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row" style="height: 43px">
                                                <div class="col-sm-4 col-md-4">
                                                    <div runat="server" id="Div1">
                                                        <label id="LBL_UNIDAD" runat="server" class="form-text">Unidad de medida *</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxComboBox ID="CMB_UNIDAD" runat="server" CssClass="control-text" ValueField="CVE_UNIDAD" TextField="CVE_UNIDAD" ClientInstanceName="cmbUnidad">
                                                                </dx:ASPxComboBox>
                                                            </div>
                                                            <i runat="server" id="ITXT_UNIDAD"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4 col-md-4">
                                                    <div runat="server" id="DivUNIDADT">
                                                        <label id="LBL_UNIDADT" runat="server" class="form-text">Unidad tarifa</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxComboBox ID="CMB_UNIDADT" runat="server" CssClass="control-text" ValueField="CVE_UNIDAD" TextField="CVE_UNIDAD">
                                                                </dx:ASPxComboBox>
                                                            </div>
                                                            <i runat="server" id="ICMB_UNIDADT"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4 col-md-4">
                                                    <div runat="server" id="Div2">
                                                        <label id="LBL_fraccion" runat="server" class="form-text">Fracción TIGIE *</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <%--<asp:TextBox ID="TXT_fraccion" runat="server" Width="130px" CssClass="form-control input-sm" MaxLength="100" BorderColor="Red"></asp:TextBox>--%>
                                                                <dx:ASPxTextBox ID="TXT_fraccion" runat="server" MaxLength="8" Width="100%" Height="25px" CssClass="control-text" ClientInstanceName="txtFraccion">
                                                                    <ClientSideEvents LostFocus="function(s,e){if(s.GetText().length<8){alert('Este campo debe ser de 8 digitos'); s.Focus() }}" />
                                                                </dx:ASPxTextBox>
                                                            </div>
                                                            <i runat="server" id="I1"></i>
                                                        </div>
                                                    </div>
                                                </div>


                                            </div>

                                            <div class="form-group row" style="height: 43px">
                                                <div class="col-sm-4 col-md-4">
                                                    <div runat="server" id="Div9">
                                                        <label id="Label4" runat="server" class="form-text">Nico</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_NICO" runat="server" Width="150px" CssClass="control-text" MaxLength="5"></dx:ASPxTextBox>
                                                            </div>
                                                            <i runat="server" id="ITXT_NICO"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4 col-md-4">
                                                    <div runat="server" id="Div4">
                                                        <label id="LBL_TIPO" runat="server" class="form-text">Tipo</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_TIPO" runat="server" Width="150px" CssClass="control-text" MaxLength="20"></dx:ASPxTextBox>
                                                            </div>
                                                            <i runat="server" id="ITXT_TIPO"></i>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-4 col-md-4">
                                                    <div runat="server" id="DivALMACEN">
                                                        <label id="LBL_ALMACEN" runat="server" class="form-text">Almacén</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxComboBox ID="CMB_ALMACEN" runat="server" CssClass="control-text" ValueField="ALMACENKEY" TextField="ALMACEN">
                                                                </dx:ASPxComboBox>
                                                            </div>
                                                            <i runat="server" id="ICMB_ALMACEN"></i>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="form-group row" style="height: 43px">
                                                <div class="col-sm-4 col-md-4">
                                                    <div runat="server" id="Div5">
                                                        <label id="Label1" runat="server" class="form-text">Auxiliar</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_AUXILIAR" runat="server" Width="130px" CssClass="control-text" MaxLength="50"></dx:ASPxTextBox>
                                                            </div>
                                                            <i runat="server" id="ITXT_AUXILIAR"></i>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-4 col-md-4">
                                                    <div runat="server" id="Div10">
                                                    </div>
                                                    <div runat="server" id="Div11">
                                                        <label id="Label5" runat="server" class="form-text">Unidad americana</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_UM_AMERICANA" ClientInstanceName="TXT_UM_AMERICANA" runat="server" MaxLength="50" Width="100%" Height="20px"></dx:ASPxTextBox>
                                                            </div>
                                                            <i runat="server" id="I2"></i>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-4 col-md-4">
                                                    <div runat="server" id="Div12">
                                                    </div>
                                                    <div runat="server" id="Div13">
                                                        <label id="Label6" runat="server" class="form-text">Factor unidad americana</label>
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

                                            <div class="form-group row" style="height: 43px">
                                                <div class="col-sm-4 col-md-4">
                                                    <div runat="server" id="Div14">
                                                    </div>
                                                    <div runat="server" id="Div15">
                                                        <label id="Label7" runat="server" class="form-text">Valor transaccion</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxSpinEdit ID="TXT_VALOR" runat="server" Number="0">
                                                                </dx:ASPxSpinEdit>
                                                            </div>
                                                            <i runat="server" id="I4"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4 col-md-4">
                                                    <div runat="server" id="Div16">
                                                    </div>
                                                    <div runat="server" id="Div17">
                                                        <label id="Label8" runat="server" class="form-text">Moneda</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_MONEDA" ClientInstanceName="TXT_MONEDA" runat="server" MaxLength="10" Width="100%" Height="20px"></dx:ASPxTextBox>
                                                            </div>
                                                            <i runat="server" id="I5"></i>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-4 col-md-4">
                                                    <div runat="server" id="Div18">
                                                    </div>
                                                    <div runat="server" id="Div19">
                                                        <label id="Label9" runat="server" class="form-text">Uso</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_USO" ClientInstanceName="TXT_USO" runat="server" MaxLength="20" Width="100%" Height="20px"></dx:ASPxTextBox>
                                                            </div>
                                                            <i runat="server" id="I6"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row" style="height: 43px">

                                                <div class="col-sm-4 col-md-4">
                                                    <div runat="server" id="Div20">
                                                    </div>
                                                    <div runat="server" id="Div21">
                                                        <label id="Label10" runat="server" class="form-text">Cliente</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_CLIENTE" ClientInstanceName="TXT_CLIENTE" runat="server" MaxLength="50" Width="100%" Height="20px"></dx:ASPxTextBox>
                                                            </div>
                                                            <i runat="server" id="I7"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4 col-md-4">
                                                    <div runat="server" id="Div22">
                                                    </div>
                                                    <div runat="server" id="Div23">
                                                        <label id="Label11" runat="server" class="form-text">País</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_PAIS" ClientInstanceName="TXT_PAIS" runat="server" MaxLength="3" Width="100%" Height="20px"></dx:ASPxTextBox>
                                                            </div>
                                                            <i runat="server" id="I8"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4 col-md-4">
                                                    <div runat="server" id="Div24">
                                                    </div>
                                                    <div runat="server" id="Div25">
                                                        <label id="Label12" runat="server" class="form-text">Costo Neto</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_COSTONETO" ClientInstanceName="TXT_COSTONETO" runat="server" MaxLength="50" Width="100%" Height="20px"></dx:ASPxTextBox>
                                                            </div>
                                                            <i runat="server" id="I9"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="modal-footer">
                                                <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-primary btn-sm" Text="Guardar" OnClientClick="Valida()">
                                <span class="glyphicon glyphicon-ok"></span>&nbsp;&nbsp;Guardar</asp:LinkButton>
                                                <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal" onclick="LimpiaControles()">Cancelar</button>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>



                            <button id="btnSucces" type="button" data-toggle="modal" data-target="#modalSucces" style="display: none;"></button>
                            <div class="modal fade" id="modalSucces" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                                <div class="modal-dialog modal-notify modal-success" role="document">
                                    <!--Content-->
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
                                            <asp:LinkButton ID="btnAceptarDel" runat="server" CssClass="btn btn-primary btn-sm txt-sm" data-dismiss="modal" OnClientClick="EventosProductos('eliminarProducto')">
                    <span class="glyphicon glyphicon-plus"></span>&nbsp;&nbsp;Aceptar
                                            </asp:LinkButton>
                                            <button id="btnCancel" runat="server" class="btn btn-secondary btn-sm" data-dismiss="modal">Cancelar</button>
                                        </div>
                                    </div>
                                </div>
                            </div>




                            <button id="btnAlternativo" type="button" data-toggle="modal" data-target="#modalAlternativo" style="display: none;">
                            </button>
                            <div class="modal fade " id="modalAlternativo" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" data-backdrop="static">
                                <div class="modal-dialog modal-lg " role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h4 class="modal-title" id="titalternativo" runat="server"></h4>
                                        </div>
                                        <div class="modal-body">
                                            <div class="row">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <button id="btnMaterial" type="button" data-toggle="modal" data-target="#modalListMateriales" style="display: none;"></button>
                            <div class="modal fade" id="modalListMateriales" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                                <div class="modal-dialog" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h4 class="modal-title" id="H1" runat="server">Material</h4>
                                        </div>
                                        <div class="modal-body">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <label id="lblClave" runat="server" class="form-text">Clave material</label>
                                                    <dx:ASPxComboBox ID="CVE_MATERIAL1" runat="server" Width="100%" CssClass="control-text" ClientInstanceName="cmbSite" CallbackPageSize="10"
                                                        DropDownStyle="DropDownList" ValueField="CVE_MATERIAL">
                                                    </dx:ASPxComboBox>
                                                    <asp:RequiredFieldValidator ID="reqCveMat" ControlToValidate="CVE_MATERIAL1" ValidationGroup="RequiedInfoGroup" ErrorMessage="***Falta Información" ForeColor="red" runat="Server">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-md-4">
                                                    <label id="Label16" runat="server" class="form-text">Utilizado</label>
                                                    <dx:ASPxSpinEdit ID="TXT_INCORPORADO" runat="server" Width="100%" CssClass="control-text" HorizontalAlign="Right" DisplayFormatString="n4">
                                                    </dx:ASPxSpinEdit>
                                                    <asp:RequiredFieldValidator ID="reqCantidad" ControlToValidate="TXT_INCORPORADO" ValidationGroup="RequiedInfoGroup" ErrorMessage="***Falta Información" ForeColor="red" runat="Server">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-4">
                                                    <label id="lblValorComer" runat="server" class="form-text">Mermado</label>
                                                    <dx:ASPxSpinEdit ID="TXT_MERMA" runat="server" Width="100%" CssClass="control-text" HorizontalAlign="Right" DisplayFormatString="n2">
                                                    </dx:ASPxSpinEdit>
                                                    <asp:RequiredFieldValidator ID="reqValorCommer" ControlToValidate="TXT_MERMA" ValidationGroup="RequiedInfoGroup" ErrorMessage="***Falta Información" ForeColor="red" runat="Server">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-4">
                                                    <label id="lblValDolares" runat="server" class="form-text">Desperdiciado</label>
                                                    <dx:ASPxSpinEdit ID="TXT_DESPERDICIO" runat="server" Width="100%" CssClass="control-text" HorizontalAlign="Right" DisplayFormatString="n2">
                                                    </dx:ASPxSpinEdit>
                                                    <asp:RequiredFieldValidator ID="reqValorDll" ControlToValidate="TXT_DESPERDICIO" ValidationGroup="RequiedInfoGroup" ErrorMessage="***Falta Información" ForeColor="red" runat="Server">
                                                    </asp:RequiredFieldValidator>
                                                </div>

                                            </div>

                                        </div>
                                        <div class="modal-footer">

                                            <asp:LinkButton ID="lnkGuardarPartida" runat="server" CssClass="btn btn-primary btn-sm txt-sm" data-dismiss="modal" OnClientClick="EventosDescargos('guardarNP')" ValidationGroup="RequiedInfoGroup" CausesValidation="true">
                                                    <span class="glyphicon glyphicon-plus"></span>&nbsp;&nbsp;Guardar
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lnk_CancelarPartida" runat="server" CssClass="btn btn-primary btn-sm txt-sm" data-dismiss="modal" OnClientClick="EventosDescargos('cancelaNP')">
                                                    <span class="glyphicon glyphicon-plus"></span>&nbsp;&nbsp;Cancelar
                                            </asp:LinkButton>

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

                <script src="../ScriptsSaci/catProductos.js"></script>
            </div>
        </div>
    </div>
    <div style="height: 100px"></div>
</asp:Content>
