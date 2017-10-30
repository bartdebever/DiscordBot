using System;
using Riot.Enums;

namespace Riot.Attributes
{
    /// <summary>
    /// Regions to disable from endpoint
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class DisabledRegionsAttribute : Attribute
    {
        public readonly ServiceRegion[] disabledServiceRegions;
        public readonly PhysicalRegion[] disabledPhysicalRegions;

        public DisabledRegionsAttribute(params ServiceRegion[] regions)
        {
            this.disabledServiceRegions = regions;
        }

        public DisabledRegionsAttribute(params PhysicalRegion[] regions)
        {
            this.disabledPhysicalRegions = regions;
        }
    }
}
