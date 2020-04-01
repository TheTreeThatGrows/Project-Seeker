namespace GameCreator.UIComponents
{
        using System.Collections;
        using System.Collections.Generic;
        using UnityEngine;
        using UnityEngine.Events;
        using UnityEngine.UI;
        using UnityEngine.Video;
        using GameCreator.Core;
        using GameCreator.Variables;
        using GameCreator.Core.Hooks;
        using GameCreator.Characters;

#if UNITY_EDITOR
    using UnityEditor;
#endif

    [AddComponentMenu("")]
    public class ActionDisplayMiniMap : IAction
    {
        public GameObject mapPanel;
	    public GameObject mapManager;
	    public GameObject rawImage;
	    public GameObject mapcanvas;
	    public bool overlay;
	    public bool rotating;

	    [Range(1,60)]
	    public float cameraSize = 20;
	    public float cameraDistance = 30;
 
	   
	    
	   
	  
	    
        private PlayerCharacter player;
     
        public enum MAPPOSITION
        {
            TopRight,
            TopLeft,
            BottomLeft,
            BottomRight
            
        }


        public MAPPOSITION mapPosition = MAPPOSITION.TopRight;

        private float mmwidth;
        private float mmoffsetx;
        private float mmoffsety;
        private RectTransform m_RectTransform;

     

        RenderTexture renderTexture;
        RectTransform rt;
        RawImage img;
        private Camera targetCamera;
    
	    private MapManager fullscreen;

	    private GameObject rotatingFrame;
	    
        // EXECUTABLE: ----------------------------------------------------------------------------

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {

	        GameObject go = GameObject.Find ("MiniMapCamera");
	        mapcanvas.SetActive(false);
	        
	        if (go){
	        	
			    img = null;
		        targetCamera = null;
		        renderTexture = null;
		        Destroy (go.gameObject);

	        }

            player = HookPlayer.Instance.Get<PlayerCharacter>();
	        if (overlay)
	        {
	        	mapPanel.SetActive(true);
	        }
	        
	        fullscreen = mapManager.GetComponent<MapManager>();
	        fullscreen.miniMapshowing = true;
	        fullscreen.fullMapshowing = false;

	        
	        if (renderTexture == null)
	        {
                rt = (RectTransform)rawImage.transform;
                renderTexture = new RenderTexture((int)rt.rect.width, (int)rt.rect.height, 32);
                renderTexture.Create();

	        }

            if (img == null)
            {

                img = rawImage.gameObject.GetComponent<RawImage>();
                img.texture = renderTexture;

            }

            if (targetCamera == null)
            {
           
                GameObject cameraMinimap = new GameObject();
                cameraMinimap.transform.parent = player.transform;

	            targetCamera = cameraMinimap.AddComponent<Camera>();
                targetCamera.enabled = true;
                targetCamera.allowHDR = false;
                targetCamera.targetTexture = renderTexture;
                targetCamera.orthographic = true;
	            targetCamera.orthographicSize = cameraSize;
                targetCamera.name = "MiniMapCamera";
	            targetCamera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + cameraDistance, player.transform.position.z);
	            targetCamera.transform.LookAt(player.transform);
	            targetCamera.transform.localRotation = Quaternion.Euler(90.0f,0.0f,0.0f);

	            
	           
            }

            

            m_RectTransform = mapPanel.GetComponent<RectTransform>();
            m_RectTransform.localScale += new Vector3(0, 0, 0);
            mmwidth = m_RectTransform.rect.width;
	        mmoffsetx = 10;
	        mmoffsety = 10;

           
                        switch (mapPosition)
                        {
                            case MAPPOSITION.BottomLeft:
                                m_RectTransform.anchoredPosition = new Vector3(mmoffsetx, mmoffsety);
                                break;
                            case MAPPOSITION.TopLeft:
                                m_RectTransform.anchoredPosition = new Vector3(mmoffsetx, Screen.height - (mmwidth + mmoffsety));
                                break;
                            case MAPPOSITION.TopRight:
                                m_RectTransform.anchoredPosition = new Vector3(Screen.width - (mmwidth + mmoffsetx), Screen.height - (mmwidth + mmoffsety));
                                break;
                            case MAPPOSITION.BottomRight:
                                m_RectTransform.anchoredPosition = new Vector3(Screen.width - (mmwidth + mmoffsetx), mmoffsety);
                                break;

                        }
                        
		
        
	        if (rotating == false)
	 
	        {
	        	
		        mapPanel.GetComponentInChildren<FrameRotate>().rotating = false;
	        
	        }
	        
	        else 
	        
	        {
		        mapPanel.GetComponentInChildren<FrameRotate>().rotating = true;
	        	
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

        public static new string NAME = "UI/MiniMap/Display MiniMap";
            private const string NODE_TITLE = "Display MiniMap";
        public const string CUSTOM_ICON_PATH = "Assets/PivecLabs/UIComponents/Icons/";

        // PROPERTIES: ----------------------------------------------------------------------------

	    private SerializedProperty spmapmanager;
	    private SerializedProperty spmapposition;
        private SerializedProperty spmappanel;
        private SerializedProperty sprawimage;
	    private SerializedProperty spcamerasize;
	    private SerializedProperty spmapcanvas;
	    private SerializedProperty spoverlay;
	    private SerializedProperty sprotate;


        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
            {
                return string.Format(
                     NODE_TITLE
                 );
            }

        protected override void OnEnableEditorChild()
            {
	            this.spmapmanager = this.serializedObject.FindProperty("mapManager");
	            this.spmapposition = this.serializedObject.FindProperty("mapPosition");
            	this.spmappanel = this.serializedObject.FindProperty("mapPanel");
            	this.sprawimage = this.serializedObject.FindProperty("rawImage");
	        	this.spcamerasize = this.serializedObject.FindProperty("cameraSize");
	            this.spmapcanvas = this.serializedObject.FindProperty("mapcanvas");
	            this.spoverlay = this.serializedObject.FindProperty("overlay");
	            this.sprotate = this.serializedObject.FindProperty("rotating");
   
        }


        protected override void OnDisableEditorChild()
            {
        		this.spmapmanager = null;
	            this.spmapposition = null;
            	this.spmappanel = null;
            	this.sprawimage = null;
	            this.spmapcanvas = null;
	            this.spoverlay = null;
	            this.sprotate = null;


        }

        public override void OnInspectorGUI()
            {
                this.serializedObject.Update();
	            EditorGUILayout.PropertyField(this.spmapmanager, new GUIContent("Map Manager"));
	            EditorGUILayout.PropertyField(this.spmappanel, new GUIContent("Panel - MiniMap"));
            	EditorGUILayout.PropertyField(this.sprawimage, new GUIContent("RawImage texturemap"));
	            EditorGUILayout.PropertyField(this.spoverlay, new GUIContent("Full Map has Overlay"));
	            if (overlay)
	            {
		            EditorGUILayout.PropertyField(this.spmapcanvas, new GUIContent("Overlay Canvas"));
	            }
	            EditorGUILayout.Space();
	            EditorGUILayout.LabelField("Properties");
	            EditorGUI.indentLevel++;
	            EditorGUILayout.PropertyField(this.spmapposition, new GUIContent("MiniMap Position"));
            	EditorGUILayout.Space();

	            EditorGUILayout.PropertyField(this.spcamerasize, new GUIContent("Field of View"));
	            EditorGUILayout.PropertyField(this.sprotate, new GUIContent("Rotate Frame"));
	            EditorGUI.indentLevel--;
	            EditorGUILayout.Space();
	 
            this.serializedObject.ApplyModifiedProperties();
            }
	    
	   

	 
#endif

        }
    }
