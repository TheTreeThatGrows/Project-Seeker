namespace GameCreator.Melee
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Events;
	using GameCreator.Core;
    using GameCreator.Characters;

    [AddComponentMenu("")]
	public class ActionMeleeDraw : IAction
	{
		public TargetCharacter character = new TargetCharacter(TargetCharacter.Target.Player);

        [Space]
        public MeleeWeapon meleeWeapon;

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {
            Character _character = this.character.GetCharacter(target);
            if (character == null) return true;

            CharacterMelee melee = _character.GetComponent<CharacterMelee>();
            if (melee != null)
            {
                CoroutinesManager.Instance.StartCoroutine(melee.Draw(this.meleeWeapon));
            }

            return true;
        }

		#if UNITY_EDITOR

        public static new string NAME = "Melee/Draw Weapon";
        private const string NODE_TITLE = "Character {0} draw {1}";

        public override string GetNodeTitle()
        {
            return string.Format(
                NODE_TITLE,
                this.character,
                this.meleeWeapon ? this.meleeWeapon.name : "(none)"
            );
        }

        #endif
    }
}
