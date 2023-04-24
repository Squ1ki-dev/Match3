using UnityEngine;

namespace MatchThreeEngine
{
	[CreateAssetMenu(menuName = "TestTask/ScriptableObjectt")]
	public sealed class TileTypeAsset : ScriptableObject
	{
		public int id;

		public int value;

		public Sprite sprite;
	}
}
