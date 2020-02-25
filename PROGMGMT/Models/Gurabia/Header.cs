using PROGMGMT.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace PROGMGMT.Models.Gurabia
{
    /// <summary>
    /// ヘッダ情報クラス
    /// </summary>
    /// <remarks>
    /// 作成者    ：  sesaki
    /// 作成日    ：  2019/11/20
    /// </remarks>
    public class Header
    {
        #region プロパティ

        [DisplayName("得意先")]
        public string CUSTMER_RNM { get; set; }

        [DisplayName("ネガNo")]
        public string SUBNEGA_NO { get; set; }

        [DisplayName("呼出しNo")]
        public string DPY_NO { get; set; }

        [DisplayName("ブランド")]
        public string BURANDO_NM { get; set; }

        [DisplayName("品名")]
        public string SYOHIN_NM { get; set; }

        [DisplayName("規格")]
        public string KAN_NM { get; set; }

        [DisplayName("校正区分")]
        public string KSKBN_NM { get; set; }

        [DisplayName("色名")]
        public string COLOR_NM { get; set; }

        public String CUSTOMER { get; set; }


        #endregion

        #region コンストラクタ

        public Header() { }

        public Header(string dpyno, string customer)
        {
            DPY_NO = dpyno;
            CUSTOMER = customer;
            GetHeader(customer);
        }

        #endregion

        #region メソッド

        /// <summary>
        /// ヘッダ情報取得
        /// </summary>
        /// <param name="customer">得意先コード</param>
        /// <remarks>
        /// 作成者    ：  sesaki
        /// 作成日    ：  2019/11/21
        /// </remarks>
        private void GetHeader(string customer)
        {
            DataSet dtSet = null;
            DataBase dataBase = null;
            try
            {
                dataBase = new DataBase();
                List<object> paraList = new List<object>();
                string sqlStr;
                if (GurabiaCustomer.IsTokan(customer))
                {
                    sqlStr = QueryBuild.GetGurabiaHeaderTokan(DPY_NO, ref paraList);
                }
                else
                {
                    sqlStr = QueryBuild.GetGurabiaHeaderToyo(DPY_NO, ref paraList);
                }

                dataBase.ConnectDB();
                dtSet = dataBase.GetDataSet(sqlStr, paraList.ToArray());

                DataRow row = dtSet.Tables[0].Rows[0];

                CUSTMER_RNM = row["CUSTMER_RNM"].ToString();
                SUBNEGA_NO = row["SUBNEGA_NO"].ToString();
                BURANDO_NM = row["BURANDO_NM"].ToString();
                SYOHIN_NM = row["SYOHIN_NM"].ToString();
                KAN_NM = row["KAN_NM"].ToString();
                KSKBN_NM = row["KSKBN_NM"].ToString();
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