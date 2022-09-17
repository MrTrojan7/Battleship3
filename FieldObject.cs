using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Battleship_3._0
{
    public class FieldObject
    {
		private const short size = Field.mapSize;
		private static Random rand = new Random();
		public short count_of_ships = 20;
		public enum Direction
        {
            LEFT,
            UP,
            RIGHT,
            DOWN,
            IMPOSSIBLE = -1
        };
		public enum ObjectType
        {
            WAVE,
            SHIP,
            DEAD,
			FAILURE,
			EMPTY
		};

		public ObjectType[,] objectType = new ObjectType[size, size];
		//MultiValueDictionary<int, int[,]> multiValueDictionary;
		//Dictionary<short[,], short> mapOfShips = new Dictionary<short[,], short>();
		//Dictionary<int, int, int> keyValuePairs;

		public static Bitmap[] images =
        {
            new Bitmap(@"E:\Step\.NET\WinForms\Battleship_3.0\images\wave.png"),
            new Bitmap(@"E:\Step\.NET\WinForms\Battleship_3.0\images\ship.png"),
            new Bitmap(@"E:\Step\.NET\WinForms\Battleship_3.0\images\dead.png"),
			new Bitmap(@"E:\Step\.NET\WinForms\Battleship_3.0\images\failure.png")
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
					objectType[i, j] = ObjectType.WAVE;
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
						objectType[y, x] = ObjectType.SHIP;
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
				x >= size
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
						objectType[y, x + 1] == ObjectType.SHIP ||
						objectType[y + 1, x] == ObjectType.SHIP ||
						objectType[y + 1, x + 1] == ObjectType.SHIP
						)
						return false;
				}
				else if (x == size - 1)
				{
					if (
						objectType[y, x - 1] == ObjectType.SHIP ||
						objectType[y + 1, x] == ObjectType.SHIP || 
						objectType[y + 1, x - 1] == ObjectType.SHIP
						)
						return false;
				}
				else
				{
					if (
						objectType[y, x - 1] == ObjectType.SHIP || 
						objectType[y, x + 1] == ObjectType.SHIP ||
						objectType[y + 1, x] == ObjectType.SHIP || 
						objectType[y + 1, x - 1] == ObjectType.SHIP ||
						objectType[y + 1, x + 1] == ObjectType.SHIP
						)
						return false;
				}
			}
			else if (y == size - 1)
			{
				if (x == 0)
				{
					if (
						objectType[y, x + 1] == ObjectType.SHIP ||
						objectType[y - 1, x] == ObjectType.SHIP ||
						objectType[y - 1, x + 1] == ObjectType.SHIP
						)
						return false;
				}
				else if (x == size - 1)
				{
					if (
						objectType[y, x - 1] == ObjectType.SHIP ||
						objectType[y - 1, x] == ObjectType.SHIP ||
						objectType[y - 1, x - 1] == ObjectType.SHIP
						)
						return false;
				}
				else
				{
					if (
						objectType[y, x - 1] == ObjectType.SHIP || 
						objectType[y, x + 1] == ObjectType.SHIP ||
						objectType[y - 1, x] == ObjectType.SHIP || 
						objectType[y - 1, x - 1] == ObjectType.SHIP ||
						objectType[y - 1, x + 1] == ObjectType.SHIP
						)
						return false;
				}
			}
			else
			{
				if (x == 0)
				{
					if (
						objectType[y - 1, x] == ObjectType.SHIP || 
						objectType[y - 1, x + 1] == ObjectType.SHIP ||
						objectType[y, x + 1] == ObjectType.SHIP ||
						objectType[y + 1, x] == ObjectType.SHIP ||
						objectType[y + 1, x + 1] == ObjectType.SHIP
						)
						return false;
				}
				else if (x == size - 1)
				{
					if (
						objectType[y - 1, x - 1] == ObjectType.SHIP || 
						objectType[y - 1, x] == ObjectType.SHIP ||
						objectType[y, x - 1] == ObjectType.SHIP ||
						objectType[y + 1, x] == ObjectType.SHIP ||
						objectType[y + 1, x - 1] == ObjectType.SHIP
						)
						return false;
				}
				else
				{
					if (
						objectType[y - 1, x - 1] == ObjectType.SHIP || 
						objectType[y - 1, x] == ObjectType.SHIP || 
						objectType[y - 1, x + 1] == ObjectType.SHIP ||
						objectType[y, x - 1] == ObjectType.SHIP || 
						objectType[y, x + 1] == ObjectType.SHIP ||
						objectType[y + 1, x - 1] == ObjectType.SHIP || 
						objectType[y + 1, x] == ObjectType.SHIP ||
						objectType[y + 1, x + 1] == ObjectType.SHIP
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
						objectType[y, x - i] = ObjectType.SHIP;
						//mapOfShips.Add(new[,] { { y }, { (short)(x - i) } }, (short)size);
					}
					break;

				case Direction.UP:
					for (short i = 0; i < size; i++)
					{
						objectType[y - i, x] = ObjectType.SHIP;
						//mapOfShips.Add(new[,] { { (short)(y - i) }, { x } }, (short)size);
					}
					break;

				case Direction.RIGHT:
					for (short i = 0; i < size; i++)
					{
						objectType[y, x + i] = ObjectType.SHIP;
						//mapOfShips.Add(new[,] { { y }, { (short)(x + i) } }, (short)size);
					}
					break;

				case Direction.DOWN:
					for (short i = 0; i < size; i++)
					{
						objectType[y + i, x] = ObjectType.SHIP;
						//mapOfShips.Add(new[,] { { (short)(y + i) }, { x } }, (short)size);
					}
					break;

				default:
					break;
			}
		}


		public bool IsWave(short y, short x)
		{
			return objectType[y, x] == ObjectType.WAVE;
		}
		public bool IsShip(short y, short x)
        {
			return objectType[y, x] == ObjectType.SHIP;
		}
		public bool IsDead(short y, short x)
		{
			return objectType[y, x] == ObjectType.DEAD;
		}
		public bool IsFailure(short y, short x)
		{
			return objectType[y, x] == ObjectType.FAILURE;
		}
		public bool IsEmpty(short y, short x)
		{
			return objectType[y, x] == ObjectType.EMPTY;
		}

		public void SetDead(short y, short x)
        {
			objectType[y, x] = ObjectType.DEAD;
        }
		public void SetFailure(short y, short x)
		{
			objectType[y, x] = ObjectType.FAILURE;
		}

		// 

		public bool IsDeadShip(short y, short x)
        {
            if (IsShipPosition(y, x))
            {
				return false;
            }
			if (IsDeadPosition(y, x))
            {

            }
			return true;
        }
		public bool IsShipPosition(short y, short x)
        {
            for (short i = -1; i <= 1; i++)
            {
                for (short j = -1; j <= 1; j++)
                {
					if (i == 0 && j == 0)
						continue;
                    if (IsPossiblePositionInField((short)(y + i), (short)(x + j)) 
						&& IsShip((short)(y + i), (short)(x + j)))
                    {
						return true;
                    }
                }
            }
			return false;
        }

		public bool IsDeadPosition(short y, short x)
        {
			for (short i = -1; i <= 1; i++)
			{
				for (short j = -1; j <= 1; j++)
				{
					if (i == 0 && j == 0)
						continue;
					if (IsPossiblePositionInField((short)(y + i), (short)(x + j))
						&& IsDead((short)(y + i), (short)(x + j)))
					{
						return true;
					}
				}
			}
			return false;
		}

        public bool IsPossiblePositionInField(short y, short x)
        {
			if (
				y < 0 ||
				y >= size ||
				x < 0 ||
				x >= size ||
				objectType[y, x] == ObjectType.SHIP
				)
			{
				return false;
			}
			return true;
		}
	}
}
