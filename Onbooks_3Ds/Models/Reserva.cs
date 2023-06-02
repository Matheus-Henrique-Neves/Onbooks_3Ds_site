using MySql.Data.MySqlClient;
using Onbooks_3Ds.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onbooks_3Ds.Models
{
    public class Reserva
    {
        private int id;
        private string tituo;
        private string identidade;
        private DateTime data_reserva; 
        private DateTime data_finalizacao_reserva;

        public Reserva(int id, string tituo, string identidade, DateTime data_reserva, DateTime data_finalizacao_reserva)
        {
            this.id = id;
            this.tituo = tituo;
            this.identidade = identidade;
            this.data_reserva = data_reserva;
            this.data_finalizacao_reserva = data_finalizacao_reserva;
        }

        public int Id { get => id; set => id = value; }
        public string Tituo { get => tituo; set => tituo = value; }
        public string Identidade { get => identidade; set => identidade = value; }
        public DateTime Data_reserva { get => data_reserva; set => data_reserva = value; }
        public DateTime Data_finalizacao_reserva { get => data_finalizacao_reserva; set => data_finalizacao_reserva = value; }
        static MySqlConnection conexao = FabricaConexao.getConexao(true, "Senai");


        internal static List<Reserva> listar()
        {
            try
            {
                conexao.Open();
                MySqlCommand qry = new MySqlCommand(
                    "SELECT * FROM reservar INNER JOIN exemplar ON reservar.FK_EXEMPLAR_id_exemplar = exemplar.id_exemplar " +
                    "INNER JOIN usuario ON reservar.FK_USUARIO_id_usuario = usuario.id_usuario " +
                    "INNER JOIN obra ON exemplar.FK_OBRA_id_obra = obra.id_obra where data_finalizacao_reserva >= CURRENT_DATE()", conexao);

                List<Reserva> lista = new List<Reserva>();

                MySqlDataReader leitor = qry.ExecuteReader();

                while (leitor.Read())
                {
                    DateTime data = (DateTime)leitor["data_reserva"];
                    DateTime data_finalizacao = (DateTime)leitor["data_finalizacao_reserva"];
                    

                    lista.Add(new Reserva(
                        int.Parse(leitor["id_reserva"].ToString()), 
                        leitor["titulo"].ToString(),
                        leitor["nome_usuario"].ToString(),

                        data_finalizacao.Date,
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
