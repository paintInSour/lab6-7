using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4_5
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           // Application.Run(new Form1());
            using (var context = new BookStore())
            {
                Category category1 = new Category()
                {
                    CategoryName = "category 1",
                };
                Category category2 = new Category()
                {
                    CategoryName = "category 2",
                };

                Book book1 = new Book()
                {
                    Title = "MyBook 1",
                    Categories = new List<Category>()
                {
                    category1,
                    category2
                }
                };
                Book book2 = new Book()
                {
                    Title = "MyBook 2",
                    Categories = new List<Category>()
                {
                    category1
                }
                };
                Book book3 = new Book()
                {
                    Title = "MyBook 3",
                    Categories = new List<Category>()
                {
                    category1
                }
                };

                context.Books.Add(book1);
                context.Books.Add(book2);
                context.Books.Add(book3);

                Author author = new Author()
                {
                    Name = "Mark",
                    Books = new List<Book>
                {
                    book1,
                    book2,
                }
                };
                Author author2 = new Author()
                {
                    Name = "Mask",
                    Books = new List<Book>
                {
                    book3,
                }
                };
                context.Authors.Add(author);
                 context.Authors.Add(author2);

                context.Dates.Add(new BookReleaseDate() { BookId = 1,Year="2094" }); 
;                context.Dates.Add(new BookReleaseDate() { BookId = 2, Year = "2000" });
;                context.Dates.Add(new BookReleaseDate() { BookId = 3, Year = "2001" });

                context.SaveChanges();

                var categories = context.Categories.ToList();
                var books = context.Books.ToList();

                

                //List<User> users = new List<User>();
                //users.Add(new User("e1", 23, new List<string> { "ua", "en" }));
                //users.Add(new User("e2", 56, new List<string> { "ru", "en" }));
                //users.Add(new User("e3", 8, new List<string> { "ua", "ch" }));
                //users.Add(new User("e4", 40, new List<string> { "ua", "en" }));

                //users.Where(u => u.Age < 30 && u.Languages.Contains("en")).ToList().ForEach(u=>Debug.WriteLine(u.Name));

                var group = context.Books.GroupBy(b => b.Author.Name).ToList();
                group.ToList().ForEach(g => g.ToList()
                                             .ForEach(record => Debug.WriteLine("book: {0} , author: {1}", record.Title, record.Author.Name)));
                var joined = context.Dates.Join(context.Books, d => d.BookId, b => b.BookId, (d, b) => new {Year= d.Year,Title=b.Title,Writer=b.Author.Name });
                
                joined.ToList().ForEach(item => Debug.WriteLine("year: {0}, title:{1},author:{2}", item.Year, item.Title, item.Writer));

                var union = context.Authors.Where(o => o.Books.Count < 10).Union(context.Authors.Where(b => b.Name.Contains("1")));
                union.ToList().ForEach(i => Debug.WriteLine(i.Name));

                var inter = context.Books.Where(o => o.Title.Contains("1")).Intersect(context.Books.Where(b => b.Title.Contains("2")));
                inter.ToList().ForEach(i => Debug.WriteLine(i.Title));

                var excep = context.Dates.Where(o => o.Year == "1994").Except(context.Dates.Where(i => i.Year == "2001"));
                excep.ToList().ForEach(i => Debug.WriteLine(i.Year));

                var sorted = context.Dates.Join(context.Books, d => d.BookId, b => b.BookId, (d, b) => new { Book = b.Title, Year = d.Year })
                    .OrderBy(u=>u.Year);
                Debug.WriteLine("sorted");
                sorted.ToList().ForEach(u => Debug.WriteLine($"{u.Book} : {u.Year}"));

                var cnt = context.Books.Where(i => i.Title.Contains("My")).Count();
                Debug.WriteLine(cnt);



            }
        }
    }
    public class User
    {
        string name;
        int age;
        List<string> languages;

        public User()
        {
        }

        public User(string name, int age, List<string> languages)
        {
            this.Name = name;
            this.Age = age;
            this.Languages = languages;
        }

        public string Name { get => name; set => name = value; }
        public int Age { get => age; set => age = value; }
        public List<string> Languages { get => languages; set => languages = value; }
    }

    public class BookStore : DbContext
    {
        public BookStore() : base("db")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                   .HasMany(b => b.Categories)
                   .WithMany(c => c.Books)
                   .Map(cs =>
                   {
                       cs.MapLeftKey("BookId");
                       cs.MapRightKey("CategoryId");
                       cs.ToTable("BookCategories");
                   });

        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookReleaseDate> Dates { get; set; }
    }
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
    public class Book
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }

    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
    public class BookReleaseDate
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int DateId { get; set; }
        public string Year { get; set; }
        public int BookId { get; set; }
    }
}
