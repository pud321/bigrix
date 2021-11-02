using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmallHealthBar : MonoBehaviour
{

    [SerializeField] GameObject health_bar;
    [SerializeField] GameObject bar_full_container;

    private RectTransform _inner_bar;
    private Image _inner_bar_color;
    private AbstractCharacter _character_obj;
    private Transform _camera_transform;
    private float _initial_width;

    private Color high_color = new Color(0.05f, 1f, 0.1f);
    private Color low_color = new Color(1f, 0.05f, 0.1f);

    private void Awake()
    {
        _inner_bar = health_bar.GetComponent<RectTransform>();
        _inner_bar_color = health_bar.GetComponent<Image>();
        _character_obj = GetComponentInParent<AbstractCharacter>();
        _camera_transform = Camera.main.transform;
    }

    private void Start()
    {
        _initial_width = _inner_bar.sizeDelta.x;
        _character_obj.OnCharacterHealth += _SetBarRaw;
        _SetBar(_character_obj.health_percent);
    }

    private void _SetBar(float bar_percent)
    {
        bool isActive = bar_percent < 1;
        bar_full_container.SetActive(isActive);

        if (isActive)
        {
            float current_width = Mathf.Lerp(0f, _initial_width, bar_percent);
            Color lerp_color = Color.Lerp(low_color, high_color, bar_percent);
            _inner_bar.sizeDelta = new Vector2(current_width, _inner_bar.sizeDelta.y);
            _inner_bar_color.color = lerp_color;
        }
    }

    private void _SetBarRaw(object o, DamageEventArgs e)
    {
        _SetBar(_character_obj.health_percent);
    }

    private void Update()
    {
        this.transform.LookAt(_camera_transform);
    }
}
