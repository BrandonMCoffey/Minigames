using UnityEngine;

namespace Slime.Scripts.Slime
{
    [CreateAssetMenu]
    public class SlimeData : ScriptableObject
    {
        [Header("Slime Settings")]
        public string Name = "Slime";
        public Color Color = Color.blue;
        public float Damage = 1f;
        public float Size = 0.5f;

        [Header("Movement Settings")]
        public Vector2 IdleTimeMinMax = new Vector2(2, 4);
        public Vector2 JumpRadiusMinMax = new Vector2(1, 2);
        public float JumpAnticipationTime = 0.2f;
        public float IdleAnimationTime = 0.4f;

        [Header("Extra Movement Settings")]
        public float JumpHeightMultiplier = 0.5f;
        public float AirTimeMultiplier = 0.7f;
        public float ShadowSizeMultiplier = 0.5f;

        [Header("Animation Sprites")]
        public Sprite Idle = null;
        public Sprite AnticipateJump = null;
        public Sprite Jumping = null;
        public Sprite Falling = null;
        public Sprite Landing = null;
    }
}