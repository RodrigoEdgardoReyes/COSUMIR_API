using CONSUMIR_API.Models;

namespace CONSUMIR_API.Services
{
    // Define una interfaz llamada IServicio_APIcs
    public interface IServicio_APIcs
    {
        // Método para obtener una lista de model_API
        Task<List<model_API>> Lista();

        // Método para obtener información de un usuario por su ID
        Task<model_API> Obtener(int idUsuario);

        // Método para guardar un nuevo usuario
        Task<bool> Guardar(model_API Usuario);

        // Método para editar un usuario existente
        Task<bool> Editar(model_API Usuario);

        // Método para eliminar un usuario por su ID
        Task<bool> Eliminar(int idUsuario);
    }

}
