using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Клас CourseManager керує записами студентів на курси (Реалізація IEnrollable)
/// </summary>
public class CourseManager : IEnrollable
{
    private readonly Dictionary<Course, List<Student>> _courseEnrollments;
    private readonly Dictionary<Course, Instructor> _courseInstructors;

    /// <summary>
    /// Конструктор для ініціалізації менеджера курсів
    /// </summary>
    public CourseManager()
    {
        _courseEnrollments = new Dictionary<Course, List<Student>>();
        _courseInstructors = new Dictionary<Course, Instructor>();
    }

    /// <summary>
    /// Реєстрація курсу у системі
    /// </summary>
    public void RegisterCourse(Course course)
    {
        if (course == null)
        {
            throw new ArgumentNullException(nameof(course));
        }

        if (!_courseEnrollments.ContainsKey(course))
        {
            _courseEnrollments[course] = new List<Student>();
        }
    }

    /// <summary>
    /// Призначення викладача до курсу
    /// </summary>
    public void AssignInstructor(Course course, Instructor instructor)
    {
        if (course == null || instructor == null)
        {
            throw new ArgumentNullException($"{nameof(course)}/{nameof(instructor)}");
        }

        if (!_courseEnrollments.ContainsKey(course))
        {
            throw new InvalidOperationException("Курс не зареєстрований.");
        }

        _courseInstructors[course] = instructor;
    }

    /// <summary>
    /// Записати студента на курс (Реалізація IEnrollable)
    /// </summary>
    public bool Enroll(Student student, Course course)
    {
        if (student == null || course == null)
        {
            throw new ArgumentNullException($"{nameof(student)}/{nameof(course)}");
        }

        if (!_courseEnrollments.ContainsKey(course))
        {
            throw new InvalidOperationException("Курс не зареєстрований.");
        }

        var enrolledStudents = _courseEnrollments[course];

        // Перевірити, чи студент вже записаний
        if (enrolledStudents.Any(s => s.Id == student.Id))
        {
            return false;
        }

        // Перевірити максимальну кількість студентів
        if (enrolledStudents.Count >= course.GetMaxStudents())
        {
            throw new InvalidOperationException($"Курс переповнений. Максимум: {course.GetMaxStudents()}");
        }

        enrolledStudents.Add(student);
        student.EnrollInCourse(course);
        return true;
    }

    /// <summary>
    /// Відписати студента від курсу (Реалізація IEnrollable)
    /// </summary>
    public bool Unenroll(Student student, Course course)
    {
        if (student == null || course == null)
        {
            throw new ArgumentNullException($"{nameof(student)}/{nameof(course)}");
        }

        if (!_courseEnrollments.ContainsKey(course))
        {
            return false;
        }

        var enrolledStudents = _courseEnrollments[course];
        var removed = enrolledStudents.RemoveAll(s => s.Id == student.Id) > 0;

        if (removed)
        {
            student.UnenrollFromCourse(course);
        }

        return removed;
    }

    /// <summary>
    /// Отримати студентів, записаних на курс
    /// </summary>
    public IReadOnlyList<Student> GetEnrolledStudents(Course course)
    {
        if (!_courseEnrollments.ContainsKey(course))
        {
            return new List<Student>().AsReadOnly();
        }

        return _courseEnrollments[course].AsReadOnly();
    }

    /// <summary>
    /// Отримати інструктора курсу
    /// </summary>
    public Instructor GetCourseInstructor(Course course)
    {
        if (course == null)
        {
            throw new ArgumentNullException(nameof(course));
        }

        return _courseInstructors.ContainsKey(course) ? _courseInstructors[course] : null;
    }

    /// <summary>
    /// Розраховувати вмісність курсу (відсоток заповненості)
    /// </summary>
    public double GetCourseCapacity(Course course)
    {
        if (!_courseEnrollments.ContainsKey(course) || course.GetMaxStudents() == 0)
        {
            return 0.0;
        }

        int enrolled = _courseEnrollments[course].Count;
        return (double)enrolled / course.GetMaxStudents() * 100;
    }

    /// <summary>
    /// Отримати статистику по курсу
    /// </summary>
    public void PrintCourseStatistics(Course course)
    {
        if (!_courseEnrollments.ContainsKey(course))
        {
            Console.WriteLine("Курс не знайдено.");
            return;
        }

        var enrolledStudents = _courseEnrollments[course];
        var instructor = GetCourseInstructor(course);

        Console.WriteLine($"\n=== Статистика курсу: {course.Name} ===");
        Console.WriteLine($"ID Курсу: {course.Id}");
        Console.WriteLine($"Опис: {course.Name}");
        Console.WriteLine($"Кредити: {course.Credits}");
        Console.WriteLine($"Викладач: {(instructor != null ? instructor.GetFullName() : "Не призначений")}");
        Console.WriteLine($"Записаних студентів: {enrolledStudents.Count} / {course.GetMaxStudents()}");
        Console.WriteLine($"Заповненість: {GetCourseCapacity(course):F1}%");
        
        if (enrolledStudents.Count > 0)
        {
            Console.WriteLine("\nСписок студентів:");
            foreach (var student in enrolledStudents)
            {
                Console.WriteLine($"  - {student.GetFullName()} (№ {student.StudentNumber}, GPA: {student.GPA:F2})");
            }
        }
        else
        {
            Console.WriteLine("\nНа цьому курсі немає студентів.");
        }
    }
}
