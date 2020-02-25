using PROGMGMT.Common;
using System;
using System.ComponentModel;
using System.Data;

namespace PROGMGMT.Models.Sappan
{
    /// <summary>
    /// 検索結果クラス
    /// </summary>
    /// <remarks>
    /// 作成者    ：  sesaki
    /// 作成日    ：  2019/09/19
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

        [DisplayName("呼出しNo")]
        public string SPDPY_NO { get; set; }

        [DisplayName("発送先")]
        public string CUSTMER_RNM { get; set; }

        [DisplayName("サブネガNo")]
        public string SUBNEGA_NO { get; set; }

        [DisplayName("ブランド")]
        public string BURANDO_NM { get; set; }

        [DisplayName("商品名")]
        public string SYOHIN_NM { get; set; }

        [DisplayName("容器型")]
        public string KAN_NM { get; set; }

        [DisplayName("図面No")]
        public string ZUMEN_NO { get; set; }

        [DisplayName("ロット数")]
        public string LOTSU { get; set; }

        [DisplayName("版数")]
        public string HANSU { get; set; }

        [DisplayName("作業指示")]
        public string WORK_SHIJI { get; set; }

        [DisplayName("完了日")]
        public string COMMIT_MGMT_SSEI { get; set; }

        [DisplayName("完了日")]
        public string COMMIT_MGMT_SKEN { get; set; }

        [DisplayName("完了日")]
        public string COMMIT_MGMT_HEN1 { get; set; }

        [DisplayName("完了日")]
        public string COMMIT_MGMT_HEN2 { get; set; }

        [DisplayName("編集")]
        public string HEN_CHK1 { get; set; }

        [DisplayName("検査")]
        public string HEN_CHK2 { get; set; }

        [DisplayName("完了日")]
        public string COMMIT_MGMT_KEN { get; set; }

        [DisplayName("資料")]
        public string GYM_CHK1 { get; set; }

        [DisplayName("色見")]
        public string GYM_CHK2 { get; set; }

        [DisplayName("完了日")]
        public string COMMIT_MGMT_GYM3 { get; set; }

        [DisplayName("完了日")]
        public string COMMIT_MGMT_GYM4 { get; set; }

        [DisplayName("製造")]
        public string GYM_CHK3 { get; set; }

        [DisplayName("検版")]
        public string GYM_CHK4 { get; set; }

        [DisplayName("出荷日")]
        public string COMMIT_MGMT_GYM5 { get; set; }

        #endregion


        #region コンストラクタ
        public SearchResult(DataRow row)
        {
            MARK = row["MARK"].ToString();
            YOTEI_DAY = row["YOTEI_DAY"].ToString();
            CMNY_NM = row["CMNY_NM"].ToString();
            SPDPY_NO = row["SPDPY_NO"].ToString();
            CUSTMER_RNM = row["CUSTMER_RNM"].ToString();
            SUBNEGA_NO = row["SUBNEGA_NO"].ToString();
            BURANDO_NM = row["BURANDO_NM"].ToString();
            SYOHIN_NM = row["SYOHIN_NM"].ToString();
            KAN_NM = row["KAN_NM"].ToString();
            ZUMEN_NO = row["ZUMEN_NO"].ToString();
            LOTSU = row["LOTSU"].ToString();
            HANSU = row["HANSU"].ToString();
            WORK_SHIJI = row["WORK_SHIJI"].ToString();

            COMMIT_MGMT_SSEI = row["COMMIT_MGMT_SSEI"].ToString();

            COMMIT_MGMT_SKEN = row["COMMIT_MGMT_SKEN"].ToString();

            COMMIT_MGMT_HEN1 = row["COMMIT_MGMT_HEN1"].ToString();
            COMMIT_MGMT_HEN2 = row["COMMIT_MGMT_HEN2"].ToString();
            HEN_CHK1 = row["HEN_CHK1"].ToString();
            HEN_CHK2 = row["HEN_CHK2"].ToString();

            COMMIT_MGMT_KEN = row["COMMIT_MGMT_KEN"].ToString();

            GYM_CHK1 = row["GYM_CHK1"].ToString();
            GYM_CHK2 = row["GYM_CHK2"].ToString();
            COMMIT_MGMT_GYM3 = row["COMMIT_MGMT_GYM3"].ToString();
            COMMIT_MGMT_GYM4 = row["COMMIT_MGMT_GYM4"].ToString();
            GYM_CHK3 = row["GYM_CHK3"].ToString();
            GYM_CHK4 = row["GYM_CHK4"].ToString();
            COMMIT_MGMT_GYM5 = row["COMMIT_MGMT_GYM5"].ToString();
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
        /// 作成日    ：  2019/09/19
        /// </remarks>
        public bool CheckOutPut(string proc)
        {
            bool flag = false;

            switch (proc)
            {
                case Constants.PROCESS_SPNSEIZO:    // 刷版製造
                    if (string.IsNullOrWhiteSpace(COMMIT_MGMT_SSEI))
                    {
                        flag = true;
                    }
                    break;

                case Constants.PROCESS_SPNKENSA:    // 刷版検査
                    if (string.IsNullOrWhiteSpace(COMMIT_MGMT_SKEN))
                    {
                        flag = true;
                    }
                    break;

                case Constants.PROCESS_HENSHU:  // 編集
                    if (string.IsNullOrWhiteSpace(COMMIT_MGMT_HEN1) ||   //ブランク時、true
                      string.IsNullOrWhiteSpace(COMMIT_MGMT_HEN2))
                    {
                        flag = true;
                    }
                    break;

                case Constants.PROCESS_KENSA:   // 検査
                    if (string.IsNullOrWhiteSpace(COMMIT_MGMT_KEN))
                    {
                        flag = true;
                    }
                    break;

                case Constants.PROCESS_GYOUMU:  //　業務課出荷
                    if (string.IsNullOrWhiteSpace(COMMIT_MGMT_GYM3) ||
                        string.IsNullOrWhiteSpace(COMMIT_MGMT_GYM4) ||
                        string.IsNullOrWhiteSpace(COMMIT_MGMT_GYM5))
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
