
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility", menuName = "AbilitySystem/Ability")]
public class Ability : ScriptableObject
{
    [SerializeField]
    private string Name;

    [SerializeField]
    private string Description;

    [SerializeField]
    private IEffect[] Effects;

    [SerializeField]
    GameObject PartileSystem;

    public void Cast(ICharacter Self, ICharacter Other)
    {
        Debug.Log("Cast : " + Name);
        foreach (IEffect Effect in Effects)
        {
            Effect.ApplyEffect(Self, Other);
        }

        Self.OnAbilityCast.Invoke(this);
        WaitforSecondsAfterWin();
        //yield Wairforseconds(0.01f);
    }

    IEnumerator WaitforSecondsAfterWin()
    {
        PartileSystem.SetActive(true);
        yield return new WaitForSeconds(2000.0f);
    }
}
