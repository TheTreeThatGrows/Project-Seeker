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
  

#if UNITY_EDITOR
    using UnityEditor;
#endif

    [AddComponentMenu("")]
    public class ActionDisplayCanvasModel : IAction
    {
	    public LayerMask objectImageLayer;
	    public LightType objectLight;

        public GameObject targetModel;
        public GameObject targetObject;

        public bool spinObject;
        public bool dragObject;
        public float timeUntilDie = 10f;
        public bool xAxis;
        public bool yAxis;
	    public bool xAxisAuto;
	    public bool yAxisAuto;
      
	    [Range(0f, 20f)]
	    public float lightIntensity = 5f;

        [Range(1f, 10f)]
        public float objectSize = 4.0f;

 
	    [Range(0.01f, 1.0f)]
	    public float autoSpeed = 0.1f;

	    [Range(1f, 20.0f)]
	    public float dragSpeed = 10f;

	    
	    RenderTexture renderTexture;
        RectTransform rt;
        RawImage img;
        private Camera targetCamera;
        private GameObject imageObject;
        public NumberProperty InitialtimerValue = new NumberProperty(1.0f);
        public NumberProperty IntervaltimerValue = new NumberProperty(1.0f);
        public NumberProperty LoopCount = new NumberProperty(10f);
        public bool InfiniteLoops;
        
        public Vector3 axis = new Vector3(0, 1f, 0);
        float Rotation;
        private Light cameraLight;
        private Vector3 speed = Vector3.zero;
        private Vector3 averageSpeed = Vector3.zero;

        private Vector2 lastMousePosition = Vector2.zero;
      
        public float RotationSpeed = 10f;

      
	    public bool RotateX = true;
	    public bool InvertX = false;
        private int _xMultiplier
        {
            get { return InvertX ? -1 : 1; }
        }

       
        public bool RotateY = true;
	    public bool InvertY = false;
        private int _yMultiplier
        {
            get { return InvertY ? -1 : 1; }
        }

	    public bool invertZ = false;
	    public int invert;

        // EXECUTABLE: ----------------------------------------------------------------------------

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {
	        invert = invertZ ? -1 : 1;

	 
            if (renderTexture == null)
            {
                rt = (RectTransform)targetObject.transform;
                renderTexture = new RenderTexture((int)rt.rect.width, (int)rt.rect.height, 32);
                renderTexture.Create();

            }

            if (img == null)
            {

                img = targetObject.gameObject.GetComponent<RawImage>();
                img.texture = renderTexture;

            }
            
	        if (imageObject == null)
	        {

		        imageObject = Instantiate(targetModel, rt.position, Quaternion.identity);
		        imageObject.transform.localScale = new Vector3(objectSize, objectSize, objectSize);


	        }

            if (targetCamera == null)
            {

                GameObject camera3d = new GameObject();
	            targetCamera = camera3d.AddComponent<Camera>();
	            
                targetCamera.enabled = true;
                targetCamera.allowHDR = false;
                targetCamera.targetTexture = renderTexture;
                targetCamera.orthographic = true;

                targetCamera.name = "3DCamera";

                targetCamera.clearFlags = CameraClearFlags.SolidColor;
                targetCamera.backgroundColor = Color.clear;
                targetCamera.gameObject.layer = layermask_to_layer(objectImageLayer.value);
                targetCamera.cullingMask = objectImageLayer.value;

	         
	            targetCamera.transform.position = new Vector3(imageObject.transform.position.x, 0, invert);

	          
	           
            }

           


            if (cameraLight == null)

            {
                cameraLight = targetCamera.gameObject.AddComponent<Light>();

                cameraLight.gameObject.layer = layermask_to_layer(objectImageLayer.value);
                cameraLight.cullingMask = objectImageLayer.value;
	            cameraLight.type = objectLight;
	            cameraLight.intensity = (lightIntensity / 10);
	            cameraLight.range = 200;
                cameraLight.color = Color.white;
	            cameraLight.bounceIntensity = 1;
                   
            }


            Vector3 containerLocalPosition = imageObject.transform.position - targetCamera.transform.position;


            float DesireDistanceFromCamera = 0.5f;
            imageObject.transform.position = targetCamera.transform.position + (containerLocalPosition * DesireDistanceFromCamera);


            imageObject.transform.LookAt(targetCamera.transform.position + targetCamera.transform.rotation * Vector3.forward,
            targetCamera.transform.rotation * Vector3.up);
            var position = imageObject.transform.position + Vector3.up * 5;
            cameraLight.transform.LookAt(position);
            targetCamera.transform.LookAt(position);
            targetCamera.Render();

             if (dragObject == true)
             {					
	             InvokeRepeating("Dragging", 0.1f, 0.1f); 
	  
             }
            if (spinObject == true)
            {
	            InvokeRepeating("Spinning", 0.1f, autoSpeed);
	 
            }
               


            Destroy(imageObject, timeUntilDie);
            Destroy(targetCamera.gameObject, timeUntilDie);

            return true;

        }

        public override IEnumerator Execute(GameObject target, IAction[] actions, int index)
        {



            return base.Execute(target, actions, index);

        }

        public static int layermask_to_layer(LayerMask layerMask)
        {
            int layerNumber = 0;
            int layer = layerMask.value;
            while (layer > 0)
            {
                layer = layer >> 1;
                layerNumber++;
            }

            return layerNumber - 1;
        }

        private void Spinning()
        {
	        float xAuto = 0;
	        float yAuto = 0;
	        
            if (imageObject == null)
            {
                CancelInvoke("Spinning"); //In case it's already running.

            }
            else
            {
            	
      
             if (xAxisAuto == true)
             {
	             xAuto = 10f;

             }
	        if (yAxisAuto == true)
	        {
		        yAuto = 10f;

	        }
	        
	            imageObject.transform.Rotate(xAuto, yAuto, 0);
            }
        }

        private void Dragging()
        {
            if (imageObject == null)
            {
                CancelInvoke("Dragging"); //In case it's already running.

            }
            else
            {
                if (lastMousePosition == Vector2.zero) lastMousePosition = Input.mousePosition;

                if (Input.GetMouseButton(0))
                {
                    var mouseDelta = ((Vector2)Input.mousePosition - lastMousePosition) * 100;
                    mouseDelta.Set(mouseDelta.x / Screen.width, mouseDelta.y / Screen.height);

                    speed = new Vector3(-mouseDelta.x * _xMultiplier, mouseDelta.y * _yMultiplier, 0);
                   }


                if (speed != Vector3.zero)
                {
                    if (xAxis == true)
                    {
                        imageObject.transform.Rotate(targetCamera.transform.up * speed.x * dragSpeed, Space.World);

                    }
                    if (yAxis == true)
                    {
                        imageObject.transform.Rotate(targetCamera.transform.right * speed.y * dragSpeed, Space.World);
                    }
                }

                lastMousePosition = Input.mousePosition;
            }

        }


        // +--------------------------------------------------------------------------------------+
        // | EDITOR                                                                               |
        // +--------------------------------------------------------------------------------------+

#if UNITY_EDITOR

	    public static new string NAME = "UI/Models/Display 3D Model on Canvas";
            private const string NODE_TITLE = "Display 3D Model on {0}";
        public const string CUSTOM_ICON_PATH = "Assets/PivecLabs/UIComponents/Icons/";

        // PROPERTIES: ----------------------------------------------------------------------------

        private SerializedProperty sptargetModel;
        private SerializedProperty spimageLayer;
        private SerializedProperty sptargetObject;
        private SerializedProperty spspinObject;
        private SerializedProperty spdragObject;
        private SerializedProperty spkillObject;
        private SerializedProperty spxAxis;
        private SerializedProperty spyAxis;
	    private SerializedProperty spxAxisAuto;
	    private SerializedProperty spyAxisAuto;
	    private SerializedProperty spscale;
        private SerializedProperty splight;
	    private SerializedProperty spinvertz;
	    private SerializedProperty spautospeed;
	    private SerializedProperty spdragspeed;
	    private SerializedProperty spobjectlight;

        // INSPECTOR METHODS: ---------------------------------------------------------------------

        public override string GetNodeTitle()
            {
                return string.Format(
                     NODE_TITLE,
                     (targetObject == null ? "none" : targetObject.name)
                 );
            }

        protected override void OnEnableEditorChild()
            {
            this.spimageLayer = this.serializedObject.FindProperty("objectImageLayer");
            this.sptargetModel = this.serializedObject.FindProperty("targetModel");
            this.sptargetObject = this.serializedObject.FindProperty("targetObject");
            this.spspinObject = this.serializedObject.FindProperty("spinObject");
            this.spdragObject = this.serializedObject.FindProperty("dragObject");
            this.spkillObject = this.serializedObject.FindProperty("timeUntilDie");
            this.spxAxis = this.serializedObject.FindProperty("xAxis");
            this.spyAxis = this.serializedObject.FindProperty("yAxis");
	            this.spxAxisAuto = this.serializedObject.FindProperty("xAxisAuto");
	            this.spyAxisAuto = this.serializedObject.FindProperty("yAxisAuto");
	            this.spscale = this.serializedObject.FindProperty("objectSize");
            this.splight = this.serializedObject.FindProperty("lightIntensity");
	            this.spinvertz = this.serializedObject.FindProperty("invertZ");
	            this.spautospeed = this.serializedObject.FindProperty("autoSpeed");
	            this.spdragspeed = this.serializedObject.FindProperty("dragSpeed");
	            this.spobjectlight = this.serializedObject.FindProperty("objectLight");
            }


        protected override void OnDisableEditorChild()
            {
            this.spimageLayer = null;
            this.sptargetObject = null;
            this.sptargetModel = null;
            this.spspinObject = null;
            this.spdragObject = null;
            this.spkillObject = null;
            this.spxAxis = null;
            this.spyAxis = null;
	            this.spxAxisAuto = null;
	            this.spyAxisAuto = null;
	            this.spscale = null;
            this.splight = null;
	            this.spinvertz = null;
	            this.spautospeed = null;
	            this.spdragspeed = null;
	            this.spobjectlight = null;

        }

        public override void OnInspectorGUI()
	    {
		

                this.serializedObject.Update();
            EditorGUILayout.PropertyField(this.sptargetObject, new GUIContent("Canvas RawImage"));
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.sptargetModel, new GUIContent("3D Model"));
            EditorGUI.indentLevel++;
	            EditorGUILayout.PropertyField(this.spimageLayer, new GUIContent("Image Layer of Model"));
	            EditorGUILayout.PropertyField(this.spinvertz, new GUIContent("Invert Model"));
                  
            EditorGUILayout.PropertyField(this.spscale, new GUIContent("Size of Model"));
            EditorGUILayout.PropertyField(this.splight, new GUIContent("Light Intensity"));
		    EditorGUILayout.PropertyField(this.spobjectlight, new GUIContent("Type"));
		    EditorGUI.indentLevel--;
            EditorGUILayout.Space();
              EditorGUILayout.Space(); EditorGUILayout.LabelField("Display Properties", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
	            EditorGUILayout.PropertyField(this.spspinObject, new GUIContent("Auto Rotate"));
	            if (spinObject == true)
	            { 
	   	            EditorGUI.indentLevel++;
		            EditorGUILayout.Space();
		            EditorGUILayout.PropertyField(this.spxAxisAuto, new GUIContent("x Axis"));
		            EditorGUILayout.PropertyField(this.spyAxisAuto, new GUIContent("y Axis"));
		            EditorGUILayout.PropertyField(this.spautospeed, new GUIContent("Rotate Speed"));
		            EditorGUI.indentLevel--;
	            }
		    EditorGUILayout.Space();
	            EditorGUILayout.PropertyField(this.spdragObject, new GUIContent("Drag to Rotate"));
	            if (dragObject == true)
            	{  
		            EditorGUI.indentLevel++;
                	EditorGUILayout.Space();
                	EditorGUILayout.PropertyField(this.spxAxis, new GUIContent("x Axis"));
                	EditorGUILayout.PropertyField(this.spyAxis, new GUIContent("y Axis"));
		            EditorGUILayout.PropertyField(this.spdragspeed, new GUIContent("Drag Speed"));
		            EditorGUI.indentLevel--;
	       		 }
          
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(this.spkillObject, new GUIContent("Time before Destroyed"));
            EditorGUI.indentLevel--;
            this.serializedObject.ApplyModifiedProperties();
            }

#endif

        }
    }
