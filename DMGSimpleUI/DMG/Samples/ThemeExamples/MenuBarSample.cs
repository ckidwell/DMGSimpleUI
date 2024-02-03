using System;
using System.Collections.Generic;
using DMGSimpleUI.DMG.Elements;
using DMGSimpleUI.DMG.Management;
using DMGSimpleUI.DMG.Models;

namespace DMGSimpleUI.DMG.Samples.ThemeExamples;

public class MenuBarSample : DMGScene
{
    public static Action QuitGame;
    private readonly List<BaseUIElement> _elements = new();
    private Texture2D backgroundTexture;
    private DMGPanel foreground;
    public static Action<DMGTransition> ScreenTransition;
    private SceneTypes _sceneTypes = SceneTypes.MENU_BAR_THEMED;
    private DMGUITheme _theme;

    
    public MenuBarSample(DMGUITheme theme)
    {
        _theme = theme;
        backgroundTexture = DMGUIGlobals.Content.Load<Texture2D>("panelbg64x");
        
        var t = DMGUIGlobals.Content.Load<Texture2D>("whitebutton128x32");
        var e = new DMGPanel(t,new(0, 0),
            DMGUIGlobals.UIFont,
            _theme, 
            new Point(DMGUIGlobals.Bounds.X,36),
            "MENU");
        
        foreground = new DMGPanel(backgroundTexture, new(0, 0),
            DMGUIGlobals.UIFont,_theme,
            new Point(DMGUIGlobals.Bounds.X, DMGUIGlobals.Bounds.Y),"", Color.Transparent);

        
        _elements.Add(e);
        
        e.AddChild(new DMGButton(t,new(65, 2),_theme ,DMGUIGlobals.UIFont,"NEW")).OnClick += OnNew;
        e.AddChild(new DMGButton(t,new(t.Width + 65, 2),_theme ,DMGUIGlobals.UIFont,"TEST1")).OnClick += OnTest1;
        e.AddChild(new DMGButton(t,new(t.Width * 2 + 65, 2),_theme ,DMGUIGlobals.UIFont,"TEST2")).OnClick += OnTest2;
        // e.AddChild(new DMGButton(t,new(t.Width * 3 + 65, 2),DMGUIGlobals.UIFont,"GRID")).OnClick += CreateGrid;
        // e.AddChild(new DMGButton(t,new(t.Width * 4 + 65, 2),DMGUIGlobals.UIFont,"AUTOMAP")).OnClick += AutoMap;
        e.AddChild(new DMGButton(t,new(DMGUIGlobals.Bounds.X - 129, 2),_theme ,DMGUIGlobals.UIFont,"EXIT")).OnClick += Quit;
        e.AddChild(new DMGPanel(t,
            new(0, DMGUIGlobals.Bounds.Y -36),
            DMGUIGlobals.UIFont,_theme,
            new Point(DMGUIGlobals.Bounds.X,36),
            "INFO:"));
        e.AddChild(foreground);
    }
    
    private void OnNew(object sender, EventArgs e)
    {
        var transition = new DMGTransition()
        {
            TransitionType = DMGTransitionType.WIPE_RIGHT,
            theme = _theme,
            duration = 2f,
            nextScene = SceneTypes.MAIN_MENU_THEMED,
            _uiElement = foreground,
        };
        ScreenTransition?.Invoke(transition);
    }

    private void OnTest1(object sender, EventArgs e)
    {
        
    }
    private void OnTest2(object sender, EventArgs e)
    {
        
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