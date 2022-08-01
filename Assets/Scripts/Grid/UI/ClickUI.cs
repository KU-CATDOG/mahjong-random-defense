using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MRD
{
    [RequireComponent(typeof(GraphicRaycaster))]
    public class ClickUI : MonoBehaviour, IPointerClickHandler
    {
        private readonly List<Action> actions = new();

        public void OnPointerClick(PointerEventData eventData)
        {
            foreach (var action in actions.ToArray()) action();
        }

        public void AddListener(Action action)
        {
            actions.Add(action);
        }

        public void ClearListener()
        {
            actions.Clear();
        }

        public void AddListenerOnly(Action action)
        {
            actions.Clear();
            actions.Add(action);
        }
    }
}
