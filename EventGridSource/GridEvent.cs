using System;

namespace EventGridSource
{
    public class GridEvent<T> where T : class
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public string EventType { get; set; }
        public T Data { get; set; }
        public DateTime EventTime { get; set; }

        public GridEvent()
        {
            Id = Guid.NewGuid().ToString();
            EventTime = DateTime.UtcNow;
        }
    }
}
