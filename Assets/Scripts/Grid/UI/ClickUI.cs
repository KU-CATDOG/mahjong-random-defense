using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MRD
{
    [RequireComponent(typeof(GraphicRaycaster))]
    public class ClickUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler /*IPointerClickHandler*/
    {
        private readonly List<Action> actions = new();
        private Grid grid;
        public bool isDown = false;

       /* public void OnPointerClick(PointerEventData eventData)
        {
            foreach (var action in actions.ToArray()) action();
        }*/

        public void OnPointerDown(PointerEventData eventData)
        {
            isDown = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isDown = false;
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
