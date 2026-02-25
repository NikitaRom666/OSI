// Тести на клас BankAccount
// Написав їх сам, щоб розуміти, як це все працює

using Xunit;
using BankingApp;

namespace BankingApp.Tests
{
    public class BankAccountTests
    {
        // Я тестую Deposit() - поповнення рахунку

        [Fact]
        public void Deposit_WithPositiveAmount_IncreasesBalance()
        {
            // Arrange - підготовляємо дані
            var account = new BankAccount("Іван Петренко", 100);

            // Act - виконуємо дію
            account.Deposit(50);

            // Assert - перевіряємо результат
            Assert.Equal(150, account.Balance);
        }

        [Fact]
        public void Deposit_WithSmallAmount_Works()
        {
            // Це граничний випадок - коли сума дуже мала
            // Тест перевіряє, що навіть 0.01 можна додати
            var account = new BankAccount("Марія", 100);
            account.Deposit(0.01m);

            Assert.Equal(100.01m, account.Balance);
        }

        [Fact]
        public void Deposit_WithZeroAmount_ThrowsException()
        {
            // Граничний випадок - сума = 0
            // Це помилка, тому має бути виняток
            var account = new BankAccount("Петро", 100);

            Assert.Throws<ArgumentException>(() => account.Deposit(0));
        }

        [Fact]
        public void Deposit_WithNegativeAmount_ThrowsException()
        {
            // Граничний випадок - від'ємна сума
            // Це точно помилка
            var account = new BankAccount("Ольга", 100);

            Assert.Throws<ArgumentException>(() => account.Deposit(-50));
        }

        // Тепер тестую Withdraw() - зняття грошей

        [Fact]
        public void Withdraw_WithValidAmount_DecreasesBalance()
        {
            // Нормальний сценарій
            var account = new BankAccount("Володимир", 200);
            account.Withdraw(75);

            Assert.Equal(125, account.Balance);
        }

        [Fact]
        public void Withdraw_WithAmountEqualToBalance_BalanceBecomesZero()
        {
            // Граничний випадок - знімаємо все до остатку
            var account = new BankAccount("Світлана", 100);
            account.Withdraw(100);

            Assert.Equal(0, account.Balance);
        }

        [Fact]
        public void Withdraw_WithAmountMoreThanBalance_ThrowsException()
        {
            // Граничний випадок - немаємо достатньо грошей
            var account = new BankAccount("Андрій", 100);

            Assert.Throws<InvalidOperationException>(() => account.Withdraw(150));
        }

        [Fact]
        public void Withdraw_WithZeroAmount_ThrowsException()
        {
            // Граничний випадок
            var account = new BankAccount("Катерина", 100);

            Assert.Throws<ArgumentException>(() => account.Withdraw(0));
        }

        [Fact]
        public void Withdraw_WithNegativeAmount_ThrowsException()
        {
            // Граничний випадок - невалідна сума
            var account = new BankAccount("Василь", 100);

            Assert.Throws<ArgumentException>(() => account.Withdraw(-25));
        }

        // Тестую TransferTo() - переведення на інший рахунок

        [Fact]
        public void TransferTo_WithValidAmount_TransfersSuccessfully()
        {
            // Нормальна операція переведення
            var account1 = new BankAccount("Юрій", 500);
            var account2 = new BankAccount("Наталія", 100);

            bool result = account1.TransferTo(account2, 200);

            Assert.True(result);
            Assert.Equal(300, account1.Balance);
            Assert.Equal(300, account2.Balance);
        }

        [Fact]
        public void TransferTo_WithZeroAmount_ReturnsFalse()
        {
            // Граничний випадок - нульова сума
            var account1 = new BankAccount("Тарас", 500);
            var account2 = new BankAccount("Марина", 100);

            bool result = account1.TransferTo(account2, 0);

            Assert.False(result);
            Assert.Equal(500, account1.Balance);  // Нічого не змінилось
            Assert.Equal(100, account2.Balance);
        }

        [Fact]
        public void TransferTo_WithInsufficientFunds_ReturnsFalse()
        {
            // Граничний випадок - не вистачає грошей
            var account1 = new BankAccount("Олег", 100);
            var account2 = new BankAccount("Оксана", 50);

            bool result = account1.TransferTo(account2, 200);

            Assert.False(result);
            Assert.Equal(100, account1.Balance);  // Не змінилось
            Assert.Equal(50, account2.Balance);   // Не змінилось
        }

        [Fact]
        public void TransferTo_WithAmountEqualsBalance_TransfersEverything()
        {
            // Граничний випадок - переводимо всі гроші
            var account1 = new BankAccount("Валентин", 250);
            var account2 = new BankAccount("Вероніка", 50);

            bool result = account1.TransferTo(account2, 250);

            Assert.True(result);
            Assert.Equal(0, account1.Balance);
            Assert.Equal(300, account2.Balance);
        }

        [Fact]
        public void TransferTo_WithNullRecipient_ThrowsException()
        {
            // Граничний випадок - відправник = null
            var account = new BankAccount("Станіслав", 500);

            Assert.Throws<ArgumentNullException>(() => account.TransferTo(null, 100));
        }

        // Тестую конструктор

        [Fact]
        public void Constructor_WithValidData_CreatesAccount()
        {
            // Нормальна ініціалізація
            var account = new BankAccount("Павло", 1000);

            Assert.Equal("Павло", account.AccountHolder);
            Assert.Equal(1000, account.Balance);
            Assert.NotNull(account.AccountNumber);
        }

        [Fact]
        public void Constructor_WithEmptyName_ThrowsException()
        {
            // Граничний випадок - пусте ім'я
            Assert.Throws<ArgumentException>(() => new BankAccount("", 100));
        }

        [Fact]
        public void Constructor_WithNegativeBalance_ThrowsException()
        {
            // Граничний випадок - від'ємний баланс при створенні
            Assert.Throws<ArgumentException>(() => new BankAccount("Ірина", -100));
        }

        [Fact]
        public void Constructor_WithZeroBalance_Works()
        {
            // Граничний випадок - нульовий баланс в порядку
            var account = new BankAccount("Броніслав", 0);
            Assert.Equal(0, account.Balance);
        }
    }
}
