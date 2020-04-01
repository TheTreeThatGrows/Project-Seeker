namespace GameCreator.Shooter
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Events;
    using GameCreator.Core;
    using GameCreator.Characters;

	[AddComponentMenu("")]
	public class ActionWeaponShoot : IAction
	{
        public TargetGameObject shooter = new TargetGameObject(TargetGameObject.Target.Player);

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {
            CharacterShooter charShooter = this.shooter.GetComponent<CharacterShooter>(target);
            if (charShooter == null)
            {
                Debug.LogError("Target Game Object does not have a CharacterShooter component");
                return true;
            }

            charShooter.Shoot();
            return true;
        }

		#if UNITY_EDITOR

        public static new string NAME = "Shooter/Weapon Shoot";
        private const string NODE_TITLE = "Character {0} shoot weapon";

        public const string CUSTOM_ICON_PATH = "Assets/Plugins/GameCreator/Shooter/Extra/Icons/Actions/";

        public override string GetNodeTitle()
        {
            return string.Format(
                NODE_TITLE,
                this.shooter
            );
        }

        #endif
    }
}