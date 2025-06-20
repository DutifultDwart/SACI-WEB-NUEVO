
function CambiarIdioma(paramm) {
    var hdnfldVariable = document.getElementById('ContentSection_hdnfldVariable');
    hdnfldVariable.value = paramm;
    __doPostBack('', '');
}

function CambiarPlanta(paramm) {
    var HiddenFPlanta = document.getElementById('ContentSection_HiddenFPlanta');
    HiddenFPlanta.value = $(paramm).data("string");
    __doPostBack('', '');
}