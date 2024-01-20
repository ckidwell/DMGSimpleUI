using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
namespace DMGSimpleUI.DMG.Management;

public class DMGUIGlobals
{
    public static float TotalSeconds { get; set; }
    public static ContentManager Content { get; set; }
    public static SpriteBatch SpriteBatch { get; set; }
    public static Point Bounds { get; set; }
    public static SpriteFont UIFont { get; set; }
    public static GraphicsDevice GraphicsDevice { get; set; }
    public static MouseState MouseState;
    public static MouseState LastMouseState;
    private static KeyboardState _lastKeyboard;
    private static KeyboardState _currentKeyboard;
    
    public static bool Clicked { get; set; }
    public static bool BeginDrag { get; set; }
    public static Rectangle MouseCursor { get; set; }
    
    public static void Update(GameTime gt)
    {
        TotalSeconds = (float) gt.ElapsedGameTime.TotalSeconds;
        LastMouseState = MouseState;
        MouseState = Mouse.GetState();
        BeginDrag = (MouseState.LeftButton == ButtonState.Pressed &&
                     LastMouseState.LeftButton == ButtonState.Released);
        Clicked = (MouseState.LeftButton == ButtonState.Pressed) &&
                  (LastMouseState.LeftButton == ButtonState.Released);
        MouseCursor = new Rectangle(MouseState.Position, new Point(1, 1));
        _lastKeyboard = _currentKeyboard;
        _currentKeyboard = Keyboard.GetState();
    }
    public static bool IsKeyPressed(Keys key)
    {
        return _currentKeyboard.IsKeyDown(key) && _lastKeyboard.IsKeyUp(key);
    }
}