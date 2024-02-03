using System;
using System.Collections.Generic;
using DMGSimpleUI.DMG.Elements;
using DMGSimpleUI.DMG.Management;
using DMGSimpleUI.DMG.Models;

namespace DMGSimpleUI.DMG.Samples.SpriteExample;

public class SpriteMenuBarSample : DMGScene
{
    public static Action QuitGame;
    private readonly List<BaseUIElement> _elements = new();
    private Texture2D backgroundTexture;
    private DMGPanel foreground;
    public static Action<DMGTransition> ScreenTransition;
    private SceneTypes _sceneTypes = SceneTypes.MENU_BAR_THEMED;
    private DMGUITheme _theme;

    
    public SpriteMenuBarSample(DMGUITheme theme)
    {
        _theme = theme;
        backgroundTexture = DMGUIGlobals.Content.Load<Texture2D>("panelbg64x");
        
        var t = DMGUIGlobals.Content.Load<Texture2D>("whitebutton128x32");
        var e = new DMGPanel(SampleSpriteLoader.menuBar,new(0, 0),
            DMGUIGlobals.UIFont,
            _theme, 
            new Point(DMGUIGlobals.Bounds.X,36),
            string.Empty);
        
        foreground = new DMGPanel(backgroundTexture, new(0, 0),
            DMGUIGlobals.UIFont,_theme,
            new Point(DMGUIGlobals.Bounds.X, DMGUIGlobals.Bounds.Y),"", Color.Transparent);
        
        _elements.Add(e);
        
        e.AddChild(new DMGButton(SampleSpriteLoader.menuText,new(65, 9),_theme ,DMGUIGlobals.UIFont,string.Empty)).OnClick += OnMenu;
        e.AddChild(new DMGButton(SampleSpriteLoader.settingsText,new(t.Width + 45, 9),_theme ,DMGUIGlobals.UIFont,string.Empty)).OnClick += OnSettings;
        e.AddChild(new DMGButton(SampleSpriteLoader.exitText,new(DMGUIGlobals.Bounds.X - 129, 9),_theme ,DMGUIGlobals.UIFont,string.Empty)).OnClick += Quit;
        e.AddChild(new DMGPanel(SampleSpriteLoader.menuBar,
            new(0, DMGUIGlobals.Bounds.Y -36),
            DMGUIGlobals.UIFont,_theme,
            new Point(DMGUIGlobals.Bounds.X,36),
            "INFO:"));
        e.AddChild(foreground);
    }
    
    private void OnMenu(object sender, EventArgs e)
    {
        var transition = new DMGTransition()
        {
            TransitionType = DMGTransitionType.WIPE_RIGHT,
            theme = _theme,
            duration = 2f,
            nextScene = SceneTypes.MAIN_MENU_SPRITE,
            _uiElement = foreground,
        };
        ScreenTransition?.Invoke(transition);
    }

    private void OnSettings(object sender, EventArgs e)
    {
        // Not Implemented
    }
  
    private void Quit(object sender, EventArgs e)
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
        DMGUIGlobals.SpriteBatch.DrawString(DMGUIGlobals.UIFont, $"Mouse: {DMGUIGlobals.MouseCursor.X} , {DMGUIGlobals.MouseCursor.Y}", new Vector2(DMGUIGlobals.Bounds.X - 150,DMGUIGlobals.Bounds.Y - 25), Color.AntiqueWhite);
        DMGUIGlobals.SpriteBatch.DrawString(DMGUIGlobals.UIFont, DMGUIGlobals.GetMessage(), new Vector2( 150, DMGUIGlobals.Bounds.Y - 25), DMGUIGlobals.GetMessageColor());   
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