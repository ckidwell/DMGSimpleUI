#DMG Simple UI

A simple UI implementation for Monogame projects by DOOM METAL GAMES.

###Features:
Supports primitives of Panels, Buttons, Text, and Scenes
Themeable
Transitions between 

This is a fairly light UI Library for Monogame. The idea is to supply very basic functionality and primitives and leave much of specifics to your game you are implementing.


Here is a sample of a Dark theme:

`
  new DMGUITheme()
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
`

![darktheme_sample](https://github.com/ckidwell/DMGSimpleUI/assets/3445949/ba3fcb1e-9c1f-47bd-bc31-124e81a640aa)

Here is a sample of a Orange/Fire theme:

`
new DMGUITheme()
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

`
![firetheme_sample](https://github.com/ckidwell/DMGSimpleUI/assets/3445949/4c651fa2-8b45-428f-90f1-0ceb1f4282ac)

And finally a sample of  Sprite based theme ; the theme file itself would be Color.White for most of the colors so the sprite is not tinted:

![spritedthemed_sample](https://github.com/ckidwell/DMGSimpleUI/assets/3445949/99154c21-2669-48c6-bb66-8fe7b3e0ab04)
