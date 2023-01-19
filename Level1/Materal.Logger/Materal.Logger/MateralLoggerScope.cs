namespace Materal.Logger
{
    public class MateralLoggerScope : IDisposable
    {
        private readonly MateralLogger _logger;
        public string Scope { get; set; }
        public MateralLoggerScope(string scope, MateralLogger logger)
        {
            Scope = scope;
            _logger = logger;
        }
        public void Dispose() => _logger.ExitScope();
    }
}
