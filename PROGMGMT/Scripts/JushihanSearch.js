$(function () {
    $("#btnClear").click(inputClear);
    $("#btnOutput").click(outputCsv);

    // 検索結果があるときはページャー設定
    if ($("#tblResult").length) {
        var _tp = new TablePager("#tblResult", 10);
        _tp.Page(1);
    }

    // 入力制御
    $("#txtNega").on("input", checkNegaNo);
    $("#txtDpy").on("input", cutNonNumber);

    var now = new Date();
    document.getElementById("now").innerText = (now.getMonth() + 1) + "/" + (now.getDate());
});

function inputClear() {
    var dt = new Date();
    var dtFrom = dt.getFullYear() + "-"
        + ("0" + (dt.getMonth() + 1)).slice(-2) + "-"
        + ("0" + dt.getDate()).slice(-2);
    $("#dateFrom").val(dtFrom);
    $("#dateTo").val("");
    $("#listProcess").prop("selectedIndex", 0);
    $("#chkOutput").prop("checked", false);
    $("#listHan").val(new Array());
    $("#txtNega").val("");
    $("#txtDpy").val("");
}

function openRegister(dpy, proc) {
    var url = "Register?dpyno=" + dpy + "&process=" + proc;
    window.open(url, "_blank", "width=1000,height=800");
}

function outputCsv() {
    var url = "Output";
    window.open(url, "_blank", "width=1200,height=350");
}