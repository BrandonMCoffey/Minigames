using UnityEngine;

namespace Assets.Utility.Scripts {
    public static class MouseUtils {
        public static Vector3 GetMousePosition2D()
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0f;
            return worldPosition;
        }
    }
}