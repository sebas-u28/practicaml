using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using MLRecomendacion;
using practicaml.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace practicaml.Controllers
{
    public class RecomendacionController : Controller
    {
        private readonly PredictionEnginePool<MLRecomendacion.MLRecomendacion.ModelInput, MLRecomendacion.MLRecomendacion.ModelOutput> _predictionEngine;

        public RecomendacionController(PredictionEnginePool<MLRecomendacion.MLRecomendacion.ModelInput, MLRecomendacion.MLRecomendacion.ModelOutput> predictionEngine)
        {
            _predictionEngine = predictionEngine;
        }

        // Datos simulados
        private static List<Usuario> usuarios = new List<Usuario>
        {
            new Usuario { UserId = 1, Nombre = "Ana" },
            new Usuario { UserId = 2, Nombre = "Luis" },
            new Usuario { UserId = 3, Nombre = "Marta" },
            new Usuario { UserId = 4, Nombre = "Carlos" },
            new Usuario { UserId = 5, Nombre = "Sofia" }
        };

        private static List<Producto> productos = new List<Producto>
        {
            new Producto { ProductId = 1, Nombre = "Producto A" },
            new Producto { ProductId = 2, Nombre = "Producto B" },
            new Producto { ProductId = 3, Nombre = "Producto C" },
            new Producto { ProductId = 4, Nombre = "Producto D" },
            new Producto { ProductId = 5, Nombre = "Producto E" }
        };

        public IActionResult Formulario()
        {
            ViewBag.Usuarios = usuarios;
            return View();
        }

        [HttpPost]
        public IActionResult Predecir(int userId)
        {
            var usuario = usuarios.FirstOrDefault(u => u.UserId == userId);
            if (usuario == null)
            {
                ViewBag.Error = "Usuario no válido.";
                ViewBag.Usuarios = usuarios;
                return View("Formulario");
            }

            var predicciones = new List<(Producto producto, float score)>();
            foreach (var producto in productos)
            {
                var input = new MLRecomendacion.MLRecomendacion.ModelInput
                {
                    UserId = userId,
                    ProductId = producto.ProductId
                };

                var prediction = _predictionEngine.Predict(input);
                predicciones.Add((producto, prediction.Score));
            }

            var topProductos = predicciones.OrderByDescending(p => p.score).Take(5).ToList();

            ViewBag.Usuarios = usuarios;
            ViewBag.Recomendaciones = topProductos;
            ViewBag.UsuarioSeleccionado = usuario.Nombre;

            return View("Formulario");
        }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    }
}
