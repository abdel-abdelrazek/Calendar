namespace Calender.Models
{
    /// <summary>
    /// Use this generic class to have key and value pairs
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="P"></typeparam>
    public class KeyValue<T,P>
    {
        public T Key { get; set; }
        public P Value { get; set; }

    }
}