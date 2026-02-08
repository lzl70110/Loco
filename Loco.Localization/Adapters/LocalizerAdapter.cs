using Loco.Localization.Resources;
using Microsoft.Extensions.Localization;

namespace Loco.Localization.Adapters;

public interface ILocalizer
{
    string this[string key] { get; }
    string this[string key, params object[] args] { get; }
}

public sealed class LocalizerAdapter : ILocalizer
{
    private readonly IStringLocalizer<SharedResource> _inner;
    public LocalizerAdapter(IStringLocalizer<SharedResource> inner) => _inner = inner;

    public string this[string key] => _inner[key];
    public string this[string key, params object[] args] => _inner[key, args];
}