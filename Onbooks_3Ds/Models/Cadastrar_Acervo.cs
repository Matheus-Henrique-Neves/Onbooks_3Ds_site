 using MySql.Data.MySqlClient;
using Onbooks_3Ds.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onbooks_3Ds.Models {
    public class Cadastrar_Acervo {
        private string id;
        private string titulo;
        private string img;
        private string ano_publicacao;
        private string issn;
        private string isbn;
       // private string nome_Autor;

        private string editora;
       // private string nome_editora;

        public Cadastrar_Acervo(string id, string titulo, string img, string ano_publicacao, string issn, string isbn,  string editora) {
            this.id = id;
            this.titulo = titulo;
            this.img = img;
            this.ano_publicacao = ano_publicacao;
            this.issn = issn;
            this.isbn = isbn;
            this.editora = editora;
        }
        static MySqlConnection conexao = FabricaConexao.getConexao(true, "Senai");
        public string Id { get => id; set => id = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public string Img { get => img; set => img = value; }
        public string Ano_publicacao { get => ano_publicacao; set => ano_publicacao = value; }
        public string Issn { get => issn; set => issn = value; }
        public string Isbn { get => isbn; set => isbn = value; }
        public string Editora { get => editora; set => editora = value; }


        internal string cadastrar_Obra()
        {
            try
            {
                if (!(titulo.Length==0)&& !(img == null) && !(ano_publicacao.Length == 0) 
                    && !(issn.Length == 0) && !(isbn.Length == 0) && !(editora.Length == 0))
                {
                    conexao.Open();
                    MySqlCommand qyr = new MySqlCommand("INSERT INTO obra VALUES(@titulo,@img,@ano_publicacao,@issn,@isbn,null,@editora)", conexao);
                    qyr.Parameters.AddWithValue("@titulo", titulo);        //esse monte de null ta aqui porque ele cadastar o que seria o endereço dele depois
                    qyr.Parameters.AddWithValue("@img", img);                 //em outra parte                                           
                    qyr.Parameters.AddWithValue("@ano_publicacao", ano_publicacao);
                    qyr.Parameters.AddWithValue("@issn", issn);
                    qyr.Parameters.AddWithValue("@isbn", isbn);
                    qyr.Parameters.AddWithValue("@editora", editora);


                    qyr.ExecuteNonQuery();
                    conexao.Close();
                    return "Foi_cadastrado" ;
                }
                else
                {
                    return "Não foi cadastrado preencha todos os campos"; 
                }
            }
            catch (Exception e)
            {
                return  e.Message ;

            }
            finally
            {
                conexao.Close();
            }




        }

        public static List<Cadastrar_Acervo> listagem()
        {
            try
            {

                conexao.Open();
                MySqlCommand qry = new MySqlCommand(
                    "SELECT * from obra LEFT JOIN editora " +
                    "on obra.FK_EDITORA_id_editora = editora.id_editora", conexao);
                List<Cadastrar_Acervo> listatrasncrita = new List<Cadastrar_Acervo>();
                

                MySqlDataReader leitor = qry.ExecuteReader();

                while (leitor.Read())
                {



                    listatrasncrita.Add(new Cadastrar_Acervo(
                        
                        leitor.GetString("id_obra"),
                        leitor.GetString("titulo"),
                        leitor.GetString("img"),
                        leitor.GetString("ano_publicacao"),
                        leitor["issn"].ToString(),
                        leitor["isbn"].ToString(),
                        leitor.GetString("nome")

                        )); ;

                }
                
                conexao.Close();
                return listatrasncrita;
            }
            catch (Exception e)
            {
                if (conexao.State == System.Data.ConnectionState.Open)
                    conexao.Close();
                string a = e.Message;
                return null;
            }


        }












    }
}
