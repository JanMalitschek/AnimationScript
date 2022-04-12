using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

namespace AnimationScript{
    public class Lexer
    {
        public static Token[] Lexicalize(string[] lines){
            List<Token> tokens = new List<Token>();
            for(int line = 0; line < lines.Length; line++){
                string currentWord = string.Empty;
                for(int col = 0; col < lines[line].Length; col++){
                    if(currentWord == string.Empty){
                        if(lines[line][col] != ' ')
                            currentWord += lines[line][col];
                    }
                    else{
                        if(lines[line][col] != ' '){
                            currentWord += lines[line][col];
                            if(col == (lines[line].Length - 1)){
                                tokens.Add(new Token(currentWord, (uint)line + 1, (uint)col));
                                currentWord = string.Empty;
                            }
                        }
                        else if(lines[line][col] == ' ' || col == (lines[line].Length - 1)){
                            //Word completed
                            if(currentWord.StartsWith("#"))
                                goto Comment;
                            else{
                                tokens.Add(new Token(currentWord, (uint)line + 1, (uint)col));
                            }
                            currentWord = string.Empty;
                        }
                    }
                }
                Comment:
                continue;
            }
            return tokens.ToArray();
        }
    }
}