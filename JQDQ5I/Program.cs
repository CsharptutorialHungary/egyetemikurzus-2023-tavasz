using GoogleBooks.Command;
using GoogleBooks.ViewCommon;

#region print info
Console.Title = "Google Books";
Console.WriteLine("GoogleBooks API version 1.0");
Console.WriteLine("Please enter full screen for the best experience.");
Console.WriteLine("Type \"help\" for more information.");
#endregion

Type t = typeof(ICommand);

var commandTypes = t.Assembly.GetTypes()
    .Where(type => type.IsAssignableTo(t)
    && !type.IsAbstract
    && !type.IsInterface);

var commandTable = commandTypes
            .Select(type => (ICommand)Activator.CreateInstance(type))
            .ToDictionary(command => command.Name, command => command);

do
{
    Console.Write("enter kommand> ");
    string? input = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(input))
        continue;

    string? command = input.Split(' ')[0];
    string[] parameters = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).ToArray();

    try
    {
        if (commandTable.ContainsKey(command) is false)
        {
            string exceptionMessage = $"Unknown command: {command}";

            string similarCommand = CommandHelper.FindMostSimilarWord(commandTable.Keys.ToList(), command);
            if (string.IsNullOrEmpty(similarCommand) is false)
                exceptionMessage += $"\nDid you mean {similarCommand}?";

            throw new Exception(exceptionMessage);
        }

        await Task.Run(() => commandTable[command].ExecuteAsync(parameters));
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }

}
while (true);

