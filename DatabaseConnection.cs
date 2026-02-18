namespace SingletonPractice
{
    public class DatabaseConnection
    {
        private static DatabaseConnection _instance;
        private static readonly object _lock = new object();
        private string _connectionString;
        private bool _isConnected = false;

        private DatabaseConnection()
        {
            _connectionString = "Server=localhost;Database=MyDB;User=admin;Password=12345";
            Console.WriteLine("DatabaseConnection: Створено з'єднання з базою даних");
        }

        public static DatabaseConnection GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new DatabaseConnection();
                    }
                }
            }
            return _instance;
        }

        public void Connect()
        {
            if (!_isConnected)
            {
                Console.WriteLine("Підключення до бази даних: " + _connectionString);
                _isConnected = true;
                Console.WriteLine("Підключено успішно!");
            }
            else
            {
                Console.WriteLine("Вже підключено до бази даних");
            }
        }

        public void ExecuteQuery(string query)
        {
            if (_isConnected)
            {
                Console.WriteLine("Виконую запит: " + query);
            }
            else
            {
                Console.WriteLine("Помилка: Спочатку потрібно підключитися до бази даних");
            }
        }

        public void Disconnect()
        {
            if (_isConnected)
            {
                Console.WriteLine("Відключення від бази даних");
                _isConnected = false;
            }
        }
    }
}
