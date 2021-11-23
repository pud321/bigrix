using UnityEngine;

public class ActiveCharacterInventory : CharacterInventory
{
    private ActionPrefabGenerator prefab_generator;

    protected void Awake()
    {
        base.Awake();
        prefab_generator = GetComponent<ActionPrefabGenerator>();
    }

    protected override PlayerCharacterData SelectCharacterObject(CharacterEnums type)
    {
        switch (type)
        {
            case CharacterEnums.Fighter:
                return new Fighter();
            case CharacterEnums.Mage:
                GameObject g = prefab_generator.InstantiateGameObject("MageBasicAttack", this.transform);
                return new Mage(g.GetComponent<ProjectileController>());
            case CharacterEnums.Priest:
                return new Priest();
            default:
                return null;
        }
    }

}
