using UnityEngine;
using System;
using System.Collections;
using Spine;

public class GSpineAttachmentLoader : AttachmentLoader{
	
	// this is the attachment loader which will hook into Futile's AtlasManager for the attachment/element data 
	public Attachment NewAttachment(Skin skin, AttachmentType type, String name){
		switch(type){
			case AttachmentType.region:
				
				FAtlasElement element = Futile.atlasManager.GetElementWithName(name);		
				RegionAttachment attachment = new RegionAttachment(name);
			
				if(element != null){
					attachment.Texture = element;
					attachment.SetUVs(element.uvBottomLeft.x, element.uvBottomLeft.y, element.uvTopRight.x, element.uvTopRight.y, false);
					attachment.RegionOffsetX = 0.0f;
					attachment.RegionOffsetY = 0.0f;
					attachment.RegionWidth = element.sourceSize.x;
					attachment.RegionHeight = element.sourceSize.y;
					attachment.RegionOriginalWidth = element.sourceSize.x;
					attachment.RegionOriginalHeight = element.sourceSize.y;
				
				}else{
					attachment.Texture = null;
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
		throw new Exception("Unknown attachment type: " + type);
	}
}