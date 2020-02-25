﻿using PROGMGMT.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web.Mvc;

namespace PROGMGMT.Models.Seihan
{
    /// <summary>
    /// 検索条件クラス
    /// </summary>
    /// <remarks>
    /// 作成者    ：  nakao
    /// 作成日    ：  2019/09/01
    /// </remarks>
    public class Condition
    {
        #region プロパティ

        [DisplayName("予定日")]
        public string ScheDateFrom { get; set; }

        public string ScheDateTo { get; set; }

        [DisplayName("工程")]
        public string Process { get; set; }

        [DisplayName("作業完了データも出力する")]
        public Boolean OutPut_Chk { get; set; }

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

        [DisplayName("出力期間")]
        public string OutputDateFrom { get; set; }  // CSV出力画面用

        public string OutputDateTo { get; set; }    // CSV出力画面用

        [DisplayName("工程")]
        public string[] OutputProcess { get; set; }    // CSV出力画面用


        public string ConditionErrorMessage { get; set; }   // 条件取得エラー
        public string InputErrorMessage { get; set; }   // 入力エラー
        public SelectList ProcessList { get; set; }     // 工程リスト
        public SelectList KoseiList { get; set; }       // 校正リスト
        public SelectList HanTypeList { get; set; }     // 版式リスト

        #endregion

        #region コンストラクタ

        public Condition()
        {
            ScheDateFrom = DateTime.Now.ToString("yyyy-MM-dd");  // 予定日 (From)：当日
            OutputDateFrom = DateTime.Now.ToString("yyyy-MM-dd");  // 予定日 (From)：当日
            GetProcessList();
            GetKoseiList();
            GetHanTypeList();
        }

        #endregion

        #region メソッド

        /// <summary>
        /// 工程リスト取得
        /// </summary>
        /// <remarks>
        /// 作成者    ：  nakao
        /// 作成日    ：  2019/09/01
        /// </remarks>
        private void GetProcessList()
        {
            DataSet dtSet = null;
            DataBase dataBase = null;
            List<SelectListItem> list = new List<SelectListItem>();
            try
            {
                dataBase = new DataBase();
                string sqlStr = QueryBuild.GetProcess(Constants.PRODUCT_SEIHAN);

                dataBase.ConnectDB();
                dtSet = dataBase.GetDataSet(sqlStr, null);


                foreach (DataRow row in dtSet.Tables[0].Rows)
                {
                    list.Add(new SelectListItem
                    {
                        Text = row["PROCESS_NM"].ToString(),
                        Value = row["PROCESS_CD"].ToString()
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
                ProcessList = new SelectList(list, "Value", "Text");
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
        /// 校正リスト取得
        /// </summary>
        /// <remarks>
        /// 作成者    ：  nakao
        /// 作成日    ：  2019/09/01
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
        /// 作成日    ：  2019/09/01
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
        /// 作成日    ：  2019/09/01
        /// </remarks>
        public bool ValidateSearch()
        {
            InputErrorMessage = Utilities.CheckDateFromTo(ScheDateFrom, ScheDateTo, "予定日");
            return string.IsNullOrEmpty(InputErrorMessage);
        }

        /// <summary>
        /// CSV出力 入力チェック
        /// </summary>
        /// <returns>True=入力OK、False=入力エラー</returns>
        /// <remarks>
        /// 作成者    ：  nakao
        /// 作成日    ：  2019/09/01
        /// </remarks>
        public bool ValidateOutput()
        {
            InputErrorMessage = Utilities.CheckDateFromTo(OutputDateFrom, OutputDateTo, "出力期間");
            return string.IsNullOrEmpty(InputErrorMessage);
        }

        #endregion
    }
}