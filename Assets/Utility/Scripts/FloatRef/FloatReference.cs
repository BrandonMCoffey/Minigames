using System;

namespace Assets.Utility.Scripts.FloatRef {
    [Serializable]
    public class FloatReference {
        public bool UseConstant;
        public float ConstantValue;
        public FloatVariable Variable;

        public FloatReference()
        {
            UseConstant = true;
            ConstantValue = 0;
        }

        public FloatReference(float value)
        {
            UseConstant = true;
            ConstantValue = value;
        }

        public float Value => UseConstant ? ConstantValue : Variable.Value;

        public static implicit operator float(FloatReference reference)
        {
            return reference.Value;
        }

        public void SetValue(float value)
        {
            if (UseConstant) {
                ConstantValue = value;
            } else {
                Variable.Value = value;
            }
        }
    }
}