﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JigiJumper.Utils {
    public class EventSystemUtility {
        PointerEventData _eventData;
        List<RaycastResult> _results;
        EventSystem _eventSystem;

        public EventSystemUtility(EventSystem eventSystem) {
            _eventSystem = eventSystem;
            _eventData = new PointerEventData(eventSystem);
            _results = new List<RaycastResult>();
        }

        public bool IsPointerOverUiObject(Vector2 inputs) {
            if (!(0 < inputs.x && inputs.x < Screen.width && 0 < inputs.y && inputs.y < Screen.height))
                return true;
            _results.Clear();
            _eventData.position = inputs;
            _eventSystem.RaycastAll(_eventData, _results);
            return _results.Count > 0;
        }
    }
}