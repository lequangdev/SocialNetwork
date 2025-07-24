using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ConnectUser
{
    public static class ConnectUserService
    {
        private static readonly ConcurrentDictionary<string, HashSet<string>> _onlineUsers = new();
        private static readonly object _lock = new();
        public static void AddConnection(string user_id, string connectionId)
        {
            lock (_lock)
            {
                if (!_onlineUsers.ContainsKey(user_id))
                    _onlineUsers[user_id] = new HashSet<string>();

                _onlineUsers[user_id].Add(connectionId);
            }
        }

        public static void RemoveConnection(string user_id, string connectionId)
        {
            lock (_lock)
            {
                if (_onlineUsers.ContainsKey(user_id))
                {
                    _onlineUsers[user_id].Remove(connectionId);
                    if (_onlineUsers[user_id].Count == 0)
                        _onlineUsers.Remove(user_id, out _);
                }
            }
        }

        public static IEnumerable<string> GetConnections(string user_id)
        {
            lock (_lock)
            {
                return _onlineUsers.ContainsKey(user_id)
                    ? _onlineUsers[user_id].ToList()
                    : Enumerable.Empty<string>();
            }
        }

        public static int GetTotalConnections()
        {
            lock (_lock)
            {
                return _onlineUsers.Values.Sum(set => set.Count);
            }
        }
        public static int GetTotalOnlineUsers()
        {
            lock (_lock)
            {
                return _onlineUsers.Count;
            }
        }

    }

}
