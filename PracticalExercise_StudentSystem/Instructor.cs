using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Клас Instructor представляє викладача у системі
/// </summary>
public class Instructor : Person
{
    private string _department;
    private string _specialization;
    private readonly List<Course> _taughtCourses;

    public string Department
    {
        get { return _department; }
        set { _department = value ?? throw new ArgumentNullException(nameof(Department)); }
    }

    public string Specialization
    {
        get { return _specialization; }
        set { _specialization = value ?? throw new ArgumentNullException(nameof(Specialization)); }
    }

    public IReadOnlyList<Course> TaughtCourses => _taughtCourses.AsReadOnly();

    /// <summary>
    /// Конструктор для ініціалізації викладача
    /// </summary>
    public Instructor(int id, string firstName, string lastName, string email, 
                      string department, string specialization)
        : base(id, firstName, lastName, email)
    {
        Department = department;
        Specialization = specialization;
        _taughtCourses = new List<Course>();
    }

    /// <summary>
    /// Призначити викладача до курсу
    /// </summary>
    public bool AssignCourse(Course course)
    {
        if (course == null)
        {
            throw new ArgumentNullException(nameof(course));
        }

        if (_taughtCourses.Contains(course))
        {
            return false; // Вже призначений
        }

        _taughtCourses.Add(course);
        return true;
    }

    /// <summary>
    /// Видалити курс з списку викладачевих курсів
    /// </summary>
    public bool RemoveCourse(Course course)
    {
        if (course == null)
        {
            throw new ArgumentNullException(nameof(course));
        }

        return _taughtCourses.Remove(course);
    }

    /// <summary>
    /// Отримати кількість курсів, що викладаються
    /// </summary>
    public int GetTaughtCoursesCount()
    {
        return _taughtCourses.Count;
    }

    /// <summary>
    /// Реалізація абстрактного методу для отримання інформації про викладача
    /// </summary>
    public override string GetInfo()
    {
        return $"[Instructor] ID: {Id}, Ім'я: {GetFullName()}, " +
               $"Кафедра: {Department}, Спеціалізація: {Specialization}, " +
               $"Кількість курсів: {_taughtCourses.Count}, Email: {Email}";
    }

    /// <summary>
    /// Отримати детальну інформацію про викладача та його курси
    /// </summary>
    public void DisplayDetailedInfo()
    {
        Console.WriteLine(GetInfo());
        if (_taughtCourses.Count > 0)
        {
            Console.WriteLine("  Викладацькі курси:");
            foreach (var course in _taughtCourses)
            {
                Console.WriteLine($"    - {course.Name}");
            }
        }
        else
        {
            Console.WriteLine("  Немає призначених курсів.");
        }
    }
}
