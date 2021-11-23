public class SmallFixedHealthBar : GeneralBar, ICharacterDataTracker
{
    private PlayerCharacterData player_data;

    protected override void SetText()
    {
        bar_text.text = "HP: " + player_data.max_health.ToString() + "/" + player_data.max_health.ToString();
    }

    public void SetPlayerData(PlayerCharacterData tracked_character)
    {
        player_data = tracked_character;
        SetBar(1);
    }
}
