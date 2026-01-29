using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public enum AbilityType { None, Jump, Dash }
    public bool hasJumpItem = false; 
    public bool hasDashItem = false;
    public AbilityType slot1 = AbilityType.None; 
    public AbilityType slot2 = AbilityType.None;

    public void AddAbility(AbilityType ability)
    {
        if (slot1 == ability || slot2 == ability)
        {
            return;
        }
        if (slot1 == AbilityType.None)
        {
            slot1 = ability;
        }
        else if (slot2 == AbilityType.None)
        {
            slot2 = ability;
        }

    }
}
