using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemInventoryController : ItemInventoryController
{
    private CharacterEnums character_type;

    public PlayerItemInventoryController(int inventory_size, int stack_capacity, CharacterEnums character_type) : base(inventory_size, stack_capacity)
    {
        this.character_type = character_type;
    }

    public override bool IsCompatible(HashSet<CharacterEnums> characters)
    {
        return characters.Contains(character_type);
    }
}
