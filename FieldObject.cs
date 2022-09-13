using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship_3._0
{
    public class FieldObject
    {
		private const short size = Field.mapSize;
		private static Random rand = new Random();
		
		private enum Direction
        {
            LEFT,
            UP,
            RIGHT,
            DOWN,
            IMPOSSIBLE = -1
        };
		public enum ObjectType
        {
            EMPTY,
            WAVE,
            SHIP,
            DEAD,
        };

		public ObjectType[,] field = new ObjectType[size, size];

		public static Bitmap[] images =
        {
            new Bitmap(@"E:\Step\.NET\WinForms\Battleship_3.0\images\empty.png"),
            new Bitmap(@"E:\Step\.NET\WinForms\Battleship_3.0\images\wave.png"),
            new Bitmap(@"E:\Step\.NET\WinForms\Battleship_3.0\images\ship.png"),
            new Bitmap(@"E:\Step\.NET\WinForms\Battleship_3.0\images\dead.png")
        };

		public FieldObject()
        {
			InitShips();
        }
		private void InitShips()
		{
            for (short i = 0; i < size; i++)
            {
                for (short j = 0; j < size; j++)
                {
					field[i, j] = ObjectType.WAVE;
                }
            }
			int cnt = 0;
			int dir = -1;
			while (cnt != 10)
			{
				short x = (short)rand.Next(size);
				short y = (short)rand.Next(size);

				//Set 4x
				if (cnt == 0)
				{
					dir = GetAllowedDirection(x, y, 4);
					if (dir != (int)Direction.IMPOSSIBLE)
					{
						SetShip(x, y, 4, (Direction)dir);
						++cnt;
					}

				}
				//Set 3x
				else if (cnt < 3)
				{
					dir = GetAllowedDirection(x, y, 3);
					if (dir != (int)Direction.IMPOSSIBLE)
					{
						SetShip(x, y, 3, (Direction)dir);
						++cnt;
					}
				}
				//Set2x
				else if (cnt < 6)
				{
					dir = GetAllowedDirection(x, y, 2);
					if (dir != (int)Direction.IMPOSSIBLE)
					{
						SetShip(x, y, 2, (Direction)dir);
						++cnt;
					}
				}
				//Set1x
				else
				{
					if (IsAllowedToSet(x, y) && OutOfBounds( x, y))
					{
						field[y, x] = ObjectType.SHIP;
						++cnt;
					}
				}

			}
		}

		private int GetAllowedDirection(short x, short y, short _size)
		{
			int[] results = { -1, -1, -1, -1 };
			int result = -1;
			int head = 0;
			if (y == 0)
			{
				if (x == 0)
				{
					//check to right
					if (CheckRight(x, y, _size))
					{
						results[head++] = (int)Direction.RIGHT;
					}
					//check to down
					if (CheckDown(x, y, _size))
					{
						results[head++] = (int)Direction.DOWN;
					}
				}
				else if (x == (size - 1))
				{
					//check to left
					if (CheckLeft(x, y, _size))
					{
						results[head++] = (int)Direction.LEFT;
					}
					//check to down
					if (CheckDown(x, y, _size))
					{
						results[head++] = (int)Direction.DOWN;
					}
				}
				else
				{
					//check to left
					if (CheckLeft(x, y, _size))
					{
						results[head++] = (int)Direction.LEFT;
					}
					//check to right
					if (CheckRight(x, y, _size))
					{
						results[head++] = (int)Direction.RIGHT;
					}
					//check to down
					if (CheckDown(x, y, _size))
					{
						results[head++] = (int)Direction.DOWN;
					}

				}
			}
			else if (y == (size - 1))
			{
				if (x == 0)
				{
					//check to right
					if (CheckRight(x, y, _size))
					{
						results[head++] = (int)Direction.RIGHT;
					}
					//check to up
					if (CheckUp(x, y, _size))
					{
						results[head++] = (int)Direction.UP;
					}
				}
				else if (x == (size - 1))
				{
					//check to left
					if (CheckLeft(x, y, _size))
					{
						results[head++] = (int)Direction.LEFT;
					}
					//check to up
					if (CheckUp(x, y, _size))
					{
						results[head++] = (int)Direction.UP;
					}
				}
				else
				{
					//check to left
					if (CheckLeft(x, y, _size))
					{
						results[head++] = (int)Direction.LEFT;
					}
					//check to right
					if (CheckRight(x, y, _size))
					{
						results[head++] = (int)Direction.RIGHT;
					}
					//check to up
					if (CheckUp(x, y, _size))
					{
						results[head++] = (int)Direction.UP;
					}
				}
			}
			else
			{
				if (x == 0)
				{
					//check to right
					if (CheckRight(x, y, _size))
					{
						results[head++] = (int)Direction.RIGHT;
					}
					//check to up
					if (CheckUp(x, y, _size))
					{
						results[head++] = (int)Direction.UP;
					}
					//check to down
					CheckDown(x, y, _size);
				}
				else if (x == (size - 1))
				{
					//check to left
					if (CheckLeft(x, y, _size))
					{
						results[head++] = (int)Direction.LEFT;
					}
					//check to up
					if (CheckUp(x, y, _size))
					{
						results[head++] = (int)Direction.UP;
					}
					//check to down
					if (CheckDown(x, y, _size))
					{
						results[head++] = (int)Direction.DOWN;
					}
				}
				else
				{
					//check to left
					if (CheckLeft(x, y, _size))
					{
						results[head++] = (int)Direction.LEFT;
					}
					//check to right
					if (CheckRight(x, y, _size))
					{
						results[head++] = (int)Direction.RIGHT;
					}
					//check to up
					if (CheckUp(x, y, _size))
					{
						results[head++] = (int)Direction.UP;
					}
				}
			}
			if (head != 0)
			{
				int rnd = rand.Next(head);
				result = results[rnd];
			}
			return result;
		}
		/// <summary>
		/// Check directions
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		private bool CheckLeft(short x, short y, short size)
		{
			for (short i = 0; i < size; i++)
			{
				if (!OutOfBounds((short)(x - i), y))
				{
					return false;
				}
				else if (!IsAllowedToSet((short)(x - i), y))
				{
					return false;
				}
			}
			return true;
		}

		private bool CheckUp(short x, short y, short size)
		{
			for (short i = 0; i < size; i++)
			{
				if (!OutOfBounds(x, (short)(y - i)))
				{
					return false;
				}
				else if (!IsAllowedToSet(x, (short)(y - i)))
				{
					return false;
				}
			}
			return true;
		}

		private bool CheckRight(short x, short y, short size)
		{
			for (short i = 0; i < size; i++)
			{
				if (!OutOfBounds((short)(x + i), y))
				{
					return false;
				}
				else if (!IsAllowedToSet((short)(x + i), y))
				{
					return false;
				}
			}
			return true;
		}

		private bool CheckDown(short x, short y, short size)
		{
			for (short i = 0; i < size; i++)
			{
				if (!OutOfBounds(x, (short)(y + i)))
				{
					return false;
				}
				else if (!IsAllowedToSet(x, (short)(y + i)))
				{
					return false;
				}
			}
			return true;
		}

		private bool OutOfBounds(short x, short y)
		{
			if (
				y < 0 ||
				y >= size ||
				x < 0 ||
				x >= size ||
				field[y,x] == ObjectType.SHIP
				)
			{
				return false;
			}
			return true;
		}

		private bool IsAllowedToSet(short x, short y)
		{
			if (y == 0)
			{
				if (x == 0)
				{
					if (
						field[y, x + 1] == ObjectType.SHIP ||
						field[y + 1, x] == ObjectType.SHIP || 
						field[y + 1, x + 1] == ObjectType.SHIP
						)
						return false;
				}
				else if (x == size - 1)
				{
					if (
						field[y, x - 1] == ObjectType.SHIP ||
						field[y + 1, x] == ObjectType.SHIP || 
						field[y + 1, x - 1] == ObjectType.SHIP
						)
						return false;
				}
				else
				{
					if (
						field[y, x - 1] == ObjectType.SHIP || 
						field[y, x + 1] == ObjectType.SHIP ||
						field[y + 1, x] == ObjectType.SHIP || 
						field[y + 1, x - 1] == ObjectType.SHIP || 
						field[y + 1, x + 1] == ObjectType.SHIP
						)
						return false;
				}
			}
			else if (y == size - 1)
			{
				if (x == 0)
				{
					if (
						field[y, x + 1] == ObjectType.SHIP ||
						field[y - 1, x] == ObjectType.SHIP || 
						field[y - 1, x + 1] == ObjectType.SHIP
						)
						return false;
				}
				else if (x == size - 1)
				{
					if (
						field[y, x - 1] == ObjectType.SHIP ||
						field[y - 1, x] == ObjectType.SHIP || 
						field[y - 1, x - 1] == ObjectType.SHIP
						)
						return false;
				}
				else
				{
					if (
						field[y, x - 1] == ObjectType.SHIP || 
						field[y, x + 1] == ObjectType.SHIP ||
						field[y - 1, x] == ObjectType.SHIP || 
						field[y - 1, x - 1] == ObjectType.SHIP || 
						field[y - 1, x + 1] == ObjectType.SHIP
						)
						return false;
				}
			}
			else
			{
				if (x == 0)
				{
					if (
						field[y - 1, x] == ObjectType.SHIP || 
						field[y - 1, x + 1] == ObjectType.SHIP ||
						field[y, x + 1] == ObjectType.SHIP ||
						field[y + 1, x] == ObjectType.SHIP || 
						field[y + 1, x + 1] == ObjectType.SHIP
						)
						return false;
				}
				else if (x == size - 1)
				{
					if (
						field[y - 1, x - 1] == ObjectType.SHIP || 
						field[y - 1, x] == ObjectType.SHIP ||
						field[y, x - 1] == ObjectType.SHIP ||
						field[y + 1, x] == ObjectType.SHIP || 
						field[y + 1, x - 1] == ObjectType.SHIP
						)
						return false;
				}
				else
				{
					if (
						field[y - 1, x - 1] == ObjectType.SHIP || 
						field[y - 1, x] == ObjectType.SHIP || 
						field[y - 1, x + 1] == ObjectType.SHIP ||
						field[y, x - 1] == ObjectType.SHIP || 
						field[y, x + 1] == ObjectType.SHIP ||
						field[y + 1, x - 1] == ObjectType.SHIP || 
						field[y + 1, x] == ObjectType.SHIP || 
						field[y + 1, x + 1] == ObjectType.SHIP
						)
						return false;
				}
			}
			return true;
		}
		private void SetShip(short x, short y, int size, Direction dir)
		{
			switch (dir)
			{
				case Direction.LEFT:
					for (short i = 0; i < size; i++)
					{
						field[y, x - i] = ObjectType.SHIP;
					}
					break;

				case Direction.UP:
					for (short i = 0; i < size; i++)
					{
						field[y - i, x] = ObjectType.SHIP;
					}
					break;

				case Direction.RIGHT:
					for (short i = 0; i < size; i++)
					{
						field[y, x + i] = ObjectType.SHIP;
					}
					break;

				case Direction.DOWN:
					for (short i = 0; i < size; i++)
					{
						field[y + i, x] = ObjectType.SHIP;
					}
					break;

				default:
					break;
			}
		}
	}
}
