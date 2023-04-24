using System.Collections.Generic;
using System.Linq;

namespace MatchThreeEngine
{
	public static class TileDataMatrixUtility
	{
		public static void Swap(int x1, int y1, int x2, int y2, TileData[,] tiles) =>
			(tiles[x1, y1], tiles[x2, y2]) = (tiles[x2, y2], tiles[x1, y1]);

		public static (TileData[], TileData[]) GetConnections(int originX, int originY, TileData[,] tiles)
		{
			var origin = tiles[originX, originY];

			var width = tiles.GetLength(0);
			var height = tiles.GetLength(1);

			var horizontalConnections = new List<TileData>();
			var verticalConnections = new List<TileData>();

			for (var x = originX - 1; x >= 0; x--)
			{
				var other = tiles[x, originY];

				if (other.TypeId != origin.TypeId) break;

				horizontalConnections.Add(other);
			}

			for (var x = originX + 1; x < width; x++)
			{
				var other = tiles[x, originY];

				if (other.TypeId != origin.TypeId) break;

				horizontalConnections.Add(other);
			}

			for (var y = originY - 1; y >= 0; y--)
			{
				var other = tiles[originX, y];

				if (other.TypeId != origin.TypeId) break;

				verticalConnections.Add(other);
			}

			for (var y = originY + 1; y < height; y++)
			{
				var other = tiles[originX, y];

				if (other.TypeId != origin.TypeId) break;

				verticalConnections.Add(other);
			}

			return (horizontalConnections.ToArray(), verticalConnections.ToArray());
		}

		public static Match FindBestMatch(TileData[,] tiles) => FindAllMatches(tiles).OrderByDescending(match => match.Score).FirstOrDefault();

		public static List<Match> FindAllMatches(TileData[,] tiles)
		{
			var matches = new List<Match>();

			for (var y = 0; y < tiles.GetLength(1); y++)
			{
				for (var x = 0; x < tiles.GetLength(0); x++)
				{
					var tile = tiles[x, y];

					var (h, v) = GetConnections(x, y, tiles);

					var match = new Match(tile, h, v);

					if (match.Score > -1) matches.Add(match);
				}
			}

			return matches;
		}

		private static (int, int) GetDirectionOffset(byte direction) => direction switch
		{
			0 => (-1, 0),
			1 => (0, -1),
			2 => (1, 0),
			3 => (0, 1),

			_ => (0, 0),
		};

		public static Move FindMove(TileData[,] tiles)
		{
			var width = tiles.GetLength(0);
			var height = tiles.GetLength(1);

			foreach (var (x, y) in Enumerable.Range(0, width * height).Select(i => (i % width, i / width)))
			{
				foreach (var (dx, dy) in new[] { (-1, 0), (0, -1), (1, 0), (0, 1) })
				{
					var x2 = x + dx;
					var y2 = y + dy;

					if (x2 < 0 || x2 >= width || y2 < 0 || y2 >= height) continue;

					Swap(x, y, x2, y2, tiles);

					if (FindBestMatch(tiles) != null) return new Move(x, y, x2, y2);

					Swap(x, y, x2, y2, tiles);
				}
			}
			return null;
		}

	}
}
