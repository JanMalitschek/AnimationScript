using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimationScript{
    public class CompilerResult{
        public class Message{
            public enum Type{
                Info,
                Warning,
                Error
            }
            public Type type = Type.Info;
            public string message;

            public Message(Type type, string message){
                this.type = type;
                this.message = message;
            }
        }
        public List<Message> messages = new List<Message>();
    }
}