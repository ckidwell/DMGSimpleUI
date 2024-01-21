using System;
using DMGSimpleUI.DMG.Models;
using DMGSimpleUI.DMG.Utils;
using MonoGame.Extended.Tweening;

namespace DMGSimpleUI.DMG.Samples;

public class SampleSceneNavigator
{
    private DMGTransition _transition;
    private DMGScene _outGoingScene;
    private DMGScene _incomingScene;

    private bool transitioning = false;
    private float _transitionHalfwayTime = 0f;
    private bool _transitionHalfway = false;

    private readonly Tweener _tweener = new Tweener();
    private Slide<Color> ColorSlider;
    
    public void InitializeTransition(DMGTransition transition, DMGScene outScene, DMGScene inScene)
    {
        _transition = transition;
        _outGoingScene = outScene;
        _incomingScene = inScene;
        
        _transitionHalfway = false;
        _transitionHalfwayTime = transition.duration / 2;
        
        // near as i can tell tweening colors is not supported?,
        // Monogame.extended discord user Gandifil said they would
        // try to fix to get a overload that might support this.
        
        //var item = transition._uiElement;
        
        // _tweener.TweenTo(target: item, expression: ui => item._color, toValue: new Color(0, 0, 0, 255),
        //         duration: _transition.duration, delay: .1f)
        //     .Easing(EasingFunctions.QuinticIn)
        //     .OnEnd(tween => _transitionComplete = true);
        ColorSlider = new Slide<Color>(_transition._uiElement._color,
            new Color(0, 0, 0), 2000d, Color.Lerp);
        
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
        
        if (!ColorSlider.Done)
        {
            //_tweener.Update(gameTime.GetElapsedSeconds());
            _transition._uiElement._color = ColorSlider.Update();
        }
        else
        {
            transitioning = false;
        }
    }

    public void Draw()
    {
        if(transitioning)
            _transition._uiElement.Draw();
    } 
    
}