namespace Wispreadki.Logic.Models;

/// <summary>Valor de reemplazo de plantilla de Wiki</summary>
public sealed class WikiTemplate
{
    /// <summary>Título de la plantilla</summary>
    public string Title { get; set; } = string.Empty;
    /// <summary>Contenido de la plantilla</summary>
    public string Content { get; set; } = string.Empty;
    /// <summary>Ruta de salida</summary>
    public string OutputPath { get; set; } = DefaultOutputFilesPath;
    /// <summary>Nombre de archivo de salida</summary>
    public string OutputFileName { get; set; } = DefaultTemplateFileName;
    /// <summary>Variables de la plantilla</summary>
    public Dictionary<string, WikiVariable> Variables { get; set; } = [];

    /// <summary>Obtiene la ruta completa de salida</summary>
    public string OutputFullPath => Path.Combine(OutputPath, OutputFileName);

    /// <summary>Obtiene el contenido de la plantilla con las variables reemplazadas</summary>
    public string GetContent()
    {
        var content = Content;
        foreach (var variable in Variables)
        {
            content = content.Replace(variable.Key, variable.Value.GetContent());
        }
        return content;
    }
}
