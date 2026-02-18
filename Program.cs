using System;
using System.Threading;
using System.Threading.Tasks;

namespace SingletonPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("═══════════════════════════════════════════════════════");
            Console.WriteLine("ПРАКТИЧНЕ ЗАНЯТТЯ: Реалізація шаблону Singleton");
            Console.WriteLine("═══════════════════════════════════════════════════════\n");

            // 1. Простий Singleton
            Console.WriteLine("1. ПРОСТИЙ SINGLETON");
            Console.WriteLine("─────────────────────────────────────────────────────");
            var singleton1 = SimpleSingleton.GetInstance();
            var singleton2 = SimpleSingleton.GetInstance();
            Console.WriteLine("Порівняння екземплярів: " + (singleton1 == singleton2 ? "Один і той самий" : "Різні"));
            singleton1.DoSomething();
            Console.WriteLine();

            // 2. Singleton з лінивою ініціалізацією
            Console.WriteLine("2. SINGLETON З ЛІНИВОЮ ІНІЦІАЛІЗАЦІЄЮ");
            Console.WriteLine("─────────────────────────────────────────────────────");
            Console.WriteLine("Створюю перший екземпляр...");
            var lazy1 = LazySingleton.GetInstance();
            Console.WriteLine("Створюю другий екземпляр...");
            var lazy2 = LazySingleton.GetInstance();
            Console.WriteLine("Порівняння екземплярів: " + (lazy1 == lazy2 ? "Один і той самий" : "Різні"));
            lazy1.DoSomething();
            Console.WriteLine();

            // 3. Потокобезпечний Singleton
            Console.WriteLine("3. ПОТОКОБЕЗПЕЧНИЙ SINGLETON");
            Console.WriteLine("─────────────────────────────────────────────────────");
            Console.WriteLine("Тестування потокобезпечності...");
            ThreadSafeSingleton instance1 = null;
            ThreadSafeSingleton instance2 = null;

            var thread1 = new Thread(() =>
            {
                instance1 = ThreadSafeSingleton.GetInstance();
            });

            var thread2 = new Thread(() =>
            {
                instance2 = ThreadSafeSingleton.GetInstance();
            });

            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();

            Console.WriteLine("Порівняння екземплярів з різних потоків: " + (instance1 == instance2 ? "Один і той самий" : "Різні"));
            instance1.DoSomething();
            Console.WriteLine();

            // 4. Використання Singleton у реальному проекті - DatabaseConnection
            Console.WriteLine("4. ВИКОРИСТАННЯ SINGLETON У РЕАЛЬНОМУ ПРОЕКТІ");
            Console.WriteLine("─────────────────────────────────────────────────────");
            Console.WriteLine("4.1. DatabaseConnection (єдине з'єднання з БД)");
            var db1 = DatabaseConnection.GetInstance();
            var db2 = DatabaseConnection.GetInstance();
            Console.WriteLine("Порівняння: " + (db1 == db2 ? "Один і той самий" : "Різні"));
            db1.Connect();
            db1.ExecuteQuery("SELECT * FROM Users");
            db2.ExecuteQuery("SELECT * FROM Products");
            Console.WriteLine();

            // 4.2. ConfigManager
            Console.WriteLine("4.2. ConfigManager (єдина конфігурація)");
            var config1 = ConfigManager.GetInstance();
            var config2 = ConfigManager.GetInstance();
            Console.WriteLine("Порівняння: " + (config1 == config2 ? "Один і той самий" : "Різні"));
            config1.PrintAllConfig();
            Console.WriteLine("Оновлюю конфігурацію через config1...");
            config1.SetConfig("Theme", "Light");
            Console.WriteLine("Читаю через config2: Theme = " + config2.GetConfig("Theme"));
            Console.WriteLine();

            // 5. Порівняння з іншими підходами
            Console.WriteLine("5. ПОРІВНЯННЯ З ІНШИМИ ПІДХОДАМИ");
            Console.WriteLine("─────────────────────────────────────────────────────");
            Console.WriteLine("Singleton vs Static Class:");
            Console.WriteLine("  Singleton:");
            Console.WriteLine("    + Можна наслідувати");
            Console.WriteLine("    + Можна передавати як параметр");
            Console.WriteLine("    + Можна реалізувати інтерфейси");
            Console.WriteLine("    + Лінива ініціалізація");
            Console.WriteLine("    - Складніша реалізація");
            Console.WriteLine();
            Console.WriteLine("  Static Class:");
            Console.WriteLine("    + Простіша реалізація");
            Console.WriteLine("    + Швидша робота");
            Console.WriteLine("    - Неможна наслідувати");
            Console.WriteLine("    - Неможна передавати як параметр");
            Console.WriteLine("    - Неможна реалізувати інтерфейси");
            Console.WriteLine();

            Console.WriteLine("Singleton vs Dependency Injection:");
            Console.WriteLine("  Singleton:");
            Console.WriteLine("    + Глобальний доступ");
            Console.WriteLine("    + Один екземпляр");
            Console.WriteLine("    - Важко тестувати");
            Console.WriteLine("    - Приховані залежності");
            Console.WriteLine();
            Console.WriteLine("  Dependency Injection:");
            Console.WriteLine("    + Легко тестувати");
            Console.WriteLine("    + Явні залежності");
            Console.WriteLine("    + Гнучкість");
            Console.WriteLine("    - Потрібен контейнер");
            Console.WriteLine();

            Console.WriteLine("═══════════════════════════════════════════════════════");
            Console.WriteLine("ВИСНОВОК:");
            Console.WriteLine("Singleton корисний для:");
            Console.WriteLine("  - З'єднань з базою даних");
            Console.WriteLine("  - Менеджерів конфігурації");
            Console.WriteLine("  - Логерів");
            Console.WriteLine("  - Кешів");
            Console.WriteLine("  - Ресурсів, які потребують єдиного екземпляра");
            Console.WriteLine("═══════════════════════════════════════════════════════");
        }
    }
}
