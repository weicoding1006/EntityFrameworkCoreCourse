using EntityFrameworkCore.Data;
using EntityFrameworkCore.Data.Dtos;
using EntityFrameworkCore.Domain;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata;

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
            //await GetFilteredTeams();
            //await GetCount();
            //await Aggregate();
            //await GroupByTeams();
            //await OrderBy();
            //await SkipAndTake();
            //await ProjectionsAndSelect();
            //await AsNoTracking();

            //await ListVsIQueryable();

            //await InsertOneRecored();
            //await InsertWithLoop();

            //await UpdateWithTracking();
            //await UpdateWithNoTracking();
            //await DeleteRecord();
            //await ExecuteUpdate();
            //await EagerLoading();
            //await InsertMoreMatches();
            //await DisplayTeamsWithScoresAsync();
            //await DisplayTeamDetailsAsync();
            //await QueryingKeylessEtityOrView();

            //await FromSqlRaw();
            //await FromSqlRawMixingWithLinq();

            async Task FromSqlRawMixingWithLinq()
            {
                var teams = await context.Teams.FromSqlInterpolated($"SELECT * FROM Teams")
                    .Where(q => q.Id == 2)
                    .Include("League")
                    .ToListAsync();
                foreach (var team in teams)
                {
                    Console.WriteLine($"{team.Name}");
                    // 如果 League 是單一導航屬性
                    Console.WriteLine($"{team.League.Name}");
                }

                //新增
                var leagudId = 1;
                var someNewTeamName = "用ExecuteSqlInterpolatedAsync更新的隊伍";
                var success = await context.Database.ExecuteSqlInterpolatedAsync($"UPDATE Teams SET Name = {someNewTeamName} WHERE Id = {leagudId}");
                Console.WriteLine(success);

                //刪除
                var teamToDeleteId = 2;
                //外鍵約束（Foreign Key Constraint）阻止了刪除操作。錯誤訊息顯示 Teams 表格的記錄正在被 Matches 表格參照（FK_Matches_Teams_AwayTeamId）
                // 刪除相關記錄
                await context.Database.ExecuteSqlInterpolatedAsync(
                    $"DELETE FROM Matches WHERE HomeTeamId = {teamToDeleteId} OR AwayTeamId = {teamToDeleteId}"
                );

                // 刪除球隊
                var result = await context.Database.ExecuteSqlInterpolatedAsync(
                    $"DELETE FROM Teams WHERE Id = {teamToDeleteId}"
                );
            }

            async Task FromSqlRaw()
            {
                Console.WriteLine("請輸入隊伍名稱");
                var teamName = Console.ReadLine();
                var teamNameParam = new MySqlParameter(){
                    ParameterName = "@teamName",
                    Value = $"%{teamName}%",
                    MySqlDbType = MySqlDbType.VarChar
                }; 
                var teams = await context.Teams.FromSqlRaw($"SELECT * FROM Teams WHERE name LIKE @teamName",teamNameParam).ToListAsync();
                foreach(var team in teams)
                {
                    Console.WriteLine($"{team.Name}");
                }

                //FromSqlInterpolated 優點
                //程式碼更簡潔
                //自動處理參數化
                //不需要手動建立參數
                Console.WriteLine("FromSqlInterpolated寫法---------------------");
                var searchTerm = $"%{teamName}%";
                //也可以使用FromSqlInterpolated
                teams = await context.Teams.FromSqlInterpolated($"SELECT * FROM Teams WHERE name LIKE {searchTerm}").ToListAsync();
                foreach (var team in teams)
                {
                    Console.WriteLine($"{team.Name}");
                }
            }


            async Task QueryingKeylessEtityOrView()
            {
                var details = await context.TeamsAndLeaguesView.ToListAsync();
                foreach (var detail in details)
                {
                    Console.WriteLine($"{detail.Name} - {detail.LeagueName}");
                }
            }


            //Select應用
            async Task DisplayTeamDetailsAsync()
            {
                var teams = await context.Teams
                    .Select(q => new TeamDetails
                    {
                        TeamId = q.Id,
                        TeamName = q.Name,
                        CoachName = q.Coach.Name,
                        TotalHomeGoals = q.HomeMatches.Sum(x => x.HomeTeamScore), // 計算並累加該球隊所有主場比賽的得分總和
                        TotalAwayGoals = q.AwayMatches.Sum(x => x.AwayTeamScore)  // 計算並累加該球隊所有客場比賽的得分總和
                    })
                    .ToListAsync();

                foreach (var team in teams)
                {
                    Console.WriteLine($"{team.TeamId} - {team.TeamName} - {team.CoachName}");
                    Console.WriteLine($"主場總得分:{team.TotalHomeGoals}，客場總得分:{team.TotalAwayGoals}");
                }
            }

            async Task DisplayTeamsWithScoresAsync()
            {
                var teams = await context.Teams
                    .Include(q => q.Coach)
                    .Include(q => q.HomeMatches.Where(q => q.HomeTeamScore > 0))
                    .ToListAsync();

                foreach (var team in teams)
                {
                    Console.WriteLine($"{team.Name} - {team.Coach.Name}");
                    foreach (var match in team.HomeMatches)
                    {
                        Console.WriteLine($"Score : {match.HomeTeamScore}");
                    }
                }
            }

            async Task InsertMoreMatches()
            {
                var match1 = new Match
                {
                    AwayTeamId = 2,
                    HomeTeamId = 3,
                    HomeTeamScore = 1,
                    AwayTeamScore = 0,
                    Date = new DateTime(2023,01,1),
                    TicketPrice = 20
                };
                var match2 = new Match
                {
                    AwayTeamId = 2,
                    HomeTeamId = 1,
                    HomeTeamScore = 2,
                    AwayTeamScore = 1,
                    Date = new DateTime(2023, 01, 1),
                    TicketPrice = 25
                };
                var match3 = new Match
                {
                    AwayTeamId = 4,
                    HomeTeamId = 1,
                    HomeTeamScore = 1,
                    AwayTeamScore = 3,
                    Date = new DateTime(2023, 01, 1),
                    TicketPrice = 30
                };
                var match4 = new Match
                {
                    AwayTeamId = 5,
                    HomeTeamId = 3,
                    HomeTeamScore = 2,
                    AwayTeamScore = 1,
                    Date = new DateTime(2023, 01, 1),
                    TicketPrice = 25
                };

                await context.AddRangeAsync(match1,match2,match3,match4);
                await context.SaveChangesAsync();
            }

            //Explicit Loading：讓開發者更靈活地控制何時載入關聯資料。
            //適用於只在需要時載入資料的情況，尤其是在資料量較大時，可以有效控制查詢的效能。
            async Task ExplicitLoading()
            {
                // 查詢指定ID的聯賽
                var league = await context.FindAsync<League>(1);

                // 檢查是否已經載入任何與該聯賽相關的隊伍
                if (!league.Teams.Any())
                {
                    Console.WriteLine("還沒有團隊被載入"); //會觸發
                }

                // 使用 Entry 和 LoadAsync 顯式地載入聯賽的 Teams
                await context.Entry(league)
                    .Collection(q => q.Teams)  // 指定載入 League 的 Teams 屬性
                    .LoadAsync();  // 顯式載入關聯的 Teams

                // 檢查是否成功載入任何隊伍
                if (league.Teams.Any())
                {
                    // 列印每個隊伍的名稱
                    foreach (var team in league.Teams)
                    {
                        Console.WriteLine($"{team.Name}");
                    }
                }
            }

            //Eager Loading 是一種在查詢時就一併載入相關聯資料的技術。
            //這樣做的好處是可以一次性獲取所需的資料，避免 N+1 查詢問題。
            async Task EagerLoading()
            {
                var leagues = await context.Leagues
                    .Include(q => q.Teams) // 如果想要資料包含Teams的，需要加入Incule，且在Model內要寫好導航屬性
                        .ThenInclude(q => q.Coach) //ThenInclude 是依賴於前面使用的 Include 方法所導航到的屬性
                    .ToListAsync();
                foreach (var league in leagues)
                {
                    Console.WriteLine($"ID:{league.Id} - Name:{league.Name}");
                    foreach (var team in league.Teams)
                    {
                        Console.WriteLine($"隊名:{team.Name} - 教練:{team.Coach.Name}");
                    }
                }
            }


            //插入相關聯的資料
            async Task InsertingRelatedData()
            {
                var league = new League
                {
                    Name = "新聯賽",
                    Teams = new List<Team>
                    {
                        new Team
                        {
                            Name = "新北隊伍",
                            Coach = new Coach
                            {
                                Name = "Jubey"
                            }
                        },
                        new Team
                        {
                            Name = "新竹隊伍",
                            Coach = new Coach
                            {
                                Name = "Demy"
                            }
                        }
                    }
                };
                await context.AddAsync(league);
                await context.SaveChangesAsync();
            }
            
            //更新多筆
            async Task ExecuteUpdate()
            {
                await context.Coaches.Where(q => q.Name == "Hani").ExecuteUpdateAsync(set => set
                .SetProperty(prop => prop.Name, "測試")
                .SetProperty(prop => prop.CreatedDate,DateTime.Now)
                );
            }
            
            //刪除多筆
            async Task ExecuteDelete()
            {
                await context.Coaches.Where(q => q.Name == "Test").ExecuteDeleteAsync();
            }
            async Task DeleteRecord()
            {
                var coach = await context.Coaches.FindAsync(12);
                context.Entry(coach).State = EntityState.Deleted;
                await context.SaveChangesAsync();
            }


            async Task UpdateWithTracking()
            {
                //使用 FindAsync 和追蹤實體進行更新
                //適合處理少量實體的更新操作，且變更檢測機制減少了開發者手動指定更新範圍的麻煩。
                //不需要顯式調用 Update，直接修改屬性後保存即可。
                var coach = await context.Coaches.FindAsync(1); // 從資料庫查找主鍵為1的 Coach
                coach.Name = "調整";                            // 修改實體的屬性
                coach.CreatedDate = DateTime.Now;              // 修改另一個屬性
                await context.SaveChangesAsync();              // 保存更改
            }

            async Task UpdateWithNoTracking()
            {
                //使用 AsNoTracking 和明確 Update 更新
                //當實體來自非追蹤模式（如 AsNoTracking），而開發者需要明確更新資料時，這種方式更合適。
                //非追蹤模式通常用於提升查詢性能，特別是在處理只讀數據時。
                //整體更新： 會將所有屬性都標記為更新，即使某些屬性沒有實際變化，這可能導致不必要的 SQL 操作
                //需要顯式調用 Update： 開發者需要更多的手動操作，增加了代碼量。
                var coach1 = await context.Coaches.AsNoTracking().FirstOrDefaultAsync(q => q.Id == 10);
                coach1.Name = "測試";

                //更新實體的所有屬性
                //可能連同導航屬性一起更新
                //可能影響性能（所有屬性都被包含在更新語句中）
                //context.Update(coach1);

                //更新實體的所有屬性，但可精確控制哪些屬性被修改
                //僅影響該實體本身，導航屬性需單獨設置
                //更高效，因為可以精細化控制需要更新的屬性
                context.Entry(coach1).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }

            async Task InsertOneRecored()
            {
                //單筆資料加入
                var newCoach = new Coach
                {
                    Name = "TestTest",
                    CreatedDate = DateTime.Now,
                };
                await context.Coaches.AddAsync(newCoach);
                await context.SaveChangesAsync();
            }

            async Task InsertWithLoop()
            {
                //單筆資料加入
                var newCoach = new Coach
                {
                    Name = "Test",
                    CreatedDate = DateTime.Now,
                };


                //多筆資料
                var newCoach1 = new Coach
                {
                    Name = "Hani",
                    CreatedDate = DateTime.Now,
                };
                List<Coach> coaches = new List<Coach>
            {
                newCoach,
                newCoach1
            };
                //foreach 方式
                //由於在迴圈中調用了多次 AddAsync，這可能會產生額外的上下文操作，但影響不大，因為資料庫寫入仍是一次完成。
                //foreach(var coach in coaches)
                //{
                //    await context.Coaches.AddAsync(coach);
                //}
                //await context.SaveChangesAsync();

                //Batch Insert
                //相較於逐筆加入的 foreach 方式，AddRangeAsync 性能更高，因為它是針對整個集合進行處理，減少了多次上下文操作的開銷。
                await context.Coaches.AddRangeAsync(coaches);
                await context.SaveChangesAsync();
            }

            async Task GetAllTeams()
            {
                var teams = await context.Teams.ToListAsync();
                foreach (var team in teams)
                {
                    Console.WriteLine($"隊伍ID:{team.Id}，隊伍名稱:{team.Name}，創建日期:{team.CreatedDate}");
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
                Console.WriteLine($"隊伍ID:{team.Id}，隊伍名稱:{team.Name}，創建日期:{team.CreatedDate}");
            }
        
            async Task GetFilteredTeams()
            {
                Console.WriteLine("請輸入隊伍名稱");
                string searchTerm = Console.ReadLine();
                var teamsFiltered = await context.Teams.Where(team => team.Name == searchTerm).ToListAsync();
                //if(teamsFiltered.Count == 0)
                //{
                //    Console.WriteLine("找不到隊伍");
                //    return;
                //}
                //foreach (var team in teamsFiltered)
                //{
                //    Console.WriteLine($"隊伍ID:{team.TeamId}，隊伍名稱:{team.Name}，創建日期:{team.CreatedDate}");
                //}

                //使用場景簡單（搜尋包含某段文字），使用 Contains，語法簡潔且足夠應用。
                var partialMatches = await context.Teams.Where(team => team.Name.Contains(searchTerm)).ToListAsync();
                //需要更靈活的匹配模式或跨平台一致性，使用 EF.Functions.Like，支持更複雜的查詢需求。
                //var partialMatches = await context.Teams.Where(team => EF.Functions.Like(team.Name,$"%{searchTerm}%")).ToListAsync();
                if (partialMatches.Count == 0)
                {
                    Console.WriteLine("找不到隊伍");
                    return;
                }
                foreach(var team in partialMatches)
                {
                    Console.WriteLine($"隊伍ID:{team.Id}，隊伍名稱:{team.Name}，創建日期:{team.CreatedDate}");
                }
            }
            
            async Task GetCount()
            {
                var numberOfTeams = await context.Teams.CountAsync();
                Console.WriteLine($"總共有{numberOfTeams}個隊伍");

                var numberOfTeamsWithCondition = await context.Teams.CountAsync(team => team.Id > 1);
                Console.WriteLine($"總共有{numberOfTeamsWithCondition}個隊伍");
            }

            //聚合
            async Task Aggregate()
            {
                //Max
                var maxTeam = await context.Teams.MaxAsync(team => team.Id);
                Console.WriteLine(maxTeam);
                //Min
                var minTeam = await context.Teams.MinAsync(team => team.Id);
                Console.WriteLine(minTeam);

                //Average
                var avgTeam = await context.Teams.AverageAsync(team => team.Id);
                Console.WriteLine(avgTeam);

                //Sum
                var sumTeams = await context.Teams.SumAsync(team => team.Id);
                Console.WriteLine(sumTeams);
            }

            //分組
            async Task GroupByTeams()
            {
                var groupedTeams = context.Teams.GroupBy(q => q.CreatedDate.Date);
                foreach (var group in groupedTeams)
                {
                    Console.WriteLine(group.Key);
                    foreach (var team in group)
                    {
                        Console.WriteLine($"分類:{group.Key}，隊伍名稱:{team.Name}");
                    }
                }
            }

            async Task OrderBy()
            {
                var orderedTeams = await context.Teams.OrderBy(q => q.Name).ToListAsync();
                foreach (var team in orderedTeams)
                {
                    Console.WriteLine($"隊伍ID:{team.Id}，隊伍名稱:{team.Name}，創建日期:{team.CreatedDate}");
                }

                var maxByDescendOrder = await context.Teams.OrderByDescending(q => q.Id).FirstOrDefaultAsync();
                if (maxByDescendOrder != null)
                {
                    Console.WriteLine($"隊伍ID:{maxByDescendOrder.Id}，隊伍名稱:{maxByDescendOrder.Name}，創建日期:{maxByDescendOrder.CreatedDate}");
                }

                var minByDescendingOrder = await context.Teams.OrderBy(q => q.Id).FirstOrDefaultAsync();
                if (minByDescendingOrder != null)
                {
                    Console.WriteLine($"隊伍ID:{minByDescendingOrder.Id}，隊伍名稱:{minByDescendingOrder.Name}，創建日期:{minByDescendingOrder.CreatedDate}");
                }
            }

            //分頁器
            async Task SkipAndTake()
            {
                var recordCount = 3;
                Console.Write("請輸入頁數 : ");
                string userInput = Console.ReadLine();
                if (int.TryParse(userInput, out int page))
                {
                    if (page > 0)
                    {
                        var teams = await context.Teams.Skip((page - 1) * recordCount).Take(recordCount).ToListAsync();
                        foreach (var team in teams)
                        {
                            Console.WriteLine($"隊伍ID:{team.Id}，隊伍名稱:{team.Name}，創建日期:{team.CreatedDate}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("輸入不得小於1");
                    }

                }
                else
                {
                    Console.WriteLine("請輸入整數");
                }
            }

            async Task ProjectionsAndSelect()
            {
                var teams = await context.Teams
                    .Select(q => new TeamInfo { Name = q.Name, CreatedDate = q.CreatedDate })
                    .ToListAsync();
                foreach (var team in teams)
                {
                    Console.WriteLine($"隊伍名稱:{team.Name}，創隊日期:{team.CreatedDate}");
                }
            }

            async Task AsNoTracking()
            {
                var teams = await context.Teams.AsNoTracking().ToListAsync();
                foreach (var team in teams)
                {
                    Console.WriteLine($"隊伍名稱:{team.Name}，創隊日期:{team.CreatedDate}，ID:{team.Id}");
                }
            }

            async Task ListVsIQueryable()
            {
                //在大多數情況下，建議使用 IQueryable 來進行資料庫查詢，
                //因為它能延遲執行，並且只處理需要的資料，效能更高。
                //List 的方式僅適用於小資料集或對即時性要求不高的場合。

                Console.WriteLine("輸入1，會搜尋ID是1的資料，輸入2會搜尋隊伍名稱是測試隊伍2");
                var option = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("List---------------------------");
                List<Team> teams = new List<Team>();
                teams = await context.Teams.ToListAsync();
                if (option == 1)
                {
                    teams = teams.Where(q => q.Id == 1).ToList();
                }
                else if (option == 2)
                {
                    teams = teams.Where(q => q.Name.Contains("測試隊伍2")).ToList();
                }

                foreach (var team in teams)
                {
                    Console.WriteLine(team.Name);
                }
                Console.WriteLine("IQueryable---------------------------");

                //IQueryable節省記憶體，只從資料庫載入需要的資料
                //延遲執行（Deferred Execution），只在調用 .ToList() 時才真正執行查詢。
                //適合處理大資料集
                var teamsAsQueryable = context.Teams.AsQueryable();
                if (option == 1)
                {
                    teamsAsQueryable = teamsAsQueryable.Where(q => q.Id == 1);
                }
                else if (option == 2)
                {
                    teamsAsQueryable = teamsAsQueryable.Where(q => q.Name.Contains("測試隊伍2"));
                }
                var result = teamsAsQueryable.ToList();

                foreach (var team in result)
                {
                    Console.WriteLine(team.Name);
                }
            }
        }


    }
}
