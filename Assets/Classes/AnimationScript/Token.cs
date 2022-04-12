using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

namespace AnimationScript{
    public class Token{
        public enum TokenType{
            //Types with content
            Number,
            Time,
            Frame,
            Percentage,
            Target,
            //Fixed Types
            targets,
            keys,
            constraints,
            vec,
            loc,
            rot,
            scl,
            key
        }
        public TokenType type;
        public uint line, column;
        public string content;
        public Token(string rawToken, uint line, uint column){
            ParseRawToken(rawToken);
            this.line = line;
            this.column = column;
        }

        private void ParseRawToken(string rawToken){
            switch(rawToken){
                case "targets": type = TokenType.targets; break;
                case "keys": type = TokenType.keys; break;
                case "constraints": type = TokenType.constraints; break;
                case "vec": type = TokenType.vec; break;
                case "loc": type = TokenType.loc; break;
                case "rot": type = TokenType.rot; break;
                case "scl": type = TokenType.scl; break;
                case "key": type = TokenType.key; break;
                default: ParseContentToken(rawToken); break;
            }
        }
        private void ParseContentToken(string rawToken){
            if(Regex.Match(rawToken, @"([0-9]+)([.,]([0-9]+))?([sf%]?)") != null){
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
            else{
                type = TokenType.Target;
                content = rawToken;
            }
        }
    }
}