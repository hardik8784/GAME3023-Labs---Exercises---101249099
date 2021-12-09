

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterUI : MonoBehaviour
{
    [SerializeField]
    private EncounterInstance Encounter;

    [SerializeField]
    private GameObject AbilitiesPanel;

    [SerializeField]
    private TMPro.TextMeshProUGUI EncounterTextBox;


    [SerializeField]
    private float TextSecondsPerCharacter = 0.2f;

    private IEnumerator AnimateTextCoroutine_ = null;

    // Start is called before the first frame update
    void Start()
    {
        AnimateTextCoroutine_ = AnimateTextCoroutine("You Have Encountered a: " + "Enemy", TextSecondsPerCharacter);
        StartCoroutine(AnimateTextCoroutine_);
        //StopCoroutine(AnimateTextCoroutine_);
        Encounter.OnTurnBegin.AddListener(AnnounceTurnBegin);
    }

    void AnnounceTurnBegin(ICharacter WhosTurn)
    {
        if (AnimateTextCoroutine_ != null)
        {
            StopCoroutine(AnimateTextCoroutine_);
        }

        AnimateTextCoroutine_ = AnimateTextCoroutine("It is : " + WhosTurn.name + "'s Turn", TextSecondsPerCharacter);
        StartCoroutine(AnimateTextCoroutine_);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator AnimateTextCoroutine(string message, float SecondsPerCharacter = 0.1f)
    {
        AbilitiesPanel.SetActive(false);
        EncounterTextBox.text = "";

        for (int Character = 0; Character < message.Length; Character++)
        {
            EncounterTextBox.text += message[Character];
            yield return new WaitForSeconds(SecondsPerCharacter);
        }

        AbilitiesPanel.SetActive(true);
        AnimateTextCoroutine_ = null;
    }
}
