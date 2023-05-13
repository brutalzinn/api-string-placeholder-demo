using ApiPlaceHolderDemo.Services;

namespace ApiPlaceHolderDemo.Models.Response
{
    public class HealthCheck
    {
        public string LastDeploy { get; set; }
        public string UpTime { get; set; }
        public string Environment { get; set; }
    }
}
