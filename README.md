Futile-SpineSprite
==================

- A sprite system and manager built ontop of the official spine-runtime that allows you to load/play exported Spine skeletons and animations with the Futile framework for Unity.

- This is very much a work in progress. There's lots of room to improve, expand and optimize.

Requirements
============
- The generic C# Spine-Runtime by Esoteric Software, https://github.com/EsotericSoftware/spine-runtimes (/spine-csharp/src/ folder)
- Futile by Matt Rix, https://github.com/MattRix/Futile (I've used the development-physics branch)

Demo
====
- Launch Futile-SpineSprite/Assets/Scenes/Example.unity


Setting up your project
=======================
- Put the SpineSprite folder into your Unity projects Plugin folder.
 
How to use
==========

Example
-------
        GSpineManager.LoadSpine("playerAvatar", "spine/player/playerJson", "spine/player/playerAtlas");
        GSpineSprite sprite = new GSpineSprite("playerAvatar");
        Futile.stage.AddChild(sprite);
        
        
Loading Spine Data
------------------
- The spine manager will load and link skeleton data to a reference name.

        GSpineManager.LoadSpine("skeletonName", "pathToSpineJson", "pathToImageAtlas");
        GSpineManager.LoadSpine("skeletonName", "pathToSpineJson");

- Both pathToSpineJson and pathToImageAtlas are relative to the /Assets/Resources/ folder.  
- The image atlas needs to be loaded before any skeleton Json data is loaded, or at the same time.
- skeletonName can be anything you wish and will be used to lookup skeleton data.


Create the Sprite
-----------------
        GSpineSprite sprite = new GSpineSprite("skeletonName");
        Futile.stage.AddChild(sprite);

Skin Controls
-------------
        sprite.SetSkin("skinName");

Animation Controls
------------------
        sprite.Play("animationName", bool loop);
        sprite.Queue("animationName", bool loop);
        sprite.Pause();
        sprite.Resume();
        sprite.Stop();
        sprite.SetAnimationTimeScale(1.0f);
        sprite.SetAnimationMix("fromAnimationName", "toAnimationName", floatDuration);

- Animation Mix is the amount of time to blend between 2 animation states. For example, you can smoothly blend from a jumping animation back to a walking animation over x seconds. Blending is done automatically when the spine runtime has a queued animation. Setting an animation explicitly will skip blending.

Color Controls
--------------
- The entire sprite can be colored as a whole or by specific slots. (alpha is supported but can cause blending artifcats with overlaping joints.)

        sprite.color = Color.white;
        sprite.FindSlotByName("slotName").color = Color.red;

Everything else
---------------
- GSpineSprite is extended from FContainer, position, rotation, scale, alpha etc are all supported.

- You can flip the animation horizontally or vertically via...

        sprite.flipX = true;
        sprite.flipY = true;
		

- The 2 main spine-runtime classes are exposed via...

        sprite.skeleton; // the Spine.Skeleton class
        sprite.animation; // the Spine.AnimationState class

Tips
====
- The sprite position is the location of the root bone of the skeleton.
- If you're using image assets with subfolders for your skins in the Spine editor, attachment names will retain the subfolder path. (For example, "goblin/head.png" or "goblingirl/head.png")
- If you build your texture atlas in Texture Packer using smart folders with the same subfolder structure as the Spine skin. The resulting published atlas will embed the folder paths to the element name, matching the Spine editor names.

TODOs
======
- Allow attachments to be created and bound to slots at runtime with any FSprite/FAtlasElement
- Once bounding box support is added to the editor, add support in the SpineSprite class (Spine Editor roadmap can be seen here https://trello.com/board/spine-editor/5131e9578f174c521c0059d9)

Credit
======
- Futile, by Matt Rix, https://github.com/MattRix/Futile
- Spine, by Esoteric Software, http://esotericsoftware.com/
- C# Spine-Runtime, by Esoteric Software, https://github.com/EsotericSoftware/spine-runtimes
