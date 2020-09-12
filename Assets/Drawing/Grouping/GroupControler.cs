using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grouping
{

    public static class GroupControler
    {
        public static void ConnectPoint(Point left, Point right, Connection c)
        {
            if (left.group == null && right.group == null)
            {
                new Group(left, right, c);
            }
            else
            {
                if (left.group != null && right.group == null)
                {
                    new Group(left.group, right, c);
                }
                else if (left.group == null && right.group != null)
                {
                    new Group(right.group, left, c);
                }
                else
                {
                    new Group(left.group, right.group, c);
                }
            }
        }
    }
}
