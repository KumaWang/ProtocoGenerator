using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iuiu.VirtualMachine
{
    public class MemberObject
    {
        private Dictionary<string, RuntimeObject> mMembers;

        public string Name { get; private set; }

        public RuntimeObject this[string key]
        {
            get
            {
                if (!mMembers.ContainsKey(key))
                {
                    return RuntimeObject.Null();
                }

                return mMembers[key];
            }
            set { mMembers[key] = value; }
        }

        public Dictionary<string, RuntimeObject> Members
        {
            get { return mMembers; }
        }

        public int Length
        {
            get { return mMembers.Count; }
        }

        public MemberObject(string name)
        {
            Name = name;
            mMembers = new Dictionary<string, RuntimeObject>();
        }

        public bool Contains(string key)
        {
            return mMembers.ContainsKey(key);
        }

        public RuntimeObject GetMember(string key)
        {
            return mMembers[key];
        }

        public RuntimeObject SetMember(string key, RuntimeObject value)
        {
            mMembers[key] = value;
            return value;
        }

        public int IndexOf(RuntimeObject member)
        {
            return mMembers.Values.ToList().IndexOf(member);
        }

        public void RemoveMember(string key)
        {
            mMembers.Remove(key);
        }

        public void ClearMembers()
        {
            mMembers.Clear();
        }
    }
}
