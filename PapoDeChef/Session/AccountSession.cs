namespace PapoDeChef.Session
{
    public static class AccountSession
    {
        public static uint ID { get; private set; }

        public static string Tag { get; private set; }

        public static void SetSesion(uint id, string tag)
        {
            ID = id;
            Tag = tag;
        }
    }
}
