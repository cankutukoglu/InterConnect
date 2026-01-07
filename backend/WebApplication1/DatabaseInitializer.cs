using LinkedInClone.Api.Data;
using LinkedInClone.Api.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

public static class DatabaseInitializer
{
    public static void DefaultUsers(AppDbContext context)
    {
        context.Database.Migrate();
        if (!context.Users.Any())
        {
            var user1 = new User
            {
                Email = "user1@mail.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("user1"),
                CreatedAt = DateTime.UtcNow,
                Profile = new Profile
                {
                    FullName = "Tim Cook",
                    City = "California",
                    Country = "USA",
                    Description = "CEO of Apple Inc.",
                    ProfilePic = "https://upload.wikimedia.org/wikipedia/commons/8/8e/Visit_of_Tim_Cook_to_the_European_Commission_-_P061904-946789.jpg",
                    ConnectionCount = 953
                },
                Experiences = new List<Experience>
                {
                    new Experience
                    {
                        Company = "Apple Inc.",
                        Title = "Chief Executive Officer",
                        StartDate = new DateOnly(1998, 8, 14),
                        EndDate = null,
                        City = "California",
                        Country = "USA",
                        Description = "Established Apple Inc, working as CEO",
                        LogoPicUrl = "https://upload.wikimedia.org/wikipedia/commons/f/fa/Apple_logo_black.svg",
                        IsCurrent = true
                    },
                    new Experience
                    {
                        Company = "IBM",
                        Title = "Logistics Director",
                        StartDate = new DateOnly(1989, 1, 1),
                        EndDate = new DateOnly(1998, 7, 31),
                        City = "New York",
                        Country = "USA",
                        Description = "Logistics Director of IBM",
                        LogoPicUrl = "https://upload.wikimedia.org/wikipedia/commons/5/51/IBM_logo.svg",
                        IsCurrent = false
                    }
                },
                Educations = new List<Education>
                {
                    new Education
                    {
                        School = "Duke University",
                        Degree = "MBA - Computer Science",
                        City = "North Carolina",
                        Country = "USA",
                        StartYear = 1984,
                        EndYear = 1986,
                        Activities = "Robotics Club",
                        LogoPicUrl = "https://upload.wikimedia.org/wikipedia/en/c/ce/Duke_University_seal.svg"
                    },
                    new Education
                    {
                        School = "Auburn University",
                        Degree = "BS - Computer Science",
                        City = "Auburn",
                        Country = "USA",
                        StartYear = 1980,
                        EndYear = 1984,
                        Activities = "ML Research Group",
                        LogoPicUrl = "https://upload.wikimedia.org/wikipedia/en/7/7f/Auburn_University_seal.svg"
                    }
                },
                Skills = new List<Skill>
                {
                    new Skill { Name = "Logistics" },
                    new Skill { Name = "Machine Learning" },
                    new Skill { Name = "C++" }
                },
                Achievements = new List<Achievement>
                {
                    new Achievement { Title = "Best Developer", Year = 1986, Description = "Annual awards" },
                    new Achievement { Title = "Hackathon Winner", Year = 1984, Description = "Company hackathon" }
                },
                Languages = new List<Language>
                {
                    new Language { Name = "English" },
                    new Language { Name = "German" },
                    new Language { Name = "French" },
                }
            };

            var user2 = new User
            {
                Email = "user2@mail.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("user2"),
                CreatedAt = DateTime.UtcNow,
                Profile = new Profile
                {
                    FullName = "Jeff Bezos",
                    City = "Seattle",
                    Country = "USA",
                    Description = "CEO of Amazon",
                    ProfilePic = "https://upload.wikimedia.org/wikipedia/commons/6/6c/Jeff_Bezos_at_Amazon_Spheres_Grand_Opening_in_Seattle_-_2018_%2839074799225%29_%28cropped%29.jpg",
                    ConnectionCount = 1092
                },
                Experiences = new List<Experience>
                {
                    new Experience
                    {
                        Company = "Amazon",
                        Title = "Chief Executive Officer",
                        StartDate = new DateOnly(1994, 6, 1),
                        EndDate = null,
                        City = "Seattle",
                        Country = "USA",
                        Description = "Established Amazon, working as CEO",
                        LogoPicUrl = "https://upload.wikimedia.org/wikipedia/commons/a/a9/Amazon_logo.svg",
                        IsCurrent = true
                    },
                },
                Educations = new List<Education>
                {
                    new Education
                    {
                        School = "Princeton University",
                        Degree = "BS - Electronics Engineering ",
                        City = "New Jersey",
                        Country = "USA",
                        StartYear = 1982,
                        EndYear = 1986,
                        Activities = "Cyber Security Society",
                        LogoPicUrl = "https://upload.wikimedia.org/wikipedia/commons/d/d0/Princeton_seal.svg"
                    },
                },
                Skills = new List<Skill>
                {
                    new Skill { Name = "Java" },
                    new Skill { Name = "C++" }
                },
                Achievements = new List<Achievement>
                {
                    new Achievement { Title = "Top Performer", Year = 1989, Description = "Quarterly recognition" },
                    new Achievement { Title = "Open Source Contributor", Year = 1988, Description = "OSS libraries" }
                },
                Languages = new List<Language>
                {
                    new Language { Name = "English" },
                    new Language { Name = "German" },
                    new Language { Name = "Spanish" }
                }
            };

            context.Users.Add(user1);
            context.Users.Add(user2);
            context.SaveChanges();
        }
    }
}
