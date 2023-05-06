using B8L0TF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace B8L0TF.Controller
{
    internal class LessonsController
    {

        private List<Lesson> _lessons = new();

        public void Init()
        {
            _lessons = CreateLessons();
            Run();
        }

        private List<Lesson> CreateLessons()
        {
            for (int i = 0; i < 10; i++)
            {
                int lessonDifficulty = new Random().Next(1, 4);
                Lesson lesson = new(lessonDifficulty);
                CreateSingleLesson(lesson);
                _lessons.Add(lesson);
            }
            return _lessons;
        }

        private static Lesson CreateSingleLesson(Lesson lesson)
        {
            Random rnd = new();

            int firstDigit = rnd.Next(lesson.difficulty * 10);
            int secondDigit = rnd.Next(lesson.difficulty * 10);

            char mathOperator;

            switch (rnd.Next(1, 4))
            {
                case 1:
                    mathOperator = '+';
                    lesson.result = firstDigit + secondDigit;
                    lesson.text = firstDigit.ToString() + mathOperator + secondDigit.ToString() + "=";
                    break;
                case 2:
                    mathOperator = '-';
                    lesson.result = firstDigit - secondDigit;
                    lesson.text = firstDigit.ToString() + mathOperator + secondDigit.ToString() + "="; 
                    break;
                case 3:
                    mathOperator = '*';
                    lesson.result = firstDigit * secondDigit;
                    lesson.text = firstDigit.ToString() + mathOperator + secondDigit.ToString() + "="; 
                    break;
                default:
                    throw new Exception("Hibas futas!");
            }

            return lesson;
        }

        public static void Run()
        {
            Console.WriteLine($"GameRun");
        }
    }
}
