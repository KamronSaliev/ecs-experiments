using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ECSExperiments.Selection
{
    public class SelectionHandler : MonoBehaviour
    {
        public event Action SelectionFinished = delegate { };

        public bool IsSelectionActive { get; private set; }

        public Rect CurrentSelectionRect { get; private set; } = Rect.zero;

        private float2 _selectionStartPosition;

        public void SetMovementMarker(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.phase == InputActionPhase.Started)
            {
                Debug.Log($"SetMovementMarker {callbackContext.ReadValue<Vector2>()}");
            }
        }
        
        public void HandleSelection(InputAction.CallbackContext callbackContext)
        {
            switch (callbackContext.phase)
            {
                case InputActionPhase.Started:
                    HandleSelectionStarted(callbackContext);
                    break;
                case InputActionPhase.Performed:
                    HandleSelectionPerformed(callbackContext);
                    break;
                case InputActionPhase.Canceled:
                    HandleSelectionFinished();
                    break;
            }
        }

        private void HandleSelectionStarted(InputAction.CallbackContext callbackContext)
        {
            _selectionStartPosition = callbackContext.ReadValue<Vector2>();
            IsSelectionActive = true;
        }

        private void HandleSelectionPerformed(InputAction.CallbackContext callbackContext)
        {
            var min = math.min(_selectionStartPosition, callbackContext.ReadValue<Vector2>()).xy;
            var max = math.max(_selectionStartPosition, callbackContext.ReadValue<Vector2>()).xy;

            CurrentSelectionRect = new Rect(min, max - min);
        }

        private void HandleSelectionFinished()
        {
            SelectionFinished?.Invoke();
            CurrentSelectionRect = new Rect();
            IsSelectionActive = false;
        }
    }
}