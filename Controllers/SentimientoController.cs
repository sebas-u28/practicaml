using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using practicaml.Models;
using practicaml.MLSentimiento;

namespace practicaml.Controllers;

public class SentimientoController : Controller
{
    private readonly PredictionEnginePool<MLSentimiento.MLSentimiento.ModelInput, MLSentimiento.MLSentimiento.ModelOutput> _predictionEngine;

    private readonly ILogger<SentimientoController> _logger;

    public SentimientoController(ILogger<SentimientoController> logger, PredictionEnginePool<MLSentimiento.MLSentimiento.ModelInput, MLSentimiento.MLSentimiento.ModelOutput> predictionEngine)
    {
        _logger = logger;
        _predictionEngine = predictionEngine;
    }

    public IActionResult Formulario()
    {
        return View();
    }
[HttpPost]
public IActionResult Predecir(string texto)
{
    var input = new MLSentimiento.MLSentimiento.ModelInput { Text = texto };
    var prediction = _predictionEngine.Predict(input);

    var resultado = prediction.PredictedLabel ? "Positivo" : "Negativo";

    ViewBag.TextoIngresado = texto;
    ViewBag.Sentimiento = resultado;

    return View("Formulario");
}

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
