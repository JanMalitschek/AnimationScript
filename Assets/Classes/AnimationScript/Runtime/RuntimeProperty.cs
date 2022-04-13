using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace AnimationScript{
    public class RuntimeProperty
    {
        public enum Type{
            Number,
            Vector_X,
            Vector_Y,
            Vector_Z,
            Quaternion_X,
            Quaternion_Y,
            Quaternion_Z,
            Quaternion_W
        }
        public Type propertyType;
        public Component component;
        public PropertyInfo property;

        public static readonly List<System.Type> KeyableTypes = new List<System.Type>{
            typeof(int),
            typeof(float),
            typeof(double),
            typeof(Vector3),
            typeof(Quaternion)
        };

        public RuntimeProperty(Component component, string propertyName){
            string[] splitName = propertyName.Split('.');
            string name = splitName[0];
            string componentName = splitName.Length > 1 ? splitName[1] : "_";
            foreach(PropertyInfo i in component.GetType().GetProperties())
                if(KeyableTypes.Contains(i.PropertyType) && i.Name.ToLower().Equals(name)){
                    this.property = i;
                    this.component = component;
                    if(componentName == "_")
                        propertyType = Type.Number;
                    else{
                        if(i.PropertyType == typeof(Vector3)){
                            switch(componentName){
                                case "x": propertyType = Type.Vector_X; break;
                                case "y": propertyType = Type.Vector_Y; break;
                                case "z": propertyType = Type.Vector_Z; break;
                            }
                        }
                        else if(i.PropertyType == typeof(Quaternion)){
                            switch(componentName){
                                case "x": propertyType = Type.Quaternion_X; break;
                                case "y": propertyType = Type.Quaternion_Y; break;
                                case "z": propertyType = Type.Quaternion_Z; break;
                                case "w": propertyType = Type.Quaternion_W; break;
                            }
                        }
                    }
                    return;
                }
        }

        public float GetValue(){
            switch(propertyType){
                case Type.Number: return GetNumberValue();
                case Type.Vector_X: return GetVectorValue().x;
                case Type.Vector_Y: return GetVectorValue().y;
                case Type.Vector_Z: return GetVectorValue().z;
                case Type.Quaternion_X: return GetQuaternionValue().x;
                case Type.Quaternion_Y: return GetQuaternionValue().y;
                case Type.Quaternion_Z: return GetQuaternionValue().z;
                case Type.Quaternion_W: return GetQuaternionValue().w;
                default: return 0.0f;
            }
        }

        private float GetNumberValue(){
            if(propertyType != Type.Number)
                return 0.0f;
            return (float)property.GetValue(component);
        }
        private void SetNumberValue(float value){
            if(propertyType != Type.Number)
                return;
            if(property.PropertyType == typeof(int))
                property.SetValue(component, (int)value);
            else if(property.PropertyType == typeof(float))
                property.SetValue(component, (float)value);
            else if(property.PropertyType == typeof(double))
                property.SetValue(component, (double)value);
        }

        private Vector3 GetVectorValue(){
            return (Vector3)property.GetValue(component);
        }
        private void SetVectorValue(Vector3 value){
            property.SetValue(component, value);
        }
        private void SetVectorXValue(float value){
            Vector3 currentValue = (Vector3)property.GetValue(component);
            currentValue.x = value;
            property.SetValue(component, currentValue);
        }
        private void SetVectorYValue(float value){
            Vector3 currentValue = (Vector3)property.GetValue(component);
            currentValue.y = value;
            property.SetValue(component, currentValue);
        }
        private void SetVectorZValue(float value){
            Vector3 currentValue = (Vector3)property.GetValue(component);
            currentValue.z = value;
            property.SetValue(component, currentValue);
        }

        private Quaternion GetQuaternionValue(){
            return (Quaternion)property.GetValue(component);
        }
        private void SetQuaternionValue(Quaternion value){
            property.SetValue(component, value);
        }
        private void SetQuaternionXValue(float value){
            Quaternion currentValue = (Quaternion)property.GetValue(component);
            currentValue.x = value;
            property.SetValue(component, currentValue);
        }
        private void SetQuaternionYValue(float value){
            Quaternion currentValue = (Quaternion)property.GetValue(component);
            currentValue.y = value;
            property.SetValue(component, currentValue);
        }
        private void SetQuaternionZValue(float value){
            Quaternion currentValue = (Quaternion)property.GetValue(component);
            currentValue.z = value;
            property.SetValue(component, currentValue);
        }
        private void SetQuaternionWValue(float value){
            Quaternion currentValue = (Quaternion)property.GetValue(component);
            currentValue.w = value;
            property.SetValue(component, currentValue);
        }
    }
}