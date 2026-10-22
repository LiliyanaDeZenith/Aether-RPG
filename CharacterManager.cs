using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    public List<CharacterInstance> partyMembers = new List<CharacterInstance>();
    int currentCharIndex = 0;

    protected override void Awake()
    {
        base.Awake();
        if (partyMembers.Count == 0)
            foreach (var d in CharacterDatabase.DefaultParty) AddCharacter(d);
    }

    public void AddCharacter(CharacterData data)
    {
        var c = new CharacterInstance(data);
        partyMembers.Add(c);
        if (partyMembers.Count == 1) c.isActive = true;
    }

    public void SwitchCharacter(int index)
    {
        if (index < 0 || index >= partyMembers.Count) return;
        partyMembers[currentCharIndex].isActive = false;
        currentCharIndex = index;
        partyMembers[currentCharIndex].isActive = true;
        Debug.Log("Beralih ke: " + partyMembers[currentCharIndex].data.characterName);
    }

    public void NormalAttack() { var c = GetCurrent(); if (c != null) c.NormalAttack(); }
    public void UseElementalSkill() { var c = GetCurrent(); if (c != null) c.UseElementalSkill(); }
    public void UseElementalBurst() { var c = GetCurrent(); if (c != null) c.UseElementalBurst(); }
    public CharacterInstance GetCurrent() { return partyMembers.Count > 0 ? partyMembers[currentCharIndex] : null; }

    void Update() { foreach (var c in partyMembers) c.UpdateCooldowns(); }
}
