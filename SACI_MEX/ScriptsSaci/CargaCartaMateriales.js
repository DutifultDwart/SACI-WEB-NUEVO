function OnUploadCompleteExcel() {
    cp2.PerformCallback();
}

function OnBtnUploadClick(s, e) {
    if (uploadControl.GetText() != "") {
        uploadControl.Upload();

    }
}

function CargaGrid(s, e) {

    if (s == 'Limpia') {
        document.getElementById("hdnGuardar").value = "0"
        cp2.PerformCallback(s, '');
    }
    else {
        cp2.PerformCallback(s, '');
    }
   
};


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
