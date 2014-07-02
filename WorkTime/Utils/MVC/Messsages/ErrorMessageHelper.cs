using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;



namespace WorkTime.Utils.MVC.Messsages
{
    /// <summary>
    /// Помощник по текстам исключений
    /// </summary>
    public static class ErrorMessageHelper
    {
        /// <summary>
        /// Получить пользовательский текст сообщения для данного исключения
        /// </summary>
        /// <param name="exception">Исключение</param>
        /// <returns>Текст сообщения</returns>
        public static string GetMessageText(Exception exception)
        {
            string result = String.Empty;
            if (exception is UpdateException || exception is EntityCommandExecutionException)
            {
                if (exception.InnerException is SqlException)
                    result = GetSqlErrorMessage(exception.InnerException as SqlException);

            }
            else if (exception is DbUpdateException)
            {
                if (exception.InnerException.InnerException is SqlException)
                    result = GetSqlErrorMessage(exception.InnerException.InnerException as SqlException);
            }
            else
            {
                if (exception is System.Reflection.TargetInvocationException)
                {
                    exception = exception.InnerException;
                }
                // Получить текст сообщения согласно имени Exception'а
                string exceptionName = exception.GetType().Name;
                result = Resources.Messages.ResourceManager.GetString(exceptionName);
            }

            return String.IsNullOrEmpty(result) ? Resources.Messages.UnknownError : result;
        }

        /// <summary>
        /// Получить текст сообщения для исключения БД
        /// </summary>
        /// <param name="exception">Исключение БД</param>
        /// <returns>Текст сообщения</returns>
        private static string GetSqlErrorMessage(SqlException exception)
        {
            string result = Resources.SqlMessages.UnknownError;
            int errorCode = exception.Number;

            switch (errorCode)
            {
                case 2601:
                    // Duplicate key
                    result = Resources.SqlMessages.DuplicateRecord;
                    break;
                case 547:
                    // Reference/Foreign key constraint conflict
                    var match = Regex.Matches(exception.Message, @"The (\w+) .+")[0];
                    string operation = match.Groups[1].Value;
                    if (operation == "INSERT" || operation == "UPDATE")
                    {
                        result = Resources.SqlMessages.ConstraintConflict;
                    }
                    else if (operation == "DELETE")
                    {
                        result = Resources.SqlMessages.DeleteErrorConstraint;
                    }
                    break;
                case 8152:
                    // String or binary data would be truncated
                    result = Resources.SqlMessages.TooLongString;
                    break;
            }

            return result;
        }
    }
}