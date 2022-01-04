using GreenDonut;
using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace hcresolver
{
    public class Query
    {
        public IEnumerable<Neo> GetNeo(ColorType? color) =>
            Enumerable
                .Range(1, 3)
                .Select(x => new Neo(x, color))
                .Where(x => color is not null ? x.colorFilter == color : true);
    }

    public class Neo
    {
        public int x { get; set; }
        public ColorType? colorFilter { get; set; }

        public Neo(int x, ColorType? colorFilter)
        {
            this.x = x;
            this.colorFilter = colorFilter;
        }

        public async Task<Pill> GetPills([Service] Matrix matrix, IResolverContext context) =>
            await matrix.RedOrBlue(context, colorFilter).LoadAsync(this.x);
    }

    public class Matrix
    {
        private readonly IEnumerable<Pill> pills = Enumerable
            .Range(1, 25)
            .Select(x => new Pill(x, (x % 2 == 0 ? ColorType.blue : ColorType.red)));

        public IDataLoader<int, Pill> RedOrBlue(IResolverContext context, ColorType? color)
        {
            return context.BatchDataLoader<int, Pill>(
                (keys, token) =>
                {
                    var result = pills.Where(
                        x => keys.Contains(x.Id) && (color is not null ? x.Color == color : true)
                    );
                    return Task.FromResult(result.ToDictionary(x => x.Id).AsReadOnly());
                },
                "MyDataLoader_" + color?.ToString()
            );
        }
    }

    public class Pill
    {
        public Pill(int id, ColorType color)
        {
            Id = id;
            Color = color;
        }

        public int Id { get; }
        public ColorType Color { get; set; }
    }

    public enum ColorType
    {
        red,
        blue
    }

    public static class DataLoaderExtension
    {
        public static IReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(
            this IDictionary<TKey, TValue> dict
        ) where TKey : notnull => (IReadOnlyDictionary<TKey, TValue>)dict;
    }
}
