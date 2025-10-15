
namespace game
{
    internal class SQLiteCommand : IDisposable
    {
        private string query;
        private SQLiteConnection connection;

        public SQLiteCommand(string query, SQLiteConnection connection)
        {
            this.query = query;
            this.connection = connection;
        }

        internal IDisposable ExecuteReader()
        {
            throw new NotImplementedException();
        }
    }
}