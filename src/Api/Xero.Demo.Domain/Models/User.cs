using System.Collections.Generic;
using static Xero.Demo.Api.Domain.Models.CONSTANTS;

namespace Xero.Demo.Api.Xero.Demo.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }

        public static List<User> Users = new List<User>
        {
            new User {Id= 1,Name= "Xero", Username="Admin", Password="Password",Role=Roles.Admin },
            new User {Id= 2,Name= "Azy", Username="Editor", Password="Password",Role=Roles.Editor},
            new User {Id= 2,Name= "Azy", Username="Reader", Password="Password",Role=Roles.Reader}
        };
    }
}