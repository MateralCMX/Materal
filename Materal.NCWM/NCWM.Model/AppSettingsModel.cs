using System.Collections.Generic;

namespace NCWM.Model
{
    public class AppSettingsModel
    {
        public TitleConfig Title { get; set; }
        public List<ConfigModel> Configs { get; set; }
    }
}
