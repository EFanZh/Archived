using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace PInvokeHelper.Parser2
{
    internal class Declaration
    {
        public Declaration(DeclarationSpecifier[] specifiers, Attribute[] attributes, InitDeclarator[] initDeclarators)
        {
            Specifiers = specifiers;
            Attributes = attributes;
            InitDeclarators = initDeclarators;
        }

        public DeclarationSpecifier[] Specifiers
        {
            get;
        }

        public Attribute[] Attributes
        {
            get;
        }

        public InitDeclarator[] InitDeclarators
        {
            get;
        }

        public Declaration Parse(string input, ref int index)
        {
            var i = index;
            var specifiers = ParseSpecifiers(input, ref i);

            if (specifiers == null)
            {
                return null;
            }

            Helper.SkipWhitespaces(input, ref i);

            var attributes = ParseAttributes(input, ref i);

            Helper.SkipWhitespaces(input, ref i);

            var initDeclarators = ParseInitDeclarators(input, ref i);

            Helper.SkipWhitespaces(input, ref i);

            if (Helper.ParseString(input, ref i, ";"))
            {
                index = i;

                return new Declaration(specifiers, attributes, initDeclarators);
            }
            else
            {
                return null;
            }
        }

        private static DeclarationSpecifier[] ParseSpecifiers(string input, ref int index)
        {
            var i = index;
            var specifier = DeclarationSpecifier.Parse(input, ref i);

            if (specifier == null)
            {
                return null;
            }

            var result = new List<DeclarationSpecifier> { specifier };

            Helper.SkipWhitespaces(input, ref i);

            specifier = DeclarationSpecifier.Parse(input, ref i);

            while (specifier != null)
            {
                result.Add(specifier);

                Helper.SkipWhitespaces(input, ref i);
                specifier = DeclarationSpecifier.Parse(input, ref i);
            }

            index = i;

            return result.ToArray();
        }

        private static Attribute[] ParseAttributes(string input, ref int index)
        {
            var i = index;
            var result = new List<Attribute>();
            var attribute = Attribute.Parse(input, ref i);

            while (attribute != null)
            {
                result.Add(attribute);

                Helper.SkipWhitespaces(input, ref i);
                attribute = Attribute.Parse(input, ref i);
            }

            index = i;

            return result.ToArray();
        }

        private static InitDeclarator[] ParseInitDeclarators(string input, ref int index)
        {
            var i = index;
            var result = new List<InitDeclarator>();
            var initDeclarator = InitDeclarator.Parse(input, ref i);
            var lastSuccess = 0;

            if (initDeclarator != null)
            {
                result.Add(initDeclarator);

                Helper.SkipWhitespaces(input, ref i);

                while (Helper.ParseString(input, ref i, ","))
                {
                    initDeclarator = InitDeclarator.Parse(input, ref i);

                    if (initDeclarator == null)
                    {
                        return null;
                    }

                    result.Add(initDeclarator);
                    lastSuccess = i;

                    Helper.SkipWhitespaces(input, ref i);
                }
            }

            index = lastSuccess;

            return result.ToArray();
        }
    }
}
