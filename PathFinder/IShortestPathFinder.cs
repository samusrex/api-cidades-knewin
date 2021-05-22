namespace api
{
    interface IShortestPathFinder
    {
        Node[] FindShortestPath(Node from, Node to);
    }

}
