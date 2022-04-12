using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimationScript{
    public class ValueToken
    {
        public enum ValueType{
            Number,
            Time,
            Frame,
            Percentage,
            Vector,
            Quaternion,
            Target,
            NumericProperty,
            VectorProperty,
            QuaternionProperty
        }   
        public ValueType type;

        public float numericValue = 0.0f;
        public string targetValue = string.Empty;
        public Vector3 vectorValue = Vector3.zero;
        public Quaternion quaternionValue = Quaternion.identity;

        private ValueToken(ValueType type, float numericValue, string targetValue, Vector3 vectorValue, Quaternion quaternionValue){
            this.type = type;
            this.numericValue = numericValue;
            this.targetValue = targetValue;
            this.vectorValue = vectorValue;
            this.quaternionValue = quaternionValue;
        }

        public static ValueToken MakeNumber(float value) => new ValueToken(ValueType.Number, value, string.Empty, Vector3.zero, Quaternion.identity);
        public static ValueToken MakeTime(float value) => new ValueToken(ValueType.Time, value, string.Empty, Vector3.zero, Quaternion.identity);
        public static ValueToken MakeFrame(float value) => new ValueToken(ValueType.Frame, value, string.Empty, Vector3.zero, Quaternion.identity);
        public static ValueToken MakePercentage(float value) => new ValueToken(ValueType.Percentage, value, string.Empty, Vector3.zero, Quaternion.identity);
        public static ValueToken MakeVector(Vector3 value) => new ValueToken(ValueType.Vector, 0.0f, string.Empty, value, Quaternion.identity);
        public static ValueToken MakeQuaternion(Quaternion value) => new ValueToken(ValueType.Quaternion, 0.0f, string.Empty, Vector3.zero, value);
        public static ValueToken MakeTarget(string value) => new ValueToken(ValueType.Target, 0.0f, value, Vector3.zero, Quaternion.identity);
        public static ValueToken MakeNumericProperty(string name, float value) => new ValueToken(ValueType.NumericProperty, value, PropertyShorthands.ResolveShorthand(name), Vector3.zero, Quaternion.identity);
        public static ValueToken MakeVectorProperty(string name, Vector3 value) => new ValueToken(ValueType.VectorProperty, 0.0f, PropertyShorthands.ResolveShorthand(name), value, Quaternion.identity);
        public static ValueToken MakeQuaternionProperty(string name, Quaternion value) => new ValueToken(ValueType.QuaternionProperty, 0.0f, PropertyShorthands.ResolveShorthand(name), Vector3.zero, value);
    }
}