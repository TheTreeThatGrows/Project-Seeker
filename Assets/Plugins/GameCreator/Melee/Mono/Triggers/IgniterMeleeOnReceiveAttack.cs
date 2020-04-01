namespace GameCreator.Melee
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using GameCreator.Core;

    [AddComponentMenu("")]
    public class IgniterMeleeOnReceiveAttack : Igniter 
	{
		#if UNITY_EDITOR
        public new static string NAME = "Melee/On Receive Attack";
        public new static bool REQUIRES_COLLIDER = true;
        #endif

        public void OnReceiveAttack(CharacterMelee attacker, MeleeClip attack,
            CharacterMelee.HitResult hitResult)
		{
            // TODO: Filter attack by type, such as hit result, by attack, by player attack, ...

			this.ExecuteTrigger(attacker.gameObject);
		}
	}
}