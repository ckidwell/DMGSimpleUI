using System.Collections.Generic;
using DMGSimpleUI.DMG.Elements;

namespace DMGSimpleUI.DMG.Models;

public abstract class DMGScene
{
    public abstract void Update();
    public abstract void Draw();
    public abstract void ReInit();
    public abstract List<BaseUIElement> GetElements();
}