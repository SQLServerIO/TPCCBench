// Ami Bar
// amibar@gmail.com

using System;
using System.Runtime.Serialization;

namespace Amib.Threading
{

    #region Exceptions

    /// <summary>
    /// Represents an exception in case IWorkItemResult.GetResult has been canceled
    /// </summary>
    [Serializable]
    public sealed class WorkItemCancelException : ApplicationException
    {
        public WorkItemCancelException()
        {
        }

        public WorkItemCancelException(string message) : base(message)
        {
        }

        public WorkItemCancelException(string message, Exception e) : base(message, e)
        {
        }

        public WorkItemCancelException(SerializationInfo si, StreamingContext sc) : base(si, sc)
        {
        }
    }

    /// <summary>
    /// Represents an exception in case IWorkItemResult.GetResult has been timed out
    /// </summary>
    [Serializable]
    public sealed class WorkItemTimeoutException : ApplicationException
    {
        public WorkItemTimeoutException()
        {
        }

        public WorkItemTimeoutException(string message) : base(message)
        {
        }

        public WorkItemTimeoutException(string message, Exception e) : base(message, e)
        {
        }

        public WorkItemTimeoutException(SerializationInfo si, StreamingContext sc) : base(si, sc)
        {
        }
    }

    /// <summary>
    /// Represents an exception in case IWorkItemResult.GetResult has been timed out
    /// </summary>
    [Serializable]
    public sealed class WorkItemResultException : ApplicationException
    {
        public WorkItemResultException()
        {
        }

        public WorkItemResultException(string message) : base(message)
        {
        }

        public WorkItemResultException(string message, Exception e) : base(message, e)
        {
        }

        public WorkItemResultException(SerializationInfo si, StreamingContext sc) : base(si, sc)
        {
        }
    }

    #endregion
}