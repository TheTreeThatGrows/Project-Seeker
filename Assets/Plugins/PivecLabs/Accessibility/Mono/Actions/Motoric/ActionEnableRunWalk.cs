namespace GameCreator.Accessibility
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
    using UnityEngine.UI;
	using UnityEngine.Events;
	using GameCreator.Core;
    using GameCreator.Variables;

	#if UNITY_EDITOR
	using UnityEditor;
	#endif

	[AddComponentMenu("")]
	public class ActionEnableRunWalk : IAction
	{
      
		public bool runwalk;
       // EXECUTABLE: ----------------------------------------------------------------------------

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {


                return false;
         
        }

        public override IEnumerator Execute(GameObject target, IAction[] actions, int index)
        {
                 
	              
	            if (runwalk == true)
	            {
		            TouchStickManager.Instance.GetComponentInChildren<AccessibilityTouchStick>().runwalk = true;
		            
	            }
	            else 
	            {
		            TouchStickManager.Instance.GetComponentInChildren<AccessibilityTouchStick>().runwalk = false;
	            }

	    
			

            yield return new WaitForSeconds(0.3f);
        }


        // +--------------------------------------------------------------------------------------+
        // | EDITOR                                                                               |
        // +--------------------------------------------------------------------------------------+

#if UNITY_EDITOR
        public const string CUSTOM_ICON_PATH = "Assets/Plugins/PivecLabs/Accessibility/Icons/";

		public static new string NAME = "Accessibility/Motoric/Enable Run Walk";
		private const string NODE_TITLE = "{0} Run/Walk";

        // PROPERTIES: ----------------------------------------------------------------------------

  		private SerializedProperty spRunWalk;

        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
        {

	        return string.Format(
		        NODE_TITLE, 
		        (this.runwalk ? "Enable" : "Disable")
	        );
        }


        protected override void OnEnableEditorChild()
        {
   	        this.spRunWalk = this.serializedObject.FindProperty("runwalk");
   
        }

        protected override void OnDisableEditorChild()
        {
  	        this.spRunWalk = null;
        }

        public override void OnInspectorGUI()
        {
            this.serializedObject.Update();
	        EditorGUILayout.Space();
	        EditorGUILayout.LabelField(new GUIContent("Run/Walk"));
	        EditorGUI.indentLevel++;
	        EditorGUILayout.PropertyField(this.spRunWalk, new GUIContent("Enable/Disable"));
	        EditorGUILayout.Space();
	  
	        EditorGUI.indentLevel--;
            this.serializedObject.ApplyModifiedProperties();
        }

#endif
    }
}
