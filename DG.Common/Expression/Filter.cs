namespace DG.Common
{ 
    public class Filter
    {
        public string PropertyName { get; set; }
        public CompareOperation Operation { get; set; } = CompareOperation.Equals;
        public object Value { get; set; }
    }
}
