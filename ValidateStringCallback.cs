//------------------------------------------------------------------------------
// <copyright file="HttpValueCollection.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

/*
 * Ordered String/String[] collection of name/value pairs
 * Based on NameValueCollection -- adds parsing from string, cookie collection
 *
 * Copyright (c) 2000 Microsoft Corporation
 */

namespace L.Util
{
    public delegate void ValidateStringCallback(string key, string value);
}
