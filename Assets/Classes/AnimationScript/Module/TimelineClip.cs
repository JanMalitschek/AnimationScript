using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimationScript{
    [System.Serializable]
    public class TimelineClip
    {
        public string target;

        public TimelineClip(string target){
            this.target = target;
        }

        [System.Serializable]
        public class AnimatedProperty{
            public string propertyName;
            public List<Keyframe> keyframes = new List<Keyframe>();
            public AnimatedProperty(string propertyName){
                this.propertyName = propertyName;
            }
            public void AddKeyframe(Keyframe keyframe){
                if(keyframes.Count == 0){
                    keyframes.Add(keyframe);
                    return;
                }
                if(keyframe.time < keyframes[0].time){
                    keyframes.Insert(0, keyframe);
                    return;   
                }
                if(keyframe.time > keyframes[keyframes.Count - 1].time){
                    keyframes.Add(keyframe);
                    return;
                }
                for(int i = 0; i < keyframes.Count - 1; i++){
                    if(keyframe.time > keyframes[i].time && keyframe.time < keyframes[i + 1].time){
                        keyframes.Insert(i + 1, keyframe);
                        return;
                    }
                    else if(keyframe.time == keyframes[i].time){
                        keyframes[i].value = keyframe.value;
                        return;
                    }
                }
            }
        }
        public List<AnimatedProperty> animatedProperties = new List<AnimatedProperty>();
        private AnimatedProperty GetOrCreateAnimatedProperty(string name){
            AnimatedProperty target = animatedProperties.Find(x => x.propertyName == name);
            if(target != null)
                return target;
            target = new AnimatedProperty(name);
            animatedProperties.Add(target);
            return target;
        }

        public void AddKeyframe(ValueToken property, ValueToken timeToken){
            switch(property.type){
                case ValueToken.ValueType.NumericProperty: AddNumericKeyframe(property, timeToken); break;
                case ValueToken.ValueType.VectorProperty: AddVectorKeyframe(property, timeToken); break;
                case ValueToken.ValueType.QuaternionProperty: AddQuaternionKeyframe(property, timeToken); break;
            }
        }
        private void AddNumericKeyframe(ValueToken property, ValueToken timeToken){
            AnimatedProperty target = GetOrCreateAnimatedProperty(property.targetValue);
            target.AddKeyframe(Keyframe.FromTimeToken(timeToken, property.numericValue));
        }
        private void AddVectorKeyframe(ValueToken property, ValueToken timeToken){
            AnimatedProperty xTarget = GetOrCreateAnimatedProperty(property.targetValue + ".x");
            AnimatedProperty yTarget = GetOrCreateAnimatedProperty(property.targetValue + ".y");
            AnimatedProperty zTarget = GetOrCreateAnimatedProperty(property.targetValue + ".z");
            xTarget.AddKeyframe(Keyframe.FromTimeToken(timeToken, property.vectorValue.x));
            yTarget.AddKeyframe(Keyframe.FromTimeToken(timeToken, property.vectorValue.y));
            zTarget.AddKeyframe(Keyframe.FromTimeToken(timeToken, property.vectorValue.z));
        }
        private void AddQuaternionKeyframe(ValueToken property, ValueToken timeToken){
            AnimatedProperty xTarget = GetOrCreateAnimatedProperty(property.targetValue + ".x");
            AnimatedProperty yTarget = GetOrCreateAnimatedProperty(property.targetValue + ".y");
            AnimatedProperty zTarget = GetOrCreateAnimatedProperty(property.targetValue + ".z");
            AnimatedProperty wTarget = GetOrCreateAnimatedProperty(property.targetValue + ".w");
            xTarget.AddKeyframe(Keyframe.FromTimeToken(timeToken, property.quaternionValue.x));
            yTarget.AddKeyframe(Keyframe.FromTimeToken(timeToken, property.quaternionValue.y));
            zTarget.AddKeyframe(Keyframe.FromTimeToken(timeToken, property.quaternionValue.z));
            wTarget.AddKeyframe(Keyframe.FromTimeToken(timeToken, property.quaternionValue.w));
        }
    }
}