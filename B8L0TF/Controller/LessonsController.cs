﻿using B8L0TF.Json;
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
        private ReadAndWriteJson ReadAndWriteJson = new();
        private List<Lesson> _lessons = new();
        private User _user = new();


        public async Task Init(string userName)
        {
            await ReadAndWriteJson.InitPlayedGames();
            _user.Name = userName;
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

            int firstDigit = rnd.Next((int)Math.Pow(10,lesson.difficulty));
            int secondDigit = rnd.Next((int)Math.Pow(10, lesson.difficulty));

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

        public async void Run()
        {
            int score = 0, questionNumber = 1;
            int? result;
            
            Console.WriteLine("A jatek hamarosan indul 10 feladatot kapsz sok sikert!");
            Console.WriteLine("A jatek hamarosan kezdodik...");
            
            System.Threading.Thread.Sleep(4000);
            Console.Clear();
            
            while (true)
            {
                foreach(var lesson in _lessons)
                {
                    Console.WriteLine($"{questionNumber}. Feladat:");
                    Console.WriteLine($"{lesson.text}");

                    try
                    {
                        result = int.Parse(Console.ReadLine());
                        if (result == lesson.result)
                        {
                            score += lesson.difficulty;
                            Console.WriteLine("A valaszod helyes!");
                        }
                        else
                        {
                            Console.WriteLine("A valaszod helytelen!");
                        }

                    } catch (Exception)
                    {
                        Console.WriteLine("Maskor szamot adj meg!");
                    }
                    
                    questionNumber++;
                    
                    System.Threading.Thread.Sleep(2000);
                    Console.Clear();
                }

                Console.WriteLine("Ha el akarod menteni az eredmenyedet ird be a 'save' parancsot, ha nem, akkor barmit beirhatsz");
                string save = Console.ReadLine();
                if (save == "save")
                {
                    SaveGameResult(score);
                    _user.result = score;
                }
            }
        }

        private void SaveGameResult(int score)
        {
            ReadAndWriteJson.AddGameToList(_user.Name, score);
            ReadAndWriteJson.SaveGames();
        }
    }
}
