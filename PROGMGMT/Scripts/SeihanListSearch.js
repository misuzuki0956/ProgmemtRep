$(function () {
  $("#btnReset").click(inputClear);

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
  $("#listKosei").val(new Array());
  $("#listHan").val(new Array());
  $("#txtShohin").val("");
  $("#txtNega").val("");
  $("#txtDpy").val("");
}
