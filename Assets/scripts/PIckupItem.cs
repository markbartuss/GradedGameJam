using UnityEngine;
using static PlayerInventory;

public class PIckupItem : MonoBehaviour
{
    public enum ItemType { JumpUnlock, DashUnlock }
    public ItemType itemType;

    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory inv = other.GetComponent<PlayerInventory>();
        if (inv == null) return;

        switch (itemType)
        {
            case ItemType.JumpUnlock:
            {
                    inv.AddAbility(AbilityType.Jump);
                    inv.hasJumpItem = true;
                    break;
            }

            case ItemType.DashUnlock:
            {
                inv.AddAbility(AbilityType.Dash);
                inv.hasDashItem = true;
                break;
            }
                
        }
        Destroy(gameObject);
    }
}
