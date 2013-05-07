using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Spine;

public class GSpineSlot : FContainer {
	
	public GSpineSlot(Slot slot){
		Update(slot);
	}
	
	private Slot _slot = null; 						// stores the current slot data	
	private GSpineAttachment _attachment = null; 	// the current attachment on this slot
	
	public string name { // stores the slot name
		get {
			if(_slot != null){
				return _slot.Data.Name;
			}else{
				return null;
			}
		}
	}
	
	// the color to apply to the attachment on this slot.
	private Color _color = Color.white;
	public Color color {
		get {
			return _color;	
		}
		set {
			_color = value;
			if(_attachment != null){
				_attachment.color = _color;
			}
		}
	}
	
	// Update the slot details
	public void Update(Slot slot){
		_slot = slot;
		
		// slot data has an attachment
		if(_slot.Attachment != null){
		
			// we already have an attachment, make sure it's visible and update with new slot data.
			if(_attachment != null){
				_attachment.isVisible = true;
				_attachment.Update(_slot);
				
			// else we do not have an attachment already set
			}else{
				_attachment = new GSpineAttachment(_slot);
				AddChild(_attachment);
			}
			
		// slot is empty
		}else{
			
			// we currently have an attachment, so hide it
			if(_attachment != null){
				_attachment.isVisible = false;
			}
		}
	}
	
}