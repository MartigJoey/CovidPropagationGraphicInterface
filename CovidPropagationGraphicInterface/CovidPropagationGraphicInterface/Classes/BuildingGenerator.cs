using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CovidPropagationGraphicInterface.Classes
{
    class BuildingGenerator
    {
        private List<Building> _buildings;
        private Point _centerZoneTopLeft;
        private Point _centerZoneBottomRight;
        private Point _perimeterZoneTopLeft;
        private Point _perimeterZoneBottomRight;

        private List<Building> _topRow;
        private List<Building> _leftColumn;
        private List<Building> _rightColumn;
        private List<Building> _bottomRow;

        internal Point CenterZoneTopLeft { get => _centerZoneTopLeft.Clone(); }
        internal Point CenterZoneBottomRight { get => _centerZoneBottomRight.Clone(); }
        internal Point PerimeterZoneTopLeft { get => _perimeterZoneTopLeft.Clone(); }
        internal Point PerimeterZoneBottomRight { get => _perimeterZoneBottomRight.Clone(); }
        internal List<Building> Buildings { get => _buildings; }

        public BuildingGenerator()
        {
            _buildings = GenerateBuildings();
            PositioningBuildings();
        }
        /// <summary>
        /// ⚠️ Ce code est utilisé uniquement dans le cadre du stage et sera supprimé 
        /// lors du travail de diplome pour permettre la création de batiement par la simulation. ⚠️
        /// </summary>
        /// <param name="quantity"></param>
        /// <returns></returns>
        private List<Building> GenerateBuildings()
        {
            List<Building> buildings = new List<Building>();
            int nbtotalPerimeterBusLine = 6;

            int topRowSizeWidth;
            int bottomRowSizeWidth;
            int sizeWidth;
            float differenceWidth;

            int leftColumnSizeHeight;
            int rightColumnSizeHeight;
            int sizeHeight;
            float differenceHeight;

            // ⚠️
            buildings.Add(new Building(new Size(50, 50), BuildingType.Home));
            buildings.Add(new Building(new Size(50, 50), BuildingType.Home));
            buildings.Add(new Building(new Size(50, 50), BuildingType.Home));
            buildings.Add(new Building(new Size(50, 50), BuildingType.Home));
            buildings.Add(new Building(new Size(50, 50), BuildingType.Home));

            buildings.Add(new Building(new Size(50, 50), BuildingType.Hospital));
            buildings.Add(new Building(new Size(50, 50), BuildingType.Hospital));
            buildings.Add(new Building(new Size(50, 50), BuildingType.School));
            buildings.Add(new Building(new Size(50, 50), BuildingType.School));
            buildings.Add(new Building(new Size(50, 50), BuildingType.School));

            buildings.Add(new Building(new Size(50, 50), BuildingType.Supermarket));
            buildings.Add(new Building(new Size(50, 50), BuildingType.Supermarket));
            buildings.Add(new Building(new Size(50, 50), BuildingType.Restaurant));
            buildings.Add(new Building(new Size(50, 50), BuildingType.Restaurant));
            buildings.Add(new Building(new Size(50, 50), BuildingType.Restaurant));

            buildings.Add(new Building(new Size(50, 50), BuildingType.Company));
            buildings.Add(new Building(new Size(50, 50), BuildingType.Company));
            buildings.Add(new Building(new Size(50, 50), BuildingType.Company));
            buildings.Add(new Building(new Size(50, 50), BuildingType.Company));
            buildings.Add(new Building(new Size(50, 50), BuildingType.Company));
            // ⚠️

            _topRow = (from building in buildings
                       where building.Type == BuildingType.Home
                       select building).ToList();

            _leftColumn = (from building in buildings
                           where building.Type == BuildingType.Restaurant || building.Type == BuildingType.Supermarket
                           select building).ToList();

            _rightColumn = (from building in buildings
                            where building.Type == BuildingType.Hospital || building.Type == BuildingType.School
                            select building).ToList();

            _bottomRow = (from building in buildings
                          where building.Type == BuildingType.Company
                          select building).ToList();

            topRowSizeWidth = _topRow.Sum(b => b.Size.Width);
            bottomRowSizeWidth = _bottomRow.Sum(b => b.Size.Width);
            sizeWidth = topRowSizeWidth > bottomRowSizeWidth ? topRowSizeWidth : bottomRowSizeWidth;
            sizeWidth += _leftColumn.Max(b => b.Size.Width);
            sizeWidth += _rightColumn.Max(b => b.Size.Width);
            sizeWidth += GlobalVariables.bus_Size.Width * nbtotalPerimeterBusLine;
            differenceWidth = sizeWidth - GlobalVariables.interface_Size_Without_Legend.Width;

            leftColumnSizeHeight = _leftColumn.Sum(b => b.Size.Height);
            rightColumnSizeHeight = _rightColumn.Sum(b => b.Size.Height);
            sizeHeight = leftColumnSizeHeight > rightColumnSizeHeight ? leftColumnSizeHeight : rightColumnSizeHeight;
            sizeHeight += _topRow.Max(b => b.Size.Height);
            sizeHeight += _bottomRow.Max(b => b.Size.Height);
            sizeHeight += GlobalVariables.bus_Size.Width * nbtotalPerimeterBusLine;
            differenceHeight = sizeHeight - GlobalVariables.interface_Size_Without_Legend.Height;

            float sizeChangeInPercentWidth = differenceWidth / sizeWidth * 100f * -1;
            float sizeChangeInPercentHeight = differenceHeight / sizeHeight * 100f * -1;

            buildings.ForEach(b => {
                SizeF newSize = new SizeF(b.Size.Width / 100f * sizeChangeInPercentWidth,
                                          b.Size.Height / 100f * sizeChangeInPercentHeight);
                b.Size = Size.Add(b.Size, Size.Round(newSize));
            });

            System.Console.WriteLine(GlobalVariables.interface_Size_Without_Legend.Height);
            System.Console.WriteLine(sizeHeight);
            System.Console.WriteLine(differenceHeight);
            System.Console.WriteLine(sizeChangeInPercentHeight);

            leftColumnSizeHeight = _leftColumn.Sum(b => b.Size.Height);
            rightColumnSizeHeight = _rightColumn.Sum(b => b.Size.Height);
            sizeHeight = leftColumnSizeHeight > rightColumnSizeHeight ? leftColumnSizeHeight : rightColumnSizeHeight;
            sizeHeight += _topRow.Max(b => b.Size.Height);
            sizeHeight += _bottomRow.Max(b => b.Size.Height);
            sizeHeight += GlobalVariables.bus_Size.Width * nbtotalPerimeterBusLine;
            System.Console.WriteLine(sizeHeight);
            System.Console.WriteLine(GlobalVariables.interface_Size_Without_Legend.Height);

            return buildings;
        }

        private void PositioningBuildings()
        {
            int spaceXY = 10;
            int numberOfBusLine = 2;

            int leftColumnInitialX = GlobalVariables.bus_Size.Width * numberOfBusLine + spaceXY;
            int topRowInitialX = _leftColumn.Max(b => b.Size.Width) + leftColumnInitialX; 
            
            int topRowInitialY = GlobalVariables.bus_Size.Width * numberOfBusLine + spaceXY;
            int leftColumnInitialY = _topRow.Max(b => b.Size.Height) + topRowInitialY;
            
            int rightColumnInitialY = leftColumnInitialY;
            int rightColumnInitialX;
            int bottomRowInitialX = topRowInitialX;
            int bottomRowInitialY;

            int topRowSpace = spaceXY;
            int leftColumnSpace = spaceXY;
            int rightColumnSpace = spaceXY;
            int bottomRowSpace = spaceXY;

            // Rows space
            int topRowSumElementSize = _topRow.Sum(b => b.Size.Width);
            int bottomRowSumElementSize = _bottomRow.Sum(b => b.Size.Width);

            if (topRowSumElementSize > bottomRowSumElementSize)
            {
                bottomRowSpace = CalculateSpace(_topRow.Count, topRowSumElementSize, topRowSpace,
                                         _bottomRow.Count, bottomRowSumElementSize, bottomRowSpace);
            }else
            {
                topRowSpace = CalculateSpace(_bottomRow.Count, bottomRowSumElementSize, bottomRowSpace,
                                             _topRow.Count, topRowSumElementSize, topRowSpace);
            }
            // Columns space
            int lcSumElementSize = _leftColumn.Sum(b => b.Size.Height);
            int rcSumElementSize = _rightColumn.Sum(b => b.Size.Height);

            if (lcSumElementSize > rcSumElementSize)
            {
                rightColumnSpace = CalculateSpace(_leftColumn.Count, lcSumElementSize, leftColumnSpace,
                                                  _rightColumn.Count, rcSumElementSize, rightColumnSpace);
            }else
            {
                leftColumnSpace = CalculateSpace(_rightColumn.Count, rcSumElementSize, rightColumnSpace,
                                                 _leftColumn.Count, lcSumElementSize, leftColumnSpace);
            }

            Building topRowPrev = PositioningBuilding(_topRow, topRowInitialX, topRowInitialY, topRowSpace, true);
            rightColumnInitialX = (int)topRowPrev.Location.X + topRowPrev.Size.Width;

            Building leftColumnPrev = PositioningBuilding(_leftColumn, leftColumnInitialX, leftColumnInitialY, leftColumnSpace, false);
            bottomRowInitialY = (int)leftColumnPrev.Location.Y + leftColumnPrev.Size.Height;

            Building rightColumnPrev = PositioningBuilding(_rightColumn, rightColumnInitialX, rightColumnInitialY, rightColumnSpace, false);
            Building bottomRowPrev = PositioningBuilding(_bottomRow, bottomRowInitialX, bottomRowInitialY, bottomRowSpace, true);

            int centerZoneTopLeftX = _leftColumn.Max(b => b.Size.Width) + (int)_leftColumn[0].Location.X;
            int centerZoneTopLeftY = _topRow.Max(b => b.Size.Height) + (int)_topRow[0].Location.Y;
            _centerZoneTopLeft = new Point(centerZoneTopLeftX, centerZoneTopLeftY);
            _centerZoneBottomRight = new Point((int)_rightColumn[0].Location.X, (int)_bottomRow[0].Location.Y);

            int perimeterZoneBottomRightX = (int)_rightColumn[0].Location.X + _rightColumn.Max(b => b.Size.Width);
            int perimeterZoneBottomRightY = (int)_bottomRow[0].Location.Y + _bottomRow.Max(b => b.Size.Height);
            _perimeterZoneTopLeft = new Point((int)_leftColumn[0].Location.X, (int)_topRow[0].Location.Y);
            _perimeterZoneBottomRight = new Point(perimeterZoneBottomRightX, perimeterZoneBottomRightY);
        }

        private int CalculateSpace(int count1, int elementSize1, int space1,
                                   int count2, int elementSize2, int space2)
        {
            int space;

            int size1 = elementSize1 + space1 * count1 - space1;
            int size2 = elementSize2 + space2 * count2 - space2;

            int sizeDifference = size1 - size2;
            space = space2 + sizeDifference / (count2 - 1);

            return space;
        }

        private Building PositioningBuilding(List<Building> buildings, int initialX, int initialY, int space, bool isRow)
        {
            Building prev = null;
            buildings.ForEach(b => {
                int x = initialX;
                int y = initialY;

                if (isRow)
                    x = prev == null ? initialX : (int)prev.Location.X + prev.Size.Width + space;
                else
                    y = prev == null ? initialY : (int)prev.Location.Y + prev.Size.Height + space;

                b.Location = new Point(x, y);
                prev = b;
            });
            return prev;
        }
    }
}
