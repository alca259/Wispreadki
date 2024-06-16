namespace Wispreadki.Logic.Models;

/// <summary>Constantes de la aplicación</summary>
public static class AppConstants
{
    /// <summary>Directorio de plantilla base</summary>
    public const string DefaultPathTemplateDirectory = "Templates";
    /// <summary>Directorio de plantillas</summary>
    public const string DefaultPathTemplateSnippetsDirectory = "Snippets";
    /// <summary>Nombre de archivo de plantilla base</summary>
    public const string DefaultTemplateFileName = "template.md";
    /// <summary>Directorio de salida de archivos</summary>
    public const string DefaultOutputFilesPath = "Output";

    /// <summary>Expresión regular para buscar variables</summary>
    public static Regex RegexSearchFilter => new(
        pattern: @"\{\{([^\}]+)\}\}",
        options: RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled,
        matchTimeout: TimeSpan.FromMilliseconds(500));
}
