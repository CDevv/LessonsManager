namespace LessonsManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            while (input != "End")
            {
                string[] tokens = input.Split();
                List<string> arguments = new List<string>();
                for (int i = 1; i < tokens.Length; i++)
                {
                    arguments.Add(tokens[i]);
                }
                string command = tokens[0];
                string output = "";
                switch (command)
                {
                    case "AddSubject":
                        output = Controller.AddSubject(arguments);
                        break;
                    case "AddLesson":
                        output = Controller.AddLesson(arguments);
                        break;
                    case "RateLesson":
                        output = Controller.RateLesson(arguments);
                        break;
                    case "GetAverageRating":
                        output = Controller.GetAverageRating(arguments);
                        break;
                    case "GetLessonsByTeacher":
                        output = Controller.GetLessonsByTeacher(arguments);
                        break;
                    case "GetLessonsBetweenDuration":
                        output = Controller.GetLessonsBetweenDuration(arguments);
                        break;
                    default:
                        output = "No such command!";
                        break;
                }
                Console.WriteLine(output);
                input = Console.ReadLine();
            }
        }
    }
}