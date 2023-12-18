using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonsManager
{
    internal static class Controller
    {
        private static Dictionary<string, Subject> subjects = new();

        public static string AddSubject(List<string> args)
        {
            string name = args[0];

            if (subjects.ContainsKey(name))
            {
                return "Subject already exists!";
            }

            Subject subject = new(name);
            subjects[name] = subject;
            return $"Created Subject {name}!";
        }

        public static string AddLesson(List<string> args)
        {
            if (args.Count < 8)
            {
                return "Not enough arguments!";
            }

            string subjectName = args[0];
            string title = args[1];
            bool durationIsValid = int.TryParse(args[2], out int duration);
            bool gradeIsValud = int.TryParse(args[3], out int grade);
            bool difficultyIsValid = int.TryParse(args[4], out int difficulty);
            string teacher = args[5];
            string type = args[6];
            string extraParam = args[7];

            if (!durationIsValid || !gradeIsValud || !difficultyIsValid)
            {
                return "Invalid arguments!";
            }
            if (!subjects.ContainsKey(subjectName))
            {
                return "Subject doesn't exist!";
            }

            Subject subject = subjects[subjectName];
            switch (type)
            {
                case "online":
                    OnlineLesson onlineLesson = new(title, duration, grade, difficulty, teacher, extraParam);
                    subject.AddLesson(onlineLesson);
                    break;
                case "onsite":
                    LectureLesson lectureLesson = new(title, duration, grade, difficulty, teacher, extraParam);
                    subject.AddLesson(lectureLesson);
                    break;
                default:
                    return "Invalid type!";
            }

            return $"Created Lesson {title} in Subject {subjectName}!";
        }

        public static string RateLesson(List<string> args)
        {
            if (args.Count < 3)
            {
                return "Not enough arguments!";
            }

            string subjectName = args[0];
            string title = args[1];
            bool ratingIsValid = int.TryParse(args[2], out int rate);

            if (!ratingIsValid)
            {
                return "Invalid arguments!";
            }
            if (!subjects.ContainsKey(subjectName))
            {
                return "Subject doesn't exist!";
            }
            Subject subject = subjects[subjectName];
            if (!subject.Lessons.ContainsKey(title))
            {
                return $"Lesson doesn't exist!";
            }

            Lesson lesson = subject.Lessons[title];
            lesson.AddRating(rate);

            return $"Rated {title} with {rate} rate.";
        }

        public static string GetAverageRating(List<string> args)
        {
            if (args.Count < 1)
            {
                return "Not enough arguments!";
            }

            string subjectName = args[0];
            if (!subjects.ContainsKey(subjectName))
            {
                return "Subject doesn't exist!";
            }

            Subject subject = subjects[subjectName];
            double averageRating = subject.AverageRating();
            return $"The average rating is: {averageRating:F2}";
        }

        public static string GetLessonsByTeacher(List<string> args)
        {
            if (args.Count < 2)
            {
                return "Not enough arguments!";
            }

            string subjectName = args[0];
            string teacher = args[1];
            if (!subjects.ContainsKey(subjectName))
            {
                return "Subject doesn't exist!";
            }

            Subject subject = subjects[subjectName];
            List<Lesson> lessons = subject.GetLessonsByTeacher(teacher);
            StringBuilder sb = new StringBuilder();
            foreach (Lesson item in lessons)
            {
                sb.Append(item);
            }
            return sb.ToString();
        }

        public static string GetLessonsBetweenDuration(List<string> args)
        {
            if (args.Count < 3)
            {
                return "Not enough arguments!";
            }

            string subjectName = args[0];
            bool fromIsValid = int.TryParse(args[1], out int from);
            bool toIsValid = int.TryParse(args[2], out int to);

            if (!fromIsValid || !toIsValid)
            {
                return "Invalid arguments!";
            }
            if (!subjects.ContainsKey(subjectName))
            {
                return "Subject doesn't exist";
            }

            Subject subject = subjects[subjectName];
            List<Lesson> lessons = subject.GetLessonsBetweenDuration(from, to);
            StringBuilder sb = new StringBuilder();
            foreach (Lesson item in lessons)
            {
                sb.AppendLine(item.ToString());
            }
            return sb.ToString();
        }
    }
}
