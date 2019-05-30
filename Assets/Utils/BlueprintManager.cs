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
                .Where(bp => transmissionCount >= bp.GetInfo()[0])
                .OrderBy(bp => transmissionCount - bp.GetInfo()[0])
                .ThenBy(bp => Math.Abs(armorCount - bp.GetInfo()[2]))
                .ThenBy(bp => Math.Abs(gunsCount - bp.GetInfo()[3]))
                .ThenBy(bp => Math.Abs(wheelCount - bp.GetInfo()[1]))
                .First();
            return result;
        }
    }
}