using CONSUMIR_API.Models;
using CONSUMIR_API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CONSUMIR_API.Controllers
{
    // controlador llamado HomeController que hereda de Controller
    public class HomeController : Controller
    {
        // Inyección de dependencia del servicio que interactúa con la API
        private readonly IServicio_APIcs _servicioApi;

        // Constructor del controlador que recibe una implementación de IServicio_APIcs como parámetro
        public HomeController(IServicio_APIcs servicioApi)
        {
            // Asigna el servicio proporcionado a la variable de clase
            _servicioApi = servicioApi;  
        }

        // Acción del controlador para la página de inicio
        public async Task<IActionResult> Index()
        {
            // Llama al método Lista del servicio para obtener una lista de datos de la API
            List<model_API> Lista = await _servicioApi.Lista();

            // Devuelve la vista "Index" con la lista de datos como modelo
            return View(Lista);
        }

        // Acción del controlador para mostrar información de un usuario
        public async Task<IActionResult> Usuario(int idUsuario)
        {
            // Instancia de model_API para representar un usuario
            model_API model_usuario = new model_API();

            // ViewBag para indicar que se está creando un nuevo usuario
            ViewBag.Accion = "Nuevo Usuario";

            // Si el idUsuario no es 0, significa que se está editando un usuario existente
            if (idUsuario != 0)
            {
                // Llama al método Obtener(idUsuario) del servicio para obtener información del usuario con el ID proporcionado
                model_usuario = await _servicioApi.Obtener(idUsuario);

                // Indica que se está editando un usuario
                ViewBag.Accion = "Editar Usuario"; 
            }

            // Devuelve la vista "Usuario" con el modelo(info) de usuario correspondiente
            return View(model_usuario);
        }

        // Acción del controlador para guardar cambios en un usuario
        [HttpPost]
        public async Task<IActionResult> GuardarCambios(model_API ob_Usuario)
        {
            bool respuesta;

            // Verifica si el userId del usuario es igual a cero, lo cual indica que es un nuevo usuario
            if (ob_Usuario.userId == 0)
                // Llama al método Guardar(ob_Usuario) del servicio para guardar un nuevo usuario
                respuesta = await _servicioApi.Guardar(ob_Usuario); 
            else
                // Llama al método Editar(ob_Usuario) del servicio para editar un usuario existente
                respuesta = await _servicioApi.Editar(ob_Usuario);  

            if (respuesta)
                // Verifica la respuesta del servicio y redirige a la página de inicio si es exitosa
                return RedirectToAction("Index"); 
            else
                // Si la operación falla, devuelve una respuesta sin contenido
                return NoContent(); 
        }

        // Acción del controlador para eliminar un usuario
        [HttpGet]
        public async Task<IActionResult> Eliminar(int idUsuario)
        {
            // Llama al método Eliminar(idUsuario) del servicio para eliminar un usuario
            var respuesta = await _servicioApi.Eliminar(idUsuario);

            if (respuesta)
                // Verifica la respuesta del servicio y redirige a la página de inicio si es exitosa
                return RedirectToAction("Index");  
            else
                // Si la operación falla, devuelve una respuesta sin contenido
                return NoContent();  
        }

        public IActionResult Privacy()
        {
            // Devuelve la vista "Privacy"
            return View();  
        }

        // Acción del controlador para manejar errores
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Devuelve la vista "Error" con un modelo que incluye el identificador de solicitud actual
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
