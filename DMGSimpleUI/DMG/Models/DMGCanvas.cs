using System;

namespace DMGSimpleUI.DMG.Models;

public class DMGCanvas
{
    private readonly RenderTarget2D _target;
    private readonly GraphicsDevice _graphicsDevice;
    private Rectangle _destinationRectangle;

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

    public void Activate()
    {
        _graphicsDevice.SetRenderTarget(_target);
        _graphicsDevice.Clear(Color.MonoGameOrange);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // _graphicsDevice.SetRenderTarget(null);
        // _graphicsDevice.Clear(Color.Black);
        //spriteBatch.Begin();
        spriteBatch.Draw(_target, _destinationRectangle, Color.White);
        //spriteBatch.End();
    }
}