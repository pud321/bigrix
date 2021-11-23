using UnityEngine;
using UnityEngine.UI;

public abstract class GeneralBar : MonoBehaviour
{
    [SerializeField] private GameObject inner_bar;
    [SerializeField] protected GameObject bar_width_container;
    [SerializeField] protected Text bar_text;

    private RectTransform _inner_bar;
    private RectTransform _outer_bar;

    private Image _inner_bar_color;
    private float _initial_width;
    protected Color high_color;
    protected Color low_color;


    protected virtual void Awake()
    {
        _inner_bar = inner_bar.GetComponent<RectTransform>();
        _outer_bar = bar_width_container.GetComponent<RectTransform>();
        _inner_bar_color = inner_bar.GetComponent<Image>();
        SetDefaultColors();
    }

    protected virtual void SetDefaultColors()
    {
        high_color = new Color(0.05f, 1f, 0.1f, 1f);
        low_color = new Color(1f, 0.05f, 0.1f, 1f);
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

        if (bar_text != null)
        {
            SetText();
        }
    }

    protected abstract void SetText();
}
