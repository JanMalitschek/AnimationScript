using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace AnimationScript{
    [System.Serializable]
    public class Timeline
    {
        public List<TimelineClip> targetClips = new List<TimelineClip>();

        public bool ContainsClipForTarget(string target){
            return targetClips.Any(x => x.target == target); 
        }
        public TimelineClip GetClipForTarget(string target){
            return targetClips.Find(x => x.target == target);
        }
    }
}