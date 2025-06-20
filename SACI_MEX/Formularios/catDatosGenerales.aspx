<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Formularios/Principal.Master" CodeBehind="catDatosGenerales.aspx.cs" Inherits="SACI_MEX.Formularios.catDatosGenerales" EnableEventValidation="true" ValidateRequest="false" EnableViewStateMac="false" %>

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


        $(document).ready(function () { //Hacia arriba
            irArriba();
        });

        function irArriba() {
            $('.ir-arriba').click(function () { $('body,html').animate({ scrollTop: '0px' }, 1000); });
            $(window).scroll(function () {
                if ($(this).scrollTop() > 0) { $('.ir-arriba').slideDown(600); } else { $('.ir-arriba').slideUp(600); }
            });
            $('.ir-abajo').click(function () { $('body,html').animate({ scrollTop: '1000px' }, 1000); });
        }



        function Valida(s, e) {
            var errores = false;

            //DATOS GENERALES
            if (R_TXT_DENOMINACION.GetValue() == null) {
                var label = document.getElementById('<%=LBL_DENOMINACION.ClientID%>');
                label.style.color = "red";
                errores = true;
            }
            else {
                var label = document.getElementById('<%=LBL_DENOMINACION.ClientID%>');
                label.style.color = "black";
            }

            if (R_TXT_RFC.GetValue() == null) {
                var label = document.getElementById('<%=LBL_RFC.ClientID%>');
                label.style.color = "red";
                errores = true;
            }
            else {
                var label = document.getElementById('<%=LBL_RFC.ClientID%>');
                label.style.color = "black";
            }



            if (errores) {
                alert('Debe agregar los campos obligatorios.');
            }
            else {
                cbpcatDatosGenerales.PerformCallback('G2uardar');
            }
        }


        function Valida2(s, e) {
            var errores = false;

            //PLANTA   
            if (TXT_NOMBRE.GetValue() == null) {
                var label = document.getElementById('<%=lblNombre.ClientID%>');
                label.style.color = "red";
                errores = true;
            }
            else {
                var label = document.getElementById('<%=lblNombre.ClientID%>');
                label.style.color = "black";
            }
            if (TXT_UBICACION.GetValue() == null) {
                var label = document.getElementById('<%=lblUbicacion.ClientID%>');
                label.style.color = "red";
                errores = true;
            }
            else {
                var label = document.getElementById('<%=lblUbicacion.ClientID%>');
                label.style.color = "black";
            }


            if (errores) {
                alert('Debe agregar los campos obligatorios.');
            }
            else {
                $('#modalPlantas').modal('hide');
                cbpcatDatosGenerales.PerformCallback('Guardar');
            }
        }


        /*Ajustar el tamaño del PageControl1 al tamaño de la pantalla */
        function OnInitPageControl1(s, e) {
            var height = Math.max(0, document.documentElement.clientHeight * 0.70);

            pagePrincipal.SetHeight(height);

        }

    </script>


</asp:Content>


<asp:Content ID="contPrinc2" ContentPlaceHolderID="MainContent" runat="server">

    <div style="height: 40px"></div>
    <div class="container">
        <div class="container-fluid">
            <div class="panel-body bordes_curvos" style="background-color: #f8f8f8">

                <div class="divCard tcentrado">
                    <h1 id="h3" runat="server" class="panel-title">&nbsp;&nbsp;A. Catálogos</h1>
                    <h1 id="h1_titulo" runat="server" class="panel-title">&nbsp;&nbsp;1. Datos Generales del Contribuyente</h1>
                </div>

                <dx:ASPxCallbackPanel ID="cbpcatDatosGenerales" runat="server" ClientInstanceName="cbpcatDatosGenerales" OnCallback="cbpcatDatosGenerales_Callback">
                    <PanelCollection>
                        <dx:PanelContent runat="server">


                            <asp:HiddenField ID="hdnPLANTAKEY" runat="server" Value="0" />
                            <asp:HiddenField ID="hdnGuardar" runat="server" ClientIDMode="Static" Value="0" />
                            <asp:TextBox ID="TXT_DGKey" runat="server" Width="100%" CssClass="form-control input-sm" MaxLength="35" Visible="false"></asp:TextBox>
                            <%--<div class="row">--%>
                            <table style="width: 100%">
                                <tr>
                                    <td style="text-align: right">
                                        <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-info btn-sm  btn-height" Text="Guardar" OnClientClick="Valida()"> 
                                        <span class="glyphicon glyphicon-ok"></span>&nbsp;&nbsp;Guardar</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                            <%--</div>--%>
                            <dx:ASPxPageControl runat="server" ID="ASPxPageControlCG" Height="100%" Width="100%" ContentStyle-Border-BorderWidth="3px" ClientInstanceName="pagePrincipal"
                                TabAlign="Justify" EnableCallBacks="false" EnableHierarchyRecreation="true" EnableTabScrolling="true" Theme="SoftOrange" Font-Size="12px"
                                ContentStyle-VerticalAlign="Top" TabStyle-BackColor="#751473" ActiveTabStyle-BackColor="#751473" AutoPostBack="false" ActiveTabIndex="0">
                                <TabStyle Paddings-PaddingLeft="50px" Paddings-PaddingRight="50px" ForeColor="#751473">
                                    <Paddings PaddingLeft="50px" PaddingRight="50px"></Paddings>
                                </TabStyle>
                                <ActiveTabStyle BackColor="#751473" ForeColor="#751473" />
                                <ContentStyle>
                                    <Paddings PaddingLeft="20px" PaddingRight="20px" PaddingTop="5px" PaddingBottom="20px" />
                                </ContentStyle>
                                <ClientSideEvents Init="OnInitPageControl1" />
                                <TabPages>
                                    <dx:TabPage Text="Datos Generales">

                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl9" runat="server">
                                                <asp:Panel ID="Panel9" runat="server">
                                                    <div class="form-group row" style="height: 360px">
                                                        <div class="container" style="width: 100%">
                                                            <div class="form-group row" style="height: 43px">
                                                                <div class="col-sm-6 col-md-6">
                                                                    <div runat="server" id="DivDenominacion">
                                                                        <asp:HiddenField ID="IDDatos_Gral" runat="server" />
                                                                        <label id="LBL_DENOMINACION" runat="server" class="form-text">Denominación o razón social *</label>
                                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                                            <div class="input-group">

                                                                                <dx:ASPxTextBox ID="R_TXT_DENOMINACION" ClientInstanceName="R_TXT_DENOMINACION" runat="server" Width="100%" MaxLength="100" CssClass="control-text">
                                                                                    <%--<ValidationSettings SetFocusOnError="false" Display="Dynamic" ErrorText="" ErrorDisplayMode="None">
                                                                                    </ValidationSettings>
                                                                                    <InvalidStyle BackColor="LightPink" />--%>
                                                                                </dx:ASPxTextBox>
                                                                                <%--<asp:TextBox ID="R_TXT_DENOMINACION" runat="server" Width="100%" CssClass="form-control input-sm" MaxLength="100">                                                               
                                                                                </asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="Validator_Deno" ControlToValidate="R_TXT_DENOMINACION" Display="Static" ForeColor="red" ErrorMessage="**Falta Información" runat="server">                                
                                                                                </asp:RequiredFieldValidator>--%>
                                                                            </div>
                                                                            <i runat="server" id="ITXT_DENOMINACION"></i>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4 col-md-4">
                                                                    <div runat="server" id="DivRFC">
                                                                        <label id="LBL_RFC" runat="server" class="form-text">RFC o CURP *</label>
                                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                                            <div class="input-group">

                                                                                <dx:ASPxTextBox ID="R_TXT_RFC" ClientInstanceName="R_TXT_RFC" runat="server" Width="100%" MaxLength="30">
                                                                                    <ValidationSettings SetFocusOnError="false" Display="Dynamic" ErrorText="" ErrorDisplayMode="None">
                                                                                    </ValidationSettings>
                                                                                    <InvalidStyle BackColor="LightPink" />
                                                                                </dx:ASPxTextBox>
                                                                                <%--<asp:TextBox ID="R_TXT_RFC" runat="server" Width="150px" CssClass="form-control input-sm" MaxLength="100"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="Validator_RFC" ControlToValidate="R_TXT_RFC" Display="Static" ViewStateMode="Enabled" ForeColor="red" ErrorMessage="**Falta Información" runat="server">                               
                                                                                </asp:RequiredFieldValidator>--%>
                                                                            </div>
                                                                            <i runat="server" id="ITXT_RFC"></i>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6 col-md-6">
                                                                    <div runat="server" id="DivActividad">
                                                                        <label id="LBL_ACTIVIDAD" runat="server" class="form-text">Actividad preponderante</label>
                                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                                            <div class="input-group">
                                                                                <dx:ASPxTextBox ID="TXT_ACTIVIDAD" runat="server" Width="100%" CssClass="control-text" MaxLength="40"></dx:ASPxTextBox>
                                                                            </div>
                                                                            <i runat="server" id="ITXT_ACTIVIDAD"></i>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Dirección">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl1" runat="server">
                                                <asp:Panel ID="Panel1" runat="server">
                                                    <div class="form-group row" style="height: 360px">
                                                        <div class="container" style="width: 100%">
                                                            <div class="form-group row" style="height: 43px">
                                                                <div class="col-sm-4 col-md-4">
                                                                    <div runat="server" id="DivCalleNum">
                                                                        <label id="LBL_CALLE_NUM" runat="server" class="form-text">Calle y número exterior</label>
                                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                                            <div class="input-group">
                                                                                <dx:ASPxTextBox ID="TXT_CALLE_NUM" runat="server" Width="100%" CssClass="control-text" MaxLength="100">
                                                                                </dx:ASPxTextBox>
                                                                            </div>
                                                                            <i runat="server" id="ITXT_CALLE_NUM"></i>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3 col-md-3">
                                                                    <div runat="server" id="Div2">
                                                                        <label id="LBL_CALLE_NUM_INT" runat="server" class="form-text">Número interior</label>
                                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                                            <div class="input-group">
                                                                                <dx:ASPxTextBox ID="TXT_CALLE_NUM_INT" runat="server" Width="100%" CssClass="control-text" MaxLength="50"></dx:ASPxTextBox>
                                                                            </div>
                                                                            <i runat="server" id="ITXT_CALLE_NUM_INT"></i>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4 col-md-4">
                                                                    <div runat="server" id="DivCP">
                                                                        <label id="LBL_CODIGO_POSTAL" runat="server" class="form-text">Código postal</label>
                                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                                            <div class="input-group">
                                                                                <dx:ASPxTextBox ID="TXT_CODIGO_POSTAL" runat="server" Width="100%" CssClass="control-text" MaxLength="10"></dx:ASPxTextBox>
                                                                            </div>
                                                                            <i runat="server" id="ITXT_CODIGO_POSTAL"></i>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row" style="height: 43px">
                                                                <div class="col-sm-4 col-md-4">
                                                                    <div runat="server" id="DivColonia">
                                                                        <label id="LBL_COLONIA" runat="server" class="form-text">Colonia</label>
                                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                                            <div class="input-group">
                                                                                <dx:ASPxTextBox ID="TXT_COLONIA" runat="server" Width="100%" CssClass="control-text" MaxLength="50"></dx:ASPxTextBox>
                                                                            </div>
                                                                            <i runat="server" id="ITXT_COLONIA"></i>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-3 col-md-3">
                                                                    <div runat="server" id="Div1">
                                                                        <label id="LBL_LOCALIDAD" runat="server" class="form-text">Localidad</label>
                                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                                            <div class="input-group">
                                                                                <dx:ASPxTextBox ID="TXT_LOCALIDAD" runat="server" Width="100%" CssClass="control-text" MaxLength="50"></dx:ASPxTextBox>
                                                                            </div>
                                                                            <i runat="server" id="ITXT_LOCALIDAD"></i>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4 col-md-4">
                                                                    <div runat="server" id="DivMunicipio">
                                                                        <label id="LBL_MUNICIPIO" runat="server" class="form-text">Municipio</label>
                                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                                            <div class="input-group">
                                                                                <dx:ASPxTextBox ID="TXT_MUNICIPIO" runat="server" Width="100%" CssClass="control-text" MaxLength="50"></dx:ASPxTextBox>
                                                                            </div>
                                                                            <i runat="server" id="ITXT_MUNICIPIO"></i>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row" style="height: 43px">
                                                                <div class="col-sm-4 col-md-4">
                                                                    <div runat="server" id="DivEntidad">
                                                                        <label id="LBL_ENTIDAD" runat="server" class="form-text">Entidad</label>
                                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                                            <div class="input-group">
                                                                                <dx:ASPxTextBox ID="TXT_ENTIDAD" runat="server" Width="100%" CssClass="control-text" MaxLength="50"></dx:ASPxTextBox>
                                                                            </div>
                                                                            <i runat="server" id="ITXT_ENTIDAD"></i>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3 col-md-3">
                                                                    <div runat="server" id="DivPais">
                                                                        <label id="LBL_PAIS" runat="server" class="form-text">Registro país</label>
                                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                                            <div class="input-group">
                                                                                <dx:ASPxTextBox ID="TXT_PAIS" runat="server" Width="100%" CssClass="control-text" MaxLength="50"></dx:ASPxTextBox>
                                                                            </div>
                                                                            <i runat="server" id="ITXT_PAIS"></i>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4 col-md-4">
                                                                    <div runat="server" id="DivTelefono">
                                                                        <label id="LBL_TELEFONO" runat="server" class="form-text">Teléfono</label>
                                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                                            <div class="input-group">
                                                                                <dx:ASPxTextBox ID="TXT_TELEFONO" runat="server" Width="100%" CssClass="control-text" MaxLength="25"></dx:ASPxTextBox>
                                                                            </div>
                                                                            <i runat="server" id="ITXT_TELEFONO"></i>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row" style="height: 43px">
                                                                <div class="col-sm-4 col-md-4">
                                                                    <div runat="server" id="DivFax">
                                                                        <label id="LBL_FAX" runat="server" class="form-text">Fax</label>
                                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                                            <div class="input-group">
                                                                                <dx:ASPxTextBox ID="TXT_FAX" runat="server" Width="100%" CssClass="control-text" MaxLength="25"></dx:ASPxTextBox>
                                                                            </div>
                                                                            <i runat="server" id="I1"></i>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-7 col-md-7">
                                                                    <div runat="server" id="DivCorreo">
                                                                        <label id="LBL_CORREO" runat="server" class="form-text">Correo electrónico</label>
                                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                                            <div class="input-group">
                                                                                <dx:ASPxTextBox ID="TXT_CORREO" runat="server" Width="100%" CssClass="control-text" MaxLength="32"></dx:ASPxTextBox>
                                                                            </div>
                                                                            <i runat="server" id="ITXT_CORREO"></i>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Número de Registro">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl2" runat="server">
                                                <asp:Panel ID="Panel2" runat="server">
                                                    <div class="form-group row" style="height: 360px;">
                                                        <div class="container" style="width: 100%;">
                                                            <div class="form-group row" style="height: 43px;">

                                                                <div class="col-sm-3 col-md-3">
                                                                    <div runat="server" id="DivRgistroPitex">
                                                                        <label id="LBL_REGISTRO_PITEX" runat="server" class="form-text">Registro PITEX</label>
                                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                                            <div class="input-group">
                                                                                <dx:ASPxTextBox ID="TXT_REGISTRO_PITEX" runat="server" Width="100%" CssClass="control-text" MaxLength="15"></dx:ASPxTextBox>
                                                                            </div>
                                                                            <i runat="server" id="ITXT_REGISTRO_PITEX"></i>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-3 col-md-3">
                                                                    <div runat="server" id="DivRegistroMaqu">
                                                                        <label id="LBL_REGISTRO_MAQUILA" runat="server" class="form-text">Registro maquila</label>
                                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                                            <div class="input-group">
                                                                                <dx:ASPxTextBox ID="TXT_REGISTRO_MAQUILA" runat="server" Width="100%" CssClass="control-text" MaxLength="15"></dx:ASPxTextBox>
                                                                            </div>
                                                                            <i runat="server" id="ITXT_REGISTRO_MAQUILA"></i>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-3 col-md-3">
                                                                    <div runat="server" id="DivRegistroAltex">
                                                                        <label id="LBL_REGISTRO_ALTEX" runat="server" class="form-text">Registro ALTEX</label>
                                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                                            <div class="input-group">
                                                                                <dx:ASPxTextBox ID="TXT_REGISTRO_ALTEX" runat="server" Width="100%" CssClass="control-text" MaxLength="15"></dx:ASPxTextBox>
                                                                            </div>
                                                                            <i runat="server" id="ITXT_REGISTRO_ALTEX"></i>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-3 col-md-3">
                                                                    <div runat="server" id="DivRegistroEcex">
                                                                        <label id="LBL_REGISTRO_ECEX" runat="server" class="form-text">Registro ECEX</label>
                                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                                            <div class="input-group">
                                                                                <dx:ASPxTextBox ID="TXT_REGISTRO_ECEX" runat="server" Width="100%" CssClass="control-text" MaxLength="15"></dx:ASPxTextBox>
                                                                            </div>
                                                                            <i runat="server" id="ITXT_REGISTRO_ECEX"></i>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>

                                                            <div class="form-group row" style="height: 43px;">



                                                                <div class="col-sm-3 col-md-3">
                                                                    <div runat="server" id="DivRegistroRecime">
                                                                        <label id="LBL_REGISTRO_RECIME" runat="server" class="form-text">Registro RECIME</label>
                                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                                            <div class="input-group">
                                                                                <dx:ASPxTextBox ID="TXT_REGISTRO_RECIME" runat="server" Width="100%" CssClass="control-text" MaxLength="15"></dx:ASPxTextBox>
                                                                            </div>
                                                                            <i runat="server" id="ITXT_REGISTRO_RECIME"></i>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-3 col-md-3">
                                                                    <div runat="server" id="DivRegistroProceC">
                                                                        <label id="LBL_REGISTRO_PROSEC" runat="server" class="form-text">Registro PROSEC</label>
                                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                                            <div class="input-group">
                                                                                <dx:ASPxTextBox ID="TXT_REGISTRO_PROSEC" runat="server" Width="100%" CssClass="control-text" MaxLength="15"></dx:ASPxTextBox>
                                                                            </div>
                                                                            <i runat="server" id="ITXT_REGISTRO_PROSEC"></i>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-3 col-md-3">
                                                                    <div runat="server" id="DivRegistroImex">
                                                                        <label id="LBL_REGISTRO_IMMEX" runat="server" class="form-text">Registro IMMEX</label>
                                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                                            <div class="input-group">
                                                                                <dx:ASPxTextBox ID="TXT_REGISTRO_IMMEX" runat="server" Width="100%" CssClass="control-text" MaxLength="15"></dx:ASPxTextBox>
                                                                            </div>
                                                                            <i runat="server" id="ITXT_REGISTRO_IMMEX"></i>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-3 col-md-3">
                                                                    <div runat="server" id="Div3">
                                                                        <label id="LBL_OFICIO_CERTIFICACION" runat="server" class="form-text">Oficio certificación</label>
                                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                                            <div class="input-group">
                                                                                <dx:ASPxTextBox ID="TXT_OFICIO_CERTIFICACION" runat="server" Width="100%" CssClass="control-text" MaxLength="50"></dx:ASPxTextBox>
                                                                            </div>
                                                                            <i runat="server" id="IITXT_OFICIO_CERTIFICACION4"></i>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="form-group row" style="height: 43px;">

                                                                <div class="col-sm-3 col-md-3" style="position: static;">

                                                                    <label id="LBL_FECHA_VIGENCIA" runat="server" class="form-text">Fecha de la certificación IVA</label>
                                                                    <%--<div class="form-group"  width: 100%; float: left;">--%>
                                                                    <div class="input-group">
                                                                        <dx:ASPxDateEdit ID="DATE_FECHA_VIGENCIA" runat="server" Width="100%" CssClass="control-text"></dx:ASPxDateEdit>
                                                                    </div>
                                                                    <%--<i runat="server" id="IDATE_FECHA_VIGENCIA"></i>
                                                                            </div>--%>
                                                                </div>
                                                                <div class="col-sm-3 col-md-3">
                                                                    <label id="lblCvePedimento" runat="server" class="form-text">Tipo certificación por empresa</label>
                                                                    <dx:ASPxComboBox ID="CMB_TIPOCERT_EMPRESA" runat="server" Width="100%" CssClass="control-text" ClientInstanceName="cmbPedimento">
                                                                        <Items>
                                                                            <dx:ListEditItem Value="A" Text="A" />
                                                                            <dx:ListEditItem Value="AA" Text="AA" />
                                                                            <dx:ListEditItem Value="AAA" Text="AAA" />
                                                                        </Items>
                                                                    </dx:ASPxComboBox>
                                                                </div>
                                                                <div class="col-sm-3 col-md-3">
                                                                    <div runat="server" id="Div4">
                                                                        <label id="Label1" runat="server" class="form-text">Certificación oea</label>
                                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                                            <div class="input-group">
                                                                                <dx:ASPxTextBox ID="TXT_CERT_OEA" runat="server" Width="100%" CssClass="control-text" MaxLength="50"></dx:ASPxTextBox>
                                                                            </div>
                                                                            <i runat="server" id="I2"></i>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-1 col-md-1"></div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Plantas">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl3" runat="server">
                                                <asp:Panel ID="Panel3" runat="server">
                                                    <div class="form-group row" style="height: 360px">

                                                        <asp:LinkButton ID="lkb_Nueva_planta" runat="server" CssClass="btn btn-info btn-sm  btn-height" OnClientClick="EventosDatosGenerales('Nuevo')">
                                                            <span class="fa fa-plus"></span>&nbsp;Agregar
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lkb_Editar_planta" runat="server" CssClass="btn btn-info btn-sm btn-height" OnClientClick="EventosDatosGenerales('Editar')">
                                                             <span class="fa fa-edit"></span>&nbsp;Editar
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lkb_Eliminar_planta" runat="server" CssClass="btn btn-info btn-sm btn-width btn-height"
                                                            OnClientClick="document.getElementById('btnQuestion').setAttribute('data-whatever', ''); document.getElementById('pModalQuestion').innerHTML  = '¿Estas seguro de eliminar el registro?';  document.getElementById('btnQuestion').click(); return false">
                                                            <span class="fa fa-times"></span>&nbsp;Borrar
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lkb_Excel" runat="server" CssClass="btn btn-info btn-sm btn-width btn-height" OnClick="lkb_Excel_Click">
                                                            <span class="fa fa-expand"></span>&nbsp;Excel
                                                        </asp:LinkButton>
                                                        <br />

                                                        <dx:ASPxGridView ID="grvPlantas" ClientInstanceName="gridPlantas" runat="server" KeyFieldName="PlantaKey"
                                                            Width="100%" AutoGenerateColumns="False" Settings-HorizontalScrollBarMode="Auto" Theme="DevEx"
                                                            EnableCallBacks="True" Styles-Header-ForeColor="#751473" Styles-Header-Font-Size="11px"
                                                            Styles-Cell-CssClass="grid_content" Settings-VerticalScrollableHeight="250">
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn Caption="ID" FieldName="PlantaKey" Visible="false" />
                                                                <dx:GridViewDataTextColumn Caption="Nombre" Width="350px" FieldName="nombre" VisibleIndex="1">
                                                                    <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Ubicación" Width="250px" FieldName="ubicacion" VisibleIndex="2">
                                                                    <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Folio" Width="250px" FieldName="Folio" VisibleIndex="3">
                                                                    <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Dirección 1" Width="120px" FieldName="Direccion1" VisibleIndex="4">
                                                                    <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Dirección 2" Width="130px" FieldName="Direccion2" VisibleIndex="5">
                                                                    <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Dirección 3" Width="130px" FieldName="Direccion3" VisibleIndex="5">
                                                                    <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" Font-Bold="true" />
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="PlantaKey" Width="130px" FieldName="PlantaKey" VisibleIndex="5" Visible="false">
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
                                                            <SettingsBehavior AllowEllipsisInText="true" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" FilterRowMode="OnClick" />
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

                                                            <GroupSummary>
                                                                <dx:ASPxSummaryItem SummaryType="Count" />
                                                            </GroupSummary>
                                                        </dx:ASPxGridView>

                                                        <dx:ASPxGridViewExporter ID="Exporter" GridViewID="grvPlantas" runat="server" PaperKind="A5" Landscape="true" />

                                                    </div>
                                                </asp:Panel>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Certificaciones">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl4" runat="server">
                                                <dx:ASPxGridView ID="grvCertificaciones" ClientInstanceName="grvCertificaciones" runat="server" KeyFieldName="DG_CERTIFICACION_KEY"
                                                    Width="100%" AutoGenerateColumns="False" Settings-HorizontalScrollBarMode="Auto" Settings-VerticalScrollBarMode="Auto" Theme="DevEx"
                                                    EnableCallBacks="true" SettingsPager-Mode="ShowPager"
                                                    Styles-Header-Font-Size="11px"
                                                    Styles-Cell-Font-Size="11px" Styles-Header-Font-Bold="true" Styles-Header-CssClass="tit_grid"
                                                    SettingsPager-PageSize="10" SettingsPager-Position="Bottom" Styles-Header-ForeColor="#751473"
                                                    OnRowInserting="grvCertificaciones_RowInserting"
                                                    OnRowUpdating="grvCertificaciones_RowUpdating"
                                                    OnRowDeleting="grvCertificaciones_RowDeleting"
                                                    OnCustomErrorText="grvCertificaciones_CustomErrorText">
                                                    <SettingsBehavior ConfirmDelete="True"></SettingsBehavior>
                                                    <SettingsText ConfirmDelete="¿Desea eliminar el registro?" />
                                                    <Columns>

                                                        <dx:GridViewDataTextColumn FieldName="DG_CERTIFICACION_KEY" Visible="false" />
                                                        <dx:GridViewCommandColumn ShowDeleteButton="true" ShowEditButton="true" ShowNewButtonInHeader="true" Width="10%">
                                                            <HeaderStyle HorizontalAlign="Center" CssClass="tit_grid" />
                                                            <CellStyle HorizontalAlign="Center"></CellStyle>
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="Rfc" FieldName="RFC" Width="20%">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <SettingsHeaderFilter Mode="CheckedList" />
                                                            <CellStyle HorizontalAlign="Center"></CellStyle>
                                                            <PropertiesTextEdit MaxLength="20">
                                                                <ValidationSettings Display="Dynamic" RequiredField-IsRequired="true" ErrorText="Campo requerido" />
                                                            </PropertiesTextEdit>
                                                        </dx:GridViewDataTextColumn>

                                                        <dx:GridViewDataTextColumn Caption="Clave" FieldName="CLAVE_CERTIFICACION" Width="20%">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <SettingsHeaderFilter Mode="CheckedList" />
                                                            <CellStyle HorizontalAlign="Center"></CellStyle>
                                                            <PropertiesTextEdit MaxLength="50">
                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorText="Campo requerido" />
                                                            </PropertiesTextEdit>
                                                        </dx:GridViewDataTextColumn>

                                                        <dx:GridViewDataTextColumn Caption="Descripción" FieldName="DESCRIPCION_CERTIFICACION" Width="20%">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <SettingsHeaderFilter Mode="CheckedList" />
                                                            <CellStyle HorizontalAlign="Center"></CellStyle>
                                                            <PropertiesTextEdit MaxLength="200">
                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorText="Campo requerido" />
                                                            </PropertiesTextEdit>
                                                        </dx:GridViewDataTextColumn>

                                                        <dx:GridViewDataDateColumn Caption="Fecha inicio" FieldName="FECHA_INICIO" Width="20%">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <SettingsHeaderFilter Mode="CheckedList" />
                                                            <CellStyle HorizontalAlign="Center"></CellStyle>
                                                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorText="Campo requerido" />
                                                            </PropertiesDateEdit>
                                                        </dx:GridViewDataDateColumn>

                                                        <dx:GridViewDataDateColumn Caption="Fecha fin" FieldName="FECHA_FIN" Width="20%">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <SettingsHeaderFilter Mode="CheckedList" />
                                                            <CellStyle HorizontalAlign="Center"></CellStyle>
                                                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorText="Campo requerido" />
                                                            </PropertiesDateEdit>
                                                        </dx:GridViewDataDateColumn>

                                                    </Columns>


                                                    <Settings ShowFilterRow="true" ShowFilterRowMenu="true" AutoFilterCondition="Contains"
                                                        ShowHeaderFilterButton="true" />
                                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" HideDataCellsAtWindowInnerWidth="800"
                                                        AdaptiveDetailColumnCount="1" AllowOnlyOneAdaptiveDetailExpanded="True">
                                                        <AdaptiveDetailLayoutProperties ColCount="2">
                                                            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                                                        </AdaptiveDetailLayoutProperties>
                                                    </SettingsAdaptivity>

                                                    <SettingsBehavior AllowSelectByRowClick="false" />
                                                    <Settings ShowFooter="False" ShowHeaderFilterButton="true" ShowFilterRowMenu="false" />
                                                    <Styles>
                                                        <SelectedRow CssClass="background_color_btn background_texto_btn" />
                                                        <Row />
                                                        <AlternatingRow Enabled="True" />
                                                        <PagerTopPanel Paddings-PaddingBottom="3px"></PagerTopPanel>
                                                    </Styles>
                                                    <SettingsPopup>
                                                        <HeaderFilter Height="200px" Width="195px" />
                                                    </SettingsPopup>
                                                    <SettingsResizing ColumnResizeMode="Control" />
                                                    <SettingsPager>
                                                        <PageSizeItemSettings Visible="true" Items="20, 50, 100" />
                                                    </SettingsPager>
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
                                                    <SettingsEditing Mode="Inline"></SettingsEditing>

                                                </dx:ASPxGridView>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                </TabPages>
                            </dx:ASPxPageControl>


                            <%--  </div>--%>


                            <button id="btnNuevo" type="button" data-toggle="modal" data-target="#modalPlantas" style="display: none;"></button>
                            <div class="modal fade" id="modalPlantas" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                                <div class="modal-dialog modal-lg" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h4 class="modal-title" runat="server" id="titNewPlanta">Nueva Planta</h4>
                                        </div>
                                        <div class="modal-body">
                                            <div class="row">
                                                <div class="col-sm-6 col-md-6">
                                                    <asp:TextBox ID="TXT_PlantaID" runat="server" Width="100%" Text="0" CssClass="form-control input-sm" Visible="false"></asp:TextBox>
                                                    <div runat="server" id="divProv">
                                                        <label id="lblNombre" runat="server" class="form-text">Nombre *</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_NOMBRE" ClientInstanceName="TXT_NOMBRE" runat="server" Width="100%" MaxLength="35">
                                                                    <%--<ValidationSettings SetFocusOnError="false" Display="Dynamic" ErrorText="" ErrorDisplayMode="None">
                                                            </ValidationSettings>
                                                            <InvalidStyle BackColor="LightPink" />--%>
                                                                </dx:ASPxTextBox>
                                                                <%--<dx:ASPxTextBox ID="TXT_NOMBRE" runat="server" Width="100%" CssClass="control-text" MaxLength="35"></dx:ASPxTextBox>--%>
                                                            </div>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TXT_NOMBRE" ValidationGroup="RequiedInfoGroup" ErrorMessage="***Falta Información" ForeColor="red" runat="Server">
                                                    </asp:RequiredFieldValidator>--%>
                                                            <i runat="server" id="ITXT_NOMBRE"></i>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-6 col-md-6">
                                                    <div runat="server">
                                                        <label id="lblUbicacion" runat="server" class="form-text">Ubicación *</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_UBICACION" ClientInstanceName="TXT_UBICACION" runat="server" Width="100%" MaxLength="100">
                                                                    <%--<ValidationSettings SetFocusOnError="false" Display="Dynamic" ErrorText="" ErrorDisplayMode="None">
                                                            </ValidationSettings>
                                                            <InvalidStyle BackColor="LightPink" />--%>
                                                                </dx:ASPxTextBox>
                                                                <%--<dx:ASPxTextBox ID="TXT_UBICACION" runat="server" Width="100%" CssClass="control-text" MaxLength="100"></dx:ASPxTextBox>--%>
                                                            </div>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="TXT_UBICACION" ValidationGroup="RequiedInfoGroup" ErrorMessage="***Falta Información" ForeColor="red" runat="Server">
                                                    </asp:RequiredFieldValidator>--%>
                                                            <i runat="server" id="ITXT_UBICACION"></i>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-6 col-md-6">
                                                    <div runat="server" id="Div5">
                                                        <label id="lblFolio" runat="server" class="form-text">Folio</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxSpinEdit ID="TXT_FOLIO" runat="server" Width="100%" CssClass="control-text"></dx:ASPxSpinEdit>
                                                            </div>
                                                            <i runat="server" id="ITXT_FOLIO"></i>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-6 col-md-6">
                                                    <div runat="server" id="Div6">
                                                        <label id="lbl_Domicilio1" runat="server" class="form-text">Dirección 1</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_Direccion1" runat="server" Width="100%" CssClass="control-text" MaxLength="100"></dx:ASPxTextBox>
                                                            </div>
                                                            <i runat="server" id="ITXT_Direccion1"></i>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-6 col-md-6">
                                                    <div runat="server" id="Div7">
                                                        <label id="lbl_Direccion2" runat="server" class="form-text">Dirección 2</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_Direccion2" runat="server" Width="100%" CssClass="control-text" MaxLength="100"></dx:ASPxTextBox>
                                                            </div>
                                                            <i runat="server" id="ITXT_Direccion2"></i>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-6 col-md-6">
                                                    <div runat="server" id="Div8">
                                                        <label id="lblDireccion3" runat="server" class="form-text">Dirección 3</label>
                                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                                            <div class="input-group">
                                                                <dx:ASPxTextBox ID="TXT_Direccion3" runat="server" Width="100%" CssClass="control-text" MaxLength="100"></dx:ASPxTextBox>
                                                                <asp:TextBox ID="TXT_PLANTAKEY" runat="server" Width="150px" CssClass="form-control input-sm" MaxLength="100" Visible="false"></asp:TextBox>
                                                            </div>
                                                            <i runat="server" id="iTXT_Direccion3"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
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
                                                <dx:ASPxLabel ID="lblRepetido" runat="server" Text="El registro a guardar ya existe" Visible="false" ForeColor="Red" Font-Size="12px"></dx:ASPxLabel>
                                                <asp:LinkButton ID="lnk_Guardar_planta" runat="server" CssClass="btn btn-primary btn-sm" Text="Guardar" OnClientClick="Valida2()" ValidationGroup="RequiedInfoGroup" CausesValidation="true">
                                            <span class="glyphicon glyphicon-ok"></span>&nbsp;&nbsp;Guardar</asp:LinkButton>
                                                <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal" onclick="LimpiaControles()">Cancelar</button>

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
                            <div class="modal fade bd-example-modal-lg" id="AlertError" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
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
                            <div class="modal fade  modal-warning" id="AlertQuestion" tabindex="-1" role="dialog" style="top: 26%; outline: none;">
                                <div class="modal-dialog  modal-sm " role="document">
                                    <div class="modal-content" style="height: 90px">
                                        <div class="alert alert-warning text-center" style="-webkit-box-shadow: 0 5px 15px rgba(0, 0, 0, .5); box-shadow: 0 5px 15px rgba(0, 0, 0, .5);">

                                            <span class="glyphicon glyphicon-question-sign ico"></span>

                                            <br />

                                            <br />

                                            <p id="pModalQuestion" class="alert-title">
                                            </p>

                                            <hr />
                                            <asp:LinkButton ID="btnAceptarDel" runat="server" CssClass="btn btn-primary btn-sm txt-sm" data-dismiss="modal" OnClientClick="EventosDatosGenerales('Borrar')">
                                        <span class="glyphicon glyphicon-plus"></span>&nbsp;&nbsp;Aceptar
                                            </asp:LinkButton>
                                            <button id="btnCancel" runat="server" class="btn btn-secondary btn-sm" data-dismiss="modal">Cancelar</button>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </dx:PanelContent>
                    </PanelCollection>
                    <ClientSideEvents EndCallback="function(s, e){ AlertasDatosGenerales(); }"></ClientSideEvents>
                </dx:ASPxCallbackPanel>
            </div>
        </div>
    </div>
    <div style="height: 100px"></div>
    <script src="../ScriptsSaci/Catalogos.js"></script>

</asp:Content>
