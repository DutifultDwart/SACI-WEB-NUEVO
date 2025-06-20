//$(document).ready(function () {
//    CargarFechaImpo()
//})



// EVENTO PARA CARGAR LOS PERIODOS CORRESPONDIENTES AL AÑO
function CargaGrid(s, e) {
    
    if (s == 's') {
        cp2.PerformCallback(s, '');
    }
    else if (s == 'r') {
        cp2.PerformCallback(s, '');
    }
    else if (s == 'editImpo') {
        cp2.PerformCallback(s, '');
    }
    else if (s == 'CancelEditImpo') {
        cp2.PerformCallback(s, '');
    }
    else if (s == 'SaveImpo') {
        cp2.PerformCallback(s, '');        
    }
    else if (s == 'DelImpo') {
        cp2.PerformCallback(s, '');
    }
    else if (s == 'AddPartida') {
        cp2.PerformCallback(s, '');
    }
    else if (s == 'EditPartida') {
        cp2.PerformCallback(s, '');
    }
    else if (s == 'DeletePartida') {
        cp2.PerformCallback(s, '');
    }
    else if (s == 'CancelPartida') {
        cp2.PerformCallback(s, '');
    }
    else if (s == 'SavePartida') {
        cp2.PerformCallback(s, '');
    }
    else if (s == 'TotalFactor') {
        cp2.PerformCallback(s, '');
    }
    else {        
        var MES = s.GetSelectedItem().GetColumnText('MES');
        cp2.PerformCallback(MES.concat('-', s.GetSelectedItem().GetColumnText('AÑO')));        
    }    
};

function Show_Hide_Display(s, e) {
    
    //var div1 = document.getElementById("checkAvailability");    
    //if (div1.style.display == "" || div1.style.display == "block") {
    //    div1.style.display = "none";
    //}
    //else {
    //    div1.style.display = "block";
    //}
    //alert('dsd')
    cp2.PerformCallback(s, '');
    //return false;
    
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
