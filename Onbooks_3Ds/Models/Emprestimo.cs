using MySql.Data.MySqlClient;
using Onbooks_3Ds.Helper;
using System;
using System.Collections.Generic;

namespace Onbooks_3Ds.Models
{
    public class Emprestimo
    {
        private string id, titulo, identidade;
        private DateTime data_emprestimo, data_devolucao;

        public Emprestimo(string id, string titulo, string identidade, DateTime data_emprestimo, DateTime data_devolucao)
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
        public DateTime Data_emprestimo { get => data_emprestimo; set => data_emprestimo = value; }
        public DateTime Data_devolucao { get => data_devolucao; set => data_devolucao = value; }
        static MySqlConnection conexao = FabricaConexao.getConexao(true, "Senai");

        internal static List<Emprestimo> GetEmprestimo()
        {
            try
            {

                conexao.Open();
                MySqlCommand qry = new MySqlCommand(
                    "Select *from emprestimo " +
                    "INNER JOIN exemplar ON emprestimo.FK_EXEMPLAR_id_exemplar = exemplar.id_exemplar " +
                    "INNER JOIN usuario ON emprestimo.FK_USUARIO_id_usuario = usuario.id_usuario " +
                    "INNER JOIN obra ON exemplar.FK_OBRA_id_obra = obra.id_obra;", conexao);

                List<Emprestimo> lista = new List<Emprestimo>();

                MySqlDataReader leitor = qry.ExecuteReader();

                while (leitor.Read())
                {
                    
                    



                    lista.Add(new Emprestimo(
                leitor["id_emprestimo"].ToString(),
                leitor["titulo"].ToString(),
                leitor["nome_usuario"].ToString(),
                leitor.GetDateTime("data_emprestimo"),
                leitor.GetDateTime("data_previsai_volta")));





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










    }
}
