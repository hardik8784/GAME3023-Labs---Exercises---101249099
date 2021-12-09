using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : ICharacter
{
    [SerializeField]
    private EncounterInstance myEncounter;
    public void CastAbility(int Slot)
    {
        CastAbility(Slot, this, myEncounter.Enemy);
    }
    public override void TakeTurn(EncounterInstance Encounter)
    {
        myEncounter = Encounter;
        //throw new System.NotImplementedException();
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
