using PROGMGMT.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace PROGMGMT.Models.Sappan
{
    /// <summary>
    /// ヘッダ情報クラス
    /// </summary>
    /// <remarks>
    /// 作成者    ：  sesaki
    /// 作成日    ：  2019/09/24
    /// </remarks>
    public class Header
    {
        #region プロパティ

        [DisplayName("ネガNo")]
        public string SUBNEGA_NO { get; set; }

        [DisplayName("呼出しNo")]
        public string SPDPY_NO { get; set; }

        [DisplayName("注文内容")]
        public string CMNY_NM { get; set; }

        [DisplayName("ロット数")]
        public string LOTSU { get; set; }

        [DisplayName("版数")]
        public string HANSU { get; set; }

        [DisplayName("ブランド")]
        public string BURANDO_NM { get; set; }

        [DisplayName("発送先")]
        public string CUSTMER_RNM { get; set; }

        [DisplayName("品名")]
        public string SYOHIN_NM { get; set; }

        [DisplayName("容器型")]
        public string KAN_NM { get; set; }

        [DisplayName("図面No")]
        public string ZUMEN_NO { get; set; }

        [DisplayName("色名")]
        public string COLOR_NM { get; set; }


        #endregion

        #region コンストラクタ

        public Header() { }

        public Header(string dpyno)
        {
            SPDPY_NO = dpyno;
            GetHeader();
        }

        #endregion

        #region メソッド

        /// <summary>
        /// ヘッダ情報取得
        /// </summary>
        /// <remarks>
        /// 作成者    ：  sesaki
        /// 作成日    ：  2019/09/24
        /// </remarks> 
        private void GetHeader()
        {
            DataSet dtSet = null;
            DataBase dataBase = null;
            try
            {
                dataBase = new DataBase();
                List<object> paraList = new List<object>();
                string sqlStr = QueryBuild.GetSappanHeader(SPDPY_NO, ref paraList);

                dataBase.ConnectDB();
                dtSet = dataBase.GetDataSet(sqlStr, paraList.ToArray());

                DataRow row = dtSet.Tables[0].Rows[0];

                SUBNEGA_NO = row["SUBNEGA_NO"].ToString();
                CMNY_NM = row["CMNY_NM"].ToString();
                LOTSU = row["LOTSU"].ToString();
                HANSU = row["HANSU"].ToString();
                BURANDO_NM = row["BURANDO_NM"].ToString();
                CUSTMER_RNM = row["CUSTMER_RNM"].ToString();
                SYOHIN_NM = row["SYOHIN_NM"].ToString();
                KAN_NM = row["KAN_NM"].ToString();
                ZUMEN_NO = row["ZUMEN_NO"].ToString();
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