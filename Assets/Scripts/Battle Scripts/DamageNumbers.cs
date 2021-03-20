using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageNumbers : MonoBehaviour
{
    // NEED TO INSTANTIATE OBJECT THEN SET THE DAMAGETODISPLAY IN SAME FUNCTION
    public List<Animator> animators;
    public List<TextMeshProUGUI> numberDisplay;
    public int damageToDisplay;                         // If Damage was taken with a number value
    public string noDamageTakenDisplay;                 // For 0 damage, dodge, block, etc
    public List<char> damageAsChar;

    private void Start()
    {
        noDamageTakenDisplay = "Missed";
        numberDisplay = new List<TextMeshProUGUI>(GetComponentsInChildren<TextMeshProUGUI>());
        animators = new List<Animator>(GetComponentsInChildren<Animator>());
        DamageAnimation();
    }
    private void DamageAnimation()
    {
        if(damageToDisplay != 0)
        {
            string a = damageToDisplay.ToString();                   // Change number to string
            for(int i = 0; i < a.Length; i++)
            {
                damageAsChar[i] = a[i];                              // Split into component characters
            }
            for(int i = 0; i < numberDisplay.Count; i++)
            { 
                numberDisplay[i].text = damageAsChar[i].ToString();  // Enter component characters into their displays
            }
        }
        else if(noDamageTakenDisplay != "")
        {
            for(int i = 0; i < noDamageTakenDisplay.Length; i++)
            {
                numberDisplay[i].text = noDamageTakenDisplay[i].ToString();
            }
        }
        StartCoroutine(DoAnimation());
    }
    IEnumerator DoAnimation()
    {
        for(int i = 0; i < animators.Count; i++)
        {
            animators[i].SetTrigger("DoAnimation");
            yield return new WaitForSeconds(0.02f);
        }
        Destroy(this.gameObject, 1.5f);
    }
}
