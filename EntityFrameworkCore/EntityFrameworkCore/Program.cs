using EntityFrameworkCore.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace EntityFrameworkCore
{
    internal class Program
    {
        //Main要改成async Task才能使用await
        static async Task Main(string[] args)
        {
            using var context = new AppDbContext();

            //await GetAllTeams();
            //await GetTeamByIdAsync(2);

            await GetFilteredTeams();


            async Task GetAllTeams()
            {
                var teams = await context.Teams.ToListAsync();
                foreach (var team in teams)
                {
                    Console.WriteLine($"隊伍ID:{team.TeamId}，隊伍名稱:{team.Name}，創建日期:{team.CreatedDate}");
                }
            }

            async Task GetTeamByIdAsync(int id)
            {
                // FirstOrDefaultAsync，適合基於任意條件的查詢，例如非主鍵條件。(最通用)
                //var team = await context.Teams.FirstOrDefaultAsync(team => team.TeamId == id);

                //SingleOrDefaultAsync基於任意條件的查詢，要求最多只能有一筆匹配記錄。
                //var team = await context.Teams.SingleOrDefaultAsync(team => team.TeamId == id);

                //FindAsync 方法是專為主鍵查詢設計的，因此它會自動匹配傳入的參數到 模型中定義的主鍵欄位。
                var team = await context.Teams.FindAsync(id);
                if (team == null)
                {
                    Console.WriteLine("查無此隊伍");
                    return;
                }
                Console.WriteLine($"隊伍ID:{team.TeamId}，隊伍名稱:{team.Name}，創建日期:{team.CreatedDate}");
            }
        
            async Task GetFilteredTeams()
            {
                Console.WriteLine("請輸入隊伍名稱");
                string name = Console.ReadLine();
                var teamsFiltered = await context.Teams.Where(team => team.Name ==  name).ToListAsync();
                if(teamsFiltered.Count == 0)
                {
                    Console.WriteLine("找不到隊伍");
                    return;
                }
                foreach (var team in teamsFiltered)
                {
                    Console.WriteLine($"隊伍ID:{team.TeamId}，隊伍名稱:{team.Name}，創建日期:{team.CreatedDate}");
                }
            }
        }


    }
}
