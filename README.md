HI GRIZZZ!!!!
==================

Futile-SpineSprite
==================

- This is a basic implementation of the Spine runtime for use with Futile inside Unity. It uses futiles sprite and atlas systems to load/play exported Spine skeletons and animations.

Requirements
============
- This has been upgraded to Unity 5.0+
- I've currently only tested with exporting Spine data from Spine 2.1.27
- I've used the lastest C# Spine-Runtime by Esoteric Software (master branch, as of April 10 2015), https://github.com/EsotericSoftware/spine-runtimes
- The Futile framework by Matt Rix (master branch, as of April 10 2015), https://github.com/MattRix/Futile

** For ease of use, I've included both spine-runtime and futile snapshots in this package.


Setting up your project
=======================
- Put the SpineSprite folder into your Unity projects Plugin folder.
- Optional, Copy the Futile folder into your Plugin folder if you dont already have it in your project.
 
How to use
==========

Example
-------
        GSpineManager.LoadSpine("playerAvatar", "spine/player/player-spine-json", "spine/player/player-atlas");
        GSpineSprite sprite = new GSpineSprite("playerAvatar");
        Futile.stage.AddChild(sprite);
        
        
Loading Spine Data
------------------
- The spine manager will load and link skeleton data to a reference name.

        GSpineManager.LoadSpine("skeletonName", "pathToSpineJson", "pathToImageAtlas");
        GSpineManager.LoadSpine("skeletonName", "pathToSpineJson");

- Your spine json and atlas image / json files all must be in the /Assets/Resources/ folder.  
- The image atlas needs to be loaded before any skeleton Json data is loaded, or at the same time using the all in one function.
- skeletonName is just a lookup name in the SpineManager can be anything you wish. It's only used for looking up spine/atlas data.


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

Events
------
- Through sprite.animation you can access Spines delegates for calling code on Start / End / Event / Complete triggers. Please see the spine-runtime documentation for more details on using these.

Tips
====
- The sprite position is the location of the root bone of the skeleton.
- If you're using image assets with subfolders for your skins in the Spine editor, attachment names will retain the subfolder path. (For example, "goblin/head.png" or "goblingirl/head.png")
- In Texture Packer, use smart folders to copy the same subfolder structure as you have in Spine. The resulting published atlas will embed the folder paths to the element name, matching the Spine editor names.

Not Implementated or Supported
==============================
- You must use Texture Packer to pack your atlases, as Futile was designed to load and cache these files. Select Unity Json as the datatype. I haven't tried using an atlas packed from Spine itself, but I suspect that this most likely will have a different structure and not work.
- Spines Mesh attachments, and the Free Form Deformation is not supported. Only basic image slots will work.
