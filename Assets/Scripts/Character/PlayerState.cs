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
        [SerializeField] private AnimationCurve jumpCurveY;

    }
}
