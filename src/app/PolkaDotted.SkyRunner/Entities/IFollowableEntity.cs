using Microsoft.Xna.Framework;

namespace PolkaDotted.SkyRunner.Entities
{
	public interface IFollowableEntity
	{
		Vector2 FollowPoint { get; } 
	}
}