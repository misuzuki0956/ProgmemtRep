using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PROGMGMT.Models.User;
using PROGMGMT.Common;
using System.Data.Common;
using System.Data;


namespace PROGMGMT.Controllers
{
    /// <summary>
    /// ログイン画面のコントローラ
    /// </summary>
    /// <remarks>
    /// 作成者    ：  sesaki
    /// 作成日    ：  2019/10/29
    /// </remarks>

    public class UserController : Controller
    {
        log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ActionResult Index()
        {
            return RedirectToAction("Login", "User");
        }

        #region ログイン画面
        [HttpGet]
        public ActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Login(Condition condition)
        {
            try
            {
                LoginUser LoginUser = new LoginUser();
                DataRow data = LoginUser.GetLoginUser(condition);

                if (data != null)
                {
                    // セッションへの格納
                    Session["GroupCode"] = data["PROCESS_CD"].ToString();
                    Session["UserId"] = data["EMPLOYEE_CD"].ToString();
                    condition.PROCESS_CD = data["PROCESS_CD"].ToString();
                    return View("Menu", condition);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, Resources.TextResource.ErrorLogin);
                    return View(condition);
                }
            }
            catch (Exception ex)
            {
                logger.Debug("LOGIN ERROR: " + ex.ToString());
                ModelState.AddModelError(string.Empty, Resources.TextResource.ErrorGetUser);
                return View(condition);
            }
            finally
            {

            }
        }
        #endregion
    }
}
