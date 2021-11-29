using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTimerUI : MonoBehaviour
{
    [SerializeField] private RectTransform box_progress;
    private Text countdown_number;
    private Outline box_highlight;

    private ISkill skill;

    private Color highlight_text_color = Color.green;
    private Color standard_color = Color.black;
    private Color outline_color = Color.yellow;

    private float height_max;
    private float width_constant;

    private bool update_hold;
    private bool isRunning;
    private float run_time_remaining;
    private readonly float run_time_max = 1f;

    private void Awake()
    {
        height_max = GetComponent<RectTransform>().sizeDelta.y;
        width_constant = GetComponent<RectTransform>().sizeDelta.x;
        box_progress.sizeDelta = new Vector2(width_constant, height_max);

        box_highlight = GetComponent<Outline>();
        box_highlight.effectColor = outline_color;
        box_highlight.enabled = false;

        countdown_number = GetComponentInChildren<Text>();
        countdown_number.color = standard_color;

        isRunning = false;
    }

    public void SetSkill(ISkill skill)
    {
        this.skill = skill;
        this.skill.OnSkillRan += SkillRan;
        UpdateText();

        update_hold = skill.timeRemaining >= 0f;
    }

    private void Update()
    {
        if (skill != null && update_hold)
        {
            UpdatePercentTimer();
            UpdateText();
        }

        if (isRunning)
        {
            run_time_remaining -= Time.deltaTime;

            if (run_time_remaining < 0)
            {
                box_highlight.enabled = false;
                isRunning = false;
            }
        }
    }

    private void SkillRan()
    {
        isRunning = true;
        run_time_remaining = run_time_max;
        box_highlight.enabled = true;
        update_hold = true;
    }

    private void UpdateText()
    {

        float time_remaining = skill.timeRemaining;

        if (time_remaining < 0f)
        {
            update_hold = false;
            time_remaining = 0f;
        }

        countdown_number.text = time_remaining.ToString("0.#");

        if (time_remaining < 3f)
        {
            countdown_number.color = highlight_text_color;
        }
        else
        {
            countdown_number.color = standard_color;
        }
    }

    private void UpdatePercentTimer()
    {
        float lerp_value = Mathf.Lerp(0, height_max, skill.percent_time_remaining);
        box_progress.sizeDelta = new Vector2(width_constant, lerp_value);
    }
}
