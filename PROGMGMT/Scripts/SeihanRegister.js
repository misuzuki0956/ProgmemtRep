$(function () {

  // 非活性化
  $(".disabled").prop("disabled", true);

  // 担当者コードイベント
  $("[id$=EmployeeCd]")
    .on("input", cutNonNumber)
    .change(setEmployeeName);

});

// 該当する担当者名をセット
function setEmployeeName(e) {
  var empCd = this.value;
  var empNmId = this.id.replace("Cd", "Name");
  var empNm = "";

  $.each(empList, function (idx, item) {
    if (item.Value == empCd) {
      empNm = item.Text;
    }
  });

  $("#" + empNmId).val(empNm);
}
