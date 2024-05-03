namespace Core.Events.Handlers
{
    public interface IAspectChangeHandler : IEventHandler
    {
        void OnAspectRatioChanged(float aspectRatio);
    }
}