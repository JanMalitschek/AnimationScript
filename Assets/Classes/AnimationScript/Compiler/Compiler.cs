using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace AnimationScript{
    public class Compiler
    {
        public CompilerResult CompilerResult {get; private set;}
        private Stack<ValueToken> stack = new Stack<ValueToken>();
        private enum Mode{
            Targets,
            Keys,
            Constraints
        }
        private Mode mode = Mode.Targets;

        public Compiler(string filePath){
            CompilerResult = new CompilerResult(filePath);
        }

        public bool Compile(string path, AnimationScriptModule targetModule){
            string[] lines = File.ReadAllLines(path);
            Token[] tokens = Lexer.Lexicalize(lines);
            foreach(Token t in tokens){
                switch(mode){
                    case Mode.Targets:
                        switch(t.type){
                            case Token.TokenType.Target:
                                if(targetModule.timeline.ContainsClipForTarget(t.content)){
                                    CompilerResult.AddError(t.line, t.column, $"Target <b>{t.content}</b> has already been defined!");
                                    return false;
                                }
                                targetModule.timeline.targetClips.Add(new TimelineClip(t.content));
                                break;

                            case Token.TokenType.targets: mode = Mode.Targets; break;
                            case Token.TokenType.keys: mode = Mode.Keys; break;
                            case Token.TokenType.constraints: mode = Mode.Constraints; break;

                            default:
                                CompilerResult.AddError(t.line, t.column, $"Unexpected Token <b>{t.rawContent}</b>!\nOnly targets may be defined in the <b>targets</b> section."); 
                                return false;
                        }
                    break;
                    case Mode.Keys:
                        switch(t.type){
                            case Token.TokenType.Number: stack.Push(ValueToken.MakeNumber(t.GetNumericValue())); break;
                            case Token.TokenType.Time: stack.Push(ValueToken.MakeTime(t.GetNumericValue())); break;
                            case Token.TokenType.Frame: stack.Push(ValueToken.MakeFrame(t.GetNumericValue())); break;
                            case Token.TokenType.Percentage: stack.Push(ValueToken.MakePercentage(t.GetNumericValue())); break;
                            case Token.TokenType.Target:
                                if(!targetModule.timeline.ContainsClipForTarget(t.content)){
                                    CompilerResult.AddError(t.line, t.column, $"Target <b>{t.rawContent}</b> was never defined!");
                                    return false;
                                }
                                stack.Push(ValueToken.MakeTarget(t.content));
                                break;

                            case Token.TokenType.vec: if(!TokenHandlers.vecHandler(t, ref stack, CompilerResult)) return false; break;
                            case Token.TokenType.eul: if(!TokenHandlers.eulHandler(t, ref stack, CompilerResult)) return false; break;

                            case Token.TokenType.Property: if(!TokenHandlers.propertyHandler(t, ref stack, CompilerResult)) return false; break;

                            case Token.TokenType.key: if(!TokenHandlers.keyHandler(t, ref stack, CompilerResult, targetModule)) return false; break;

                            case Token.TokenType.targets: mode = Mode.Targets; break;
                            case Token.TokenType.keys: mode = Mode.Keys; break;
                            case Token.TokenType.constraints: mode = Mode.Constraints; break;
                        }
                    break;
                    case Mode.Constraints:

                    break;
                }
            }
            return true;
        }
    }
}