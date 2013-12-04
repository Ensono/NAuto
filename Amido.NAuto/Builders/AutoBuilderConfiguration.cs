using System;
using Amido.NAuto.Randomizers;

namespace Amido.NAuto.Builders
{
    public class AutoBuilderConfiguration
    {
        public int StringMinLength { get; set; }
        public int StringMaxLength { get; set; }
        public CharacterSetType DefaultStringCharacterSetType { get; set; }
        public Casing DefaultStringCasing { get; set; }
        public Spaces DefaultStringSpaces { get; set; }
        public Language DefaultLanguage { get; set; }
        public int IntMinimum { get; set; }
        public int IntMaximum { get; set; }
        public double DoubleMinimum { get; set; }
        public double DoubleMaximum { get; set; }
        public DateTime DefaultDateTime { get; set; }
        public bool DefaultBoolean { get; set; }
        public int MaxDepth { get; set; }
        public int DefaultCollectionItemCount { get; set; }
        public Conventions Conventions { get; set; }

        public AutoBuilderConfiguration(
            CharacterSetType stringCharacterSetType = CharacterSetType.Alpha,
            Casing stringCasing = Casing.ProperCase,
            Spaces stringSpaces = Spaces.None,
            Language defaultLanguage = Language.English,
            int intMinimum = 1,
            int intMaximum = 10000,
            double doubleMinimum = 100,
            double doubleMaximum = 100000,
            bool defaultBoolean = true,
            int stringMinLength = 5,
            int stringMaxLength = 25,
            int maxDepth = 5,
            int defaultCollectionItemCount = 2
            ) :this(
            DateTime.UtcNow, 
            stringCharacterSetType,
            stringCasing,
            stringSpaces,
            defaultLanguage,
            intMinimum,
            intMaximum,
            doubleMinimum,
            doubleMaximum,
            defaultBoolean,
            stringMinLength,
            stringMaxLength,
            maxDepth, 
            defaultCollectionItemCount)
        {
        }

        public AutoBuilderConfiguration(
            DateTime defaultDateTime,
            CharacterSetType stringCharacterSetType = CharacterSetType.Alpha,
            Casing stringCasing = Casing.ProperCase,
            Spaces stringSpaces = Spaces.None,
            Language defaultLanguage = Language.English,
            int intMinimum = 1,
            int intMaximum = 10000,
            double doubleMinimum = 100,
            double doubleMaximum = 100000,
            bool defaultBoolean = true,
            int stringMinLength = 5,
            int stringMaxLength = 25,
            int maxDepth = 5,
            int defaultCollectionItemCount = 2
            )
        {
            DefaultDateTime = defaultDateTime;
            DefaultStringCharacterSetType = stringCharacterSetType;
            DefaultStringCasing = stringCasing;
            DefaultStringSpaces = stringSpaces;
            DefaultLanguage = defaultLanguage;
            IntMinimum = intMinimum;
            IntMaximum = intMaximum;
            DoubleMinimum = doubleMinimum;
            DoubleMaximum = doubleMaximum;
            DefaultBoolean = defaultBoolean;
            StringMinLength = stringMinLength;
            StringMaxLength = stringMaxLength;
            MaxDepth = maxDepth;
            DefaultCollectionItemCount = defaultCollectionItemCount;
            Conventions = new Conventions();
        }
    }
}