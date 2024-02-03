using System;
using System.Collections.Generic;
using DMGSimpleUI.DMG.Elements;
using DMGSimpleUI.DMG.Management;
using DMGSimpleUI.DMG.Models;

namespace DMGSimpleUI.DMG.Samples.ThemeExamples;

public class MainMenuSample : DMGScene
{
    public static Action QuitGame;
    public static Action<DMGTransition> ScreenTransition;
    private SceneTypes _sceneTypes = SceneTypes.MAIN_MENU_THEMED;
    
    private Texture2D backgroundTexture;
    private readonly List<BaseUIElement> _elements = new();

    private DMGPanel background;
    private DMGPanel foreground;
    private DMGUITheme _theme;
    
    public MainMenuSample(DMGUITheme theme)
    {
        _theme = theme;
        
        backgroundTexture = DMGUIGlobals.Content.Load<Texture2D>("panelbg64x");
        
        var t = DMGUIGlobals.Content.Load<Texture2D>("whitebutton128x32"); 
        
        background = new DMGPanel(backgroundTexture, new(0, 0),
            DMGUIGlobals.UIFont,_theme,
            new Point(DMGUIGlobals.Bounds.X, DMGUIGlobals.Bounds.Y), string.Empty);
        foreground = new DMGPanel(backgroundTexture, new(0, 0),
            DMGUIGlobals.UIFont,_theme,
            new Point(DMGUIGlobals.Bounds.X, DMGUIGlobals.Bounds.Y), string.Empty, Color.Transparent);
        
        // ReSharper disable once PossibleLossOfFraction
        var H_CENTER = (float)(DMGUIGlobals.Bounds.Y / 2) ;
        var V_CENTER = (float)DMGUIGlobals.Bounds.X / 2;
        
        background.AddChild(new DMGPanel(backgroundTexture, new((int)(V_CENTER - 64), (int)H_CENTER - 80),
            DMGUIGlobals.UIFont,_theme,
            new Point(55, 55), "GAME TITLE"));
        background.AddChild( new DMGButton(t, new Vector2(V_CENTER -64, H_CENTER - 35),
            _theme,
            DMGUIGlobals.UIFont, "PLAY GAME")).OnClick += OnPlayGame;
        background.AddChild(new DMGButton(t, new Vector2(V_CENTER -64, H_CENTER),
            _theme,
            DMGUIGlobals.UIFont, "SETTINGS")).OnClick += OnSettings;
        background.AddChild(new DMGButton(t, new Vector2(V_CENTER -64, H_CENTER  +35),
            _theme,
            DMGUIGlobals.UIFont, "QUIT GAME")).OnClick += OnQuit;
        
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
            nextScene = SceneTypes.MENU_BAR_THEMED,
            _uiElement = foreground,
        };

        ScreenTransition?.Invoke(transition);
    }

    private void OnSettings(object sender, EventArgs e)
    {
        // NOT IMPLEMENTED
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