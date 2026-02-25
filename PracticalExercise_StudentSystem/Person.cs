using System;
using System.Collections.Generic;

/// <summary>
/// Абстрактний базовий клас для представлення людини (Абстракція)
/// </summary>
public abstract class Person : IEntity, IManageable
{
    // Приватні поля для інкапсуляції
    private int _id;
    private string _firstName;
    private string _lastName;
    private string _email;

    // Властивості з контролем доступу
    public int Id
    {
        get { return _id; }
        protected set { _id = value; }
    }

    public string FirstName
    {
        get { return _firstName; }
        set { _firstName = value ?? throw new ArgumentNullException(nameof(FirstName)); }
    }

    public string LastName
    {
        get { return _lastName; }
        set { _lastName = value ?? throw new ArgumentNullException(nameof(LastName)); }
    }

    public string Email
    {
        get { return _email; }
        set
        {
            if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
            {
                throw new ArgumentException("Невалідна електронна адреса.");
            }
            _email = value;
        }
    }

    /// <summary>
    /// Конструктор для ініціалізації базової інформації про людину
    /// </summary>
    /// <param name="id">Унікальний ідентифікатор</param>
    /// <param name="firstName">Ім'я</param>
    /// <param name="lastName">Прізвище</param>
    /// <param name="email">Електронна адреса</param>
    protected Person(int id, string firstName, string lastName, string email)
    {
        _id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    /// <summary>
    /// Абстрактний метод для отримання інформації (поліморфізм)
    /// </summary>
    public abstract string GetInfo();

    /// <summary>
    /// Здобути повне ім'я особи
    /// </summary>
    public string GetFullName()
    {
        return $"{FirstName} {LastName}";
    }
}

/// <summary>
/// Абстрактний клас для курсу
/// </summary>
public abstract class Course : IEntity, IManageable
{
    private int _id;
    private string _name;
    private string _description;
    private int _credits;

    public int Id
    {
        get { return _id; }
        protected set { _id = value; }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value ?? throw new ArgumentNullException(nameof(Name)); }
    }

    public string Description
    {
        get { return _description; }
        set { _description = value ?? string.Empty; }
    }

    public int Credits
    {
        get { return _credits; }
        set
        {
            if (value < 1 || value > 6)
            {
                throw new ArgumentException("Кредити повинні бути від 1 до 6.");
            }
            _credits = value;
        }
    }

    /// <summary>
    /// Конструктор для ініціалізації курсу
    /// </summary>
    protected Course(int id, string name, string description, int credits)
    {
        _id = id;
        Name = name;
        Description = description;
        Credits = credits;
    }

    public abstract string GetInfo();

    /// <summary>
    /// Отримати максимальну кількість студентів у курсі
    /// </summary>
    public abstract int GetMaxStudents();
}
