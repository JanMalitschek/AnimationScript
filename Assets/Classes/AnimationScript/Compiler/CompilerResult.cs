using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimationScript{
    public class CompilerResult{
        private string filePath;

        public CompilerResult(string path){
            filePath = path;
        }

        public class Message{
            public enum Type{
                Info,
                Warning,
                Error
            }
            public Type type = Type.Info;
            public string path;
            public int line, col;
            public string message;

            public Message(Type type, string path, int line, int col, string message){
                this.type = type;
                this.path = path;
                this.line = line;
                this.col = col;
                this.message = message;
            }

            public string GetMessage(){
                return $"{path}({line},{col}): {message}";
            }
            public void Print(){
                switch(type){
                    case Message.Type.Info: Debug.Log(GetMessage()); break;
                    case Message.Type.Warning: Debug.LogWarning(GetMessage()); break;
                    case Message.Type.Error: Debug.LogError(GetMessage()); break;
                }
            }
        }
        public List<Message> messages = new List<Message>();

        public void AddInfo(int line, int col, string info) => messages.Add(new Message(Message.Type.Info, filePath, line, col, info));
        public void AddWarning(int line, int col, string warning) => messages.Add(new Message(Message.Type.Warning, filePath, line, col, warning));
        public void AddError(int line, int col, string error) => messages.Add(new Message(Message.Type.Error, filePath, line, col, error));

        public void Print(){
            foreach(Message m in messages)
                m.Print();
        }
    }
}