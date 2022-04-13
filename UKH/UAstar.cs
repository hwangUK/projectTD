using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace UKH
{
 
    public class AStarNode
    {
        public int X;
        public int Y;
        public int GCost;       // Start ~ Current
        public int HCost;       // Current ~ End
        private int FCost;
        public AStarNode Parent;

        public AStarNode(int x, int y)
        {
            X = x;
            Y = y;
            Reset();
        }

        public int GetFCost()
        {
            return GCost + HCost;
        }
        public void SetFCost(int value)
        {
            FCost = value;
        }

        public void Reset()
        {
            FCost = 0;
            GCost = 0;
            HCost = 0;
            Parent = null;
        }
        public void Execute(AStarNode parentNode, AStarNode endNode)
        {
            Parent = parentNode;
            GCost = CalcGValue(parentNode, this);
            int diffX = Mathf.Abs(endNode.X - X);
            int diffY = Mathf.Abs(endNode.Y - Y);
            HCost = (diffX + diffY) * 10;
        }
        public int CalcGValue(AStarNode parentNode, AStarNode currentNode)
        {
            int diffX = Mathf.Abs(parentNode.X - currentNode.X);
            int diffY = Mathf.Abs(parentNode.Y - currentNode.Y);
            int value = 10;
            if (diffX == 1 && diffY == 1)
            {
                value = 14;
            }
            return parentNode.GCost + value;
        }
    }

   
}

