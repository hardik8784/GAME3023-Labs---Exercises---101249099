using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacter : ICharacter
{
    public override void TakeTurn(EncounterInstance Encounter)
    {
        Debug.Log("AI Turn!");
        // Encounter.AdvanceTurns();
        CastAbility(Random.Range(0, Abilities.Length), this, Encounter.Player);
        //CastAbility(0, this, Encounter.Player);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
