using System;
namespace Pets.Helpers
{
    public class LoggingHelper
    {
        public static ExType LogErrorAndCreateException<ExType>(string message, Exception exception) where ExType : Exception, new()
        {
            // TODO Log 'message' and exception details here
            return CreateNewExceptionFromMessage<ExType>(message);
        }

        public static ExType LogErrorAndCreateException<ExType>(string message) where ExType : Exception, new()
        {
            // TODO Log 'message'  here
            return CreateNewExceptionFromMessage<ExType>(message);
        }

        public static string LogErrorAndReturnMessage(string message)
        {
            // TODO Log 'message' as error
            return message;
        }

        private static ExType CreateNewExceptionFromMessage<ExType>(string message) where ExType : Exception, new()
        {
            return (ExType)Activator.CreateInstance(typeof(ExType), message);
        }

    }
}
