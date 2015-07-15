using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using binaryorm.Models;

namespace binaryorm
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BinaryOrmDbContext())
            {
                //1. Список людей, которые прошли тесты.
                var passedUsers2 = db.TestWorks.Where(n => n.Result >= n.Test.PassMark).Select(t => new
                {
                    User = t.User.Name,
                    Result = t.Result
                }).ToList();

                Console.WriteLine("Users, who passed test:");
                passedUsers2.ForEach(n => Console.WriteLine("Name: {0} \t Result: {1}", n.User, n.Result));

                ////2. Список тех, кто прошли тесты успешно и уложилися во время.
                var passedInTimeUsers = db.TestWorks.Where(n => n.Result >= n.Test.PassMark && n.PassTime <= n.Test.MaxPassTime).Select(t => new
                {
                    User = t.User.Name,
                    Result = t.Result
                }).ToList();

                Console.WriteLine("\n\rUsers, who passed test in time:");
                passedInTimeUsers.ForEach(n => Console.WriteLine("Name: {0} \t Result: {1}", n.User, n.Result));

                ////3. Список людей, которые прошли тесты успешно и не уложились во время
                var passedOutTimeUsers = db.TestWorks.Where(n => n.Result >= n.Test.PassMark && n.PassTime > n.Test.MaxPassTime).Select(t => new
                {
                    User = t.User.Name,
                    Result = t.Result
                }).ToList();

                Console.WriteLine("\n\rUsers, who passed test out the time:");
                passedOutTimeUsers.ForEach(n => Console.WriteLine("Name: {0} \t Result: {1}", n.User, n.Result));

                ////4. Список студентов по городам. (Из Львова: 10 студентов, из Киева: 20)
                var usersByCity =
                    db.Users.GroupBy(n => n.City, (m, k) => new { City = m, Users = k.ToList() })
                        .ToList();

                Console.WriteLine("\n\rUsers by city:");
                foreach (var city in usersByCity)
                {
                    Console.WriteLine("City: {0}", city.City);
                    foreach (var user in city.Users)
                    {
                        Console.WriteLine("Student: {0} \t Age: {1} \t Univercity: {2}", user.Name, user.Age, user.Univercity);

                    }
                }

                ////5. Список успешных студентов по городам.
                var passedUsersByCity = db.TestWorks.Where(n => n.Result >= n.Test.PassMark && n.PassTime <= n.Test.MaxPassTime).Select(t => new
                {
                    User = t.User.Name,
                    City = t.User.City,
                    Result = t.Result
                }).GroupBy(n => n.City, (key, g) => new
                {
                    City = key,
                    Users = g.ToList()
                }).ToList();

                Console.WriteLine("\n\rSuccessful users by city:");
                foreach (var city in passedUsersByCity)
                {
                    Console.WriteLine("City: {0}", city.City);
                    foreach (var user in city.Users)
                    {
                        Console.WriteLine("Student: {0} \t Result: {1}", user.User, user.Result);
                        
                    }
                }

                ////6. Результат для каждого студента - его баллы, время, баллы в процентах для каждой категории.
                var resultsForUsers =
                    db.TestWorks.Select(
                        n =>
                            new
                            {
                                User = n.User.Name,
                                Result = n.Result,
                                Time = n.PassTime,
                                Category = n.Test.Category.Name,
                                PassPercent =
                                    (float) (n.Result)/
                                    db.TestWorks.Where(k => k.Test.Category.CategoryId == n.Test.Category.CategoryId)
                                        .Max(m => m.Result)*100
                            })
                        .GroupBy(n => n.User,
                            (m, k) =>
                                new
                                {
                                    User = m,
                                    Results =
                                        k.Select(
                                            n =>
                                                new
                                                {
                                                    n.Category,
                                                    n.Time,
                                                    n.Result,
                                                    n.PassPercent
                                                }).ToList()
                                })
                        .ToList();

                Console.WriteLine("\n\rResults for users:");
                foreach (var user in resultsForUsers)
                {
                    Console.WriteLine("Student: {0}", user.User);
                    foreach (var result in user.Results)
                    {
                        Console.WriteLine("Category: {0} \t PassPercent: {1} \t PassTime: {2} \t Result: {3}", result.Category, result.PassPercent, result.Time, result.Result);

                    }
                }

                ////7. Рейтинг популярности вопросов в тестах (выводить количество использования данного вопроса в тестах)
                var questionsRating =
                    db.Questions.Select(
                        question =>
                            new
                            {
                                QuestionName = question.Text,
                                Rating = db.Tests.Count(n => n.Questions.Any(m => m.QuestionId == question.QuestionId))
                            }).ToList();

                Console.WriteLine("\n\rQuestion using rating in tests:");
                questionsRating.ForEach(n => Console.WriteLine("Question: {0} \t Rating: {1}", n.QuestionName, n.Rating));

                ////8. Рейтинг учителей по количеству лекций (Количество прочитанных лекций)
                var teachersRating =
                    db.Teachers.Select(
                        teacher =>
                            new
                            {
                                Teacher = teacher.Name,
                                Rating = teacher.Lectures.Count()
                            }).ToList();

                Console.WriteLine("\n\rTeachers rating(count of lectures):");
                teachersRating.ForEach(n => Console.WriteLine("Teacher: {0} \t Rating: {1}", n.Teacher, n.Rating));

                ////9. Средний бал тестов по категориям, отсортированый по убыванию.
                var testAvg = db.Categories.Select(n => new
                {
                    Category = n.Name,
                    Average =
                        !db.TestWorks.Any(k => k.Test.Category.CategoryId == n.CategoryId)
                            ? 0
                            : db.TestWorks.Where(k => k.Test.Category.CategoryId == n.CategoryId).Average(k => k.Result)
                }).OrderByDescending(n => n.Average).ToList();

                Console.WriteLine("\n\rTests average by category:");
                testAvg.ForEach(n => Console.WriteLine("Category: {0} \t Avg: {1}", n.Category, n.Average));
                
                ////10. Рейтинг вопросов по набранным баллам
                var questionsRatingByMark = db.Questions.Select(n =>
                    new
                    {
                        Name = n.Text,
                        Rating = db.TestWorks.Where(k => k.Test.Questions.Contains(n)).Max(k => k.Result)
                    }).OrderByDescending(n=>n.Rating).ToList();

                Console.WriteLine("\n\rQuestion rating by test mark mark:");
                questionsRatingByMark.ForEach(n => Console.WriteLine("Category: {0} \t Rating: {1}", n.Name, n.Rating));


                Console.ReadLine();

            }

        }
    }
}
