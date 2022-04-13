using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace AnimationScript{
    public class AnimationScriptRuntime : MonoBehaviour
    {
        public AnimationScriptModule module;
        public List<RuntimeTarget> targets = new List<RuntimeTarget>();

        public void FindTargets(){
            targets.Clear();
            if(module){
                foreach(TimelineClip c in module.timeline.targetClips)
                    targets.Add(new RuntimeTarget(c.target));
            }
        }

        public void FindPropertyInfos(){
            foreach(RuntimeTarget t in targets){
                foreach(Component c in t.target.GetComponents<Component>()){
                    
                }
            }
        }
    }
}