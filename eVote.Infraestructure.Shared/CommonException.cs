

namespace eVote.Infrastructure.Shared
{
    public class CommonException
    {
        public static void NotFound(object entity, string Name)
        {
            if (entity == null)
            
                throw new EVoteExceptions($"The entity: {Name} was not found");
        }

        public static void IsNull(object entity, string Name)
        {
            if (entity == null)
                throw new EVoteExceptions($"The entity: {Name} is null");
        }

        public static List<T> Exists<T>(List<T> entity)
        {
            if (entity != null || entity!.Any())
            {
                return entity!;
            }
            else
            {
                Console.WriteLine("The entities already exists");
                return new List<T>();
            }
        }

        public static IQueryable<T> ExistsQuery<T>(IQueryable<T> entity)
        {
            if (entity != null || entity!.Any())
            {
                return entity!;
            }
            else
            {
                Console.WriteLine("The entities already exists");
                return new List<T>().AsQueryable();
            }
        }


    }
}
