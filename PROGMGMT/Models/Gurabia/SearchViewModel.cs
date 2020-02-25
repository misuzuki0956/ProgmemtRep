using PROGMGMT.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace PROGMGMT.Models.Gurabia
{
    /// <summary>
    /// 検索画面表示モデル
    /// </summary>
    /// <remarks>
    /// 作成者    ：  sesaki
    /// 作成日    ：  2019/11/13
    /// </remarks> 
    public class SearchViewModel
    {
        #region プロパティ

        public List<SearchResult> SearchResults { get; set; }

        public Condition Condition { get; set; }

        [DisplayName("件数")]
        public string ResultCount { get; set; }

        public string SearchErrorMessage { get; set; }
        #endregion

        #region コンストラクタ
        public SearchViewModel()
        {
            Condition = new Condition();
            ResultCount = "";
        }
        public SearchViewModel(Condition con)
        {
            Condition = con;
            ResultCount = "";
        }

        #endregion

        #region メソッド

        /// <summary>
        /// 検索結果取得
        /// </summary>
        /// <remarks>
        /// 作成者    ：  sesaki
        /// 作成日    ：  2019/11/13
        /// </remarks> 
        public void GetSearchResults()
        {
            // 検索条件が正しければ検索実施
            if (Condition.ValidateSearch() == false)
            {
                return;
            }

            DataSet dtSet = null;
            DataBase dataBase = null;


            try
            {
                long totalCount = 0;
                SearchResults = new List<SearchResult>();
                List<object> paraList = new List<object>();

                // 得意先：東罐興業
                if (GurabiaCustomer.IsTokan(Condition.Customer))
                {
                    string queryStr = QueryBuild.GetGurabiaSearchTokan(Condition, ref paraList);

                    dataBase = new DataBase();
                    dataBase.ConnectDB();

                    dtSet = dataBase.GetDataSet(queryStr, paraList.ToArray());  // クエリ実行
                    totalCount = dtSet.Tables[0].Rows.Count;                    // 全体件数

                    foreach (DataRow row in dtSet.Tables[0].Rows)
                    {
                        SearchResult sr = new SearchResult(row);
                        if (Condition.OutPut_Chk || sr.CheckOutPut(Condition.Process))
                        {
                            SearchResults.Add(sr);
                        }
                    }

                    ResultCount = SearchResults.Count.ToString() + "/" + totalCount.ToString();

                    dataBase.DisconnectDB();
                }

                // 得意先：東洋製罐
                else
                {
                    string queryStr = QueryBuild.GetGurabiaSearchToyo(Condition, ref paraList);

                    dataBase = new DataBase();
                    dataBase.ConnectDB();

                    dtSet = dataBase.GetDataSet(queryStr, paraList.ToArray());  // クエリ実行
                    totalCount = dtSet.Tables[0].Rows.Count;                    // 全体件数

                    foreach (DataRow row in dtSet.Tables[0].Rows)
                    {
                        SearchResult sr = new SearchResult(row);
                        if (Condition.OutPut_Chk || sr.CheckOutPut(Condition.Process))
                        {
                            SearchResults.Add(sr);
                        }
                    }

                    ResultCount = SearchResults.Count.ToString() + "/" + totalCount.ToString();

                    dataBase.DisconnectDB();
                }
            }
            catch (Exception ex)
            {
                SearchErrorMessage = Resources.TextResource.ErrorGetSearchResult;
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