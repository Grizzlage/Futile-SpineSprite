using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Spine;

public class GSpineSprite : FContainer {

	public Skeleton skeleton;
	public Spine.AnimationState animation;

	public List<GSpineSlot> slots = new List<GSpineSlot>();
	
	private bool _isSpineDirty = false;
	
	// construct a new sprite with the spine skeleton name
	public GSpineSprite(string name){
		if(GSpineManager.DoesContainSkeleton(name)){
			BuildSkeleton(GSpineManager.GetSkeletonByName(name));
		}else{
			Debug.Log("Skeleton " + name + " is not loaded");
		}
	}
	
	// construct a skeleton with a spine skeleton data object
	public GSpineSprite(SkeletonData skeletonData){
		BuildSkeleton(skeletonData);	
	}
	
	// build the sprite and slots with the skeleton data
	private void BuildSkeleton(SkeletonData skeletonData){
		
		skeleton = new Skeleton(skeletonData);
		skeleton.UpdateWorldTransform();
		
		// check if there are multiple skins loaded
		if(skeleton.Data.Skins.Count > 0){
			// load the first skin as default
			SetSkin(skeleton.Data.Skins[0].Name);
		}
		
		// build all the child sprites in the container, add them based on the slot draw order and create using the default slot/attachment
		for(int i = 0; i < skeleton.DrawOrder.Count; i++){
			GSpineSlot slot = new GSpineSlot(skeleton.DrawOrder[i]);
			slots.Add(slot);
			AddChild(slot);
		}
		
		AnimationStateData animationStateData = new AnimationStateData(skeleton.Data);
		animation = new Spine.AnimationState(animationStateData);
	}
	
	// the color of the sprite
	private Color _color = Color.white;
	public Color color {
		get {
			return _color;	
		}
		set {
			_color = value;
			// if we set a color to the entire sprite, then apply it to all slots in the sprite.
			for(int i = 0; i < skeleton.DrawOrder.Count; i++){
				slots[i].color = _color;
			}
		}
	}
	
	// finds a slot with the given name
	public GSpineSlot FindSlotByName(string name){
		for(int i = 0; i < slots.Count; i++){
			if(slots[i].name == name){
				return slots[i];
			}
		}
		return null;
	}
	
	
	// FCONTAINER SETUP ==========================================================================================
	override public void HandleAddedToStage(){
		Futile.instance.SignalUpdate += Update;
		base.HandleAddedToStage();
	}
	
	override public void HandleRemovedFromStage(){
		Futile.instance.SignalUpdate -= Update;
		base.HandleRemovedFromStage();
	}

	
	// ANIMATION WRAPPER ==========================================================================================
	public float animationTimeScale = 1.0f;				// the timescale to apply to the animation deltatime
	private float _prevAnimationTimeScale = 0.0f;		// used for pausing, this saves the current timescale
	private bool _isAnimationPaused = false;			// paused flag
	
	// sets the animation time stale
	public void SetAnimationTimeScale(float timeScale){
		animationTimeScale = timeScale;	
		_isAnimationPaused = false;
	}
	
	// checks to see if the animation name exists
	public bool DoesContainAnimation(string animationName){
		for(int i = 0; i < skeleton.Data.Animations.Count; i++){
			if(animationName == skeleton.Data.Animations[i].Name){
				return true;
			}
		}
		Debug.Log("Animation was not found - " + animation);
		return false;
	}
	
	// TODO: move this to the skeleton class, this should be set at the skeleton data level, not the individual sprite.
	public void SetAnimationMix(string fromAnimation, string toAnimation, float duration){
		if(animation != null){
			if(DoesContainAnimation(fromAnimation) && DoesContainAnimation(toAnimation)){ 
				animation.Data.SetMix(fromAnimation, toAnimation, duration);
			}
		}
	}
	
	// play the animation name, and if it should loop or not
	public void Play(string name, bool loop = true){
		if(DoesContainAnimation(name)){
			animation.SetAnimation(0, name, loop);
		}
	}
	
	// add the animation to the queue
	public void Queue(string name, bool loop = true, float delay = 0.0f){
		if(DoesContainAnimation(name)){
			animation.AddAnimation(0, name, loop, delay);
		}
	}
	
	// pause the current animation
	public void Pause(){
		if(!_isAnimationPaused){
			_isAnimationPaused = true;
			_prevAnimationTimeScale = animationTimeScale;
			animationTimeScale = 0.0f;
		}
	}
	
	// unpause the current animation
	public void Resume(){
		if(_isAnimationPaused){
			_isAnimationPaused = false;
			animationTimeScale = _prevAnimationTimeScale;
		}
	}
	
	// stop the current animation and reset to the base pose
	public void Stop(){

		animation.ClearTracks();
		skeleton.SetToSetupPose();

		_isSpineDirty = true;
	}
	
	// returns if the current animation is completed or not, this will return true for looped animations if the time has passed the duration of 1 single complete play.
	public bool IsComplete(){
		TrackEntry track = animation.GetCurrent(0);
		if(track != null){

			// time is more then the end time but looping is set to false
			// ** with looping set to true time can be larger then end time.
			if(track.time >= track.EndTime && !track.loop){
				return true;
			}
		}

		return false;
	}
	
	
	// SKINS ==========================================================================================
	
	// does this skeleton contain the skin name
	public bool DoesContainSkin(string skin){
		for(int i = 0; i < skeleton.Data.Skins.Count; i++){
			if(skin == skeleton.Data.Skins[i].Name){
				return true;
			}
		}
		Debug.Log("Skin was not found - " + skin);
		return false;
	}
	
	// sets the skeleton skin to the new skin
	public void SetSkin(string skin){
		if(DoesContainSkin(skin)){
			skeleton.SetSkin(skin);

			skeleton.SetToSetupPose();

			_isSpineDirty = true;
		}
	}
	
	// UPDATING ==========================================================================================
	public bool flipX {
		get { return skeleton.FlipX; }
		set { skeleton.FlipX = value; }
	}
	
	public bool flipY {
		get { return skeleton.FlipY; }
		set { skeleton.FlipY = value; }
	}
	
	// UPDATING ==========================================================================================
	
	// updates the sprite / skeleton
	public void Update(){

		TrackEntry track = animation.GetCurrent(0);

		// is the animation currently playing? then update the bone positions and slot data

		if(track != null){
			// only update if were looping, or if or time is less then the end time for non looping tracks.
			if(track.loop || (!track.loop && track.time <= track.EndTime)){
				animation.Update(Time.deltaTime * animationTimeScale);
				animation.Apply(skeleton);
				_isSpineDirty = true;
			}
		}
		
		// if the skeleton is dirty then refresh all the slots.
		if(_isSpineDirty){
			_isSpineDirty = false;
			skeleton.UpdateWorldTransform();
			
			for(int i = 0; i < skeleton.DrawOrder.Count; i++){
				slots[i].Update(skeleton.DrawOrder[i]);
			}	
		}
	}
	
	
	
}
