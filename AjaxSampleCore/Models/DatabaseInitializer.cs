namespace AjaxSampleCore.Models
{
    class DatabaseInitializer
    {
        public static async Task Run(BookContext context)
        {
            var authors = new List<Author> {
                new Author { Name = "山田 祥寛" },
                new Author { Name = "掌田 津耶乃" },
                new Author { Name = "Dino Esposito" },
                new Author { Name = "井上 章" },
                new Author { Name = "ホセ・M・アギラール" }
            };

            var publishers = new List<Publisher> {
                new Publisher { Code = "7981", Name = "翔泳社" },
                new Publisher { Code = "7980", Name = "秀和システム" },
                new Publisher { Code = "8222", Name = "日経BP" }
            };


            var books = new List<Book> {
                new Book { Code = "4798163651", Title = "独習ASP.NET Webフォーム 第6版", Price = 4180, PublisherCode = "7981", Publisher = publishers[0], Authors = new List<Author> { authors[0] } },
                new Book { Code = "4798041793", Title = "ASP.NET MVC5実践プログラミング ", Price = 3850, PublisherCode = "7980", Publisher = publishers[1], Authors = new List<Author> { authors[0] } },
                new Book { Code = "4822253805", Title = "プログラミングASP.NET Core ", Price = 5720, PublisherCode = "8222", Publisher = publishers[2], Authors = new List<Author> { authors[2], authors[3] } },
                new Book { Code = "4822298418", Title = "プログラミング ASP.NET SignalR", Price = 3520, PublisherCode = "8222", Publisher = publishers[2], Authors = new List<Author> { authors[3], authors[4] } },
                new Book { Code = "479806050X", Title = "C#フレームワーク ASP.NET Core3入門", Price = 3520, PublisherCode = "7980", Publisher = publishers[1], Authors = new List<Author> { authors[1] } }
            };

            authors   .ForEach(author    => context.Authors   .Add(author   ));
            publishers.ForEach(publisher => context.Publishers.Add(publisher));
            books     .ForEach(book      => context.Books     .Add(book     ));
            
            await context.SaveChangesAsync();
        }
    }
}
