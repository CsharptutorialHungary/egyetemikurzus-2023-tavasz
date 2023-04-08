using GoogleBooks.Command;



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
    string? command = input?.Split(' ')[0];
    string? parameter = input?.Split(' ')[1];

    if (string.IsNullOrWhiteSpace(command))
        continue;

    try
    {
        if (commandTable.ContainsKey(command) is false)
        {
            throw new Exception($"Unknown command: {command}");
        }
        commandTable[command].Execute(parameter);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }

}
while (true);
