$(function () {

    // ユーザーIDイベント
    $("[id$=Id]")
        .on("input", cutNonNumber);

    // パスワードイベント
    $("[id$=Pass]")
        .on("input", cutNonNumber);

});
