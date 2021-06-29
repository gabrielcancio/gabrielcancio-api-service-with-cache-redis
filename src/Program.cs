using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace api_service_with_cache_redis
{
    class Program
    {
        // static async Task Main(string[] args)
        // {
        //     // Final Test
        //     List<Register> response = await ApiService
        //       .DefineBaseUrl("http://api.portaldatransparencia.gov.br")
        //       .AddHeader("chave-api-dados", "1f644e567ce13bd43ab08bb7871e7739")
        //       .AddResource("api-de-dados")
        //       .AddResource("auxilio-emergencial-beneficiario-por-municipio")
        //       .AddQueryParam("codigoIbge", "2111300")
        //       .AddQueryParam("mesAno", "202008")
        //       .AddQueryParam("pagina", "1")
        //       .Send<List<Register>>();

        //     response.ForEach(delegate (Register register) {
        //         Console.WriteLine(register.valor);
        //     });
        // }

        static async Task Main(string[] args)
        {
            // Stage Test
            string[][] queryParams = new string[2][] {
                new string[] {"codigoIbge", "mesAno", "pagina"},
                new string[] {"2111300", "202008", "1"}
            };

            List<Register> response = await ApiService
              .DefineBaseUrl("http://api.portaldatransparencia.gov.br")
              .AddHeader("chave-api-dados", "1f644e567ce13bd43ab08bb7871e7739")
              .AddResource("api-de-dados")
              .AddResource("auxilio-emergencial-beneficiario-por-municipio")
              .AddQueryParam(queryParams)
              .SendRequestForManyPages<Register>(3, 5, "pagina");

            response.ForEach(delegate (Register register) {
                Console.WriteLine(register.valor);
            });
        }
    }
}
