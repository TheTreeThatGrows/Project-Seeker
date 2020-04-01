namespace GameCreator.Accessibility
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using GameCreator.Core;
    using GameCreator.Characters;
    using GameCreator.Core.Hooks;
    using GameCreator.Variables;

#if UNITY_EDITOR
    using UnityEditor;
#endif

    [AddComponentMenu("")]
    public class ActionVolumeWithTag : IAction
    {

        public bool audioVolumeVar = false;
        [Range(0.0f, 1.0f)] public float _audioVolume = 1.0f;
        [VariableFilter(Variable.DataType.Number)]
        public VariableProperty audioVolume = new VariableProperty(Variable.VarType.GlobalVariable);


        public AudioSource audioSource;
        private AudioSource[] soundsArrayTag;

        public string tagStr = "";

        // EXECUTABLE: ----------------------------------------------------------------------------

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {


            var temp1 = GameObject.FindGameObjectsWithTag(tagStr);

            if (temp1 != null)
            {
                soundsArrayTag = new AudioSource[temp1.Length];
                for (int i = 0; i < soundsArrayTag.Length; i++)
                {
                    soundsArrayTag[i] = temp1[i].GetComponentInChildren<AudioSource>();
                }




                if (audioVolumeVar == true)
                {
                    float value = (float)this.audioVolume.Get(target);
                    for (int i = 0; i < this.soundsArrayTag.Length; ++i)
                    {
                        soundsArrayTag[i].volume = value;
                    }
                }
                else
                {
                    for (int i = 0; i < this.soundsArrayTag.Length; ++i)
                    {
                        soundsArrayTag[i].volume = _audioVolume;
                    }

                }

            }

            return true;
        }




        // +--------------------------------------------------------------------------------------+
        // | EDITOR                                                                               |
        // +--------------------------------------------------------------------------------------+

#if UNITY_EDITOR

        public const string CUSTOM_ICON_PATH = "Assets/Plugins/PivecLabs/Accessibility/Icons/";

        public static new string NAME = "Accessibility/Auditory/Volume Control with Tag";
        private const string NODE_TITLE = "Volume Control with TAG";


        // PROPERTIES: ----------------------------------------------------------------------------

        private SerializedProperty spAudioToggle;
        private SerializedProperty spAudioSlider;
        private SerializedProperty spAudio;

        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
        {
            return string.Format(NODE_TITLE);
        }

        protected override void OnEnableEditorChild()
        {
            this.spAudioToggle = this.serializedObject.FindProperty("audioVolumeVar");
            this.spAudioSlider = this.serializedObject.FindProperty("_audioVolume");
            this.spAudio = this.serializedObject.FindProperty("audioVolume");
        }

        protected override void OnDisableEditorChild()
        {
            this.spAudioToggle = null;
            this.spAudioSlider = null;
            this.spAudio = null;
        }

        public override void OnInspectorGUI()
        {
            this.serializedObject.Update();



            EditorGUILayout.LabelField("Audio Source Objects", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            tagStr = EditorGUILayout.TagField("Tag for Audio Source", tagStr);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Audio Volume", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(this.spAudioToggle, new GUIContent("Value from Variable"));
            if (audioVolumeVar == true)
            {
                EditorGUILayout.PropertyField(this.spAudio, new GUIContent("Volume Value"));
            }
            else
            {
                EditorGUILayout.PropertyField(this.spAudioSlider, new GUIContent("From 0 to 1"));
            }
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();



            this.serializedObject.ApplyModifiedProperties();
        }

            #endif
        }
}