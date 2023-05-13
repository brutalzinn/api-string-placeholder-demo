using System;

namespace ApiPlaceHolderDemo.Services
{
    public class ApiCicloDeVida
    {
        public DateTime iniciouEm { get; set; }
        public ApiCicloDeVida()
        {
            iniciouEm = DateTime.Now;
        }
    }
}
