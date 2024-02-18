using CONSUMIR_API.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace CONSUMIR_API.Services
{
    public class Servicio_API : IServicio_APIcs
    {
        private static string _baseurl;

        public Servicio_API()
        {
            // Configuración de la URL base desde el archivo de configuración
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseurl = builder.GetSection("ApiSettings:baseUrl").Value;
        }

        public async Task<bool> Editar(model_API Usuario)
        {
            bool respuesta = false;

            // Configuración del cliente HTTP y envío de la solicitud PUT para editar un usuario
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            var content = new StringContent(JsonConvert.SerializeObject(Usuario), Encoding.UTF8, "application/json");
            var response = await cliente.PutAsync("https://jsonplaceholder.typicode.com//posts/1", content);

            // Verificación del éxito de la operación
            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<bool> Eliminar(int idUsuario)
        {
            bool respuesta = false;

            // Configuración del cliente HTTP y envío de la solicitud DELETE para eliminar un usuario
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            var response = await cliente.DeleteAsync($"https://jsonplaceholder.typicode.com//posts/{idUsuario}");

            // Verificación del éxito de la operación
            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<bool> Guardar(model_API Usuario)
        {
            bool respuesta = false;

            // Configuración del cliente HTTP y envío de la solicitud POST para guardar un usuario
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            var content = new StringContent(JsonConvert.SerializeObject(Usuario), Encoding.UTF8, "application/json");
            var response = await cliente.PostAsync("https://jsonplaceholder.typicode.com/posts/", content);

            // Verificación del éxito de la operación
            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<List<model_API>> Lista()
        {
            List<model_API> lista = new List<model_API>();

            // Configuración del cliente HTTP y envío de la solicitud GET para obtener la lista de usuarios
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            var response = await cliente.GetAsync("https://jsonplaceholder.typicode.com/posts");

            // Verificación del éxito de la operación y deserialización de la respuesta JSON
            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                lista = JsonConvert.DeserializeObject<List<model_API>>(json_respuesta);
            }
            return lista;
        }

        public async Task<model_API> Obtener(int idUsuario)
        {
            model_API objeto = new model_API();

            // Configuración del cliente HTTP y envío de la solicitud GET para obtener un usuario por ID
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            var response = await cliente.GetAsync($"https://jsonplaceholder.typicode.com/posts/{idUsuario}");

            // Verificación del éxito de la operación y deserialización de la respuesta JSON
            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<model_API>(json_respuesta);
                objeto = resultado;
            }
            return objeto;
        }
    }

}
