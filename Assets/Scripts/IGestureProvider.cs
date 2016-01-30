using UnityEngine;
using System.Collections;
using System;

public enum GestureType { 
	Left, 
	Right, 
	Up,
	None = -1
}

public interface IGestureProvider {

	Action<GestureType, int> EventGestureMade { get; set; }
}