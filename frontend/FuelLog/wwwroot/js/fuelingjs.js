window.addEventListener('load', function () {
    console.log('fuelingjs loaded')
    $('#spinner').hide();
});
function DoPostBack() {
    //alert('DoPostBack called');
    var select = document.getElementById("user");
    var option = select.options[select.selectedIndex];
    if (option.value != "-") {
        $('#spinner').show();
        document.CreateFueling.action = "/Fueling/GetVehicles";
        document.CreateFueling.submit();
    }
}

