using System;
using System.ComponentModel.DataAnnotations;

namespace Materal.ConfigCenter.ProtalServer.PresentationModel.Namespace
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
