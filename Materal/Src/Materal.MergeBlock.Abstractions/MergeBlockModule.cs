namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// MergeBlock模块
    /// </summary>
    public abstract class MergeBlockModule(string name) : IMergeBlockModule
    {
        /// <inheritdoc/>
        public string Name { get; } = name;
        /// <inheritdoc/>
        public virtual void OnPreConfigureServices(ServiceConfigurationContext context) { }
        /// <inheritdoc/>
        public virtual async Task OnPreConfigureServicesAsync(ServiceConfigurationContext context) => await Task.CompletedTask;
        /// <inheritdoc/>
        public virtual void OnConfigureServices(ServiceConfigurationContext context) { }
        /// <inheritdoc/>
        public virtual async Task OnConfigureServicesAsync(ServiceConfigurationContext context) => await Task.CompletedTask;
        /// <inheritdoc/>
        public virtual void OnPostConfigureServices(ServiceConfigurationContext context) { }
        /// <inheritdoc/>
        public virtual async Task OnPostConfigureServicesAsync(ServiceConfigurationContext context) => await Task.CompletedTask;
        /// <inheritdoc/>
        public virtual void OnPreApplicationInitialization(ApplicationInitializationContext context) { }
        /// <inheritdoc/>
        public virtual async Task OnPreApplicationInitializationAsync(ApplicationInitializationContext context) => await Task.CompletedTask;
        /// <inheritdoc/>
        public virtual void OnApplicationInitialization(ApplicationInitializationContext context) { }
        /// <inheritdoc/>
        public virtual async Task OnApplicationInitializationAsync(ApplicationInitializationContext context) => await Task.CompletedTask;
        /// <inheritdoc/>
        public virtual void OnPostApplicationInitialization(ApplicationInitializationContext context) { }
        /// <inheritdoc/>
        public virtual async Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context) => await Task.CompletedTask;
        /// <inheritdoc/>
        public override string ToString() => $"{Name}<{GetType().Name}>";
    }
}
