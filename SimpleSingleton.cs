namespace SingletonPractice
{
    public class SimpleSingleton
    {
        private static SimpleSingleton _instance;

        private SimpleSingleton()
        {
            Console.WriteLine("SimpleSingleton: Створено екземпляр");
        }

        public static SimpleSingleton GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SimpleSingleton();
            }
            return _instance;
        }

        public void DoSomething()
        {
            Console.WriteLine("SimpleSingleton: Виконую дію");
        }
    }
}
