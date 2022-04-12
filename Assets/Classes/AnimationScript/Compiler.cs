using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace AnimationScript{
    public class Compiler
    {
        public CompilerResult CompilerResult {get; private set;}
        private Stack<Token> tokenStack = new Stack<Token>();

        public bool Compile(string path){
            string[] lines = File.ReadAllLines(path);
            Token[] tokens = Lexer.Lexicalize(lines);
            return true;
        }
    }
}