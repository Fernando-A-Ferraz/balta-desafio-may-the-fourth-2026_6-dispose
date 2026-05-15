using DisposeAssistant.Console.Services;

Console.Title = "Dispose Assistant - May The Fourth 2026";

ExibirCabecalho();

var disposeService = new DisposeAssistantService();

if (!disposeService.HasData())
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.Write("🗑️ Quantos tipos de coleta deseja cadastrar? ");
    Console.ResetColor();

    var collectionInput = Console.ReadLine();

    if (!int.TryParse(collectionInput, out var totalCollections))
    {
        Console.WriteLine("❌ Valor inválido.");
        return;
    }

    for (int i = 1; i <= totalCollections; i++)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"♻️ Coleta {i}");
        Console.ResetColor();

        Console.Write("Tipo de lixo: ");
        var wasteType = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Dia da coleta:");
        Console.WriteLine("1 - Domingo");
        Console.WriteLine("2 - Segunda");
        Console.WriteLine("3 - Terça");
        Console.WriteLine("4 - Quarta");
        Console.WriteLine("5 - Quinta");
        Console.WriteLine("6 - Sexta");
        Console.WriteLine("7 - Sábado");

        Console.Write("Escolha: ");
        var dayInput = Console.ReadLine();

        if (!int.TryParse(dayInput, out var day))
            continue;

        var dayOfWeek = day switch
        {
            1 => DayOfWeek.Sunday,
            2 => DayOfWeek.Monday,
            3 => DayOfWeek.Tuesday,
            4 => DayOfWeek.Wednesday,
            5 => DayOfWeek.Thursday,
            6 => DayOfWeek.Friday,
            7 => DayOfWeek.Saturday,
            _ => DayOfWeek.Monday
        };

        disposeService.AddCollection(
            wasteType,
            dayOfWeek);
    }

    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.Write("🔋 Quantos itens especiais deseja cadastrar? ");
    Console.ResetColor();

    var specialInput = Console.ReadLine();

    if (int.TryParse(specialInput, out var totalSpecial))
    {
        for (int i = 1; i <= totalSpecial; i++)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"📦 Item especial {i}");
            Console.ResetColor();

            Console.Write("Nome do item: ");
            var name = Console.ReadLine() ?? string.Empty;

            Console.Write("Ponto de coleta: ");
            var collectionPoint = Console.ReadLine() ?? string.Empty;

            disposeService.AddSpecialItem(
                name,
                collectionPoint);
        }
    }

    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("✅ Dados cadastrados com sucesso!");
    Console.ResetColor();
}
else
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("✅ Dados carregados dos arquivos JSON.");
    Console.ResetColor();
}

Console.WriteLine();
Console.ForegroundColor = ConsoleColor.Magenta;
Console.WriteLine("📅 Coletas do dia:");
Console.ResetColor();

var todayCollections = disposeService.GetTodayCollections();

if (todayCollections.Count == 0)
{
    Console.WriteLine("Nenhuma coleta programada para hoje.");
}
else
{
    foreach (var collection in todayCollections)
    {
        Console.WriteLine($"♻️ {collection.WasteType}");
    }
}

Console.WriteLine();
Console.ForegroundColor = ConsoleColor.Magenta;
Console.WriteLine("🔔 Lembretes especiais:");
Console.ResetColor();

var reminders = disposeService.GetSpecialReminders();

foreach (var item in reminders)
{
    Console.WriteLine(
        $"📦 {item.Name} → Levar para: {item.CollectionPoint}");
}

Console.WriteLine();
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("🎉 Parabéns! Você concluiu o May The Fourth 2026!");
Console.ResetColor();

Console.ForegroundColor = ConsoleColor.DarkGray;
Console.WriteLine("Pressione qualquer tecla para encerrar...");
Console.ResetColor();

Console.ReadKey();

static void ExibirCabecalho()
{
    Console.ForegroundColor = ConsoleColor.Magenta;

    Console.WriteLine("======================================");
    Console.WriteLine(" 🌌 MAY THE FOURTH 2026");
    Console.WriteLine("     DISPOSE ASSISTANT");
    Console.WriteLine("======================================");

    Console.ResetColor();
    Console.WriteLine();
}
