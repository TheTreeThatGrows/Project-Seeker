/**
 * Melee-Inventory Integration
 * Author: John Money (Denarius)
 * Version: 1.0
 * 
 * Add action to Inventory Equip and Unequip.
 */

namespace GameCreator.Core
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using GameCreator.Melee;

    [AddComponentMenu("")]
    public class ActionSetEquippedWeapon : IAction
    {
        public MeleeWeapon meleeWeapon;
        public HumanBodyBones bone = HumanBodyBones.RightHand;

        private static PlayerMeleeWeapon playerMeleeWeapon;

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {
            playerMeleeWeapon = target.GetComponent<PlayerMeleeWeapon>();
            if (playerMeleeWeapon != null)
            {
                playerMeleeWeapon.meleeWeapon = this.meleeWeapon;
                playerMeleeWeapon.bone = this.bone;
            }
            else
            {
                Debug.LogError("[ActionSetEquippedWeapon] PlayerMeleeWeapon component not added to player", target);
            }

            return true;
        }

        #if UNITY_EDITOR
        public static new string NAME = "Melee/Set Equipped Weapon";
        #endif
    }
}
