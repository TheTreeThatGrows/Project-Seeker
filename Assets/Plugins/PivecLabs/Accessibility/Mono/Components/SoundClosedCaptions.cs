using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Rendering;
using GameCreator.Core;
using GameCreator.Core.Hooks;
using GameCreator.Localization;


namespace GameCreator.Accessibility
{
    [RequireComponent(typeof(AudioListener))]
    [AddComponentMenu("Game Creator/Accessibility/Sound Closed Captions", 100)]

  
    public class SoundClosedCaptions : MonoBehaviour
    {
        // PROPERTIES: -------------------------------------------------------------------------------------------------

        [HideInInspector]
        public bool subtitlesOn = true;
        [HideInInspector]
        public Color color = Color.white;
        [HideInInspector]
        public float timeShowing = 2.0f;
        [HideInInspector]
        public float timeBetweenScans = 0.0f;
 
        [System.Serializable]
        public class soundSubtitles
        {
            public bool enabled = true;

            public AudioSource audioSource;

            public String soundTitle;
                      
            public float distancetosound;

        }

        public List<soundSubtitles> soundsToHear = new List<soundSubtitles>();

        private AudioSource[] soundsArray;
     
        private string forward = "forward : ";
        private string forwardL = "forward on left :  ";
        private string forwardR = "forward on right :  ";
        private string back = "back :  ";
        private string backL = "back on left :  ";
        private string backR = "back on right :  ";
        private string left =  "on left :  ";
        private string right = "on right :  ";
        private string distance;
        private string direction;
        private string units = " meters ";
        private string unit = " meter ";
        private string ccmessage = "[ Sound ]  ";

        private static SoundClosedCaptions localInstance;

        public static SoundClosedCaptions cc { get { return localInstance; } }

        private Coroutine lastRoutine = null;

 

        // EXECUTABLE: ----------------------------------------------------------------------------

        private void Awake()
        {

            if (localInstance != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                localInstance = this;
            }
            soundsArray = Resources.FindObjectsOfTypeAll<AudioSource>();

            for (int i = 0; i < this.soundsArray.Length; ++i)
            {
                soundsToHear.Add(new soundSubtitles());

                soundsToHear[i].audioSource = soundsArray[i];

                soundsToHear[i].enabled = soundsArray[i].enabled;

                soundsToHear[i].soundTitle = soundsToHear[i].audioSource.clip.name;

                soundsToHear[i].distancetosound = soundsToHear[i].audioSource.maxDistance;
            }


        }



        public static void startCC()
        {
             cc.lastRoutine = cc.StartCoroutine(cc.subtitleMessage());
          
        }

        public static void stopCC()
        {

            cc.StopCoroutine(cc.lastRoutine);
            GameCreator.Messages.SimpleMessageManager.Instance.HideText();
        }


        void postMessage(string item)
        {
            string message = ccmessage + distance + direction + item;

            GameCreator.Messages.SimpleMessageManager.Instance.ShowText(message, this.color);

        }


       private IEnumerator subtitleMessage()
        {
            while (subtitlesOn)
            {


                for (int i = 0; i < this.soundsToHear.Count; ++i)
                {
          
                    if (soundsToHear[i].enabled == true)
                    {

                        if (soundsToHear[i].audioSource.isPlaying)

                        {
                            if (soundsToHear[i].distancetosound > Vector3.Distance(this.transform.position, soundsToHear[i].audioSource.transform.position))

                            {
                                float dist = Vector3.Distance(this.transform.position, soundsToHear[i].audioSource.transform.position);
                                if (dist < 1.5) distance = dist.ToString("F0") + unit;
                                    else distance = dist.ToString("F0") + units;

                                Vector3 localPos = this.transform.InverseTransformPoint(soundsToHear[i].audioSource.transform.position);

                                if ((localPos.x < -1.0f) && (localPos.z > 1.0f))
                                {
                                    direction = forwardL;
        
                                }
                                else if ((localPos.x > 1.0f) && (localPos.z > 1.0f))
                                {
                                    direction = forwardR;
                                 }

                                else if ((localPos.x < -1.0f) && (localPos.z < -1.0f))
                                {
                                    direction = backL;
                                }
                                else if ((localPos.x > 1.0f) && (localPos.z < -1.0f))
                                {
                                    direction = backR;
                                }

                                else if ((localPos.x < -1.0f) && (localPos.z > -1.0f) && (localPos.z < 1.0f))
                                {
                                    direction = left;
                                }
                                else if ((localPos.x > 1.0f) && (localPos.z > -1.0f) && (localPos.z < 1.0f))
                                {
                                    direction = right;
                                }
                                else if (localPos.z < 0)
                                {
                                    direction = back;
                                }

                                else if (localPos.z > 0)
                                {
                                    direction = forward;
                                }

                                postMessage(soundsToHear[i].soundTitle);
                              
                                yield return new WaitForSeconds(timeShowing);

                            }
                            else
                                GameCreator.Messages.SimpleMessageManager.Instance.ShowText("Closed Captions ON", Color.gray);


                        }

                    }

                }

                yield return new WaitForSeconds(timeBetweenScans);
            }
              
       }


    }


}

