using System;
using System.Collections.Generic;
using DMGSimpleUI.DMG.Elements;
using DMGSimpleUI.DMG.Models;
using DMGSimpleUI.DMG.Samples;

namespace DMGSimpleUI.DMG.Management;

public class UIManager
{
    private Texture2D ButtonTexture { get; }
    private SpriteFont Font { get; }
    private readonly List<BaseUIElement> _elements = new();
    
    private UIAlertMessage infoMessage = new UIAlertMessage{message = String.Empty, color = Color.White };
   
    private Dictionary<int, UIAlertMessage> UIAlertMessages = new Dictionary<int, UIAlertMessage>();
    private int messageCount = 0;
    
    // UI Samples
    private MenuBarSample _menuBarSample;
    private MainMenuSample _mainMenuSample;
    private SampleSceneNavigator _sampleSceneNavigator = new SampleSceneNavigator();
    
    // Delegate for active UI 
    private delegate void DrawActiveUI();
    private DrawActiveUI _drawActiveUi;
    private delegate void UpdateActiveUI();
    private UpdateActiveUI _updateActiveUi;
    
    private Game _game;
    private DMGUITheme _theme;

    public UIManager(Game game, DMGUITheme theme)
    {
        _game = game;
        _theme = theme;
        
        ButtonTexture = DMGUIGlobals.Content.Load<Texture2D>("whitebutton128x32");
        Font = DMGUIGlobals.Content.Load<SpriteFont>("KarenFat");
        DMGUIGlobals.UIFont = GetUISpriteFont();

        _menuBarSample = new MenuBarSample(_theme);
        _mainMenuSample = new MainMenuSample(_theme);

         _drawActiveUi = _mainMenuSample.Draw;
         _updateActiveUi = _mainMenuSample.Update;
         
         // _drawActiveUi = _menuBarSample.Draw;
         // _updateActiveUi = _menuBarSample.Update;
        
        MenuBarSample.QuitGame += OnQuitGame;
        MainMenuSample.QuitGame += OnQuitGame;
        MainMenuSample.ScreenTransition += OnScreenTransition;
         
        AddUIAlertMessage("Welcome to DMG Simple UI Demo", Color.Aqua);
    }

    private void OnScreenTransition(DMGTransition transition)
    {
        _sampleSceneNavigator.InitializeTransition(transition, _mainMenuSample, _menuBarSample);
    }

    private void OnQuitGame()
    {
        _game.Exit();
    }

    public void ProcessInput()
    {
        
    }
    public void AddUIAlertMessage(string m, Color c)
    {
        var newMessage = new UIAlertMessage {message = m, color = c};
        UIAlertMessages.Add(messageCount++, newMessage);
        infoMessage = newMessage;
    }
    public SpriteFont GetUISpriteFont()
    {
        return Font;
    }

    public BaseUIElement AddUIElement(BaseUIElement e)
    {
        _elements.Add(e);

        return e;
    }

    public void Update(GameTime gameTime)
    {
        _updateActiveUi();
        
        if(_sampleSceneNavigator.TransitionActive())
            _sampleSceneNavigator.Update(gameTime);
    }

    public void Draw()
    {
        _drawActiveUi();
        
        DMGUIGlobals.SpriteBatch.DrawString(Font, $"Mouse: {DMGUIGlobals.MouseCursor.X} , {DMGUIGlobals.MouseCursor.Y}", new Vector2(DMGUIGlobals.Bounds.X - 150,DMGUIGlobals.Bounds.Y - 25), Color.AntiqueWhite);
        DMGUIGlobals.SpriteBatch.DrawString(Font, infoMessage.message, new Vector2( 150, DMGUIGlobals.Bounds.Y - 25), infoMessage.color);
    }
}