using System.Collections.Generic;
using static Dotnet.Sample.Shared.CONSTANTS;

namespace Dotnet.Sample.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }

        public static readonly List<User> Users = new List<User>
        {
            new User {Id= 1,Name= "Admin", Username="Admin", Password="Password",Role=Roles.Admin },
            new User {Id= 2,Name= "Editor", Username="Editor", Password="Password",Role=Roles.Editor},
            new User {Id= 3,Name= "Reader", Username="Reader", Password="Password",Role=Roles.Reader}
        };
    }
}