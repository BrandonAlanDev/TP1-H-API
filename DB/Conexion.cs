using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using TP1_API.Models;

namespace TP1_API.DB
{
    public class Conexion
    {
        private readonly string connectionString = "Server=server-terciario.hilet.com,11333;Database=honor;User Id=sa;Password=1234!\"qwerQW;";

        // Obtener todas las opiniones
        public IEnumerable<Opinion> GetOpinions()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "EXEC dbo.CRUD_comentario @STATEMENT = 1";
                return connection.Query<Opinion>(query);
            }
        }

        // Obtener opiniones por nombre de usuario
        public IEnumerable<Opinion> GetOpinionsByUser(string nombreUsuario)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "EXEC dbo.CRUD_comentario @STATEMENT = 2, @NOMBRE_USUARIO = @nombreUsuario";
                return connection.Query<Opinion>(query, new { nombreUsuario });
            }
        }

        // Eliminar (marcar como no visible) una opinión por id
        public void DeleteOpinion(int idComentario)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "EXEC dbo.CRUD_comentario @STATEMENT = 3, @ID_COMENTARIO = @idComentario";
                connection.Execute(query, new { idComentario });
            }
        }

        // Insertar una nueva opinión
        public void InsertOpinion(Opinion opinion)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "EXEC dbo.CRUD_comentario @STATEMENT = 4, @COMENTARIO = @comentario, @VISIBLE = @visible, @NOMBRE_USUARIO = @nombre_usuario, @IMAGEN = @imagen";
                connection.Execute(query, new { 
                    opinion.comentario, 
                    opinion.visible, 
                    opinion.nombre_usuario, 
                    opinion.imagen 
                });
            }
        }
    }
}