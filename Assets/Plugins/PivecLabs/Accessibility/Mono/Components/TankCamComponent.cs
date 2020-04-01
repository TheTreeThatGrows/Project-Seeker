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

    public class TankCamComponent : MonoBehaviour
    {

        [HideInInspector]
        public bool enableTankCamera = false;
        [HideInInspector]
        public float Sensitivity = 2.0f;
        [HideInInspector]
	    public float LeftLean = 15f;
        [HideInInspector]
	    public float RightLean = -15f;
        [HideInInspector]
	    public TargetCharacter character = new TargetCharacter(TargetCharacter.Target.Player);
        
	    private Character charTarget;
	    private Transform charTrans;

	    void Start()
	    {
	    	
		    
			     charTarget = this.character.GetCharacter(gameObject);
			     charTrans = charTarget.transform.GetChild(0);
			  
		    
	    }

        // Update is called once per frame
        void Update()
        {

            if (enableTankCamera == true)

            {
	           
	           
                var controller = HookCamera.Instance.Get<GameCreator.Camera.CameraController>();

                if (controller.currentCameraMotor != null && controller.currentCameraMotor.cameraMotorType.GetType() == typeof(GameCreator.Camera.CameraMotorTypeAdventure))
                {
                    var rotation = controller.currentCameraMotor.GetComponent<GameCreator.Camera.CameraMotorTypeAdventure>();

                    if (Input.GetKey(KeyCode.A))
                    {

                        if (Input.GetKey(KeyCode.W))
                        {
	                        
	                        charTarget.GetCharacterAnimator().SetRotationRoll(LeftLean);
	                       
                        }
                        else
                        {

	                        charTarget.GetCharacterAnimator().SetRotationRoll(0);

                        }

                        rotation.AddRotation(0.0f, -Sensitivity);


                    }

                    if (Input.GetKey(KeyCode.D))
                    {

                        if (Input.GetKey(KeyCode.W))
                        {
	                        charTarget.GetCharacterAnimator().SetRotationRoll(RightLean);
                        }
                        else
                        {

	                        charTarget.GetCharacterAnimator().SetRotationRoll(0);

                        }
                        rotation.AddRotation(0.0f, Sensitivity);


                    }

                  
                    if ((Input.GetKeyUp(KeyCode.A)) || (Input.GetKeyUp(KeyCode.D)))
                    {

	                    charTarget.GetCharacterAnimator().SetRotationRoll(0);


                    }
                    
	              
                }

                if (controller.currentCameraMotor != null && controller.currentCameraMotor.cameraMotorType.GetType() == typeof(GameCreator.Camera.CameraMotorTypeFirstPerson))
                {
                    var rotation = controller.currentCameraMotor.GetComponent<GameCreator.Camera.CameraMotorTypeFirstPerson>();
                    if (Input.GetKey(KeyCode.A))
                    {

                        

                        rotation.AddRotation(0.0f, -Sensitivity);


                    }

                    if (Input.GetKey(KeyCode.D))
                    {

                        

                        rotation.AddRotation(0.0f, Sensitivity);


                    }

                   
                }

           }

        }
    }
}