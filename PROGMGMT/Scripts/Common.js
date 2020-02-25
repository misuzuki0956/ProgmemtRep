// input イベント用 半角数字以外カット
function cutNonNumber(e) {
  e.target.value = e.target.value.replace(/[^0-9]/g, "");
}

// input イベント用 ネガ入力チェック（英数、ハイフン、スペースのみ許可）
function checkNegaNo(e) {
  e.target.value = e.target.value.replace(/[^0-9a-zA-Z -]/g, "");
}
