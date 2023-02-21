using Core;


namespace Data
{
    public class Loader
    {
        public GameStateModel LoadGameData()
        {
            return DataSystem.LoadFileJson<GameStateModel>("GameModel");
        }

        public SettingsModel LoadSettingsData()
        {
            return DataSystem.LoadFileJson<SettingsModel>("SettingsModel");
        }
    }
}