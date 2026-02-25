/// <summary>
/// Інтерфейс для сутностей, що мають унікальний ідентифікатор
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Унікальний ідентифікатор
    /// </summary>
    int Id { get; }
}

/// <summary>
/// Інтерфейс для керування записами у курси
/// </summary>
public interface IEnrollable
{
    /// <summary>
    /// Записати студента на курс
    /// </summary>
    /// <param name="student">Студент для запису</param>
    /// <param name="course">Курс для запису</param>
    /// <returns>true якщо успішно записано, false якщо студент вже записаний</returns>
    bool Enroll(Student student, Course course);

    /// <summary>
    /// Відписати студента від курсу
    /// </summary>
    /// <param name="student">Студент для видалення</param>
    /// <param name="course">Курс для видалення</param>
    /// <returns>true якщо успішно відписано, false якщо студент не був записаний</returns>
    bool Unenroll(Student student, Course course);
}

/// <summary>
/// Інтерфейс для управління сутностями
/// </summary>
public interface IManageable
{
    /// <summary>
    /// Отримати інформацію про сутність у форматі рядка
    /// </summary>
    /// <returns>Рядкове представлення сутності</returns>
    string GetInfo();
}
