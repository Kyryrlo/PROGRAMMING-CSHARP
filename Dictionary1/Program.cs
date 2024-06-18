using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static string GetFilePath(string fileName)
    {
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        return Path.Combine(basePath, fileName);
    }

    // Читает содержимое файлов eng.txt и rus.txt и возвращает их в виде списков строк
    static (List<string>, List<string>) FilesInLists()
    {
        string engFilePath = GetFilePath("eng.txt");
        string rusFilePath = GetFilePath("rus.txt");

        List<string> eList = new List<string>();
        List<string> rList = new List<string>();

        try
        {
            // Чтение всех строк из файлов и приведение их к нижнему регистру
            eList = File.ReadAllLines(engFilePath).Select(line => line.Trim().ToLower()).ToList();
            rList = File.ReadAllLines(rusFilePath).Select(line => line.Trim().ToLower()).ToList();
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine($"Ошибка: файл не найден. {e.FileName}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Произошла ошибка: {e.Message}");
        }

        return (eList, rList);
    }

    // Находит перевод слова в списках слов и возвращает его
    static (string, string) WordTranslation(string uWord)
    {
        string v = null;
        var (eList, rList) = FilesInLists();

        // Проверка наличия слова в английском списке
        if (eList.Contains(uWord.ToLower()))
        {
            int eInd = eList.IndexOf(uWord.ToLower());
            v = rList[eInd];
        }
        // Проверка наличия слова в русском списке
        else if (rList.Contains(uWord.ToLower()))
        {
            int rInd = rList.IndexOf(uWord.ToLower());
            v = eList[rInd];
        }

        return (v, uWord);
    }

    // Добавляет новое слово и его перевод в соответствующие списки и файлы
    static void WordInsertation(int lang, string word, string translation)
    {
        var (eList, rList) = FilesInLists();

        if (lang == 1)
        {
            eList.Add(word.ToLower());
            rList.Add(translation.ToLower());
        }
        else if (lang == 2)
        {
            rList.Add(word.ToLower());
            eList.Add(translation.ToLower());
        }

        // Запись обновленных списков обратно в файлы
        File.WriteAllLines(GetFilePath("eng.txt"), eList);
        File.WriteAllLines(GetFilePath("rus.txt"), rList);

        Console.WriteLine("Перевод добавлен.");
    }

    // Исправляет перевод слова в списках и файлах
    static void DictionaryCorrection(string word, string corWord)
    {
        var (eList, rList) = FilesInLists();

        // Поиск и исправление слова в русском списке
        if (rList.Contains(word.ToLower()))
        {
            int wordInd = rList.IndexOf(word.ToLower());
            rList[wordInd] = corWord.ToLower();
        }
        // Поиск и исправление слова в английском списке
        else if (eList.Contains(word.ToLower()))
        {
            int wordInd = eList.IndexOf(word.ToLower());
            eList[wordInd] = corWord.ToLower();
        }

        // Запись обновленных списков обратно в файлы
        File.WriteAllLines(GetFilePath("eng.txt"), eList);
        File.WriteAllLines(GetFilePath("rus.txt"), rList);

        Console.WriteLine("Исправленный перевод записан.");
    }

    // Проводит тестирование знаний пользователя и возвращает количество правильных и общих ответов
    static (int, int) KnowledgeControl()
    {
        int corAnsw = 0;
        int questions = 0;
        var (eList, rList) = FilesInLists();
        Random rand = new Random();

        // Проверка наличия слов в списках
        if (eList.Count == 0 || rList.Count == 0)
        {
            Console.WriteLine("Списки слов пусты, невозможно провести контроль знаний.");
            return (corAnsw, questions);
        }

        while (true)
        {
            // Случайный выбор языка и слова для перевода
            int v = rand.Next(1, 3);
            int num = rand.Next(0, eList.Count);

            string userAnsw;
            if (v == 1)
            {
                Console.Write($"Введите перевод для {rList[num]} (Для того чтобы прекратить - введите stop): ");
                userAnsw = Console.ReadLine().ToLower();
                if (userAnsw == "stop") break;
                if (userAnsw == eList[num])
                {
                    questions++;
                    corAnsw++;
                    Console.WriteLine("Молодец, правильно!");
                }
                else
                {
                    questions++;
                    Console.WriteLine("Неправильно");
                }
            }
            else
            {
                Console.Write($"Введите перевод для {eList[num]} (Для того чтобы прекратить - введите stop): ");
                userAnsw = Console.ReadLine().ToLower();
                if (userAnsw == "stop") break;
                if (userAnsw == rList[num])
                {
                    questions++;
                    corAnsw++;
                    Console.WriteLine("Молодец, правильно!");
                }
                else
                {
                    questions++;
                    Console.WriteLine("Неправильно");
                }
            }
        }

        return (corAnsw, questions);
    }

    // Основная функция программы
    static void Main()
    {
        

        bool wan = false;
        while (true)
        {
            try
            {
                // Спрашиваем пользователя, хочет ли он пройти тестирование
                Console.Write("Хотите проверить свои знания английского?(1-Да, 2-Нет): ");
                int choice = int.Parse(Console.ReadLine());
                wan = choice == 1;
                break;
            }
            catch
            {
                Console.WriteLine("Вы ввели неправильное значение");
            }
        }

        if (wan)
        {
            // Запуск функции контроля знаний
            var (corAnsw, questions) = KnowledgeControl();
            if (questions > 0)
            {
                double percent = (double)corAnsw / questions * 100;
                Console.WriteLine($"Вы ответили правильно на {percent} процентов ответов");
            }
            else
            {
                Console.WriteLine("Нет вопросов для проверки.");
            }
        }

        // Запрос слова для перевода
        Console.Write("Введите слово, которое вы хотите перевести: ");
        string uWord = Console.ReadLine();
        var (v, ursWord) = WordTranslation(uWord);

        if (v != null)
        {
            Console.WriteLine($"Ваше слово {ursWord} переводится как {v}");
        }
        else
        {
            Console.WriteLine("Ваше слово отсутствует у нас в словаре");
        }

        // Спрашиваем пользователя, хочет ли он добавить перевод
        Console.Write("Хотите добавить перевод?(1-Да, 2-Нет): ");
        int wanAdd = int.Parse(Console.ReadLine());

        if (wanAdd == 1)
        {
            // Запрос языка и слов для добавления
            Console.Write("Введите язык первого слова (1-English/2-Russian): ");
            int lang = int.Parse(Console.ReadLine());
            Console.Write("Введите слово: ");
            string word = Console.ReadLine();
            Console.Write("Введите перевод: ");
            string translation = Console.ReadLine();
            WordInsertation(lang, word, translation);
        }
        else
        {
            bool wantCorrection = false;
            while (true)
            {
                try
                {
                    // Спрашиваем пользователя, хочет ли он внести коррективы
                    Console.Write("Вы считаете перевод корректным, или хотите внести коррективы? (1-Да, 2-Нет): ");
                    int choice = int.Parse(Console.ReadLine());
                    wantCorrection = choice == 1;
                    break;
                }
                catch
                {
                    Console.WriteLine("Вы ввели неправильное значение");
                }
            }

            if (wantCorrection)
            {
                // Запрос правильного перевода для исправления
                Console.Write("Введите правильный перевод: ");
                string corWord = Console.ReadLine();
                DictionaryCorrection(uWord, corWord);
            }
        }
    }
}
