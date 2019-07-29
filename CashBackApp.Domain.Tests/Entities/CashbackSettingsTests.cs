using CashBackApp.Domain.Entities;
using CashBackApp.Domain.Enums;
using System;
using Xunit;

namespace CashBackApp.Domain.Tests.Entities
{
    public class CashbackSettingsTests
    {
        Guid id = Guid.NewGuid();
        Guid companyId = Guid.NewGuid();
        GenreEnum genre = GenreEnum.CLASSIC;
        DayOfWeek dayOfWeek = DayOfWeek.Monday;
        double percentage = 10;

        [Fact]
        public void New_CashbackSettings_IsValid()
        {
            var cashbackSetting = new CashbackSettings
            {
                CompanyId = companyId,
                Genre = genre,
                DayOfWeek = dayOfWeek,
                Percentage = percentage
            };

            Assert.NotNull(cashbackSetting);
            Assert.Equal(companyId, cashbackSetting.CompanyId);
            Assert.Equal(genre, cashbackSetting.Genre);
            Assert.Equal(dayOfWeek, cashbackSetting.DayOfWeek);
            Assert.True(cashbackSetting.Active);
            Assert.True(cashbackSetting.IsValid());
        }

        [Theory]
        [InlineData("26802574-1681-416e-9044-1a30d2a3d7ed", -1)]
        [InlineData("", 0)]
        [InlineData("26802574-1681-416e-9044-1a30d2a45454", 100)]
        [InlineData("", 101)]
        public void Invalid_CashbackSettings_Must_Fail(string companyId, double value)
        {
            Assert.Throws<ArgumentException>(() => new CashbackSettings { CompanyId = Guid.Parse(companyId), Percentage = value }.IsValid());
        }
    }
}
