namespace DracoGame.Shared.Helpers
{
    public static class Constants
    {
        public static class Entities
        {
            public const string MainMenu = @"main-menu";
            public const string TestMap = @"test-map";
            public const string Player = @"player";
        }

        public static class Content
        {
            public static class Map
            {
                public const string Test = @"Maps/Test";
            }

            public const string Characters = @"Characters/Tileset";
        }

        public static class MapLayers
        {
            public const string Collision = @"Collision";
            public const string WalkableBehind = @"Walkable behind";
            public const string Objects = @"Objects";
            public const string Walkable = @"Walkable";
            public const string Road = @"Road";
            public const string SpawnPoints = @"Spawn points";
        }
    }
}