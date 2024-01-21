using System.Collections.Generic;
using DMGSimpleUI.DMG.Elements;

namespace DMGSimpleUI.DMG.Models;

public class DMGTransition
{
    public DMGTransitionType TransitionType { get; set; }
    public BaseUIElement _uiElement;
    //public List<BaseUIElement> _elements = new();
    public float duration;
    public bool isDone;
}