namespace BlazorApp.Services
{
    public class UserService
    {
        private readonly Dictionary<string, string> _userConnections = new Dictionary<string, string>();

        public void Add(string connectionId, string username)
        {
            _userConnections[username] = connectionId;
        }

        public void RemoveByName(string username)
        {
            if (_userConnections.ContainsKey(username))
            {
                _userConnections.Remove(username);
            }
        }

        public string GetConnectionIdByName(string username)
        {
            if(username is not null && _userConnections.Keys.Contains(username)){
                return _userConnections[username];
            }
            else
            {
                return string.Empty;
            }
        }

        public IEnumerable<(string ConnectionId, string Username)> GetAll()
        {
            foreach (var pair in _userConnections)
            {
                yield return (pair.Value, pair.Key);
            }
        }
    }
}