using System;
using System.Collections.Generic;
using DMGSimpleUI.DMG.Management;

namespace DMGSimpleUI.DMG.Elements;

public class BaseUIElement
{
    protected Texture2D _texture;
    protected string _text;
    protected Vector2 _position;
    protected Rectangle _rect;
    protected Color _shade = Color.White;
    protected Color _color;
    protected SpriteFont _font;
    protected Vector2 _origin;
    protected int leftTextPadding;
    protected int topleftTextPadding;
    protected double screenWidthAdjustment;
    protected double screenHeightAdjustment;
    protected bool _interactable = false;

    protected List<BaseUIElement> _children = new List<BaseUIElement>();
    
    public event EventHandler OnClick;

    public BaseUIElement(Texture2D t, Vector2 position,SpriteFont f, bool interactable, List<BaseUIElement> children, string buttonText = "")
    {
        _texture = t;
        _position = position;
        _text = buttonText;
        _origin = new Vector2(t.Height *2, t.Width *2);
        _rect = new Rectangle((int)position.X, (int)position.Y, t.Width, t.Height);
        _font = f;
        _interactable = interactable;
        _children.AddRange(children);
        
        // ReSharper disable once PossibleLossOfFraction
        screenWidthAdjustment = DMGUIGlobals.Bounds.X / 2 - t.Width ;
        // ReSharper disable once PossibleLossOfFraction
        screenHeightAdjustment = DMGUIGlobals.Bounds.Y / 2 - (t.Height * 4.2);
        
        leftTextPadding = (int) (t.Height /2 );
        topleftTextPadding = (int) (t.Width  * .1);
    }

    protected BaseUIElement()
    {
        
    }

    public virtual void Update()
    {
        if (!_interactable) return;

        _shade = DMGUIGlobals.MouseCursor.Intersects(_rect) ? Color.Gray : Color.White;
    }

    public virtual void Draw()
    {
        DMGUIGlobals.SpriteBatch.Draw(_texture, _position,null,_shade,0f,_origin,1f,SpriteEffects.None,0f);
        
        if(_text.Length > 0) DMGUIGlobals.SpriteBatch.DrawString(_font, $"{_text}", new Vector2(_position.X -_origin.X + leftTextPadding, _position.Y - _origin.Y +topleftTextPadding), Color.White);

        foreach (var e in _children)
        {
            e.Draw();
        }
    }
    
    public BaseUIElement AddChild(BaseUIElement element)
    {
        _children.Add(element);
        return element;
    }
    protected virtual void OnClickEvent(EventArgs e)
    {
        EventHandler handler = OnClick;
        handler?.Invoke(this, e);
    }

    protected virtual void Click()
    {
        if (!_interactable) return;
        OnClick?.Invoke(this, EventArgs.Empty);
    }
}