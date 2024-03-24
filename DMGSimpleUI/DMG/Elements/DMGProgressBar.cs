using System;
using System.Runtime.InteropServices;
using DMGSimpleUI.DMG.Management;
using DMGSimpleUI.DMG.Models;
using MonoGame.Extended.BitmapFonts;

namespace DMGSimpleUI.DMG.Elements;

public class DMGProgressBar : BaseUIElement
{
    public new event EventHandler OnClick;
    private Rectangle worldPosition;
    private DMGUITheme _theme;
    private Color _fontCurrentColor;
    private Color _backGroundColor;
    private Texture2D _backGroundTexture;
    
    // progress bar values
    private float _maxVal;
    private float _currentVal;
    private Rectangle _slidingBar;
    
    public DMGProgressBar(Texture2D t, Texture2D background, Vector2 position, float maximumValue, DMGUITheme theme,BitmapFont f, string buttonText = "") : base()
    {
        _theme = theme;
        _color = _theme.buttonNormalColor;
        _texture = t;
        _backGroundTexture = background;
        _position = position;
        _text = buttonText;
        _origin = new Vector2( 0,0);
        _fontCurrentColor = _theme.fontColor;
        
        _rect = new Rectangle((int)_position.X, (int)_position.Y, t.Width, t.Height);

        _font = f;
        _interactable = false;
        
        leftTextPadding = (int) (t.Width * .1);
        topleftTextPadding = (int) (t.Height *.34 );

        _maxVal = maximumValue;
        _currentVal = maximumValue;
        _slidingBar = new Rectangle(0, 0, t.Width, t.Height);

    }
    public DMGProgressBar(Texture2D t, Texture2D background, Vector2 position, Color color, Color backgroundColor,float maximumValue, DMGUITheme theme,BitmapFont f, string buttonText = "") : base()
    {
        _theme = theme;
        _color = color;
        _shade = color;
        _backGroundColor = backgroundColor;
        _texture = t;
        _backGroundTexture = background;
        _position = position;
        _text = buttonText;
        _origin = new Vector2( 0,0);
        _fontCurrentColor = _theme.fontColor;
        
        _rect = new Rectangle((int)_position.X, (int)_position.Y, t.Width, t.Height);

        _font = f;
        _interactable = false;
        
        leftTextPadding = (int) (t.Width * .1);
        topleftTextPadding = (int) (t.Height *.34 );

        _maxVal = maximumValue;
        _currentVal = maximumValue;
        _slidingBar = new Rectangle(0, 0, t.Width, t.Height);
    }
   
    public override void Draw()
    {
        var worldPosition = _position;
        
        DMGUIGlobals.SpriteBatch.Draw(_backGroundTexture, worldPosition,null,_backGroundColor,0f,_origin,1f,SpriteEffects.None,0f);
        DMGUIGlobals.SpriteBatch.Draw(_texture, worldPosition,_slidingBar,_shade,0f,_origin,1f,SpriteEffects.None,1f);
        
        if (_text.Length > 0)
        {
            DMGUIGlobals.SpriteBatch.DrawString(_font,
                $"{_text}",
                new Vector2(worldPosition.X -_origin.X + leftTextPadding,
                    worldPosition.Y - _origin.Y +topleftTextPadding),
                _fontCurrentColor);
        }
        
        var _childrenSpan = CollectionsMarshal.AsSpan(_children);
        for (var i = 0; i < _childrenSpan.Length; i++)
        {
            _childrenSpan[i].Draw();
        }
       
    }
    public void SetValue(float value)
    {
        _currentVal = value;
    }
    public override void Update()
    {
        _slidingBar.Width = (int)(_currentVal / _maxVal * _texture.Width);
        return;
    }
    
    protected override void Click()
    {
        return;
    }

}