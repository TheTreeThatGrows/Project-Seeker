namespace GameCreator.Accessibility
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using GameCreator.Core;
    using GameCreator.Core.Hooks;
    using GameCreator.Variables;

#if UNITY_EDITOR
    using UnityEditor;
	#endif

	[AddComponentMenu("")]
	public class ActionColourSwitching : IAction
	{

        // PROPERTIES: ----------------------------------------------------------------------------
      
        [Header("Up to 4 Colour switches possible")]
        public bool ColourSwitch1 = false;
        public bool ColourSwitch2 = false;
        public bool ColourSwitch3 = false;
        public bool ColourSwitch4 = false;

        // colour switch 1 variables
        public Color _originalColour1 = Color.green;
        public Color _newColour1 = Color.blue;
        public bool originalcolourVar1 = false;
        public bool newcolourVar1 = false;
        public bool toleranceVar1 = false; 
        public bool featherVar1 = false;
        [Range(0.0f, 1.0f)] public float _tolerance1 = 0.0f;
        [Range(0.0f, 0.5f)] public float _feathering1 = 0.0f;
        [VariableFilter(Variable.DataType.Color)]
        public VariableProperty originalColour1 = new VariableProperty(Variable.VarType.GlobalVariable);
        public VariableProperty newColour1 = new VariableProperty(Variable.VarType.GlobalVariable);
        [VariableFilter(Variable.DataType.Number)]
        public VariableProperty tolerance1 = new VariableProperty(Variable.VarType.GlobalVariable);
        public VariableProperty feathering1 = new VariableProperty(Variable.VarType.GlobalVariable);

        // colour switch 2 variables
        public Color _originalColour2 = Color.green;
        public Color _newColour2 = Color.blue;
        public bool originalcolourVar2 = false;
        public bool newcolourVar2 = false;
        public bool toleranceVar2 = false;
        public bool featherVar2 = false;
        [Range(0.0f, 1.0f)] public float _tolerance2 = 0.0f;
        [Range(0.0f, 0.5f)] public float _feathering2 = 0.0f;
        [VariableFilter(Variable.DataType.Color)]
        public VariableProperty originalColour2 = new VariableProperty(Variable.VarType.GlobalVariable);
        public VariableProperty newColour2 = new VariableProperty(Variable.VarType.GlobalVariable);
        [VariableFilter(Variable.DataType.Number)]
        public VariableProperty tolerance2 = new VariableProperty(Variable.VarType.GlobalVariable);
        public VariableProperty feathering2 = new VariableProperty(Variable.VarType.GlobalVariable);


        // colour switch 3 variables
        public Color _originalColour3 = Color.green;
        public Color _newColour3 = Color.blue;
        public bool originalcolourVar3 = false;
        public bool newcolourVar3 = false;
        public bool toleranceVar3 = false;
        public bool featherVar3 = false;
        [Range(0.0f, 1.0f)] public float _tolerance3 = 0.0f;
        [Range(0.0f, 0.5f)] public float _feathering3 = 0.0f;
        [VariableFilter(Variable.DataType.Color)]
        public VariableProperty originalColour3 = new VariableProperty(Variable.VarType.GlobalVariable);
        public VariableProperty newColour3 = new VariableProperty(Variable.VarType.GlobalVariable);
        [VariableFilter(Variable.DataType.Number)]
        public VariableProperty tolerance3 = new VariableProperty(Variable.VarType.GlobalVariable);
        public VariableProperty feathering3 = new VariableProperty(Variable.VarType.GlobalVariable);

        // colour switch 4 variables
        public Color _originalColour4 = Color.green;
        public Color _newColour4 = Color.blue;
        public bool originalcolourVar4 = false;
        public bool newcolourVar4 = false;
        public bool toleranceVar4 = false;
        public bool featherVar4 = false;
        [Range(0.0f, 1.0f)] public float _tolerance4 = 0.0f;
        [Range(0.0f, 0.5f)] public float _feathering4 = 0.0f;
        [VariableFilter(Variable.DataType.Color)]
        public VariableProperty originalColour4 = new VariableProperty(Variable.VarType.GlobalVariable);
        public VariableProperty newColour4 = new VariableProperty(Variable.VarType.GlobalVariable);
        [VariableFilter(Variable.DataType.Number)]
        public VariableProperty tolerance4 = new VariableProperty(Variable.VarType.GlobalVariable);
        public VariableProperty feathering4 = new VariableProperty(Variable.VarType.GlobalVariable);

      
        // EXECUTABLE: ----------------------------------------------------------------------------

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {

             return false;
        }

        public override IEnumerator Execute(GameObject target, IAction[] actions, int index)
        {

            if (HookCamera.Instance != null)
            {
                if (HookCamera.Instance.gameObject.GetComponent<CameraColourSwitcher>() == null)
                {
                    HookCamera.Instance.gameObject.AddComponent<CameraColourSwitcher>();
                    Debug.Log("Adding Camera Component");
                    yield return new WaitForSeconds(0.5f);
                }
              
              
              CameraColourSwitcher camswap = HookCamera.Instance.Get<CameraColourSwitcher>();


                if (ColourSwitch1 == true)
                {
                    camswap.switchColors[0].enabled = true;
       
                    if (originalcolourVar1 == true)
                    {
                        camswap.switchColors[0].oldColor = (Color)this.originalColour1.Get(target);
                    }
                    else
                    {
                        camswap.switchColors[0].oldColor = _originalColour1;
                    }


                    if (newcolourVar1 == true)
                    {
                        camswap.switchColors[0].newColor = (Color)this.newColour1.Get(target);
                    }
                    else
                    {
                        camswap.switchColors[0].newColor = _newColour1;
                    }

                    if (toleranceVar1 == true)
                    {
                        camswap.switchColors[0].tolerance = (float)this.tolerance1.Get(target);
                    }
                    else
                    {
                        camswap.switchColors[0].tolerance = _tolerance1;
                    }

                    if (featherVar1 == true)
                    {
                        camswap.switchColors[0].feathering = (float)this.feathering1.Get(target);
                    }
                    else
                    {
                        camswap.switchColors[0].feathering = _feathering1;
                    }


                } else
                {
                    camswap.switchColors[0].enabled = false;
                    camswap.switchColors[0].oldColor = Color.white;
                    camswap.switchColors[0].newColor = Color.white;
                    camswap.switchColors[0].tolerance = 0.0f;
                    camswap.switchColors[0].feathering = 0.0f;
                }

                if (ColourSwitch2 == true)
                {
                    camswap.switchColors[1].enabled = true;
                    if (originalcolourVar2 == true)
                    {
                        camswap.switchColors[1].oldColor = (Color)this.originalColour2.Get(target);
                    }
                    else
                    {
                        camswap.switchColors[1].oldColor = _originalColour2;
                    }


                    if (newcolourVar2 == true)
                    {
                        camswap.switchColors[1].newColor = (Color)this.newColour2.Get(target);
                    }
                    else
                    {
                        camswap.switchColors[1].newColor = _newColour2;
                    }


                    if (toleranceVar2 == true)
                    {
                        camswap.switchColors[1].tolerance = (float)this.tolerance2.Get(target);
                    }
                    else
                    {
                        camswap.switchColors[1].tolerance = _tolerance2;
                    }

                    if (featherVar2 == true)
                    {
                        camswap.switchColors[1].feathering = (float)this.feathering2.Get(target);
                    }
                    else
                    {
                        camswap.switchColors[1].feathering = _feathering2;
                    }


                }
                else
                {
                    camswap.switchColors[1].enabled = false;
                    camswap.switchColors[1].oldColor = Color.white;
                    camswap.switchColors[1].newColor = Color.white;
                    camswap.switchColors[1].tolerance = 0.0f;
                    camswap.switchColors[1].feathering = 0.0f;
                }

                if (ColourSwitch3 == true)
                {
                    camswap.switchColors[2].enabled = true;
                    if (originalcolourVar3 == true)
                    {
                        camswap.switchColors[2].oldColor = (Color)this.originalColour3.Get(target);
                    }
                    else
                    {
                        camswap.switchColors[2].oldColor = _originalColour3;
                    }


                    if (newcolourVar3 == true)
                    {
                        camswap.switchColors[2].newColor = (Color)this.newColour3.Get(target);
                    }
                    else
                    {
                        camswap.switchColors[2].newColor = _newColour3;
                    }


                    if (toleranceVar3 == true)
                    {
                        camswap.switchColors[2].tolerance = (float)this.tolerance3.Get(target);
                    }
                    else
                    {
                        camswap.switchColors[2].tolerance = _tolerance3;
                    }

                    if (featherVar3 == true)
                    {
                        camswap.switchColors[2].feathering = (float)this.feathering3.Get(target);
                    }
                    else
                    {
                        camswap.switchColors[2].feathering = _feathering3;
                    }


                }
                else
                {
                    camswap.switchColors[2].enabled = false;
                    camswap.switchColors[2].oldColor = Color.white;
                    camswap.switchColors[2].newColor = Color.white;
                    camswap.switchColors[2].tolerance = 0.0f;
                    camswap.switchColors[2].feathering = 0.0f;
                }

                if (ColourSwitch4 == true)
                {
                    camswap.switchColors[3].enabled = true;
                    if (originalcolourVar4 == true)
                    {
                        camswap.switchColors[3].oldColor = (Color)this.originalColour4.Get(target);
                    }
                    else
                    {
                        camswap.switchColors[3].oldColor = _originalColour4;
                    }


                    if (newcolourVar4 == true)
                    {
                        camswap.switchColors[3].newColor = (Color)this.newColour4.Get(target);
                    }
                    else
                    {
                        camswap.switchColors[3].newColor = _newColour4;
                    }


                    if (toleranceVar4 == true)
                    {
                        camswap.switchColors[3].tolerance = (float)this.tolerance4.Get(target);
                    }
                    else
                    {
                        camswap.switchColors[3].tolerance = _tolerance4;
                    }

                    if (featherVar4 == true)
                    {
                        camswap.switchColors[3].feathering = (float)this.feathering4.Get(target);
                    }
                    else
                    {
                        camswap.switchColors[3].feathering = _feathering4;
                    }


                }
                else
                {
                    camswap.switchColors[3].enabled = false;
                    camswap.switchColors[3].oldColor = Color.white;
                    camswap.switchColors[3].newColor = Color.white;
                    camswap.switchColors[3].tolerance = 0.0f;
                    camswap.switchColors[3].feathering = 0.0f;
                }


            }

            yield return base.Execute(target, actions, index);
        }


      

        // +--------------------------------------------------------------------------------------+
        // | EDITOR                                                                               |
        // +--------------------------------------------------------------------------------------+

#if UNITY_EDITOR

        public const string CUSTOM_ICON_PATH = "Assets/Plugins/PivecLabs/Accessibility/Icons/";

        public static new string NAME = "Accessibility/Visual/Colour Switching";
		private const string NODE_TITLE = "Switching Colours";

        // PROPERTIES: ----------------------------------------------------------------------------
        private SerializedProperty spColourSwitch1;
        private SerializedProperty spColourSwitch2;
        private SerializedProperty spColourSwitch3;
        private SerializedProperty spColourSwitch4;

        private SerializedProperty spOriginalColour1;
        private SerializedProperty spNewColour1;
        private SerializedProperty spOriginalColour1Var;
        private SerializedProperty spNewColour1Var;
        private SerializedProperty spOriginalColourToggle1;
        private SerializedProperty spNewColourToggle1;
        private SerializedProperty spToleranceToggle1;
        private SerializedProperty spFeatheringToggle1;
        private SerializedProperty spToleranceSlider1;
        private SerializedProperty spFeatheringSlider1;
        private SerializedProperty spTolerance1;
        private SerializedProperty spFeathering1;

        private SerializedProperty spOriginalColour2;
        private SerializedProperty spNewColour2;
        private SerializedProperty spOriginalColour2Var;
        private SerializedProperty spNewColour2Var;
        private SerializedProperty spOriginalColourToggle2;
        private SerializedProperty spNewColourToggle2;
        private SerializedProperty spToleranceToggle2;
        private SerializedProperty spFeatheringToggle2;
        private SerializedProperty spToleranceSlider2;
        private SerializedProperty spFeatheringSlider2;
        private SerializedProperty spTolerance2;
        private SerializedProperty spFeathering2;

        private SerializedProperty spOriginalColour3;
        private SerializedProperty spNewColour3;
        private SerializedProperty spOriginalColour3Var;
        private SerializedProperty spNewColour3Var;
        private SerializedProperty spOriginalColourToggle3;
        private SerializedProperty spNewColourToggle3;
        private SerializedProperty spToleranceToggle3;
        private SerializedProperty spFeatheringToggle3;
        private SerializedProperty spToleranceSlider3;
        private SerializedProperty spFeatheringSlider3;
        private SerializedProperty spTolerance3;
        private SerializedProperty spFeathering3;

        private SerializedProperty spOriginalColour4;
        private SerializedProperty spNewColour4;
        private SerializedProperty spOriginalColour4Var;
        private SerializedProperty spNewColour4Var;
        private SerializedProperty spOriginalColourToggle4;
        private SerializedProperty spNewColourToggle4;
        private SerializedProperty spToleranceToggle4;
        private SerializedProperty spFeatheringToggle4;
        private SerializedProperty spToleranceSlider4;
        private SerializedProperty spFeatheringSlider4;
        private SerializedProperty spTolerance4;
        private SerializedProperty spFeathering4;


        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
		{
			return string.Format(NODE_TITLE);
		}

		protected override void OnEnableEditorChild ()
		{
            this.spColourSwitch1 = this.serializedObject.FindProperty("ColourSwitch1");
            this.spColourSwitch2 = this.serializedObject.FindProperty("ColourSwitch2");
            this.spColourSwitch3 = this.serializedObject.FindProperty("ColourSwitch3");
            this.spColourSwitch4 = this.serializedObject.FindProperty("ColourSwitch4");

            this.spOriginalColour1 = this.serializedObject.FindProperty("_originalColour1");
            this.spNewColour1 = this.serializedObject.FindProperty("_newColour1");
            this.spOriginalColour1Var = this.serializedObject.FindProperty("originalColour1");
            this.spNewColour1Var = this.serializedObject.FindProperty("newColour1");
            this.spOriginalColourToggle1 = this.serializedObject.FindProperty("originalcolourVar1");
            this.spNewColourToggle1 = this.serializedObject.FindProperty("newcolourVar1");
            this.spToleranceToggle1 = this.serializedObject.FindProperty("toleranceVar1");
            this.spFeatheringToggle1 = this.serializedObject.FindProperty("featherVar1");
            this.spToleranceSlider1 = this.serializedObject.FindProperty("_tolerance1");
            this.spFeatheringSlider1 = this.serializedObject.FindProperty("_feathering1");
            this.spTolerance1 = this.serializedObject.FindProperty("tolerance1");
            this.spFeathering1 = this.serializedObject.FindProperty("feathering1");

            this.spOriginalColour2 = this.serializedObject.FindProperty("_originalColour2");
            this.spNewColour2 = this.serializedObject.FindProperty("_newColour2");
            this.spOriginalColour2Var = this.serializedObject.FindProperty("originalColour2");
            this.spNewColour2Var = this.serializedObject.FindProperty("newColour2");
            this.spOriginalColourToggle2 = this.serializedObject.FindProperty("originalcolourVar2");
            this.spNewColourToggle2 = this.serializedObject.FindProperty("newcolourVar2");
            this.spToleranceToggle2 = this.serializedObject.FindProperty("toleranceVar2");
            this.spFeatheringToggle2 = this.serializedObject.FindProperty("featherVar2");
            this.spToleranceSlider2 = this.serializedObject.FindProperty("_tolerance2");
            this.spFeatheringSlider2 = this.serializedObject.FindProperty("_feathering2");
            this.spTolerance2 = this.serializedObject.FindProperty("tolerance2");
            this.spFeathering2 = this.serializedObject.FindProperty("feathering2");

            this.spOriginalColour3 = this.serializedObject.FindProperty("_originalColour3");
            this.spNewColour3 = this.serializedObject.FindProperty("_newColour3");
            this.spOriginalColour3Var = this.serializedObject.FindProperty("originalColour3");
            this.spNewColour3Var = this.serializedObject.FindProperty("newColour3");
            this.spOriginalColourToggle3 = this.serializedObject.FindProperty("originalcolourVar3");
            this.spNewColourToggle3 = this.serializedObject.FindProperty("newcolourVar3");
            this.spToleranceToggle3 = this.serializedObject.FindProperty("toleranceVar3");
            this.spFeatheringToggle3 = this.serializedObject.FindProperty("featherVar3");
            this.spToleranceSlider3 = this.serializedObject.FindProperty("_tolerance3");
            this.spFeatheringSlider3 = this.serializedObject.FindProperty("_feathering3");
            this.spTolerance3 = this.serializedObject.FindProperty("tolerance3");
            this.spFeathering3 = this.serializedObject.FindProperty("feathering3");

            this.spOriginalColour4 = this.serializedObject.FindProperty("_originalColour4");
            this.spNewColour4 = this.serializedObject.FindProperty("_newColour4");
            this.spOriginalColour4Var = this.serializedObject.FindProperty("originalColour4");
            this.spNewColour4Var = this.serializedObject.FindProperty("newColour4");
            this.spOriginalColourToggle4 = this.serializedObject.FindProperty("originalcolourVar4");
            this.spNewColourToggle4 = this.serializedObject.FindProperty("newcolourVar4");
            this.spToleranceToggle4 = this.serializedObject.FindProperty("toleranceVar4");
            this.spFeatheringToggle4 = this.serializedObject.FindProperty("featherVar4");
            this.spToleranceSlider4 = this.serializedObject.FindProperty("_tolerance4");
            this.spFeatheringSlider4 = this.serializedObject.FindProperty("_feathering4");
            this.spTolerance4 = this.serializedObject.FindProperty("tolerance4");
            this.spFeathering4 = this.serializedObject.FindProperty("feathering4");


        }

        protected override void OnDisableEditorChild ()
		{
            this.spColourSwitch1 = null;
            this.spColourSwitch2 = null;
            this.spColourSwitch3 = null;
            this.spColourSwitch4 = null;

            this.spOriginalColour1 = null;
            this.spNewColour1 = null;
            this.spOriginalColour1Var = null;
            this.spNewColour1Var = null;
            this.spOriginalColourToggle1 = null;
            this.spNewColourToggle1 = null;
            this.spToleranceToggle1 = null;
            this.spFeatheringToggle1 = null;
            this.spToleranceSlider1 = null;
            this.spFeatheringSlider1 = null;
            this.spTolerance1 = null;
            this.spFeathering1 = null;

            this.spOriginalColour2 = null;
            this.spNewColour2 = null;
            this.spOriginalColour2Var = null;
            this.spNewColour2Var = null;
            this.spOriginalColourToggle2 = null;
            this.spNewColourToggle2 = null;
            this.spToleranceToggle2 = null;
            this.spFeatheringToggle2 = null;
            this.spToleranceSlider2 = null;
            this.spFeatheringSlider2 = null;
            this.spTolerance2 = null;
            this.spFeathering2 = null;

            this.spOriginalColour3 = null;
            this.spNewColour3 = null;
            this.spOriginalColour3Var = null;
            this.spNewColour3Var = null;
            this.spOriginalColourToggle3 = null;
            this.spNewColourToggle3 = null;
            this.spToleranceToggle3 = null;
            this.spFeatheringToggle3 = null;
            this.spToleranceSlider3 = null;
            this.spFeatheringSlider3 = null;
            this.spTolerance3 = null;
            this.spFeathering3 = null;

            this.spOriginalColour4 = null;
            this.spNewColour4 = null;
            this.spOriginalColour4Var = null;
            this.spNewColour4Var = null;
            this.spOriginalColourToggle4 = null;
            this.spNewColourToggle4 = null;
            this.spToleranceToggle4 = null;
            this.spFeatheringToggle4 = null;
            this.spToleranceSlider4 = null;
            this.spFeatheringSlider4 = null;
            this.spTolerance4 = null;
            this.spFeathering4 = null;


        }

        public override void OnInspectorGUI()
		{
			this.serializedObject.Update();
     
            EditorGUILayout.PropertyField(this.spColourSwitch1, new GUIContent("Enable Swap 1"));
            if (ColourSwitch1 == true)
            {

                EditorGUILayout.PropertyField(this.spOriginalColourToggle1, new GUIContent("Value from Variable"));
                if (originalcolourVar1 == true)
                {
                    EditorGUILayout.PropertyField(this.spOriginalColour1Var, new GUIContent("Original Colour"));
                }
                else
                {
                    EditorGUILayout.PropertyField(this.spOriginalColour1, new GUIContent("Original Colour"));
                }
               
                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(this.spNewColourToggle1, new GUIContent("Value from Variable"));
                if (newcolourVar1 == true)
                {
                    EditorGUILayout.PropertyField(this.spNewColour1Var, new GUIContent("New Colour"));
                }
                else
                {
                    EditorGUILayout.PropertyField(this.spNewColour1, new GUIContent("New Colour"));
                }

                EditorGUILayout.Space();

                EditorGUILayout.PropertyField(this.spToleranceToggle1, new GUIContent("Value from Variable"));
                if (toleranceVar1 == true)
                {
                    EditorGUILayout.PropertyField(this.spTolerance1, new GUIContent("Tolerance"));
                }
                else
                {
                    EditorGUILayout.PropertyField(this.spToleranceSlider1, new GUIContent("Tolerance"));
                }
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                 EditorGUILayout.PropertyField(this.spFeatheringToggle1, new GUIContent("Value from Variable"));
                if (featherVar1 == true)
                {
                    EditorGUILayout.PropertyField(this.spFeathering1, new GUIContent("Feathering"));
                }
                else
                {
                    EditorGUILayout.PropertyField(this.spFeatheringSlider1, new GUIContent("Feathering"));
                }

                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            }


            EditorGUILayout.PropertyField(this.spColourSwitch2, new GUIContent("Enable Swap 2"));
            if (ColourSwitch2 == true)
            {
                EditorGUILayout.PropertyField(this.spOriginalColourToggle2, new GUIContent("Value from Variable"));
                if (originalcolourVar2 == true)
                {
                    EditorGUILayout.PropertyField(this.spOriginalColour2Var, new GUIContent("Original Colour"));
                }
                else
                {
                    EditorGUILayout.PropertyField(this.spOriginalColour2, new GUIContent("Original Colour"));
                }

                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(this.spNewColourToggle2, new GUIContent("Value from Variable"));
                if (newcolourVar2 == true)
                {
                    EditorGUILayout.PropertyField(this.spNewColour2Var, new GUIContent("New Colour"));
                }
                else
                {
                    EditorGUILayout.PropertyField(this.spNewColour2, new GUIContent("New Colour"));
                }

                EditorGUILayout.Space();

                EditorGUILayout.PropertyField(this.spToleranceToggle2, new GUIContent("Value from Variable"));
                if (toleranceVar2 == true)
                {
                    EditorGUILayout.PropertyField(this.spTolerance2, new GUIContent("Tolerance"));
                }
                else
                {
                    EditorGUILayout.PropertyField(this.spToleranceSlider2, new GUIContent("Tolerance"));
                }
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(this.spFeatheringToggle2, new GUIContent("Value from Variable"));
                if (featherVar2 == true)
                {
                    EditorGUILayout.PropertyField(this.spFeathering2, new GUIContent("Feathering"));
                }
                else
                {
                    EditorGUILayout.PropertyField(this.spFeatheringSlider2, new GUIContent("Feathering"));
                }

                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            }


            EditorGUILayout.PropertyField(this.spColourSwitch3, new GUIContent("Enable Swap 3"));
            if (ColourSwitch3 == true)
            {
                EditorGUILayout.PropertyField(this.spOriginalColourToggle3, new GUIContent("Value from Variable"));
                if (originalcolourVar3 == true)
                {
                    EditorGUILayout.PropertyField(this.spOriginalColour3Var, new GUIContent("Original Colour"));
                }
                else
                {
                    EditorGUILayout.PropertyField(this.spOriginalColour3, new GUIContent("Original Colour"));
                }

                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(this.spNewColourToggle3, new GUIContent("Value from Variable"));
                if (newcolourVar3 == true)
                {
                    EditorGUILayout.PropertyField(this.spNewColour3Var, new GUIContent("New Colour"));
                }
                else
                {
                    EditorGUILayout.PropertyField(this.spNewColour3, new GUIContent("New Colour"));
                }

                EditorGUILayout.Space();

                EditorGUILayout.PropertyField(this.spToleranceToggle3, new GUIContent("Value from Variable"));
                if (toleranceVar3 == true)
                {
                    EditorGUILayout.PropertyField(this.spTolerance3, new GUIContent("Tolerance"));
                }
                else
                {
                    EditorGUILayout.PropertyField(this.spToleranceSlider3, new GUIContent("Tolerance"));
                }
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(this.spFeatheringToggle3, new GUIContent("Value from Variable"));
                if (featherVar3 == true)
                {
                    EditorGUILayout.PropertyField(this.spFeathering3, new GUIContent("Feathering"));
                }
                else
                {
                    EditorGUILayout.PropertyField(this.spFeatheringSlider3, new GUIContent("Feathering"));
                }

                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            }


            EditorGUILayout.PropertyField(this.spColourSwitch4, new GUIContent("Enable Swap 4"));
            if (ColourSwitch4 == true)
            {
                EditorGUILayout.PropertyField(this.spOriginalColourToggle4, new GUIContent("Value from Variable"));
                if (originalcolourVar4 == true)
                {
                    EditorGUILayout.PropertyField(this.spOriginalColour4Var, new GUIContent("Original Colour"));
                }
                else
                {
                    EditorGUILayout.PropertyField(this.spOriginalColour4, new GUIContent("Original Colour"));
                }

                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(this.spNewColourToggle4, new GUIContent("Value from Variable"));
                if (newcolourVar4 == true)
                {
                    EditorGUILayout.PropertyField(this.spNewColour4Var, new GUIContent("New Colour"));
                }
                else
                {
                    EditorGUILayout.PropertyField(this.spNewColour4, new GUIContent("New Colour"));
                }

                EditorGUILayout.Space();

                EditorGUILayout.PropertyField(this.spToleranceToggle4, new GUIContent("Value from Variable"));
                if (toleranceVar4 == true)
                {
                    EditorGUILayout.PropertyField(this.spTolerance4, new GUIContent("Tolerance"));
                }
                else
                {
                    EditorGUILayout.PropertyField(this.spToleranceSlider4, new GUIContent("Tolerance"));
                }
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(this.spFeatheringToggle4, new GUIContent("Value from Variable"));
                if (featherVar4 == true)
                {
                    EditorGUILayout.PropertyField(this.spFeathering4, new GUIContent("Feathering"));
                }
                else
                {
                    EditorGUILayout.PropertyField(this.spFeatheringSlider4, new GUIContent("Feathering"));
                }

                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            }
            this.serializedObject.ApplyModifiedProperties();
		}

		#endif
	}
}
