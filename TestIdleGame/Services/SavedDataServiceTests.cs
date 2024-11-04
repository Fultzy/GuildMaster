
using IdleGame.Services;
using IdleGame.Models;
using IdleGame.Jobs;

namespace TestIdleGame.Services;
public class SavedDataServiceTests
{
    [Fact]
    public void SaveData_SavesData()
    {
        // Arrange
        var player = new Player
        {
            Name = "TestPlayer",
            Stats = new Stats
            {
                Level = 1,
                XP = 0,
                XPToNextLevel = 100,
                Health = 100,
                MaxHealth = 100,
                Attack = 1,
                Defence = 1
            },
            Skills = new Skills
            {
                WoodCuttingLevel = 1,
                CombatLevel = 1
            }
        };

        // Act
        SavedDataService.SaveData();

        // Assert
        var loadedPlayer = SavedDataService.LoadData();
        Assert.NotNull(loadedPlayer);
        Assert.Equal(player.Name, loadedPlayer.Name);
        Assert.Equal(player.Stats.Level, loadedPlayer.Stats.Level);
        Assert.Equal(player.Stats.XP, loadedPlayer.Stats.XP);
        Assert.Equal(player.Stats.XPToNextLevel, loadedPlayer.Stats.XPToNextLevel);
        Assert.Equal(player.Stats.Health, loadedPlayer.Stats.Health);
        Assert.Equal(player.Stats.MaxHealth, loadedPlayer.Stats.MaxHealth);
        Assert.Equal(player.Stats.Attack, loadedPlayer.Stats.Attack);
        Assert.Equal(player.Stats.Defence, loadedPlayer.Stats.Defence);
        Assert.Equal(player.Skills.WoodCuttingLevel, loadedPlayer.Skills.WoodCuttingLevel);
        Assert.Equal(player.Skills.CombatLevel, loadedPlayer.Skills.CombatLevel);
    }

    [Fact]
    public void LoadSavedData_ReturnsValidData()
    {
        // Arrange/Act
        var loadedPlayer = SavedDataService.LoadData();

        // Assert
        Assert.NotNull(loadedPlayer);
        Assert.NotEmpty(loadedPlayer.Name);
        Assert.True(loadedPlayer.Stats.Level >= 1);
        Assert.True(loadedPlayer.Stats.XP >= 0);
        Assert.True(loadedPlayer.Stats.XPToNextLevel >= 0);

        Assert.True(loadedPlayer.Stats.Health >= 0);
        Assert.True(loadedPlayer.Stats.MaxHealth >= 0);
        Assert.True(loadedPlayer.Stats.Attack >= 0);
        Assert.True(loadedPlayer.Stats.Defence >= 0);

        Assert.True(loadedPlayer.Skills.WoodCuttingLevel >= 1);
        Assert.True(loadedPlayer.Skills.CombatLevel >= 1);

    }
}
