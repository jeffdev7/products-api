namespace products.infra.Data
{
    public class Seeder
    {
        public static void RunSeeder()
        {
            using (var context = new ApplicationContext())
            {
                var products = context.Products.ToList();

            }
        }
    }
}
