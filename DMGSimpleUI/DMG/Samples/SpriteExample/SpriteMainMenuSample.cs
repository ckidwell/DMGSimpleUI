using System;
using System.Collections.Generic;
using DMGSimpleUI.DMG.Elements;
using DMGSimpleUI.DMG.Management;
using DMGSimpleUI.DMG.Models;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Tiled;

namespace DMGSimpleUI.DMG.Samples.SpriteExample;

public class SpriteMainMenuSample: DMGScene
{
    public static Action QuitGame;
    public static Action<DMGTransition> ScreenTransition;
    private SceneTypes _sceneTypes = SceneTypes.MAIN_MENU_THEMED;
    
    private Texture2D backgroundTexture;
    private readonly List<BaseUIElement> _elements = new();

    private DMGPanel background;
    private DMGPanel foreground;
    private DMGUITheme _theme;
    private DMGPanel gameNamePanel;

    public SpriteMainMenuSample(DMGUITheme theme)
    {
        _theme = theme;
        backgroundTexture = DMGUIGlobals.Content.Load<Texture2D>("panelbg64x");

        var t = DMGUIGlobals.Content.Load<Texture2D>("whitebutton128x32"); 
        
        background = new DMGPanel(SampleSpriteLoader.background, new(0, 0),
            DMGUIGlobals.UIFont,_theme,
            new Point(DMGUIGlobals.Bounds.X, DMGUIGlobals.Bounds.Y), string.Empty);
        foreground = new DMGPanel(backgroundTexture, new(0, 0),
            DMGUIGlobals.UIFont,_theme,
            new Point(DMGUIGlobals.Bounds.X, DMGUIGlobals.Bounds.Y), string.Empty, Color.Transparent);
        
        gameNamePanel =  new DMGPanel(SampleSpriteLoader.gameTitle, new(125, 50),
            DMGUIGlobals.UIFont,_theme,
            new Point(SampleSpriteLoader.gameTitle.Width,SampleSpriteLoader.gameTitle.Height), string.Empty);
            
        // ReSharper disable once PossibleLossOfFraction
        var H_CENTER = (float)(DMGUIGlobals.Bounds.Y / 2) ;
        var V_CENTER = (float)DMGUIGlobals.Bounds.X / 2;

        var spriteWidthHalf = SampleSpriteLoader.playGameButton.Width /2;
        background.AddChild( new DMGButton(SampleSpriteLoader.playGameButton, new Vector2(V_CENTER -spriteWidthHalf, H_CENTER -25),
            _theme,
            DMGUIGlobals.UIFont, string.Empty)).OnClick += OnPlayGame;
        background.AddChild(new DMGButton(SampleSpriteLoader.settingsButton, new Vector2(V_CENTER -spriteWidthHalf, H_CENTER +50),
            _theme,
            DMGUIGlobals.UIFont, string.Empty)).OnClick += OnSettings;
        background.AddChild(new DMGButton(SampleSpriteLoader.quitGameButton, new Vector2(V_CENTER -spriteWidthHalf, H_CENTER +125),
            _theme,
            DMGUIGlobals.UIFont, string.Empty)).OnClick += OnQuit;
        background.AddChild(gameNamePanel);
        background.AddChild(foreground);
        
        _elements.Add(background);
    }

    private void OnPlayGame(object sender, EventArgs e)
    {
        var transition = new DMGTransition()
        {
            TransitionType = DMGTransitionType.WIPE_RIGHT,
            theme = _theme,
            duration = 2f,
            nextScene = SceneTypes.MENU_BAR_SPRITE,
            _uiElement = foreground,
        };

        ScreenTransition?.Invoke(transition);
    }

    private void OnSettings(object sender, EventArgs e)
    {
        
    }

    private void OnQuit(object sender, EventArgs e)
    {
        QuitGame?.Invoke();
    }

    public override void Update()
    {
        foreach (var item in _elements)
        {
            item.Update();
        }
    }

    public override void Draw()
    {
        foreach (var item in _elements)
        {
            if(item.Visible) item.Draw();
        }
    }

    public override void ReInit()
    {
        foreground._color = Color.Transparent;
    }

    public override List<BaseUIElement> GetElements()
    {
        return _elements;
    }
}