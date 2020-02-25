namespace PROGMGMT.Models.Jushihan
{
    public class RegisterViewModel
    {
        /// <summary>
        /// 登録画面表示モデル
        /// </summary>
        /// <remarks>
        /// 作成者    ：  kawana
        /// 作成日    ：  2019/10/24
        /// </remarks>
        #region プロパティ
        public Header Header { get; set; }
        public RegisterGroup RegisterGroup { get; set; }
        public string RegistResultMessage { get; set; }
        #endregion

        #region コンストラクタ
        public RegisterViewModel() { }

        public RegisterViewModel(string dpyno, string process)
        {
            Header = new Header(dpyno);
            RegisterGroup = new RegisterGroup(dpyno, process);
        }

        public RegisterViewModel(string dpyno, string process, bool result)
        {
            Header = new Header(dpyno);
            RegisterGroup = new RegisterGroup(dpyno, process);
            if (result)
            {
                RegistResultMessage = Resources.TextResource.RegistSuccess;
            }
            else
            {
                RegistResultMessage = Resources.TextResource.RegistFailure;
            }
        }

        #endregion
    }
}