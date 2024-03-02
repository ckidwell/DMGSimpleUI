using DMGSimpleUI.DMG.Management;
using DMGSimpleUI.DMG.Models;
using DMGSimpleUI.DMG.Samples;

namespace DMGSimpleUI;

public class Game1 : Game
{
    private UIManager _uiManager;
    
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }
 
    protected override void Initialize()
    {
        DMGUIGlobals.Content = Content;
        DMGUIGlobals.GraphicsDeviceManager = _graphics;
        
        DMGUIGlobals.Theme = SampleThemes.GetTexturedTheme();
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        DMGUIGlobals.SpriteBatch = _spriteBatch;
        SampleSpriteLoader.LoadSampleSprites(Content);
        _uiManager = new UIManager(this,_graphics,DMGUIGlobals.Theme);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        DMGUIGlobals.Update(gameTime);
        _uiManager.Update(gameTime);
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _uiManager.Draw();
        base.Draw(gameTime);
    }

}