using UnityEngine;
using System;
using System.Collections;
using Spine;

public class GSpineAttachmentLoader : AttachmentLoader{


	public RegionAttachment NewRegionAttachment (Skin skin, String name, String path){

		FAtlasElement element = Futile.atlasManager.GetElementWithName(name);		
		RegionAttachment attachment = new RegionAttachment(name);

		if(element != null){
			attachment.RendererObject = element;
			attachment.SetUVs(element.uvBottomLeft.x, element.uvBottomLeft.y, element.uvTopRight.x, element.uvTopRight.y, false);
			attachment.RegionOffsetX = 0.0f;
			attachment.RegionOffsetY = 0.0f;
			attachment.RegionWidth = element.sourceSize.x;
			attachment.RegionHeight = element.sourceSize.y;
			attachment.RegionOriginalWidth = element.sourceSize.x;
			attachment.RegionOriginalHeight = element.sourceSize.y;

		}else{
			attachment.RendererObject = null;
			attachment.SetUVs(0.0f, 0.0f, 0.0f, 0.0f, false);
			attachment.RegionOffsetX = 0.0f;
			attachment.RegionOffsetY = 0.0f;
			attachment.RegionWidth = 0.0f;
			attachment.RegionHeight = 0.0f;
			attachment.RegionOriginalWidth = 0.0f;
			attachment.RegionOriginalHeight = 0.0f;
			
			Debug.Log("Element [" + name + "] not found in Futile.AtlasManager");
		}
		return attachment;
	}

	/// <return>May be null to not load any attachment.</return>
	public MeshAttachment NewMeshAttachment (Skin skin, String name, String path){
		return null;
	}
	
	/// <return>May be null to not load any attachment.</return>
	public SkinnedMeshAttachment NewSkinnedMeshAttachment (Skin skin, String name, String path){
		return null;
	}
	
	/// <return>May be null to not load any attachment.</return>
	public BoundingBoxAttachment NewBoundingBoxAttachment (Skin skin, String name){
		return null;
	}
}