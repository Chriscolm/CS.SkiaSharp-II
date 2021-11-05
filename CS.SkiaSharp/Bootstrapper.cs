using CS.SkiaSharpExample.Elements.Contracts;
using CS.SkiaSharpExample.Elements.Manager;
using CS.SkiaSharpExample.Eventbus;
using CS.SkiaSharpExample.Eventbus.Contracts;
using CS.SkiaSharpExample.Renderer.Contracts;
using CS.SkiaSharpExample.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace CS.SkiaSharpExample
{
    public class Bootstrapper
    {
        private readonly IServiceCollection _serviceCollection;
        private ServiceProvider _serviceProvider;

        public Bootstrapper()
        {
            _serviceCollection = new ServiceCollection();
        }

        public void Activate()
        {
            _serviceCollection.AddScoped<IRenderer, Renderer.Renderer>();
            _serviceCollection.AddScoped<ISceneManager, SceneManager>();

            _serviceCollection.AddSingleton<IMessageBroker, MessageBroker>();

            _serviceCollection.AddScoped<CanvasViewModel>();
            _serviceCollection.AddScoped<ControlsViewModel>();

            _serviceProvider = _serviceCollection.BuildServiceProvider();
        }

        public T Resolve<T>()
        {
            T result = _serviceProvider.GetRequiredService<T>();
            return result;
        }
    }
}
