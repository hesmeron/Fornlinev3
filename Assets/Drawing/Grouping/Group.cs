using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grouping
{
    public class Group : Groupable
    {
        Groupable leftChild, rightChild;
        [SerializeField]
        List<Connection> internalConnetctions = new List<Connection>();
        Group parent;
        public bool isStable;
        List<Connection> externalConnetctions = new List<Connection>();
        List<Point> points = new List<Point>();

        public Group (Point left, Point right, Connection c)
        {
            leftChild = left;
            rightChild = right;
            left.group = this;
            right.group = this;
            internalConnetctions.Add(c);
            isStable = true;
        }

        public Group (Group group, Point point, Connection c)
        {

        }

        public Group (Group left, Group right, Connection c)
        {

        }
    }
}
