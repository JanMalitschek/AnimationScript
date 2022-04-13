using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimationScript{
    public class AnimationScriptRuntime : MonoBehaviour
    {
        public AnimationScriptModule module;
        public List<RuntimeTarget> targets = new List<RuntimeTarget>();
    }
}