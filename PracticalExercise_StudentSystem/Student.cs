using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Клас Student представляє студента у системі (Спеціалізована сутність через наслідування)
/// </summary>
public class Student : Person
{
    private string _studentNumber;
    private double _gpa;
    private readonly List<Course> _enrolledCourses;

    public string StudentNumber
    {
        get { return _studentNumber; }
        set { _studentNumber = value ?? throw new ArgumentNullException(nameof(StudentNumber)); }
    }

    public double GPA
    {
        get { return _gpa; }
        set
        {
            if (value < 0.0 || value > 4.0)
            {
                throw new ArgumentException("GPA повинен бути від 0.0 до 4.0.");
            }
            _gpa = value;
        }
    }

    public IReadOnlyList<Course> EnrolledCourses => _enrolledCourses.AsReadOnly();

    /// <summary>
    /// Конструктор для ініціалізації студента
    /// </summary>
    public Student(int id, string firstName, string lastName, string email, string studentNumber, double gpa = 0.0)
        : base(id, firstName, lastName, email)
    {
        StudentNumber = studentNumber;
        GPA = gpa;
        _enrolledCourses = new List<Course>();
    }

    /// <summary>
    /// Записати студента на курс
    /// </summary>
    /// <param name="course">Курс для запису</param>
    /// <returns>true якщо успішно записано</returns>
    public bool EnrollInCourse(Course course)
    {
        if (course == null)
        {
            throw new ArgumentNullException(nameof(course));
        }

        if (_enrolledCourses.Contains(course))
        {
            return false; // Вже записаний
        }

        _enrolledCourses.Add(course);
        return true;
    }

    /// <summary>
    /// Видалити студента з курсу
    /// </summary>
    public bool UnenrollFromCourse(Course course)
    {
        if (course == null)
        {
            throw new ArgumentNullException(nameof(course));
        }

        return _enrolledCourses.Remove(course);
    }

    /// <summary>
    /// Отримати кількість записаних курсів
    /// </summary>
    public int GetEnrolledCoursesCount()
    {
        return _enrolledCourses.Count;
    }

    /// <summary>
    /// Отримати загальну кількість кредитів
    /// </summary>
    public int GetTotalCredits()
    {
        return _enrolledCourses.Sum(c => c.Credits);
    }

    /// <summary>
    /// Реалізація абстрактного методу для отримання інформації про студента
    /// </summary>
    public override string GetInfo()
    {
        return $"[Student] ID: {Id}, Ім'я: {GetFullName()}, " +
               $"Номер студента: {StudentNumber}, GPA: {GPA:F2}, " +
               $"Записаних курсів: {_enrolledCourses.Count}, " +
               $"Email: {Email}";
    }

    /// <summary>
    /// Отримати детальну інформацію про студента та його курси
    /// </summary>
    public void DisplayDetailedInfo()
    {
        Console.WriteLine(GetInfo());
        if (_enrolledCourses.Count > 0)
        {
            Console.WriteLine("  Записані курси:");
            foreach (var course in _enrolledCourses)
            {
                Console.WriteLine($"    - {course.Name} ({course.Credits} кредитів)");
            }
        }
        else
        {
            Console.WriteLine("  Немає записаних курсів.");
        }
    }
}
