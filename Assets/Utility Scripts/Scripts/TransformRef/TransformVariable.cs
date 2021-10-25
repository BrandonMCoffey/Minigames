using UnityEngine;

namespace Assets.Utility.Scripts.TransformRef {
    [CreateAssetMenu]
    public class TransformVariable : ScriptableObject {
        public Vector3 Position;
        public Quaternion Rotation;
    }
}