using iuiu.server;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace iuiu.VirtualMachine
{
    public static class RuntimeObjectOffsets
    {
        public const int Tag = 0;
        public const int Marker = 2;
        public const int Value = 4;
        public const int Reference = 8;
    }

    public enum RuntimeObjectMarkers : ushort
    {
        Number = 0xFFF8,
        Tagged = 0xFFF9
    }

    public enum RuntimeObjectType : ushort
    {
        [Description("空")]
        Null,
        [Description("布尔值")]
        Boolean,
        [Description("浮点数")]
        Number,
        [Description("字符串")]
        String,
        [Description("物体")]
        Object,
        [Description("函数")]
        Function,
        [Description("函数集合")]
        FunctionCollection,
        [Description("CLR函数")]
        ClrDelegate,
        [Description("CLR物体")]
        ClrObject,

        Invoke = Function | FunctionCollection
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct RuntimeObject : IEquatable<RuntimeObject>
    {
        [FieldOffset(RuntimeObjectOffsets.Tag)]
        public RuntimeObjectType Type;

        [FieldOffset(RuntimeObjectOffsets.Marker)]
        public RuntimeObjectMarkers Markers;

        [FieldOffset(RuntimeObjectOffsets.Value)]
        public Single Number;

        [FieldOffset(RuntimeObjectOffsets.Value)]
        public IntPtr IntPtr;

        public Boolean Boolean
        {
            get { return Number != 0; }
            set { Number = value ? 1 : 0; }
        }

        [FieldOffset(RuntimeObjectOffsets.Reference)]
        public MemberObject Object;

        [FieldOffset(RuntimeObjectOffsets.Reference)]
        public String String;

        [FieldOffset(RuntimeObjectOffsets.Reference)]
        public Function Function;

        [FieldOffset(RuntimeObjectOffsets.Reference)]
        public Delegate ClrDelegate;

        [FieldOffset(RuntimeObjectOffsets.Reference)]
        public Object ClrObject;

        public static bool operator true(RuntimeObject x)
        {
            return x.Number != 0 ? true : false;
        }

        public static bool operator false(RuntimeObject x)
        {
            return x.Number == 0 ? true : false;
        }

        public static RuntimeObject operator +(RuntimeObject x)
        {
            x.Number = +x.Number;
            return x;
        }

        public static RuntimeObject operator -(RuntimeObject x)
        {
            x.Number = -x.Number;
            return x;
        }

        public static RuntimeObject operator ~(RuntimeObject x)
        {
            x.Number = ~(int)x.Number;
            return x;
        }

        public static RuntimeObject operator !(RuntimeObject x)
        {
            if (x.Number == 0)
            {
                x.Number = 1;
            }
            else
            {
                x.Number = 0;
            }

            return x;
        }

        public static RuntimeObject operator *(RuntimeObject x, RuntimeObject y)
        {
            x.Number = x.Number * y.Number;
            return x;
        }

        public static RuntimeObject operator /(RuntimeObject x, RuntimeObject y)
        {
            x.Number = x.Number / y.Number;
            return x;
        }

        public static RuntimeObject operator +(RuntimeObject x, RuntimeObject y)
        {
            switch (x.Type)
            {
                case RuntimeObjectType.String:

                    switch (y.Type)
                    {
                        case RuntimeObjectType.String:
                            x.String = x.String + y.String;
                            return x;
                        case RuntimeObjectType.Boolean:
                            x.String = x.String + (y.Number != 0 ? "true" : "false");
                            return x;
                        case RuntimeObjectType.Number:
                            x.String = x.String + y.Number;
                            return x;
                    }

                    break;

                case RuntimeObjectType.Boolean:

                    switch (y.Type)
                    {
                        case RuntimeObjectType.String:
                            y.String = (x.Number != 0 ? "true" : "false") + y.String;
                            return y;
                    }

                    break;
                case RuntimeObjectType.Function:

                    switch (y.Type)
                    {
                        case RuntimeObjectType.String:
                            y.String = "func" + y.String;
                            return y;
                        case RuntimeObjectType.Boolean:
                            x.Type = RuntimeObjectType.String;
                            x.String = "func" + (y.Number != 0 ? "true" : "false");
                            return x;
                        case RuntimeObjectType.Function:
                            var fc = new FunctionCollection();
                            fc.Functions.Add(x.Function);
                            fc.Functions.Add(y.Function);
                            x.Type = RuntimeObjectType.FunctionCollection;
                            x.Function = fc;
                            return x;
                        case RuntimeObjectType.FunctionCollection:
                            var fc2 = y.Function as FunctionCollection;
                            var fc3 = new FunctionCollection();
                            fc3.Functions.Add(x.Function);
                            for (var i = 0; i < fc2.Functions.Count; i++)
                                fc3.Functions.Add(fc2.Functions[i]);

                            x.Type = RuntimeObjectType.FunctionCollection;
                            x.Function = fc3;
                            return x;
                        case RuntimeObjectType.Number:
                            x.Type = RuntimeObjectType.String;
                            x.String = "func" + y.Number;
                            return x;
                    }

                    break;
                case RuntimeObjectType.FunctionCollection:

                    switch (y.Type)
                    {
                        case RuntimeObjectType.Function:
                            var fc = x.Function as FunctionCollection;
                            fc.Functions.Add(y.Function);
                            return x;
                        case RuntimeObjectType.FunctionCollection:
                            var fc2 = y.Function as FunctionCollection;
                            var fc3 = x.Function as FunctionCollection;
                            for (var i = 0; i < fc2.Functions.Count; i++)
                                fc3.Functions.Add(fc2.Functions[i]);

                            return x;
                    }

                    break;
                case RuntimeObjectType.Number:

                    switch (y.Type)
                    {
                        case RuntimeObjectType.String:
                            y.String = x.Number + y.String;
                            return y;
                        case RuntimeObjectType.Boolean:
                            x.Type = RuntimeObjectType.String;
                            x.String = x.Number + (y.Number != 0 ? "true" : "false");
                            return x;
                        case RuntimeObjectType.Number:
                            x.Number = x.Number + y.Number;
                            return x;
                    }

                    break;
            }

            throw new NotSupportedException("不支持的操作");
        }

        public static RuntimeObject operator -(RuntimeObject x, RuntimeObject y)
        {
            switch (x.Type)
            {
                case RuntimeObjectType.Number:

                    switch (y.Type)
                    {
                        case RuntimeObjectType.Number:
                            x.Number = x.Number - y.Number;
                            return x;
                    }

                    break;
                case RuntimeObjectType.FunctionCollection:

                    switch (y.Type)
                    {
                        case RuntimeObjectType.Function:

                            var fc = x.Function as FunctionCollection;
                            fc.Functions.Remove(y.Function);
                            switch (fc.Functions.Count)
                            {
                                case 0: return default(RuntimeObject);
                                case 1: x.Function = fc.Functions[0]; x.Type = RuntimeObjectType.Function; return x;
                                default: return x;
                            }

                        case RuntimeObjectType.FunctionCollection:

                            var fc2 = x.Function as FunctionCollection;
                            var fc3 = y.Function as FunctionCollection;

                            for (var i = 0; i < fc3.Functions.Count; i++)
                                fc2.Functions.Remove(fc3.Functions[i]);

                            switch (fc2.Functions.Count)
                            {
                                case 0: return default(RuntimeObject);
                                case 1: x.Function = fc2.Functions[0]; x.Type = RuntimeObjectType.Function; return x;
                                default: return x;
                            }
                    }

                    break;
                case RuntimeObjectType.Function:

                    switch (y.Type)
                    {
                        case RuntimeObjectType.Function:

                            if (x.Equals(y))
                                return default(RuntimeObject);
                            else
                                return x;

                        case RuntimeObjectType.FunctionCollection:

                            var fc = y.Function as FunctionCollection;
                            for (var i = 0; i < fc.Functions.Count; i++)
                                if (x.Function.Equals(fc.Functions[i]))
                                    return default(RuntimeObject);

                            return x;
                    }

                    break;
            }

            throw new NotImplementedException();
        }

        [SpecialName]
        public static RuntimeObject op_LeftShift(RuntimeObject x, RuntimeObject y)
        {
            x.Number = (int)x.Number << (int)y.Number;
            return x;
        }

        [SpecialName]
        public static RuntimeObject op_RightShift(RuntimeObject x, RuntimeObject y)
        {
            x.Number = (int)x.Number >> (int)y.Number;
            return x;
        }

        public static RuntimeObject operator <(RuntimeObject x, RuntimeObject y)
        {
            x.Number = x.Number < y.Number ? 1 : 0;
            x.Type = RuntimeObjectType.Boolean;
            return x;
        }

        public static RuntimeObject operator >(RuntimeObject x, RuntimeObject y)
        {
            x.Number = x.Number > y.Number ? 1 : 0;
            x.Type = RuntimeObjectType.Boolean;
            return x;
        }

        public static RuntimeObject operator <=(RuntimeObject x, RuntimeObject y)
        {
            x.Number = x.Number <= y.Number ? 1 : 0;
            x.Type = RuntimeObjectType.Boolean;
            return x;
        }

        public static RuntimeObject operator >=(RuntimeObject x, RuntimeObject y)
        {
            x.Number = x.Number >= y.Number ? 1 : 0;
            x.Type = RuntimeObjectType.Boolean;
            return x;
        }

        public static RuntimeObject operator ==(RuntimeObject x, RuntimeObject y)
        {
            RuntimeObject o = new RuntimeObject();
            o.Number = x.Equals(y) ? 1 : 0;
            o.Type = RuntimeObjectType.Boolean;
            return o;
        }

        public static RuntimeObject operator !=(RuntimeObject x, RuntimeObject y)
        {
            RuntimeObject o = new RuntimeObject();
            o.Number = !x.Equals(y) ? 1 : 0;
            o.Type = RuntimeObjectType.Boolean;
            return o;
        }

        public static RuntimeObject operator &(RuntimeObject x, RuntimeObject y)
        {
            x.Number = (int)x.Number & (int)y.Number;
            return x;
        }

        public static RuntimeObject operator %(RuntimeObject x, RuntimeObject y)
        {
            x.Number = x.Number % y.Number;
            return x;
        }

        public static RuntimeObject operator ^(RuntimeObject x, RuntimeObject y)
        {
            x.Number = (int)x.Number ^ (int)y.Number;
            return x;
        }

        public static RuntimeObject operator |(RuntimeObject x, RuntimeObject y)
        {
            x.Number = (int)x.Number | (int)y.Number;
            return x;
        }

        [SpecialName]
        public static RuntimeObject op_AndAlso(RuntimeObject x, RuntimeObject y)
        {
            x.Number = (x.Number != 0 && y.Number != 0) ? 1 : 0;
            return x;
        }

        [SpecialName]
        public static RuntimeObject op_OrElse(RuntimeObject x, RuntimeObject y)
        {
            x.Number = (x.Number != 0 || y.Number != 0) ? 1 : 0;
            return x;
        }

        public static RuntimeObject New()
        {
            // 生成运行时唯一key
            var memberName = ConvertUtility.GuidTo16String();

            // 新建object
            MemberObject memberObject = new MemberObject(memberName);

            return Box(memberObject);
        }

        public static RuntimeObject Null()
        {
            RuntimeObject o = new RuntimeObject();
            o.Number = 0;
            o.Type = RuntimeObjectType.Null;
            return o;
        }

        public static RuntimeObject Box(bool value)
        {
            RuntimeObject o = new RuntimeObject();
            o.Number = value ? 1 : 0;
            o.Type = RuntimeObjectType.Boolean;
            return o;
        }

        public static RuntimeObject Box(float value)
        {
            RuntimeObject o = new RuntimeObject();
            o.Number = value;
            o.Type = RuntimeObjectType.Number;
            return o;
        }

        public static RuntimeObject Box(double value)
        {
            RuntimeObject o = new RuntimeObject();
            o.Number = (float)value;
            o.Type = RuntimeObjectType.Number;
            return o;
        }

        public static RuntimeObject Box(string value)
        {
            RuntimeObject o = new RuntimeObject();
            o.String = value;
            o.Type = RuntimeObjectType.String;
            return o;
        }

        public static RuntimeObject Box(Function value)
        {
            RuntimeObject o = new RuntimeObject();
            o.Function = value;
            o.Type = RuntimeObjectType.Function;
            return o;
        }

        public static RuntimeObject Box(char[] value)
        {
            RuntimeObject o = RuntimeObject.New();
            for (var i = 0; i < value.Length; i++)
            {
                o.Object[i.ToString()] = RuntimeObject.Box(value[i].ToString());
            }
            return o;
        }

        public static RuntimeObject Box(string[] value)
        {
            RuntimeObject o = RuntimeObject.New();
            for (var i = 0; i < value.Length; i++)
            {
                o.Object[i.ToString()] = RuntimeObject.Box(value[i]);
            }
            return o;
        }

        public static RuntimeObject Box(Delegate value)
        {
            RuntimeObject o = new RuntimeObject();
            o.ClrDelegate = value;
            o.Type = RuntimeObjectType.ClrDelegate;
            return o;
        }

        public static RuntimeObject Box(Object value)
        {
            RuntimeObject o = new RuntimeObject();
            o.ClrObject = value;
            o.Type = RuntimeObjectType.ClrObject;
            return o;
        }

        public static RuntimeObject Box(MemberObject value) 
        {
            RuntimeObject o = new RuntimeObject();
            o.Object = value;
            o.Type = RuntimeObjectType.Object;
            return o;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public bool Equals(RuntimeObject other)
        {
            var value = Type == other.Type;
            if (!value)
                return false;

            switch (Type)
            {
                case RuntimeObjectType.Null:
                    return other.Type == RuntimeObjectType.Null;
                case RuntimeObjectType.Boolean:
                    return Number == other.Number;
                case RuntimeObjectType.Function:
                    return Function.FunctionName == other.Function.FunctionName;
                case RuntimeObjectType.Number:
                    return Number == other.Number;
                case RuntimeObjectType.String:
                    return String == other.String;
                case RuntimeObjectType.Object:
                    return Object == other.Object;

            }

            return false;
        }

        public override string ToString()
        {
            switch (Type) 
            {
                case RuntimeObjectType.Boolean:
                    return Number != 0 ? "true" : "false";
                case RuntimeObjectType.Function:
                    return "{Function}";
                case RuntimeObjectType.FunctionCollection:
                    return "funcArray:" + Function.ToString();
                case RuntimeObjectType.Null:
                    return "null";
                case RuntimeObjectType.Number:
                    return Number.ToString();
                case RuntimeObjectType.Object:
                    return Object.Members.Count > 0 ? "{...}" : "{ }";
                case RuntimeObjectType.String:
                    return String;
                case RuntimeObjectType.ClrDelegate:
                    return "{ClrDelegate}";
                case RuntimeObjectType.ClrObject:
                    return "{ClrObject}";
            }

            return "未知的。";
        }

        public TypeCode GetTypeCode()
        {
            throw new NotImplementedException();
        }

        public bool ToBoolean()
        {
            return Number != 0;
        }

        public byte ToByte()
        {
            return (byte)Number;
        }

        public char ToChar()
        {
            return (char)Number;
        }

        public DateTime ToDateTime()
        {
            throw new NotImplementedException();
        }

        public decimal ToDecimal()
        {
            return (decimal)Number;
        }

        public double ToDouble()
        {
            return (double)Number;
        }

        public short ToInt16()
        {
            return (short)Number;
        }

        public int ToInt32()
        {
            return (int)Number;
        }

        public long ToInt64()
        {
            return (long)Number;
        }

        public sbyte ToSByte()
        {
            return (sbyte)Number;
        }

        public float ToSingle()
        {
            return (float)Number;
        }

        public object ToType(Type conversionType)
        {
            throw new NotImplementedException();
        }

        public ushort ToUInt16()
        {
            return (ushort)Number;
        }

        public uint ToUInt32()
        {
            return (uint)Number;
        }

        public ulong ToUInt64()
        {
            return (ulong)Number;
        }

        public static implicit operator RuntimeObject(bool value)
        {
            return RuntimeObject.Box(value);
        }

        public static implicit operator RuntimeObject(float value)
        {
            return RuntimeObject.Box(value);
        }

        public static implicit operator RuntimeObject(double value)
        {
            return RuntimeObject.Box((float)value);
        }

        public static implicit operator RuntimeObject(string value)
        {
            return RuntimeObject.Box(value);
        }

        public static implicit operator RuntimeObject(Function value)
        {
            return RuntimeObject.Box(value);
        }

        public static implicit operator RuntimeObject(MemberObject value)
        {
            return RuntimeObject.Box(value);
        }

        public static implicit operator RuntimeObject(Delegate value)
        {
            return RuntimeObject.Box(value);
        }

        public RuntimeObject Clone()
        {
            switch (Type)
            {
                case RuntimeObjectType.Boolean:
                    return Number != 0 ? true : false;
                case RuntimeObjectType.Null:
                    return RuntimeObject.Null();
                case RuntimeObjectType.Number:
                    return Number;
                case RuntimeObjectType.Object:
                    var obj = RuntimeObject.New();
                    var mobj = new MemberObject(obj.Object.Name);
                    foreach (var member in obj.Object.Members)
                    {
                        mobj[member.Key] = member.Value.Clone();
                    }
                    obj.Object = mobj;
                    return obj;
                case RuntimeObjectType.String:
                    return String;
            }

            throw new NotImplementedException();
        }

    }
}
