// ФУНКЦИИ
int InputChecker(string userMessage, string exeptionMessage) // Функция для защиты от ввода НЕ-чисел
{
    Console.Write(userMessage);
    try
    {
        return Convert.ToInt32(Console.ReadLine());   
    }
    catch
    {
        Console.WriteLine(exeptionMessage);
    }
    return InputChecker(userMessage, exeptionMessage);  
}

int InputNaturalChecker (string userMessage, string exeptionMessage) // Функция для ввода только натуральных чисел
{
    int naturalNumber;
    while (true)
    {
        naturalNumber = InputChecker(userMessage, exeptionMessage);
        if (naturalNumber < 0) Console.WriteLine(exeptionMessage);
        else break;
    }
    return naturalNumber;
}

string EndingChanger(int num, string one, string two, string many) // Функция для замены окончания, на входе числительное и окончания
                                                                   // для один -ист, два -иста, много -истов, окончания для разных
                                                                   // слов могут быть разные, поэтому посылаем их в функцию
{
    int n = num % 100;
    string ending = string.Empty; // это наше окончание, его будем возвращать

    if (n / 10 == 1) ending = many;                        // предпоследний разряд - 1
    else  // в этом ветвлении проверяем последний разряд
    {
        if (n % 10 == 1) ending = one;                     // последний разряд - 1
        else if (n % 10 >= 2 && n % 10 <= 4) ending = two; // последний разряд - 2..4
        else ending = many;                                // последний разряд - 5..9 или 0
    }
    return ending;
}

string[] StringArrayInput() // поскольку в задаче предлагается не использовать множества,
                            // но изначально не известно количество вводимых пользователем строк,
                            // сделаем свою реализаци динамического массива.
                            // Единственное ограничение - пустая строка в таком виде не сможет
                            // быть элементом массива
{
    int currentPositionIndex = 0, // номер позиции (или иначе, количество уже введёных элементов) надо отслеживать 
        currentArrayNumber = 0,   // это номер массива в нашем блоке массивов
        currentMaxIndex = 4,      // это максимальный размер текущего массива, начнём с 4 элементов, степень двойки
        arraySizeMultiplier = 2;  // каждый следующий массив будем удваивать

    string [][] megaArray = new string [30][]; // примерно 2 млрд чисел, думаю, хватит. Однако не знаю, можно ли сделать потенциально бесконечный ввод
    megaArray [0] = new string [currentMaxIndex]; 

    string nextInput = string.Empty;
    
    while (true) 
    { 
        Console.Write($"Введите {currentPositionIndex + 1}-ю строку: ");        
        nextInput = Console.ReadLine(); // сначала считываем строковое значение, проверям,
                                        // является ли пустой строкой и либо заканчиваем, 
                                        // либо уже остальные проверки мутим

        if (nextInput == "") break; // выходим из цикла While
        else
        {          
            if (currentMaxIndex == currentPositionIndex + 1) // как только текущий массив заполняется
                                                             // мы перекидываем все значения из него в следующий
                                                             // в два раза больший массив и продолжаем работу
            {
                megaArray [currentArrayNumber + 1] = new string [currentMaxIndex * arraySizeMultiplier];

                for (int i = 0; i < megaArray[currentArrayNumber].Length; i++)
                    megaArray[currentArrayNumber + 1][i] = megaArray[currentArrayNumber][i];

                currentMaxIndex *= arraySizeMultiplier; //следующий массив в два раз больше
                currentArrayNumber++;
            }
            megaArray [currentArrayNumber] [currentPositionIndex] = nextInput;
            currentPositionIndex++;
        }                       
    } 

    Console.Write($"\nВы ввели {currentPositionIndex} строк{EndingChanger(currentPositionIndex,"у","и","")}. ");

    string [] stringArray = new string [currentPositionIndex]; // наконец, мы создаём наш массив со строками
    for (int i = 0; i < currentPositionIndex; i++)
                        stringArray[i] = megaArray[currentArrayNumber][i];

    return stringArray;
}

// ГЛАВНЫЙ КОД 
Console.Clear();
Console.WriteLine("\n    --- Итоговая проверочная работа ---");
Console.WriteLine("Задача: Написать программу, которая из имеющегося массива строк формирует "
                  + "новый массив из строк, длина которых меньше, либо равна 3 символам. "
                  + "Первоначальный массив можно ввести с клавиатуры, либо задать на старте "
                  + "выполнения алгоритма. При решении не рекомендуется пользоваться "
                  + "коллекциями, лучше обойтись исключительно массивами.\n");

Console.WriteLine("Последовательно, через 'Enter' введите строковые элементы массива. Для окончания ввода - введите пустую строку."); 
string[] initialArray = StringArrayInput();
string list = string.Join(", ", initialArray);
Console.WriteLine($"Все введённые строки:\n[{list}]\n"); 

Console.WriteLine("Укажите ограничение по количеству символов в строке в новом массиве (три по условию задачи)"); 
int maxBound = InputNaturalChecker("Введите число: ", "Только натуральные числа! (ctrl+c для экстренного прерывания)"); 

int count = 0; // количество элементов, отвечающих условию задачи
foreach (string item in initialArray)
    if (item.Length <= maxBound) count++;
Console.WriteLine($"\nВ введённом масиве {count} строк{EndingChanger(count,"а","и","")} соответству{EndingChanger(count,"ет","ют","ют")} заданному условию\n"); 
string [] newArray = new string[count];

count = 0; // обнуляем счётчик
foreach (string item in initialArray)
    if (item.Length <= maxBound)
    {
        newArray[count] = item;
        count++; 
    }

list = string.Join(", ", newArray);
Console.WriteLine($"Строки, отвечающие условию:\n[{list}]"); 
Console.WriteLine("   --- Конец задачи ---\n");