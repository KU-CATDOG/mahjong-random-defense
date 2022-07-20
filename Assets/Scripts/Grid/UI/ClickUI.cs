using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MRD
{
    [RequireComponent(typeof(UnityEngine.UI.GraphicRaycaster))]
    public class ClickUI : MonoBehaviour, IPointerClickHandler
    {
        private List<Action> actions = new();

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
        public void OnPointerClick(PointerEventData eventData)
        {
            foreach (var action in actions.ToArray() ) action();
        }
    }
}

