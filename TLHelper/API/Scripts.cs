using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using TLHelper.Settings;
using static TLHelper.API.Variables;

namespace TLHelper.API
{
    public class Scripts
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

        public class FullScript
        {
#pragma warning disable IDE1006 // Benennungsstile
            public string id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public string last_updated { get; set; }
            public string status_id { get; set; }
            public string script_type { get; set; }
#pragma warning restore IDE1006 // Benennungsstile
        }

        public class ScriptContent
        {
            public string id { get; set; }
            public string script { get; set; }
            public string script_type { get; set; }
        }

        private static readonly HttpClient client = new HttpClient();

        static Scripts()
        {
            client.BaseAddress = new Uri(Environment_Variables.API_ROOT);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<List<FullScript>> GetOutdatedScripts()
        {
            HttpResponseMessage response = await client.GetAsync($"helper/scripts/list?version={Environment_Variables.CURRENT_VERSION}&license={License}");
            List<FullScript> result = new List<FullScript>();

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    FullScript[] res = response.Content.ReadAsAsync<FullScript[]>().Result;
                    foreach (FullScript script in res)
                    {
                        var file = Environment_Variables.SCRIPTS_DIR + "/" + script.id;
                        if (File.Exists(file + ".tls")) file += ".tls";
                        else if (File.Exists(file + ".ahk-tl")) file += ".ahk-tl";
                        else
                        {
                            result.Add(script);
                            continue;
                        }
                        var lastUpdated = Convert.ToDateTime(script.last_updated);
                        if (File.GetLastWriteTime(file) < lastUpdated)
                        {
                            result.Add(script);
                        }
                    }
                } catch (Exception)
                {
                    return result;
                }
            }
            return result;
        }

        public static async Task<List<string>> GetScriptsToDelete()
        {
            HttpResponseMessage response = await client.GetAsync($"helper/scripts/list?version={Environment_Variables.CURRENT_VERSION}&license={License}");
            List<string> result = new List<string>();

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    FullScript[] res = response.Content.ReadAsAsync<FullScript[]>().Result;
                    foreach (string scriptFile in Directory.GetFiles(Environment_Variables.SCRIPTS_DIR))
                    {
                        string file = Path.GetFileNameWithoutExtension(scriptFile);
                        bool keep = false;
                        foreach (FullScript script in res)
                        {
                            Console.WriteLine(script.id + " => " + file);
                            if (script.id.Equals(file))
                            {
                                keep = true;
                                break;
                            }
                        }
                        if (!keep) result.Add(scriptFile);
                    }
                }
                catch (Exception)
                {
                    return result;
                }
            }
            return result;
        }

        public static async Task<string> DownloadScript(string scriptId)
        {
            HttpResponseMessage response = await client.GetAsync($"helper/scripts/download?version={Environment_Variables.CURRENT_VERSION}&license={License}&script_id={scriptId}");
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    ScriptContent content = response.Content.ReadAsAsync<ScriptContent>().Result;
                    return content.script;
                } catch (Exception)
                {
                    return null;
                }
            }
            return null;
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

    }
}
