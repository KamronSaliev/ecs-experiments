using System;
using Unity.Mathematics;
using UnityEngine;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

namespace ECSExperiments.Input
{
    public class InputControlsAuthoring : MonoBehaviour
    {
        public event Action SelectionFinished;
        
        public bool IsSelectionActive => _isSelectionActive;

        public Rect CurrentSelectionRect { get; private set; }
        
        private float2 _selectionStartPosition;
        private bool _isSelectionActive;
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
            _isSelectionActive = true;
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
            
            CurrentSelectionRect = new Rect();
            _currentFinger = null;
            _isSelectionActive = false;
            SelectionFinished?.Invoke();
        }
    }
}