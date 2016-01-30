using UnityEngine;
using System.Collections;
using System;

public enum GestureType { 
	Left, 
	Right, 
	Up 
}

public interface IGestureProvider {

	Action<GestureType, int> Test { get; set; }
}