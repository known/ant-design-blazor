﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using static AntDesign.Docs.Build.CLI.Command.GenerateDemoJsonCommand;

namespace AntDesign.Docs.Build.CLI.Documentations
{
    internal class EnumDocumentation : IApiDocumentation
    {
        public EnumDocumentation()
        {
        }

        public string Name { get; set; }
        public string Summary { get; set; }
        public string ObsoleteMessage { get; set; }
        public string UnderlyingType { get; internal set; }
    }
}
