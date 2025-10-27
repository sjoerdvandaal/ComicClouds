
using game;
using System.Data;

internal class SQLiteDataAdapter : IDisposable
{
    private string query;
    private SQLiteConnection connection;
    private System.Data.SQLite.SQLiteConnection connection1;

    public SQLiteDataAdapter(string query, SQLiteConnection connection)
    {
        this.query = query;
        this.connection = connection;
    }

    public SQLiteDataAdapter(string query, System.Data.SQLite.SQLiteConnection connection1)
    {
        this.query = query;
        this.connection1 = connection1;
    }

    internal void Fill(DataTable table)
    {
        throw new NotImplementedException();
    }
}