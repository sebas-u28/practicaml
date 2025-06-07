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

        private static List<Producto> productos = new List<Producto>
        {
            new Producto { ProductId = 1, Nombre = "P1" },
            new Producto { ProductId = 2, Nombre = "P2" },
            new Producto { ProductId = 3, Nombre = "P3" },
            new Producto { ProductId = 4, Nombre = "P4" },
            new Producto { ProductId = 5, Nombre = "P5" }
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

        private static List<Usuario> usuarios = new List<Usuario>
{
    new Usuario { UserId = 1, Nombre = "U1" },
    new Usuario { UserId = 2, Nombre = "U2" },
    new Usuario { UserId = 3, Nombre = "U3" },
    new Usuario { UserId = 4, Nombre = "U4" },
    new Usuario { UserId = 5, Nombre = "U5" },
    new Usuario { UserId = 6, Nombre = "U6" },
    new Usuario { UserId = 7, Nombre = "U7" },
    new Usuario { UserId = 8, Nombre = "U8" },
    new Usuario { UserId = 9, Nombre = "U9" },
    new Usuario { UserId = 10, Nombre = "U10" },
    new Usuario { UserId = 11, Nombre = "U11" },
    new Usuario { UserId = 12, Nombre = "U12" },
    new Usuario { UserId = 13, Nombre = "U13" },
    new Usuario { UserId = 14, Nombre = "U14" },
    new Usuario { UserId = 15, Nombre = "U15" },
    new Usuario { UserId = 16, Nombre = "U16" },
    new Usuario { UserId = 17, Nombre = "U17" },
    new Usuario { UserId = 18, Nombre = "U18" },
    new Usuario { UserId = 19, Nombre = "U19" },
    new Usuario { UserId = 20, Nombre = "U20" },
    new Usuario { UserId = 21, Nombre = "U21" },
    new Usuario { UserId = 22, Nombre = "U22" },
    new Usuario { UserId = 23, Nombre = "U23" },
    new Usuario { UserId = 24, Nombre = "U24" },
    new Usuario { UserId = 25, Nombre = "U25" },
    new Usuario { UserId = 26, Nombre = "U26" },
    new Usuario { UserId = 27, Nombre = "U27" },
    new Usuario { UserId = 28, Nombre = "U28" },
    new Usuario { UserId = 29, Nombre = "U29" },
    new Usuario { UserId = 30, Nombre = "U30" },
    new Usuario { UserId = 31, Nombre = "U31" },
    new Usuario { UserId = 32, Nombre = "U32" },
    new Usuario { UserId = 33, Nombre = "U33" },
    new Usuario { UserId = 34, Nombre = "U34" },
    new Usuario { UserId = 35, Nombre = "U35" },
    new Usuario { UserId = 36, Nombre = "U36" },
    new Usuario { UserId = 37, Nombre = "U37" },
    new Usuario { UserId = 38, Nombre = "U38" },
    new Usuario { UserId = 39, Nombre = "U39" },
    new Usuario { UserId = 40, Nombre = "U40" }
};


    }
    
    
}
