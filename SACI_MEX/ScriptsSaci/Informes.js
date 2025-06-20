function EventosInformeF4CTM(s, e) {
    if (s != 'undefined') {
        cbpDescargosCTMA.PerformCallback(s);
    }
    else {
        cbpDescargosCTMA.PerformCallback(s);
    }
}

function EventosInformeF4Desperdicio(s, e) {
    if (s != 'undefined') {
        cbpDescargosDesperdicios.PerformCallback(s);
    }
    else {
        cbpDescargosDesperdicios.PerformCallback(s);
    }
}

function EventosInformeSaldos(s, e) {
    if (s != 'undefined') {
        cbpInformeSaldos.PerformCallback(s);
    }
    else {
        cbpInformeSaldos.PerformCallback(s);
    }
}

function EventosInformeSaldosFecha(s, e) {
    if (s != 'undefined') {
        cbpInformeSaldosFecha.PerformCallback(s);
    }
    else {
        cbpInformeSaldosFecha.PerformCallback(s);
    }
}

function EventosInformeEstructuras(s, e) {
    if (s != 'undefined') {
        cbpInformeEstructuras.PerformCallback(s);
    }
    else {
        cbpInformeEstructuras.PerformCallback(s);
    }
}

function EventosInformeCompulsa(s, e) {
    if (s != 'undefined') {
        cbpInformeCompulsa.PerformCallback(s);
    }
    else {
        cbpInformeCompulsa.PerformCallback(s);
    }
}

function EventosInformeVencimientos(s, e) {
    if (s != 'undefined') {
        cbpInfoVencimientos.PerformCallback(s);
    }
    else {
        cbpInfoVencimientos.PerformCallback(s);
    }
}

function EventosInfHisDesImpo(s, e) {
    if (s != 'undefined') {
        cbpInformeHDesImpo.PerformCallback(s);
    }
    else {
        cbpInformeHDesImpo.PerformCallback(s);
    }
}

function EventosInfHisDesExpo(s, e) {
    if (s != 'undefined') {
        cbpInformeHDesExpo.PerformCallback(s);
    }
    else {
        cbpInformeHDesExpo.PerformCallback(s);
    }
}

function EventosInfoImpo(s, e) {
    if (s != 'undefined') {
        cbpInformeImpo.PerformCallback(s);
    }
    else {
        cbpInformeImpo.PerformCallback(s);
    }
}

function EventosInfoExpo(s, e) {
    if (s != 'undefined') {
        cbpInformeExpo.PerformCallback(s);
    }
    else {
        cbpInformeExpo.PerformCallback(s);
    }
}

function EventosInformeDirigidos(s, e) {
    if (s != 'undefined') {
        cbpInformeDirigidos.PerformCallback(s);
    }
    else {
        cbpInformeDirigidos.PerformCallback(s);
    }
}

//[MBA][19/01/2021][nuevo repote de informe de permisos]
function EventosInformePermisos(s, e) {
    if (s != 'undefined') {
        cbpInformePermisos.PerformCallback(s);
    }
    else {
        cbpInformePermisos.PerformCallback(s);
    }
}

//[MBA][18/03/2021][Informe Ajustes]
function EventosInformeAjuste(s, e) {
    if (s != 'undefined') {
        cbpInformeAjuste.PerformCallback(s);
    }
    else {
        cbpInformeAjuste.PerformCallback(s);
    }
}


function UpdatePagerF4CTM(s, e) {

    if (document.getElementById("hdnGuardar").value == "1") {
        document.getElementById('btnErrorUser').setAttribute('data-whatever', 'gagdas'); document.getElementById('btnErrorUser').click();
    }
    else if (document.getElementById("hdnGuardar").value == "2") {
        document.getElementById('btnError').setAttribute('data-whatever', 'gagdas'); document.getElementById('btnError').click();
    }
}

function UpdatePagerF4Desperdicios(s, e) {
    if (document.getElementById("hdnGuardar").value == "1") {
        document.getElementById('btnErrorUser').setAttribute('data-whatever', 'gagdas'); document.getElementById('btnErrorUser').click();
    }
    else if (document.getElementById("hdnGuardar").value == "2") {
        document.getElementById('btnError').setAttribute('data-whatever', 'gagdas'); document.getElementById('btnError').click();
    }
}

function UpdatePager(s, e) {
    if (document.getElementById("hdnGuardar").value == "1") {
        document.getElementById('btnErrorUser').setAttribute('data-whatever', 'gagdas'); document.getElementById('btnErrorUser').click();
    }
    else if (document.getElementById("hdnGuardar").value == "2") {
        document.getElementById('btnError').setAttribute('data-whatever', 'gagdas'); document.getElementById('btnError').click();
    }
}

function EventosInformeRectificacion(s, e) {
    if (s != 'undefined') {
        cbpInformeRectificaciones.PerformCallback(s);
    }
    else {
        cbpInformeRectificaciones.PerformCallback(s);
    }

}