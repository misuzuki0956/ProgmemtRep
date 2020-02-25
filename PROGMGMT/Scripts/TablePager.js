/*
  TablePager ：  テーブルの tr の表示制御
  前提  ：  １．１つ目がヘッダー
            ２．jQeryあり
  使い方
    インスタンス作成（引数：テーブルのID（"#"付き）、表示行数）
      html  :   <table id="xxxx"></table>
      js    :   var hoge = new TablePaging("#table_id", 10);

  １）ページ切替    ：  hoge.Page(n);   //  n：表示したいページ

  変更履歴

*/
var TablePager = (function() {

  // コンストラクタ
  var TablePager = function(tbl, dsp) {
    if ( !( this instanceof TablePager ) ) {
      return new TablePager(tbl);
    }
    var _t = this;

    _t.tbl = $(tbl);      //  対象テーブル
    _t.dsp = dsp;         // １ページ表示数
    _t.page = 1;          //  表示中ページ
    _t.last = Math.ceil( ( $("tr", tbl).length - 1 ) / dsp ); //  最終ページ
    _t.pager;             // ページ切替表示エリア

    // ページリンクがあった場合はクリアして再利用
    if ($(tbl + "Pager").length > 0) {
      _t.pager = $(tbl + "Pager").empty();
    } else {
      _t.pager = $("<div></div>", { id: _t.tbl[0].id + "Pager" });
      // ページ切替リンク作成
      $(tbl).after(
        _t.pager
      );
    }

    // 最初へのリンク
    _t.pager.append(
      $("<a href='#'><<</a>").click(function(){_t.Page(1);})
    );

    // 間のページリンク
    for ( i = 1; i <= _t.last; i++ ) {
      _t.pager.append("｜");
      _t.pager.append(
        $("<a href='#'>" + i + "</a>").click(function(){_t.Page(this.text);})
      );
    }

    _t.pager.append("｜");

    // 最後へのリンク
    _t.pager.append(
      $("<a href='#'>>></a>").click(function(){_t.Page(_t.last);})
    );
  }

  // ページ切替
  TablePager.prototype.Page = function(n) {

    if ( n < 1 || this.last < n ) return;

    this.page = n;
    $("tr:gt(0)", this.tbl).hide();
    $("tr:gt(" + ( n - 1 ) * this.dsp + "):lt(" + this.dsp + ")").show();

    // 必要であればリンク制御追加
  }

  return TablePager;
})();
