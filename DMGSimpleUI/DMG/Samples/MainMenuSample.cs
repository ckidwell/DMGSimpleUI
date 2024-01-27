using System;
using System.Collections.Generic;
using DMGSimpleUI.DMG.Elements;
using DMGSimpleUI.DMG.Management;
using DMGSimpleUI.DMG.Models;

namespace DMGSimpleUI.DMG.Samples;

public class MainMenuSample : DMGScene
{
    public static Action QuitGame;
    public static Action<DMGTransition> ScreenTransition;
    private SceneTypes _sceneTypes = SceneTypes.MAIN_MENU;
    
    private Texture2D backgroundTexture;
    private readonly List<BaseUIElement> _elements = new();

    private DMGPanel background;
    private DMGPanel foreground;
    
    public MainMenuSample(DMGUITheme theme)
    {
        backgroundTexture = DMGUIGlobals.Content.Load<Texture2D>("panelbg64x");
        
        var t = DMGUIGlobals.Content.Load<Texture2D>("whitebutton128x32"); 
        
        background = new DMGPanel(backgroundTexture, new(0, 0),
            DMGUIGlobals.UIFont,theme,
            new Point(DMGUIGlobals.Bounds.X, DMGUIGlobals.Bounds.Y), "BACKGROUND PANEL..");
        foreground = new DMGPanel(backgroundTexture, new(0, 0),
            DMGUIGlobals.UIFont,theme,
            new Point(DMGUIGlobals.Bounds.X, DMGUIGlobals.Bounds.Y), "FOREGROUND PANEL..", Color.Transparent);
        
        // ReSharper disable once PossibleLossOfFraction
        var H_CENTER = (float)(DMGUIGlobals.Bounds.Y / 2) ;
        var V_CENTER = (float)DMGUIGlobals.Bounds.X / 2;
        
        background.AddChild(new DMGPanel(backgroundTexture, new(540, 325),
            DMGUIGlobals.UIFont,theme,
            new Point(200, 256), "MAIN MENU PANEL"));
        background.AddChild( new DMGButton(t, new Vector2(V_CENTER -64, H_CENTER),
            theme,
            DMGUIGlobals.UIFont, "PLAY GAME")).OnClick += OnPlayGame;
        background.AddChild(new DMGButton(t, new Vector2(V_CENTER -64, H_CENTER +35),
            theme,
            DMGUIGlobals.UIFont, "SETTINGS")).OnClick += OnSettings;
        background.AddChild(new DMGButton(t, new Vector2(V_CENTER -64, H_CENTER +70),
            theme,
            DMGUIGlobals.UIFont, "QUIT GAME")).OnClick += OnQuit;
        
        background.AddChild(foreground);
        
        _elements.Add(background);
    }

    private void OnPlayGame(object sender, EventArgs e)
    {
        var tran = new DMGTransition()
        {
            TransitionType = DMGTransitionType.FADE_OUT,
            duration = 2f,
            callingScene = SceneTypes.MAIN_MENU,
            nextScene = SceneTypes.MENU_BAR,
            _uiElement = foreground,
        };
        ScreenTransition?.Invoke(tran);
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
            item.Draw();
        }
    }

    public override void ReInit()
    {
        foreground._color = Color.Transparent;
    }
}