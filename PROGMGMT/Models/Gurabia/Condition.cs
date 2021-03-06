﻿using PROGMGMT.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web.Mvc;

namespace PROGMGMT.Models.Gurabia
{
    /// <summary>
    /// 検索条件クラス
    /// </summary>
    /// <remarks>
    /// 作成者    ：  sesaki
    /// 作成日    ：  2019/11/14
    /// </remarks>
    public class Condition
    {
        #region プロパティ

        [DisplayName("予定日")]
        public string ScheDateFrom { get; set; }

        public string ScheDateTo { get; set; }

        [DisplayName("工程")]
        public string Process { get; set; }

        [DisplayName("得意先")]
        public string Customer { get; set; }

        [DisplayName("校正区分")]
        public string[] Kosei_Kbn { get; set; }
        public string[] Tokan_Kosei_Kbn { get; set; }

        [DisplayName("ネガNo")]
        public string Nega_No { get; set; }

        [DisplayName("呼出しNo")]
        public string Dpy_No { get; set; }

        [DisplayName("作業完了データも出力する")]
        public Boolean OutPut_Chk { get; set; }

        [DisplayName("出力期間")]
        public string OutputDateFrom { get; set; }  // CSV出力画面用

        public string OutputDateTo { get; set; }    // CSV出力画面用

        [DisplayName("工程")]
        public string[] OutputProcess { get; set; }    // CSV出力画面用

        public string ConditionErrorMessage { get; set; }   // 条件取得エラー
        public string InputErrorMessage { get; set; }   // 入力エラー
        public SelectList ProcessList { get; set; }     // 工程リスト
        public SelectList CustomerList { get; set; }     // 得意先リスト
        public SelectList KoseiList { get; set; }        // 校正区分リスト
        public SelectList TokanKoseiList { get; set; }        // 東罐興業校正区分リスト

        #endregion

        #region コンストラクタ
        public Condition()
        {
            ScheDateFrom = DateTime.Now.ToString("yyyy-MM-dd");  // 予定日 (From)：当日
            OutputDateFrom = DateTime.Now.ToString("yyyy-MM-dd");  // 予定日 (From)：当日
            GetProcessList();
            GetCustomerList();
            GetKoseiList();
            GetTokanKoseiList();
        }

        #endregion

        #region メソッド

        /// <summary>
        /// 工程リスト取得
        /// </summary>
        /// <remarks>
        /// 作成者    ：  sesaki
        /// 作成日    ：  2019/11/13
        /// </remarks>
        private void GetProcessList()
        {
            DataSet dtSet = null;
            DataBase dataBase = null;
            List<SelectListItem> list = new List<SelectListItem>();
            try
            {
                dataBase = new DataBase();
                string sqlStr = QueryBuild.GetProcess(Constants.PRODUCT_GURABIA);

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
        /// 得意先リスト
        /// </summary>
        /// <remarks>
        /// 作成者    ：  sesaki
        /// 作成日    ：  2019/11/13
        /// </remarks>
        private void GetCustomerList()
        {
            CustomerList = GurabiaCustomer.MakeList();
        }

        /// <summary>
        /// 校正区分リスト取得
        /// </summary>
        /// <remarks>
        /// 作成者    ：  sesaki
        /// 作成日    ：  2019/11/13
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
        /// 東罐興業校正区分リスト取得
        /// </summary>
        /// <remarks>
        /// 作成者    ：  nakao
        /// 作成日    ：  2019/12/06
        /// </remarks>
        private void GetTokanKoseiList()
        {
            DataSet dtSet = null;
            DataBase dataBase = null;
            List<SelectListItem> list = new List<SelectListItem>();
            try
            {

                dataBase = new DataBase();
                string sqlStr = QueryBuild.GetTokanKoseiKubun();

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
                TokanKoseiList = new SelectList(list, "Value", "Text");
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
        /// 作成者    ：  sesaki
        /// 作成日    ：  2019/11/13
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
        /// 作成者    ：  sesaki
        /// 作成日    ：  2019/11/28
        /// </remarks>
        public bool ValidateOutput()
        {
            InputErrorMessage = Utilities.CheckDateFromTo(OutputDateFrom, OutputDateTo, "出力期間");
            return string.IsNullOrEmpty(InputErrorMessage);
        }
        #endregion
    }
}