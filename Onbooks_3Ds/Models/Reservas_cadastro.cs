using MySql.Data.MySqlClient;
using Onbooks_3Ds.Helper;
using System;

namespace Onbooks_3Ds.Models
{
    public class Reservas_cadastro
    {
        private string data_reserva, data_final, id_exemp, id_usuario;

        public Reservas_cadastro(string data_reserva, string data_final, string id_exemp, string id_usuario)
        {
            this.data_reserva = data_reserva;
            this.data_final = data_final;
            this.id_exemp = id_exemp;
            this.id_usuario = id_usuario;
        }

        public string Data_reserva { get => data_reserva; set => data_reserva = value; }
        public string Data_final { get => data_final; set => data_final = value; }
        public string Id_exemp { get => id_exemp; set => id_exemp = value; }
        public string Id_usuario { get => id_usuario; set => id_usuario = value; }
        static MySqlConnection conexao = FabricaConexao.getConexao(true, "Senai");


        internal string Cadastrar_Reserva()
        {
            try
            {
                if (!(data_reserva.Length == 0) && !(data_final.Length == 0) && !(id_exemp.Length == 0) && !(id_usuario.Length == 0))
                {
                    conexao.Open();
                    MySqlCommand qyr = new MySqlCommand("INSERT INTO reservar VALUES(@data_reserva,@data_final,null,@id_usuario,@id_exemp)", conexao);
                    qyr.Parameters.AddWithValue("@id_usuario", id_usuario);
                    qyr.Parameters.AddWithValue("@id_exemp", id_exemp);
                    qyr.Parameters.AddWithValue("@data_reserva", data_reserva);
                    qyr.Parameters.AddWithValue("@data_final", data_final);




                    qyr.ExecuteNonQuery();
                    conexao.Close();
                    return "Reservado";
                }
                else
                {
                    return "Não Foi Reservado";
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
