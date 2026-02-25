using System;
using System.Collections.Generic;

/// <summary>
/// Клас UniversityCourse представляє стандартний університетський курс
/// </summary>
public class UniversityCourse : Course
{
    private const int STANDARD_MAX_STUDENTS = 30;

    /// <summary>
    /// Конструктор для ініціалізації університетського курсу
    /// </summary>
    public UniversityCourse(int id, string name, string description, int credits)
        : base(id, name, description, credits)
    {
    }

    /// <summary>
    /// Отримати максимальну кількість студентів у курсі
    /// </summary>
    public override int GetMaxStudents()
    {
        return STANDARD_MAX_STUDENTS;
    }

    /// <summary>
    /// Реалізація методу для отримання інформації про курс
    /// </summary>
    public override string GetInfo()
    {
        return $"[UniversityCourse] ID: {Id}, Назва: {Name}, " +
               $"Кредити: {Credits}, Макс студентів: {GetMaxStudents()}";
    }
}

/// <summary>
/// Клас LabCourse представляє лабораторний курс з меньшим максимумом студентів
/// </summary>
public class LabCourse : Course
{
    private const int LAB_MAX_STUDENTS = 15;
    private string _labTechnology;

    public string LabTechnology
    {
        get { return _labTechnology; }
        set { _labTechnology = value ?? throw new ArgumentNullException(nameof(LabTechnology)); }
    }

    /// <summary>
    /// Конструктор для ініціалізації лабораторного курсу
    /// </summary>
    public LabCourse(int id, string name, string description, int credits, string labTechnology)
        : base(id, name, description, credits)
    {
        LabTechnology = labTechnology;
    }

    /// <summary>
    /// Отримати максимальну кількість студентів у курсі
    /// </summary>
    public override int GetMaxStudents()
    {
        return LAB_MAX_STUDENTS;
    }

    /// <summary>
    /// Реалізація методу для отримання інформації про курс
    /// </summary>
    public override string GetInfo()
    {
        return $"[LabCourse] ID: {Id}, Назва: {Name}, " +
               $"Кредити: {Credits}, Технологія: {LabTechnology}, Макс студентів: {GetMaxStudents()}";
    }
}
