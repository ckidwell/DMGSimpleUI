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
    
    // Render Target items
    private readonly DMGCanvas _dmgCanvas;
    private readonly GraphicsDeviceManager _graphics;

    public UIManager(Game game, GraphicsDeviceManager graphics, DMGUITheme theme)
    {
        _game = game;
        _theme = theme;
        _graphics = graphics;
        
        DMGUIGlobals.Bounds = new(1280, 720);
        
        _dmgCanvas = new(_graphics.GraphicsDevice,
            DMGUIGlobals.Bounds.X,
            DMGUIGlobals.Bounds.Y);
        
        _graphics.PreferredBackBufferWidth = DMGUIGlobals.Bounds.X;
        _graphics.PreferredBackBufferHeight =  DMGUIGlobals.Bounds.Y;
        
        _graphics.ApplyChanges();
        ButtonTexture = DMGUIGlobals.Content.Load<Texture2D>("whitebutton128x32");
        Font = DMGUIGlobals.Content.Load<SpriteFont>("KarenFat");
        DMGUIGlobals.UIFont = GetUISpriteFont();

        SampleItemsInit();
    }

    private void SampleItemsInit()
    {
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
        SetResolution(DMGUIGlobals.Bounds.X,DMGUIGlobals.Bounds.Y);
    }
    
    private void SetResolution(int height, int width)
    {
        _graphics.PreferredBackBufferWidth = height;
        _graphics.PreferredBackBufferHeight = width;
        _game.Window.IsBorderless = false;
        _graphics.ApplyChanges();
        _dmgCanvas.SetDestinationRectangle();
    }
    private void SetFullScreen()
    {
        _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        _game.Window.IsBorderless = true;
        _graphics.ApplyChanges();
        _dmgCanvas.SetDestinationRectangle();
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
        if (DMGUIGlobals.IsKeyPressed(Keys.F1)) SetResolution(1280, 720);
        if (DMGUIGlobals.IsKeyPressed(Keys.F2)) SetResolution(1920, 1080);
        if (DMGUIGlobals.IsKeyPressed(Keys.F3)) SetResolution(640, 1080);
        if (DMGUIGlobals.IsKeyPressed(Keys.F4)) SetFullScreen();
        
        _updateActiveUi();
        
        if(_sampleSceneNavigator.TransitionActive())
            _sampleSceneNavigator.Update(gameTime);
    }

    public void Draw()
    {
        _dmgCanvas.Activate();
        DMGUIGlobals.SpriteBatch.Begin();
        {
            _drawActiveUi();
            DMGUIGlobals.SpriteBatch.DrawString(Font, $"Mouse: {DMGUIGlobals.MouseCursor.X} , {DMGUIGlobals.MouseCursor.Y}", new Vector2(DMGUIGlobals.Bounds.X - 150,DMGUIGlobals.Bounds.Y - 25), Color.AntiqueWhite);
            DMGUIGlobals.SpriteBatch.DrawString(Font, infoMessage.message, new Vector2( 150, DMGUIGlobals.Bounds.Y - 25), infoMessage.color);     
        }
        DMGUIGlobals.SpriteBatch.End();
        
        _graphics.GraphicsDevice.SetRenderTarget(null);
        _graphics.GraphicsDevice.Clear(Color.Black);
        DMGUIGlobals.SpriteBatch.Begin();
        _dmgCanvas.Draw(DMGUIGlobals.SpriteBatch);
        DMGUIGlobals.SpriteBatch.End();
    }
}