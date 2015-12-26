using System.Collections.Generic;

namespace PInvokeHelper.Parser2
{
    internal class Declarator
    {
        public Declarator(Pointer pointer, DirectDeclarator directDeclarator)
        {
            Pointer = pointer;
            DirectDeclarator = directDeclarator;
        }

        private Pointer Pointer
        {
            get;
        }

        private DirectDeclarator DirectDeclarator
        {
            get;
        }

        public static Declarator Parse(string input, ref int index)
        {
            var i = index;
            var pointer = Pointer.Parse(input, ref i);

            Helper.SkipWhitespaces(input, ref i);

            var directDeclarator = DirectDeclarator.Parse(input, ref i);

            if (directDeclarator == null)
            {
                return null;
            }

            index = i;

            return new Declarator(pointer, directDeclarator);
        }
    }

    internal class Pointer
    {
        public Pointer(TypeQualifier[][] typeQualifierGroups)
        {
            TypeQualifierGroups = typeQualifierGroups;
        }

        public TypeQualifier[][] TypeQualifierGroups
        {
            get;
        }

        public static Pointer Parse(string input, ref int index)
        {
            var i = index;

            if (!Helper.ParseString(input, ref i, "*"))
            {
                return null;
            }

            var typeQualifierGroups = new List<TypeQualifier[]>();
            var typeQualifiers = ParseTypeQualifiers(input, ref i);

            typeQualifierGroups.Add(typeQualifiers);

            Helper.SkipWhitespaces(input, ref i);
        }

        private static TypeQualifier[] ParseTypeQualifiers(string input, ref int index)
        {
            var i = index;
            var result = new List<TypeQualifier>();
            var typeQualifier = TypeQualifier.Parse(input, ref i);

            while (typeQualifier != null)
            {
                result.Add(typeQualifier);

                Helper.SkipWhitespaces(input, ref i);
                typeQualifier = TypeQualifier.Parse(input, ref i);
            }

            index = i;

            return result.ToArray();
        }
    }

    internal class DirectDeclarator
    {
        public static DirectDeclarator Parse(string input, ref int index)
        {
        }
    }

    internal class TypeQualifier
    {
        public static TypeQualifier Parse(string input, ref int index)
        {
        }
    }
}
