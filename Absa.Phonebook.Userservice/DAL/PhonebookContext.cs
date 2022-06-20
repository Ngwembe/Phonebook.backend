using Absa.Phonebook.Contactservice.Models;
using Microsoft.EntityFrameworkCore;

namespace Absa.Phonebook.Contactservice.DAL
{
    public class PhonebookContext : DbContext
    {
        DbSet<Person> Person { get; set; }
        DbSet<Contact> Contact { get; set; }

        public PhonebookContext(DbContextOptions options) : base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test");
        //}
    }
}
