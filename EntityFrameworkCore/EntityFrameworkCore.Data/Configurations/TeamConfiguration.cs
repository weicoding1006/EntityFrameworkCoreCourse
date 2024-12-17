using EntityFrameworkCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Data.Configurations
{
    class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasIndex(q => q.Name).IsUnique(); //為 Name 屬性建立一個唯一索引。
            builder.HasMany(m => m.HomeMatches) //定義當前實體（假設是 Team）與另一個實體（假設是 Match）之間的一對多關係。
                .WithOne(q => q.HomeTeam) //定義反向關係，表示 Match 實體與 Team 實體之間的關聯。表示比賽 (Match) 有一個屬性 HomeTeam 指向該比賽的主隊
                .HasForeignKey(q => q.HomeTeamId) //指定外鍵 HomeTeamId，用來在 Match 資料表中連接 Team 的主鍵。
                .IsRequired() //設定 HomeTeamId 外鍵是必填欄位（不可為 NULL）。
                .OnDelete(DeleteBehavior.Restrict); //DeleteBehavior.Restrict 表示當主隊 Team 的資料被刪除時，禁止刪除與之相關的比賽記錄 Match。

            builder.HasMany(m => m.AwayMatches)
                .WithOne(q => q.AwayTeam)
                .HasForeignKey(q => q.AwayTeamId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasData(
                new Team
                {
                    Id = 1,
                    Name = "測試隊伍1",
                    //DateTimeOffset.UtcNow.DateTime 等於取得目前 UTC 時間，並轉換為不含時區資訊的 DateTime 格式。
                    CreatedDate = DateTimeOffset.UtcNow.DateTime,
                    LeagueId = 1,
                    CoachId = 1,
                },
                 new Team
                 {
                     Id = 2,
                     Name = "測試隊伍2",
                     CreatedDate = DateTimeOffset.UtcNow.DateTime,
                     LeagueId = 1,
                     CoachId = 2
                 },
                new Team
                {
                    Id = 3,
                    Name = "測試隊伍3",
                    CreatedDate = DateTimeOffset.UtcNow.DateTime,
                    LeagueId = 1,
                    CoachId = 3,
                
                }
            );
        }
    }
}
