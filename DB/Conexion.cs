using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP1_API.Models;
using System.Data.SqlClient;

namespace TP1_API.DB
{
    public class Conexion
    {
        private string connectionString="asdas";

        // Codigo de comando generico que no necesito que devuelva nada para no repetir codigo
        private static void Comando(string queryString,
            string connectionString)
        {
            try{
                using (SqlConnection connection = new SqlConnection(
                        connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }catch(Exception ex){
            }
        }

        // Verifica la existencia, si existe, devuelve el id, si no existe devuelve 0
        public int existeUsuario(string user){
            try{
                using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = $"SELECT id_usuario FROM Usuarios WHERE nombre_usuario = @user ;";
                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@user", user);
                            return Convert.ToInt32(cmd.ExecuteScalar());
                        }
                    }
            }catch{return 0;}
        }

        // Verifica la existencia, si existe, devuelve el id, si no existe crea un usuario, luego devuelve su id, si sigue sin existir devuelve 0
        public int comprobarUsuario(string user,string imagen){
            int resultado = existeUsuario(user);
            if(resultado) return resultado;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"INSERT INTO Usuarios(nombre_usuario,imagen) VALUES (@user,@imagen) ;";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@user", user);
                        cmd.Parameters.AddWithValue("@imagen", imagen);
                        cmd.ExecuteScalar();
                    }
                }
            }catch{}
            int resultado = existeUsuario(user);
            if(resultado){return resultado;} else return 0; 
        }

        //Sube un comentario
        public void cargarComentario(Opinion opinion){
            string comment=opinion.comment;
            int userid;
            userid=comprobarUsuario(opinion.user,opinion.imagen);
            string query = $"INSERT INTO Comentarios(comentario,visible,fk_id_usuario) VALUES ('{comment}',1,{userid});";
            Comando(query,connectionString);
        }

        
    }
    
}