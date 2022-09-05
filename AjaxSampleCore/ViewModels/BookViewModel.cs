using System.Text;

namespace AjaxSampleCore.ViewModels
{
    using Models;

    public class BookViewModel
    {
        public string Code { get; set; } = "";
        public string Title { get; set; } = "";
        public string Price { get; set; } = "";
        public string ReleaseDate { get; set; } = "";
        public string Publisher { get; set; } = "";
        public string Authors { get; set; } = "";
        public string Url => $"https://www.amazon.co.jp/dp/{Code}";
        public string ImageUrl => $"https://images-na.ssl-images-amazon.com/images/P/{Code}.09.MZZZZZZZ";
    }

    static class BookViewModelExtensions
    {
        public static BookViewModel ToViewModel(this Book @this)
            => new BookViewModel {
                Code        = @this.Code,
                Title       = @this.Title,
                Price       = @this.Price is null ? "未定" : $"{@this.Price:#,0}",
                ReleaseDate = @this.ReleaseDate is null ? "未定" : @this.ReleaseDate.Value.ToShortDateString(),
                Publisher   = @this.Publisher.Name,
                Authors     = @this.Authors.Select(author => author.Name).Connect()
            };

        public static string Connect(this IEnumerable<string> @this, string separator = ", ")
        {
            var stringBuilder = new StringBuilder();
            @this.ForEach((index, text) => {
                if (index != 0)
                    stringBuilder.Append(separator);
                stringBuilder.Append(text);
            });
            return stringBuilder.ToString();
        }

        public static void ForEach<TElement>(this IEnumerable<TElement> @this, Action<int, TElement> action)
        {
            var index = 0;
            foreach (var element in @this)
                action(index++, element);
        }
    }
}