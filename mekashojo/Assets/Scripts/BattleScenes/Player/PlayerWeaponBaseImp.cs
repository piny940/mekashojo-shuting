using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerWeaponBaseImp : MonoBehaviour
{
    Action ProceedFirst;
    Action Attack;
    Action ProceedLast;
    Func<bool> CanAttack;
    float EnergyCost;
    protected bool lastCanAttack = false;

    protected void SetMethod(Action Attack, Func<bool> CanAttack, float energyCost, Action ProceedFirst, Action ProceedLast)
    {
        this.Attack = Attack;
        this.CanAttack = CanAttack;
        this.EnergyCost = energyCost;
        this.ProceedFirst = ProceedFirst;
        this.ProceedLast = ProceedLast;
    }

    public void Execute(ref float energyAmount)
    {
        //攻撃のはじめにする処理
        if (ProceedFirst != null && !lastCanAttack && CanAttack())
        {
            ProceedFirst();
        }

        //攻撃の終わりにする処理
        if (ProceedLast != null && lastCanAttack && !CanAttack())
        {
            ProceedLast();
        }

        lastCanAttack = CanAttack();

        //攻撃そのもの
        if (CanAttack())
        {
            Attack();
            energyAmount -= EnergyCost;
        }

    }
}
