using System.Collections.Generic;
using DMGSimpleUI.DMG.Elements;
using DMGSimpleUI.DMG.Samples;

namespace DMGSimpleUI.DMG.Models;

public class DMGTransition
{
    public DMGTransitionType TransitionType { get; set; }
    public BaseUIElement _uiElement;
    public SceneTypes callingScene;
    public SceneTypes nextScene;
    public float duration;
    public bool isDone;
}