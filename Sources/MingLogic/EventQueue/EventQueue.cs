namespace MingLogic
{
    using System;

    public class EventQueue
    {
        private EventNode root;

        public void Enqueue(ScheduledEvent item)
        {
            this.root = EventNode.Merge(this.root, new EventNode(item));
        }

        public ScheduledEvent DeleteMin()
        {
            ScheduledEvent result = this.root.Data;
            this.root = this.root.DeleteMin();
            return result;
        }

        public bool IsEmpty()
        {
            return this.root == null;
        }

        private class EventNode
        {
            private ScheduledEvent data;
            private int distance;
            private EventNode left;
            private EventNode right;

            public EventNode(ScheduledEvent data)
            {
                this.data = data;
                this.distance = 1;
            }

            public ScheduledEvent Data
            {
                get
                {
                    return this.data;
                }
            }

            public static EventNode Merge(EventNode tree1, EventNode tree2)
            {
                if (tree1 == null)
                {
                    return tree2;
                }
                else if (tree2 == null)
                {
                    return tree1;
                }
                else
                {
                    if (tree1.data.Time <= tree2.data.Time)
                    {
                        tree1.right = EventNode.Merge(tree1.right, tree2);
                        int leftDistance = tree1.left == null ? 0 : tree1.left.distance;
                        int rightDistance = tree1.right == null ? 0 : tree1.right.distance;
                        if (leftDistance < rightDistance)
                        {
                            EventNode temp = tree1.left;
                            tree1.left = tree1.right;
                            tree1.right = temp;
                            tree1.distance = tree1.left.distance + 1;
                        }

                        return tree1;
                    }
                    else
                    {
                        return EventNode.Merge(tree2, tree1);
                    }
                }
            }

            public EventNode DeleteMin()
            {
                return EventNode.Merge(this.left, this.right);
            }
        }
    }
}
