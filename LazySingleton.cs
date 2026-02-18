namespace SingletonPractice
{
    public class LazySingleton
    {
        private static LazySingleton _instance;
        private static bool _isInitialized = false;

        private LazySingleton()
        {
            Console.WriteLine("LazySingleton: Створено екземпляр (лінива ініціалізація)");
        }

        public static LazySingleton GetInstance()
        {
            if (!_isInitialized)
            {
                _instance = new LazySingleton();
                _isInitialized = true;
            }
            return _instance;
        }

        public void DoSomething()
        {
            Console.WriteLine("LazySingleton: Виконую дію");
        }
    }
}
