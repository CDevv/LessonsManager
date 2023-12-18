using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonsManager
{
    internal class Subject
    {
		private string name;
        private Dictionary<string, Lesson> lessons;

		public string Name
		{
			get { return name; }
			set
			{
                if (value.Length < 2 || value.Length > 40)
                {
					throw new ArgumentException("Name should be between 2 and 40 characters!");
                }
                name = value;
			}
		}
        public Dictionary<string, Lesson> Lessons
        {
            get { return lessons; }
        }

        public Subject(string name)
        {
            lessons = new Dictionary<string, Lesson>();
            Name = name;
        }

        public void AddLesson(Lesson lesson)
        {
            string lessonTitle = lesson.Title;
            if (lessons.ContainsKey(lessonTitle))
            {
                throw new ArgumentException($"Lesson {lessonTitle} exists!");
            }
            lessons[lessonTitle] = lesson;
        }

        public void AddRate(string title, int rate)
        {
            if (!lessons.ContainsKey(title))
            {
                throw new ArgumentException("Lesson not found!");
            }
            lessons[title].AddRating(rate);
        }

        public double AverageRating()
        {
            return lessons.Average(x => x.Value.Rating);
        }

        public List<Lesson> GetLessonsByTeacher(string teacher)
        {
            return lessons.ToList().Select(x => x.Value).Where(x => x.Teacher == teacher).OrderByDescending(x => x.Duration).ToList();
        }

        public List<Lesson> GetLessonsBetweenDuration(int from, int to)
        {
            return lessons.ToList().Select(x => x.Value).Where(x => x.Duration >= from && x.Duration <= to).OrderByDescending(x => x.Rating).ToList();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Subject {Name}");
            sb.AppendLine($"Total Lessons: {lessons.Count}");
            return sb.ToString();
        }
    }
}
