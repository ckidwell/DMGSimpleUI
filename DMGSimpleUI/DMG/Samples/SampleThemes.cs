using DMGSimpleUI.DMG.Models;

namespace DMGSimpleUI.DMG.Samples;

public static class SampleThemes
{

    public static DMGUITheme GetDarkTheme()
    {
        return new DMGUITheme()
        {
            backgroundColor = new Color(3, 4, 6, 255),
            foregroundColor = new Color(39,50,68,255),
            panelColor =  new Color(41,61,88,255),
            buttonNormalColor = new Color(42, 71, 109, 255),
            buttonDisabledColor = new Color(36, 36, 36, 255),
            buttonHoverOverColor = new Color(36, 94, 152, 255),
            fontColor = new Color(111, 156, 245, 255),
            fontHoverColor = new Color(109,156,249,255),
            fontDisabledColor = new Color(111,130,130,255),
        };
    }

    public static DMGUITheme GetFireTheme()
    {
        return new DMGUITheme()
        {
            backgroundColor = new Color(45, 26, 26, 255),
            foregroundColor = new Color(45, 26, 26, 255),
            panelColor =  new Color(66,31,29,255),
            buttonNormalColor = new Color(148, 52, 24, 255),
            buttonDisabledColor = new Color(113, 66, 36, 255),
            buttonHoverOverColor = new Color(167, 60, 15, 255),
            fontColor = new Color(187, 60, 15, 255),
            fontHoverColor = new Color(249, 160, 31, 255),
            fontDisabledColor = new Color(196,180,157,255),

        };
    }
    public static DMGUITheme GetTexturedTheme()
    {
        return new DMGUITheme()
        {
            backgroundColor = Color.White,
            panelColor =  Color.White,
            buttonNormalColor =  Color.White,
            buttonDisabledColor = new Color(169, 169, 169, 255),
            buttonHoverOverColor =  new Color(237,237,237,255),
            fontColor = new Color(128, 128, 128, 255),
            fontHoverColor =  new Color(128, 128, 128, 255),
            fontDisabledColor = new Color(128, 128, 128, 255),
        };
    }
}