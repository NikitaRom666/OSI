// Завдання для себе: написати простий клас BankAccount
// та тести до нього. Це типовий приклад що розглядається на лекціях

using System;

namespace BankingApp
{
    /// <summary>
    /// Клас, що представляє банківський рахунок.
    /// Допускає поповнення, зняття грошей та перевірку балансу.
    /// </summary>
    public class BankAccount
    {
        private decimal _balance;
        private string _accountHolder;

        /// <summary>
        /// Власник рахунку
        /// </summary>
        public string AccountHolder
        {
            get { return _accountHolder; }
        }

        /// <summary>
        /// Поточний баланс
        /// </summary>
        public decimal Balance
        {
            get { return _balance; }
        }

        /// <summary>
        /// Номер рахунку (для тестування)
        /// </summary>
        public string AccountNumber { get; private set; }

        /// <summary>
        /// Конструктор приймає ім'я власника та початковий баланс
        /// </summary>
        public BankAccount(string accountHolder, decimal initialBalance = 0)
        {
            if (string.IsNullOrWhiteSpace(accountHolder))
            {
                throw new ArgumentException("Ім'я власника не може бути порожним");
            }

            if (initialBalance < 0)
            {
                throw new ArgumentException("Баланс не може бути від'ємним");
            }

            _accountHolder = accountHolder;
            _balance = initialBalance;
            AccountNumber = GenerateAccountNumber();
        }

        /// <summary>
        /// Поповнити рахунок на певну суму
        /// </summary>
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Сума поповнення має бути більше нуля");
            }

            _balance += amount;
        }

        /// <summary>
        /// Зняти гроші з рахунку
        /// </summary>
        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Сума зняття має бути більше нуля");
            }

            if (amount > _balance)
            {
                throw new InvalidOperationException("Недостатньо коштів на рахунку");
            }

            _balance -= amount;
        }

        /// <summary>
        /// Перевести гроші на інший рахунок (трохи складніша операція)
        /// </summary>
        public bool TransferTo(BankAccount recipient, decimal amount)
        {
            if (recipient == null)
            {
                throw new ArgumentNullException(nameof(recipient));
            }

            if (amount <= 0)
            {
                return false; // Невалідна сума
            }

            if (amount > _balance)
            {
                return false; // Недостатньо коштів
            }

            this.Withdraw(amount);
            recipient.Deposit(amount);
            return true;
        }

        private string GenerateAccountNumber()
        {
            // Просте генерування номеру рахунку
            return "ACC" + DateTime.Now.Ticks.ToString().Substring(0, 8);
        }

        public override string ToString()
        {
            return $"Рахунок: {AccountNumber}, Власник: {_accountHolder}, Баланс: {_balance:C}";
        }
    }
}
