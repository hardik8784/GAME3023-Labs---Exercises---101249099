

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EncounterInstance : MonoBehaviour
{

    private int turnNumber;
    public int TurnNumber
    {
        get { return turnNumber; }
        private set { turnNumber = value; }
    }

    [SerializeField]
    public PlayerCharacter Player;
    public AICharacter Enemy;
    public ICharacter CurrentCharacter;

    public UnityEvent<PlayerCharacter> OnPlayerTurnBegin;
    public UnityEvent<PlayerCharacter> OnPlayerTurnEnd;
    public UnityEvent<AICharacter> OnEnemyTurnBegin;
    public UnityEvent<ICharacter> OnTurnBegin;

    // Start is called before the first frame update
    void Start()
    {
        CurrentCharacter = Player;
        Player.OnAbilityCast.AddListener(OnAbilityCastCallBack);
    }

    public void OnAbilityCastCallBack(Ability Casted)
    {
        AdvanceTurns();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AdvanceTurns()
    {
        turnNumber++;
        if (CurrentCharacter == Player)
        {
            CurrentCharacter = Enemy;
            Player.OnAbilityCast.RemoveListener(OnAbilityCastCallBack);
            Enemy.OnAbilityCast.AddListener(OnAbilityCastCallBack);
            OnPlayerTurnEnd.Invoke(Player);
            OnEnemyTurnBegin.Invoke(Enemy);
        }
        else
        {
            CurrentCharacter = Player;
            Enemy.OnAbilityCast.RemoveListener(OnAbilityCastCallBack);
            Player.OnAbilityCast.AddListener(OnAbilityCastCallBack);
            OnPlayerTurnBegin.Invoke(Player);
        }
        OnTurnBegin.Invoke(CurrentCharacter);
        CurrentCharacter.TakeTurn(this);

    }

    public void EndBattle()
    {
        //FindObjectOfType<WorldTraveller>().ExitEncounter();
        //FindObjectOfType<PlayerBehaviour>().ExitEncounter();
    }
}
