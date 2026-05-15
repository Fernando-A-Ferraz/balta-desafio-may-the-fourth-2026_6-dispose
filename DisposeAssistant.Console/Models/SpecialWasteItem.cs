namespace DisposeAssistant.Console.Models;

public class SpecialWasteItem
{
    public string Name { get; set; } = string.Empty;
    public string CollectionPoint { get; set; } = string.Empty;
    public bool ReminderEnabled { get; set; } = true;
}
