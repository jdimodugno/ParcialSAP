using System;
namespace Core
{
    public class OperationResult<T>
    {
        public string OperationType { get; set; }
        public T data { get; set; }
        public string Error { get; set; }
        public bool HasError { get => !string.IsNullOrWhiteSpace(Error); }
    }
}
