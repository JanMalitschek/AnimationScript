using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimationScript{
    public static class PropertyShorthands
    {
        public static string ResolveShorthand(string shorthand){
            switch(shorthand){
                case "loc": return "transform.position";
                case "rot": return "transform.rotation";
                case "scl": return "transform.scale";
                default: return shorthand;
            }
        }   
    }
}