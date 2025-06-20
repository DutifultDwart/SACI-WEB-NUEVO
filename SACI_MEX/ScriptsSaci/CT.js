function EventosCT(s, e) {
    if (s != 'undefined') {
        cbkConsTransf.PerformCallback(s, '');
    }
    else {
        cbkConsTransf.PerformCallback(s, '');
    }
}
function UpdatePager(s, e) {

    if (document.getElementById("hdnGuardar").value == "1") {
        document.getElementById('btnSucces').setAttribute('data-whatever', 'gagdas'); document.getElementById('btnSucces').click();
    }
    else if (document.getElementById("hdnGuardar").value == "2") {
        document.getElementById('btnErrorUser').setAttribute('data-whatever', 'gagdas'); document.getElementById('btnErrorUser').click();
    }
    else if (document.getElementById("hdnGuardar").value == "3") {
        document.getElementById('btnCTPartidas').setAttribute('data-whatever', 'gagdas'); document.getElementById('btnCTPartidas').click();
    }
    else if (document.getElementById("hdnGuardar").value == "4") {
        document.getElementById('btnError').setAttribute('data-whatever', 'gagdas'); document.getElementById('btnError').click();
    }
}