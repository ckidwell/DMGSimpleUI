using DMGSimpleUI.DMG.Models;

namespace DMGSimpleUI.DMG.Samples;

public static class SampleThemes
{

    public static DMGUITheme GetDarkTheme()
    {
        return new DMGUITheme()
        {
            backgroundColor = new Color(3, 4, 6, 255),
            foregroundColor = Color.Black,
            panelColor =  new Color(45,45,45,255),
            buttonNormalColor = new Color(72, 72, 72, 255),
            buttonDisabledColor = new Color(36, 36, 36, 255),
            buttonHoverOverColor = new Color(40, 49, 69, 255),
            fontColor = new Color(111, 156, 245, 255),
            fontHoverColor = new Color(109,156,249,255),
            fontDisabledColor = new Color(111,130,130,255),

        };
    }
    public static DMGUITheme GetFireTheme()
    {
        return new DMGUITheme()
        {
            backgroundColor = new Color(27, 11, 0, 255),
            foregroundColor = Color.Black,
            panelColor =  new Color(93,37,0,255),
            buttonNormalColor = new Color(153, 61, 4, 255),
            buttonDisabledColor = new Color(113, 66, 36, 255),
            buttonHoverOverColor = new Color(193, 77, 4, 255),
            fontColor = new Color(249, 160, 31, 255),
            fontHoverColor = new Color(249, 160, 31, 255),
            fontDisabledColor = new Color(196,180,157,255),

        };
    }
    public static DMGUITheme GetTexturedTheme()
    {
        return new DMGUITheme()
        {
            backgroundColor = new Color(255, 255, 0, 255),
            panelColor =  new Color(255,255,255,255),
            buttonNormalColor =  new Color(255,255,255,255),
            buttonDisabledColor = new Color(169, 169, 169, 255),
            buttonHoverOverColor =  new Color(237,237,237,255),
            fontColor = new Color(0, 0, 0, 255),
            fontHoverColor =  new Color(0, 0, 0, 255),
            fontDisabledColor = new Color(0, 0, 0, 255),

        };
    }
}