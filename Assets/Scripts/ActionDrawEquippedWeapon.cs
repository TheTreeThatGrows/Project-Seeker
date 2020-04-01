/**
 * Melee-Inventory Integration
 * Author: John Money (Denarius)
 * Version: 1.0
 * 
 * Add action to Ready Weapon trigger.
 */

namespace GameCreator.Core
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using GameCreator.Melee;
    using GameCreator.Characters;


    [AddComponentMenu("")]
	public class ActionDrawEquippedWeapon : IAction
	{
        /* duplicate of ActionMeleeDraw with integration to MeleeWeapon 
           and CharacterAttachments */

        public TargetCharacter character = new TargetCharacter();
        public bool showEquippedWeapon = true;

        private static PlayerMeleeWeapon playerMeleeWeapon;
        private MeleeWeapon meleeWeapon;

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {
            Character characterTarget = this.character.GetCharacter(target);
            if (characterTarget == null) return true;

            CharacterAnimator animator = characterTarget.GetCharacterAnimator();
            if (animator == null) return true;

            CharacterAttachments attachments = animator.GetCharacterAttachments();
            if (attachments == null) return true;

            CharacterMelee melee = characterTarget.GetComponent<CharacterMelee>();
            if (melee != null)
            {
                playerMeleeWeapon = characterTarget.GetComponent<PlayerMeleeWeapon>();
                if (playerMeleeWeapon != null)
                {
                    if (this.showEquippedWeapon)
                    {
                        meleeWeapon = playerMeleeWeapon.meleeWeapon;

                        // requires patch to CharacterAttachments.cs
                        attachments.Hide(playerMeleeWeapon.bone);
                    }
                    else
                    {
                        attachments.Show(playerMeleeWeapon.bone);
                    }

                    CoroutinesManager.Instance.StartCoroutine(melee.Draw(meleeWeapon));
                }
                else
                {
                    Debug.LogError("[ActionSetEquippedWeapon] PlayerMeleeWeapon component not added to player", target);
                }
            }

            return true;
        }

        #if UNITY_EDITOR

        public static new string NAME = "Melee/Draw Equipped Weapon";
        private const string NODE_TITLE = "Character {0} {1} equipped weapon";

        public override string GetNodeTitle()
        {
            return string.Format(
                NODE_TITLE,
                this.character,
                this.showEquippedWeapon ? "show" : "hide"
            );
        }

        #endif
    }
}
