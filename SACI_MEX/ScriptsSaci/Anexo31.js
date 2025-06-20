//
//Inf. de descargos Anexo 31 
//

function EventosAnexo31(s, e) {
    if (s != 'undefined') {
        cbpcatAnexo31.PerformCallback(s);
    }
    else {
        cbpcatAnexo31.PerformCallback(s);
    }
}

function AlertasAnexo31(s, e) {

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

