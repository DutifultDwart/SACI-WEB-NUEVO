<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Formularios/Principal.Master" CodeBehind="Login.aspx.cs" Inherits="SACI_MEX.Login" EnableEventValidation="false" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v17.1, Version=17.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<asp:Content ID="contPrinc1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function ShowInfo() {
            var x = document.getElementById("myDIV");
            if (x.style.display === "none") {
                x.style.display = "block";
            } else {
                x.style.display = "none";
            }
        }


        //$(document).ready(function () {
        //    $("#modalRegisterForm").modal()

        //})

        function GetBase(list) {
            var abc = list.text;
            document.getElementById('dropdownMenu1').textContent = abc;
            var idbd = list.id;
            document.getElementById("<%= idBase.ClientID %>").value = idbd;
        }

        function CerrarRegistro() {
            document.getElementById("<%= idBase.ClientID %>").value = "0";
        }


        function restrictQuotes(evt) {
            var keyCode = evt.which ? evt.which : evt.keyCode;
            return (keyCode != '"'.charCodeAt() && keyCode != "'".charCodeAt() && keyCode != "=".charCodeAt());
        }


        function Submits() {
            //debugger;
            var txtU = document.getElementById('<%=t_u.ClientID %>');
            var txtp = document.getElementById('<%=t_p.ClientID %>');

            if (txtU == '') {
                return false;
            }
            else if (txtp == '') {
                return false;
            }
            else {
                var k = CryptoJS.enc.Utf8.parse('10100100010100100010100100010100'); //
                var iv = CryptoJS.enc.Utf8.parse('1010010001010010');

                var encU = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(txtU.value.trim()), k,
                {
                    keySize: 256 / 8,
                    iv: iv,
                    mode: CryptoJS.mode.CBC,
                    padding: CryptoJS.pad.Pkcs7
                });

                document.getElementById('<%=HDu.ClientID%>').value = encU;

                var encP = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(txtp.value.trim()), k,
                {
                    keySize: 256 / 8,
                    iv: iv,
                    mode: CryptoJS.mode.CBC,
                    padding: CryptoJS.pad.Pkcs7
                });

                document.getElementById('<%=HDp.ClientID%>').value = encP;
                document.getElementById('<%=t_u.ClientID %>').value = '';
                document.getElementById('<%=t_p.ClientID %>').value = '';
            }
        }

        function ValidaPwd() {

            var strength = GetPasswordStrength(document.getElementById('<%=TXT_PWD_B.ClientID %>').value);
            if (strength == 1) {
                valida = 1;
            }
            else {
                valida = 0;
            }

            if (valida == 0) {
                alert('La contraseña no cumple con las características requeridas.\n10 caracteres mínimo\nDebe contener:\n1 mayúscula \n1 número \n1 caracter especial: !@#$%/&*?_¿-()¡[]+=\,<>:;')
            }
            else
                document.getElementById('<%=BtnValidaPwd.ClientID %>').click();
        }

        var minPwdLength = 10;
        var strongPwdLength = 10;

        function GetPasswordStrength(password) {
            if (password.length == 0) return -1;
            if (password.length < minPwdLength) return 0;

            var rate = 0;
            if (password.length >= strongPwdLength) rate++;
            if (password.match(/[0-9]/)) rate++;
            //if (password.match(/[a-z]/)) rate++;
            if (password.match(/[A-Z]/)) rate++;
            //if (password.match(/[!,@,#,$,%,^,&,*,?,_,~,\-,(,),\s,\[,\],+,=,\,,<,>,:,;]/)) rate++;
            if (password.match(/[!,@,#,$,%,/,&,*,?,_,¿,\-,(,),¡,\[,\],+,=,\,,<,>,:,;]/)) rate++;
            //return rate / 5;
            return rate / 4;
            //return rate / 3;
        }

    </script>

    <style>  
        
        .textoHeigth {

            padding: .35rem .5rem;
            line-height: 1.5;
            border-radius: .2rem;
            height: 35px;
            text-align: center;
            font-size: 14px;
            font-family: 'Arial', sans-serif;
            font-weight: bolder;

        }  
    </style>

</asp:Content>


<asp:Content ID="contPrinc2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div id="MyDivI" runat="server"></div>
    <button id="btnView1" type="button" data-toggle="modal" data-target="#modalRegisterForm" style="display: none;"></button>
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <div class="container-fluid">
                <%--<asp:Image ImageUrl="~/img/LogoSAC.jpg" Width="100%" Height="100%" runat="server" />--%>
            </div>

            <asp:HiddenField ID="idBase" runat="server" ClientIDMode="Static" Value="0" />
            <asp:HiddenField ID="HDu" runat="server" />
            <asp:HiddenField ID="HDp" runat="server" />

            <div class="modal fade" id="modalRegisterForm" tabindex="1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog modal-notify" role="document">
                   
                    <hr />
                    <div class="login-card modal-content" style="width: 80%; position:static">
                        
                            <dx:ASPxLabel ID="lblTitLogn" runat="server" ForeColor="#17a2b8" Font-Size="39px" Text="Sistema de Origen" />
                            <%--<h1>Sistema de Origen</h1>--%>
                        
                        <br />

                        <%--
                        <input type="text" id="txtUsuario" runat="server" class="form-control validate" placeholder="Usuario" />
                        <label data-error="wrong" data-success="right" for="form3"></label>

                        <input type="password" id="txtContraseña" runat="server" name="pass" placeholder="Password" />
                        --%>


                        <asp:TextBox ID="t_u" runat="server" type="text" class="form-control" placeholder="Usuario" MaxLength="30" BackColor="#FFFFFF"
                            AutoCompleteType="Disabled" EnableViewState="false" onkeypress="return restrictQuotes(event);">
                        </asp:TextBox>
                        <asp:TextBox ID="t_p" runat="server" type="password" class="form-control" TextMode="Password" BackColor="#FFFFFF"
                             placeholder="Contraseña" MaxLength="50" EnableViewState="false" onkeypress="return restrictQuotes(event);">
                        </asp:TextBox>
                        

                        <div class="dropdown dropup" style="position:static;">
                            <button class="btn form-control validate dropdown-toggle px-3" style="width: 96%" type="button" id="dropdownMenu1" data-toggle="dropdown"
                                aria-haspopup="true" aria-expanded="false">
                                BASE DE DATOS</button>
                            <div class="dropdown-menu dropdown-primary" style="width:61%;">
                                
                                <a class="dropdown-item" id="Bd1" href="#" onclick="GetBase(this)">DEMO_SISTEMA_ORIGEN</a>

                                <%--<a class="dropdown-item" id="Bd1" href="#" onclick="GetBase(this)">ADIENT MEXICO AUTOMOTRIZ</a>--%>


                                <%--<a class="dropdown-item" id="Bd1" href="#" onclick="GetBase(this)">Antolin</a>--%>


                                <%--
                                <a class="dropdown-item" id="Bd1" href="#" onclick="GetBase(this)">MOLEX_IMMEX</a>
                                <a class="dropdown-item" id="Bd3" href="#" onclick="GetBase(this)">OMNI_MANUFACTURING_IMMEX</a>
				                <a class="dropdown-item" id="Bd4" href="#" onclick="GetBase(this)">MOLEX_NOGALES_IMMEX</a>
                                --%>

                                <%--<a class="dropdown-item" id="Bd1" href="#" onclick="GetBase(this)">Adient industries</a>--%>


                                <%--<a class="dropdown-item" id="Bd1" href="#" onclick="GetBase(this)">MAGNA_IMMEX_CELAYA</a>--%>


                                <%--<a class="dropdown-item" id="Bd1" href="#" onclick="GetBase(this)">CWBEARING</a>--%>

                               
                                <%--<a class="dropdown-item" id="Bd1" href="#" onclick="GetBase(this)">MSF</a>
                                <a class="dropdown-item" id="Bd2" href="#" onclick="GetBase(this)">MAM</a>
                                <a class="dropdown-item" id="Bd3" href="#" onclick="GetBase(this)">MCM</a>
				                <a class="dropdown-item" id="Bd4" href="#" onclick="GetBase(this)">MDM</a>
				                <a class="dropdown-item" id="Bd5" href="#" onclick="GetBase(this)">MBRB</a>
				                <a class="dropdown-item" id="Bd6" href="#" onclick="GetBase(this)">MCO</a>
				                <a class="dropdown-item" id="Bd7" href="#" onclick="GetBase(this)">MBMX</a>
				                <a class="dropdown-item" id="Bd8" href="#" onclick="GetBase(this)">MBS</a>
                                <a class="dropdown-item" id="Bd9" href="#" onclick="GetBase(this)">MBQRO</a>
                                --%>
                                
                                                            
                                
                                <%--
                                <a class="dropdown-item" id="Bd1" href="#" onclick="GetBase(this)">MAGNA_IMMEX_HERMOSILLO</a>
                                <a class="dropdown-item" id="Bd2" href="#" onclick="GetBase(this)">MAGNA_IMMEX_QUERETARO</a>
                                <a class="dropdown-item" id="Bd4" href="#" onclick="GetBase(this)">NATIONALMATERIAL_IMMEX_2</a>
                                <a class="dropdown-item" id="Bd5" href="#" onclick="GetBase(this)">PRASAD</a>      
                                --%>

                            </div>
                        </div>

                        <br />

                        <asp:LinkButton ID="lkb_Nuevosd" runat="server" OnClientClick="Submits()" CssClass="btn btn-info bordes_curvos textoHeigth" 
                               OnClick="btnAceptar_ServerClick" Text="ENTRAR" Width="100%" >                           
                        </asp:LinkButton>

                        <%--<dx:BootstrapButton ID="btnInicio" runat="server" Text="Entrar" OnClick="btnAceptar_ServerClick" Width="100%"
                            CssClasses-Control="azulRey" AutoPostBack="false">
                            <ClientSideEvents GotFocus="function(s, e) {Submits(); }" />
                        </dx:BootstrapButton>--%>

                        <dx:ASPxLabel ID="lbl_Intentos" runat="server" Font-Size="12px" ForeColor="Red" Text="" Font-Bold="true" Visible="false" />
                        <div>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 50%">
                                        <div class="text-left" style="padding-top: 15px; font-size: 10px;">
                                            <%--<asp:LinkButton ID="lkb_Registrar" runat="server" ForeColor="Navy" Font-Size="Small" OnClick="lkb_Registrar_Click">
                                                    Registrar aplicación
                                            </asp:LinkButton>--%>
                                        </div>
                                    </td>
                                    <td style="width: 50%">
                                        <div class="text-right" style="padding-top: 15px; font-size: 10px;">
                                            <asp:Label ID="lbl_version" runat="server"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>

        </asp:View>
        <asp:View ID="View2" runat="server">
            <div>
                <div class="row ">
                    <h4 class="modal-title" id="H1" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Registrar aplicación</h4>
                </div>
                <hr />
                <div class="row">
                    <div class="col-sm-9 col-lg-9">
                        <div class="container" style="width: 100%">
                            <div class="row">
                                <div class="col-md-4 col-lg-4">
                                    <div id="DivRFC1">
                                        <label id="Label1" runat="server" class="form-text">RFC *</label>
                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                            <div class="input-group">
                                                <dx:ASPxTextBox ID="txtRFC" ClientInstanceName="txtRFC" runat="server" Width="100%" MaxLength="30" CssClass="control-text"></dx:ASPxTextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtRFC" ValidationGroup="RequiredInfoRegistro" ErrorMessage="*Falta Información" ForeColor="red" runat="Server">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-8 col-lg-8">
                                    <div runat="server" id="Div1">
                                        <label id="Label2" runat="server" class="form-text">Razón social *</label>
                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                            <div class="input-group">
                                                <dx:ASPxTextBox ID="txtRazonSocial" ClientInstanceName="txtRazonSocial" runat="server" Width="100%" CssClass="control-text" MaxLength="50"></dx:ASPxTextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtRazonSocial" ValidationGroup="RequiredInfoRegistro" ErrorMessage="*Falta Información" ForeColor="red" runat="Server">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-8 col-lg-8">
                                    <div runat="server" id="Div2">
                                        <label id="Label3" runat="server" class="form-text">Registro *</label>
                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                            <div class="input-group">
                                                <dx:ASPxTextBox ID="txtRegistro" ClientInstanceName="txtRegistro" runat="server" Width="100%" CssClass="control-text" MaxLength="50"></dx:ASPxTextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtRegistro" ValidationGroup="RequiredInfoRegistro" ErrorMessage="*Falta Información" ForeColor="red" runat="Server">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-8 col-lg-8">
                                    <div runat="server" id="Div3">
                                        <label id="Label4" runat="server" class="form-text">Cadena numérica </label>
                                        <div class="form-group" style="position: relative; width: 100%; float: left;">
                                            <div class="input-group">
                                                <dx:ASPxTextBox ID="txtCadenaNumerica" ClientInstanceName="txtCadenaNumerica" runat="server" Width="100%" CssClass="control-text" MaxLength="500"></dx:ASPxTextBox>
                                            </div>
                                        </div>
                                    </div>
                                    
                                </div>
                                <div class="col-md-4 col-lg-4">
                                    <br />
                                <asp:LinkButton ID="lnk_ConvertirSimbolos" runat="server" CssClass="btn btn-info btn-sm" OnClick="lnk_ConvertirSimbolos_Click" Width="180px">
                                            <span class="fa fa-save"></span>&nbsp;Convertir a simbolos
                    </asp:LinkButton>
                                    </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6 col-md-offset-0" style="text-align: right;">
                    <asp:LinkButton ID="lkb_Guardar" runat="server" CssClass="btn btn-info btn-sm" OnClick="lkb_Guardar_Click" ValidationGroup="RequiredInfoRegistro" CausesValidation="true">
                                            <span class="fa fa-edit"></span>&nbsp;Guardar
                    </asp:LinkButton>
                    <asp:LinkButton ID="lkb_Cancelar" runat="server" CssClass="btn btn-info btn-sm" OnClick="lkb_Cancelar_Click" Width="150px">
                                            <span class="fa fa-home"></span>&nbsp;Regresar a inicio
                    </asp:LinkButton>
                </div>
                <label id="lblError" runat="server" visible="false"></label>
            </div>
        </asp:View>
        <asp:View ID="View3" runat="server">

            <div class="container-fluid">
                <asp:Image ImageUrl="~/img/LogoSAC.jpg" Width="100%" Height="100%" runat="server" />
            </div>

            <button id="btnSuccessV" type="button" data-toggle="modal" data-target="#AlertSuccessV" style="display: none;"></button>
            <div class="modal fade" id="AlertSuccessV" tabindex="1" role="dialog" style="top: 15%;" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">
                <div class="modal-dialog modal-sm" role="document">
                    <div class="login-card modal-content alert alert-success" style="width: 100%; position:static">
                        
                        <span class="fa fa-thumbs-up" style="text-align:center; font-size:24px"></span>
                        <br />
                        <p id="pModalSuccesV" runat="server" class="alert-title" style="text-align:center; font-size:14px">
                        </p>
                        <p id="pModalSuccesV2" runat="server" class="alert-title" style="text-align:center;  font-weight:bold; font-size:14px">
                        </p>
                        <hr />
                        <table style="width: 100%;">
                                <tr>
                                    <td style="text-align:center; width: 100%">
                                        <dx:ASPxButton ID="btnIrVerificar" runat="server" CssClass="btn btn-info btn-sm btn-height" Text="Verificar" OnClick="btnAbrirModalVerificar_Click">
                                        
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                        </table>                        
                        
                    </div>
                </div>
            </div>

            <button id="btnModalVerificaSMS" type="button" data-toggle="modal" data-target="#ModalVerificaSMS" data-whatever="Nuevo" onclick="return false;" style="display: none;"></button>
            <div class="modal fade" id="ModalVerificaSMS" tabindex="1" role="dialog" style="top: 15%;" aria-labelledby="myModalLabel" data-backdrop="static">
                <div class="modal-dialog modal-sm" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 id="H2" class="modal-title" runat="server"></h4>
                        </div>
                        <asp:Panel ID="Panel1" runat="server">
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <dx:ASPxLabel ID="lblTit_Verificar1" runat="server" Font-Size="12px" />
                                    </div>
                                    <div class="col-md-12">
                                        <dx:ASPxLabel ID="lblTit_Verificar2" runat="server" Font-Size="12px" />
                                    </div>
                                    <div class="col-md-12">
                                        <dx:ASPxLabel ID="lblTit_Verificar3" runat="server" Font-Size="12px" />
                                    </div>
                                    <div class="col-md-12">
                                        <dx:ASPxLabel ID="lblTit_Verificar4" runat="server" Font-Size="12px" />
                                    </div>
                                    <div class="col-md-12">&nbsp;</div>
                                    <div class="col-md-12">
                                        <dx:ASPxTextBox ID="TxT_CodigoVerificador" ClientInstanceName="TxT_CodigoVerificador" runat="server" NullText="código verificador" Width="70%" CssClass="control-text" MaxLength="10"></dx:ASPxTextBox>
                                    </div>
                                    <div class="col-md-12">&nbsp;</div>
                                    <div class="col-md-12">
                                        <table style="width:100%;">
                                            <tr>
                                                <td style="width:50%; text-align:center">
                                                    <dx:ASPxButton ID="ASPxButton1" runat="server" CssClass="btn btn-info btn-sm btn-height" Text="Verificar" OnClick="btnVerificar_Click">
                                                    <%--<ClientSideEvents Click="function(s, e) { Callback.PerformCallback(); LoadingPanel1.Show(); }"
                                                       Init="function(s, e) {LoadingPanel1.Hide(); }" />--%>
                                                    </dx:ASPxButton>
                                                </td>
                                                <td style="width:50%; text-align:center">
                                                    <dx:ASPxButton ID="ASPxButton2" runat="server" CssClass="btn btn-info btn-sm btn-height" Text="Cancelar" OnClick="btRegresar_Click">
                                                        <%--<ClientSideEvents Click="function(s, e) { Callback.PerformCallback(); LoadingPanel1.Show(); }"
                                                            Init="function(s, e) {LoadingPanel1.Hide(); }" />--%>
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>                                        
                                    </div>                                    
                                </div>
                            </div>                            
                        </asp:Panel>
                    </div>
                </div>
            </div>


            <button id="btnSuccesM" type="button" data-toggle="modal" data-target="#AlertSuccesM" style="display: none;"></button>
            <div class="modal fade" id="AlertSuccesM" tabindex="1" role="dialog" style="top: 15%;" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static">
                <div class="modal-dialog modal-sm" role="document">
                    <div class="login-card modal-content alert alert-success" style="width: 100%; position:static">
                        
                        <span class="fa fa-thumbs-up" style="text-align:center; font-size:24px"></span>
                        <br />
                        <p id="pModalSuccesM" runat="server" class="alert-title" style="text-align:center; font-size:14px">
                        </p>
                        <p id="pModalSuccesM2" runat="server" class="alert-title" style="text-align:center; font-size:14px; font-weight:bold">
                        </p>
                        <hr />
                        <table style="width: 100%;">
                                <tr>
                                    <td style="text-align:center; width: 100%">
                                        <dx:ASPxButton ID="ASPxButton3" runat="server" CssClass="btn btn-info btn-sm btn-height" Text="Verificar" OnClick="btnAbrirModalVerificarM_Click">
                                        
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                        </table>                        
                        
                    </div>
                </div>
            </div>

            <button id="btnModalVerificaCorreo" type="button" data-toggle="modal" data-target="#ModalVerificaCorreo" data-whatever="Nuevo" onclick="return false;" style="display: none;"></button>
            <div class="modal fade" id="ModalVerificaCorreo" tabindex="1" role="dialog" style="top: 15%;" aria-labelledby="myModalLabel" data-backdrop="static">
                <div class="modal-dialog modal-sm" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 id="ModalTit" class="modal-title" runat="server"></h4>
                        </div>
                        <asp:Panel ID="Panel3" runat="server">
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <dx:ASPxLabel ID="lblTit_Verificar1M" runat="server" Font-Size="12px" />
                                    </div>
                                    <div class="col-md-12">
                                        <dx:ASPxLabel ID="lblTit_Verificar2M" runat="server" Font-Size="12px" />
                                    </div>
                                    <div class="col-md-12">
                                        <dx:ASPxLabel ID="lblTit_Verificar3M" runat="server" Font-Size="12px" />
                                    </div>
                                    <div class="col-md-12">
                                        <dx:ASPxLabel ID="lblTit_Verificar4M" runat="server" Font-Size="12px" />
                                    </div>
                                    <div class="col-md-12">&nbsp;</div>
                                    <div class="col-md-12">
                                        <dx:ASPxTextBox ID="TxT_CodigoVerificadorM" ClientInstanceName="TxT_CodigoVerificadorM" runat="server" NullText="código verificador" Width="70%" CssClass="control-text" MaxLength="10"></dx:ASPxTextBox>
                                    </div>
                                    <div class="col-md-12">&nbsp;</div>
                                    <div class="col-md-12">
                                        <table style="width:100%;">
                                            <tr>
                                                <td style="width:50%; text-align:center">
                                                    <dx:ASPxButton ID="btVerificar" runat="server" CssClass="btn btn-info btn-sm btn-height" Text="Verificar" OnClick="btnVerificarM_Click">
                                                    <%--<ClientSideEvents Click="function(s, e) { Callback.PerformCallback(); LoadingPanel1.Show(); }"
                                                       Init="function(s, e) {LoadingPanel1.Hide(); }" />--%>
                                                    </dx:ASPxButton>
                                                </td>
                                                <td style="width:50%; text-align:center">
                                                    <dx:ASPxButton ID="btRegresar" runat="server" CssClass="btn btn-info btn-sm btn-height" Text="Cancelar" OnClick="btRegresarM_Click">
                                                        <%--<ClientSideEvents Click="function(s, e) { Callback.PerformCallback(); LoadingPanel1.Show(); }"
                                                            Init="function(s, e) {LoadingPanel1.Hide(); }" />--%>
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>                                        
                                    </div>                                    
                                </div>
                            </div>                            
                        </asp:Panel>
                    </div>
                </div>
            </div>



        </asp:View>
    </asp:MultiView>

    <button id="btnModalVigencia" type="button" data-toggle="modal" data-target="#ModalVigencia" data-whatever="Nuevo" onclick="return false;" style="display: none;"></button>
    <div class="modal fade" id="ModalVigencia" tabindex="-1" role="dialog" aria-labelledby="ModalBDTitulo" data-backdrop="static">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <%--<div class="modal-header">
                    <h4 id="ModalVigenciaTitulo" class="modal-title" runat="server"></h4>
                </div>--%>
                <asp:Panel ID="Panel2" runat="server">
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div runat="server" id="Div6">
                                    <dx:ASPxLabel ID="lblTitPwd" runat="server" Text="La vigencia de la contraseña terminó, cree una nueva contraseña" Font-Size="13px" />
                                </div>
                                <div runat="server" id="Div7">&nbsp;</div>
                                <div runat="server" id="Div9">
                                    <dx:ASPxLabel ID="LBL_TIT_PWD_A" runat="server" Text="* Escriba su contraseña actual:" Font-Size="13px" />
                                </div>
                                <div id="div8" runat="server" class="form-group input-group textWidth" style="position: relative; float: left;" title="" data-toggle="tooltip">
                                    
                                    <asp:TextBox ID="TXT_PWD_A" runat="server" type="password" class="form-control" TextMode="Password" Font-Size="13px" BackColor="#F8F8F8" placeholder="Contraseña Actual" MaxLength="50" onkeypress="return restrictQuotes(event);"></asp:TextBox>
                                </div>

                                <div runat="server" id="Div10">&nbsp;</div>
                                
                                <div runat="server" id="Div11">
                                    <dx:ASPxLabel ID="LBL_TIT_PWD_B" runat="server" Text="* Escriba su nueva contraseña:" Font-Size="13px" />
                                </div>
                                <div id="div12" runat="server" class="form-group input-group textWidth" style="position: relative; float: left;" title="" data-toggle="tooltip">
                                    
                                    <asp:TextBox ID="TXT_PWD_B" runat="server" type="password" class="form-control" TextMode="Password" Font-Size="13px" BackColor="#F8F8F8" placeholder="Nueva Contraseña" MaxLength="50" data-toggle="popover" data-trigger="focus" data-container="body" data-html="true" data-content="8 caracteres mínimo <br> Debe contener: <br> 1 mayúscula <br> 1 número <br> 1 caracter especial: !@#$%/&*?_¿-()¡[]+=\,<>:; " data-placement="bottom" onkeypress="return restrictQuotes(event);"></asp:TextBox>
                                </div>
                                <i runat="server" id="ITxtContrasena"></i>

                                <div runat="server" id="Div13">&nbsp;</div>
                                
                                <div runat="server" id="Div14">
                                    <dx:ASPxLabel ID="LBL_TIT_PWD_C" runat="server" Text="* Confirme su contraseña:" Font-Size="13px" />
                                </div>
                                <div id="div15" runat="server" class="form-group input-group textWidth" style="position: relative; float: left;" title="" data-toggle="tooltip">
                                    
                                    <asp:TextBox ID="TXT_PWD_C" runat="server" type="password" class="form-control" TextMode="Password" Font-Size="13px" BackColor="#F8F8F8" placeholder="Confirme Contraseña" MaxLength="50" onkeypress="return restrictQuotes(event);"></asp:TextBox>
                                </div>


                                <div class="form-group text-right" style="position: relative; width: 100%; float: left;">
                                    <asp:Label Text="* Campo obligatorio" Font-Italic="true" runat="server" CssClass="asp-label" ForeColor="Red" />
                                </div>
                                <br />
                                <br />
                            </div>
                            <br />

                            <div class="col-md-12" align="center">                                
                                <asp:LinkButton ID="LinkCancelPwd" runat="server" CssClass="btn btn-info btn-sm btn-height" Text="Cancelar" OnClick="LinkCancelPwd_Click" Width="110px">
                                    <span class="fa fa-times"></span>&nbsp;Cancelar
                                </asp:LinkButton>                                
                                &nbsp;
                                <asp:LinkButton ID="lkbContinuar" runat="server" CssClass="btn btn-info btn-sm btn-height" Text="Guardar" OnClientClick="ValidaPwd();return false;" Width="115px">
                                    <span class="fa fa-check"></span>&nbsp;Continuar</asp:LinkButton>
                                <asp:Button ID="BtnValidaPwd" runat="server" Style="display: none" OnClick="BtnValidaPwd_Click" />                                                                                            
                            </div>


                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>

    <button id="btnSucces" type="button" data-toggle="modal" data-target="#modalSucces" style="display: none;"></button>
    <!-- Central Modal Medium Success -->
    <div class="modal fade" id="modalSucces" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-notify modal-success" role="document">
            <!--Content-->
            <div class="modal-content">
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


</asp:Content>
