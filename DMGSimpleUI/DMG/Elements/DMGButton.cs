using System;
using System.Runtime.InteropServices;
using DMGSimpleUI.DMG.Management;
using DMGSimpleUI.DMG.Models;

namespace DMGSimpleUI.DMG.Elements;

public class DMGButton: BaseUIElement
{
    public new event EventHandler OnClick;
    private Rectangle worldPosition;
    private DMGUITheme _theme;
    private Color _fontCurrentColor;
    
    public DMGButton(Texture2D t, Vector2 position, DMGUITheme theme,SpriteFont f, string buttonText = "") : base()
    {
        _theme = theme;
        _color = _theme.buttonNormalColor;
        _texture = t;
        _position = position;
        _text = buttonText;
        _origin = new Vector2( 0,0);
        _fontCurrentColor = _theme.fontColor;
        
        _rect = new Rectangle((int)_position.X, (int)_position.Y, t.Width, t.Height);

        _font = f;
        _interactable = true;
        
        leftTextPadding = (int) (t.Width * .1);
        topleftTextPadding = (int) (t.Height *.34 );
    }
    public DMGButton(Texture2D t, Vector2 position,DMGUITheme theme, SpriteFont f,Point size, string buttonText = "") : base()
    {
        // the clickable area for this works, but the texture is not scaled/sized to visually match
        _theme = theme;
        _color = new Color(77, 77, 77, 255);
        _texture = t;
        _position = position;
        _text = buttonText;
        _origin = new Vector2( 0,0);
        _rect = new Rectangle((int)position.X, (int)position.Y, size.Y, size.X);
        _font = f;
        _interactable = true;
    
        // ReSharper disable once PossibleLossOfFraction
        //screenWidthAdjustment = DMGUIGlobals.Bounds.X / 2 - t.Width ;
        // ReSharper disable once PossibleLossOfFraction
        //screenHeightAdjustment = DMGUIGlobals.Bounds.Y / 2 - (t.Height * 4.2);
        
        leftTextPadding = (int) (t.Width * .1);
        topleftTextPadding = (int) (t.Height *.34 );
    }
 
    public override void Draw()
    {
        var worldPosition = _position;
        DMGUIGlobals.SpriteBatch.Draw(_texture, worldPosition,null,_shade,0f,_origin,1f,SpriteEffects.None,0f);
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
    public override void Update()
    {
        //if (!_interactable) return;
       
        var _childrenSpan = CollectionsMarshal.AsSpan(_children);
        for (var i = 0; i < _childrenSpan.Length; i++)
        {
            _childrenSpan[i].Update();
        }
        if (DMGUIGlobals.MouseCursor.Intersects(_rect))
        {
            _shade = _theme.buttonHoverOverColor;
            _fontCurrentColor = _theme.fontHoverColor;
            
            if (DMGUIGlobals.Clicked) Click();
        }
        else
        {
            _shade = _theme.buttonNormalColor;
        }
    }
    
    protected override void Click()
    {
        OnClick?.Invoke(this, EventArgs.Empty);
        OnClickEvent(EventArgs.Empty);
    }
}