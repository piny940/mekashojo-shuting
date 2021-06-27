using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerWeaponBaseImp : MonoBehaviour
{
    public class AttackManager
    {
        Action ProceedFirst;
        Action Attack;
        Action ProceedLast;
        Func<bool> CanAttack;
        Func<bool> CanStartAttack;
        float EnergyCost;
        bool _canAttack = false;

        public AttackManager(Action Attack, Func<bool> CanAttack, float EnergyCost, Action ProceedFirst, Action ProceedLast, Func<bool> CanStartAttack)
        {
            this.Attack = Attack;
            this.CanAttack = CanAttack;
            this.EnergyCost = EnergyCost;
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
                if (ProceedFirst != null && !_canAttack && CanAttack())
                {
                    ProceedFirst();
                }

                //攻撃の終わりにする処理
                if (ProceedLast != null && _canAttack && CanAttack())
                {
                    ProceedLast();
                }

                _canAttack = CanAttack();
            }
            else
            {
                //キャノン・レーザー
                //攻撃のはじめにする処理
                if (!_canAttack && CanStartAttack())
                {
                    ProceedFirst();
                    _canAttack = true;
                }

                //攻撃の終わりにする処理
                if (_canAttack && CanAttack())
                {
                    ProceedLast();
                    _canAttack = false;
                }
            }

            //攻撃そのもの
            if (_canAttack)
            {
                Attack();
                energyAmount -= EnergyCost;
            }

        }


    }


    
}
