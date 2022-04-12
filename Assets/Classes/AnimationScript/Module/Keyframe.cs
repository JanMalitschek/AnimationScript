using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimationScript{
    [System.Serializable]
    public class Keyframe
    {
        public float time;
        public float value;

        public Keyframe(float time, float value){
            this.time = time;
            this.value = value;
        }
        public static Keyframe FromFramenumber(float frame, float framerate, float value){
            return new Keyframe(frame * (1.0f / framerate), value);
        }
        public static Keyframe FromPercentage(float percentage, float duration, float value){
            return new Keyframe(duration * percentage, value);
        }
        public static Keyframe FromTimeToken(ValueToken timeToken, float value){
            switch(timeToken.type){
                case ValueToken.ValueType.Time: return new Keyframe(timeToken.numericValue, value);
                //@TODO: where does framerate come from?
                case ValueToken.ValueType.Frame: return FromFramenumber(timeToken.numericValue, 24.0f, value);
                //@TODO: where does duration come from?
                case ValueToken.ValueType.Percentage: return FromPercentage(timeToken.numericValue, 10.0f, value);
            }
            return new Keyframe(-1.0f, 0.0f);
        }
    }
}