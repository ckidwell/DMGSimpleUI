using DMGSimpleUI.DMG.Management;

namespace DMGSimpleUI.DMG.Elements;

public class DMGText: BaseUIElement
{
    public DMGText(string text,Vector2 position, SpriteFont f, Color color)
    {
        _position = position;
        _text = text;
        _font = f;
        _color = color;
    }

    public override void Update()
    {
        foreach (var e in _children)
        {
            e.Update();
        }
    }

    public override void Draw()
    {
        if(_text.Length > 0)DMGUIGlobals.SpriteBatch.DrawString(_font, 
            $"{_text}", 
            new Vector2(_position.X -_origin.X +leftTextPadding, _position.Y - _origin.Y +topleftTextPadding), 
            Color.White);
        
        foreach (var e in _children)
        {
            e.Draw();
        }
    } 
    
}