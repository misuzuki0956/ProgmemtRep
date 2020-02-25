using PROGMGMT.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace PROGMGMT.Models.SeihanList
{
    /// <summary>
    /// 検索画面表示モデル
    /// </summary>
    /// <remarks>
    /// 作成者    ：  nakao
    /// 作成日    ：  2019/11/08
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
        /// 作成者    ：  nakao
        /// 作成日    ：  2019/11/08
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
                long unfinishedCount = 0;
                SearchResults = new List<SearchResult>();
                List<object> paraList = new List<object>();
                string queryStr = QueryBuild.GetSeihanMgmtList(Condition, ref paraList);

                dataBase = new DataBase();
                dataBase.ConnectDB();

                dtSet = dataBase.GetDataSet(queryStr, paraList.ToArray());  // クエリ実行

                foreach (DataRow row in dtSet.Tables[0].Rows)
                {
                    SearchResult sr = new SearchResult(row);
                    SearchResults.Add(sr);

                    if (sr.IsUnfinished()) unfinishedCount++;   // 未完了集計
                }

                ResultCount = unfinishedCount.ToString() + "/" + SearchResults.Count.ToString();

                dataBase.DisconnectDB();
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