using Character_Zem;
using System;
using System.ComponentModel;
using System.Globalization;

public class Game
{
    
    List<Character> list = new List<Character>();
    public void Menu()
    {
        while (true)
        {
            Console.WriteLine("" +
                        "Выберите действие:\n" +
                        "1. Создать персонажа\n" +
                        "2. Вывод информации о персонаже\n" +
                        "3. Переключение персонажа\n" +
                        "0. Выйти из приложения\n");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 0:
                    return;
                case 1:
                    CreateCharacter();
                    break;
                case 2:
                    InfoOutAll();
                    break;
                case 3:
                    Switch();
                    break;

            }
        }
        
    }

    public void CreateCharacter()
    {
        Console.WriteLine("\nВведите имя персонажа: ");
        string name = Console.ReadLine();
        Console.WriteLine("Введите координату x: ");
        int x = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Введите координату y: ");
        int y = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Введите принадлежность к лагерю: ");
        bool camp = Convert.ToBoolean(Console.ReadLine());
        Console.WriteLine("Введите максимальное количество HP: ");
        int HP = Convert.ToInt32(Console.ReadLine());
        bool life = true;
        int winsScore = 0;
        

        Character character = new Character(name, x, y, camp, HP, life, winsScore);
        

        list.Add(character);
        

        Console.WriteLine("\nПерсонаж успешно создан!\n");
    }

    public void InfoOutAll()
    {
        foreach (Character character in list)
        Console.WriteLine(
            $"--------------------------------\n" +
            $"Имя: {character._name}\n" +
            $"Координаты: {character._x};{character._y}\n" +
            $"Лагерь: {character._camp}\n" +
            $"Масимальное/нынешнее HP: {character._max_HP}/{character._current_HP}\n" +
            $"--------------------------------\n");
    }

    public void InfoOut(Character currentchar)
    {
        {
            Console.WriteLine(
                "--------------------------------\n" +
                $"Имя: {currentchar._name}\n" +
                $"Координаты: {currentchar._x};{currentchar._y}\n" +
                $"Лагерь: {currentchar._camp}\n" +
                $"Масимальное/нынешнее HP: {currentchar._max_HP}/{currentchar._current_HP}\n" +
                $"--------------------------------\n");
        }
    }

    public void Switch()
    {
        Console.WriteLine("Выберите персонажа:");

        for (int i = 0; i < list.Count; i++)
        {
            Console.WriteLine($"{i + 1} {list[i]._name}");
        }

        int choice = Convert.ToInt32(Console.ReadLine());

        if (choice > 0 && choice <= list.Count) 
        {
            Character currentchar = list[choice - 1];
            Console.WriteLine($"\nПерсонаж {currentchar._name} выбран\n");

            while (true)
            {
                Console.WriteLine(
                    "Выберите действие персонажа\n" +
                    "1. Показать инфу о персонаже\n" +
                    "2. Движение\n" +
                    "0. Выход\n");
                int perschoice = Convert.ToInt32(Console.ReadLine());

                switch (perschoice)
                {
                    case 0:
                        return;
                    case 1:
                        InfoOut(currentchar);
                        break;
                    case 2:
                        Move(currentchar);
                        break;
                }
            }
        }     
    }

    public void Move(Character currentchar)
    {
        while (true)
        {
            Console.WriteLine("\nВыбирети одно направление:");
            Console.WriteLine("\n");
            Console.WriteLine("                Вверх(8)                 ");
            Console.WriteLine("Влево(4)                        Вправо(6)");
            Console.WriteLine("                Вниз(2)                   ");
            Console.WriteLine("\n");
            Console.WriteLine("Нажмите 0 чтоб прекратить движение.");

            int move = Convert.ToInt32(Console.ReadLine());

            switch (move)
            {
                case 0:
                    return;
                case 8:
                    if (currentchar._y > 10)
                    {
                        Console.WriteLine("Вы не можете пройти дальше!\n");
                    }
                    else
                    {
                        currentchar._y++;
                    }
                    break;
                case 2:
                    if (currentchar._y < -9)
                    {
                        Console.WriteLine("Вы не можете пройти дальше!\n");
                    }
                    else
                    {
                        currentchar._y--;
                    }
                    break;
                case 4:
                    if (currentchar._x < -9)
                    {
                        Console.WriteLine("Вы не можете пройти дальше!\n");
                    }
                    else
                    {
                        currentchar._x--;
                    }
                    break;
                case 6:
                    if (currentchar._x > 10)
                    {
                        Console.WriteLine("Вы не можете пройти дальше!\n");
                    }
                    else
                    {
                        currentchar._x++;
                    }
                    break;

            }

            foreach (Character otherChar in list)
            {
                if (otherChar != currentchar)
                {
                    Battle(currentchar, otherChar);
                }
            }

        }
    }

    public void Battle(Character currentchar, Character otherChar) //Метод боя
    {
        if (currentchar._x == otherChar._x && currentchar._y == otherChar._y && currentchar._camp != otherChar._camp) //Условия для битвы
        {
            Console.WriteLine("\nВраг обноружен. Битва началась!\n");
            Random random = new Random();

            while (currentchar._current_HP > 0 && otherChar._current_HP > 0) //Цикл пока один из врагов не умрёт
            {
                // Первым бьет текущий персонаж
                int currentCharDamage = random.Next(1, currentchar._max_HP); //Урон рандомиться
                otherChar._current_HP -= currentCharDamage;
                Console.WriteLine($"'{currentchar._name}' атакует '{otherChar._name}' и наносит '{currentCharDamage}' урона.");

                if (otherChar._current_HP <= 0) //Условия если враг умер
                {
                    Console.WriteLine($"{otherChar._name} повержен!");
                    currentchar._winsScore++;
                    break;
                }

                // Затем атакует второй персонаж
                int otherCharDamage = random.Next(1, otherChar._max_HP);
                currentchar._current_HP -= otherCharDamage;
                Console.WriteLine($"'{otherChar._name}' атакует '{currentchar._name}' и наносит '{otherCharDamage}' урона.");

                if (currentchar._current_HP <= 0) //Условия если наш персонаж умер
                {
                    Console.WriteLine($"{currentchar._name} повержен!");
                    Console.WriteLine("GAME OVER");
                    otherChar._winsScore++;
                    Console.WriteLine("Хотите переключиться на другого персонажа");
                    break;
                }
            }
        }
        else
        {
            Console.WriteLine("Персонажи не столкнулись или принадлежат одному лагерю, битва не произошла.");
        }
    }
}