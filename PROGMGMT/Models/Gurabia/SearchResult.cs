using PROGMGMT.Common;
using System.ComponentModel;
using System.Data;

namespace PROGMGMT.Models.Gurabia
{
    /// <summary>
    /// 検索結果クラス
    /// </summary>
    /// <remarks>
    /// 作成者    ：  sesaki
    /// 作成日    ：  2019/11/13
    /// </remarks>
    public class SearchResult
    {
        #region プロパティ

        [DisplayName("  ")]
        public string MARK { get; set; }

        [DisplayName("予定日")]
        public string YOTEI_DAY { get; set; }

        [DisplayName("注文内容")]
        public string CMNY_NM { get; set; }

        [DisplayName("注文区分")]
        public string CMKBN_NM { get; set; }

        [DisplayName("呼出しNo")]
        public string DPY_NO { get; set; }

        [DisplayName("発送先")]
        public string CUSTMER_RNM { get; set; }

        [DisplayName("サブネガNo")]
        public string SUBNEGA_NO { get; set; }

        [DisplayName("ブランド")]
        public string BURANDO_NM { get; set; }

        [DisplayName("品名")]
        public string SYOHIN_NM { get; set; }

        [DisplayName("規格")]
        public string KAN_NM { get; set; }

        [DisplayName("校正区分")]
        public string KSKBN_NM { get; set; }

        [DisplayName("作業指示")]
        public string WORK_SHIJI { get; set; }

        [DisplayName("完了日")]
        public string COMMIT_MGMT_HAN { get; set; }

        [DisplayName("完了日")]
        public string COMMIT_MGMT_HEN1 { get; set; }

        [DisplayName("完了日")]
        public string COMMIT_MGMT_HEN2 { get; set; }

        [DisplayName("編集")]
        public string HEN_CHK1 { get; set; }

        [DisplayName("検査")]
        public string HEN_CHK2 { get; set; }

        [DisplayName("電送予定日")]
        public string DENSOU_DAY { get; set; }

        [DisplayName("電送")]
        public string DENSO_CHK { get; set; }

        [DisplayName("完了日")]
        public string COMMIT_MGMT_KEN1 { get; set; }

        [DisplayName("完了日")]
        public string COMMIT_MGMT_KEN2 { get; set; }

        [DisplayName("工程1")]
        public string KEN_CHK1 { get; set; }

        [DisplayName("工程2")]
        public string KEN_CHK2 { get; set; }

        [DisplayName("出荷日")]
        public string COMMIT_MGMT_GYM{ get; set; }
        #endregion


        #region コンストラクタ
        public SearchResult(DataRow row)
        {
            MARK = row["MARK"].ToString();
            YOTEI_DAY = row["YOTEI_DAY"].ToString();
            CMNY_NM = row["CMNY_NM"].ToString();
            CMKBN_NM = row["CMKBN_NM"].ToString();
            DPY_NO = row["DPY_NO"].ToString();
            SUBNEGA_NO = row["SUBNEGA_NO"].ToString();
            BURANDO_NM = row["BURANDO_NM"].ToString();
            SYOHIN_NM = row["SYOHIN_NM"].ToString();
            KAN_NM = row["KAN_NM"].ToString();
            KSKBN_NM = row["KSKBN_NM"].ToString();
            WORK_SHIJI = row["WORK_SHIJI"].ToString();

            COMMIT_MGMT_HAN = row["COMMIT_MGMT_HAN"].ToString();

            COMMIT_MGMT_HEN1 = row["COMMIT_MGMT_HEN1"].ToString();
            COMMIT_MGMT_HEN2 = row["COMMIT_MGMT_HEN2"].ToString();
            HEN_CHK1 = row["HEN_CHK1"].ToString();
            HEN_CHK2 = row["HEN_CHK2"].ToString();
            DENSOU_DAY = row["DENSOU_DAY"].ToString();
            DENSO_CHK = row["DENSO_CHK"].ToString();

            COMMIT_MGMT_KEN1 = row["COMMIT_MGMT_KEN1"].ToString();
            COMMIT_MGMT_KEN2 = row["COMMIT_MGMT_KEN2"].ToString();
            KEN_CHK1 = row["KEN_CHK1"].ToString();
            KEN_CHK2 = row["KEN_CHK2"].ToString();

            COMMIT_MGMT_GYM = row["COMMIT_MGMT_GYM"].ToString();
        }
        #endregion

        #region メソッド
        /// <summary>
        /// 出力判定
        /// </summary>
        /// <param name="proc">工程</param>
        /// <returns>True=表示、False=非表示</returns>
        /// <remarks>
        /// 作成者    ：  sesaki
        /// 作成日    ：  2019/11/13
        /// </remarks>
        public bool CheckOutPut(string proc)
        {
            bool flag = false;

            switch (proc)
            {
                case Constants.PROCESS_HANSHITA:    // 版下
                    if (string.IsNullOrWhiteSpace(COMMIT_MGMT_HAN))
                    {
                        flag = true;
                    }
                    break;

                case Constants.PROCESS_HENSHU:  // 編集
                    //電送予定日有りで、チェックが無い
                    if (!(string.IsNullOrWhiteSpace(DENSOU_DAY)) && (string.IsNullOrWhiteSpace(DENSO_CHK)))
                    {
                        flag = true;
                    }

                    if (flag == false)
                    {
                        if (string.IsNullOrWhiteSpace(COMMIT_MGMT_HEN1) ||   //ブランク時、true
                                  string.IsNullOrWhiteSpace(COMMIT_MGMT_HEN2))
                        {
                            flag = true;
                        }
                    }
                    break;

               case Constants.PROCESS_KENSA:   // 検査
                    //電送予定日有りで、チェックが無い
                    if (!(string.IsNullOrWhiteSpace(DENSOU_DAY)) && (string.IsNullOrWhiteSpace(DENSO_CHK)))
                    {
                        flag = true;
                    }

                    if (flag == false)
                    {
                        if (string.IsNullOrWhiteSpace(COMMIT_MGMT_KEN1) ||   //ブランク時、true
                                  string.IsNullOrWhiteSpace(COMMIT_MGMT_KEN2))
                        {
                            flag = true;
                        }
                    }
                    break;

                case Constants.PROCESS_GYOUMU:  //　業務課出荷
                    if (string.IsNullOrWhiteSpace(COMMIT_MGMT_GYM))
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
