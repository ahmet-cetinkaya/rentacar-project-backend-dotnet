using System.Linq.Dynamic.Core;

namespace Core.Persistence.Dynamic;

public static class IQueryableDynamicFilterExtensions
{
    private static readonly IDictionary<string, string>
        Operators = new Dictionary<string, string>
        {
            { "eq", "=" },
            { "neq", "!=" },
            { "lt", "<" },
            { "lte", "<=" },
            { "gt", ">" },
            { "gte", ">=" },
            { "isnull", "== null" },
            { "isnotnull", "!= null" },
            { "startswith", "StartsWith" },
            { "endswith", "EndsWith" },
            { "contains", "Contains" },
            { "doesnotcontain", "Contains" }
        };

    public static IQueryable<T> ToDynamic<T>(
        this IQueryable<T> query, Dynamic dynamic)
    {
        if (dynamic.Filter is not null) query = Filter(query, dynamic.Filter);
        if (dynamic.Sort is not null && dynamic.Sort.Any()) query = Sort(query, dynamic.Sort);
        return query;
    }

    private static IQueryable<T> Filter<T>(
        IQueryable<T> queryable, Filter filter)
    {
        IList<Filter> filters = GetAllFilters(filter);
        string?[] values = filters.Select(f => f.Value).ToArray();
        string where = Transform(filter, filters);
        queryable = queryable.Where(where, values);

        return queryable;
    }

    private static IQueryable<T> Sort<T>(
        IQueryable<T> queryable, IEnumerable<Sort> sort)
    {
        if (sort.Any())
        {
            string ordering = string.Join(",", sort.Select(s => $"{s.Field} {s.Dir}"));
            return queryable.OrderBy(ordering);
        }

        return queryable;
    }

    public static IList<Filter> GetAllFilters(Filter filter)
    {
        List<Filter> filters = new();
        GetFilters(filter, filters);
        return filters;
    }

    private static void GetFilters(Filter filter, IList<Filter> filters)
    {
        if (filter.Filters is not null && filter.Filters.Any())
            foreach (Filter item in filter.Filters)
                GetFilters(item, filters);
        else
            filters.Add(filter);
    }

    public static string Transform(Filter filter, IList<Filter> filters)
    {
        if (filter.Filters is not null && filter.Filters.Any())
            return $"({string.Join($" {filter.Logic} ", filter.Filters.Select(f => Transform(f, filters)).ToArray())})";

        int index = filters.IndexOf(filter);
        string comparison = Operators[filter.Operator];

        if (filter.Operator == "doesnotcontain")
            return $"(!np({filter.Field}).{comparison}(@{index}))";

        if (comparison == "StartsWith" ||
            comparison == "EndsWith" ||
            comparison == "Contains")
            return $"(np({filter.Field}).{comparison}(@{index}))";

        if (comparison == "== null" || comparison == "!= null")
            return $"np({filter.Field}) {comparison}";

        return $"np({filter.Field}) {comparison} @{index}";
    }
}