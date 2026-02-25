# Про Mock-об'єкти (які я називаю "підробними")

## Що це таке?

Mock-об'єкт — це фальшивий об'єкт, який ми створюємо в тестах замість справжнього. Де-то я читав, що це "тест-двійник" — мені нравиться це описання.

Уявіть: у вас є система оплати, яка звертається до банку (справжнього банку через інтернет). Ви не хочете в тесті обращаться до справжнього банку, тому що:
1. Це повільно
2. Можливо недоступно
3. Можна ненавмисне перевести реальні гроші! 😱

Тому ви робите "підробного" банку для тестів.

## Приклад без Mock-об'єкта (погано)

```csharp
// Клас, що залежить від справжнього сервісу
public class PaymentProcessor
{
    private BankService _bankService;

    public PaymentProcessor()
    {
        _bankService = new BankService(); // справжний сервіс!
    }

    public bool ProcessPayment(decimal amount)
    {
        return _bankService.ChargeCard(amount); // справжній запит до банку
    }
}

// Тест (погано)
[Fact]
public void ProcessPayment_ShouldCharge()
{
    var processor = new PaymentProcessor();
    
    // На цьому моменту можливо зробиться справжній платіж!!!
    bool result = processor.ProcessPayment(100);
    
    Assert.True(result);
}
```

Це BAD, бо:
- Тест залежить від зовнішнього сервісу
- Може не пройти, якщо немає інтернету
- Повільний
- Ненадійний

## Приклад з Mock-об'єктом (добре)

```csharp
// Спочатку робимо інтерфейс
public interface IBankService
{
    bool ChargeCard(decimal amount);
}

// Справжня реалізація
public class RealBankService : IBankService
{
    public bool ChargeCard(decimal amount)
    {
        // справжній http запит до банку
    }
}

// Клас, що приймає сервіс як параметр (коротко називається "dependency injection" або DI)
public class PaymentProcessor
{
    private IBankService _bankService;

    public PaymentProcessor(IBankService bankService)
    {
        _bankService = bankService;
    }

    public bool ProcessPayment(decimal amount)
    {
        return _bankService.ChargeCard(amount);
    }
}

// Тест з Mock-об'єктом (добре!)
[Fact]
public void ProcessPayment_WithValidAmount_ReturnsTrue()
{
    // Arrange - створюємо підробний банк
    var mockBank = new Mock<IBankService>();
    mockBank.Setup(b => b.ChargeCard(It.IsAny<decimal>()))
            .Returns(true); // завжди буде повертати true

    var processor = new PaymentProcessor(mockBank.Object);

    // Act
    bool result = processor.ProcessPayment(100);

    // Assert
    Assert.True(result);
    // Крім того, можна перевірити, що метод був викликаний
    mockBank.Verify(b => b.ChargeCard(100), Times.Once());
}

[Fact]
public void ProcessPayment_WhenBankFails_ReturnsFalse()
{
    // Arrange - цей раз банк повертає false
    var mockBank = new Mock<IBankService>();
    mockBank.Setup(b => b.ChargeCard(It.IsAny<decimal>()))
            .Returns(false);

    var processor = new PaymentProcessor(mockBank.Object);

    // Act
    bool result = processor.ProcessPayment(100);

    // Assert
    Assert.False(result);
}
```

Це GOOD, тому що:
- Тест не залежить від справжнього банку
- Швидкий
- Ви можете симулювати різні сценарії (успіх, помилка, таймаут...)
- Надійний

## Коли використовувати Mock-об'єкти

### ПОТРІБНО використовувати:

**1. База даних**
```csharp
// Не хочемо звертатися до справжної БД в тестах
var mockDatabase = new Mock<IUserRepository>();
mockDatabase.Setup(db => db.GetUserById(1))
            .Returns(new User { Id = 1, Name = "John" });

var userService = new UserService(mockDatabase.Object);
var user = userService.GetUserDetails(1);

Assert.Equal("John", user.Name);
```

**2. Зовнішні API**
```csharp
// Не треба звертатися до справжнього API Google Maps
var mockGoogleMaps = new Mock<IGoogleMapsAPI>();
mockGoogleMaps.Setup(g => g.GetDistance("Київ", "Львів"))
              .Returns(540);

var routeFinder = new RouteFinder(mockGoogleMaps.Object);
var distance = routeFinder.GetDistance("Київ", "Львів");

Assert.Equal(540, distance);
```

**3. Файлова система**
```csharp
// Не хочемо створювати справжні файли під час тестування
var mockFileSystem = new Mock<IFileSystem>();
mockFileSystem.Setup(fs => fs.ReadFile("config.json"))
              .Returns("{\"key\": \"value\"}");

var configLoader = new ConfigLoader(mockFileSystem.Object);
var config = configLoader.LoadConfig("config.json");

Assert.NotNull(config);
```

**4. Дорогі операції**
```csharp
// Рахування складних математичних формул - це повільно
var mockCalculator = new Mock<IComplexCalculator>();
mockCalculator.Setup(c => c.CalculatePi(1000000))
              .Returns(3.14159265359);
```

**5. Сторонні сервіси**
```csharp
// Email, SMS, відео конвертація тощо
var mockEmailService = new Mock<IEmailService>();
mockEmailService.Setup(e => e.SendEmail(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);
```

### Можна БЕЗ Mock-об'єктів:

**1. Проста логіка без залежностей**
```csharp
// Не потрібен mock
[Fact]
public void CalculateSum_ReturnCorrectResult()
{
    var calculator = new Calculator();
    int result = calculator.Add(5, 3);
    Assert.Equal(8, result);
}
```

**2. Об'єкти значень (Value Objects)**
```csharp
// Не потрібен mock
var person = new Person { Name = "John", Age = 30 };
Assert.Equal("John", person.Name);
```

**3. Інтеграційні тесты**
```csharp
// В інтеграційних тестах тестуємо справжню взаємодію
[Fact]
public void SaveAndRetrieveUser_Works()
{
    // Використовуємо справжню БД (або тестову копію)
    var database = new TestDatabase();
    var userRepository = new UserRepository(database);
    
    userRepository.Save(new User { Id = 1, Name = "John" });
    var user = userRepository.GetById(1);
    
    Assert.Equal("John", user.Name);
}
```

**4. Простої об'єкти для фіксчур (test data)**
```csharp
// Не потрібен mock
var testUser = new User { Id = 1, Name = "TestUser" };
var userService = new UserService();
bool isValid = userService.IsValidUser(testUser);
Assert.True(isValid);
```

## Що має мати хороший Mock?

1. **Setup** - налаштування, що робити
2. **Verify** - перевірка, що був викликаний
3. **Возвращення значень** - відповіді на запити

```csharp
var mockPayment = new Mock<IPaymentGateway>();

// Setup - налаштування
mockPayment.Setup(p => p.ProcessPayment(100))
           .Returns(true);

var processor = new PaymentProcessor(mockPayment.Object);
processor.ProcessPayment(100);

// Verify - перевірка, що метод був викликаний саме один раз
mockPayment.Verify(p => p.ProcessPayment(100), Times.Once());
```

## Мої тести у BankAccountTests.cs

Я не використовував Mock-об'єкти, тому що клас `BankAccount` не має зовнішніх залежностей. Він просто оперує числами. Якби я додав операцію "переведення на іншу карту" чи "снаття через ATM", тоді б мені знадобилися Mock-об'єкти для:
- Зовнішньої АТМ системи
- Інших банків
- Логування операцій

Але для простого рахунку це не потрібно.

## Висновок

Mock-об'єкти — це не якаяс геніальна штука, що складна для розуміння. Це просто способіб сказати: "У цьому тесті я хочу симулювати поведінку цього об'єкта без справжньої його роботи".

Використовуйте їх розумно — не потрібно мокувати все поспіль, але й не забувайте про них коли це необхідно.
