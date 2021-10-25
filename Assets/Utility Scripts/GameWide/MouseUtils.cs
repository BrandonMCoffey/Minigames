using UnityEngine;
using UnityEngine.EventSystems;

namespace Utility_Scripts.GameWide
{
    public static class MouseUtils
    {
        public static Vector3 GetMousePosition2D()
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0f;
            return worldPosition;
        }

        public static bool IsMouseOverUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
    }
}