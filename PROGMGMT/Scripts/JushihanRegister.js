$(function () {

    // 非活性化
    $(".disabled").prop("disabled", true);

    // 担当者コードイベント
    $("[id$=EmployeeCd]")
        .on("input", cutNonNumber)
        .change(setEmployeeName);

    // 製造ライン
    $("[id$=PRODUCT_LINE]")
        .on("input", cutNonNumber);

    // 現象ライン
    $("[id$=GENSHO_LINE]")
        .on("input", cutNonNumber);

    // アウト版数
    $("[id$=OUT_HANSU]")
        .on("input", cutNonNumber);

    // ケースNo
    $("[id$=CaseNo]")
        .on("input", cutNonNumber);


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
