using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;

namespace binaryorm.Models
{
    public class BinaryOrmDbInitializer : DropCreateDatabaseAlways<BinaryOrmDbContext>
    {
        protected override void Seed(BinaryOrmDbContext context)
        {
            IList<Category> categories = new List<Category>();
            categories.Add(new Category() { Name = ".Net" });
            categories.Add(new Category() { Name = "JS" });
            categories.Add(new Category() { Name = "PHP" });
            categories.Add(new Category() { Name = "DB" });
            categories.Add(new Category() { Name = "OOP" });
            categories.Add(new Category() { Name = "English" });
            context.Categories.AddRange(categories);


            IList<User> users = new List<User>();
            users.Add(new User()
            {
                Name = "Brian West",
                Email = "dfs@ddfd.com",
                Age = 12,
                City = "Lviv",
                Category = categories[1],
                Univercity = "5556"
            });
            users.Add(new User()
            {
                Name = "Donnell Titus",
                Email = "dfs@ddfd.com",
                Age = 12,
                City = "Lviv",
                Category = categories[2],
                Univercity = "5556"
            });
            users.Add(new User()
            {
                Name = "Hunter Sansing",
                Email = "dfs@ddfd.com",
                Age = 12,
                City = "Kyiv",
                Category = categories[1],
                Univercity = "5556"
            });
            users.Add(new User()
            {
                Name = "Lou Antonucci",
                Email = "dfs@ddfd.com",
                Age = 12,
                City = "Kyiv",
                Category = categories[1],
                Univercity = "5556"
            });
            users.Add(new User()
            {
                Name = "Iain Milito",
                Email = "dfs@ddfd.com",
                Age = 12,
                City = "Kyiv",
                Category = categories[3],
                Univercity = "5556"
            });
            users.Add(new User()
            {
                Name = "Calla Sills",
                Email = "dfs@ddfd.com",
                Age = 12,
                City = "Zaporizhya",
                Category = categories[3],
                Univercity = "5556"
            });
            context.Users.AddRange(users);



            var questions = new List<Question>()
            {
                new Question()
                {
                    Category = categories[0],
                    Text = "Question1 text"
                },
                new Question()
                {
                    Category = categories[0],
                    Text = "Question2 text"
                },
                new Question()
                {
                    Category = categories[0],
                    Text = "Question3 text"
                },
                new Question()
                {
                    Category = categories[0],
                    Text = "Question4 text"
                },
            };

            context.Questions.AddRange(questions);


            var testsCategory0 = new List<Test>()
            {
                new Test()
                {
                    Category = categories[0],
                    MaxPassTime = 48,
                    Name = ".Net Basic Test",
                    PassMark = 50,
                    Questions = new List<Question>() {questions[0], questions[1]}
                },
                new Test()
                {
                    Category = categories[0],
                    MaxPassTime = 48,
                    Name = ".Net Advanced Test",
                    PassMark = 50,
                    Questions = new List<Question>() {questions[1], questions[2]}
                }
            };
            context.Tests.AddRange(testsCategory0);
            var testsCategory1 = new List<Test>()
            {
                new Test()
                {
                    Category = categories[1],
                    MaxPassTime = 48,
                    Name = "JS Basic Test",
                    PassMark = 50,
                    Questions = questions
                },
                new Test()
                {
                    Category = categories[1],
                    MaxPassTime = 48,
                    Name = "JS Advanced Test",
                    PassMark = 50,
                    Questions = questions
                }
            };
            context.Tests.AddRange(testsCategory1);

            var testsCategory2 = new List<Test>()
            {
                new Test()
                {
                    Category = categories[2],
                    MaxPassTime = 48,
                    Name = "PHP Basic Test",
                    PassMark = 50,
                    Questions = questions
                },
                new Test()
                {
                    Category = categories[2],
                    MaxPassTime = 48,
                    Name = "PHP Advanced Test",
                    PassMark = 50,
                    Questions = questions
                }
            };
            context.Tests.AddRange(testsCategory2);

            var testsCategory3 = new List<Test>()
            {
                new Test()
                {
                    Category = categories[3],
                    MaxPassTime = 48,
                    Name = "DB Basic Test",
                    PassMark = 50,
                    Questions = questions
                },
                new Test()
                {
                    Category = categories[3],
                    MaxPassTime = 48,
                    Name = "DB Advanced Test",
                    PassMark = 50,
                    Questions = questions
                }
            };
            context.Tests.AddRange(testsCategory3);

            var testsCategory5 = new List<Test>()
            {
                new Test()
                {
                    Category = categories[4],
                    MaxPassTime = 48,
                    Name = "English Basic Test",
                    PassMark = 50,
                    Questions = questions
                },
                new Test()
                {
                    Category = categories[4],
                    MaxPassTime = 48,
                    Name = "English Speaking",
                    PassMark = 50,
                    Questions = questions
                }
            };
            context.Tests.AddRange(testsCategory5);

            var testWorks = new List<TestWork>()
            {
                new TestWork()
                {
                    PassTime = 34,
                    Result = 66,
                    Test = testsCategory0[0],
                    User = users[0]
                },
                new TestWork()
                {
                    PassTime = 34,
                    Result = 66,
                    Test = testsCategory1[1],
                    User = users[1]
                },
                new TestWork()
                {
                    PassTime = 34,
                    Result = 66,
                    Test = testsCategory5[0],
                    User = users[2]
                },
                new TestWork()
                {
                    PassTime = 34,
                    Result = 66,
                    Test = testsCategory0[0],
                    User = users[4]
                },
                new TestWork()
                {
                    PassTime = 74,
                    Result = 66,
                    Test = testsCategory1[1],
                    User = users[4]
                },
                new TestWork()
                {
                    PassTime = 34,
                    Result = 66,
                    Test = testsCategory5[0],
                    User = users[5]
                },
            };

            context.TestWorks.AddRange(testWorks);


            var lectures = new List<Lecture>()
            {
                new Lecture()
                {
                    Name = "BSA 15. GIT",
                    Category = categories[0],
                    Description = ""
                },
                new Lecture()
                {
                    Name = "BSA 15. .NET Lecture 4. Dependency Injection & Inversion of Control",
                    Category = categories[0],
                    Description = ""
                },
                new Lecture()
                {
                    Name = "Binary Studio Academy 2015: Introduction",
                    Category = categories[0],
                    Description = ""
                },
                new Lecture()
                {
                    Name = "BSA 15 .NET SQL",
                    Category = categories[0],
                    Description = ""
                },
                new Lecture()
                {
                    Name = "BSA 15 .NET Code Testing",
                    Category = categories[0],
                    Description = ""
                },
                new Lecture()
                {
                    Name = "BSA 15 .NET Code Testing",
                    Category = categories[0],
                    Description = ""
                }
            };

            context.Lectures.AddRange(lectures);

            var teachers = new List<Teacher>()
            {
                new Teacher()
                {
                    Name = "Troy J. Jones",
                    Lectures = new List<Lecture>(){lectures[0], lectures[1],lectures[2]}
                },
                new Teacher()
                {
                    Name = "Gail D. Kirk",
                    Lectures = new List<Lecture>(){lectures[3], lectures[4]}

                },
                new Teacher()
                {
                    Name = "John T. Velez",
                    Lectures = new List<Lecture>(){lectures[5]}

                }
            };

            context.Teachers.AddRange(teachers);


            teachers.AddRange(teachers);

            base.Seed(context);
        }
    }
   
       
        
}