using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterUI : MonoBehaviour
{
    [SerializeField]
    private GameObject AbilitiesPanel;

    [SerializeField]
    private TMPro.TextMeshProUGUI EncounterText;

    private IEnumerator AnimateText = null;

    [SerializeField]
    private float AnimationSeconds = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        IEnumerator AnimateText = AnimateTextCoroutine("You have encountered a:" + " Enemy", AnimationSeconds);
        StartCoroutine(AnimateText);
        //StopCoroutine(AnimateText);
    }

    IEnumerator AnimateTextCoroutine(string message,float AnimationSeconds=0.1f)
    {
        AbilitiesPanel.SetActive(false);
        EncounterText.text = " ";
        for (int character =0; character< message.Length;character++)
        {
            EncounterText.text += message[character];
            yield return new WaitForSeconds(AnimationSeconds);
        }
        AbilitiesPanel.SetActive(true);
        AnimateText = null;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
