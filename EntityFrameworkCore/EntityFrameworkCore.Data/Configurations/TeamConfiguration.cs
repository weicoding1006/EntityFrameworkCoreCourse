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
            builder.HasData(
                new Team
                {
                    Id = 1,
                    Name = "測試隊伍1",
                    //DateTimeOffset.UtcNow.DateTime 等於取得目前 UTC 時間，並轉換為不含時區資訊的 DateTime 格式。
                    CreatedDate = DateTimeOffset.UtcNow.DateTime,
                },
                 new Team
                 {
                     Id = 2,
                     Name = "測試隊伍2",
                     CreatedDate = DateTimeOffset.UtcNow.DateTime,
                 },
                new Team
                {
                    Id = 3,
                    Name = "測試隊伍3",
                    CreatedDate = DateTimeOffset.UtcNow.DateTime,
                }
            );
        }
    }
}
