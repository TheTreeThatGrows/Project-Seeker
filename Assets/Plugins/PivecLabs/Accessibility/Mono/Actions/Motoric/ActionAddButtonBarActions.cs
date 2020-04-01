
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
	public class ActionAddButtonBarActions : IAction
	{
        private Accessibility.AccessibilityButtonBar ButtonBar;
        private ButtonActions buttonA;
        private ButtonActions buttonB;
        private ButtonActions buttonC;
        private ButtonActions buttonD;
        private ButtonActions buttonE;

        public bool enableButtonA;
        public bool enableButtonB;
        public bool enableButtonC;
        public bool enableButtonD;
        public bool enableButtonE;

        public enum SourceA
        {
            Actions,
            Variable
        }
        public SourceA sourceA = SourceA.Actions;
        public Actions actionsA;
        Actions actionsToExecuteA = null;

        public enum SourceB
        {
            Actions,
            Variable
        }
        public SourceB sourceB = SourceB.Actions;
        public Actions actionsB;
        Actions actionsToExecuteB = null;

        public enum SourceC
        {
            Actions,
            Variable
        }
        public SourceC sourceC = SourceC.Actions;
        public Actions actionsC;
        Actions actionsToExecuteC = null;

        public enum SourceD
        {
            Actions,
            Variable
        }
        public SourceD sourceD = SourceD.Actions;
        public Actions actionsD;
        Actions actionsToExecuteD = null;

        public enum SourceE
        {
            Actions,
            Variable
        }
        public SourceE sourceE = SourceE.Actions;
        public Actions actionsE;
        Actions actionsToExecuteE = null;


        [VariableFilter(Variable.DataType.GameObject)]
        public VariableProperty variableA = new VariableProperty(Variable.VarType.LocalVariable);
        [VariableFilter(Variable.DataType.GameObject)]
        public VariableProperty variableB = new VariableProperty(Variable.VarType.LocalVariable);
        [VariableFilter(Variable.DataType.GameObject)]
        public VariableProperty variableC = new VariableProperty(Variable.VarType.LocalVariable);
        [VariableFilter(Variable.DataType.GameObject)]
        public VariableProperty variableD = new VariableProperty(Variable.VarType.LocalVariable);
        [VariableFilter(Variable.DataType.GameObject)]
        public VariableProperty variableE = new VariableProperty(Variable.VarType.LocalVariable);

        public bool waitToFinish = false;

        private bool actionsComplete = false;
        private bool forceStop = false;


        // EXECUTABLE: ----------------------------------------------------------------------------

        public override IEnumerator Execute(GameObject target, IAction[] actions, int index)
        {
          

            switch (this.sourceA)
            {
                case SourceA.Actions:
                    actionsToExecuteA = this.actionsA;
                    break;

                case SourceA.Variable:
                    GameObject value = this.variableA.Get(target) as GameObject;
                    if (value != null) actionsToExecuteA = value.GetComponent<Actions>();
                    break;
            }


            switch (this.sourceB)
            {
                case SourceB.Actions:
                    actionsToExecuteB = this.actionsB;
                    break;

                case SourceB.Variable:
                    GameObject value = this.variableB.Get(target) as GameObject;
                    if (value != null) actionsToExecuteB = value.GetComponent<Actions>();
                    break;
            }


            switch (this.sourceC)
            {
                case SourceC.Actions:
                    actionsToExecuteC = this.actionsC;
                    break;

                case SourceC.Variable:
                    GameObject value = this.variableC.Get(target) as GameObject;
                    if (value != null) actionsToExecuteC = value.GetComponent<Actions>();
                    break;
            }


            switch (this.sourceD)
            {
                case SourceD.Actions:
                    actionsToExecuteD = this.actionsD;
                    break;

                case SourceD.Variable:
                    GameObject value = this.variableD.Get(target) as GameObject;
                    if (value != null) actionsToExecuteD = value.GetComponent<Actions>();
                    break;
            }


            switch (this.sourceE)
            {
                case SourceE.Actions:
                    actionsToExecuteE = this.actionsE;
                    break;

                case SourceE.Variable:
                    GameObject value = this.variableE.Get(target) as GameObject;
                    if (value != null) actionsToExecuteE = value.GetComponent<Actions>();
                    break;
            }


            if ((actionsToExecuteA != null) && (enableButtonA == true) && (target.name == "ATSButtonA"))
            {
                this.actionsComplete = false;
                actionsToExecuteA.actionsList.Execute(target, this.OnCompleteActions);

                if (this.waitToFinish)
                {
                    WaitUntil wait = new WaitUntil(() =>
                    {
                        if (actionsToExecuteA == null) return true;
                        if (this.forceStop)
                        {
                            actionsToExecuteA.actionsList.Stop();
                            return true;
                        }

                        return this.actionsComplete;
                    });

                    yield return wait;
                }
            }


            if ((actionsToExecuteB != null) && (enableButtonB == true) && (target.name == "ATSButtonB"))
            {
                this.actionsComplete = false;
                actionsToExecuteB.actionsList.Execute(target, this.OnCompleteActions);

                if (this.waitToFinish)
                {
                    WaitUntil wait = new WaitUntil(() =>
                    {
                        if (actionsToExecuteB == null) return true;
                        if (this.forceStop)
                        {
                            actionsToExecuteB.actionsList.Stop();
                            return true;
                        }

                        return this.actionsComplete;
                    });

                    yield return wait;
                }
            }


            if ((actionsToExecuteC != null) && (enableButtonC == true) && (target.name == "ATSButtonC"))
            {
                this.actionsComplete = false;
                actionsToExecuteC.actionsList.Execute(target, this.OnCompleteActions);

                if (this.waitToFinish)
                {
                    WaitUntil wait = new WaitUntil(() =>
                    {
                        if (actionsToExecuteC == null) return true;
                        if (this.forceStop)
                        {
                            actionsToExecuteC.actionsList.Stop();
                            return true;
                        }

                        return this.actionsComplete;
                    });

                    yield return wait;
                }
            }


            if ((actionsToExecuteD != null) && (enableButtonD == true) && (target.name == "ATSButtonD"))
            {
                this.actionsComplete = false;
                actionsToExecuteD.actionsList.Execute(target, this.OnCompleteActions);

                if (this.waitToFinish)
                {
                    WaitUntil wait = new WaitUntil(() =>
                    {
                        if (actionsToExecuteD == null) return true;
                        if (this.forceStop)
                        {
                            actionsToExecuteD.actionsList.Stop();
                            return true;
                        }

                        return this.actionsComplete;
                    });

                    yield return wait;
                }
            }


            if ((actionsToExecuteE != null) && (enableButtonE == true) && (target.name == "ATSButtonE"))
            {
                this.actionsComplete = false;
                actionsToExecuteE.actionsList.Execute(target, this.OnCompleteActions);

                if (this.waitToFinish)
                {
                    WaitUntil wait = new WaitUntil(() =>
                    {
                        if (actionsToExecuteE == null) return true;
                        if (this.forceStop)
                        {
                            actionsToExecuteE.actionsList.Stop();
                            return true;
                        }

                        return this.actionsComplete;
                    });

                    yield return wait;
                }
            }



            yield return 0;
        }

        private void OnCompleteActions()
        {
            this.actionsComplete = true;
        }




        // +--------------------------------------------------------------------------------------+
        // | EDITOR                                                                               |
        // +--------------------------------------------------------------------------------------+

#if UNITY_EDITOR
        public const string CUSTOM_ICON_PATH = "Assets/Plugins/PivecLabs/Accessibility/Icons/";

        public static new string NAME = "Accessibility/Motoric/Add ButtonBar Actions";

        private const string NODE_TITLE = "Add actions to Button Bar";

        // PROPERTIES: ----------------------------------------------------------------------------

        private SerializedProperty spSourceA;
        private SerializedProperty spActionsA;
        private SerializedProperty spVariableA;
        private SerializedProperty spEnableA;
        private SerializedProperty spSourceB;
        private SerializedProperty spActionsB;
        private SerializedProperty spVariableB;
        private SerializedProperty spEnableB;
        private SerializedProperty spSourceC;
        private SerializedProperty spActionsC;
        private SerializedProperty spVariableC;
        private SerializedProperty spEnableC;
        private SerializedProperty spSourceD;
        private SerializedProperty spActionsD;
        private SerializedProperty spVariableD;
        private SerializedProperty spEnableD;
        private SerializedProperty spSourceE;
        private SerializedProperty spActionsE;
        private SerializedProperty spVariableE;
        private SerializedProperty spEnableE;


        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
        {

            return string.Format(NODE_TITLE);
        }


        protected override void OnEnableEditorChild()
        {
            this.spSourceA = this.serializedObject.FindProperty("sourceA");
            this.spVariableA = this.serializedObject.FindProperty("variableA");
            this.spActionsA = this.serializedObject.FindProperty("actionsA");
            this.spEnableA = this.serializedObject.FindProperty("enableButtonA");
            this.spSourceB = this.serializedObject.FindProperty("sourceB");
            this.spVariableB = this.serializedObject.FindProperty("variableB");
            this.spActionsB = this.serializedObject.FindProperty("actionsB");
            this.spEnableB = this.serializedObject.FindProperty("enableButtonB");
            this.spSourceC = this.serializedObject.FindProperty("sourceC");
            this.spVariableC = this.serializedObject.FindProperty("variableC");
            this.spActionsC = this.serializedObject.FindProperty("actionsC");
            this.spEnableC = this.serializedObject.FindProperty("enableButtonC");
            this.spSourceD = this.serializedObject.FindProperty("sourceD");
            this.spVariableD = this.serializedObject.FindProperty("variableD");
            this.spActionsD = this.serializedObject.FindProperty("actionsD");
            this.spEnableD = this.serializedObject.FindProperty("enableButtonD");
            this.spSourceE = this.serializedObject.FindProperty("sourceE");
            this.spVariableE = this.serializedObject.FindProperty("variableE");
            this.spActionsE = this.serializedObject.FindProperty("actionsE");
            this.spEnableE = this.serializedObject.FindProperty("enableButtonE");
        }

        protected override void OnDisableEditorChild()
        {
            this.spSourceA = null;
            this.spVariableA = null;
            this.spActionsA = null;
            this.spEnableA = null;
            this.spSourceB = null;
            this.spVariableB = null;
            this.spActionsB = null;
            this.spEnableB = null;
            this.spSourceC = null;
            this.spVariableC = null;
            this.spActionsC = null;
            this.spEnableC = null;
            this.spSourceD = null;
            this.spVariableD = null;
            this.spActionsD = null;
            this.spEnableD = null;
            this.spSourceE = null;
            this.spVariableE = null;
            this.spActionsE = null;
            this.spEnableE = null;
        }

        public override void OnInspectorGUI()
        {
            this.serializedObject.Update();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Button A Actions", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(this.spEnableA);

            EditorGUILayout.PropertyField(this.spSourceA);
            switch (this.spSourceA.enumValueIndex)
            {
                case (int)SourceA.Actions:
                    EditorGUILayout.PropertyField(this.spActionsA);
                    break;

                case (int)SourceA.Variable:
                    EditorGUILayout.PropertyField(this.spVariableA);
                    break;
            }

            EditorGUI.indentLevel--;
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Button B Actions", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(this.spEnableB);

            EditorGUILayout.PropertyField(this.spSourceB);
            switch (this.spSourceB.enumValueIndex)
            {
                case (int)SourceB.Actions:
                    EditorGUILayout.PropertyField(this.spActionsB);
                    break;

                case (int)SourceB.Variable:
                    EditorGUILayout.PropertyField(this.spVariableB);
                    break;
            }

            EditorGUI.indentLevel--;
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Button C Actions", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(this.spEnableC);

            EditorGUILayout.PropertyField(this.spSourceC);
            switch (this.spSourceC.enumValueIndex)
            {
                case (int)SourceC.Actions:
                    EditorGUILayout.PropertyField(this.spActionsC);
                    break;

                case (int)SourceC.Variable:
                    EditorGUILayout.PropertyField(this.spVariableC);
                    break;
            }

            EditorGUI.indentLevel--;
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Button D Actions", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(this.spEnableD);

            EditorGUILayout.PropertyField(this.spSourceD);
            switch (this.spSourceD.enumValueIndex)
            {
                case (int)SourceD.Actions:
                    EditorGUILayout.PropertyField(this.spActionsD);
                    break;

                case (int)SourceD.Variable:
                    EditorGUILayout.PropertyField(this.spVariableD);
                    break;
            }

            EditorGUI.indentLevel--;
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Button E Actions", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(this.spEnableE);

            EditorGUILayout.PropertyField(this.spSourceE);
            switch (this.spSourceE.enumValueIndex)
            {
                case (int)SourceE.Actions:
                    EditorGUILayout.PropertyField(this.spActionsE);
                    break;

                case (int)SourceE.Variable:
                    EditorGUILayout.PropertyField(this.spVariableE);
                    break;
            }

            EditorGUI.indentLevel--;

            this.serializedObject.ApplyModifiedProperties();
        }

#endif
    }
}