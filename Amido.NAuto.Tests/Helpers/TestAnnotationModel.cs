using System.ComponentModel.DataAnnotations;

namespace Amido.NAuto.UnitTests.Helpers
{
    public class TestAnnotationModel
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Url)]
        public string Url { get; set; }

        [StringLength(10)]
        public string StringLengthTestNoMinimum { get; set; }
        
        [StringLength(50, MinimumLength = 45)]
        public string StringLengthTestMinAndMax { get; set; }

        [Range(1, 10)]
        public int RangeIntTest { get; set; }

        [Range(1.2, 10.2)]
        public double RangeDoubleTest { get; set; }
    }
}