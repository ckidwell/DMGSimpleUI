using System;
using DMGSimpleUI.DMG.Management;
using DMGSimpleUI.DMG.Models;
using DMGSimpleUI.DMG.Utils;
using MonoGame.Extended;
using MonoGame.Extended.Tweening;


namespace DMGSimpleUI.DMG.Samples;

public class SampleSceneNavigator
{
    private DMGTransition _transition;
    private DMGScene _incomingScene;

    private bool transitioning;
    
    private readonly Tweener _tweener = new Tweener();
    private Slide<Color> ColorSlider;

    public Vector2 rectanglePosition;
    
    // Wipe variables
    private Rectangle _r = Rectangle.Empty;
    private int _displayWidth;
    private int _displayHeight;
    private Vector2 _new_r_Position;
    
    public void InitializeTransition(DMGTransition transition,  DMGScene inScene)
    {
        _transition = transition;
        _incomingScene = inScene;
        
        // near as i can tell tweening colors is not supported?,
        // Monogame.extended discord user Gandifil said they would
        // try to fix to get a overload that might support this.
      
        switch (_transition.TransitionType)
        {
            case DMGTransitionType.FADE_OUT: 
                ColorSlider = new Slide<Color>(_transition._uiElement._color,
                    new Color(0, 0, 0), 2000d, Color.Lerp);
                break;
            case DMGTransitionType.FADE_IN: 
                ColorSlider = new Slide<Color>(new Color(0, 0, 0),
                    _transition._uiElement._color,
                    2000d, Color.Lerp);
                break;
            case DMGTransitionType.WIPE_RIGHT:
                WipeSetup(transition,DMGTransitionType.WIPE_RIGHT);
                break;
            case DMGTransitionType.WIPE_LEFT: 
                WipeSetup(transition,DMGTransitionType.WIPE_LEFT);
                break;
            case DMGTransitionType.WIPE_UP: 
                WipeSetup(transition,DMGTransitionType.WIPE_UP);
                break;
            case DMGTransitionType.WIPE_DOWN: 
                WipeSetup(transition,DMGTransitionType.WIPE_DOWN);
                break;
            
        }

        transitioning = true;
    }

    private void WipeSetup(DMGTransition transition, DMGTransitionType transitionType)
    {
        _r = _transition._uiElement._rect;
        _displayHeight = DMGUIGlobals.GraphicsDeviceManager.GraphicsDevice.Viewport.Bounds.Height;
        _displayWidth = DMGUIGlobals.GraphicsDeviceManager.GraphicsDevice.Viewport.Bounds.Right;
        _transition._uiElement._color = Color.Blue;

        var height_Adjust = 0;
        var width_Adjust = 0;
        
        switch (transitionType)
        {
            case DMGTransitionType.WIPE_RIGHT:
                width_Adjust = _displayWidth;
                break;
            case DMGTransitionType.WIPE_LEFT:
                width_Adjust = _displayWidth * -1;
                break;
            case DMGTransitionType.WIPE_DOWN:
                height_Adjust = _displayHeight ;
                break;
            case DMGTransitionType.WIPE_UP:
                height_Adjust = _displayHeight * -1;
                break;
        }
        
        _new_r_Position = new Vector2(_r.X + width_Adjust, _r.Y + height_Adjust); 
        rectanglePosition = transition._uiElement._position;
        
        _tweener.TweenTo( 
                this,
                i => i.rectanglePosition,
                _new_r_Position,
                _transition.duration,
                .1f)
            .Easing(EasingFunctions.QuinticIn)
            .OnEnd(tween => transitioning = false);
                
        transitioning = true;
    }

    public bool TransitionActive()
    {
        return transitioning;
    }

    public DMGScene NextScene()
    {
        return _incomingScene;
    }
    public void Update(GameTime gameTime)
    {
        if (!transitioning) return;

        switch (_transition.TransitionType)
        {
            case DMGTransitionType.FADE_OUT: 
                Fade ();
                break;
            case DMGTransitionType.FADE_IN: 
                Fade ();
                break;
            case DMGTransitionType.WIPE_RIGHT: 
            case DMGTransitionType.WIPE_DOWN:
            case DMGTransitionType.WIPE_LEFT: 
            case DMGTransitionType.WIPE_UP: 
                Wipe();
                break;
        }
        
    }

    private void Wipe()
    {
        _tweener.Update((float)DMGUIGlobals.ElapsedGameTime.TotalSeconds);
    }

    private void Fade()
    {
        if (!ColorSlider.Done)
        {
            _transition._uiElement._color = ColorSlider.Update();
        }
        else
        {
            transitioning = false;
        }
    }
    public void Draw(SpriteBatch _spriteBatch)
    {
        if (!transitioning) return;
        
        switch (_transition.TransitionType)
        {
            case DMGTransitionType.FADE_IN:
                _transition._uiElement.Draw();
                break;
            case DMGTransitionType.FADE_OUT:
                _transition._uiElement.Draw();
                break;
            case DMGTransitionType.WIPE_RIGHT:
            case DMGTransitionType.WIPE_LEFT:
            case DMGTransitionType.WIPE_DOWN:
            case DMGTransitionType.WIPE_UP:
                _spriteBatch.FillRectangle(rectanglePosition.X,
                    rectanglePosition.Y,
                    _transition._uiElement._rect.Size.Y,
                    _transition._uiElement._rect.Size.X,
                    _transition.theme.foregroundColor);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    } 
}