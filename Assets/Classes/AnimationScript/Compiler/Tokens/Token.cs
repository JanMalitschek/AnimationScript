using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

namespace AnimationScript{
    [System.Serializable]
    public class Token{
        public enum TokenType{
            //Types with content
            Number,
            Time,
            Frame,
            Percentage,
            Target,
            Property,
            //Fixed Types
            targets,
            keys,
            constraints,
            vec,
            eul,
            key
        }
        public TokenType type;
        public int line, column;
        public string rawContent;
        public string content;
        public Token(string rawToken, int line, int column){
            ParseRawToken(rawToken);
            this.line = line;
            this.column = column;
            this.rawContent = rawToken;
        }

        private void ParseRawToken(string rawToken){
            switch(rawToken){
                case "targets": type = TokenType.targets; break;
                case "keys": type = TokenType.keys; break;
                case "constraints": type = TokenType.constraints; break;
                case "vec": type = TokenType.vec; break;
                case "eul": type = TokenType.eul; break;
                case "loc":
                case "rot":
                case "scl": type = TokenType.Property; content = rawToken; break;
                case "key": type = TokenType.key; break;
                default: ParseContentToken(rawToken); break;
            }
        }
        private void ParseContentToken(string rawToken){
            if(Regex.Match(rawToken, @"([0-9]+)([.,][0-9]+)?([sf%]?)").Success){
                if(rawToken.EndsWith("s")){
                    type = TokenType.Time;
                    content = rawToken.TrimEnd('s');
                }
                else if(rawToken.EndsWith("f")){
                    type = TokenType.Frame;
                    content = rawToken.TrimEnd('f');
                }
                else if(rawToken.EndsWith("%")){
                    type = TokenType.Frame;
                    content = rawToken.TrimEnd('%');
                }
                else{
                    type = TokenType.Number;
                    content = rawToken;
                }
            }
            else if(Regex.Match(rawToken, @"[a-z]+(\.[a-z]+)+").Success){
                type = TokenType.Property;
                content = rawToken;
                Debug.Log(rawToken);
            }
            else{
                type = TokenType.Target;
                content = rawToken;
            }
        }

        public float GetNumericValue(){
            if(float.TryParse(content, out float result))
                return result;
            return 0.0f;
        }
    }
}