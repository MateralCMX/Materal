using System;
using System.ComponentModel.DataAnnotations;

namespace ConfigCenter.Services.Models.Namespace
{
    public class EditNamespaceModel: AddNamespaceModel
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识不能为空")]
        public Guid ID { get; set; }
    }
}
