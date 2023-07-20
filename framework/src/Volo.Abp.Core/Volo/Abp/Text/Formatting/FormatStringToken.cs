namespace Volo.Abp.Text.Formatting;

/// <summary>
/// 学到这里
/// </summary>
internal class FormatStringToken
{
    public string Text { get; private set; }

    public FormatStringTokenType Type { get; private set; }

    public FormatStringToken(string text, FormatStringTokenType type)
    {
        Text = text;
        Type = type;
    }
}
