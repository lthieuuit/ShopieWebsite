using Facebook;
using Model.Dao;
using Model.EF;
using Shopie.Common;
using Shopie.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shopie.Controllers
{
    public class UserController : Controller
    {
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (dao.CheckUserName(model.UserName))
                {
                    ModelState.AddModelError("", "Username already used");
                }
                else if (dao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email already used");
                }
                else
                {
                    var user = new User();
                    user.UserName = model.UserName;
                    user.Name = model.Name;
                    user.Password = model.Password;
                    user.Phone = model.Phone;
                    user.Email = model.Email;
                    user.Address = model.Address;
                    user.CreateDate = DateTime.Now;
                    user.Status = true;
                    user.Permission = 2;
                    var encryptedPW = Encrypt.MD5Hash(user.Password);
                    user.Password = encryptedPW;
                    var result = dao.Insert(user);
                    if (result > 0)
                    {
                        ViewBag.Success = "Sign Up complete";
                        model = new RegisterModel();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Somethong wrong, try again later!");
                    }
                }
            }
            return View(model);
        }

        public ActionResult Login()
        {

            return View();
        }
            
        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppID"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",

            });

            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppID"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });
            var accessToken = result.access_token;
            if(!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                // Get the user's information like email, first name, middle name etc
                dynamic me = fb.Get("me?field=first_name,middle_name,last_name,id,email,phone");
                string email = me.email;
                string userName = me.email;
                string firstname = me.first_name;
                string middlename = me.middle_name;
                string lastname = me.last_name;
                string phone = me.phone;

                var user = new User();
                user.Email = email;
                user.UserName = email;
                user.Status = true;
                user.Permission = 1;
                user.Name = firstname + " " + middlename + " " + lastname;
                user.CreateDate = DateTime.Now;
                var resultInsert = new UserDao().InsertForFacebook(user);
                
                if(resultInsert > 0)
                {
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.ID;
                    userSession.UserEmail = user.Email;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    
                }
            }
            return Redirect("/");
        }

        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/");
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName, Encrypt.MD5Hash(model.Password));
                if (result == 1)
                {
                    var user = dao.GetByID(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.ID;
                    userSession.UserEmail = user.Email;
                    Session.Add(CommonConstants.USER_SESSION, userSession);

                    return Redirect("/");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "You need to Sign up");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Account is Blocked!");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Wrong password");
                }
                else
                {
                    ModelState.AddModelError("", "Unknown Login Session!");

                }
            }

            return View(model);
        }

    }
}