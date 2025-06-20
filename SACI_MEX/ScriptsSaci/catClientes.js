function EventosClientes(s, e) {
    cbpCatClientes.PerformCallback(s, '');
}

function EventosAgentes(s, e) {
    cbpcatAgentes.PerformCallback(s, '');
}

function EventosSubmaquilas(s, e) {
    cbpcatSubmaquilas.PerformCallback(s, '');
}

function EventosAF(s, e) {
    cbpcatAF.PerformCallback(s, '');
}

function EventosCategorias(s, e) {
    cbpcatCategorias.PerformCallback(s, '');
}

function EventosDivAlmacen(s, e) {
    cbpcatDivAlmacen.PerformCallback(s, '');
}

function EventosUnidades(s, e) {
    cbpcatUnidades.PerformCallback(s, '');
}

function EventosPermisos(s, e) {
    cbpcatPermisos.PerformCallback(s, '');
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