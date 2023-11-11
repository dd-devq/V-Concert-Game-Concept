namespace Game.System
{
    public class UIManager : ManualSingletonMono<UIManager>
    {
    }

    public enum UIIndex
    {
        Authentication,
        MainMenu,
        Shop,
        SongSelection,
        Setting,
        Inventory,
        HUD,
        Item,
        Lobby,
        Pause,
        Victory,
    }
}