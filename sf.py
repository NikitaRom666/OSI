import time
import random

# ==========================================
# ЕТАП 1: БАЗОВА АРХІТЕКТУРА (КЛАСИ)
# ==========================================

class Asset:
    """Модель активу (криптовалюти)"""
    def __init__(self, symbol, price):
        self.symbol = symbol
        self.price = price

    def update_price(self):
        """Імітація зміни ціни на ринку (+/- 5%)"""
        change_percent = random.uniform(-0.05, 0.05)
        self.price *= (1 + change_percent)
        return change_percent

class Portfolio:
    """Модуль управління активами користувача"""
    def __init__(self, start_balance_usd):
        self.balance_usd = start_balance_usd
        self.holdings = {}  # Словник {Symbol: Quantity}

    def buy(self, asset, amount_usd):
        """Логіка купівлі"""
        if amount_usd > self.balance_usd:
            print(f" [!] Помилка: Недостатньо коштів для купівлі {asset.symbol}")
            return False
        
        quantity = amount_usd / asset.price
        self.balance_usd -= amount_usd
        
        if asset.symbol in self.holdings:
            self.holdings[asset.symbol] += quantity
        else:
            self.holdings[asset.symbol] = quantity
            
        print(f" [+] КУПЛЕНО: {quantity:.4f} {asset.symbol} за ${amount_usd:.2f} (Курс: ${asset.price:.2f})")
        return True

    def sell(self, asset):
        """Логіка продажу всього активу"""
        if asset.symbol not in self.holdings:
            print(f" [!] Помилка: У вас немає {asset.symbol}")
            return False
        
        quantity = self.holdings[asset.symbol]
        revenue = quantity * asset.price
        self.balance_usd += revenue
        del self.holdings[asset.symbol]
        
        print(f" [-] ПРОДАНО: {quantity:.4f} {asset.symbol} за ${revenue:.2f} (Курс: ${asset.price:.2f})")
        return True

    def get_total_value(self, current_assets_map):
        """Розрахунок загальної вартості портфеля"""
        total = self.balance_usd
        for symbol, qty in self.holdings.items():
            if symbol in current_assets_map:
                total += qty * current_assets_map[symbol].price
        return total

# ==========================================
# ЕТАП 2: РЕАЛІЗАЦІЯ ІНТЕРФЕЙСУ ТА ЛОГІКИ
# ==========================================

class PrototypeApp:
    def __init__(self):
        # Ініціалізація ринку (доступні валюти)
        self.assets = {
            "BTC": Asset("BTC", 45000.00),
            "ETH": Asset("ETH", 3200.00),
            "SOL": Asset("SOL", 110.00)
        }
        # Ініціалізація користувача з $1000
        self.user = Portfolio(start_balance_usd=1000.00)

    def show_dashboard(self, day):
        print("\n" + "="*40)
        print(f" ДЕНЬ {day}: РИНОК ТА ПОРТФЕЛЬ")
        print("="*40)
        
        # Відображення ринку
        print(" [РИНОК]:")
        for symbol, asset in self.assets.items():
            print(f"  - {symbol}: ${asset.price:.2f}")

        # Відображення портфеля
        print(f"\n [ПОРТФЕЛЬ]:")
        print(f"  - USD Баланс: ${self.user.balance_usd:.2f}")
        for symbol, qty in self.user.holdings.items():
            current_val = qty * self.assets[symbol].price
            print(f"  - {symbol}: {qty:.4f} (Вартість: ${current_val:.2f})")
        
        total_val = self.user.get_total_value(self.assets)
        print(f"\n ЗАГАЛЬНА ВАРТІСТЬ АКАУНТУ: ${total_val:.2f}")
        print("-" * 40)

    # ==========================================
    # ЕТАП 3: ТЕСТУВАННЯ ТА ДЕМОНСТРАЦІЯ (СЦЕНАРІЙ)
    # ==========================================
    def run_demo_scenario(self):
        print(">>> ЗАПУСК ПРОТОТИПУ ПРОГРАМНОГО ПРОДУКТУ <<<\n")
        
        # ДЕНЬ 1: Початковий стан
        self.show_dashboard(1)
        
        # Тест функції купівлі
        print("\n>>> ОПЕРАЦІЯ: Інвестуємо $500 у Bitcoin...")
        self.user.buy(self.assets["BTC"], 500)
        
        print(">>> ОПЕРАЦІЯ: Інвестуємо $300 у Ethereum...")
        self.user.buy(self.assets["ETH"], 300)

        # ДЕНЬ 2: Зміна цін (Симуляція часу)
        print("\n... Проходить 24 години ...")
        for asset in self.assets.values():
            change = asset.update_price()
            # Для демо підкрутимо BTC вгору, щоб показати прибуток, якщо рандом пішов вниз
            if asset.symbol == "BTC" and change < 0:
                asset.price *= 1.10 # "Памп" ринку

        self.show_dashboard(2)

        # Тест функції продажу (Фіксація прибутку)
        print("\n>>> ОПЕРАЦІЯ: Продаємо весь Bitcoin...")
        self.user.sell(self.assets["BTC"])

        # Фінальний звіт
        print("\n" + "#"*40)
        total_final = self.user.get_total_value(self.assets)
        profit = total_final - 1000
        print(f" ФІНАЛЬНИЙ РЕЗУЛЬТАТ:")
        print(f" Початковий баланс: $1000.00")
        print(f" Поточний баланс:   ${total_final:.2f}")
        print(f" Чистий прибуток:   ${profit:.2f}")
        print("#"*40)

# Запуск програми
if __name__ == "__main__":
    app = PrototypeApp()
    app.run_demo_scenario()