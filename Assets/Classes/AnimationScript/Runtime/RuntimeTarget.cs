using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimationScript{
    [System.Serializable]
    public class RuntimeTarget
    {
        public string name;
        public GameObject target;

        public RuntimeTarget(string name){
            this.name = name;
        }
    }
}