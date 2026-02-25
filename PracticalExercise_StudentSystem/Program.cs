using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        
        Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║  Система управління студентами та курсами                 ║");
        Console.WriteLine("║  Практичне завдання з ООП (Object-Oriented Programming)   ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");

        // 1. Створення викладачів
        Console.WriteLine("[Крок 1] Створення викладачів...");
        var instructor1 = new Instructor(1, "Іван", "Павленко", "ivan.pavlenko@university.edu", 
                                        "Комп'ютерні науки", "Програмування");
        var instructor2 = new Instructor(2, "Марія", "Волкова", "maria.volkova@university.edu", 
                                        "Комп'ютерні науки", "Бази даних");
        
        Console.WriteLine($"  ✓ {instructor1.GetInfo()}");
        Console.WriteLine($"  ✓ {instructor2.GetInfo()}\n");

        // 2. Створення курсів
        Console.WriteLine("[Крок 2] Створення курсів...");
        var course1 = new UniversityCourse(101, "Основи C#", "Початковий курс програмування на C#", 3);
        var course2 = new UniversityCourse(102, "Веб-розробка", "Розробка веб-додатків", 4);
        var course3 = new LabCourse(103, "Лабораторія C#", "Практичні роботи з C#", 2, "Visual Studio 2022");
        
        Console.WriteLine($"  ✓ {course1.GetInfo()}");
        Console.WriteLine($"  ✓ {course2.GetInfo()}");
        Console.WriteLine($"  ✓ {course3.GetInfo()}\n");

        // 3. Ініціалізація менеджера курсів та призначення викладачів
        Console.WriteLine("[Крок 3] Інтеграція менеджера курсів и призначення викладачів...");
        var courseManager = new CourseManager();
        
        courseManager.RegisterCourse(course1);
        courseManager.RegisterCourse(course2);
        courseManager.RegisterCourse(course3);
        
        courseManager.AssignInstructor(course1, instructor1);
        courseManager.AssignInstructor(course2, instructor2);
        courseManager.AssignInstructor(course3, instructor1);
        
        instructor1.AssignCourse(course1);
        instructor1.AssignCourse(course3);
        instructor2.AssignCourse(course2);
        
        Console.WriteLine("  ✓ Курси зареєстровані та викладачі призначені\n");

        // 4. Створення студентів
        Console.WriteLine("[Крок 4] Створення студентів...");
        var students = new List<Student>
        {
            new Student(201, "Олег", "Коваленко", "oleg.kovalenko@student.edu", "2023-101", 3.85),
            new Student(202, "Світлана", "Козак", "svitlana.kozak@student.edu", "2023-102", 3.92),
            new Student(203, "Петро", "Сидоренко", "petro.sydorenko@student.edu", "2023-103", 3.45),
            new Student(204, "Анна", "Шевченко", "anna.shevchenko@student.edu", "2023-104", 3.78),
            new Student(205, "Максим", "Грищенко", "maksym.hryshchenko@student.edu", "2023-105", 3.55)
        };

        foreach (var student in students)
        {
            Console.WriteLine($"  ✓ {student.GetInfo()}");
        }
        Console.WriteLine();

        // 5. Запис студентів на курси (Демонстрація IEnrollable)
        Console.WriteLine("[Крок 5] Запис студентів на курси...");
        try
        {
            courseManager.Enroll(students[0], course1); // Олег -> Основи C#
            courseManager.Enroll(students[0], course3); // Олег -> Лабораторія C#
            courseManager.Enroll(students[1], course1); // Світлана -> Основи C#
            courseManager.Enroll(students[1], course2); // Світлана -> Веб-розробка
            courseManager.Enroll(students[2], course2); // Петро -> Веб-розробка
            courseManager.Enroll(students[2], course3); // Петро -> Лабораторія C#
            courseManager.Enroll(students[3], course1); // Анна -> Основи C#
            courseManager.Enroll(students[4], course3); // Максим -> Лабораторія C#
            
            Console.WriteLine("  ✓ Студенти успішно записані на курси\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  ✗ Помилка при записі: {ex.Message}\n");
        }

        // 6. Виведення детальної інформації про студентів
        Console.WriteLine("[Крок 6] Детальна інформація про студентів...");
        foreach (var student in students)
        {
            student.DisplayDetailedInfo();
        }
        Console.WriteLine();

        // 7. Виведення статистики викладачів
        Console.WriteLine("[Крок 7] Інформація про викладачів...");
        instructor1.DisplayDetailedInfo();
        Console.WriteLine();
        instructor2.DisplayDetailedInfo();
        Console.WriteLine();

        // 8. Виведення статистики курсів
        Console.WriteLine("[Крок 8] Статистика курсів...");
        courseManager.PrintCourseStatistics(course1);
        courseManager.PrintCourseStatistics(course2);
        courseManager.PrintCourseStatistics(course3);
        Console.WriteLine();

        // 9. Демонстрація відписки зі змінами
        Console.WriteLine("[Крок 9] Демонстрація відписки студента...");
        Console.WriteLine($"До відписки - {students[0].GetFullName()} записаний на {students[0].GetEnrolledCoursesCount()} курсів.");
        
        bool unenrollResult = courseManager.Unenroll(students[0], course3);
        Console.WriteLine($"Відписка зі курсу '{course3.Name}': {(unenrollResult ? "успішна" : "невдала")}");
        Console.WriteLine($"Після відписки - {students[0].GetFullName()} записаний на {students[0].GetEnrolledCoursesCount()} курсів.\n");

        // 10. Розрахунок загальних кредитів для студента
        Console.WriteLine("[Крок 10] Загальна статистика по студентам...");
        foreach (var student in students)
        {
            Console.WriteLine($"{student.GetFullName()}: " +
                            $"{student.GetEnrolledCoursesCount()} курсів, " +
                            $"{student.GetTotalCredits()} кредитів");
        }
        Console.WriteLine();

        // 11. Завершення програми
        Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║  Програма завершена. Дякуємо за використання системи!    ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
    }
}
