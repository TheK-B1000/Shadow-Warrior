﻿using UnityEngine;
using System.Collections;
using System;

namespace MalbersAnimations
{
    public class ActionZone : MonoBehaviour
    {
        public Actions actionsToUse;
        [HideInInspector]  public int ID;
        [HideInInspector]  public int index;
   
        public bool HeadOnly;

        void OnTriggerEnter(Collider other)
        {
            if (HeadOnly)
            {
                if (other.name.ToLower().Contains("head"))
                {
                    other.SendMessageUpwards("ActionEmotion", ID, SendMessageOptions.DontRequireReceiver);
                }
            }
            else
            {
                other.SendMessageUpwards("ActionEmotion", ID, SendMessageOptions.DontRequireReceiver);
            }
        }

        void OnTriggerExit(Collider other)
        {
            other.SendMessageUpwards("ActionEmotion", -1, SendMessageOptions.DontRequireReceiver);
        }
    }
}