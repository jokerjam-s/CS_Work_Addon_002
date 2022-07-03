/**
* У пользователя есть баланс в каждой из представленных валют. 
* С помощью команд он может попросить сконвертировать одну валюту в другую. 
* Курс конвертации просто описать в программе. 
* Программа заканчивает свою работу в тот момент, когда решит пользователь.
*
*/

/// Валютный баланс пользователя 
/// структура key=>Value (код валюты => сумма на балансе)
Dictionary<string, double> balance = new Dictionary<string, double>
{
    {"RUB", 0.0},
    {"EUR", 0.0},
    {"USD", 0.0}
};

/// курсы конверсии валют
Dictionary<string, double> conversion = new Dictionary<string, double>
{
    {"RUBEUR", 0.6},
    {"RUBUSD", 0.5},
    {"USDEUR", 0.9},
    {"USDRUB", 1.9},
    {"EURRUB", 1.7},
    {"EURUSD", 1.1}
};

/// Запрос вещественного числа от пользователя. 
///     message - сообщение пользователю
double InputDouble(string message)
{
    Console.Write(message);
    return Convert.ToDouble(Console.ReadLine());
}

/// Запрос строкового значения от пользователя. 
///     message - сообщение пользователю
string InputStr(string message)
{
    Console.Write(message);
    return Console.ReadLine();
}

/// Отображение баланса счетов пользователя
///     balance - структура key=>Value с описанием баланса 
void PrintBalance(Dictionary<string, double> balance)
{
    foreach (var item in balance)
    {
        Console.WriteLine(String.Format("{0}: {1,10:0.00}", item.Key, item.Value));
    }
}

/// Пополнение баланса пользователя. Запрашивает название валюты,
/// если наименование введенное пользователем есть в списке - 
/// запрашивает сумму для добавления, иначе выдает сообщение об ошибке.
///     balance - структура key=>Value с описанием баланса 
void AddBalance(Dictionary<string, double> balance)
{
    string questionStr = "Choise currency for append" + CurrencyLine(balance);

    string currencyName = InputStr(questionStr).ToUpper();

    if (balance.ContainsKey(currencyName))
    {
        double addSum = InputDouble("Sum for add: ");
        balance[currencyName] += addSum;
    }
    else
    {
        Console.WriteLine("Wrong currency name!");
    }
}

/// Вывод справки о системе
void PrintHelp()
{
    Console.WriteLine("System help ");
    Console.WriteLine("\thelp    - print program help");
    Console.WriteLine("\tbalance - show balance");
    Console.WriteLine("\tadd     - append sum to account");
    Console.WriteLine("\tconvert - convert currency");
    Console.WriteLine("\texit    - close program");
}

/// Конвертация из одной валюты в другую
/// Запрашивает из какой валюты и в какую будет проводится перевод. 
/// Если польщователь вводит недопустимые значения валют - выдает сообщение
/// о невозможности проведения операции. Также счет источник не должен быть
/// пустым ( >0 )
///     balance - структура key=>Value с описанием баланса 
///     convercion - индексы конвертации
void ConvertCurrency(Dictionary<string, double> balance, Dictionary<string, double> conversion)
{
    string currencyLine = CurrencyLine(balance);

    string accountSource = InputStr($"Account of source{currencyLine}").ToUpper();
    string accountDestination = InputStr($"Account of destination{currencyLine}").ToUpper();

    if (balance.ContainsKey(accountSource) && balance.ContainsKey(accountDestination))
    {
        string conversionCode = accountSource + accountDestination;
        if (conversion.ContainsKey(conversionCode))
        {
            double sourceSum = balance[accountSource];

            if (sourceSum > 0)
            {
                double conversionKoef = conversion[conversionCode];
                balance[accountDestination] += sourceSum * conversionKoef;
                balance[accountSource] = 0.0;
            }
            else
            {
                Console.WriteLine("Account of source is empty!");
            }
        }
        else
        {
            Console.WriteLine("Unsupported operation!");
        }
    }
    else
    {
        Console.WriteLine("Wrond currency name!");
    }
}

/// Строка списка валют.
/// Формируется из ключей списка баланса.
///     balance - структура key=>Value с описанием баланса 
string CurrencyLine(Dictionary<string, double> balance)
{
    string currencyLine = " ( ";
    foreach (var item in balance)
    {
        currencyLine += $"{item.Key} ";
    }
    currencyLine += "): ";

    return currencyLine;
}


/// main body
Console.Clear();

while (true)
{
    string command = InputStr("> ").ToLower();

    if (command == "help")
    {
        PrintHelp();
    }
    else if (command == "balance")
    {
        PrintBalance(balance);
    }
    else if (command == "add")
    {
        AddBalance(balance);
    }
    else if (command == "convert")
    {
        ConvertCurrency(balance, conversion);
    }
    else if (command == "exit")
    {
        break;
    }
    else
    {
        Console.WriteLine("Wrong command. Use command 'help' for showing system help");
    }
}
