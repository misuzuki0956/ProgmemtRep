$(function () {
    $("#btnClear").click(inputClear);
});

function inputClear() {
    var dt = new Date();
    var dtFrom = dt.getFullYear() + "-"
        + ("0" + (dt.getMonth() + 1)).slice(-2) + "-"
        + ("0" + dt.getDate()).slice(-2);
    $("#dateFrom").val(dtFrom);
    $("#dateTo").val("");
    $("#listProcess").val(new Array());
    $("#listCmny").val(new Array());
    $("#listFact").val(new Array());
}
