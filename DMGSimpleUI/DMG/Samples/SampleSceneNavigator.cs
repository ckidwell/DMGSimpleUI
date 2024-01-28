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
    private DMGScene _outGoingScene;
    private DMGScene _incomingScene;

    private bool transitioning = false;
    private bool _transitionHalfway = false;
    
    private readonly Tweener _tweener = new Tweener();
    private Slide<Color> ColorSlider;

    public Vector2 rectanglePosition;
    private Rectangle RectangleDestination;
    
    public void InitializeTransition(DMGTransition transition, DMGScene outScene, DMGScene inScene)
    {
        _transition = transition;
        _outGoingScene = outScene;
        _incomingScene = inScene;
        
        _transitionHalfway = false;
        
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
                
                var r = _transition._uiElement._rect;
                var displayWidth = DMGUIGlobals.GraphicsDeviceManager.GraphicsDevice.Viewport.Bounds.Right;
                
                _transition._uiElement._color = _transition.theme.foregroundColor;

                var new_r_Position = new Vector2(r.X + displayWidth, r.Y);
                rectanglePosition = transition._uiElement._position;
        
                 _tweener.TweenTo( 
                          this,
                         i => i.rectanglePosition,
                         new_r_Position,
                          _transition.duration,
                         .1f)
                     .Easing(EasingFunctions.QuinticIn)
                     .OnEnd(tween => transitioning = false);
                
                transitioning = true;
                break;
            case DMGTransitionType.WIPE_LEFT: 
              
                break;
            case DMGTransitionType.WIPE_UP: 
              
                break;
            case DMGTransitionType.WIPE_DOWN: 
              
                break;
            
        }

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
                WipeRight ();
                break;
        }
        
    }

    private void WipeRight()
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
                _spriteBatch.FillRectangle(rectanglePosition.X,
                    rectanglePosition.Y,
                    _transition._uiElement._rect.Size.X,
                    _transition._uiElement._rect.Size.Y,
                    _transition.theme.foregroundColor);
                break;
            case DMGTransitionType.WIPE_LEFT:
                break;
            case DMGTransitionType.WIPE_DOWN:
                break;
            case DMGTransitionType.WIPE_UP:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    } 
}