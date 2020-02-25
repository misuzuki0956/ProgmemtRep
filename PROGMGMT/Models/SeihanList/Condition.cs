using PROGMGMT.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web.Mvc;

namespace PROGMGMT.Models.SeihanList
{
    /// <summary>
    /// 検索条件クラス
    /// </summary>
    /// <remarks>
    /// 作成者    ：  nakao
    /// 作成日    ：  2019/11/08
    /// </remarks>
    public class Condition
    {
        #region プロパティ

        [DisplayName("出荷日")]
        public string YoteiDayFrom { get; set; }

        public string YoteiDayTo { get; set; }

        [DisplayName("校正区分")]
        public string[] Kosei_Kbn { get; set; }

        [DisplayName("版式")]
        public string[] Han_Type { get; set; }

        [DisplayName("ネガNo")]
        public string Nega_No { get; set; }

        [DisplayName("呼出しNo")]
        public string Dpy_No { get; set; }

        [DisplayName("品名")]
        public string Shohin_Name { get; set; }


        public string ConditionErrorMessage { get; set; }   // 条件取得エラー
        public string InputErrorMessage { get; set; }   // 入力エラー
        public SelectList KoseiList { get; set; }       // 校正リスト
        public SelectList HanTypeList { get; set; }     // 版式リスト

        #endregion

        #region コンストラクタ

        public Condition()
        {
            YoteiDayFrom = DateTime.Now.ToString("yyyy-MM-dd");  // 出荷日 (From)：当日
            GetKoseiList();
            GetHanTypeList();
        }

        #endregion

        #region メソッド

        /// <summary>
        /// 校正リスト取得
        /// </summary>
        /// <remarks>
        /// 作成者    ：  nakao
        /// 作成日    ：  2019/11/08
        /// </remarks>
        private void GetKoseiList()
        {
            DataSet dtSet = null;
            DataBase dataBase = null;
            List<SelectListItem> list = new List<SelectListItem>();
            try
            {
                dataBase = new DataBase();
                string sqlStr = QueryBuild.GetKoseiKubun();

                dataBase.ConnectDB();
                dtSet = dataBase.GetDataSet(sqlStr, null);

                foreach (DataRow row in dtSet.Tables[0].Rows)
                {
                    list.Add(new SelectListItem
                    {
                        Text = row["KSKBN_NM"].ToString(),
                        Value = row["KSKBN_CD"].ToString()
                    });
                }

                dataBase.DisconnectDB();
            }
            catch (Exception ex)
            {
                ConditionErrorMessage = Resources.TextResource.ErrorGetCondition;
            }
            finally
            {
                KoseiList = new SelectList(list, "Value", "Text");
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

        /// <summary>
        /// 版式リスト取得
        /// </summary>
        /// <remarks>
        /// 作成者    ：  nakao
        /// 作成日    ：  2019/11/08
        /// </remarks>
        private void GetHanTypeList()
        {
            DataSet dtSet = null;
            DataBase dataBase = null;
            List<SelectListItem> list = new List<SelectListItem>();
            try
            {

                dataBase = new DataBase();
                string sqlStr = QueryBuild.GetHanType();

                dataBase.ConnectDB();
                dtSet = dataBase.GetDataSet(sqlStr, null);

                foreach (DataRow row in dtSet.Tables[0].Rows)
                {
                    list.Add(new SelectListItem
                    {
                        Text = row["HANSKI_NM"].ToString(),
                        Value = row["HAN_KBN"].ToString()
                    });
                }

                dataBase.DisconnectDB();
            }
            catch (Exception ex)
            {
                ConditionErrorMessage = Resources.TextResource.ErrorGetCondition;
            }
            finally
            {
                HanTypeList = new SelectList(list, "Value", "Text");
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

        /// <summary>
        /// 検索 入力チェック
        /// </summary>
        /// <returns>True=入力OK、False=入力エラー</returns>
        /// <remarks>
        /// 作成者    ：  nakao
        /// 作成日    ：  2019/11/08
        /// </remarks>
        public bool ValidateSearch()
        {
            InputErrorMessage = Utilities.CheckDateFromTo(YoteiDayFrom, YoteiDayTo, "出荷日");
            return string.IsNullOrEmpty(InputErrorMessage);
        }

        #endregion
    }
}