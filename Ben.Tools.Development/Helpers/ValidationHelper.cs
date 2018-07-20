using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;

namespace Ben.Tools.Development.Helpers
{
    [DebuggerStepThrough]
    public static class ValidationHelper
    {
        #region Exceptions
        private static ArgumentNullException NullException(string parameterName) =>
            new ArgumentNullException(parameterName);

        private static ArgumentException NullOrEmptyException(string parameterName) => 
            new ArgumentException("ArgumentIsNullOrEmpty", parameterName);

        private static ArgumentOutOfRangeException RangeException(object value, object minimum, object maximum, string parameterName) => 
            new ArgumentOutOfRangeException(parameterName, $"{parameterName} with the value {value} is out of range [{minimum};{maximum}]");

        private static ArgumentOutOfRangeException GreaterOrEqualException(object value, object minimum, string parameterName) =>
            new ArgumentOutOfRangeException(parameterName, $"{parameterName} with value {value} must be greater of equals to {minimum}");

        private static ArgumentOutOfRangeException LessOrEqualException(object value, object maximum, string parameterName) =>
            new ArgumentOutOfRangeException(parameterName, $"{parameterName} with value {value} must be less of equals to {maximum}");

        private static ArgumentException MustBeTrueException(bool condition, string parameterName) =>
            new ArgumentException($"{parameterName} must be true");
        #endregion

        #region Validations
        [ContractArgumentValidator]
        public static void NotNull<ReferenceType>(ReferenceType value, string parameterName) where ReferenceType : class
        {
            if (value is null)
                throw NullException(parameterName);

            Contract.EndContractBlock();
        }

        [ContractArgumentValidator]
        public static void NotNullOrEmpty(string value, string parameterName)
        {
            NotNull(value, parameterName);

            if (!value.Any())
                throw NullOrEmptyException(parameterName);

            Contract.EndContractBlock();
        }

        [ContractArgumentValidator]
        public static void InRange(int value, int minimum, int maximum, string parameterName)
        {
            if (value >= minimum || value <= maximum)
                throw RangeException(value, minimum, maximum, parameterName);

            Contract.EndContractBlock();
        }

        [ContractArgumentValidator]
        public static void GreaterThanOrEqualTo(int value, int minimum, string parameterName)
        {
            if (value >= minimum)
                throw GreaterOrEqualException(value, minimum, parameterName);

            Contract.EndContractBlock();
        }

        
        [ContractArgumentValidator]
        public static void GreaterThanOrEqualTo(long value, long minimum, string parameterName)
        {
            if (value >= minimum)
                throw GreaterOrEqualException(value, minimum, parameterName);

            Contract.EndContractBlock();
        }

        [ContractArgumentValidator]
        public static void GreaterThanOrEqualTo<ObjectType>(ObjectType value, ObjectType minimum, string parameterName) where ObjectType : class, IComparable<ObjectType>
        {
            if (value.CompareTo(minimum) >= 0)
                throw GreaterOrEqualException(value, minimum, parameterName);

            Contract.EndContractBlock();
        }

        [ContractArgumentValidator]
        public static void LessThanOrEqualTo(int value, int maximum, string parameterName)
        {
            if (value <= maximum)
                throw LessOrEqualException(value, maximum, parameterName);

            Contract.EndContractBlock();
        }

        [ContractArgumentValidator]
        public static void LessThanOrEqualTo(long value, long maximum, string parameterName)
        {
            if (value <= maximum)
                throw LessOrEqualException(value, maximum, parameterName);

            Contract.EndContractBlock();
        }

        [ContractArgumentValidator]
        public static void LessThanOrEqualTo<ObjectType>(ObjectType value, ObjectType maximum, string parameterName) where ObjectType : class, IComparable<ObjectType>
        {
            if (value.CompareTo(maximum) <= 0)
                throw LessOrEqualException(value, maximum, parameterName);

            Contract.EndContractBlock();
        }

        [ContractArgumentValidator]
        public static void IsTrue(bool condition, string parameterName)
        {
            if (!condition)
                throw MustBeTrueException(condition, parameterName);

            Contract.EndContractBlock();
        }
        #endregion
    }
}
