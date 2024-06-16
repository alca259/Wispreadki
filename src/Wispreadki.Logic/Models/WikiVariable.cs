namespace Wispreadki.Logic.Models;

public sealed class WikiVariable
{
    /// <summary>Soportado: default, h1, h2, h3, h4, h5, h6, hyperlink (external), anchor (internal)</summary>
	public required string Type { get; set; }
    /// <summary>Valor por defecto</summary>
    public string DefaultValue { get; set; } = string.Empty;
    /// <summary>Valores disponibles para combo</summary>
    public List<string> AvailableValues { get; set; } = [];
    /// <summary>Valor seleccionado o escrito</summary>
	public string Value { get; set; } = string.Empty;
    /// <summary>Título a mostrar en caso de ser un anchor o hyperlink</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>Indica si es un combo</summary>
	public bool IsCombo => AvailableValues.Count > 0;

    /// <summary>Obtiene el contenido de la variable</summary>
    public string GetContent() => Type switch
    {
        "h1" => $"# {GetValueOrDefault()}",
        "h2" => $"## {GetValueOrDefault()}",
        "h3" => $"### {GetValueOrDefault()}",
        "h4" => $"#### {GetValueOrDefault()}",
        "h5" => $"##### {GetValueOrDefault()}",
        "h6" => $"###### {GetValueOrDefault()}",
        "hyperlink" => $"[{GetValueOrDefault()}]({Title})",
        "anchor" => $"[{GetValueOrDefault()}](#{Title})",
        _ => GetValueOrDefault()
    };

    public string GetValueOrDefault() => string.IsNullOrEmpty(Value) ? DefaultValue : Value;
}