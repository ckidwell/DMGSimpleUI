using DMGSimpleUI.DMG.Elements;

namespace DMGSimpleUI.DMG.Models;

public class DMGTransition
{
    public DMGTransitionType TransitionType { get; set; }
    public DMGUITheme theme { get; set; }
    public BaseUIElement _uiElement;
    public SceneTypes callingScene;
    public SceneTypes nextScene;
    public float duration;
    public bool isDone;
}