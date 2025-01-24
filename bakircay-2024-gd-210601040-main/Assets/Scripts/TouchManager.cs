using System;
using UnityEngine;

namespace Common
{
    public struct TouchData
    {
        public bool hasValue;
        public Vector2 position;
        public Vector2 deltaPosition;
    }

    public class TouchManager : MonoBehaviour
    {
        private static TouchManager _instance;

        public static TouchManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<TouchManager>();

                    if (_instance == null)
                    {
                        Debug.LogWarning("TouchManager not found in scene");
                    }
                }

                return _instance;
            }
        }

        public Action<TouchData> OnTouchBegan;
        public Action<TouchData> OnTouchMoved;
        public Action<TouchData> OnTouchEnded;

        private TouchData _currentTouchData = new TouchData() { hasValue = false };

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                TouchBegan(Input.mousePosition);
            }
            else if (Input.GetMouseButton(0))
            {
                if (!_currentTouchData.hasValue)
                {
                    TouchBegan(Input.mousePosition);
                    return;
                }

                TouchMoved(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (!_currentTouchData.hasValue)
                {
                    return;
                }

                TouchEnded(Input.mousePosition);
            }
        }

        private void TouchBegan(Vector2 position)
        {
            _currentTouchData = CreateTouchData(position, true);
            OnTouchBegan?.Invoke(_currentTouchData);
        }

        private void TouchMoved(Vector2 position)
        {
            _currentTouchData = CreateTouchData(position, true);
            OnTouchMoved?.Invoke(_currentTouchData);
        }

        private void TouchEnded(Vector2 position)
        {
            _currentTouchData = CreateTouchData(position, false);
            OnTouchEnded?.Invoke(_currentTouchData);
        }

        private TouchData CreateTouchData(Vector2 position, bool hasValue)
        {
            return new TouchData()
            {
                hasValue = hasValue,
                position = position,
                deltaPosition = hasValue ? position - _currentTouchData.position : Vector2.zero
            };
        }
    }
}
