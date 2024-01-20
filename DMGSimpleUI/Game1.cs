using DMGSimpleUI.DMG.Management;
using DMGSimpleUI.DMG.Models;
using DMGSimpleUI.DMG.Samples;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DMGSimpleUI;

public class Game1 : Game
{
    private UIManager _uiManager;
    
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private DMGUITheme _theme;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }
 
    protected override void Initialize()
    {
        DMGUIGlobals.Bounds = new(1280, 720);
        DMGUIGlobals.Content = Content;
        
        _graphics.PreferredBackBufferWidth = DMGUIGlobals.Bounds.X;
        _graphics.PreferredBackBufferHeight =  DMGUIGlobals.Bounds.Y;
        
        _graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        DMGUIGlobals.SpriteBatch = _spriteBatch;
        
        _theme = SampleThemes.GetFireTheme();
        _uiManager = new UIManager(this,_theme);

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
        GraphicsDevice.Clear(_theme.backgroundColor);

       _spriteBatch.Begin();
       {
           _uiManager.Draw();
       }
       _spriteBatch.End();

        base.Draw(gameTime);
    }

}