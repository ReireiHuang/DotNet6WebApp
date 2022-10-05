namespace DotNet6WebApp.Service
{
    public class GoogleCapchaResponse
    {
        public bool success { get; set; }
        public DateTime challenge_ts { get; set; }
        public string hostname { get; set; }
    }
}
