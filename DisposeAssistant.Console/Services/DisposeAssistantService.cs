using System.Text.Json;
using DisposeAssistant.Console.Agents;
using DisposeAssistant.Console.Models;

namespace DisposeAssistant.Console.Services;

public class DisposeAssistantService
{
    private readonly CollectionAgent _collectionAgent = new();
    private readonly ReminderAgent _reminderAgent = new();
    private readonly LocationAgent _locationAgent = new();

    private readonly string _collectionFile =
        "Data/collections.json";

    private readonly string _specialWasteFile =
        "Data/special-waste.json";

    private readonly List<WasteCollection> _collections;
    private readonly List<SpecialWasteItem> _specialItems;

    public DisposeAssistantService()
    {
        _collections = LoadCollections();
        _specialItems = LoadSpecialItems();
    }

    public bool HasData()
    {
        return _collections.Any() ||
               _specialItems.Any();
    }

    public void AddCollection(
        string wasteType,
        DayOfWeek day)
    {
        _collections.Add(new WasteCollection
        {
            WasteType = wasteType,
            CollectionDay = day
        });

        SaveCollections();
    }

    public void AddSpecialItem(
        string name,
        string collectionPoint)
    {
        _specialItems.Add(new SpecialWasteItem
        {
            Name = name,
            CollectionPoint = collectionPoint,
            ReminderEnabled = true
        });

        SaveSpecialItems();
    }

    public List<WasteCollection> GetTodayCollections()
    {
        return _collectionAgent
            .GetTodayCollections(_collections);
    }

    public List<SpecialWasteItem> GetSpecialReminders()
    {
        return _reminderAgent
            .GetEnabledReminders(_specialItems);
    }

    private List<WasteCollection> LoadCollections()
    {
        if (!File.Exists(_collectionFile))
            return [];

        var json = File.ReadAllText(_collectionFile);

        return JsonSerializer.Deserialize<List<WasteCollection>>(json)
               ?? [];
    }

    private List<SpecialWasteItem> LoadSpecialItems()
    {
        if (!File.Exists(_specialWasteFile))
            return [];

        var json = File.ReadAllText(_specialWasteFile);

        return JsonSerializer.Deserialize<List<SpecialWasteItem>>(json)
               ?? [];
    }

    private void SaveCollections()
    {
        Directory.CreateDirectory("Data");

        var json = JsonSerializer.Serialize(
            _collections,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

        File.WriteAllText(_collectionFile, json);
    }

    private void SaveSpecialItems()
    {
        Directory.CreateDirectory("Data");

        var json = JsonSerializer.Serialize(
            _specialItems,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

        File.WriteAllText(_specialWasteFile, json);
    }
}