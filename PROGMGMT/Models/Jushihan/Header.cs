using PROGMGMT.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace PROGMGMT.Models.Jushihan
{
    /// </summary>
    /// ヘッダ情報クラス
    /// </summary>
    /// <remarks>
    /// 作成者    ：  kawana
    /// 作成日    ：  2019/10/24
    /// </remarks>
    public class Header
    {
        #region プロパティ

        [DisplayName("ネガNo")]
        public string SUBNEGA_NO { get; set; }

        [DisplayName("呼出しNo")]
        public string JSBDPY_NO { get; set; }

        [DisplayName("注文内容")]
        public string CMNY { get; set; }

        [DisplayName("ブランド")]
        public string BURANDO_NM { get; set; }

        [DisplayName("発送先")]
        public string CUSTMER_RNM { get; set; }

        [DisplayName("ロット数")]
        public string LOTSU { get; set; }

        [DisplayName("版数")]
        public string HANSU { get; set; }

        [DisplayName("品名")]
        public string SYOHIN_NM { get; set; }

        [DisplayName("容器型")]
        public string KAN_NM { get; set; }

        [DisplayName("図面No")]
        public string ZUMEN_NO { get; set; }

        [DisplayName("版式")]
        public string HAN_KBN { get; set; }

        [DisplayName("色名")]
        public string COLOR_NM { get; set; }

        #endregion

        #region コンストラクタ

        public Header() { }

        public Header(string dpyno)
        {
            JSBDPY_NO = dpyno;
            GetHeader();
        }

        #endregion

        #region メソッド

        /// <summary>
        /// ヘッダ情報取得
        /// </summary>
        /// <remarks>
        /// 作成者    ：  kawana
        /// 作成日    ：  2019/10/24
        /// </remarks>
        private void GetHeader()
        {
            DataSet dtSet = null;
            DataBase dataBase = null;
            try
            {
                dataBase = new DataBase();
                List<object> paraList = new List<object>();
                string sqlStr = QueryBuild.GetJushihanHeader(JSBDPY_NO, ref paraList);

                dataBase.ConnectDB();
                dtSet = dataBase.GetDataSet(sqlStr, paraList.ToArray());

                DataRow row = dtSet.Tables[0].Rows[0];

                SUBNEGA_NO = row["SUBNEGA_NO"].ToString();
                CMNY = row["CMNY_NM"].ToString();
                LOTSU = row["LOTSU"].ToString();
                HANSU = row["HANSU"].ToString();
                BURANDO_NM = row["BURANDO_NM"].ToString();
                CUSTMER_RNM = row["CUSTMER_RNM"].ToString();
                SYOHIN_NM = row["SYOHIN_NM"].ToString();
                KAN_NM = row["KAN_NM"].ToString();
                ZUMEN_NO = row["ZUMEN_NO"].ToString();
                HAN_KBN = row["HAN_KBN"].ToString();
                COLOR_NM = row["COLOR_NM"].ToString();

                dataBase.DisconnectDB();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (dataBase != null)
                {
                    dataBase.DisconnectDB();
                }
                if (dtSet != null)
                {
                    dtSet.Dispose();
                }
            }
        }

        #endregion
    }
}
