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
    
    // private UIAlertMessage infoMessage = new UIAlertMessage{message = String.Empty, color = Color.White };
    // private Dictionary<int, UIAlertMessage> UIAlertMessages = new Dictionary<int, UIAlertMessage>();
    // private int messageCount = 0;
    
    // UI Samples
    private MenuBarSample _menuBarSample;
    private MainMenuSample _mainMenuSample;
    private SampleSceneNavigator _sampleSceneNavigator = new SampleSceneNavigator();
    
    // Delegate for active UI 
    private delegate void DrawActiveUIDelegate();
    private DrawActiveUIDelegate _drawActiveUiDelegate;
    private delegate void UpdateActiveUIDelegate();
    private UpdateActiveUIDelegate _updateActiveUiDelegate;
    
    private Game _game;
    private DMGUITheme _theme;
    
    //scene transition
    private bool transitionInProgress = false;
    
    // Render Target items
    private readonly DMGCanvas _dmgCanvas;
    private readonly GraphicsDeviceManager _graphics;
    public static Vector2 CursorScaling { get; private set; } 

    public UIManager(Game game, GraphicsDeviceManager graphics, DMGUITheme theme)
    {
        _game = game;
        _theme = theme;
        _graphics = graphics;
        
        
        DMGUIGlobals.Bounds = new(640, 480);
        
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

        _drawActiveUiDelegate = _mainMenuSample.Draw;
        _updateActiveUiDelegate = _mainMenuSample.Update;
         
        // _drawActiveUi = _menuBarSample.Draw;
        // _updateActiveUi = _menuBarSample.Update;
        
        MenuBarSample.QuitGame += OnQuitGame;
        MainMenuSample.QuitGame += OnQuitGame;
        MainMenuSample.ScreenTransition += OnScreenTransition;
        MenuBarSample.ScreenTransition += OnScreenTransition;
         
        DMGUIGlobals.AddUIAlertMessage("Welcome to DMG Simple UI Demo", Color.Aqua);
        //SetResolution(DMGUIGlobals.Bounds.X,DMGUIGlobals.Bounds.Y);
        SetResolution(480,360);
    }
    
    private void SetResolution(int height, int width)
    {
        _graphics.PreferredBackBufferWidth = height;
        _graphics.PreferredBackBufferHeight = width;
        _game.Window.IsBorderless = false;
        _graphics.ApplyChanges();
        _dmgCanvas.SetDestinationRectangle();
        UpdateCursorScaling();
    }
    private void SetFullScreen()
    {
        _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        _game.Window.IsBorderless = true;
        _graphics.ApplyChanges();
        _dmgCanvas.SetDestinationRectangle();
    }
    private void UpdateCursorScaling()
    {

        var r_target_width = _dmgCanvas.GetRenderTarget().Width;
        var display_mode_width = (float) _graphics.PreferredBackBufferWidth; //GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width; <- is this for fullscreen?
        var divided_width = r_target_width / display_mode_width;
        
        var r_target_height = _dmgCanvas.GetRenderTarget().Height;
        var display_mode_height = (float) _graphics.PreferredBackBufferHeight; //GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height; <- is this for fullscreen?
        var divided_height = r_target_height / display_mode_height;

       // BUG: This cursor scaling works well UNTIL we have excessive letterboxing
       // at such time it starts stretching the coordinates to the real screen,
       // not the rendered bit inside the letterboxing
       // did some googling - not sure on how to solve this yet
       // problem occurs for both vertical and horizontal letterboxing
       
        CursorScaling = new Vector2(divided_width, divided_height);
        
        // CursorScaling = new Vector2(_dmgCanvas.GetRenderTarget().Width / (float) GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
        //     _dmgCanvas.GetRenderTarget().Height / (float) GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
    }
  
    private void OnQuitGame()
    {
        _game.Exit();
    }

    public void ProcessInput()
    {
        if (DMGUIGlobals.IsKeyPressed(Keys.F1)) SetResolution(1280, 720);
        if (DMGUIGlobals.IsKeyPressed(Keys.F2)) SetResolution(1920, 1080);
        if (DMGUIGlobals.IsKeyPressed(Keys.F3)) SetResolution(1400, 900);
        if (DMGUIGlobals.IsKeyPressed(Keys.F4)) SetResolution(800, 1280);
        if (DMGUIGlobals.IsKeyPressed(Keys.F5)) SetResolution(1800, 480);
        if (DMGUIGlobals.IsKeyPressed(Keys.F6)) SetFullScreen();
    }
    // public void AddUIAlertMessage(string m, Color c)
    // {
    //     var newMessage = new UIAlertMessage {message = m, color = c};
    //     UIAlertMessages.Add(messageCount++, newMessage);
    //     infoMessage = newMessage;
    // }
    public SpriteFont GetUISpriteFont()
    {
        return Font;
    }

    public BaseUIElement AddUIElement(BaseUIElement e)
    {
        _elements.Add(e);

        return e;
    }
    private void OnScreenTransition(DMGTransition transition)
    {
        _sampleSceneNavigator.InitializeTransition(transition,
            GetSceneByEnum(transition.callingScene),
            GetSceneByEnum(transition.nextScene));
        transitionInProgress = true;
    }

    private DMGScene GetSceneByEnum(SceneTypes scene)
    {
        return scene switch
        {
            SceneTypes.MAIN_MENU => _mainMenuSample,
            SceneTypes.MENU_BAR => _menuBarSample,
            _ => throw new ArgumentException("Invalid scene type"),
        };
    }
    public void Update(GameTime gameTime)
    {
        ProcessInput();
        
        _updateActiveUiDelegate();

        if (!transitionInProgress) return;

        if (_sampleSceneNavigator.TransitionActive())
        {
            _sampleSceneNavigator.Update(gameTime);
        }
        else
        {
            var nextScene = _sampleSceneNavigator.NextScene();
            _drawActiveUiDelegate = nextScene.Draw;
            _updateActiveUiDelegate = nextScene.Update;
            nextScene.ReInit();
            transitionInProgress = false;
        }

    }

    public void Draw()
    {
        _dmgCanvas.Activate();
        DMGUIGlobals.SpriteBatch.Begin();
        {
            _drawActiveUiDelegate();
            if (_sampleSceneNavigator.TransitionActive())
            {
                _sampleSceneNavigator.Draw(DMGUIGlobals.SpriteBatch);
            }
             
        }
        DMGUIGlobals.SpriteBatch.End();
        
        _graphics.GraphicsDevice.SetRenderTarget(null);
        _graphics.GraphicsDevice.Clear(Color.Black);
        DMGUIGlobals.SpriteBatch.Begin();
        _dmgCanvas.Draw(DMGUIGlobals.SpriteBatch);
        DMGUIGlobals.SpriteBatch.End();
    }
}