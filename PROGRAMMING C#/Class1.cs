using System;

namespace MassiividJaKordused
{
    class Program
    {
        static void Main(string[] args)
        {
            GenerateAndPrintSquares();

            CalculateArrayStats();

            CollectAndPrintUserData();

            RepeatUntilElephant();

            GuessTheNumber();

            PrintLargestFourNumber();

            PrintMultiplicationTable();

            Console.WriteLine("Нажмите любую клавишу для завершения...");
            Console.ReadKey();
        }

        //-----------------------------------------------1--------------------------------------------------
        static void GenerateAndPrintSquares()
        {
            // Генерация случайных чисел N и M
            Random rnd = new Random();
            int N = rnd.Next(-100, 101);
            int M = rnd.Next(-100, 101);

            // Определение начала и конца диапазона
            int start = Math.Min(N, M);
            int end = Math.Max(N, M);

            // Создание массива для чисел от start до end
            int[] numbers = new int[end - start + 1];
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = start + i;
            }

            // Вывод квадратов чисел на экран
            Console.WriteLine($"Числа от {start} до {end} и их квадраты:");
            foreach (int number in numbers)
            {
                Console.WriteLine($"{number}^2 = {number * number}");
            }
        }

        //-----------------------------------------------2--------------------------------------------------
        static void CalculateArrayStats()
        {
            // Массив для хранения пяти чисел
            int[] numbers = new int[5];

            // Запрос чисел у пользователя
            for (int i = 0; i < 5; i++)
            {
                Console.Write($"Введите число {i + 1}: ");
                numbers[i] = int.Parse(Console.ReadLine());
            }

            // Вычисление суммы, произведения и среднего арифметического
            int sum = 0;
            int product = 1;
            foreach (int num in numbers)
            {
                sum += num;
                product *= num;
            }
            double average = sum / 5.0;

            // Вывод результатов
            Console.WriteLine($"Сумма: {sum}");
            Console.WriteLine($"Среднее: {average}");
            Console.WriteLine($"Произведение: {product}");
        }

        //-----------------------------------------------3--------------------------------------------------
        static void CollectAndPrintUserData()
        {
            // Массивы для хранения имен и возрастов
            string[] names = new string[5];
            int[] ages = new int[5];

            // Запрос данных у пользователя
            for (int i = 0; i < 5; i++)
            {
                Console.Write($"Введите имя {i + 1}-го человека: ");
                names[i] = Console.ReadLine();
                Console.Write($"Введите возраст {names[i]}: ");
                ages[i] = int.Parse(Console.ReadLine());
            }

            // Вычисление общего возраста и среднего арифметического возраста
            int totalAge = 0;
            foreach (int age in ages)
            {
                totalAge += age;
            }
            double averageAge = totalAge / 5.0;

            // Поиск самого старшего и самого младшего человека
            int maxAge = ages[0];
            int minAge = ages[0];
            string oldestName = names[0];
            string youngestName = names[0];
            for (int i = 1; i < 5; i++)
            {
                if (ages[i] > maxAge)
                {
                    maxAge = ages[i];
                    oldestName = names[i];
                }
                if (ages[i] < minAge)
                {
                    minAge = ages[i];
                    youngestName = names[i];
                }
            }

            // Вывод результатов
            Console.WriteLine($"Общий возраст: {totalAge}");
            Console.WriteLine($"Средний возраст: {averageAge}");
            Console.WriteLine($"Самый старший: {oldestName}, возраст {maxAge}");
            Console.WriteLine($"Самый младший: {youngestName}, возраст {minAge}");
        }

        //-----------------------------------------------4--------------------------------------------------
        static void RepeatUntilElephant()
        {
            string answer;
            do
            {
                Console.WriteLine("Osta elevant ära!");
                answer = Console.ReadLine();
            } while (answer != "elevant");
        }

        //-----------------------------------------------5--------------------------------------------------
        static void GuessTheNumber()
        {
            Random rnd = new Random();
            int numberToGuess = rnd.Next(1, 101);
            int attempts = 0;
            int guess;
            do
            {
                Console.Write("Угадайте число от 1 до 100: ");
                guess = int.Parse(Console.ReadLine());
                attempts++;
                if (guess < numberToGuess)
                {
                    Console.WriteLine("Больше");
                }
                else if (guess > numberToGuess)
                {
                    Console.WriteLine("Меньше");
                }
            } while (guess != numberToGuess);
            Console.WriteLine($"Поздравляем! Вы угадали число {numberToGuess} за {attempts} попыток.");
        }

        //-----------------------------------------------6--------------------------------------------------
        static void PrintLargestFourNumber()
        {
            // Массив для хранения четырех чисел
            int[] numbers = new int[4];

            // Запрос чисел у пользователя
            for (int i = 0; i < 4; i++)
            {
                Console.Write($"Введите число {i + 1}: ");
                numbers[i] = int.Parse(Console.ReadLine());
            }

            // Сортировка массива в порядке убывания
            Array.Sort(numbers);
            Array.Reverse(numbers);

            // Формирование наибольшего четырехзначного числа
            int largestFourDigitNumber = numbers[0] * 1000 + numbers[1] * 100 + numbers[2] * 10 + numbers[3];

            // Вывод наибольшего четырехзначного

            Console.WriteLine($"Наибольшее четырехзначное число, составленное из введенных чисел: {largestFourDigitNumber}");
        }

        //-----------------------------------------------7--------------------------------------------------
        static void PrintMultiplicationTable()
        {
            // Вывод таблицы умножения
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    Console.Write($"{i * j,4}");
                }
                Console.WriteLine();
            }
        }
    }
}