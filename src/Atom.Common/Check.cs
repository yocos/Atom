// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Atom.Common
{
    [DebuggerStepThrough]
    public static class Check
    {
        private const string ArgumentPropertyNull = "The property '{property}' of the argument '{argument}' cannot be null.";
        private const string CollectionArgumentIsEmpty = "The collection argument '{argumentName}' must contain at least one element.";
        private const string ArgumentIsEmpty = "The string argument '{argumentName}' cannot be empty.";

        public static T NotNull<T>(T value, string parameterName)
        {
            if (ReferenceEquals(value, null))
            {
                NotEmpty(parameterName, nameof(parameterName));
                throw new ArgumentNullException(parameterName);
            }
            return value;
        }

        public static T NotNull<T>(T value, string parameterName, string propertyName)
        {
            if (ReferenceEquals(value, null))
            {
                NotEmpty(parameterName, nameof(parameterName));
                NotEmpty(propertyName, nameof(propertyName));
                throw new ArgumentException(string.Format(ArgumentPropertyNull, propertyName, parameterName));
            }
            return value;
        }

        public static IReadOnlyList<T> NotEmpty<T>(IReadOnlyList<T> value, string parameterName)
        {
            NotNull(value, parameterName);
            if (value.Count == 0)
            {
                NotEmpty(parameterName, nameof(parameterName));
                throw new ArgumentException(string.Format(CollectionArgumentIsEmpty, parameterName));
            }
            return value;
        }

        public static string NotEmpty(string value, string parameterName)
        {
            Exception e = null;
            if (ReferenceEquals(value, null))
            {
                e = new ArgumentNullException(parameterName);
            }
            else if (value.Trim().Length == 0)
            {
                e = new ArgumentException(string.Format(ArgumentIsEmpty, parameterName));
            }
            if (e != null)
            {
                NotEmpty(parameterName, nameof(parameterName));
                throw e;
            }
            return value;
        }

        public static string NullButNotEmpty(string value, string parameterName)
        {
            if (!ReferenceEquals(value, null)
            && (value.Length == 0))
            {
                NotEmpty(parameterName, nameof(parameterName));
                throw new ArgumentException(string.Format(ArgumentIsEmpty, parameterName));
            }
            return value;
        }
        public static IReadOnlyList<T> HasNoNulls<T>(IReadOnlyList<T> value, string parameterName)
        where T : class
        {
            NotNull(value, parameterName);
            if (value.Any(e => e == null))
            {
                NotEmpty(parameterName, nameof(parameterName));
                throw new ArgumentException(parameterName);
            }
            return value;
        }

    }
}