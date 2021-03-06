﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Juce.Feedbacks.Example7
{
    public class ButtonExample7 : MonoBehaviour, IPointerDownHandler
    {
        public event Action OnClick;

        public void OnPointerDown(PointerEventData pointerEventData)
        {
            OnClick?.Invoke();
        }
    }
}