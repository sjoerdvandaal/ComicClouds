using System;

namespace game
{
    internal class SQLiteConnection : IDisposable
    {
        private string connectionString;

        public SQLiteConnection(string connectionString)
        {
            this.connectionString = connectionString;
        }

        internal void Open()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {

        }
    }
}
