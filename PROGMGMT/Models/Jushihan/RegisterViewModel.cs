namespace PROGMGMT.Models.Jushihan
{
    public class RegisterViewModel
    {
        /// <summary>
        /// �o�^��ʕ\�����f��
        /// </summary>
        /// <remarks>
        /// �쐬��    �F  kawana
        /// �쐬��    �F  2019/10/24
        /// </remarks>
        #region �v���p�e�B
        public Header Header { get; set; }
        public RegisterGroup RegisterGroup { get; set; }
        public string RegistResultMessage { get; set; }
        #endregion

        #region �R���X�g���N�^
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