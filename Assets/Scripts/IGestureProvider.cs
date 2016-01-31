using UnityEngine;
using System.Collections;
using System;

public interface IGestureProvider {

	Action<EventType, int> Test { get; set; }
}