function EventosDescargos(s, e) {
    //if (s != 'undefined') {
    if (s == 'seleccionarPartida') {
        //var index = e.visibleIndex
        //if (index != null) {
        //    $("#hiddenSalidas").val(index)
        //};
        cbkDescargos.PerformCallback(s, '');
    }
    else {
        cbkDescargos.PerformCallback(s, '');
    }
    //else {
    //    var MES = s.GetSelectedItem().GetColumnText('MES');
    //    cbkDescargos.PerformCallback(MES.concat('-', s.GetSelectedItem().GetColumnText('AÑO')));
    //}
}


function UpdatePager(s, e) {

    if (document.getElementById("hdnGuardar").value == "1") {
        document.getElementById('btnSucces').setAttribute('data-whatever', 'gagdas'); document.getElementById('btnSucces').click();       
    }
    else if (document.getElementById("hdnGuardar").value == "2") {
        document.getElementById('btnErrorUser').setAttribute('data-whatever', 'gagdas'); document.getElementById('btnErrorUser').click();
    }
    else if (document.getElementById("hdnGuardar").value == "3") {
      document.getElementById('btnImpoPartidas').setAttribute('data-whatever', 'gagdas'); document.getElementById('btnImpoPartidas').click();
    }
}