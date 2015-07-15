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
                //Список людей, которые прошли тесты.
                var passedUsers2 = db.TestWorks.Where(n => n.Result >= n.Test.PassMark).Select(t => new
                {
                    User = t.User.Name,
                    Result = t.Result
                }).ToList();

                Console.WriteLine("Users, who passed test:");
                passedUsers2.ForEach(n => Console.WriteLine("Name: {0} \t Result: {1}", n.User, n.Result));

                ////Список тех, кто прошли тесты успешно и уложилися во время.
                var passedInTimeUsers = db.TestWorks.Where(n => n.Result >= n.Test.PassMark && n.PassTime <= n.Test.MaxPassTime).Select(t => new
                {
                    User = t.User.Name,
                    Result = t.Result
                }).ToList();

                Console.WriteLine("\n\rUsers, who passed test in time:");
                passedInTimeUsers.ForEach(n => Console.WriteLine("Name: {0} \t Result: {1}", n.User, n.Result));

                ////Список людей, которые прошли тесты успешно и не уложились во время
                var passedOutTimeUsers = db.TestWorks.Where(n => n.Result >= n.Test.PassMark && n.PassTime > n.Test.MaxPassTime).Select(t => new
                {
                    User = t.User.Name,
                    Result = t.Result
                }).ToList();

                Console.WriteLine("\n\rUsers, who passed test out the time:");
                passedOutTimeUsers.ForEach(n => Console.WriteLine("Name: {0} \t Result: {1}", n.User, n.Result));

                ////Список студентов по городам. (Из Львова: 10 студентов, из Киева: 20)
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

                ////Список успешных студентов по городам.
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

                ////Результат для каждого студента - его баллы, время, баллы в процентах для каждой категории.
                var resultsForUsers =
                    db.TestWorks.Select(
                        n =>
                            new
                            {
                                User = n.User.Name,
                                Result = n.Result,
                                Time = n.PassTime,
                                Category = n.Test.Category.Name,
                                PassPercent = (float)(n.Result) / n.Test.PassMark * 100
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

                Console.ReadLine();

            }

        }
    }
}
