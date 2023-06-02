using CadUsu3dsN.Helper;
using MySql.Data.MySqlClient;
using Onbooks_3Ds.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onbooks_3Ds.Models
{
    public class User
    {
        private string email, senha, tipo;

        public User(string email, string senha, string tipo)
        {
            this.email = email;
            this.senha = senha;
            this.tipo = tipo;
        }

        static MySqlConnection conexao = FabricaConexao.getConexao(true, "Senai");

        public string Email { get => email; set => email = value; }
        public string Senha { get => senha; set => senha = value; }
        public string Type { get => tipo; set => tipo = value; }

        internal string VarificarLogin()
        {
            string senhaCripitografada = senha.GerarHash();
            if (tipo == "Normal")
            {
                try
                {
                    conexao.Open();
                    MySqlCommand qyr = new MySqlCommand("SELECT * FROM usuario WHERE email=@email AND senha =@senha", conexao);
                    qyr.Parameters.AddWithValue("@email", this.email);
                    qyr.Parameters.AddWithValue("@senha", senhaCripitografada);
                    MySqlDataReader resultado = qyr.ExecuteReader();
                    if (resultado.Read())
                    {
                        conexao.Close();
                        return "Aprovado";
                    }
                    else
                    {
                        conexao.Close();
                        return "Reprovado";

                    }
                }
                catch (Exception e)
                {
                    if (conexao.State == System.Data.ConnectionState.Open)
                    {
                        conexao.Close();
                    }
                    return e.Message;
                }
            }
            else
            {
                try
                {
                    conexao.Open();
                    MySqlCommand qyr = new MySqlCommand("SELECT * FROM adm WHERE email=@email AND senha =@senha", conexao);
                    qyr.Parameters.AddWithValue("@email", this.email);
                    qyr.Parameters.AddWithValue("@senha", senhaCripitografada);
                    MySqlDataReader resultado = qyr.ExecuteReader();
                    if (resultado.Read())
                    {
                        conexao.Close();
                        return "Aprovado";
                    }
                    else
                    {
                        conexao.Close();
                        return "Reprovado";

                    }
                }
                catch (Exception e)
                {
                    if (conexao.State == System.Data.ConnectionState.Open)
                    {
                        conexao.Close();
                    }
                    return e.Message;
                }

            }

        }


    }
}
