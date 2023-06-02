using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Onbooks_3Ds.Helper
{
    public class FabricaConexao
    {
       
        public static MySqlConnection getConexao(bool Devolopment=true,string conexaoString ="Default")
        {
            if (Devolopment) {
                return new MySqlConnection(
                    Configuration_Development().GetConnectionString(conexaoString));
            }
            else
            {
                return new MySqlConnection(
                   Configuration_Release().GetConnectionString(conexaoString));
            }
        }

        //esse aqui é para pegar as strings de conexão do mysql na parte de desenvolvimento
        private static IConfigurationRoot Configuration_Development()
        {
            IConfigurationBuilder builder =
                new ConfigurationBuilder().SetBasePath(
                    Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Development.json");
            return builder.Build();
        }
        //esse aqui é para pegar as strings de conexão do mysql na parte de quando o site ficar pronto(muito provavel ser o mesmo mas se não for ja esta pronto) 
        private static IConfigurationRoot Configuration_Release()
        {
            IConfigurationBuilder builder =
                new ConfigurationBuilder().SetBasePath(
                    Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Development.json");
            return builder.Build();
        }
    }
}
