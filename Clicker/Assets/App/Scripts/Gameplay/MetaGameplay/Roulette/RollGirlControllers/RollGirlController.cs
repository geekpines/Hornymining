using DragonBones;
using UnityEngine;

public class RollGirlController : MonoBehaviour
{
    [field: SerializeField] public UnityArmatureComponent ArmatureComponent { get; private set; }

    private void OnEnable()
    {
        ArmatureComponent.animation.Play("stand");
    }

    public void RollEnable()
    {
        ArmatureComponent.animation.Play("rolling");
    }

    public void PlaySad()
    {
        ArmatureComponent.animation.Play("sad");
    }

    public void PlayHappy()
    {
        ;
        if(Random.Range(1, 100) < 50)
        {
            ArmatureComponent.animation.Play("happy");
        }
        else
        {
            ArmatureComponent.animation.Play("smile");
        }
    }
}
