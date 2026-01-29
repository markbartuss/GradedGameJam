using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    public PlayerInventory inventory;
    public PlayerMovement movement;

    public Image slot1Icon;
    public Image slot2Icon;
    public GameObject slot1Highlight;
    public GameObject slot2Highlight;
    public Sprite jumpIcon;
    public Sprite dashIcon;
    public Sprite emptyIcon;
    void Start()
    {

    }


    void Update()
    {
        slot1Icon.sprite = GetSprite(inventory.slot1);
        slot2Icon.sprite = GetSprite(inventory.slot2);

        slot1Highlight.SetActive(movement.CurrentAbility == PlayerInventory.AbilityType.Jump // whichever ability is currently active is the one that is being highlighted
            && inventory.slot1 == PlayerInventory.AbilityType.Jump
            ||
            movement.CurrentAbility == PlayerInventory.AbilityType.Dash
            && inventory.slot1 == PlayerInventory.AbilityType.Dash)
        ;

        slot2Highlight.SetActive(movement.CurrentAbility == PlayerInventory.AbilityType.Jump 
            && inventory.slot2 == PlayerInventory.AbilityType.Jump
            ||
            movement.CurrentAbility == PlayerInventory.AbilityType.Dash
            && inventory.slot2 == PlayerInventory.AbilityType.Dash);
    }
    private Sprite GetSprite(PlayerInventory.AbilityType ability)
    {
        switch (ability)
        { 
            case PlayerInventory.AbilityType.Jump: return jumpIcon;
            case PlayerInventory.AbilityType.Dash: return dashIcon; 
            default: return emptyIcon; 
        }

    }
    

    
}

    

