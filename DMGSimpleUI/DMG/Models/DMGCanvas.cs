using System;

namespace DMGSimpleUI.DMG.Models;

public class DMGCanvas
{
    private readonly RenderTarget2D _target;
    private readonly GraphicsDevice _graphicsDevice;
    private Rectangle _destinationRectangle;
    private Color onyx = new Color(3, 4, 6, 255);

    public DMGCanvas(GraphicsDevice graphicsDevice, int width, int height)
    {
        _graphicsDevice = graphicsDevice;
        _target = new(_graphicsDevice, width, height);
    }

    public void SetDestinationRectangle()
    {
        var screenSize = _graphicsDevice.PresentationParameters.Bounds;

        var scaleX = (float)screenSize.Width / _target.Width;
        var scaleY = (float)screenSize.Height / _target.Height;
        var scale = Math.Min(scaleX, scaleY);

        var newWidth = (int)(_target.Width * scale);
        var newHeight = (int)(_target.Height * scale);

        var posX = (screenSize.Width - newWidth) / 2;
        var posY = (screenSize.Height - newHeight) / 2;

        _destinationRectangle = new Rectangle(posX, posY, newWidth, newHeight);
    }

    public RenderTarget2D GetRenderTarget()
    {
        return _target;
    }
    public void Activate()
    {
        _graphicsDevice.SetRenderTarget(_target);
        _graphicsDevice.Clear(onyx);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_target, _destinationRectangle, Color.White);
    }
}