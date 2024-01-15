using System;
using System.Collections.Generic;
using DMGSimpleUI.DMG.Elements;
using DMGSimpleUI.DMG.Management;
using DMGSimpleUI.DMG.Models;

namespace DMGSimpleUI.DMG.Samples;

public class MainMenuSample : DMGScene
{
    public static Action QuitGame;

    private Texture2D backgroundTexture;
    private readonly List<BaseUIElement> _elements = new();
    
    public MainMenuSample(DMGUITheme theme)
    {
        backgroundTexture = DMGUIGlobals.Content.Load<Texture2D>("panelbg64x");
        var t = DMGUIGlobals.Content.Load<Texture2D>("whitebutton128x32"); 
        
        var bg = new DMGPanel(backgroundTexture, new(0, 0),
            DMGUIGlobals.UIFont,theme,
            new Point(DMGUIGlobals.Bounds.X, DMGUIGlobals.Bounds.Y), "BACKGROUND PANEL..");

        // ReSharper disable once PossibleLossOfFraction
        var H_CENTER = (float)(DMGUIGlobals.Bounds.Y / 2) ;
        var V_CENTER = (float)DMGUIGlobals.Bounds.X / 2;
        bg.AddChild(new DMGPanel(backgroundTexture, new(540, 325),
            DMGUIGlobals.UIFont,theme,
            new Point(200, 256), "MAIN MENU PANEL"));
        bg.AddChild( new DMGButton(t, new Vector2(V_CENTER -64, H_CENTER),
            theme,
            DMGUIGlobals.UIFont, "PLAY GAME"));
        bg.AddChild(new DMGButton(t, new Vector2(V_CENTER -64, H_CENTER +35),
            theme,
            DMGUIGlobals.UIFont, "SETTINGS")).OnClick += OnSettings;
        bg.AddChild(new DMGButton(t, new Vector2(V_CENTER -64, H_CENTER +70),
            theme,
            DMGUIGlobals.UIFont, "QUIT GAME")).OnClick += OnQuit;
        
        _elements.Add(bg);
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
}