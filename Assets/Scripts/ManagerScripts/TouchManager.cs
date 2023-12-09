using UnityEngine;
using UnityEngine.Events;

namespace Blended
{
	// Structure defining touch input details
    public struct TouchInput
    {
        public enum TouchPhase
        {
            Started,
            Moved,
            Ended,
            NotActive
        }

        public Vector2 FirstWorldPosition;
        public Vector2 WorldPosition;
        public Vector2 FirstScreenPosition;
        public Vector2 ScreenPosition;
        public Vector2 DeltaScreenPosition;
        public Vector2 DeltaWorldPosition;
        public TouchPhase Phase; // current touch phase
    }

    // Delegate for touch events
    public delegate void TouchEvent(TouchInput touch);

    public class TouchManager : MonoSingleton<TouchManager>
    {
        private TouchInput _touch; // current touch input

        public bool isActive = true;

        // Events for different touch phases
        public  TouchEvent onTouchBegan = null, onTouchMoved = null, onTouchEnded = null;

        // Unified action for any touch phase
        public UnityAction OnTouch;

        private void Update()
        {
            if (!isActive) return;

            ApplyTouchForCurrentPlatform(); // Apply touch logic based on the platform

            
            // Trigger events based on the current touch phase
            switch (_touch.Phase)
            {
                case TouchInput.TouchPhase.NotActive:
                    return;
                case TouchInput.TouchPhase.Started:
                    onTouchBegan?.Invoke(_touch);
                    if (OnTouch != null)
                        OnTouch();
                    break;
                case TouchInput.TouchPhase.Moved:
                    onTouchMoved?.Invoke(_touch);
                    break;
                case TouchInput.TouchPhase.Ended:
                    onTouchEnded?.Invoke(_touch);
                    break;
            }
        }

        // Platform-dependent touch input logic
        private void ApplyTouchForCurrentPlatform()
        {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
	        // Mouse input for Unity Editor or standalone Windows builds
            if (!Input.GetMouseButton(0) && !Input.GetMouseButtonUp(0))
            {
                _touch.Phase = TouchInput.TouchPhase.NotActive;
            }
            else
            {
                Vector2 screenPosition = Input.mousePosition;
                Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

                if (Input.GetMouseButtonDown(0))
                {
                    _touch.Phase = TouchInput.TouchPhase.Started;
                    _touch.FirstScreenPosition = screenPosition;
                    _touch.FirstWorldPosition = worldPosition;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    _touch.Phase = TouchInput.TouchPhase.Ended;
                }
                else
                {
                    _touch.Phase = TouchInput.TouchPhase.Moved;
                    _touch.DeltaScreenPosition = screenPosition - _touch.ScreenPosition;
                    _touch.DeltaWorldPosition = worldPosition - _touch.WorldPosition;
                }

                _touch.ScreenPosition = screenPosition;
                _touch.WorldPosition = worldPosition;
            }

#else
		 // Touch input for other platforms (mobile)

		if (Input.touchCount > 0)
		{
			var inputTouch = Input.GetTouch(0);
			
			var screenPosition = inputTouch.position;
			Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

			if (inputTouch.phase == TouchPhase.Began)
			{
				_touch.Phase = TouchInput.TouchPhase.Started;
				_touch.FirstScreenPosition = screenPosition;
				_touch.FirstWorldPosition = worldPosition;
	
				_touch.ScreenPosition = screenPosition;
				_touch.WorldPosition = worldPosition;
			}
			else if (inputTouch.phase == TouchPhase.Moved || inputTouch.phase == TouchPhase.Stationary)
			{
				_touch.Phase = TouchInput.TouchPhase.Moved;
				_touch.DeltaScreenPosition = screenPosition - _touch.ScreenPosition;
				_touch.DeltaWorldPosition = worldPosition - _touch.WorldPosition;
	
				_touch.ScreenPosition = screenPosition;
				_touch.WorldPosition = worldPosition;
			}
			else
			{
				_touch.Phase = TouchInput.TouchPhase.Ended;
			}
		}
		else
		{
			_touch.Phase = TouchInput.TouchPhase.NotActive;
		}

#endif
        }
    }
}