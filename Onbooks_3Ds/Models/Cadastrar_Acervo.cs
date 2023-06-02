 using MySql.Data.MySqlClient;
using Onbooks_3Ds.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onbooks_3Ds.Models {
    public class Cadastrar_Acervo {
        private string titulo;
        private string img;
        private string ano_publicacao;
        private string issn;
        private string isbn;
       // private string nome_Autor;

        private string editora;
       // private string nome_editora;

        public Cadastrar_Acervo(string titulo, string img, string ano_publicacao, string issn, string isbn,  string editora) {
            this.titulo = titulo;
            this.img = img;
            this.ano_publicacao = ano_publicacao;
            this.issn = issn;
            this.isbn = isbn;
            this.editora = editora;
        }
        static MySqlConnection conexao = FabricaConexao.getConexao(true, "Casa");
        public string Titulo { get => titulo; set => titulo = value; }
        public string Img { get => img; set => img = value; }
        public string Ano_publicacao { get => ano_publicacao; set => ano_publicacao = value; }
        public string Issn { get => issn; set => issn = value; }
        public string Isbn { get => isbn; set => isbn = value; }
        public string Editora { get => editora; set => editora = value; }


        internal string cadastrar_livro()
        {
            return "a";
        }









    }
}
