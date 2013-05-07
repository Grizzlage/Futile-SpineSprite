using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Spine;

public class Example : MonoBehaviour {
	
	private GSpineSprite powerup;
	private GSpineSprite spineboy;
	private GSpineSprite goblin;
	
	// for gui stuff
	private GSpineSprite _activeSprite;
	private Vector2 _scrollPosition;
	
	void Start(){

		// Setup Futile
		FutileParams fparams = new FutileParams(true, true, true, true);
		fparams.AddResolutionLevel(1024.0f,	1.0f, 1.0f, "");
		fparams.origin = new Vector2(0.5f,0.5f);
		Futile.instance.Init(fparams);
		
		// Load the powerup sprite
		GSpineManager.LoadSpine("powerup", "spine/powerup/powerupJson", "spine/powerup/powerupAtlas");
		powerup = new GSpineSprite("powerup");
		Futile.stage.AddChild(powerup);
		powerup.Play("animation");
		powerup.SetPosition((Futile.screen.halfWidth * 0.25f), -(Futile.screen.halfHeight * 0.5f));
		_activeSprite = powerup;
		
		// load the goblin sprite
		GSpineManager.LoadSpine("goblin", "spine/goblin/goblinJson", "spine/goblin/goblinAtlas");
		goblin = new GSpineSprite("goblin");
		Futile.stage.AddChild(goblin);
		goblin.SetSkin("goblin");
		goblin.Play("walk");
		goblin.SetPosition((Futile.screen.halfWidth * 0.25f), -(Futile.screen.halfHeight * 0.5f));
		goblin.isVisible = false;
		
		// load the spineboy sprite
		GSpineManager.LoadSpine("spineboy", "spine/spineboy/spineboyJson", "spine/spineboy/spineboyAtlas");
		spineboy = new GSpineSprite("spineboy");
		Futile.stage.AddChild(spineboy);
		spineboy.SetAnimationMix("jump", "walk", 0.4f);
		spineboy.Play("walk");
		spineboy.SetPosition((Futile.screen.halfWidth * 0.25f), -(Futile.screen.halfHeight * 0.5f));	
		spineboy.isVisible = false;
	}
	
	
	void OnGUI() {
		GUI.skin.button.fixedWidth = 200;
		GUILayout.BeginArea(new Rect(0, 0, GUI.skin.button.fixedWidth + 25, Screen.height));
		_scrollPosition = GUILayout.BeginScrollView(_scrollPosition, GUILayout.Width(GUI.skin.button.fixedWidth + 25), GUILayout.Height(Screen.height));
		
		
		// display the poweup sprite
		if(GUILayout.Button("Show Powerup Sprite")){
			_activeSprite.isVisible = false;
			_activeSprite = powerup;
			_activeSprite.isVisible = true;
		}
		
		// display the goblin sprite
		if(GUILayout.Button("Show Goblin Sprite")){
			_activeSprite.isVisible = false;
			_activeSprite = goblin;
			_activeSprite.isVisible = true;
		}
		
		// display spineboy sprite
		if(GUILayout.Button("Show Spineboy Sprite")){
			_activeSprite.isVisible = false;
			_activeSprite = spineboy;
			_activeSprite.isVisible = true;
		}
		
		// play the default animation for the active sprite
		if(GUILayout.Button("Play Animation")){
			if(_activeSprite == powerup)
				powerup.Play("animation");
			else if(_activeSprite == goblin)
				goblin.Play("walk");
			else if(_activeSprite == spineboy)
				spineboy.Play("walk");
		}
	
		// pause the animation
		if(GUILayout.Button("Pause Animation")){
			_activeSprite.Pause();
		}
		
		// resume it
		if(GUILayout.Button("Resume Animation")){
			_activeSprite.Resume();
		}
		
		// stop the animation and reset the pose
		if(GUILayout.Button("Stop Animation")){
			_activeSprite.Stop();
		}
		
		// adjusts the play speed
		if(GUILayout.Button("Slow Motion")){
			_activeSprite.SetAnimationTimeScale(0.5f);
		}
		
		// resets the play speed
		if(GUILayout.Button("Normal Speed")){
			_activeSprite.SetAnimationTimeScale(1.0f);
		}
		
		// color the entire spine blue
		if(GUILayout.Button("Color Sprite Blue")){
			_activeSprite.color = Color.blue;
		}
		
		// reset the color
		if(GUILayout.Button("Clear Sprite Color")){
			_activeSprite.color = Color.white;
		}
		
		// color the powerup wings
		if(GUILayout.Button("Powerup - Color Wings Red")){
			powerup.FindSlotByName("left wing").color = Color.red;
			powerup.FindSlotByName("right wing").color = Color.red;
		}
		
		// changes the skins on the goblin
		if(GUILayout.Button("Goblin - Set Skin (Goblin)")){
			goblin.SetSkin("goblin");
		}
		if(GUILayout.Button("Goblin - Set Skin (GoblinGirl)")){
			goblin.SetSkin("goblingirl");
		}
		
		// spineboy jumps once and stops
		if(GUILayout.Button("Spineboy - Jump Once")){
			spineboy.Play("jump", false);
		}
		
		// spineboy jumps in a loop
		if(GUILayout.Button("Spineboy - Jump Forever")){
			spineboy.Play("jump", true);
		}
		
		// spineboy jumps and then queues walk to start and loop when jumping is finished
		if(GUILayout.Button("Spineboy - Jump & Walk")){
			spineboy.Play("jump", false);
			spineboy.Queue("walk", true);
		}
		
		
		// sets the sprite scale 2x
		if(GUILayout.Button("Scale sprite 2x")){
			_activeSprite.scale = 2.0f;
		}
		
		// sets the sprite scale .5x
		if(GUILayout.Button("Scale sprite 0.5x")){
			_activeSprite.scale = 0.5f;	
		}
		
		// resets the sprite scale
		if(GUILayout.Button("Reset Scale")){
			_activeSprite.scale = 1.0f;
		}
		
		// rotate sprite 45
		if(GUILayout.Button("Rotate sprite 45deg")){
			_activeSprite.rotation -= 45.0f;
		}
		
		// reset sprite rotation
		if(GUILayout.Button("Reset rotation")){
			_activeSprite.rotation = 0.0f;
		}
		
		
		// flip horizontally
		if(GUILayout.Button("Flip X")){
			_activeSprite.flipX = !_activeSprite.flipX;
		}
		// flip vertically
		if(GUILayout.Button("Flip Y")){
			_activeSprite.flipY = !_activeSprite.flipY;
		}
		
		
		GUILayout.EndScrollView();
		GUILayout.EndArea();
    }
}













	