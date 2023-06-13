using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Onbooks_3Ds.Helper;
using System;
using System.Collections.Generic;

namespace Onbooks_3Ds.Models
{
    public class Emprestimo
    {
        private string id, titulo, identidade;
        private string data_emprestimo, data_devolucao;

        public Emprestimo(string id, string titulo, string identidade, string data_emprestimo, string data_devolucao)
        {
            this.id = id;
            this.titulo = titulo;
            this.identidade = identidade;
            this.data_emprestimo = data_emprestimo;
            this.data_devolucao = data_devolucao;
        }
        public string Id { get => id; set => id = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public string Identidade { get => identidade; set => identidade = value; }
        public string Data_emprestimo { get => data_emprestimo; set => data_emprestimo = value; }
        public string Data_devolucao { get => data_devolucao; set => data_devolucao = value; }

        static MySqlConnection conexao = FabricaConexao.getConexao(true, "Senai");

        internal static List<Emprestimo> GetEmprestimo()
        {
            try
            {

                conexao.Open();
                MySqlCommand qry = new MySqlCommand(
                    "Select * from emprestimo " +
                    "INNER JOIN exemplar ON emprestimo.FK_EXEMPLAR_id_exemplar = exemplar.id_exemplar " +
                    "INNER JOIN usuario ON emprestimo.FK_USUARIO_id_usuario = usuario.id_usuario " +
                    "INNER JOIN obra ON exemplar.FK_OBRA_id_obra = obra.id_obra ", conexao);

                List<Emprestimo> lista = new List<Emprestimo>();

                MySqlDataReader leitor = qry.ExecuteReader();

                while (leitor.Read())
                {
                    lista.Add(new Emprestimo(
                leitor["id_emprestimo"].ToString(),
                leitor["titulo"].ToString(),
                leitor["nome_usuario"].ToString(),
                leitor["data_emprestimo"].ToString(),
                leitor["data_previsai_volta"].ToString()));
                }

                conexao.Close();
                return lista;
            }
            catch (Exception e)
            {
                if (conexao.State == System.Data.ConnectionState.Open)
                    conexao.Close();
                string mensagem =e.Message;
                return null;
            }


        }

        internal string Devolucao()
        {
            try
            {
                conexao.Open();
                MySqlCommand qry = new MySqlCommand(
                    "DELETE FROM Emprestimo WHERE id_emprestimo = @cod", conexao);
                qry.Parameters.AddWithValue("@cod", this.id);

                qry.ExecuteNonQuery();
                conexao.Close();
                return "Devolvido com sucesso";
            }
            catch (Exception e)
            {
                if (conexao.State == System.Data.ConnectionState.Open)
                    conexao.Close();
                return e.Message;
            }
        }
        internal string Cadastro_Emprestimo()
        {
            try
            {
                conexao.Open();
                MySqlCommand qry = new MySqlCommand(
                    "insert into emprestimo value(@Data_emprestimo,null,@Data_devolucao,null,null,null,null,@Codigo_livro,@id)", conexao);
                qry.Parameters.AddWithValue("@Data_emprestimo", this.data_emprestimo);
                qry.Parameters.AddWithValue("@Data_devolucao", this.data_devolucao);
                qry.Parameters.AddWithValue("@id", this.id);
                qry.Parameters.AddWithValue("@Codigo_livro", this.Titulo);

                qry.ExecuteNonQuery();
                conexao.Close();
                return "Emprestimo Aprovado";
            }
            catch (Exception e)
            {
                if (conexao.State == System.Data.ConnectionState.Open)
                    conexao.Close();
                return e.Message;
            }
            
        }








    }
}
