using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace DMGSimpleUI.DMG.Samples;

public class SampleSpriteLoader
{
    public static Texture2D background;
    public static Texture2D exitText;
    public static Texture2D gameTitle;
    public static Texture2D menuBar;
    public static Texture2D playText;
    public static Texture2D playGameButton;
    public static Texture2D quitGameButton;
    public static Texture2D settingsButton;
    
    public static void LoadSampleSprites(ContentManager Content)
    {
        background = Content.Load<Texture2D>("SampleSprites/tui_background"); 
        exitText = Content.Load<Texture2D>("SampleSprites/tui_exit");
        playText = Content.Load<Texture2D>("SampleSprites/tui_gametitle");
        gameTitle = Content.Load<Texture2D>("SampleSprites/tui_menubar");
        menuBar = Content.Load<Texture2D>("SampleSprites/tui_play");
        playGameButton = Content.Load<Texture2D>("SampleSprites/tui_playgame");
        playGameButton = Content.Load<Texture2D>("SampleSprites/tui_quitgame");
        playGameButton = Content.Load<Texture2D>("SampleSprites/tui_settings");
       
    }
}