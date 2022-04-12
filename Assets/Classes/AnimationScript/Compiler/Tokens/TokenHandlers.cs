using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimationScript{
    public static class TokenHandlers{
        public static bool TryPop(ref Stack<ValueToken> stack, out ValueToken token){
            if(stack.Count > 0){
                token = stack.Pop();
                return true;
            }
            token = null;
            return false;
        }
        public static bool TryPopOfType(ref Stack<ValueToken> stack, ValueToken.ValueType type, out ValueToken tokenOfType, CompilerResult compilerResult, Token sourceToken, string stackEmptyError, string typeMismatchError){
            if(TryPop(ref stack, out ValueToken t)){
                if(t.type != type){
                    compilerResult.AddError(sourceToken.line, sourceToken.column, typeMismatchError);
                    tokenOfType = null;
                    return false;
                }
                tokenOfType = t;
                return true;
            }
            else{
                compilerResult.AddError(sourceToken.line, sourceToken.column, stackEmptyError);
                tokenOfType = null;
                return false;
            }
        }
        public static bool TryPopAllOfType(ref Stack<ValueToken> stack, ValueToken.ValueType type, out List<ValueToken> tokensOfType, CompilerResult compilerResult, Token sourceToken, string noneFoundWarning){
            tokensOfType = new List<ValueToken>();
            while(stack.Count > 0 && stack.Peek().type == type)
                tokensOfType.Add(stack.Pop());
            if(tokensOfType.Count > 0)
                return true;
            compilerResult.AddWarning(sourceToken.line, sourceToken.column, noneFoundWarning);
            return false;
        }
        public static bool TryPopTimeType(ref Stack<ValueToken> stack, out ValueToken timeToken, CompilerResult compilerResult, Token sourceToken, string stackEmptyError, string typeMismatchError){
            if(TryPop(ref stack, out ValueToken t)){
                if(t.type != ValueToken.ValueType.Time && t.type != ValueToken.ValueType.Frame && t.type != ValueToken.ValueType.Percentage){
                    compilerResult.AddError(sourceToken.line, sourceToken.column, typeMismatchError);
                    timeToken = t;
                    return false;
                }
                timeToken = t;
                return true;
            }
            else{
                compilerResult.AddError(sourceToken.line, sourceToken.column, stackEmptyError);
                timeToken = null;
                return false;
            }
        }
        public static bool TryPopKeyableType(ref Stack<ValueToken> stack, out ValueToken keyableTypeToken, CompilerResult compilerResult, Token sourceToken, string stackEmptyError, string typeMismatchError){
            if(TryPop(ref stack, out ValueToken t)){
                if(t.type != ValueToken.ValueType.Number && t.type != ValueToken.ValueType.Vector && t.type != ValueToken.ValueType.Quaternion){
                    compilerResult.AddError(sourceToken.line, sourceToken.column, typeMismatchError);
                    keyableTypeToken = t;
                    return false;
                }
                keyableTypeToken = t;
                return true;
            }
            else{
                compilerResult.AddError(sourceToken.line, sourceToken.column, stackEmptyError);
                keyableTypeToken = null;
                return false;
            }
        }
        public static bool TryPopAllOfPropertyType(ref Stack<ValueToken> stack, out List<ValueToken> tokensOfType, CompilerResult compilerResult, Token sourceToken, string noneFoundWarning){
            tokensOfType = new List<ValueToken>();
            while(stack.Count > 0 && (stack.Peek().type == ValueToken.ValueType.NumericProperty || stack.Peek().type == ValueToken.ValueType.VectorProperty || stack.Peek().type == ValueToken.ValueType.QuaternionProperty))
                tokensOfType.Add(stack.Pop());
            if(tokensOfType.Count > 0)
                return true;
            compilerResult.AddWarning(sourceToken.line, sourceToken.column, noneFoundWarning);
            return false;
        }

        public static bool vecHandler(Token token, ref Stack<ValueToken> stack, CompilerResult compilerResult){
            ValueToken z, y, x;
            Vector3 vector = Vector3.zero;
            if(!TryPopOfType(ref stack, ValueToken.ValueType.Number, out z, compilerResult, token,
                stackEmptyError: $"<b>vec</b> must be preceeded by three numbers!",
                typeMismatchError: $"Z component must be of type <b>Number</b>!"))
                return false;
            if(!TryPopOfType(ref stack, ValueToken.ValueType.Number, out y, compilerResult, token,
                stackEmptyError: $"<b>vec</b> must be preceeded by three numbers!",
                typeMismatchError: $"Y component must be of type <b>Number</b>!"))
                return false;
            if(!TryPopOfType(ref stack, ValueToken.ValueType.Number, out x, compilerResult, token,
                stackEmptyError: $"<b>vec</b> must be preceeded by three numbers!",
                typeMismatchError: $"X component must be of type <b>Number</b>!"))
                return false;
            stack.Push(ValueToken.MakeVector(vector));
            return true;
        }

        public static bool eulHandler(Token token, ref Stack<ValueToken> stack, CompilerResult compilerResult){
            ValueToken z, y, x;
            Vector3 vector = Vector3.zero;
            if(!TryPopOfType(ref stack, ValueToken.ValueType.Number, out z, compilerResult, token,
                stackEmptyError: $"<b>vec</b> must be preceeded by three numbers!",
                typeMismatchError: $"Z component must be of type <b>Number</b>!"))
                return false;
            if(!TryPopOfType(ref stack, ValueToken.ValueType.Number, out y, compilerResult, token,
                stackEmptyError: $"<b>vec</b> must be preceeded by three numbers!",
                typeMismatchError: $"Y component must be of type <b>Number</b>!"))
                return false;
            if(!TryPopOfType(ref stack, ValueToken.ValueType.Number, out x, compilerResult, token,
                stackEmptyError: $"<b>vec</b> must be preceeded by three numbers!",
                typeMismatchError: $"X component must be of type <b>Number</b>!"))
                return false;
            stack.Push(ValueToken.MakeQuaternion(Quaternion.Euler(vector)));
            return true;
        }

        public static bool propertyHandler(Token token, ref Stack<ValueToken> stack, CompilerResult compilerResult){
            ValueToken keyableTypeToken;
            if(!TryPopKeyableType(ref stack, out keyableTypeToken, compilerResult, token,
                stackEmptyError: $"No value was provided to property <b>{token.rawContent}</b>!",
                typeMismatchError: $"No keyable type was provided to property <b>{token.rawContent}</b>!\nKeyable types include <b>Number</b>, <b>Vector</b> and <b>Quaternion</b>."))
                return false;
            switch(keyableTypeToken.type){
                case ValueToken.ValueType.Number: stack.Push(ValueToken.MakeNumericProperty(token.content, keyableTypeToken.numericValue)); return true;
                case ValueToken.ValueType.Vector: stack.Push(ValueToken.MakeVectorProperty(token.content, keyableTypeToken.vectorValue)); return true;
                case ValueToken.ValueType.Quaternion: stack.Push(ValueToken.MakeQuaternionProperty(token.content, keyableTypeToken.quaternionValue)); return true;
            }
            return false;
        }

        public static bool keyHandler(Token token, ref Stack<ValueToken> stack, CompilerResult compilerResult, AnimationScriptModule targetModule){
            ValueToken time;
            List<ValueToken> properties;
            ValueToken target;
            if(!TryPopTimeType(ref stack, out time, compilerResult, token,
                stackEmptyError: "No timestamp was provided to <b>key</b>!",
                typeMismatchError: "<b>key</b> timestamp must be of type <b>Time</b>, <b>Frame</b> or <b>Percentage</b>!"))
                return false;
            if(!TryPopAllOfPropertyType(ref stack, out properties, compilerResult, token,
                noneFoundWarning: "No properties were provided to <b>key</b>!"))
                return false;
            if(!TryPopOfType(ref stack, ValueToken.ValueType.Target, out target, compilerResult, token,
                stackEmptyError: "No target was provided to <b>key</b>!",
                typeMismatchError: "<b>key</b> target must be of type <b>Target</b>!"))
                return false;
            TimelineClip clip = targetModule.timeline.GetClipForTarget(target.targetValue);
            foreach(ValueToken property in properties)
                clip.AddKeyframe(property, time);
            return true;
        }
    }
}