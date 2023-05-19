namespace WorkflowAutomation.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(DocumentsDbContext context)
        {
          //  context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
