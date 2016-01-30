using UnityEngine;
using System.Collections;
using System;

public interface IGestureProvider {

	Action<GestureType, int> Test { get; set; }
}