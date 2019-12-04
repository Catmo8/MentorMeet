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
            database.CreateTableAsync<Matches>().Wait();
            database.CreateTableAsync<Matching>().Wait();
            database.CreateTableAsync<Messages>().Wait();
        }

        #region User
        public Task<List<User>> UserGetItemsAsync()
        {
            return database.Table<User>().ToListAsync();
        }

        public Task<User> UserGetItemAsync(int id)
        {
            return database.Table<User>().Where(i => i.UserId == id).FirstOrDefaultAsync();
        }

        public Task<int> UserSaveItemAsync(User item)
        {
            if (item.UserId != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        //public Task<int> UserDeleteItemAsync(User item)
        //{
        //    return database.DeleteAsync(item);
        //}
        #endregion

        #region Matches
        public Task<List<Matches>> MatchesGetItemsAsync()
        {
            return database.Table<Matches>().ToListAsync();
        }

        public Task<Matches> MatchesGetItemAsync(int id)
        {
            return database.Table<Matches>().Where(i => i.MatchId == id).FirstOrDefaultAsync();
        }

        public Task<int> MatchesSaveItemAsync(Matches item)
        {
            if (item.MatchId != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Matches item)
        {
            return database.DeleteAsync(item);
        }

        #endregion

        #region Matching
        public Task<List<Matching>> MatchingGetItemsAsync()
        {
            return database.Table<Matching>().ToListAsync();
        }

        public Task<Matching> MatchingGetItemAsync(int id)
        {
            return database.Table<Matching>().Where(i => i.MatchingId == id).FirstOrDefaultAsync();
        }

        public Task<int> MatchingSaveItemAsync(Matching item)
        {
            if (item.MatchingId != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> MatchingDeleteItemAsync(Matching item)
        {
            return database.DeleteAsync(item);
        }

        #endregion

        #region Messages
        public Task<List<Messages>> MessagesGetItemsAsync()
        {
            return database.Table<Messages>().ToListAsync();
        }

        public Task<Messages> MessagesGetItemAsync(int id)
        {
            return database.Table<Messages>().Where(i => i.MessageId == id).FirstOrDefaultAsync();
        }

        public Task<int> MessagesSaveItemAsync(Messages item)
        {
            if (item.MessageId != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> MessagesDeleteItemAsync(Messages item)
        {
            return database.DeleteAsync(item);
        }

        #endregion
    }
}
