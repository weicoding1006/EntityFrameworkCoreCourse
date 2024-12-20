using EntityFrameworkCore.Data.Configurations;
using EntityFrameworkCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Data
{
    //資料庫遷移(code first)
    //Add-Migration "Demo"
    //update-database
    public class AppDbContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<TeamsAndLeaguesView> TeamsAndLeaguesView { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "Server=localhost;Database=EntityFrameworkCoreCourse;User=root;Password=password;",
                ServerVersion.AutoDetect("Server=localhost;Database=EntityFrameworkCoreCourse;User=root;Password=password;")
            )

            //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            //LogTo 的用途
            //LogTo 是 EF Core 用來記錄操作細節的功能
            //查看 EF Core 如何將 LINQ 查詢轉換為 SQL 查詢
            .LogTo(Console.WriteLine, LogLevel.Information)// 日誌輸出到主控台
            .EnableSensitiveDataLogging() // 記錄敏感資料
            .EnableDetailedErrors();// 顯示詳細錯誤訊息

        }

        //增加初始資料
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new TeamConfiguration());
            //modelBuilder.ApplyConfiguration(new LeagueConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<TeamsAndLeaguesView>()  // 配置 TeamsAndLeaguesView 實體
                .HasNoKey()                             // 告訴 EF Core 這個實體沒有主鍵
                .ToView("vw_TeamsAndLeagues");          // 指定這是一個資料庫視圖，而不是實體表格
        }
    }
}
