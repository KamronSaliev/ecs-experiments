using System;
using Unity.Mathematics;
using UnityEngine;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

namespace ECSExperiments.Selection
{
    public class Selection : MonoBehaviour
    {
        public event Action SelectionStarted;
        public event Action SelectionFinished;

        public bool IsSelectionActive { get; private set; }

        public Rect CurrentSelectionRect { get; private set; }

        private float2 _selectionStartPosition;
        private EnhancedTouch.Finger _currentFinger;

        private void OnEnable()
        {
            EnhancedTouch.EnhancedTouchSupport.Enable();
            EnhancedTouch.Touch.onFingerDown += OnFingerDown;
            EnhancedTouch.Touch.onFingerMove += OnFingerMove;
            EnhancedTouch.Touch.onFingerUp += OnFingerUp;
        }

        private void OnDisable()
        {
            EnhancedTouch.EnhancedTouchSupport.Disable();
            EnhancedTouch.Touch.onFingerDown -= OnFingerDown;
            EnhancedTouch.Touch.onFingerMove -= OnFingerMove;
            EnhancedTouch.Touch.onFingerUp -= OnFingerUp;
        }

        private void OnFingerDown(EnhancedTouch.Finger finger)
        {
            if (_currentFinger != null)
            {
                return;
            }

            Debug.Log($"OnFingerDown: {finger.screenPosition}");

            _currentFinger = finger;
            _selectionStartPosition = finger.screenPosition;
            IsSelectionActive = true;
            SelectionStarted?.Invoke();
        }

        private void OnFingerMove(EnhancedTouch.Finger finger)
        {
            if (_currentFinger != finger)
            {
                return;
            }

            var min = math.min(_selectionStartPosition, _currentFinger.screenPosition).xy;
            var max = math.max(_selectionStartPosition, _currentFinger.screenPosition).xy;
            CurrentSelectionRect = new Rect(min, max - min);
        }

        private void OnFingerUp(EnhancedTouch.Finger finger)
        {
            if (_currentFinger != finger)
            {
                return;
            }

            Debug.Log($"OnFingerUp: {finger.screenPosition}");

            SelectionFinished?.Invoke();
            CurrentSelectionRect = new Rect();
            _currentFinger = null;
            IsSelectionActive = false;
        }
    }
}