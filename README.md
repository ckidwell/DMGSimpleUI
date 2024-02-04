# DMG Simple UI

A simple UI implementation for Monogame projects by DOOM METAL GAMES.

This is a fairly light UI Library for Monogame. The idea is to supply very basic functionality and primitives and leave much of specifics to your game you are implementing.

### Features:

1. Supports primitives of :
 - Panels
 - Buttons
 - Text
 - Scenes
2. Themeable
3. Transitions
   - Wipes - UP, DOWN, LEFT, RIGHT
   - Fade In/Out
4. Render Target Size
   - Set a target size to design to and then scale the draging via Render Target

## Samples / Demo Implementations:

You can find some sample implementations in the project under DMGSimpleUI\DMG\Samples\
- DMGSimpleUI\DMG\Samples\SpriteExample contains two classes that use Texture2D's to implement the UI
- DMGSimpleUI\DMG\Samples\ThemeExamples contains two classes that use Texture2D's to implement the UI
  
In the root Sampmles folder you wlil find a SampleSceneNavigator that uses Delegates to swap themes in conjunction with the DMGTransition class, as well as a SampleTheme class that contains the samples that are demonstrated below.


Here is a GIF demonstrated a Sprite Based UI theme with a WIPE_RIGHT transition to a scene with a menu bar and then navigate back
![sprite_anim_demo2](https://github.com/ckidwell/DMGSimpleUI/assets/3445949/440404c9-24d0-47c4-a809-28df993af4e3)


Here is a sample of a Dark theme:

```
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
```

![darktheme_sample](https://github.com/ckidwell/DMGSimpleUI/assets/3445949/ba3fcb1e-9c1f-47bd-bc31-124e81a640aa)

Here is a sample of a Orange/Fire theme:

```
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

```
![firetheme_sample](https://github.com/ckidwell/DMGSimpleUI/assets/3445949/4c651fa2-8b45-428f-90f1-0ceb1f4282ac)

A sample of  Sprite based theme ; the theme file itself would be Color.White for most of the colors so the sprite is not tinted:



![spritedthemed_sample](https://github.com/ckidwell/DMGSimpleUI/assets/3445949/99154c21-2669-48c6-bb66-8fe7b3e0ab04)



## Transition
Transitions can be called with a small snippet of code, implementation example included.  You should have a UI element in front of all other elements, referred here as a foreground element to mask what is being drawn behind it.


```
 var transition = new DMGTransition()
        {
            TransitionType = DMGTransitionType.FADE_IN,
            theme = _theme,
            duration = 2f,
            nextScene = SceneTypes.MENU_BAR_THEMED,
            _uiElement = foreground,
        };
```

The resulting animation:

![fade_in](https://github.com/ckidwell/DMGSimpleUI/assets/3445949/ad0f55f4-cf46-4b2c-b5bf-b7ce3b98bd77)

Transitions are implemented with Monogame.Extended.Tween library so you can customize with differnt EasingFunctions

## Render Target

Press F1-F5 to toggle between a few sample hard coded resolutions to demonstrate render target functionality. You will want to create your own code to either require certain resolutions or detect the users resolution and adjust to it.

**NOTE** Currently there is a bug where render targets that are very extended that display black letterboxing for scaling cause the intersecction of cursor over UI elements to be skewed so that the larger the letterbox is the more skewed the UI element is in the same axis as the letterboxing. In extreme cases this can result in the clickable area of a button being  completely outside of a buttons actual visible area.

![render_target](https://github.com/ckidwell/DMGSimpleUI/assets/3445949/354ccff3-8fd7-43dd-8f84-ba3dd217142c)
