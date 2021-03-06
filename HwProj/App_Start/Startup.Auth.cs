﻿using System;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using HwProj.Models;
using HwProj.Models.Contexts;
using HwProj.Models.Repositories;
using HwProj.Models.Roles;
using Microsoft.Owin.Security.Google;
using Owin.Security.Providers.GitHub;
//TODO:
// вынести одинаковые строки в константы
namespace HwProj
{
	//TODO: фикс уведомлений домашка, фикс уведомлений изменении ролей
    public partial class Startup
    {
        // Дополнительные сведения о настройке аутентификации см. на странице https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
			// Настройка контекста базы данных, диспетчера пользователей и диспетчера входа для использования одного экземпляра на запрос
			app.CreatePerOwinContext(AppDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
	        app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

			// Включение использования файла cookie, в котором приложение может хранить информацию для пользователя, выполнившего вход,
			// и использование файла cookie для временного хранения информации о входах пользователя с помощью стороннего поставщика входа
			// Настройка файла cookie для входа
			app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Позволяет приложению проверять метку безопасности при входе пользователя.
                    // Эта функция безопасности используется, когда вы меняете пароль или добавляете внешнее имя входа в свою учетную запись.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, User>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
	
                }
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Позволяет приложению временно хранить информацию о пользователе, пока проверяется второй фактор двухфакторной проверки подлинности.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Позволяет приложению запомнить второй фактор проверки имени входа. Например, это может быть телефон или почта.
            // Если выбрать этот параметр, то на устройстве, с помощью которого вы входите, будет сохранен второй шаг проверки при входе.
            // Точно так же действует параметр RememberMe при входе.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

			// Раскомментируйте приведенные далее строки, чтобы включить вход с помощью сторонних поставщиков входа
			//app.UseMicrosoftAccountAuthentication(
			//    clientId: "",
			//    clientSecret: "");

			//app.UseTwitterAuthentication(
			//   consumerKey: "",
			//   consumerSecret: "");

			//app.UseFacebookAuthentication(
			//   appId: "",
			//   appSecret: "");

			app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
			{
			    ClientId = "822435394647-2r22d16i8nrfgsj4eoin5io7ir1n0u58.apps.googleusercontent.com",
			    ClientSecret = "vMR0V7d9VgQ1T8ORQ_yoPJk2"
			});

	        app.UseVkontakteAuthentication("6413355", "uo12VNcp8qK5Wc7cpXXW", "email");

            app.UseGitHubAuthentication(new GitHubAuthenticationOptions()
			{
				ClientId = "05f28aee6fc34fa4be32",
				ClientSecret = "15e227ae3fcd20a08dc3a533735a9b477e6e4cce",
				Scope = { "gist", "repo" },
			Provider = new GitHubAuthenticationProvider()
			{
				OnAuthenticated = context =>
				{
					context.Identity.AddClaim(new Claim("GitHubAccessToken", context.AccessToken));
					return System.Threading.Tasks.Task.CompletedTask;
				}
			}});
        }
	}
}