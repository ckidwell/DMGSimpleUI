using Microsoft.Xna.Framework.Content;

namespace DMGSimpleUI.DMG.Samples;

public class SampleSpriteLoader
{
    public static Texture2D background;
    public static Texture2D exitText;
    public static Texture2D gameTitle;
    public static Texture2D menuBar;
    public static Texture2D playText;
    public static Texture2D settingsText;
    public static Texture2D menuText;
    public static Texture2D playGameButton;
    public static Texture2D quitGameButton;
    public static Texture2D settingsButton;
    public static Texture2D healthBar;
    
    public static void LoadSampleSprites(ContentManager Content)
    {
        background = Content.Load<Texture2D>("SampleSprites/tui_background"); 
        exitText = Content.Load<Texture2D>("SampleSprites/tui_exit");
        playText = Content.Load<Texture2D>("SampleSprites/tui_play");
        settingsText = Content.Load<Texture2D>("SampleSprites/tui_settings2");
        menuText = Content.Load<Texture2D>("SampleSprites/tui_menu");
        gameTitle = Content.Load<Texture2D>("SampleSprites/tui_gametitle");
        menuBar = Content.Load<Texture2D>("SampleSprites/tui_menubar");
        playGameButton = Content.Load<Texture2D>("SampleSprites/tui_playgame");
        quitGameButton = Content.Load<Texture2D>("SampleSprites/tui_quitgame");
        settingsButton = Content.Load<Texture2D>("SampleSprites/tui_settings");
        healthBar = Content.Load<Texture2D>("SampleSprites/healthbar");
    }
}