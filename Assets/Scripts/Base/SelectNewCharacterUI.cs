using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public delegate void SendCharacterTypeEventHander(CharacterEnums type);

public class SelectNewCharacterUI : MonoBehaviour
{
    [SerializeField] private GameObject menu_prefab;
    [SerializeField] private GameObject button_prefab;
    [SerializeField] private Transform canvas_transform;

    public event SendCharacterTypeEventHander OnCreateCharacter;

    private GameObject instantiated_menu;
    private GameObject button_menu;
    private Button exit_button;

    private void Awake()
    {
        instantiated_menu = Instantiate(menu_prefab, canvas_transform);
        button_menu = instantiated_menu.GetComponentInChildren<VerticalLayoutGroup>().gameObject;
        exit_button = instantiated_menu.GetComponentInChildren<Button>();

        CloseCharacterMenu();
        
        GameObject instantiated_prefab;

        foreach (CharacterEnums type in GameStats.player_character_types)
        {
            instantiated_prefab = Instantiate(button_prefab, button_menu.transform);
            instantiated_prefab.GetComponentInChildren<Text>().text = type.ToString();
            instantiated_prefab.GetComponentInChildren<Button>().onClick.AddListener(() => RunCreateCharacter(type));
        }
    }

    private void Start()
    {
        exit_button.onClick.AddListener(CloseCharacterMenu);
    }

    public void OpenCharacterMenu()
    {
        instantiated_menu.SetActive(true);
    }

    public void CloseCharacterMenu()
    {
        instantiated_menu.SetActive(false);
    }

    private void RunCreateCharacter(CharacterEnums type)
    {
        OnCreateCharacter?.Invoke(type);
        CloseCharacterMenu();
    }
}

