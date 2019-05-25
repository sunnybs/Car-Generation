using System;
using System.Linq;
using System.Reflection;

namespace Assets.Utils
{
    public class BlueprintManager
    {
        public static BaseBlueprint PickByCount(int transmissionCount, int wheelCount, int armorCount, int gunsCount)
        {
            var blueprints = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.Namespace == "Assets.Utils.Blueprints")
                .Select(bp => (BaseBlueprint) Activator.CreateInstance(bp))
                .ToArray();

            var result = blueprints
                .OrderBy(bp => Math.Abs(bp.GetInfo()[0] - transmissionCount))
                .ThenBy(bp => Math.Abs(bp.GetInfo()[2] - armorCount))
                .ThenBy(bp => Math.Abs(bp.GetInfo()[3] - gunsCount))
                .ThenBy(bp => Math.Abs(bp.GetInfo()[1] - wheelCount))
                .First();
            return result;
        }
    }
}