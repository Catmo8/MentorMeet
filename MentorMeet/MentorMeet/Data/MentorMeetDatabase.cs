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
            database.CreateTableAsync<MessagesTable>().Wait();
        }

        public Task<List<User>> UserGetItemsAsync()
        {
            return database.Table<User>().ToListAsync();
        }
        public Task<List<MessagesTable>> MessagesTableGetItemsAsync()
        {
            return database.Table<MessagesTable>().ToListAsync();
        }

        public Task<User> UserGetItemAsync(int id)
        {
            return database.Table<User>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        public Task<MessagesTable> MessagesTableGetItemAsync(int id)
        {
            return database.Table<MessagesTable>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> UserSaveItemAsync(User item)
        {
            if (item.Id != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> MessagesTableSaveItemAsync(MessagesTable item)
        {
            if (item.Id != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        //public Task<int> DeleteItemAsync(User item)
        //{
        //    return database.DeleteAsync(item);
        //}
    }
}
