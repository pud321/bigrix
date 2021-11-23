using UnityEngine;
using UnityEngine.UI;

public class ToggleGameobject : MonoBehaviour
{
    [SerializeField] public GameObject toggle_object;
    [SerializeField] private bool initial_state;

    private Button game_button;

    protected virtual void Awake()
    {
        game_button = GetComponent<Button>();
    }

    protected virtual void Start()
    {
        if (toggle_object == null) { return; }
        game_button.onClick.AddListener(Toggle);
        toggle_object.SetActive(initial_state);
    }

    public virtual void Toggle()
    {
        if (toggle_object == null) { return; }

        toggle_object.SetActive(!toggle_object.activeSelf);
    }
}
