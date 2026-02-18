namespace SingletonPractice
{
    public class ThreadSafeSingleton
    {
        private static ThreadSafeSingleton _instance;
        private static readonly object _lock = new object();

        private ThreadSafeSingleton()
        {
            Console.WriteLine("ThreadSafeSingleton: Створено екземпляр (потокобезпечний)");
        }

        public static ThreadSafeSingleton GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ThreadSafeSingleton();
                    }
                }
            }
            return _instance;
        }

        public void DoSomething()
        {
            Console.WriteLine("ThreadSafeSingleton: Виконую дію");
        }
    }
}
