using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TLHelper.API.Variables;

namespace TLHelper.API
{
    public class Users
    {

#pragma warning disable IDE1006 // Benennungsstile
        public class Response
        {
            public string errorCode { get; set; }
            public string errorMsg { get; set; }
            public bool error { get; set; }
            public string response { get; set; }
        }

        public class AuthResultResponseModel
        {
            public string user { get; set; }
            public string license_type { get; set; }
            public string errorCode { get; set; }
            public string errorMsg { get; set; }
            public bool error { get; set; } = false;
            public string response { get; set; }
        }

        public class AuthResultModel
        {
            public string user { get; set; }
            public string license_type { get; set; }
        }
#pragma warning restore IDE1006 // Benennungsstile

        public static AuthResultModel AuthResult;

        private static readonly HttpClient client = new HttpClient();

        static Users()
        {
            client.BaseAddress = new Uri(EnvironmentVariables.API_ROOT);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<bool> AuthLicense()
        {
            HttpResponseMessage response = await client.GetAsync($"helper/authenticate?version={EnvironmentVariables.CURRENT_VERSION}&license={License}");

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    AuthResult = await response.Content.ReadAsAsync<AuthResultModel>();
                    if (AuthResult.user == null) return false;
                    return true;
                }
                catch (Exception)
                {
                    HandleError(response.Content.ReadAsAsync<Response>().Result);
                    return false;
                }
            }
            HandleError(DefaultResponse);
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
                expect += cr;
            }
            expect *= mult;

            HttpResponseMessage response = await client.GetAsync($"users/auth-server?version={EnvironmentVariables.CURRENT_VERSION}&str={str}");

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
            errorMsg = "Connection to the server failed",
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

        private static bool IsError(Response r) => r.error || false;

    }
}
