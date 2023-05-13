using ApiPlaceHolderDemo.Integrations.ApiPlaceHolderDemo;

namespace ApiPlaceHolderDemo.Models.Settings
{
    public class ApiConfig
    {
        public bool Swagger { get; set; }
        public string CorsOrigin { get; set; }
        public Authorization Authorization { get; set; }
        public Integrations Integrations { get; set; }
    }

    public class Authorization
    {
        public bool Activate { get; set; }
        public string ApiHeader { get; set; }
        public string ApiKey { get; set; }
    }

    public class Integrations
    {
        public ApiApiPlaceHolderDemoConfig ApiApiPlaceHolderDemoConfig { get; set; }
    }

}