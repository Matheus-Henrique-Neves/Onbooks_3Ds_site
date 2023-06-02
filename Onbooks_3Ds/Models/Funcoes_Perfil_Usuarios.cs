using CadUsu3dsN.Helper;
using MySql.Data.MySqlClient;
using Onbooks_3Ds.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onbooks_3Ds.Models
{
    public class Funcoes_Perfil_Usuarios
    {
        private string nome_usuario;
        private string cpf;
        private string email;
        private string telefone;
        private string senha;
        /*contrutor dos Cadastros*/
        public Funcoes_Perfil_Usuarios(string nome_usuario, string cpf, string email, string telefone, string senha) {
            this.nome_usuario = nome_usuario;
            this.cpf = cpf;
            this.email = email;
            this.telefone = telefone;
            this.senha = senha;
        }
        /*Os metodos emcapsulados e a string de conexão para o server mudar o que esta abaixo caso for mexer em caso*/
        static MySqlConnection conexao = FabricaConexao.getConexao(true, "Senai");
        public string Nome_usuario { get => nome_usuario; set => nome_usuario = value; }
        public string Cpf { get => cpf; set => cpf = value; }
        public string Email { get => email; set => email = value; }
        public string Telefone { get => telefone; set => telefone = value; }
        public string Senha { get => senha; set => senha = value; }
         





        internal string Criar_Usuario()
        {
            if (!(nome_usuario == null) && !(email == null) && !(telefone == null) && !(senha == null) && !(cpf ==null))
            {
                if (cpf.VarificaraCPF()) { 
                
                string senhaCripitografada = senha.GerarHash();
                string cpfCripitografado=cpf.GerarHash();
                try
                {
                conexao.Open();
                MySqlCommand qyr = new MySqlCommand("INSERT INTO usuario VALUES(@nome_usuario,@cpf,@email,@telefone,@senha,null,null,null,null,null,null,null,null)", conexao);
                qyr.Parameters.AddWithValue("@nome_usuario",this.nome_usuario);
                qyr.Parameters.AddWithValue("@cpf", cpfCripitografado);
                qyr.Parameters.AddWithValue("@email", this.email);
                    qyr.Parameters.AddWithValue("@senha", senhaCripitografada);
                    qyr.Parameters.AddWithValue("@telefone", this.telefone);
                
                /*Execulta o cadastro da conta (fazer isso para o web service para evitar conflito entre possivel site e app/android )*/
                qyr.ExecuteNonQuery();
                conexao.Close();
                return "cadastradro com sucesso";
                } catch(Exception e) {
                if (conexao.State == System.Data.ConnectionState.Open)
                    conexao.Close();
                return e.Message;
            
                }
                }
                else
                {
                  return "Cpf invalido(Não Cadastrado)";
                }


            }
            else
            {
                return "Preencha todos os dados de forma valida";
            }

        }
        
        //tem que arrumar esse metodo abaixo
        internal string Deletar_Usuario() {
            try {
                conexao.Open();
                MySqlCommand qyr = new MySqlCommand("DELETE FROM Pessoas WHERE  = @cod", conexao);
                qyr.Parameters.AddWithValue("@nome_usuario", this.nome_usuario);
                /*Execulta o deletar conta porem(fazer isso para o web service para evitar conflito entre possivel site e app/android )*/
                qyr.ExecuteNonQuery();
                conexao.Close();
                return "parabens foi cadastradro";
            } catch (Exception e) {
                if (conexao.State == System.Data.ConnectionState.Open)
                    conexao.Close();
                return e.Message;
            }

        }

        internal string atualizar_Usuario()
        {
            if (!(nome_usuario == null) && !(email == null) && !(telefone == null) && !(senha == null) && !(cpf == null))
            {
                if (cpf.VarificaraCPF())
                {

                    string senhaCripitografada = senha.GerarHash();
                    string cpfCripitografado = cpf.GerarHash();
                    try
                    {
                        conexao.Open();
                        MySqlCommand qyr = new MySqlCommand("INSERT INTO usuario VALUES(@nome_usuario,@cpf,@email,@telefone,@senha,null)", conexao);
                        qyr.Parameters.AddWithValue("@nome_usuario", this.nome_usuario);
                        qyr.Parameters.AddWithValue("@cpf", cpfCripitografado);
                        qyr.Parameters.AddWithValue("@email", this.email);
                        qyr.Parameters.AddWithValue("@senha", senhaCripitografada);
                        qyr.Parameters.AddWithValue("@telefone", this.telefone);

                        /*Execulta o cadastro da conta (fazer isso para o web service para evitar conflito entre possivel site e app/android )*/
                        qyr.ExecuteNonQuery();
                        conexao.Close();
                        return "cadastradro com sucesso";
                    }
                    catch (Exception e)
                    {
                        if (conexao.State == System.Data.ConnectionState.Open)
                            conexao.Close();
                        return e.Message;

                    }
                }
                else
                {
                    return "Cpf invalido(Não Cadastrado)";
                }


            }
            else
            {
                return "Preencha todos os dados de forma valida";
            }

        }

       
    }
}
