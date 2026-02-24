from abc import ABC, abstractmethod
import datetime

# ==========================================
# ЗАВДАННЯ 1, 3, 4: АБСТРАКЦІЯ ТА НАСЛІДУВАННЯ
# ==========================================

class FinancialAsset(ABC):
    """
    Абстрактний базовий клас, що описує будь-який фінансовий актив.
    Визначає інтерфейс, який повинні реалізувати всі спадкоємці.
    """
    def __init__(self, symbol: str, price: float):
        self.symbol = symbol.upper()
        self._price = price  # Protected attribute (інкапсуляція)

    @abstractmethod
    def get_description(self):
        """Абстрактний метод: кожен актив повинен мати свій опис."""
        pass

    @property
    def price(self):
        """Геттер для отримання ціни (захищений доступ)."""
        return self._price

    @price.setter
    def price(self, new_price):
        """Сеттер для зміни ціни з валідацією."""
        if new_price < 0:
            print(f" [Помилка] Ціна не може бути від'ємною!")
        else:
            self._price = new_price

class CryptoCoin(FinancialAsset):
    """
    Клас для криптовалют. Успадковує FinancialAsset.
    """
    def __init__(self, symbol, price, algorithm):
        super().__init__(symbol, price)
        self.algorithm = algorithm

    def get_description(self):
        return f"Криптовалюта {self.symbol} (Алгоритм: {self.algorithm})"

class StockShare(FinancialAsset):
    """
    Клас для акцій компаній. Успадковує FinancialAsset.
    """
    def __init__(self, symbol, price, company_name):
        super().__init__(symbol, price)
        self.company_name = company_name

    def get_description(self):
        return f"Акція компанії {self.company_name} (Тікер: {self.symbol})"

# ==========================================
# ЗАВДАННЯ 2, 4: ІНКАПСУЛЯЦІЯ ТА УПРАВЛІННЯ
# ==========================================

class SecureWallet:
    """
    Клас гаманця, що керує активами.
    Демонструє інкапсуляцію (приватний баланс).
    """
    def __init__(self, owner_name, start_balance):
        self.owner = owner_name
        self.__balance = start_balance  # Private attribute (недоступний ззовні)
        self.portfolio = [] # Список активів

    def show_balance(self):
        """Безпечний метод для перегляду балансу."""
        print(f" >> Баланс гаманця {self.owner}: ${self.__balance:.2f}")

    def buy_asset(self, asset: FinancialAsset, quantity: float):
        """Метод купівлі активу."""
        cost = asset.price * quantity
        
        if cost > self.__balance:
            print(f" [X] Недостатньо коштів для купівлі {asset.symbol}. Потрібно: ${cost:.2f}")
            return

        self.__balance -= cost
        self.portfolio.append({"asset": asset, "qty": quantity})
        print(f" [+] Успішно куплено {quantity} шт. {asset.symbol} на суму ${cost:.2f}")

    def show_portfolio_report(self):
        """Генерує звіт по портфелю (Поліморфізм у дії)."""
        print(f"\n --- ЗВІТ ДЛЯ {self.owner.upper()} ({datetime.date.today()}) ---")
        total_value = self.__balance
        
        for item in self.portfolio:
            asset = item['asset']
            qty = item['qty']
            current_val = asset.price * qty
            total_value += current_val
            # Викликаємо get_description(), який різний для Крипти та Акцій
            print(f" > {asset.get_description()} | Кількість: {qty} | Вартість: ${current_val:.2f}")
        
        print(f" ------------------------------------------------")
        print(f" Всього активів + Готівка: ${total_value:.2f}\n")

# ==========================================
# ЗАВДАННЯ 5, 6: ТЕСТУВАННЯ ТА ДОКУМЕНТАЦІЯ
# ==========================================

if __name__ == "__main__":
    print(">>> ЗАПУСК СИСТЕМИ (OOP DEMO) <<<\n")

    # 1. Створення активів
    btc = CryptoCoin("BTC", 45000.00, "SHA-256")
    apple = StockShare("AAPL", 185.50, "Apple Inc.")
    
    # 2. Створення гаманця
    my_wallet = SecureWallet("Nikita", 10000.00)
    my_wallet.show_balance()

    # 3. Тест інкапсуляції (спроба зламати баланс)
    # my_wallet.__balance = 1000000 # Це викличе помилку або не спрацює
    
    # 4. Операції
    my_wallet.buy_asset(btc, 0.1)     # Купуємо 0.1 Біткоїна ($4500)
    my_wallet.buy_asset(apple, 10)    # Купуємо 10 акцій Apple ($1855)
    
    # 5. Спроба купити забагато (Тест логіки)
    my_wallet.buy_asset(btc, 2.0)     # Коштує $90,000 -> Має бути відмова

    # 6. Зміна ринку (використання сетера)
    print("\n ... Новини ринку: Біткоїн виріс! ...")
    btc.price = 48000.00 # Працює через @property

    # 7. Фінальний звіт
    my_wallet.show_portfolio_report()