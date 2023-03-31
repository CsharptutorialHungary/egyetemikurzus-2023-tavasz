using reflection_linq;

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
    Console.Write("enter command> ");
    string? input = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(input))
        continue;

    if (commandTable.ContainsKey(input))
    {
        try
        {
            commandTable[input].Execute();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    else
    {
        Console.WriteLine("Unknown command");
    }
}
while (true);