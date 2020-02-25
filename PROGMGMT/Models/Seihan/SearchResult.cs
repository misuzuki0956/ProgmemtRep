using PROGMGMT.Common;
using System.ComponentModel;
using System.Data;

namespace PROGMGMT.Models.Seihan
{
    /// <summary>
    /// 検索結果クラス
    /// </summary>
    public class SearchResult
    {
        #region プロパティ

        [DisplayName("  ")]
        public string MARK { get; set; }

        [DisplayName("予定日")]
        public string YOTEI_DAY { get; set; }

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

        [DisplayName("容器型")]
        public string KAN_NM { get; set; }

        [DisplayName("図面No")]
        public string ZUMEN_NO { get; set; }

        [DisplayName("版式")]
        public string HAN_KBN { get; set; }

        [DisplayName("作業指示")]
        public string WORK_MEMO { get; set; }

        [DisplayName("校正区分")]
        public string KSKBN { get; set; }

        [DisplayName("完了日")]
        public string COMMIT_DATE_HAN { get; set; }

        [DisplayName("完了日")]
        public string COMMIT_DATE_HEN1 { get; set; }

        [DisplayName("完了日")]
        public string COMMIT_DATE_HEN2 { get; set; }

        [DisplayName("編集")]
        public string HEN_MARK1 { get; set; }

        [DisplayName("検査")]
        public string HEN_MARK2 { get; set; }

        [DisplayName("完了日")]
        public string COMMIT_DATE_KEN1 { get; set; }

        [DisplayName("完了日")]
        public string COMMIT_DATE_KEN2 { get; set; }

        [DisplayName("工程１")]
        public string KEN_MARK1 { get; set; }

        [DisplayName("工程２")]
        public string KEN_MARK2 { get; set; }

        [DisplayName("校正材")]
        public string KOUSEIZAI { get; set; }

        [DisplayName("色数")]
        public string COLOR_NUM { get; set; }

        [DisplayName("部数")]
        public string COPY_NUM { get; set; }

        [DisplayName("完了日")]
        public string COMMIT_DATE_HIRA { get; set; }

        [DisplayName("完了日")]
        public string COMMIT_DATE_KYOK { get; set; }

        [DisplayName("完了日")]
        public string COMMIT_DATE_KOUSEI { get; set; }

        [DisplayName("完了日")]
        public string COMMIT_DATE_GYOUM { get; set; }
        #endregion

        #region コンストラクタ
        public SearchResult(DataRow row)
        {
            MARK = row["MARK"].ToString();
            YOTEI_DAY = row["YOTEI_DAY"].ToString();
            CMNY = row["CMNY_NM"].ToString();
            CMKBN = row["CMKBN_NM"].ToString();
            DPY_NO = row["DPY_NO"].ToString();
            SUBNEGA_NO = row["SUBNEGA_NO"].ToString();
            BURANDO_NM = row["BURANDO_NM"].ToString();
            SYOHIN_NM = row["SYOHIN_NM"].ToString();
            KAN_NM = row["KAN_NM"].ToString();
            ZUMEN_NO = row["ZUMEN_NO"].ToString();
            HAN_KBN = row["HAN_KBN"].ToString();
            WORK_MEMO = row["WORK_SHIJI"].ToString();
            KSKBN = row["KSKBN_NM"].ToString();
            KOUSEIZAI = row["KOSEIZAI"].ToString();
            COLOR_NUM = row["CLRKZ"].ToString();
            COPY_NUM = row["GOKEI_CT"].ToString();

            COMMIT_DATE_HAN = row["COMMIT_DATE_HAN"].ToString();

            COMMIT_DATE_HEN1 = row["COMMIT_DATE_HEN1"].ToString();
            COMMIT_DATE_HEN2 = row["COMMIT_DATE_HEN2"].ToString();
            HEN_MARK1 = row["HEN_CHK1"].ToString();
            HEN_MARK2 = row["HEN_CHK2"].ToString();

            COMMIT_DATE_KEN1 = row["COMMIT_DATE_KEN1"].ToString();
            COMMIT_DATE_KEN2 = row["COMMIT_DATE_KEN2"].ToString();
            KEN_MARK1 = row["KEN_CHK1"].ToString();
            KEN_MARK2 = row["KEN_CHK2"].ToString();

            COMMIT_DATE_HIRA = row["COMMIT_DATE_HIRA"].ToString();

            COMMIT_DATE_KYOK = row["COMMIT_DATE_KYOK"].ToString();

            COMMIT_DATE_KOUSEI = row["COMMIT_DATE_KOUSEI"].ToString();

            COMMIT_DATE_GYOUM = row["COMMIT_DATE_GYOUM"].ToString();
        }
        #endregion

        #region メソッド
        /// <summary>
        /// 出力判定
        /// </summary>
        /// <param name="proc">工程</param>
        /// <returns>True=表示、False=非表示</returns>
        /// <remarks>
        /// 作成者    ：  nakao
        /// 作成日    ：  2019/09/01
        /// </remarks>
        public bool CheckOutPut(string proc)
        {
            bool flag = false;

            switch (proc)
            {
                case Constants.PROCESS_HANSHITA:    // 版下
                    if (string.IsNullOrWhiteSpace(COMMIT_DATE_HAN))
                    {
                        flag = true;
                    }
                    break;

                case Constants.PROCESS_HENSHU:  // 編集
                    if (string.IsNullOrWhiteSpace(COMMIT_DATE_HEN1) ||   //ブランク時、true
                      string.IsNullOrWhiteSpace(COMMIT_DATE_HEN2))
                    {
                        flag = true;
                    }
                    break;

                case Constants.PROCESS_KENSA:   // 検査
                    if (string.IsNullOrWhiteSpace(COMMIT_DATE_KEN1) ||   //ブランク時、true
                      string.IsNullOrWhiteSpace(COMMIT_DATE_KEN2))
                    {
                        flag = true;
                    }
                    break;

                case Constants.PROCESS_HKOSEI:  // 平板
                    if (string.IsNullOrWhiteSpace(COMMIT_DATE_HIRA))
                    {
                        flag = true;
                    }
                    break;
                case Constants.PROCESS_KKOSEI:  // 曲面
                    if (string.IsNullOrWhiteSpace(COMMIT_DATE_KYOK))
                    {
                        flag = true;
                    }
                    break;
                case Constants.PROCESS_KOSEIKENSA:  //　校正検査
                    if (string.IsNullOrWhiteSpace(COMMIT_DATE_KOUSEI))
                    {
                        flag = true;
                    }
                    break;
                case Constants.PROCESS_GYOUMU:  //　業務
                    if (string.IsNullOrWhiteSpace(COMMIT_DATE_GYOUM))
                    {
                        flag = true;
                    }
                    break;
            }
            return flag;
        }
        #endregion
    }
}