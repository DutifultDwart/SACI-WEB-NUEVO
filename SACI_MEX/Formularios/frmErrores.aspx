<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Formularios/Principal.Master" CodeBehind="frmErrores.aspx.cs" Inherits="SACI_MEX.Formularios.WebServices.frmErrores" EnableEventValidation="true" ValidateRequest="false" EnableViewStateMac="false" %>

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

    </script>
</asp:Content>




<asp:Content ID="contPrinc2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h4 id="h1_titulo" runat="server" class="panel-title_h4_error">Error</h4>
        <hr style="border-color: #7AA9EC; border: double" />
        <br />

        <table style="width: 100%">
            <tr>
       
                <td style="width: 1%">
                    <img src="../img/warningRed.png" class="img_error" />
                </td>
                <td>
                    <h4 id="H4" runat="server" class="panel-title_h4_error_2">¡Atención!  </h4>
                </td>
            </tr>
            <tr>
                <td style="width: 1%"></td>
                <td>
                    <h4 id="H1" runat="server">Se ha producido un error al mostrar la página. Para continuar, de clic en el botón Aceptar</h4>
                </td>
            </tr>
            <tr>
                <td style="width: 1%"></td>
                <td>
                    <div class="row">
                        <div class="col-md-12 col-lg-12">
                            <textarea runat="server" class="form-control" id="txtArea" rows="3"  style="resize: none; height:300px"></textarea>
                        </div>
                    </div>

                </td>
                <td style="width: 1%"></td>
            </tr>
            <tr>
                <td></td>
                <td align="center">
                    <asp:LinkButton ID="lkb_Limpiar_filtros" runat="server" CssClass="btn btn-info btn-sm btn-height" PostBackUrl="~/default.aspx">
                    <span class="fa fa-check"></span>&nbsp;Aceptar
                    </asp:LinkButton>
                </td>
            </tr>
        </table>


    </div>
</asp:Content>
