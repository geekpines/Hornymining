using DragonBones;
using UnityEngine;

public class RollGirlController : MonoBehaviour
{
    [field: SerializeField] public UnityArmatureComponent ArmatureComponent { get; private set; }

    private void OnEnable()
    {
        ArmatureComponent.animationName = "stand";
    }

    public void RollEnable()
    {
        ArmatureComponent.animationName = "rolling";
    }


}
