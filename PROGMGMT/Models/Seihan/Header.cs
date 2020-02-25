using PROGMGMT.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace PROGMGMT.Models.Seihan
{
    /// <summary>
    /// ヘッダ情報クラス
    /// </summary>
    /// <remarks>
    /// 作成者    ：  nakao
    /// 作成日    ：  2019/09/01
    /// </remarks>
    public class Header
    {
        #region プロパティ

        [DisplayName("ネガNo")]
        public string SUBNEGA_NO { get; set; }

        [DisplayName("呼出しNo")]
        public string DPY_NO { get; set; }

        [DisplayName("注文内容")]
        public string CMNY { get; set; }

        [DisplayName("注文区分")]
        public string CMKBN { get; set; }

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

        [DisplayName("色名")]
        public string COLOR_NM { get; set; }

        [DisplayName("校正区分")]
        public string KSKBN { get; set; }

        [DisplayName("校正材")]
        public string KSZAI { get; set; }

        [DisplayName("校正刷部数")]
        public string KSBUSU { get; set; }

        #endregion

        #region コンストラクタ

        public Header() { }

        public Header(string dpyno)
        {
            DPY_NO = dpyno;
            GetHeader();
        }

        #endregion

        #region メソッド

        /// <summary>
        /// ヘッダ情報取得
        /// </summary>
        /// <remarks>
        /// 作成者    ：  nakao
        /// 作成日    ：  2019/09/01
        /// </remarks>
        private void GetHeader()
        {
            DataSet dtSet = null;
            DataBase dataBase = null;
            try
            {
                dataBase = new DataBase();
                List<object> paraList = new List<object>();
                string sqlStr = QueryBuild.GetSeihanHeader(DPY_NO, ref paraList);

                dataBase.ConnectDB();
                dtSet = dataBase.GetDataSet(sqlStr, paraList.ToArray());

                DataRow row = dtSet.Tables[0].Rows[0];

                SUBNEGA_NO = row["SUBNEGA_NO"].ToString();
                CMNY = row["CMNY_NM"].ToString();
                CMKBN = row["CMKBN_NM"].ToString();
                BURANDO_NM = row["BURANDO_NM"].ToString();
                SYOHIN_NM = row["SYOHIN_NM"].ToString();
                KAN_NM = row["KAN_NM"].ToString();
                ZUMEN_NO = row["ZUMEN_NO"].ToString();
                HAN_KBN = row["HAN_KBN"].ToString();
                COLOR_NM = row["COLOR_NM"].ToString();
                KSKBN = row["KSKBN_NM"].ToString();
                KSZAI = row["KOSEIZAI"].ToString();
                KSBUSU = row["GOKEI_CT"].ToString();

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