using UnityEngine;


namespace Game
{
    [CreateAssetMenu]
    public class PlayerState : ScriptableObject
    {
        [Header ("Run")]
        [SerializeField] private float playerMovementSpeed;
        public float runMaxSpeed;
        public float runAccelAmount;
        public float runDeccelAmount;
        
        [Space(15)]
        [Header ("Jump")]
        public AnimationCurve jumpForceCurveY;
        public float jumpForceMultiplier = 5f;
        [Tooltip("Для удобства создания кривой (ускорение идет на заданное значение)")]
        public float curveTimeMultiplier = 10f;

    }
}
