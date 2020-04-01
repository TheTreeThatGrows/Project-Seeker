namespace GameCreator.Accessibility
{
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.EventSystems;
    using System.Collections;

    using GameCreator.Characters;     using GameCreator.Core.Hooks;     using GameCreator.Variables; 
    [AddComponentMenu("")]
    public class AccessibilityButtonBar : MonoBehaviour
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        public Image jsContainer;

        private PlayerCharacter player;
     
        // EVENT METHODS: -------------------------------------------------------------------------

        private void Start()
		{
             player = HookPlayer.Instance.Get<PlayerCharacter>();
    

        }

    }
}