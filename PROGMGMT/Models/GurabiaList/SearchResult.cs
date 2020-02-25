using System.ComponentModel;
using System.Data;

namespace PROGMGMT.Models.GurabiaList
{
    /// <summary>
    /// 検索結果クラス
    /// </summary>
    /// <remarks>
    /// 作成者    ：  nakao
    /// 作成日    ：  2019/11/13
    /// </remarks>
    public class SearchResult
    {
        #region プロパティ
        [DisplayName("注文内容")]
        public string CMNY { get; set; }

        [DisplayName("注文区分")]
        public string CMKBN { get; set; }

        [DisplayName("呼出しNo")]
        public string DPY_NO { get; set; }

        [DisplayName("サブネガNo")]
        public string SUBNEGA_NO { get; set; }

        [DisplayName("ブランド")]
        public string BURANDO_NM { get; set; }

        [DisplayName("品名")]
        public string SYOHIN_NM { get; set; }

        [DisplayName("規格")]
        public string KIKAKU { get; set; }

        [DisplayName("校正区分")]
        public string KSKBN_NM { get; set; }

        [DisplayName("版下予定")]
        public string HANSHITA_YOTEI { get; set; }
        [DisplayName("完了日")]
        public string HANSHITA_COMMIT { get; set; }

        [DisplayName("編集(編集)予定")]
        public string HENSHUH_YOTEI { get; set; }
        [DisplayName("完了日")]
        public string HENSHUH_COMMIT { get; set; }

        [DisplayName("電送予定日")]
        public string DENSO_YOTEI { get; set; }
        [DisplayName("完了")]
        public string DENSO_COMMIT { get; set; }

        [DisplayName("編集(検査)予定")]
        public string HENSHUK_YOTEI { get; set; }
        [DisplayName("完了日")]
        public string HENSHUK_COMMIT { get; set; }

        [DisplayName("検査(工程1)予定")]
        public string KENSA1_YOTEI { get; set; }
        [DisplayName("完了日")]
        public string KENSA1_COMMIT { get; set; }

        [DisplayName("検査(工程2)予定")]
        public string KENSA2_YOTEI { get; set; }
        [DisplayName("完了日")]
        public string KENSA2_COMMIT { get; set; }

        [DisplayName("業務予定")]
        public string GYOUMU_YOTEI { get; set; }
        [DisplayName("完了日")]
        public string GYOUMU_COMMIT { get; set; }

        [DisplayName("発送指示")]
        public string WORK_MEMO { get; set; }

        #endregion

        #region コンストラクタ
        public SearchResult(DataRow row)
        {
            CMNY = row["CMNY_NM"].ToString();
            CMKBN = row["CMKBN_NM"].ToString();
            DPY_NO = row["DPY_NO"].ToString();
            SUBNEGA_NO = row["SUBNEGA_NO"].ToString();
            BURANDO_NM = row["BURANDO_NM"].ToString();
            SYOHIN_NM = row["SYOHIN_NM"].ToString();
            KIKAKU = row["KIKAKU"].ToString();
            KSKBN_NM = row["KSKBN_NM"].ToString();

            HANSHITA_YOTEI = row["HANSHITA_YOTEI"].ToString();
            HANSHITA_COMMIT = row["HANSHITA_COMMIT"].ToString();
            HENSHUH_YOTEI = row["HENSHUH_YOTEI"].ToString();
            HENSHUH_COMMIT = row["HENSHUH_COMMIT"].ToString();
            DENSO_YOTEI = row["DENSO_YOTEI"].ToString();
            DENSO_COMMIT = row["DENSO_COMMIT"].ToString();
            HENSHUK_YOTEI = row["HENSHUK_YOTEI"].ToString();
            HENSHUK_COMMIT = row["HENSHUK_COMMIT"].ToString();
            KENSA1_YOTEI = row["KENSA1_YOTEI"].ToString();
            KENSA1_COMMIT = row["KENSA1_COMMIT"].ToString();
            KENSA2_YOTEI = row["KENSA2_YOTEI"].ToString();
            KENSA2_COMMIT = row["KENSA2_COMMIT"].ToString();
            GYOUMU_YOTEI = row["GYOUMU_YOTEI"].ToString();
            GYOUMU_COMMIT = row["GYOUMU_COMMIT"].ToString();

            WORK_MEMO = row["WORK_SHIJI"].ToString();
        }

        #endregion

        #region メソッド

        /// <summary>
        /// 未完了確認
        /// </summary>
        /// <returns>
        ///   True =予定日対して１つでも完了日が未登録
        ///   False=予定日対して完了日が全て登録されている
        /// </returns>
        /// <remarks>
        /// 作成者    ：  nakao
        /// 作成日    ：  2019/11/13
        /// </remarks>
        public bool IsUnfinished()
        {
            bool rslt;

            // データ有り ： yy/mm/dd,  データ無し ： -
            // 予定日があり、完了日がないデータがあれば true となる
            // 電送は 予定日：yy/mm/dd かつ 完了：空白 の場合 true
            rslt = HANSHITA_COMMIT.Length < HANSHITA_YOTEI.Length
                || HENSHUH_COMMIT.Length < HENSHUH_YOTEI.Length
                || ( DENSO_YOTEI.Length + DENSO_COMMIT.Length == 8 )
                || HENSHUK_COMMIT.Length < HENSHUK_YOTEI.Length
                || KENSA1_COMMIT.Length < KENSA1_YOTEI.Length
                || KENSA2_COMMIT.Length < KENSA2_YOTEI.Length
                || GYOUMU_COMMIT.Length < GYOUMU_YOTEI.Length;

            return rslt;
        }

        #endregion
    }
}