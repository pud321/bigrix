using UnityEngine;
using UnityEngine.UI;

public class CharacterBoxAppearance : MonoBehaviour, ICharacterTracker<CharacterManager>
{
    private Image box_background;
    private Color alive_color = new Color(0.95f, 0.95f, 0.95f);
    private Color dead_color = new Color(1f, 0.05f, 0.1f);

    protected virtual void Awake()
    {
        box_background = GetComponent<Image>();
    }

    protected void Start()
    {
        box_background.color = alive_color;
    }

    public void SetTracking(CharacterManager tracked_character)
    {
        tracked_character.OnCharacterHealth += GetObjectDamage;
        tracked_character.OnDeathGeneral += GetObjectChange;
    }

    private void GetObjectDamage(object o, DamageEventArgs e)
    {

    }

    public void GetObjectChange(CharacterManager character_object)
    {
        if (character_object.health_percent <= 0f)
        {
            box_background.color = dead_color;
        }
    }
}
