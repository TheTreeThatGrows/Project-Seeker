namespace GameCreator.Accessibility
{
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.EventSystems;
    using System.Collections;

	using GameCreator.Characters;
	using GameCreator.Core.Hooks;
	using GameCreator.Variables;
	
    [AddComponentMenu("")]
    public class AccessibilityTouchStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    { 
        // PROPERTIES: ----------------------------------------------------------------------------

        public Image jsContainer;
	    public Image joystick;
	    public bool tankcam = true;
	    public bool runwalk = true;
	    
        //  private Vector3 direction = Vector3.zero;
        private PlayerCharacter player;
        public Vector3 direction = Vector3.zero;

       float pitch = 0.0f;
       float yaw = 0.0f;
	   float x = 0.0f;
	   float y = 0.0f;
	    
	   private GameCreator.Camera.CameraController cameras;
       private GameCreator.Camera.CameraMotorTypeAdventure ADV;
       private GameCreator.Camera.CameraMotorTypeFirstPerson FPS;

        bool adv;
        bool fps;
	    bool pointerdown;
	    
        // EVENT METHODS: -------------------------------------------------------------------------

        private void Start()
		{
			cameras = HookCamera.Instance.Get<GameCreator.Camera.CameraController>();
            

			player = HookPlayer.Instance.Get<PlayerCharacter>();
            
			if (cameras.currentCameraMotor != null && cameras.currentCameraMotor.cameraMotorType.GetType() == typeof(GameCreator.Camera.CameraMotorTypeAdventure))
			{

				ADV = cameras.currentCameraMotor.GetComponent<GameCreator.Camera.CameraMotorTypeAdventure>();
				adv = true;
				fps = false;
			}

			else if (cameras.currentCameraMotor != null && cameras.currentCameraMotor.cameraMotorType.GetType() == typeof(GameCreator.Camera.CameraMotorTypeFirstPerson))
			{
				FPS = cameras.currentCameraMotor.GetComponent<GameCreator.Camera.CameraMotorTypeFirstPerson>();
				adv = false;
				fps = true;
			}

      
		}
        
	    private void Update()
	    
	    {
	    	
		    if(pointerdown == true && tankcam == true)      
		    {
	        	
			    pitch = x;
	        
			    if (adv == true)
				    ADV.AddRotation(yaw, pitch);
			    else if (fps == true)
				    FPS.AddRotation(yaw, pitch);
	        	
		    }
	    	
	    }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 position = Vector2.zero;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                this.jsContainer.rectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out position
            );

            position.x = (position.x / this.jsContainer.rectTransform.sizeDelta.x);
            position.y = (position.y / this.jsContainer.rectTransform.sizeDelta.y);
            
	         x = (Mathf.Approximately(this.jsContainer.rectTransform.pivot.x, 1f)) ? position.x * 2 + 1 : position.x * 2 - 1;
	         y = (Mathf.Approximately(this.jsContainer.rectTransform.pivot.y, 1f)) ? position.y * 2 + 1 : position.y * 2 - 1;
	        
	        if ((y > 0.6 || y < -0.6) && (runwalk == true))
            {    
	            player.characterLocomotion.canRun = true;
	            VariablesManager.SetGlobal("PlayerRunning", true);
            }
            else
            {
            	player.characterLocomotion.canRun = false;
            	VariablesManager.SetGlobal("PlayerRunning", false);
            }
            
	       
	       
        }

    public void OnPointerDown(PointerEventData eventData)
        {
	        if (cameras.currentCameraMotor != null && cameras.currentCameraMotor.cameraMotorType.GetType() == typeof(GameCreator.Camera.CameraMotorTypeAdventure))
            {

		        ADV = cameras.currentCameraMotor.GetComponent<GameCreator.Camera.CameraMotorTypeAdventure>();
                adv = true;
                fps = false;
            }

	        else if (cameras.currentCameraMotor != null && cameras.currentCameraMotor.cameraMotorType.GetType() == typeof(GameCreator.Camera.CameraMotorTypeFirstPerson))
            {
		        FPS = cameras.currentCameraMotor.GetComponent<GameCreator.Camera.CameraMotorTypeFirstPerson>();
                adv = false;
                fps = true;
            }

	        OnDrag(eventData);
	        pointerdown = true;
	       
        }

        public void OnPointerUp(PointerEventData eventData)
        {
           
	        pointerdown = false;
            this.direction = Vector3.zero;
            this.joystick.rectTransform.anchoredPosition = Vector3.zero;
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public  Vector2 GetDirection()
        {
            return this.direction;
        }
    }
}