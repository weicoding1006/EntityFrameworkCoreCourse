using EntityFrameworkCore.Data;

namespace EntityFrameworkCore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new AppDbContext();
            var teams = context.Teams.ToList();
            foreach (var item in teams)
            {
                Console.WriteLine($"隊伍ID:{item.TeamId}，隊伍名稱:{item.Name}，創建日期:{item.CreatedDate}");
            }
        }
    }
}
