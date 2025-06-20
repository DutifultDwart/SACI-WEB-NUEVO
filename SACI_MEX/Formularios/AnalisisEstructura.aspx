<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Formularios/Principal.Master" CodeBehind="AnalisisEstructura.aspx.cs" Inherits="SACI_MEX.Formularios.AnalisisEstructura" EnableEventValidation="true" ValidateRequest="false" EnableViewStateMac="false" %>

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

            //VALIDA CLAVE             
            if (DESDE.GetValue() == null) {
                var label = document.getElementById('<%=DESDE.ClientID%>');
                label.style.color = "red";
                errores = true;
            }
            else {
                var label = document.getElementById('<%=DESDE.ClientID%>');
                label.style.color = "black";
            }

            //VALIDA DESCRIPCION             
            if (HASTA.GetValue() == null) {
                var label = document.getElementById('<%=HASTA.ClientID%>');
                label.style.color = "red";
                errores = true;
            }
            else {
                var label = document.getElementById('<%=HASTA.ClientID%>');
                label.style.color = "black";
            }


            if (errores) {
                alert('Debe agregar los campos obligatorios.');
            }
            else {
                cbpCatAnaEsts.PerformCallback('buscaAnaEst');
            }
        }

        /*Ajustar el tamaño del grvRef al tamaño de la pantalla */
        function OnInitGridAnaEsts(s, e) {
            var height = Math.max(0, document.documentElement.clientHeight * 0.70);

            gridT.SetHeight(height);
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

        function EventosAnaEsts(s, e) {
            cbpCatAnaEsts.PerformCallback(s, '');
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

    </script>
</asp:Content>


<asp:Content ID="contPrinc2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="height: 40px"></div>
    <div class="container">
        <div class="container-fluid">
            <div class="panel-body bordes_curvos" style="background-color: #f8f8f8">

                <div class="divCard tcentrado">
                    <h1 id="h1_titulo" runat="server" class="panel-title">&nbsp;&nbsp;Análisis Estructura</h1>
                </div>
                <dx:ASPxCallbackPanel ID="cbpCatAnaEsts" runat="server" ClientInstanceName="cbpCatAnaEsts" OnCallback="cbpCatAnaEsts_Callback">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <asp:HiddenField ID="hdnGuardar" runat="server" ClientIDMode="Static" Value="0" />
                            <asp:HiddenField ID="hdnAnaEstKey" runat="server" ClientIDMode="Static" />

                            <div class="row" style="padding-left: 10px; padding-top: 10px">

                                <div class="col-sm-2" style="padding-top: 10px">
                                    <dx:ASPxDateEdit ID="DESDE" runat="server" ClientInstanceName="DESDE" EditFormat="Custom" Date=""
                                        CssClass="control-text" NullText="Desde" DisplayFormatString="dd/MM/yyyy">
                                        <CalendarProperties EnableMonthNavigation="True" EnableYearNavigation="True" ShowClearButton="False" ShowDayHeaders="True" ShowTodayButton="False" ShowWeekNumbers="False">
                                            <Style Font-Size="12px"></Style>
                                        </CalendarProperties>
                                    </dx:ASPxDateEdit>
                                </div>
                                <div class="col-sm-2" style="padding-top: 10px">
                                    <dx:ASPxDateEdit ID="HASTA" runat="server" ClientInstanceName="HASTA" EditFormat="Custom" Date=""
                                        CssClass="control-text" NullText="Hasta" DisplayFormatString="dd/MM/yyyy">
                                        <CalendarProperties EnableMonthNavigation="True" EnableYearNavigation="True" ShowClearButton="False" ShowDayHeaders="True" ShowTodayButton="False" ShowWeekNumbers="False">
                                            <Style Font-Size="12px"></Style>
                                        </CalendarProperties>
                                    </dx:ASPxDateEdit>
                                </div>
                                <div class="col-sm-3">
                                    <asp:LinkButton ID="lkb_Buscar" runat="server" CssClass="btn btn-info btn-sm  btn-height" OnClientClick=" Valida()">
                                        <span class="fa fa-search"></span>&nbsp;Buscar
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lkb_Excel" runat="server" CssClass="btn btn-info btn-sm btn-width btn-height" OnClick="lkb_Excel_Click">
                                        <span class="fa fa-expand"></span>&nbsp;Excel
                                    </asp:LinkButton>
                                </div>

                                <div class="col-sm-3" style="padding-top: 10px">
                                    <dx:ASPxDropDownEdit ClientInstanceName="checkComboBox" NullText="Tratados" ID="dde_filtros" ClientIDMode="Static" Width="100%" runat="server" AnimationType="None">
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

                                <div class="col-sm-2">
                                    <asp:LinkButton ID="lkb_Analisis" runat="server" CssClass="btn btn-info btn-sm  btn-height" OnClick="lkb_Analisis_Click">
                                        <span class="fa fa-check-square"></span>&nbsp;Analizar
                                    </asp:LinkButton>
                                </div>

                            </div>
                            <br />

                            <dx:ASPxGridView ID="grvAnaEsts" ClientInstanceName="gridT" runat="server" KeyFieldName="UID_CARGA"
                                Width="100%" AutoGenerateColumns="False" Settings-HorizontalScrollBarMode="Auto" Theme="DevEx" Styles-Cell-CssClass="grid_content"
                                EnableCallBacks="True" Styles-Header-ForeColor="#751473" Styles-Header-Font-Size="11px">
                                <SettingsBehavior AllowEllipsisInText="true" AllowSort="false" />
                                <Columns>

                                    <dx:GridViewDataTextColumn Caption="ID" FieldName="UID_CARGA" Visible="false" />
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="true" Caption="Analizar" Width="10%">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn Caption="Clave producto" Width="30%" FieldName="CVE_PRODUCTO">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataDateColumn Caption="Fecha inicio" Width="15%" FieldName="E_INICIO">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        <CellStyle HorizontalAlign="Center"></CellStyle>
                                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" />
                                    </dx:GridViewDataDateColumn>
                                    <dx:GridViewDataDateColumn Caption="Fecha final" Width="15%" FieldName="E_FIN">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                        <CellStyle HorizontalAlign="Center"></CellStyle>
                                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" />
                                    </dx:GridViewDataDateColumn>
                                    <dx:GridViewDataTextColumn Caption="Nombre archivo" Width="30%" FieldName="NOMBRE_ARCHIVO">
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
                                <SettingsBehavior AllowSelectByRowClick="true" AllowSelectSingleRowOnly="false" AllowEllipsisInText="true" />
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
                                <ClientSideEvents Init="OnInitGridAnaEsts" />
                                <GroupSummary>
                                    <dx:ASPxSummaryItem SummaryType="Count" />
                                </GroupSummary>
                            </dx:ASPxGridView>


                            <dx:ASPxGridViewExporter ID="Exporter" GridViewID="grvAnaEsts" runat="server" PaperKind="A5" Landscape="true" />
                            <%--</div>--%>

                            <button id="btnNuevo" type="button" data-toggle="modal" data-target="#modalAnaEst" style="display: none;"></button>
                            <div class="modal fade" id="modalAnaEst" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                                <div class="modal-dialog modal-sm" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h4 class="modal-title" runat="server" id="titNewAnaEst">Nuevo AnaEst</h4>
                                        </div>
                                        <div class="modal-body">
                                            <div class="form-group row">

                                                <div class="col-sm-12">
                                                    <asp:TextBox ID="TXT_CKey" runat="server" Width="100%" Text="0" CssClass="control-text" Visible="false"></asp:TextBox>
                                                    <div runat="server" id="divProv">
                                                        <label id="LBL_Clave" runat="server" class="form-text">Clave *</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_Clave" runat="server" Width="100%" CssClass="control-text" MaxLength="15" ClientInstanceName="txtClave"></dx:ASPxTextBox>
                                                            </div>
                                                            <i runat="server" id="ITXT_Clave"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12">
                                                    <div runat="server" id="DivNombreAcc">
                                                        <label id="LBL_NOMBRE" runat="server" class="form-text">Descripción *</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxMemo ID="MEMO_DESC" runat="server" Width="100%" Height="70px" CssClass="control-text" ClientInstanceName="memoDESC"></dx:ASPxMemo>
                                                            </div>
                                                            <i runat="server" id="ITXT_MEMODESC"></i>
                                                        </div>
                                                    </div>
                                                </div>


                                            </div>
                                            <div class="modal-footer">
                                                <dx:ASPxLabel ID="lblRepetido" runat="server" Text="El registro a guardar ya existe." Visible="false" ForeColor="Red" Font-Size="12px"></dx:ASPxLabel>
                                                <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-primary btn-sm" Text="Guardar" OnClientClick="Valida()" CausesValidation="true">
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
                                            <asp:LinkButton ID="btnAceptarDel" runat="server" CssClass="btn btn-primary btn-sm txt-sm" data-dismiss="modal" OnClientClick="EventosAnaEsts('borrarAnaEst')">
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
    <div style="height: 100px"></div>
</asp:Content>
