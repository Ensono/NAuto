namespace Amido.NAuto.IntegrationTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;

    using Amido.NAuto.IntegrationTests.Helpers;

    using NUnit.Framework;

    public class Simple
    {
        public string Blah { get; set; }
    }

    [TestFixture]
    public class WrapperClassTest
    {
        [Test]
        public void TestWrapper()
        {
            NAuto.AutoBuild<ResponseWrapper<Simple>>().Construct().Build();
        }
    }

    public class ResponseWrapper<TViewModel> where TViewModel : class
    {
        public ResponseWrapper(List<ErrorResponseViewModel> errorResponseViewModels)
            : this(null, errorResponseViewModels)
        {
        }

        public ResponseWrapper(TViewModel viewModel, List<ErrorResponseViewModel> errorResponseViewModels)
        {
            this.AdditionalValues = new Dictionary<string, string>();
            this.ErrorResponseViewModels = new List<ErrorResponseViewModel>();
            this.ViewModel = viewModel;

            if (errorResponseViewModels != null)
            {
                this.ErrorResponseViewModels.AddRange(errorResponseViewModels);
            }
        }

        public TViewModel ViewModel { get; private set; }

        public List<ErrorResponseViewModel> ErrorResponseViewModels { get; private set; }

        public string Uid { get; set; }

        public Dictionary<string, string> AdditionalValues { get; private set; }

        public bool HasErrored
        {
            get
            {
                return ErrorResponseViewModels.Any(x => x.Code.HasValue && x.Code.Value > 0);
            }
        }

        public string ErrorMessage
        {
            get
            {
                var errorMessageStringBuilder = new StringBuilder();

                foreach (var errorMessage in ErrorResponseViewModels)
                {
                    errorMessageStringBuilder.AppendLine(errorMessage.ToString());
                }

                return errorMessageStringBuilder.ToString();
            }
        }
    }

    [DataContract]
    public class ErrorResponseViewModel
    {
        public ErrorResponseViewModel()
        {
        }

        public ErrorResponseViewModel(int? code, string errorCode, string message)
        {
            this.Code = code;
            this.ErrorCode = errorCode;
            this.Message = message;
        }

        public int? Code { get; set; }

        [DataMember]
        public string ErrorCode { get; set; }

        [DataMember]
        public string Message { get; set; }

        public override string ToString()
        {
            return string.Format("Error Code: {0} Message: {1}", Code, Message);
        }
    }

}
