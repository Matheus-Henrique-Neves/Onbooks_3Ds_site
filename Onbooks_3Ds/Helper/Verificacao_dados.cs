using CadUsu3dsN.Helper;
using MySql.Data.MySqlClient;
using System;

namespace Onbooks_3Ds.Helper
{
    public static class Verificacao_dados
    {
         static MySqlConnection conexao = FabricaConexao.getConexao(true,"Casa");
        public static bool VarificaraCPF(this string cpf) 
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };


            if (cpf.Length != 11)
                return false;

            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    return false;

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);


        }
        public static string VarificarLogin(string email, string senha)
        {
            string senhacripitografada = senha.GerarHash();
            try {
                conexao.Open();
                MySqlCommand qyr = new MySqlCommand("SELECT INTO usuarios WHERE email=@email AND senha=@senha", conexao);
                qyr.Parameters.AddWithValue("@email", email);
                qyr.Parameters.AddWithValue("@senha", senhacripitografada);
                MySqlDataReader resultado = qyr.ExecuteReader();
                if (resultado.Read()) {
                    return "Aprovado";
                } else {

                    return "LOGIN OU SENHA ERRADOS";

                }
            } catch (Exception) {
                return "ERRO";
            }


            
        }


    }
}
