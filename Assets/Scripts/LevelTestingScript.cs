using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelTestingScript : MonoBehaviour
{
    public TextMeshProUGUI text;
    public int level;
    public int totalXP;
    public int nextLvlXP;
    public int baseXP = 15;
    public float exponential = 2.3f;

    void Update()
    {
        Reset();
        addXP();
        NextLevel();

        text.text = "Level: " + level 
            + "\n"+ "TotalXP: " + totalXP
            + "\n"+ "NextLvlXP: " + nextLvlXP
            + "\n"+ "Difference: " + (nextLvlXP - totalXP);

    }

    private void NextLevel()
    {
        nextLvlXP = (int)(baseXP * Mathf.Pow(level, exponential) + (baseXP * level));
        if(totalXP >= nextLvlXP)
        {
            level++;
        }
    }
    private void addXP()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            totalXP = nextLvlXP;
        }
    }
    private void Reset()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            totalXP = 0;
            level = 0;
        }
    }
}
