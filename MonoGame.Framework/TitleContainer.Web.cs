﻿// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.IO;

namespace Microsoft.Xna.Framework
{
    partial class TitleContainer
    {
        static partial void PlatformInit()
        {
            Location = string.Empty;
        }

        private static Stream PlatformOpenStream(string safeName)
        {
            return File.OpenRead(safeName);
        }
    }
}
