using System;
using Amido.NAuto.Randomizers;

namespace Amido.NAuto.Builders
{
    public class AutoBuilderConfiguration
    {
        public AutoBuilderConfiguration(
            CharacterSetType stringCharacterSetType = CharacterSetType.Alpha,
            Casing stringCasing = Casing.ProperCase,
            Spaces stringSpaces = Spaces.None,
            Language defaultLanguage = Language.English,
            int intMinimum = 1,
            int intMaximum = 10000,
            double doubleMinimum = 100,
            double doubleMaximum = 100000,
            short shortMinimum = 1,
            short shortMaximum = 32000,
            bool defaultBoolean = true,
            int stringMinLength = 5,
            int stringMaxLength = 25,
            int maxDepth = 5,
            int defaultCollectionItemCount = 2)
            : this(
                DateTime.UtcNow,
                stringCharacterSetType,
                stringCasing,
                stringSpaces,
                defaultLanguage,
                intMinimum,
                intMaximum,
                doubleMinimum,
                doubleMaximum,
                shortMinimum,
                shortMaximum,
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
            short shortMinimum = 1,
            short shortMaximum = 32000,
            bool defaultBoolean = true,
            int stringMinLength = 5,
            int stringMaxLength = 25,
            int maxDepth = 5,
            int defaultCollectionItemCount = 2)
        {
            this.DefaultDateTime = defaultDateTime;
            this.DefaultStringCharacterSetType = stringCharacterSetType;
            this.DefaultStringCasing = stringCasing;
            this.DefaultStringSpaces = stringSpaces;
            this.DefaultLanguage = defaultLanguage;
            this.IntMinimum = intMinimum;
            this.IntMaximum = intMaximum;
            this.DoubleMinimum = doubleMinimum;
            this.DoubleMaximum = doubleMaximum;
            this.ShortMinimum = shortMinimum;
            this.ShortMaximum = shortMaximum;
            this.DefaultBoolean = defaultBoolean;
            this.StringMinLength = stringMinLength;
            this.StringMaxLength = stringMaxLength;
            this.MaxDepth = maxDepth;
            this.DefaultCollectionItemCount = defaultCollectionItemCount;
            this.Conventions = new Conventions();
        }

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

        public short ShortMinimum { get; set; }

        public short ShortMaximum { get; set; }

        public DateTime DefaultDateTime { get; set; }

        public bool DefaultBoolean { get; set; }

        public int MaxDepth { get; set; }

        public int DefaultCollectionItemCount { get; set; }

        public Conventions Conventions { get; set; }
    }
}