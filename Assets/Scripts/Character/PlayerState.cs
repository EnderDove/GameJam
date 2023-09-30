using UnityEngine;


namespace Game
{
    [CreateAssetMenu]
    public class PlayerState : ScriptableObject
    {
        [SerializeField] private float playerMovementSpeed;
        [SerializeField] private AnimationCurve jumpCurveY;

    }
}
