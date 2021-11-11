using UnityEngine;

public class SmallHealthBarWorld : SmallHealthBar
{
    private Transform _camera_transform;

    protected override void Awake()
    {
        base.Awake();
        _character_obj = GetComponentInParent<CharacterManager>();
        _camera_transform = Camera.main.transform;
    }

    protected override void Start()
    {
        base.Start();
        StartTracking();
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
