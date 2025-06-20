function ShowInfo() {
    var x = document.getElementById("myDIV");
    if (x.style.display === "none") {
        x.style.display = "block";
    } else {
        x.style.display = "none";
    }
}


function ActivaCve(txtControl) {

    $('#modalActividad').on('shown.bs.modal', function () {
        var TXTCVE = document.getElementById(txtControl)
        TXTCVE.focus()
    });
}


function AddSelectedItems() {
    MoveSelectedItems(lbAvailable, lbChoosen);
    UpdateButtonState();
}
function AddAllItems() {
    MoveAllItems(lbAvailable, lbChoosen);
    UpdateButtonState();
}
function RemoveSelectedItems() {
    MoveSelectedItems(lbChoosen, lbAvailable);
    UpdateButtonState();
}
function RemoveAllItems() {
    MoveAllItems(lbChoosen, lbAvailable);
    UpdateButtonState();
}
function MoveSelectedItems(srcListBox, dstListBox) {
    srcListBox.BeginUpdate();
    dstListBox.BeginUpdate();
    var items = srcListBox.GetSelectedItems();
    for (var i = items.length - 1; i >= 0; i = i - 1) {
        dstListBox.AddItem(items[i].text, items[i].value);
        srcListBox.RemoveItem(items[i].index);
    }
    srcListBox.EndUpdate();
    dstListBox.EndUpdate();
}
function MoveAllItems(srcListBox, dstListBox) {
    srcListBox.BeginUpdate();
    var count = srcListBox.GetItemCount();
    for (var i = 0; i < count; i++) {
        var item = srcListBox.GetItem(i);
        dstListBox.AddItem(item.text, item.value);
    }
    srcListBox.EndUpdate();
    srcListBox.ClearItems();
}
function UpdateButtonState() {
    btnMoveAllItemsToRight.SetEnabled(lbAvailable.GetItemCount() > 0);
    btnMoveAllItemsToLeft.SetEnabled(lbChoosen.GetItemCount() > 0);
    btnMoveSelectedItemsToRight.SetEnabled(lbAvailable.GetSelectedItems().length > 0);
    btnMoveSelectedItemsToLeft.SetEnabled(lbChoosen.GetSelectedItems().length > 0);
}


function LimpiaControles() {
    var TXTCVE = document.getElementById('<%= TXT_CVE_PERFIL.ClientID %>')
    var TXTNOM = document.getElementById('<%= TXT_NOM_PERFIL.ClientID %>')
    TXTCVE.value = '';
    TXTNOM.value = '';
}


function soloNumeros(e) {
    var key = window.Event ? e.which : e.keyCode
    return (key >= 48 && key <= 57)
}