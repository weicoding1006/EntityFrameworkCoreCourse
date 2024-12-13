using EntityFrameworkCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkCore.Data
{
    internal class LeagueConfiguration : IEntityTypeConfiguration<League>
    {
        public void Configure(EntityTypeBuilder<League> builder)
        {
            builder.HasData(
                new League
                {
                    Id = 1,
                    Name = "測試聯盟1"
                },
                new League
                {
                    Id = 2,
                    Name = "測試聯盟2"
                },
                new League
                {
                    Id = 3,
                    Name = "測試聯盟3"
                }
            );
        }
    }
}
