using System;
using DMGSimpleUI.DMG.Management;
using DMGSimpleUI.DMG.Models;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tweening;

namespace DMGSimpleUI.DMG.Samples;

public class SampleSceneNavigator
{
    private DMGTransition _transition;
    private DMGScene _outGoingScene;
    private DMGScene _incomingScene;

    private float _transitionHalfwayTime = 0f;
    
    private bool _transitionHalfway = false;
    private bool _transitionComplete = false;
    private readonly Tweener _tweener = new Tweener();
    
    public void InitializeTransition(DMGTransition transition, DMGScene outScene, DMGScene inScene)
    {
        _transition = transition;
        _outGoingScene = outScene;
        _incomingScene = inScene;
        
        _transitionHalfway = false;
        _transitionComplete = false;

        _transitionHalfwayTime = transition.duration / 2;
        
        var item = transition._uiElement;
        
        // near as i can tell tweening colors is not supported?
        
        _tweener.TweenTo(target: item, expression: ui => item._color, toValue: new Color(0, 0, 0, 255),
                duration: _transition.duration, delay: .1f)
            .Easing(EasingFunctions.QuinticIn)
            .OnEnd(tween => _transitionComplete = true);
    }

    public bool TransitionActive()
    {
        return !_transitionComplete;
    }
    public void Update(GameTime gameTime)
    {
        if(!_transitionComplete)
            _tweener.Update(gameTime.GetElapsedSeconds());
    }

    public void Draw()
    {
        if(!_transitionComplete)
            _transition._uiElement.Draw();
    } 
    
}