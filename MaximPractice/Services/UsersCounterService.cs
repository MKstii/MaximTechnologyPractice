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
            Count++;
        }
        public void RemoveUserCount()
        {
            Count--;
        }
    }
}
