$(function () {

    // �񊈐���
    $(".disabled").prop("disabled", true);

    // �S���҃R�[�h�C�x���g
    $("[id$=EmployeeCd]")
        .on("input", cutNonNumber)
        .change(setEmployeeName);

    // �������C��
    $("[id$=PRODUCT_LINE]")
        .on("input", cutNonNumber);

    // ���ۃ��C��
    $("[id$=GENSHO_LINE]")
        .on("input", cutNonNumber);

    // �A�E�g�Ő�
    $("[id$=OUT_HANSU]")
        .on("input", cutNonNumber);

    // �P�[�XNo
    $("[id$=CaseNo]")
        .on("input", cutNonNumber);


});

// �Y������S���Җ����Z�b�g
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
