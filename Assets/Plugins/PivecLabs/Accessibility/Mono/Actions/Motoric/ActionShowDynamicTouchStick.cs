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
	public class ActionShowDynamicTouchStick : IAction
	{
      
		public bool touchStick;
		public bool runwalk;
		public bool tankcam;
        // EXECUTABLE: ----------------------------------------------------------------------------

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {


                return false;
         
        }

        public override IEnumerator Execute(GameObject target, IAction[] actions, int index)
        {
            if (touchStick == true)
            {
	            RectTransform ds = GameCreator.Core.TouchStickManager.Instance.GetComponentInChildren<DynamicTouchStick>().GetComponent<RectTransform>();
	            if (GameCreator.Core.TouchStickManager.Instance.GetComponentInChildren<DynamicTouchStick>().right == true)
		            
		            ds.anchoredPosition =  new Vector2 ((Screen.width -( ds.rect.width/2)),  ds.rect.y);
		            
	            else
	            
	            	ds.anchoredPosition =  new Vector2 ((ds.rect.width/2),  ds.rect.y);
	
	            GameCreator.Core.TouchStickManager.FORCE_USAGE = true;
	            GameCreator.Core.TouchStickManager.Instance.SetVisibility(true);
	
	            if (tankcam == true)
	            {
		           TouchStickManager.Instance.GetComponentInChildren<AccessibilityTouchStick>().tankcam = true;
		            
	            }
	            else 
	            {
		            TouchStickManager.Instance.GetComponentInChildren<AccessibilityTouchStick>().tankcam = false;
	            }
	            
	            if (runwalk == true)
	            {
		            TouchStickManager.Instance.GetComponentInChildren<AccessibilityTouchStick>().runwalk = true;
		            
	            }
	            else 
	            {
		            TouchStickManager.Instance.GetComponentInChildren<AccessibilityTouchStick>().runwalk = false;
	            }

	            
            }

            else
            {
	            RectTransform ds = GameCreator.Core.TouchStickManager.Instance.GetComponentInChildren<DynamicTouchStick>().GetComponent<RectTransform>();
	            ds.anchoredPosition =  new Vector2 (-Screen.width,-Screen.height);
	            GameCreator.Core.TouchStickManager.FORCE_USAGE = false;
	            GameCreator.Core.TouchStickManager.Instance.SetVisibility(false);
	  
            }

			

            yield return new WaitForSeconds(0.3f);
        }


        // +--------------------------------------------------------------------------------------+
        // | EDITOR                                                                               |
        // +--------------------------------------------------------------------------------------+

#if UNITY_EDITOR
        public const string CUSTOM_ICON_PATH = "Assets/Plugins/PivecLabs/Accessibility/Icons/";

		public static new string NAME = "Accessibility/Motoric/Show Dynamic TouchStick";
		private const string NODE_TITLE = "{0} Dynamic TouchStick";

        // PROPERTIES: ----------------------------------------------------------------------------

        private SerializedProperty spShowStick;
		private SerializedProperty spTankCam;
		private SerializedProperty spRunWalk;

        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
        {

	        return string.Format(
		        NODE_TITLE, 
		        (this.touchStick ? "Show" : "Hide")
	        );        }


        protected override void OnEnableEditorChild()
        {
            this.spShowStick = this.serializedObject.FindProperty("touchStick");
	        this.spTankCam = this.serializedObject.FindProperty("tankcam");
	        this.spRunWalk = this.serializedObject.FindProperty("runwalk");
   
        }

        protected override void OnDisableEditorChild()
        {
            this.spShowStick = null;
	        this.spTankCam = null;
	        this.spRunWalk = null;
        }

        public override void OnInspectorGUI()
        {
            this.serializedObject.Update();

            EditorGUILayout.Space();
	        EditorGUILayout.LabelField(new GUIContent("Enable Dynamic TouchStick"));
	        EditorGUILayout.Space();
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(this.spShowStick);
	      
    	    EditorGUI.indentLevel++;
	        EditorGUILayout.PropertyField(this.spTankCam, new GUIContent("with TankCam"));
	        EditorGUILayout.Space();
	     
	        EditorGUI.indentLevel--;
	        EditorGUILayout.LabelField(new GUIContent("Enable"));
	        EditorGUI.indentLevel++;
	        EditorGUILayout.PropertyField(this.spRunWalk, new GUIContent("Run/Walk"));
	        EditorGUILayout.Space();
	  
	        EditorGUI.indentLevel--;
	        EditorGUI.indentLevel--;
            this.serializedObject.ApplyModifiedProperties();
        }

#endif
    }
}
