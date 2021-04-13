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

        internal Point CenterZoneTopLeft { get => _centerZoneTopLeft.Clone(); }
        internal Point CenterZoneBottomRight { get => _centerZoneBottomRight.Clone(); }
        internal Point PerimeterZoneTopLeft { get => _perimeterZoneTopLeft.Clone(); }
        internal Point PerimeterZoneBottomRight { get => _perimeterZoneBottomRight.Clone(); }

        public BuildingGenerator(List<Building> buildings)
        {
            _buildings = buildings;
            PositioningBuildings();
        }
        private void PositioningBuildings()
        {
            List<Building> topRow = new List<Building>(); int trInitialX = 90, trInitialY = 40, trSpace = 10;

            List<Building> leftColumn = new List<Building>(); int lcInitialX = 40, lcInitialY = 90, lcSpace = 10;
            List<Building> rightColumn = new List<Building>(); int rcInitialX = 20, rcInitialY = 90, rcSpace = 10;

            List<Building> bottomRow = new List<Building>(); int brInitialX = 90, brInitialY = 30, brSpace = 10;

            topRow = (from building in _buildings
                      where building.Type == BuildingType.Home
                      select building).ToList();

            leftColumn = (from building in _buildings
                          where building.Type == BuildingType.Restaurant || building.Type == BuildingType.Supermarket
                          select building).ToList();

            rightColumn = (from building in _buildings
                           where building.Type == BuildingType.Hospital || building.Type == BuildingType.School
                           select building).ToList();

            bottomRow = (from building in _buildings
                         where building.Type == BuildingType.Company
                         select building).ToList();

            // Rows space
            int trSumElementSize = topRow.Sum(b => b.Size.Width);
            int brSumElementSize = bottomRow.Sum(b => b.Size.Width);

            if (trSumElementSize > brSumElementSize)
            {
                brSpace = CalculateSpace(topRow.Count, trSumElementSize, trSpace,
                                         bottomRow.Count, brSumElementSize, brSpace);
            }else
            {
                trSpace = CalculateSpace(bottomRow.Count, brSumElementSize, brSpace,
                                             topRow.Count, trSumElementSize, trSpace);
            }
            // Columns space
            int lcSumElementSize = leftColumn.Sum(b => b.Size.Height);
            int rcSumElementSize = rightColumn.Sum(b => b.Size.Height);

            if (lcSumElementSize > rcSumElementSize)
            {
                rcSpace = CalculateSpace(leftColumn.Count, lcSumElementSize, lcSpace,
                                         rightColumn.Count, rcSumElementSize, rcSpace);
            }else
            {
                lcSpace = CalculateSpace(rightColumn.Count, rcSumElementSize, rcSpace,
                                         leftColumn.Count, lcSumElementSize, lcSpace);
            }

            Building trPrev = PositioningBuilding(topRow, trInitialX, trInitialY, trSpace, true);
            rcInitialX = (int)trPrev.Location.X + trPrev.Size.Width;

            Building lcPrev = PositioningBuilding(leftColumn, lcInitialX, lcInitialY, lcSpace, false);
            brInitialY = (int)lcPrev.Location.Y + lcPrev.Size.Height;

            Building rcPrev = PositioningBuilding(rightColumn, rcInitialX, rcInitialY, rcSpace, false);
            Building brPrev = PositioningBuilding(bottomRow, brInitialX, brInitialY, brSpace, true);

            int centerZoneTopLeftX = leftColumn.Max(b => b.Size.Width) + (int)leftColumn[0].Location.X;
            int centerZoneTopLeftY = topRow.Max(b => b.Size.Height) + (int)topRow[0].Location.Y;
            _centerZoneTopLeft = new Point(centerZoneTopLeftX, centerZoneTopLeftY);
            _centerZoneBottomRight = new Point((int)rightColumn[0].Location.X, (int)bottomRow[0].Location.Y);

            int perimeterZoneBottomRightX = (int)rightColumn[0].Location.X + rightColumn.Max(b => b.Size.Width);
            int perimeterZoneBottomRightY = (int)bottomRow[0].Location.Y + bottomRow.Max(b => b.Size.Height);
            _perimeterZoneTopLeft = new Point((int)leftColumn[0].Location.X, (int)topRow[0].Location.Y);
            _perimeterZoneBottomRight = new Point(perimeterZoneBottomRightX, perimeterZoneBottomRightY);

            //CreateBusLines(CenterZoneTopLeft, CenterZoneBottomRight, PerimeterZoneTopLeft, PerimeterZoneBottomRight);
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
