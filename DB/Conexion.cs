using System.Data.SqlClient;
using Dapper;
using TP1_API.Models;

namespace TP1_API.DB
{
    public class Conexion
    {
        private readonly string connectionString = $"Server=server-terciario.hilet.com,11333;Database=honor;User Id=sa;Password=1234!\"qwerQW;";

        // Obtener todas las opiniones
        public IEnumerable<Opinion> GetOpinions()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Comentarios";
                return connection.Query<Opinion>(query);
            }
        }

        // Obtener opiniones por nombre de usuario
        public IEnumerable<Opinion> GetOpinionsByUser(string nombreUsuario)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Comentarios WHERE nombre_usuario = @nombreUsuario";
                return connection.Query<Opinion>(query, new { nombreUsuario });
            }
        }

        // Eliminar (marcar como no visible) una opinión por id
        public void DeleteOpinion(int idComentario)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Comentarios SET visible = 0 WHERE id_comentario = @idComentario";
                connection.Execute(query, new { idComentario });
            }
        }

        // Insertar una nueva opinión
        public void InsertOpinion(Opinion opinion)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Comentarios (comentario, visible, nombre_usuario, imagen) VALUES (@comentario, @visible, @nombre_usuario, @imagen)";
                connection.Execute(query, opinion);
            }
        }
    }
}