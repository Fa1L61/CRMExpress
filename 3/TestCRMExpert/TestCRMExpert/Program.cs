class Program
{
    static void Main(string[] args)
    {
        var meetingPlanner = new MeetingController();

        Thread reminderThread = new Thread(CheckMeetingStarts);
        reminderThread.Start(meetingPlanner);

        meetingPlanner.Run();

        Console.ReadKey();
    }

    static void CheckMeetingStarts(object meetingManagerObj)
    {
        MeetingController meetingManager = (MeetingController)meetingManagerObj;

        while (true)
        {
            meetingManager.CheckMeetingStarts(); // Проверка начала встреч 

            Thread.Sleep(60000); // Проверка каждую минуту 
        }
    }
}


