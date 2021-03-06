﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROGMGMT.Models.Sappan
{
    /// <summary>
    /// 登録画面表示モデル
    /// </summary>
    /// <remarks>
    /// 作成者    ：  sesaki
    /// 作成日    ：  2019/09/24
    /// </remarks> 
    public class RegisterViewModel
    {
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