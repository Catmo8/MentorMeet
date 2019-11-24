using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using MentorMeet.Models;


namespace MentorMeet.Data
{
    public class MentorMeetDatabase
    {
        readonly SQLiteAsyncConnection database;
        public MentorMeetDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<User>().Wait();
        }
        public Task<List<User>> GetItemssAsync()
        {
            return database.Table<User>().ToListAsync();
        }

        public Task<User> GetItemAsync(int id)
        {
            return database.Table<User>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(User note)
        {
            if (note.Id != 0)
            {
                return database.UpdateAsync(note);
            }
            else
            {
                return database.InsertAsync(note);
            }
        }

        public Task<int> DeleteItemAsync(User note)
        {
            return database.DeleteAsync(note);
        }
    }
}
