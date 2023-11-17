using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ECSExperiments.Input
{
    public class InputHandler : MonoBehaviour
    {
        public event Action SelectionFinished = delegate { };
        public event Action<float3> MovementMarkerSet = delegate { };

        public bool IsSelectionActive { get; private set; }
        public Rect SelectionRect { get; private set; } = Rect.zero;

        public float3 MovementMarkerPosition { get; private set; }

        private float2 _selectionStartPosition;

        public void SetMovementMarker(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.phase == InputActionPhase.Started)
            {
                var input = callbackContext.ReadValue<Vector2>();
                MovementMarkerPosition = new float3(input, 0.0f);
                MovementMarkerSet.Invoke(MovementMarkerPosition);
                Debug.Log($"SetMovementMarker {MovementMarkerPosition}");
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

            SelectionRect = new Rect(min, max - min);
        }

        private void HandleSelectionFinished()
        {
            SelectionFinished.Invoke();
            SelectionRect = new Rect();
            IsSelectionActive = false;
        }
    }
}