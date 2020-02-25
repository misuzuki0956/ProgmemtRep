using PROGMGMT.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace PROGMGMT.Models.Jushihan
{
    /// <summary>
    /// ������ʕ\�����f��
    /// </summary>
    /// <remarks>
    /// �쐬��    �F  kawana
    /// �쐬��    �F  2019/10/04
    /// </remarks> 
    /// 
    public class SearchViewModel
    {
        #region �v���p�e�B

        public List<SearchResult> SearchResults { get; set; }

        public Condition Condition { get; set; }
        [DisplayName("")]

        public string ResultCount { get; set; }

        public string SearchErrorMessage { get; set; }
        #endregion

        #region �R���X�g���N�^

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

        #region ���\�b�h

        /// <summary>
        /// �������ʎ擾
        /// </summary>
        /// <remarks>
        /// �쐬��    �F  kawana
        /// �쐬��    �F  2019/10/04
        /// </remarks> 
        public void GetSearchResults()
        {
            // ������������������Ό������{
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
                string queryStr = QueryBuild.GetJushihanSearch(Condition, ref paraList);

                dataBase = new DataBase();
                dataBase.ConnectDB();

                dtSet = dataBase.GetDataSet(queryStr, paraList.ToArray());  // �N�G�����s
                totalCount = dtSet.Tables[0].Rows.Count;                    // �S�̌���

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