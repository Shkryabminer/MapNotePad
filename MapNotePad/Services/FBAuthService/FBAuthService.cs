using MapNotePad.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Forms;
using Device = Xamarin.Forms.Device;

namespace MapNotePad.Services.FBAuthService
{
    public class FBAuthService : IFBAuthService
    {
        public string Email { get; set; }

        #region --IFBAuthService implementation--

        public async Task<string> GetFBAccauntEmail()
        {
            string clientID = string.Empty;
            string redirectUri = string.Empty;

            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    {
                        clientID = Constants.FacebookClient.AppID;
                        redirectUri = Constants.FacebookClient.FacebookAndroidRedirectUrl;
                        break;
                    }

                case Device.iOS:
                    {
                        clientID = Constants.FacebookClient.AppID;
                        redirectUri = Constants.FacebookClient.FacebookIOSRedirectUrl;
                        break;
                    }
            }

            var authentificator = new OAuth2Authenticator(
                                          clientID,
                                          Constants.FacebookClient.FacebookScope,
                                         new Uri(Constants.FacebookClient.FacebookAuthorizeUrl),
                                         new Uri(redirectUri),
                                         null,
                                         false);

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authentificator);

            authentificator.Completed += Authentificator_Completed;

            return await WaitForEmail();
        }

        #endregion

        #region -- Private helpers --


        private async void Authentificator_Completed(object sender, AuthenticatorCompletedEventArgs e)
        {
            var authentificator = sender as OAuth2Authenticator;

            authentificator.Completed -= Authentificator_Completed;

            if (e.IsAuthenticated)
            {
                string accessToken = e.Account.Properties[Constants.FacebookClient.FacebookAccesTockenKey];

                var prof = await GetFbUSerProfileAsync(accessToken);

                Email = prof.Email;
            }

            else
            {
                Email = string.Empty;
            }
        }

        private async Task<FBProfile> GetFbUSerProfileAsync(string tocken)
        {
            HttpClient client = new HttpClient();

            var response = await client.GetStringAsync(Constants.FacebookClient.FacebookUserInfoUrl + tocken);

            var data = JsonConvert.DeserializeObject<FBProfile>(response);

            return data;
        }

        private async Task<string> WaitForEmail()
        {
           await Task.Run(()=>
           { 
               while (Email==null)
               {
                   
               }
            });
            return Email;
        }
        #endregion
    }
}
