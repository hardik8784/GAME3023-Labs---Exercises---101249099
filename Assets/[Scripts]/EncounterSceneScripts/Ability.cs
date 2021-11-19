using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewAbility", menuName ="AbilitySystem/Ability")]
public class Ability : ScriptableObject
{
    [SerializeField]
    private string Name;

    [SerializeField]
    private IEffect[] Effects;

    [SerializeField]
    private string Description;

    void Cast(ICharacter Self,ICharacter Other)
    {

    }
}
