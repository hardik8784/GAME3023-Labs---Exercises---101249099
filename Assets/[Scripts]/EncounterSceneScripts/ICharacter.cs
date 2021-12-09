

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ICharacter : MonoBehaviour
{
    [SerializeField]
    protected Ability[] Abilities;

    private EncounterInstance Encounter;
    public UnityEvent<Ability> OnAbilityCast;
    public void CastAbility(int AbilitySlot, ICharacter Self, ICharacter Opponent)
    {
        Abilities[AbilitySlot].Cast(Self, Opponent);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public abstract void TakeTurn(EncounterInstance Encounter);

}
