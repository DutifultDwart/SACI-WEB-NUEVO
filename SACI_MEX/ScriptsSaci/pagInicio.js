function EventosInicio(s, e) {
    cbpDefault.PerformCallback(s, '');
}

function UpdatePager(s, e) {

    if (document.getElementById("hdnGuardar").value == "1") {
        document.getElementById('btnErrorUser').setAttribute('data-whatever', 'gagdas'); document.getElementById('btnErrorUser').click();
    }
    else if (document.getElementById("hdnGuardar").value == "2") {
        document.getElementById('btnError').setAttribute('data-whatever', 'gagdas'); document.getElementById('btnError').click();
    }
   else  if (document.getElementById("hdnGuardar").value == "3") {
        document.getElementById('btnSucces').setAttribute('data-whatever', 'gagdas'); document.getElementById('btnSucces').click();
    }
}