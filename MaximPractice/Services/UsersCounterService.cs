using MaximPractice.src.Settings;

namespace MaximPractice.Services
{
    public class UsersCounterService
    {
        public int Count { get; private set; }

        public UsersCounterService()
        {
            Count = 0;
        }

        public void AddUserCount()
        {
            lock (this)
            {
                Count++;
            }
        }
        public void RemoveUserCount()
        {
            lock (this)
            {
                Count--;
            }
            
        }
    }
}
