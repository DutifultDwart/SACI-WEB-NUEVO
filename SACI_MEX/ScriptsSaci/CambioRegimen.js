// EVENTO PARA CARGAR LOS PERIODOS CORRESPONDIENTES AL AÑO
function CargaGrid(s, e) {

    if (s != 'undefined') {
        cp2.PerformCallback(s, '');
    }
    else {
        var MES = s.GetSelectedItem().GetColumnText('MES');
        cp2.PerformCallback(MES.concat('-', s.GetSelectedItem().GetColumnText('AÑO')));
    }
}


function Show_Hide_Display(s, e) {
    cp2.PerformCallback(s, '');   
}


function UpdatePager(s, e) {

    if (document.getElementById("hddGuardar").value == "1") {
        document.getElementById('btnSucces').setAttribute('data-whatever', 'gagdas'); document.getElementById('btnSucces').click();
    }
    else if (document.getElementById("hddGuardar").value == "2") {
        document.getElementById('btnErrorUser').setAttribute('data-whatever', 'gagdas'); document.getElementById('btnErrorUser').click();
    }
    else if (document.getElementById("hddGuardar").value == "3") {
        document.getElementById('btnError').setAttribute('data-whatever', 'gagdas'); document.getElementById('btnError').click();
    }
}
