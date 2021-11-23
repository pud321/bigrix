public class SmallHealthBar : GeneralBar, ICharacterTracker<CharacterManager>
{
    protected CharacterManager _character_obj;

    private void SetBarEvent(object o, DamageEventArgs e)
    {
        SetBar(_character_obj.health_percent);
    }

    protected void SetBarTracking()
    {
        _character_obj.OnCharacterHealth += SetBarEvent;
        SetBar(_character_obj.health_percent);
    }

    protected override void SetText()
    {
        bar_text.text = "HP: " + _character_obj.current_health.ToString() + "/" + _character_obj.character_data.max_health.ToString();
    }

    public void SetTracking(CharacterManager tracked_character)
    {
        _character_obj = tracked_character;
        SetBarTracking();
    }
}
