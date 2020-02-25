namespace PROGMGMT.Models.Seihan
{
    /// <summary>
    /// 登録画面表示モデル
    /// </summary>
    /// <remarks>
    /// 作成者    ：  nakao
    /// 作成日    ：  2019/09/01
    /// </remarks>
    public class RegisterViewModel
    {
        #region プロパティ
        public Header Header { get; set; }
        public RegisterGroup RegisterGroup { get; set; }
        public string RegistResultMessage { get; set; }

        public bool RegistResult { get; }
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
            RegistResult = result;
        }

        #endregion
    }
}