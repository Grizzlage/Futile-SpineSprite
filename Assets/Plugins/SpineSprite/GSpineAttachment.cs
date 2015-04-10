using UnityEngine;
using System.Collections;
using Spine;


// exted fsprite to allow spine attachments to control and update the element and verticies.
public class GSpineAttachment : FSprite {
	
	// the spine attachment 
	private RegionAttachment _attachment;

	float[] vertices = new float[8];
	
	// the name of the attachment
	public string name {
		get {
			if(_attachment != null){
				return _attachment.Name;
			}else{
				return null;
			}
		}
	}
	
	// constructor that uses a slot to build the attachment
	public GSpineAttachment(Slot slot) : base((slot.Attachment as RegionAttachment).RendererObject as FAtlasElement){
		Update(slot);
	}
	
	// this is the runtime tint color to mix with the attachment color.
	private Color _slotCustomColor = Futile.white;
	override public Color color {
		get {
			return _slotCustomColor;	
		}
		set {
			_slotCustomColor = value;
		}
	}
	
	// update the attachment with the current slot data
	public void Update(Slot slot){
		_attachment = slot.Attachment as RegionAttachment;

		//_attachment.UpdateOffset();
		//_attachment.UpdateVertices(slot.Bone);

		_attachment.ComputeWorldVertices(slot.Bone, vertices);

		element = _attachment.RendererObject as FAtlasElement;
		element = null;
		
		base.color = _slotCustomColor * new Color(slot.R, slot.G, slot.B, slot.A);
		
		UpdateLocalVertices();
	}
	
	// override the function to apply the attachment verticies to the sprites
	override public void UpdateLocalVertices(){
		base.UpdateLocalVertices();
		
		// bind the verticies from the spine attachment to the fsprite verticies
		if(_attachment != null){


			_localVertices[0].Set(vertices[RegionAttachment.X2], vertices[RegionAttachment.Y2]);
			_localVertices[1].Set(vertices[RegionAttachment.X3], vertices[RegionAttachment.Y3]);
			_localVertices[2].Set(vertices[RegionAttachment.X4], vertices[RegionAttachment.Y4]);
			_localVertices[3].Set(vertices[RegionAttachment.X1], vertices[RegionAttachment.Y1]);
		}
	}
}
