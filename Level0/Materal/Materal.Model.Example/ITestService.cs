using System;
using System.ComponentModel.DataAnnotations;

namespace Materal.Model.Example
{
    public interface ITestService
    {
        [DataValidation]
        void Test01(TempModel tempModel);
    }

    public class TempModel
    {
        [Required]
        public Guid ID { get; set; }
    }
}
