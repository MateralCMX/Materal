namespace MateralPublish.Models
{
    public class OscillatorProjectModel : BaseProjectModel
    {
        public OscillatorProjectModel(string directoryPath) : base(directoryPath)
        {
        }
        protected override bool IsPublishProject(string name)
        {
            string[] whiteList = new[]
            {
                "Materal.Oscillator",
                "Materal.Oscillator.Abstractions",
                "Materal.Oscillator.DR",
                "Materal.Oscillator.LocalDR",
                "Materal.Oscillator.SqliteRepositoryImpl",
                "Materal.Oscillator.SqlServerRepositoryImpl"
            };
            return whiteList.Contains(name);
        }
    }
}
