using System;
using System.Collections.Generic;
using DMGSimpleUI.DMG.Elements;
using DMGSimpleUI.DMG.Management;
using DMGSimpleUI.DMG.Models;

namespace DMGSimpleUI.DMG.Samples;

public class MenuBarSample : DMGScene
{
    public static Action QuitGame;
    private readonly List<BaseUIElement> _elements = new();
    
    public MenuBarSample(DMGUITheme theme)
    {

        var t = DMGUIGlobals.Content.Load<Texture2D>("whitebutton128x32");
        var e = new DMGPanel(t,new(0, 0),
            DMGUIGlobals.UIFont,
            theme, 
            new Point(DMGUIGlobals.Bounds.X,36),
            "MENU");
        _elements.Add(e);
        
        e.AddChild(new DMGButton(t,new(65, 2),theme ,DMGUIGlobals.UIFont,"NEW")).OnClick += OnNew;
        e.AddChild(new DMGButton(t,new(t.Width + 65, 2),theme ,DMGUIGlobals.UIFont,"TEST1")).OnClick += OnTest1;
        e.AddChild(new DMGButton(t,new(t.Width * 2 + 65, 2),theme ,DMGUIGlobals.UIFont,"TEST2")).OnClick += OnTest2;
        // e.AddChild(new DMGButton(t,new(t.Width * 3 + 65, 2),DMGUIGlobals.UIFont,"GRID")).OnClick += CreateGrid;
        // e.AddChild(new DMGButton(t,new(t.Width * 4 + 65, 2),DMGUIGlobals.UIFont,"AUTOMAP")).OnClick += AutoMap;
        e.AddChild(new DMGButton(t,new(DMGUIGlobals.Bounds.X - 129, 2),theme ,DMGUIGlobals.UIFont,"EXIT")).OnClick += Quit;
        e.AddChild(new DMGPanel(t,
            new(0, DMGUIGlobals.Bounds.Y -36),
            DMGUIGlobals.UIFont,theme,
            new Point(DMGUIGlobals.Bounds.X,36),
            "INFO:"));
    }
    
    private void OnNew(object sender, EventArgs e)
    {
        
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
            item.Draw();
        }
    } 
}