using System.Runtime.InteropServices;
using DMGSimpleUI.DMG.Management;
using DMGSimpleUI.DMG.Models;
using MonoGame.Extended.BitmapFonts;

namespace DMGSimpleUI.DMG.Elements;

public class DMGPanel : BaseUIElement
{

    public DMGPanel(Texture2D t, Vector2 position,BitmapFont f, DMGUITheme theme,  Point size, string text = "", Color? color = null)
    {
        _texture = t;
        _position = position;
        _text = text;
        _origin = new Vector2( 0,0);
        _rect = new Rectangle((int)position.X, (int)position.Y, size.Y, size.X);
        _font = f;
        _color = color ?? theme.panelColor;
        _interactable = true;
        

        leftTextPadding = (int) (t.Height * .1);
        topleftTextPadding = (int) (t.Width * .1);
    }

    public override void Update()
    {
        var _childrenSpan = CollectionsMarshal.AsSpan(_children);
        for (var i = 0; i < _childrenSpan.Length; i++)
        {
            _childrenSpan[i].Update();
        }
    }

    public override void Draw()
    {
        var worldPosition = _position;
        // limited to graphic size / no stretch
        //DMGUIGlobals.SpriteBatch.Draw(_texture, worldPosition,_rect,_color,0f,_origin,1f,SpriteEffects.None,0f);
        
        // stretches to specified rect size, may cause artifacts
        DMGUIGlobals.SpriteBatch.Draw(_texture, new Rectangle(new Point((int)worldPosition.X,(int)worldPosition.Y),new Point(_rect.Height,_rect.Width)), _color );
        
        if(_text.Length > 0)DMGUIGlobals.SpriteBatch.DrawString(_font, $"{_text}", new Vector2(worldPosition.X -_origin.X +leftTextPadding, worldPosition.Y - _origin.Y +topleftTextPadding), Color.White);
        
        var _childrenSpan = CollectionsMarshal.AsSpan(_children);
        for (var i = 0; i < _childrenSpan.Length; i++)
        {
            _childrenSpan[i].Draw();
        }
    }
}