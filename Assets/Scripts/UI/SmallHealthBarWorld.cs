using UnityEngine;
using UnityEngine.UI;

public class SmallHealthBarWorld : SmallHealthBar
{
    [SerializeField] private Image outer_bar_image;

    private Transform _camera_transform;
    protected Color outer_bar_color = new Color(0.8f, 0.8f, 0.8f, 0.1f);

    protected override void Awake()
    {
        base.Awake();
        _character_obj = GetComponentInParent<CharacterManager>();
        _camera_transform = Camera.main.transform;
        high_color = new Color(0.05f, 1f, 0.1f, 0.6f);
        low_color = new Color(1f, 0.05f, 0.1f, 0.6f);
    }

    protected override void Start()
    {
        base.Start();
        StartTracking();
        outer_bar_image.color = outer_bar_color;
    }

    protected override void SetBar(float bar_percent)
    {

        bool isActive = bar_percent < 1;
        bar_width_container.SetActive(isActive);

        if (isActive)
        {
            base.SetBar(bar_percent);
        }
    }

    private void Update()
    {
        this.transform.LookAt(_camera_transform);
    }

    private void StartTracking()
    {
        if (_character_obj == null)
        {
            _character_obj = GetComponentInParent<CharacterManager>();
            SetBarTracking();
        }
    }
}
