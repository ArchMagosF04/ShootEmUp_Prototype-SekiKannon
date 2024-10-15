using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IParryEffect
{
    public void OnParryEffect(Player_Shield player_Shield);

    public void OnBlockEffect(Player_Shield player_Shield);
}
