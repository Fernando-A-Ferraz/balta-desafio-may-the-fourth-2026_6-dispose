namespace DisposeAssistant.Console.Models;

public class WasteCollection
{
    public string WasteType { get; set; } = string.Empty;
    public DayOfWeek CollectionDay { get; set; }

    public bool IsToday =>
        DateTime.Today.DayOfWeek == CollectionDay;
}
