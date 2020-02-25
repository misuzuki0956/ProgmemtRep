using PROGMGMT.Common;
using System;
using System.ComponentModel;
using System.Data;

namespace PROGMGMT.Models.Jushihan
{
    public class SearchResult
    {
        /// <summary>
        /// 検索結果クラス
        /// </summary>
        /// <remarks>
        /// 作成者    ：  kawana
        /// 作成日    ：  2019/10/04
        /// </remarks>
        
        #region プロパティ

        [DisplayName("  ")]
        public string MARK { get; set; }

        [DisplayName("予定日")]
        public string YOTEI_DAY { get; set; }

         [DisplayName("注文内容")]
        public string CMNY_NM { get; set; }

        [DisplayName("呼出しNo")]
        public string JSBDPY_NO { get; set; }

        [DisplayName("発送先")]
        public string CUSTMER_RNM { get; set; }

        [DisplayName("サブネガNo")]
        public string SUBNEGA_NO { get; set; }

        [DisplayName("ブランド名")]
        public string BURANDO_NM { get; set; }

        [DisplayName("商品名")]
        public string SYOHIN_NM { get; set; }

        [DisplayName("容器型")]
        public string KAN_NM { get; set; }

        [DisplayName("図面No")]
        public string ZUMEN_NO { get; set; }

        [DisplayName("版式")]
        public string HAN_KBN { get; set; }

        [DisplayName("ロット数")]
        public string LOTSU { get; set; }

        [DisplayName("版数")]
        public string HANSU { get; set; }

        [DisplayName("作業指示")]
        public string WORK_SHIJI { get; set; }

        [DisplayName("資料")]
        public string DOC_FLG { get; set; }

        [DisplayName("色見本")]
        public string CLRMH_FLG { get; set; }

        [DisplayName("完了日")]
        public string COMMIT_DATE_SPNSEIZO { get; set; }

        [DisplayName("完了日")]
        public string COMMIT_DATE_SPNKENSA { get; set; }

        [DisplayName("製造")]
        public string SPN_CHK1 { get; set; }

        [DisplayName("検版")]
        public string SPN_CHK2 { get; set; }

        [DisplayName("完了日")]
        public string COMMIT_DATE_GYOUMU { get; set; }

        #endregion

        #region コンストラクタ
        public SearchResult(DataRow row)
        {
            MARK = row["MARK"].ToString();
            YOTEI_DAY = row["YOTEI_DAY"].ToString();
            CMNY_NM = row["CMNY_NM"].ToString();
            JSBDPY_NO = row["JSBDPY_NO"].ToString();
            CUSTMER_RNM = row["CUSTMER_RNM"].ToString();
            SUBNEGA_NO = row["SUBNEGA_NO"].ToString();
            BURANDO_NM = row["BURANDO_NM"].ToString();
            SYOHIN_NM = row["SYOHIN_NM"].ToString();
            KAN_NM = row["KAN_NM"].ToString();
            ZUMEN_NO = row["ZUMEN_NO"].ToString();
            HAN_KBN = row["HAN_KBN"].ToString();
            LOTSU = row["LOTSU"].ToString();
            HANSU = row["HANSU"].ToString();
            WORK_SHIJI = row["WORK_SHIJI"].ToString();
            CLRMH_FLG = row["GYM_CHK1"].ToString();
            DOC_FLG = row["GYM_CHK2"].ToString();
            COMMIT_DATE_SPNSEIZO = row["COMMIT_DATE_SPNSEIZO"].ToString();
            COMMIT_DATE_SPNKENSA = row["COMMIT_DATE_SPNKENSA"].ToString();
            SPN_CHK1 = row["SPN_CHK1"].ToString();
            SPN_CHK2 = row["SPN_CHK2"].ToString();
            COMMIT_DATE_GYOUMU = row["COMMIT_DATE_GYOUMU"].ToString();
        }
        #endregion

        #region メソッド
        /// <summary>
        /// 出力判定
        /// </summary>
        /// <param name="proc">工程</param>
        /// <returns>True=表示、False=非表示</returns>
        /// <remarks>
        /// 作成者    ：  kawana
        /// 作成日    ：  2019/10/04
        /// </remarks>

        public bool CheckOutPut(string proc)
        {
            bool flag = false;

            switch (proc)
            {
                case Constants.PROCESS_SPNSEIZO:  // 刷版G　製造
                    if (string.IsNullOrWhiteSpace(COMMIT_DATE_SPNSEIZO))    //ブランク時、true
                    {
                        flag = true;
                    }
                    break;

                case Constants.PROCESS_SPNKENSA:  // 刷版G　検版
                    if (string.IsNullOrWhiteSpace(COMMIT_DATE_SPNKENSA))   //ブランク時、true
                    {
                        flag = true;
                    }
                    break;

                case Constants.PROCESS_GYOUMU:  //　業務
                    if (string.IsNullOrWhiteSpace(COMMIT_DATE_GYOUMU) ||
                        string.IsNullOrWhiteSpace(COMMIT_DATE_SPNSEIZO) ||
                        string.IsNullOrWhiteSpace(COMMIT_DATE_SPNKENSA))
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