
namespace GameCreator.Accessibility
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using GameCreator.Core;
    using GameCreator.Core.Hooks;

    [RequireComponent(typeof(HookCamera))]
    [AddComponentMenu("Game Creator/Accessibility/Camera Component Colour", 100)]

    public class CameraColourCorrecting : MonoBehaviour
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        private Material materialCorrecting = null;

        private Shader ShaderCorrecting;
        [HideInInspector] public float hue = 1.0f;
        [HideInInspector] public float saturation = 1.0f;
        [HideInInspector] public float brightness = 1.0f;
        [HideInInspector] public float contrast = 1.0f;

        [HideInInspector] public bool correcting = false;

        // INITIALIZERS: ----------------------------------------------------------------------------
      

        private Material material2
        {
            get
            {
                materialCorrecting = CreateMaterial(ShaderCorrecting, materialCorrecting);
                return materialCorrecting;
            }
        }
        protected Material CreateMaterial(Shader shader, Material material)
        {
            if (shader == null)
            {
                return null;
            }

            if (shader.isSupported && material && material.shader == shader)
                return material;

            if (!shader.isSupported)
            {
                return null;
            }
            else
            {
                material = new Material(shader);
                material.hideFlags = HideFlags.DontSave;
                if (material)
                    return material;
                else
                    return null;
            }
        }

        // METHODS: ----------------------------------------------------------------------------
   
        void OnRenderImage (RenderTexture src, RenderTexture dest) 
		{


            ShaderCorrecting = Shader.Find("Hidden/ColourCorrecting");

            if (material2 != null && correcting == true)
            {

                 material2.SetFloat("_Brightness", brightness);
                 material2.SetFloat("_Saturation", saturation);
                 material2.SetFloat("_Contrast", contrast);
                 material2.SetFloat("_Hue", hue / 360);

                Graphics.Blit(src, dest, material2);
             
            }

            else 
			{
				Graphics.Blit(src, dest);
               
            }
		}

    }
}
