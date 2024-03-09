using System.ComponentModel.DataAnnotations;

namespace GorevYoneticisi.Model
{
    public class Users
    {
        public int Id { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set;}
    }
}
