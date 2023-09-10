using Microsoft.EntityFrameworkCore;

namespace StockWallet.Db.Mysql.Utils;

public static class CustomDbMethods
{
    public static bool IsTracked<TContext, TEntity>(this TContext context, TEntity item)
    where TContext: DbContext
    where TEntity: class
    {
        return context.Set<TEntity>().Local.Any(e => e.Equals(item));
    }
}