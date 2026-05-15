using DisposeAssistant.Console.Models;

namespace DisposeAssistant.Console.Agents;

public class LocationAgent
{
    public string GetCollectionPoint(
        SpecialWasteItem item)
    {
        return item.CollectionPoint;
    }
}
