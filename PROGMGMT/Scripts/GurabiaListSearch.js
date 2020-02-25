$(function () {
  $("#btnReset").click(inputClear);

  // 得意先変更で校正区分切り替え
  $("#listCustomer").change(function () {
    $("#listKosei").toggle();
    $("#listTokanKosei").toggle();
  });

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
  $("#listCustomer").prop("selectedIndex", 0);
  $("#listKosei").val(new Array());
  $("#listTokanKosei").val(new Array());
  $("#txtNega").val("");
  $("#txtDpy").val("");
}
