namespace Wispreadki.Logic.Models;

/// <summary>Variable de plantilla de Wiki</summary>
public sealed class WikiVariable
{
    /// <summary>Código de la variable</summary>
    public required string Code { get; set; }
    /// <summary>Soportado: default, multiple, hyperlink (external), anchor (internal)</summary>
	public required string Type { get; set; }
    /// <summary>Valor por defecto</summary>
    public string DefaultValue { get; set; } = string.Empty;
    /// <summary>Valores disponibles para combo</summary>
    public List<string> AvailableValues { get; set; } = [];
    /// <summary>Valor seleccionado o escrito</summary>
	public string Value { get; set; } = string.Empty;
    /// <summary>Título a mostrar en caso de ser un anchor o hyperlink</summary>
    public string Title { get; set; } = string.Empty;
    /// <summary>Valores multiples de la variable</summary>
    public List<KeyValuePair<string, string>> Values { get; set; } = [];

    /// <summary>Indica si es un combo</summary>
	public bool IsCombo => AvailableValues.Count > 0;
    /// <summary>Indica si la plantilla es múltiple</summary>
    /// <remarks>Debe contener un "for 'mycode'" en la plantilla base para que funcione correctamente</remarks>
    public bool IsMultiple => Code.StartsWith("for ", StringComparison.InvariantCultureIgnoreCase);
    /// <summary>Obtiene el código de la plantilla</summary>
    public string GetCode() => Code.Replace("for ", string.Empty, StringComparison.InvariantCultureIgnoreCase);

    /// <summary>Obtiene el contenido de la variable</summary>
    public string GetContent() => Type.ToLower() switch
    {
        "hyperlink" or "anchor" => $"[{GetValueOrDefault()}]({GetTitle()})",
        _ => GetValueOrDefault()
    };

    /// <summary>Obtiene el valor por defecto si no hay valor</summary>
    public string GetValueOrDefault()
    {
        if (IsCombo && !IsMultiple)
        {
            return AvailableValues.Contains(Value, StringComparer.InvariantCultureIgnoreCase) ? Value : DefaultValue;
        }

        if (IsMultiple)
        {
            StringBuilder replaceContent = new();

            if (Values.Count > 0)
            {
                foreach (var data in Values)
                {
                    replaceContent = replaceContent.AppendLine($"- {data.Key} => {data.Value}");
                }
            }
            else
            {
                replaceContent = replaceContent.AppendLine($"- {DefaultValue}");
            }

            return replaceContent.ToString();
        }


        return string.IsNullOrEmpty(Value) ? DefaultValue : Value;
    }

    /// <summary>Obtiene el título si no hay valor</summary>
    public string GetTitle() => !string.IsNullOrEmpty(Title) ? Title : GetValueOrDefault();
}