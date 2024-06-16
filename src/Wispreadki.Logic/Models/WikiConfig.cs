namespace Wispreadki.Logic.Models;

/// <summary>Configuración de Wiki</summary>
public sealed class WikiConfig
{
    /// <summary>Directorio de plantilla base</summary>
    public string PathTemplateDirectory { get; set; } = DefaultPathTemplateDirectory;
    /// <summary>Nombre de archivo de plantilla base</summary>
    public string TemplateFileName { get; set; } = DefaultTemplateFileName;
    /// <summary>Directorio de salida de archivos</summary>
    public string OutputFilesPath { get; set; } = DefaultOutputFilesPath;

    /// <summary>Lista de plantillas</summary>
    /// <remarks>Valores de reemplazo {{body}} buscarán un archivo de plantilla interna con ese nombre en directorio y subdirectorios (body.json)</remarks>
    public List<WikiTemplate> Templates { get; set; } = [];

    /// <summary>Obtiene la ruta completa de salida</summary>
    public string OutputFullPath => Path.Combine(OutputFilesPath, TemplateFileName);

    /// <summary>Obtiene el contenido de la plantilla con las variables reemplazadas</summary>
    public string GetContent()
    {
        var content = File.ReadAllText(Path.Combine(PathTemplateDirectory, TemplateFileName));
        foreach (var template in Templates)
        {
            content = content.Replace($"{{{{{template.Title}}}}}", template.GetContent());
        }
        return content;
    }
}
