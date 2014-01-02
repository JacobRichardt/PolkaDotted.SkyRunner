using FarseerPhysics;
using OpenTK;

namespace PolkaDotted.SkyRunner
{
	public static class Extensions
	{
		public static Vector2 ToTK(this Microsoft.Xna.Framework.Vector2 value)
		{
			return new Vector2(value.X, value.Y);
		}

		public static Vector2 ToDisplayTK(this Microsoft.Xna.Framework.Vector2 value)
		{
			return ConvertUnits.ToDisplayUnits(value).ToTK();
		}

		public static Microsoft.Xna.Framework.Vector2 ToFarSeer(this Vector2 value)
		{
			return new Microsoft.Xna.Framework.Vector2(value.X, value.Y);
		}
	}
}