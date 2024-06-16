namespace Wispreadki.Logic.Models;

/// <summary>Valor de reemplazo de plantilla de Wiki</summary>
public sealed class WikiTemplate
{
    /// <summary>Código de la plantilla</summary>
    public required string Code { get; set; }
    /// <summary>Título de la plantilla</summary>
    public string Title { get; set; } = string.Empty;
    /// <summary>Contenido de la plantilla</summary>
    /// <remarks>Se lee el contenido de la carpeta Snippets con el mismo código de plantilla</remarks>
    public string ContentFileName { get; set; } = string.Empty;
    /// <summary>Orden de la plantilla</summary>
    public int Order { get; set; } = 0;
    /// <summary>Variables de la plantilla</summary>
    public List<WikiVariable> Variables { get; set; } = [];
    /// <summary>Indica si la plantilla es múltiple</summary>
    /// <remarks>Debe contener un "for 'mycode'" en la plantilla base para que funcione correctamente</remarks>
    public bool IsMultiple => Code.StartsWith("for ", StringComparison.InvariantCultureIgnoreCase);
    /// <summary>Obtiene el código de la plantilla</summary>
    public string GetCode() => Code.Replace("for ", string.Empty, StringComparison.InvariantCultureIgnoreCase);

    /// <summary>Obtiene el contenido de la plantilla con las variables reemplazadas</summary>
    public string GetContent()
    {
        var path = Path.Combine(DefaultPathTemplateDirectory, DefaultPathTemplateSnippetsDirectory, ContentFileName);
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"No se encontró el archivo de plantilla {path}");
        }

        var content = File.ReadAllText(path);
        foreach (var variable in Variables)
        {
            content = content.Replace($"{{{{{variable.Code}}}}}", variable.GetContent(), StringComparison.InvariantCultureIgnoreCase);
        }
        return content;
    }

    /// <summary>Obtiene las variables en el archivo</summary>
    public List<string> GetVariablesInFile()
    {
        var path = Path.Combine(DefaultPathTemplateDirectory, DefaultPathTemplateSnippetsDirectory, ContentFileName);
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"No se encontró el archivo de plantilla {path}");
        }

        var content = File.ReadAllText(path);

        var matches = RegexSearchFilter.Matches(content);
        return [.. matches.Select(m => m.Groups[1].Value).Order()];
    }
}
