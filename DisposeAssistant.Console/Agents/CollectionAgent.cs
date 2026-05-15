using DisposeAssistant.Console.Models;

namespace DisposeAssistant.Console.Agents;

public class CollectionAgent
{
    public List<WasteCollection> GetTodayCollections(
        List<WasteCollection> collections)
    {
        return collections
            .Where(c => c.IsToday)
            .ToList();
    }
}