using System;

namespace Materal.ApplicationUpdate.WebAPI.Models
{
    /// <summary>
    /// ������ͼģ��
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// ���ر�ʶ
        /// </summary>
        public string RequestId { get; set; }
        /// <summary>
        /// ��ʾ���ر�ʶ
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}