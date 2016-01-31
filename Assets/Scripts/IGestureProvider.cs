using UnityEngine;
using System.Collections;
using System;
//
//*public enum GestureEvent { 
//	Left, 
//	Right, 
//	Up,
//	None = -1
//*/
//
//ublic interface IGestureProvider {
//
//	Action<EventType, int> EventGestureMade { get; set; }
//


public interface IGestureProvider
{
	Action<PSMoveEvent> OnGesture {get; set;}
}