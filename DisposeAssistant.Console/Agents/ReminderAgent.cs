using DisposeAssistant.Console.Models;

namespace DisposeAssistant.Console.Agents;

public class ReminderAgent
{
    public List<SpecialWasteItem> GetEnabledReminders(
        List<SpecialWasteItem> items)
    {
        return items
            .Where(i => i.ReminderEnabled)
            .ToList();
    }
}

