using Microsoft.AspNetCore.Components.Forms;
using MySql.Data.MySqlClient;
using Onbooks_3Ds.Helper;
using System;

namespace Onbooks_3Ds.Models
{
    public class Acervo_cadastro
    {
        private string id_obra, id_biblioteca, data_aqui;

        public Acervo_cadastro(string id_obra, string id_biblioteca, string data_aqui)
        {
            this.Id_obra = id_obra;
            this.Id_biblioteca = id_biblioteca;
            this.Data_aqui = data_aqui;
            
        }

        public string Id_obra { get => id_obra; set => id_obra = value; }
        public string Id_biblioteca { get => id_biblioteca; set => id_biblioteca = value; }
        public string Data_aqui { get => data_aqui; set => data_aqui = value; }
        
        static MySqlConnection conexao = FabricaConexao.getConexao(true, "Senai");


        internal string Cadastrar_Acervo()
        {
            try
            {
                if (!(id_obra.Length == 0) && !(id_biblioteca.Length == 0) && !(data_aqui.Length == 0))
                {
                    conexao.Open();
                    MySqlCommand qyr = new MySqlCommand("INSERT INTO exemplar VALUES(@data_aqui,null,@id_obra,@id_biblioteca)", conexao);
                    qyr.Parameters.AddWithValue("@id_obra",id_obra);        
                    qyr.Parameters.AddWithValue("@id_biblioteca", id_biblioteca);                                                          
                    qyr.Parameters.AddWithValue("@data_aqui", data_aqui);                                                            
                                                                              



                    qyr.ExecuteNonQuery();
                    conexao.Close();
                    return "Foi_cadastrado";
                }
                else
                {
                    return "Não foi cadastrado preencha todos os campos";
                }
            }
            catch (Exception e)
            {
                return e.Message;

            }
            finally
            {
                conexao.Close();
            }




        }











    }
}
