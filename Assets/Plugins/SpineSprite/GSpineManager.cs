using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Spine;

public class GSpineManager {
	
	public GSpineManager(){
	}
	
	// static dictionary of all the skeleton data
	private static Dictionary<string, SkeletonData> _skeletons = new Dictionary<string, SkeletonData>();
	
	// the futile specific attachment loader
	private static GSpineAttachmentLoader _attachmentLoader = new GSpineAttachmentLoader();
	
	// this actually loads the spine json file into a skeletondata object, then adds it to the dictionary
	public static void LoadSpine(string name, string jsonPath){
		if(!DoesContainSkeleton(name)){
			FileInfo info = new FileInfo("assets/resources/" + jsonPath + Futile.resourceSuffix + ".txt");
		    if(info == null || info.Exists == false){
				Debug.Log("Could not load Skeleton, file " + jsonPath + Futile.resourceSuffix + " not found.");
				return;
			}
			
			SkeletonJson json = new SkeletonJson(_attachmentLoader);
			SkeletonData skeletonData = json.ReadSkeletonData("assets/resources/" + jsonPath + Futile.resourceSuffix + ".txt");
			if(skeletonData != null){
				_skeletons.Add(name, skeletonData);
			}else{
				Debug.Log("Could not load Skeleton Data");	
			}
		}else{
			Debug.Log("Skeleton already exists for name [" + name + "]");	
		}
	}
	
	// sugar to allow you to load a spine json file and futile atlas at the same time.
	public static void LoadSpine(string name, string jsonPath, string atlasPath){
		Futile.atlasManager.LoadAtlas(atlasPath);
		LoadSpine(name, jsonPath);
	}
	
	// check if the skeleton name exists in the dictionary
	public static bool DoesContainSkeleton(string name){
		if(_skeletons.ContainsKey(name)){
			return true;
		}
		return false;
	}
	
	// gets the skeletondata object for the skeleton name
	public static SkeletonData GetSkeletonByName(string name){
		if(_skeletons.ContainsKey(name)){
			return _skeletons[name];
		}
		return null;
	}
}

