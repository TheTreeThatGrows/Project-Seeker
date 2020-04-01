namespace GameCreator.Accessibility
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Events;
	using GameCreator.Core;
    using GameCreator.Variables;

#if UNITY_EDITOR
    using UnityEditor;
	#endif

	[AddComponentMenu("")]
	public class ActionShowButtonBar : IAction
	{
 
        public bool showButtonA = false;
        public bool showButtonB = false;
        public bool showButtonC = false;
        public bool showButtonD = false;
        public bool showButtonE = false;



        // EXECUTABLE: ----------------------------------------------------------------------------

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {

            if (GameCreator.Core.TouchStickManager.FORCE_USAGE == true)

            {
                if (showButtonA == false)

                {
                    AccessibilityButtonBarActions.A.SetActive(false);
                }
                else
                {
                    AccessibilityButtonBarActions.A.SetActive(true);
                }
                if (showButtonB == false)

                {
                    AccessibilityButtonBarActions.B.SetActive(false);
                }
                else
                {
                    AccessibilityButtonBarActions.B.SetActive(true);
                }
                if (showButtonC == false)

                {
                    AccessibilityButtonBarActions.C.SetActive(false);
                }
                else
                {
                    AccessibilityButtonBarActions.C.SetActive(true);
                }
                if (showButtonD == false)

                {
                    AccessibilityButtonBarActions.D.SetActive(false);
                }
                else
                {
                    AccessibilityButtonBarActions.D.SetActive(true);
                }
                if (showButtonE == false)

                {
                    AccessibilityButtonBarActions.E.SetActive(false);
                }
                else
                {
                    AccessibilityButtonBarActions.E.SetActive(true);
                }

            }
            return true;
        }

        public override IEnumerator Execute(GameObject target, IAction[] actions, int index)
        {

            return base.Execute(target, actions, index);
        }



        // +--------------------------------------------------------------------------------------+
        // | EDITOR                                                                               |
        // +--------------------------------------------------------------------------------------+

#if UNITY_EDITOR
        public const string CUSTOM_ICON_PATH = "Assets/Plugins/PivecLabs/Accessibility/Icons/";

        public static new string NAME = "Accessibility/Motoric/Show ButtonBar";
        private const string NODE_TITLE = "Show Buttons on Bar";

        // PROPERTIES: ----------------------------------------------------------------------------

         private SerializedProperty spShowA;
        private SerializedProperty spShowB;
        private SerializedProperty spShowC;
        private SerializedProperty spShowD;
        private SerializedProperty spShowE;


        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
        {

            return string.Format(NODE_TITLE);
        }


        protected override void OnEnableEditorChild()
        {
            this.spShowA = this.serializedObject.FindProperty("showButtonA");
            this.spShowB = this.serializedObject.FindProperty("showButtonB");
            this.spShowC = this.serializedObject.FindProperty("showButtonC");
            this.spShowD = this.serializedObject.FindProperty("showButtonD");
            this.spShowE = this.serializedObject.FindProperty("showButtonE");
        }

        protected override void OnDisableEditorChild()
        {
             this.spShowA = null;
             this.spShowB = null;
             this.spShowC = null;
             this.spShowD = null;
             this.spShowE = null;
        }

        public override void OnInspectorGUI()
        {
            this.serializedObject.Update();

             EditorGUILayout.Space();
             EditorGUILayout.PropertyField(this.spShowA);
             EditorGUILayout.Space();
             EditorGUILayout.PropertyField(this.spShowB);
             EditorGUILayout.Space();
             EditorGUILayout.PropertyField(this.spShowC);
             EditorGUILayout.Space();
             EditorGUILayout.PropertyField(this.spShowD);
             EditorGUILayout.Space();
             EditorGUILayout.PropertyField(this.spShowE);

            this.serializedObject.ApplyModifiedProperties();
        }

#endif
    }
}