using UnityEngine;
using UnityEngine.UI;

public class SmallHealthBar : MonoBehaviour, ICharacterTracker
{

    [SerializeField] private GameObject health_bar;
    [SerializeField] protected GameObject bar_width_container;

    protected CharacterManager _character_obj;

    private RectTransform _inner_bar;
    private RectTransform _outer_bar;

    private Image _inner_bar_color;
    private float _initial_width;

    private Color high_color = new Color(0.05f, 1f, 0.1f);
    private Color low_color = new Color(1f, 0.05f, 0.1f);

    protected virtual void Awake()
    {
        _inner_bar = health_bar.GetComponent<RectTransform>();
        _outer_bar = bar_width_container.GetComponent<RectTransform>();
        _inner_bar_color = health_bar.GetComponent<Image>();
    }

    protected virtual void Start()
    {
        _initial_width = _outer_bar.sizeDelta.x;

    }

    protected virtual void SetBar(float bar_percent)
    {
        if (_initial_width == 0f)
        {
            _initial_width = _outer_bar.sizeDelta.x;
        }

        float current_width = Mathf.Lerp(0f, _initial_width, bar_percent);
        Color lerp_color = Color.Lerp(low_color, high_color, bar_percent);
        _inner_bar.sizeDelta = new Vector2(current_width, _inner_bar.sizeDelta.y);
        _inner_bar_color.color = lerp_color;
    }

    private void SetBarRaw(object o, DamageEventArgs e)
    {
        SetBar(_character_obj.health_percent);
    }

    public void SetTracking(CharacterManager tracked_character)
    {
        _character_obj = tracked_character;
        SetBarTracking();
    }

    protected void SetBarTracking()
    {
        _character_obj.OnCharacterHealth += SetBarRaw;
        SetBar(_character_obj.health_percent);
    }

}
