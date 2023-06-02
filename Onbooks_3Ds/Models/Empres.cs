using MySql.Data.MySqlClient;
using Onbooks_3Ds.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onbooks_3Ds.Models
{
    public class Empres
    {
        private string id, titulo, identidade;
        private DateTime data_emprestimo, data_devolucao;

        public Empres(string id, string titulo, string identidade,  DateTime data_emprestimo, DateTime data_devolucao)
        {
            this.id = id;
            this.titulo = titulo;
            this.identidade = identidade;
            
            this.data_emprestimo = data_emprestimo;
            this.data_devolucao = data_devolucao;
        }


        static MySqlConnection conexao = FabricaConexao.getConexao(true, "Senai");

        public string Id { get => id; set => id = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public string Identidade { get => identidade; set => identidade = value; }
        
        public DateTime Data_emprestimo { get => data_emprestimo; set => data_emprestimo = value; }
        public DateTime Data_devolucao { get => data_devolucao; set => data_devolucao = value; }

        internal static List<Empres> Escrever_tela()
        {
            try
            {

                conexao.Open();
                MySqlCommand qry = new MySqlCommand(
                    "Select * from emprestimo " +
                    "INNER JOIN exemplar on emprestimo.FK_EXEMPLAR_id_exemplar = exemplar.id_exemplar " +
                    "INNER JOIN usuario on emprestimo.FK_USUARIO_id_usuario = usuario.id_usuario " +
                    "INNER JOIN obra on exemplar.FK_OBRA_id_obra = obra.id_obra ", conexao);

                List<Empres> lista = new List<Empres>();

                MySqlDataReader leitor = qry.ExecuteReader();

                while (leitor.Read())
                {
                    DateTime data = (DateTime)leitor["data_previsao_volta"];
                    DateTime data_empres = (DateTime)leitor["data_emprestimo"];
                    
                    
                        
                        lista.Add(new Empres(
    leitor.GetString("id_emprestimo"),
    leitor["titulo"].ToString(),
    leitor["nome"].ToString(),
    data_empres.Date,
    data.Date
    


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
