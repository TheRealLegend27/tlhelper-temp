using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TLHelper.Settings;

namespace TLHelper.API
{
    public class Users
    {

        public class Response
        {
#pragma warning disable IDE1006 // Benennungsstile
            public string errorCode { get; set; }
            public string errorMsg { get; set; }
            public bool error { get; set; }
            public string response { get; set; }
#pragma warning restore IDE1006 // Benennungsstile
        }
        public class UserModel
        {
#pragma warning disable IDE1006 // Benennungsstile
            public string username { get; set; }
            public string type { get; set; }
            public int auth_lvl { get; set; }
#pragma warning restore IDE1006 // Benennungsstile
        }

        public static UserModel CurrentUser;
        private static string _token = "";
        public static string Token {
            get { return _token; }
            set {
                _token = value;
                SettingsManager.SetSetting("ath", _token);
            }
        }

        private static readonly HttpClient client = new HttpClient();

        static Users()
        {
            client.BaseAddress = new Uri("https://tlhelper.fischer-enterprise.de/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<bool> Login(string username, string password)
        {
            Response r = DefaultResponse;
            HttpResponseMessage response = await client.GetAsync($"users/login?username={username}&password={password}");

            if (response.IsSuccessStatusCode)
                r = await response.Content.ReadAsAsync<Response>();

            if (HandleError(r))
            {
                // REQUEST SUCCESS
                Token = r.response;
                return true;
            }
            return false;
        }

        public static async Task<bool> Authenticate()
        {
            if (Token.Length == 0) return false;

            HttpResponseMessage response = await client.GetAsync($"users/authenticate?token={Token}");

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    UserModel user = await response.Content.ReadAsAsync<UserModel>();
                    CurrentUser = user;
                    if (user.auth_lvl >= 1) return true;
                } catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        private static readonly char[] strParts = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
        public static async Task<bool> AuthServer()
        {
            Response r = DefaultResponse;

            int mult = new Random().Next(1, 9);
            string str = mult + "";
            var expect = 0;

            Random randPart = new Random();
            while (str.Length < 20)
            {
                var cr = strParts[randPart.Next(0, strParts.Length - 1)];
                str += cr;
                expect += (int)cr;
            }
            expect *= mult;

            HttpResponseMessage response = await client.GetAsync($"users/auth-server?token={Token}&str={str}");

            if (response.IsSuccessStatusCode)
                r = await response.Content.ReadAsAsync<Response>();

            if (HandleError(r))
            {
                // REQUEST SUCCESS
                var res = int.Parse(r.response);
                if (res == expect) return true;
                MessageBox.Show("Could not authenticate the Server!", "Error!");
            }
            return false;
        }

        private static Response DefaultResponse => new Response()
        {
            error = true,
            errorCode = "tl:connection-failed",
            errorMsg = "connection-failed",
            response = null
        };

        private static bool HandleError(Response r)
        {
            if (r.error)
            {
                MessageBox.Show(r.errorCode + ": " + r.errorMsg, "Error!");
                return false;
            }
            return true;
        }

    }
}
