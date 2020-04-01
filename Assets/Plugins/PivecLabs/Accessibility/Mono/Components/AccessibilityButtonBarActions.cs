namespace GameCreator.Accessibility
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using GameCreator.Core;
    using GameCreator.Variables;

    public class AccessibilityButtonBarActions : MonoBehaviour
    {
        public static GameObject A;
        public static GameObject B;
        public static GameObject C;
        public static GameObject D;
        public static GameObject E;

        private void Start()
        {
            A = GameObject.Find("ATSButtonA");
            B = GameObject.Find("ATSButtonB");
            C = GameObject.Find("ATSButtonC");
            D = GameObject.Find("ATSButtonD");
            E = GameObject.Find("ATSButtonE");
        }

        public void callActionsA()
        {
            GameObject buttonA = GameObject.Find("ButtonBarActions");
            if (buttonA != null) buttonA.GetComponent<Actions>().Execute(A);
           
        }
        public void callActionsB()
        {
            GameObject buttonB = GameObject.Find("ButtonBarActions");
            if (buttonB != null) buttonB.GetComponent<Actions>().Execute(B);
        }
        public void callActionsC()
        {
            GameObject buttonC = GameObject.Find("ButtonBarActions");
            if (buttonC != null) buttonC.GetComponent<Actions>().Execute(C);
        }
        public void callActionsD()
        {
            GameObject buttonD = GameObject.Find("ButtonBarActions");
            if (buttonD != null) buttonD.GetComponent<Actions>().Execute(D);
        }
        public void callActionsE()
        {
            GameObject buttonE = GameObject.Find("ButtonBarActions");
            if (buttonE != null) buttonE.GetComponent<Actions>().Execute(E);
        }

    }

}