namespace Wispreadki.Logic.Models;

/// <summary>Configuración de Wiki</summary>
public sealed class WikiConfig
{
    /// <summary>Lista de plantillas</summary>
    /// <remarks>Valores de reemplazo {{body}} buscarán un archivo de plantilla interna con ese nombre en directorio y subdirectorios (body.json)</remarks>
    public List<WikiTemplate> Templates { get; set; } = [];

    /// <summary>Obtiene el contenido de la plantilla con las variables reemplazadas</summary>
    /// <param name="inputData">Datos de entrada para las plantillas</param>
    public string GetContent(List<WikiTemplate> inputData)
    {
        var path = Path.Combine(DefaultPathTemplateDirectory, DefaultTemplateFileName);
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"No se encontró el archivo de plantilla {path}");
        }

        var content = File.ReadAllText(path);
        foreach (var template in Templates.OrderBy(o => o.Order))
        {
            var realCode = template.GetCode();

            if (template.IsMultiple && inputData.Exists(e => e.GetCode() == realCode))
            {   
                var templateData = inputData.Where(t => t.GetCode() == realCode).OrderBy(o => o.Order).ToList();

                StringBuilder replaceContent = new();

                foreach (var data in templateData)
                {
                    var templateContent = data.GetContent();
                    replaceContent = replaceContent.AppendLine(templateContent);
                }

                content = content.Replace($"{{{{{template.Code}}}}}", replaceContent.ToString());
                continue;
            }

            content = content.Replace($"{{{{{template.Code}}}}}", template.GetContent());
        }

        return content;
    }

    /// <summary>Obtiene las plantillas en el archivo</summary>
    public static List<string> GetTemplatesInFile()
    {
        var path = Path.Combine(DefaultPathTemplateDirectory, DefaultPathTemplateSnippetsDirectory);
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"No se encontró el archivo de plantilla {path}");
        }

        var content = File.ReadAllText(path);
        var matches = RegexSearchFilter.Matches(content);
        return [.. matches.Select(m => m.Groups[1].Value).Order()];
    }
}
