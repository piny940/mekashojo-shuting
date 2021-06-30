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
    Func<bool> CanStartAttack;
    float EnergyCost;
    public bool canAttack = false;

    protected void SetMethod(Action Attack, Func<bool> CanAttack, float energyCost, Action ProceedFirst, Action ProceedLast, Func<bool> CanStartAttack)
    {
        this.Attack = Attack;
        this.CanAttack = CanAttack;
        this.EnergyCost = energyCost;
        this.ProceedFirst = ProceedFirst;
        this.ProceedLast = ProceedLast;
        this.CanStartAttack = CanStartAttack;
    }

    public void Execute(ref float energyAmount)
    {
        if (CanStartAttack == null)
        {
            //キャノン・レーザー以外
            //攻撃のはじめにする処理
            if (ProceedFirst != null && !canAttack && CanAttack())
            {
                ProceedFirst();
            }

            //攻撃の終わりにする処理
            if (ProceedLast != null && canAttack && !CanAttack())
            {
                ProceedLast();
            }

            canAttack = CanAttack();
        }
        else
        {
            //キャノン・レーザー
            //攻撃のはじめにする処理
            if (ProceedFirst != null && !canAttack && CanStartAttack())
            {
                ProceedFirst();
            }

            //攻撃の終わりにする処理
            if (ProceedLast != null && canAttack && !CanAttack())
            {
                ProceedLast();
            }
        }

        //攻撃そのもの
        if (canAttack)
        {
            Attack();
            energyAmount -= EnergyCost;
        }

    }
    

    
}
