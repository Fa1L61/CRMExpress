using TestCRMExpert.Models;

public class MeetingController
{
    private List<Meeting> _meetings = new List<Meeting>();

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("1. Добавить встречу");
            Console.WriteLine("2. Изменить встречу");
            Console.WriteLine("3. Удалить встречу");
            Console.WriteLine("4. Просмотреть расписание");
            Console.WriteLine("5. Экспортировать расписание");
            Console.WriteLine("6. Выход");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddMeeting();
                    break;
                case "2":
                    EditMeeting();
                    break;
                case "3":
                    DeleteMeeting();
                    break;
                case "4":
                    ViewSchedule();
                    break;
                case "5":
                    ExportSchedule();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Неправильный выбор");
                    break;
            }
        }
    }

    private void AddMeeting()
    {
        try
        {
            Console.Write("Введите имя встречи: ");
            var name = Console.ReadLine();
            Console.Write("Введите время начала встречи (yyyy-MM-dd HH:mm): ");
            var startTime = DateTime.Parse(Console.ReadLine());
            Console.Write("Введите время окончания встречи (yyyy-MM-dd HH:mm): ");
            var endTime = DateTime.Parse(Console.ReadLine());
            Console.Write("Введите время напоминания (HH:mm): ");
            var reminder = DateTime.Parse(Console.ReadLine());

            if (endTime <= startTime)
            {
                Console.WriteLine("Время окончания должно быть больше времени начала");
                return;
            }

            if (_meetings.Any(m => m.StartTime < endTime && m.EndTime > startTime) )
            {
                Console.WriteLine("Встречи пересекаются");
                return;
            }

            if (_meetings.Any(m => m.Name == name))
            {
                Console.WriteLine("Встреча с таким названием уже запланирована");
                return;
            }

            _meetings.Add(new Meeting(name, startTime, endTime, reminder));
            Console.WriteLine("Встреча добавлена");
        }
        catch
        {
            Console.WriteLine("Не удалось добавить встречу");
        }
    }

    private void EditMeeting()
    {
        Console.Write("Введите имя встречи: ");
        var name = Console.ReadLine();

        var meeting = _meetings.FirstOrDefault(m => m.Name == name);

        if (meeting == null)
        {
            Console.WriteLine("Встреча не найдена");
            return;
        }

        try
        {
            Console.Write("Введите новое время начала встречи (yyyy-MM-dd HH:mm): ");
            var newStartTime = DateTime.Parse(Console.ReadLine());
            Console.Write("Введите новое время окончания встречи (yyyy-MM-dd HH:mm): ");
            var newEndTime = DateTime.Parse(Console.ReadLine());
            Console.Write("Введите новое время напоминания (HH:mm): ");
            var newReminder = DateTime.Parse(Console.ReadLine());

            if (newEndTime <= newStartTime)
            {
                Console.WriteLine("Время окончания должно быть больше времени начала");
                return;
            }

            if (_meetings.Any(m => m != meeting && m.StartTime < newEndTime && m.EndTime > newStartTime))
            {
                Console.WriteLine("Встречи пересекаются");
                return;
            }

            meeting.StartTime = newStartTime;
            meeting.EndTime = newEndTime;
            meeting.Reminder = newReminder;
            Console.WriteLine("Встреча изменена");
        }
        catch
        {
            Console.WriteLine("Неверный формат ввода данных");
        }

    }

    private void DeleteMeeting()
    {
        Console.Write("Введите имя встречи: ");
        var name = Console.ReadLine();

        var meeting = _meetings.FirstOrDefault(m => m.Name == name);

        if (meeting == null)
        {
            Console.WriteLine("Встреча не найдена");
            return;
        }

        _meetings.Remove(meeting);
        Console.WriteLine("Встреча удалена");
    }

    private void ViewSchedule()
    {
        Console.Write("Введите дату (yyyy-MM-dd): ");
        var date = DateTime.Parse(Console.ReadLine());

        var meetings = _meetings.Where(m => m.StartTime.Date == date.Date).ToList();

        if (meetings.Count == 0)
        {
            Console.WriteLine("Встреч нет");
            return;
        }

        foreach (var meeting in meetings)
        {
            Console.WriteLine($"{meeting.Name} - {meeting.StartTime} - {meeting.EndTime}");
        }
    }

    private void ExportSchedule()
    {
        Console.Write("Введите дату (yyyy-MM-dd): ");
        var date = DateTime.Parse(Console.ReadLine());

        var meetings = _meetings.Where(m => m.StartTime.Date == date.Date).ToList();

        if (meetings.Count == 0)
        {
            Console.WriteLine("Встреч нет");
            return;
        }

        var filePath = "schedule.txt";

        using (var writer = new StreamWriter(filePath))
        {
            foreach (var meeting in meetings)
            {
                writer.WriteLine($"{meeting.Name} - {meeting.StartTime} - {meeting.EndTime}");
            }
        }

        Console.WriteLine("Расписание экспортировано в файл schedule.txt");
    }

    public void CheckMeetingStarts()
    {
        foreach (var meeting in _meetings)
        {
            if (meeting.Reminder.ToString("MM/dd/yyyy H:mm") == DateTime.Now.ToString("MM/dd/yyyy H:mm"))
            {
                Console.WriteLine($"Напоминание о встрече {meeting.Name}: начало в {meeting.StartTime}");
            }
        }
    }
}
