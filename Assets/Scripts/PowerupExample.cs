using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Spine;

public class PowerupExample : MonoBehaviour {

	private GSpineSprite sprite;
	
	private Vector2 _scrollPosition;
	
	void Start(){

		// Setup Futile
		FutileParams fparams = new FutileParams(true, true, true, true);
		fparams.AddResolutionLevel(1024.0f,	1.0f, 1.0f, "");
		fparams.origin = new Vector2(0.5f,0.5f);
		Futile.instance.Init(fparams);
		

		GSpineManager.LoadSpine("powerup", "spine/powerup/powerup-spine", "spine/powerup/powerup-atlas");
		sprite = new GSpineSprite("powerup");
		Futile.stage.AddChild(sprite);
		sprite.SetPosition((Futile.screen.halfWidth * 0.25f), -(Futile.screen.halfHeight * 0.5f));
		sprite.Play("animation");
	}
	
	
	void OnGUI() {
		GUI.skin.button.fixedWidth = 200;
		GUILayout.BeginArea(new Rect(0, 0, GUI.skin.button.fixedWidth + 25, Screen.height));
		_scrollPosition = GUILayout.BeginScrollView(_scrollPosition, GUILayout.Width(GUI.skin.button.fixedWidth + 25), GUILayout.Height(Screen.height));
		

		if(GUILayout.Button("Goblins Example")){
			Application.LoadLevel("GoblinsExample");
		}
		if(GUILayout.Button("Powerup Example")){
			Application.LoadLevel("PowerupExample");
		}
		if(GUILayout.Button("Spineboy Original Example")){
			Application.LoadLevel("SpineboyOriginalExample");
		}
		if(GUILayout.Button("Spineboy Spaceman Example")){
			Application.LoadLevel("SpineboySpacemanExample");
		}


		if(GUILayout.Button("Play Animation")){
			sprite.Play("animation");
		}
		if(GUILayout.Button("Pause Animation")){
			sprite.Pause();
		}
		if(GUILayout.Button("Resume Animation")){
			sprite.Resume();
		}
		if(GUILayout.Button("Stop Animation")){
			sprite.Stop();
		}
		

		if(GUILayout.Button("Slow Motion")){
			sprite.SetAnimationTimeScale(0.5f);
		}
		if(GUILayout.Button("Normal Speed")){
			sprite.SetAnimationTimeScale(1.0f);
		}


		if(GUILayout.Button("Tint Wings Red")){
			sprite.FindSlotByName("left wing").color = Color.red;
			sprite.FindSlotByName("right wing").color = Color.red;
		}
		if(GUILayout.Button("Tint Entire Sprite Blue")){
			sprite.color = Color.blue;
		}
		if(GUILayout.Button("Clear Tint")){
			sprite.color = Color.white;
		}


		if(GUILayout.Button("Scale sprite 2x")){
			sprite.scale = 2.0f;
		}
		if(GUILayout.Button("Scale sprite 0.5x")){
			sprite.scale = 0.5f;	
		}
		if(GUILayout.Button("Reset Scale")){
			sprite.scale = 1.0f;
		}
		

		if(GUILayout.Button("Rotate sprite 45deg")){
			sprite.rotation -= 45.0f;
		}
		if(GUILayout.Button("Reset rotation")){
			sprite.rotation = 0.0f;
		}
		

		if(GUILayout.Button("Flip X")){
			sprite.flipX = !sprite.flipX;
		}
		if(GUILayout.Button("Flip Y")){
			sprite.flipY = !sprite.flipY;
		}
		
		
		GUILayout.EndScrollView();
		GUILayout.EndArea();
    }
}













	