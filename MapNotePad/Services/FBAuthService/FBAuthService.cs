using Acr.UserDialogs;
using MapNotePad.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Forms;
using Device = Xamarin.Forms.Device;

namespace MapNotePad.Services.FBAuthService
{
    public class FBAuthService : IFBAuthService
    {
        public FaceBookProfile _fbProfile { get; set; }

        #region --IFBAuthService implementation--

        public async Task<FaceBookProfile> GetFBAccauntEmail(CancellationTokenSource cts)
        {
            string clientID = Constants.FacebookClient.AppID; ;
            string redirectUri = Constants.FacebookClient.FacebookRedirectUrl; 
            bool isUsingNativeUI = false;            

            var authentificator = new OAuth2Authenticator(
                                          clientID,
                                          Constants.FacebookClient.FacebookScope,
                                         new Uri(Constants.FacebookClient.FacebookAuthorizeUrl),
                                         new Uri(redirectUri),
                                         isUsingNativeUI: isUsingNativeUI);

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();

            try
            {               
                {
                    presenter.Login(authentificator);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            authentificator.Completed += Authentificator_Completed;

            return await WaitForEmail(cts.Token);
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

                _fbProfile = prof;
            }

            else
            {
                _fbProfile = null;
            }
        }

        private async Task<FaceBookProfile> GetFbUSerProfileAsync(string tocken)
        {
            HttpClient client = new HttpClient();

            var response = await client.GetStringAsync(Constants.FacebookClient.FacebookUserInfoUrl + tocken);

            var data = JsonConvert.DeserializeObject<FaceBookProfile>(response);

            return data;
        }

        private async Task<FaceBookProfile> WaitForEmail(CancellationToken cts)
        {
           await Task.Run(()=>
           { 
               while (!cts.IsCancellationRequested && _fbProfile==null)
               {                   
               }
           });
            return _fbProfile;
        }
        #endregion
    }
}
