function EventosExpo(s, e) {
    if (s != 'undefined') {
        cpExportaciones.PerformCallback(s, '');
    }
    else {
        var MES = s.GetSelectedItem().GetColumnText('MES');
        cpExportaciones.PerformCallback(MES.concat('-', s.GetSelectedItem().GetColumnText('AÑO')));
    }
}

function Show_Hide_Display(s, e) {

    cpExportaciones.PerformCallback(s, '');

}

function UpdatePager(s, e) {

    if (document.getElementById("hdnGuardar").value == "1") {
        document.getElementById('btnSucces').setAttribute('data-whatever', 'gagdas'); document.getElementById('btnSucces').click();
    }
    else if (document.getElementById("hdnGuardar").value == "2") {
        document.getElementById('btnErrorUser').setAttribute('data-whatever', 'gagdas'); document.getElementById('btnErrorUser').click();
    }
    else if (document.getElementById("hdnGuardar").value == "3") {
        document.getElementById('btnError').setAttribute('data-whatever', 'gagdas'); document.getElementById('btnError').click();
    }
}
