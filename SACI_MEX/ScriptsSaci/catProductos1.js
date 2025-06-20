function EventosProductos(s, e) {
    if (s == 'seleccionarPartida') {
        cbpCatProductos.PerformCallback(s, '');
    }
    else {
        cbpCatProductos.PerformCallback(s, '');
    }
}

function FinGrid(s,e) {
    alert('Fin grid');
    //grvLstMateriales.PerformCallback();
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
        document.getElementById('btnEstructura').setAttribute('data-whatever', 'gagdas'); document.getElementById('btnEstructura').click();
    }
    else if (document.getElementById("hdnGuardar").value == "5") {
        document.getElementById('btnError').setAttribute('data-whatever', 'gagdas'); document.getElementById('btnError').click();
    }
}