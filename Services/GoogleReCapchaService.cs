using Microsoft.Extensions.Options;
using static System.Net.WebRequestMethods;
using System.Net;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using DotNet6WebApp.Business;

namespace DotNet6WebApp.Service
{
    public class GoogleReCaptchaService
    {
        private readonly GoogleCaptchaConfig _config;
        private readonly SQL_TestContext _context;
        public GoogleReCaptchaService(SQL_TestContext context)
        {
            _context = context;

            _config = new GoogleCaptchaConfig()
            {
                SecretKey = context.DbconfigSetting.Where(x=>x.Id == 2).FirstOrDefault().Value ,
                SiteKey = context.DbconfigSetting.Where(x => x.Id == 1).FirstOrDefault().Value
            };
        }
        public async Task<bool> VerifyToken(string token)
        {

            try {
                var url = $"https://www.google.com/recaptcha/api/siteverify?secret={_config.SecretKey}&response={token}";
                using (var client = new HttpClient()) 
                {
                    var httpResult = await client.GetAsync(url);
                    if (httpResult.StatusCode != HttpStatusCode.OK)
                    {
                        return false;
                    }

                    var responseString = await httpResult.Content.ReadAsStringAsync();

                    var googleResult = JsonConvert.DeserializeObject<GoogleCapchaResponse>(responseString);

                    return googleResult.success;
                }
            }
            catch (Exception ex) {
                return false;
            }
        }
    }
}
