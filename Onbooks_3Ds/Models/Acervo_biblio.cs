using MySql.Data.MySqlClient;
using Onbooks_3Ds.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onbooks_3Ds.Models
{
    public class Acervo_biblio
    {
        private string image, id, titulo, biblioteca ;

        public Acervo_biblio(string image, string id, string titulo, string biblioteca)
        {
            this.image = image;
            this.id = id;
            this.titulo = titulo;
            this.biblioteca = biblioteca;
           
        }

        public string Image { get => image; set => image = value; }
        public string Id { get => id; set => id = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public string Biblioteca { get => biblioteca; set => biblioteca = value; }
        static MySqlConnection conexao = FabricaConexao.getConexao(true, "Senai");



        internal static List<Acervo_biblio> listagem()
        {
            try
            {

                conexao.Open();
                MySqlCommand qry = new MySqlCommand(
                    "SELECT * from exemplar LEFT JOIN biblioteca " +
                    "on exemplar.FK_BIBLIOTECA_id_biblioteca = biblioteca.id_biblioteca LEFT JOIN obra " +
                    "on exemplar.FK_OBRA_id_obra = obra.id_obra", conexao);

                List<Acervo_biblio> lista = new List<Acervo_biblio>();

                MySqlDataReader leitor = qry.ExecuteReader();

                while (leitor.Read())
                {
                  


                    lista.Add(new Acervo_biblio(
                        leitor.GetString("img"),
                        leitor["id_exemplar"].ToString(),
                        leitor["titulo"].ToString(),
                        leitor["nome"].ToString()

                        ));
                    
                }
                
                conexao.Close();
                return lista;
            }
            catch (Exception e)
            {
                if (conexao.State == System.Data.ConnectionState.Open)
                    conexao.Close();
                return null;
            }


        }

        







    }
}
