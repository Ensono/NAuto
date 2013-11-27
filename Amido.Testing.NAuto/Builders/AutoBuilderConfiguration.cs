using System;
using Amido.Testing.NAuto.Randomizers;

namespace Amido.Testing.NAuto.Builders
{
    public class AutoBuilderConfiguration
    {
        public int StringMinLength { get; set; }
        public int StringMaxLength { get; set; }
        public CharacterSetType StringStringCharacterSetType { get; set; }
        public Casing StringStringCasing { get; set; }
        public Spaces StringStringSpaces { get; set; }
        public int IntMinimum { get; set; }
        public int IntMaximum { get; set; }
        public double DoubleMinimum { get; set; }
        public double DoubleMaximum { get; set; }
        public DateTime DefaultDateTime { get; set; }
        public bool DefaultBoolean { get; set; }
        public int MaxDepth { get; set; }
        public int DefaultListItemCount { get; set; }
        public Conventions Conventions { get; set; }

        public AutoBuilderConfiguration(
            CharacterSetType stringCharacterSetType = CharacterSetType.Anything,
            Casing stringCasing = Casing.Any,
            Spaces stringSpaces = Spaces.Any,
            int intMinimum = 1,
            int intMaximum = 10000,
            double doubleMinimum = 100,
            double doubleMaximum = 100000,
            bool defaultBoolean = true,
            int stringMinLength = 5,
            int stringMaxLength = 25,
            int maxDepth = 3,
            int defaultListItemCount = 2
            ) :this(
            DateTime.UtcNow, 
            stringCharacterSetType,
            stringCasing,
            stringSpaces,
            intMinimum,
            intMaximum,
            doubleMinimum,
            doubleMaximum,
            defaultBoolean,
            stringMinLength,
            stringMaxLength,
            maxDepth, 
            defaultListItemCount)
        {
        }

        public AutoBuilderConfiguration(
            DateTime defaultDateTime,
            CharacterSetType stringCharacterSetType = CharacterSetType.Anything,
            Casing stringCasing = Casing.Any,
            Spaces stringSpaces = Spaces.Any,
            int intMinimum = 1,
            int intMaximum = 10000,
            double doubleMinimum = 100,
            double doubleMaximum = 100000,
            bool defaultBoolean = true,
            int stringMinLength = 5,
            int stringMaxLength = 25,
            int maxDepth = 3,
            int defaultListItemCount = 2
            )
        {
            DefaultDateTime = defaultDateTime;
            StringStringCharacterSetType = stringCharacterSetType;
            StringStringCasing = stringCasing;
            StringStringSpaces = stringSpaces;
            IntMinimum = intMinimum;
            IntMaximum = intMaximum;
            DoubleMinimum = doubleMinimum;
            DoubleMaximum = doubleMaximum;
            DefaultBoolean = defaultBoolean;
            StringMinLength = stringMinLength;
            StringMaxLength = stringMaxLength;
            MaxDepth = maxDepth;
            DefaultListItemCount = defaultListItemCount;
            Conventions = new Conventions();
        }
    }
}