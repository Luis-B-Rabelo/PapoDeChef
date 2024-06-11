namespace PapoDeChef.Database
{
    public static class DBConn
    {
        private static PapoDeChefDB _db = new();

        public static PapoDeChefDB DB
        {
            get => _db;
        }

    }
}
