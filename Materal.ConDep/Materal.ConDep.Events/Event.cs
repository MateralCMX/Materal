namespace Materal.ConDep.Events
{
    public class Event
    {
        public Event()
        {
            HandlerName = GetType().Name + "Handler";
        }
        public Event(string handlerName)
        {
            HandlerName = handlerName;
        }
        public string HandlerName { get; set; }
    }
}
