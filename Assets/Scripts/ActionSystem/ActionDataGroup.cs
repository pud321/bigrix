using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDataGroup : IActionData
{
    public List<IActionData> individual_action_data;

    public ActionDataGroup()
    {
        individual_action_data = new List<IActionData>();
    }

    public void AddAction(IActionData data)
    {
        individual_action_data.Add(data);
    }

    public void RemoveAction(IActionData data)
    {
        individual_action_data.Remove(data);
    }

    public int damage { 
        get 
        {
            int temp = 0;

            foreach (IActionData data in individual_action_data)
            {
                temp += data.damage;
            }

            return temp;
        }
    }

    public float range
    {
        get
        {
            float temp = 0f;

            foreach (IActionData data in individual_action_data)
            {
                temp += data.range;
            }
            return temp;
        }
    }

    public float frequency
    {
        get
        {
            float temp = 0f;

            foreach (IActionData data in individual_action_data)
            {
                temp += data.frequency;
            }
            return temp;
        }
    }
}
