namespace GameCreator.Accessibility
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;
    using System.Security.Cryptography;
    using System.Text;

    using GameCreator.Core;
    using GameCreator.Variables;

#if UNITY_EDITOR
    using UnityEditor;
#endif

    [AddComponentMenu("")]
	public class ConditionVariableTwoEncryptedStrings : ICondition
    {
        [VariableFilter(Variable.DataType.String)]
	    public VariableProperty variable1 = new VariableProperty();
	    public VariableProperty variable2 = new VariableProperty();
        public string decryptKey = "abcdefghijklmnopqrstuvwxyz123456";
	    private string decrypted1;
	    private string decrypted2;

        // OVERRIDERS: ----------------------------------------------------------------------------

        public override bool Check(GameObject target)
        {

	        if ((this.variable1.Get(target) != null) && (this.variable2.Get(target) != null))
            {
		        byte[] encryptedBytes1 = System.Convert.FromBase64String(this.variable1.ToStringValue(target));

		        decrypted1 = decrypting(encryptedBytes1, decryptKey);

		        byte[] encryptedBytes2 = System.Convert.FromBase64String(this.variable2.ToStringValue(target));

		        decrypted2 = decrypting(encryptedBytes2, decryptKey);

            }
            
	        
	        return this.decrypted1 == this.decrypted2;
        }

        public static string decrypting(byte[] toEncryptArray, string key)
        {
            try
            {

                byte[] keyArray = Encoding.UTF8.GetBytes(key);
                RijndaelManaged rManaged = new RijndaelManaged();
                rManaged.Key = keyArray;
                rManaged.Mode = CipherMode.ECB;
                rManaged.Padding = PaddingMode.ISO10126;
                ICryptoTransform cTransform = rManaged.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return Encoding.UTF8.GetString(resultArray);
            }
            catch

            {
                Debug.Log("Decrypting Failed");
            }
            return null;
        }
        // +--------------------------------------------------------------------------------------+
        // | EDITOR                                                                               |
        // +--------------------------------------------------------------------------------------+

#if UNITY_EDITOR

        public const string CUSTOM_ICON_PATH = "Assets/Plugins/PivecLabs/Accessibility/Icons/";
	    public static new string NAME = "Accessibility/Conditions/Variable Two Encrypted Strings";
        private const string NODE_TITLE = "Comparing {0} with {1}";

        // PROPERTIES: ----------------------------------------------------------------------------

        private SerializedProperty spvariable1;
	    private SerializedProperty spvariable2;
        private SerializedProperty spKey;

        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
        {
	        return string.Format(NODE_TITLE, this.variable1, this.variable2);
        }

        protected override void OnEnableEditorChild()
        {
	        this.spvariable1 = this.serializedObject.FindProperty("variable1");
	        this.spvariable2 = this.serializedObject.FindProperty("variable2");
            this.spKey = this.serializedObject.FindProperty("decryptKey");
        }

        protected override void OnDisableEditorChild()
        {
	        this.spvariable1 = null;
	        this.spvariable2 = null;
            this.spKey = null;
        }

        public override void OnInspectorGUI()
        {
            this.serializedObject.Update();
	        EditorGUILayout.PropertyField(this.spvariable1, new GUIContent("Encrypted Variable 1"));
	        EditorGUILayout.PropertyField(this.spvariable2, new GUIContent("Encrypted Variable 2"));
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.spKey, new GUIContent("Decryption Key "));
            EditorGUILayout.LabelField(new GUIContent("Must be identical to Encryption Key"));


            EditorGUILayout.Space();

            this.serializedObject.ApplyModifiedProperties();

        }
#endif
	}
}