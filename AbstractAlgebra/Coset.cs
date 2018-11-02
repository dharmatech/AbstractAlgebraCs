using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;

namespace AbstractAlgebraCoset
{
    public class Coset<T> : IEquatable<Coset<T>>
    {
        public Group<T> Group;
        public T Element;
        public string Name;

        public bool Equals(Coset<T> obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(obj, this)) return true;

            if (obj.GetType() != GetType()) return false;

            return
                Group.Set == obj.Group.Set
                &&
                Group.Op == obj.Group.Op
                &&
                Element.Equals(obj.Element);
        }

        public override bool Equals(object obj) => Equals(obj as Coset<T>);

        public static bool operator ==(Coset<T> a, Coset<T> b) =>
            ReferenceEquals(a, null) ? ReferenceEquals(b, null) : a.Equals(b);

        public static bool operator !=(Coset<T> a, Coset<T> b) => !(a == b);

        public override int GetHashCode()
        {
            return Element.GetHashCode();
        }

        public override string ToString() =>
            String.Format("{0}{1}{2}", Name, Group.OpString, Group.Lookup(Element));

        public Coset<T> Combine(Coset<T> H)
        {
            if (Group != H.Group) throw new Exception();

            return new Coset<T>()
            {
                Group = Group,
                Element = Group.Op(Element, H.Element),
                Name = Name
            };
        }

        public MathSet<T> ToRightCoset() => Group.RightCoset(Element);
    }
}
