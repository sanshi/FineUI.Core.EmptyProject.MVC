using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace FineUI.Core.EmptyProject.MVC.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Hello()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult btnHello_Click()
        {
            Alert.Show("你好 FineUI！", MessageBoxIcon.Warning);

            return UIHelper.Result();
        }



        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult btnLogin_Click(string tbxUserName, string tbxPassword)
        {
            if (tbxUserName == "admin" && tbxPassword == "admin")
            {
                ShowNotify("成功登录！", MessageBoxIcon.Success);
            }
            else
            {
                ShowNotify("用户名或密码错误！", MessageBoxIcon.Error);
            }

            return UIHelper.Result();
        }


        // GET: Themes
        public IActionResult Themes()
        {
            return View();
        }

        // GET: Error
        public IActionResult Error()
        {
            return View();
        }
    }
}