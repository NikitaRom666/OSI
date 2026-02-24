from abc import ABC, abstractmethod

# =========================================================
# ТЕМА: Створення основних класів (OOP)
# =========================================================

class Asset(ABC):
    """
    Абстрактний клас (Abstract Base Class).
    Визначає шаблон для будь-якого активу.
    """
    def __init__(self, name, ticker, price):
        self.name = name
        self.ticker = ticker
        self._price = price  # Protected attribute (інкапсуляція)

    @abstractmethod
    def get_info(self):
        """Цей метод обов'язково мають реалізувати діти класу."""
        pass

    @property
    def price(self):
        """Геттер для отримання ціни."""
        return self._price

    @price.setter
    def price(self, new_price):
        """Сеттер для безпечної зміни ціни."""
        if new_price >= 0:
            self._price = new_price
        else:
            print(" [Помилка] Ціна не може бути від'ємною!")

# --- Наслідування (Inheritance) ---

class Crypto(Asset):
    def get_info(self):
        return f"[Crypto] {self.name} ({self.ticker}): ${self._price}"

class Stock(Asset):
    def __init__(self, name, ticker, price, sector):
        super().__init__(name, ticker, price)
        self.sector = sector  # Унікальне поле для акцій

    def get_info(self):
        return f"[Stock]  {self.name} ({self.ticker}) | Сектор: {self.sector} | Ціна: ${self._price}"

# --- Клас-Менеджер (Composition) ---

class PortfolioManager:
    """
    Клас для управління списком активів.
    """
    def __init__(self, owner):
        self.owner = owner
        self.__assets = []  # Private list (повна інкапсуляція)

    def add_asset(self, asset: Asset):
        self.__assets.append(asset)
        print(f" >> Додано актив: {asset.ticker}")

    def show_report(self):
        print(f"\n--- Звіт портфеля користувача {self.owner} ---")
        total_value = 0
        for asset in self.__assets:
            print(asset.get_info())
            total_value += asset.price
        print(f"-------------------------------------------")
        print(f"Загальна вартість: ${total_value:.2f}\n")

# --- Демонстрація (Testing) ---
if __name__ == "__main__":
    # 1. Створюємо активи
    btc = Crypto("Bitcoin", "BTC", 42000)
    eth = Crypto("Ethereum", "ETH", 2900)
    tesla = Stock("Tesla Inc.", "TSLA", 180, "Auto")

    # 2. Створюємо менеджер
    my_portfolio = PortfolioManager("Nikita")

    # 3. Додаємо активи
    my_portfolio.add_asset(btc)
    my_portfolio.add_asset(eth)
    my_portfolio.add_asset(tesla)

    # 4. Тест зміни ціни через сетер
    print("\n[Update] Ринок змінився...")
    btc.price = 45000  # Ок
    tesla.price = -100 # Помилка (валідація працює)

    # 5. Вивід звіту
    my_portfolio.show_report()