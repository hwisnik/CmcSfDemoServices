// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StructureMapWebApiDependencyScope.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
#pragma warning disable 1591

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.Http.Dependencies;
using CommonServiceLocator;
using StructureMap;

namespace CmcSfDemoServices.DependencyResolution
{
    /// <summary>
    /// The structure map web api dependency scope.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class StructureMapWebApiDependencyScope : StructureMapDependencyScope, IDependencyScope
	{
		public StructureMapWebApiDependencyScope(IContainer container)
			: base(container)
		{
		}
	}
}
