using System;

namespace Dungeosis
{
    ///<summary>Configuration settings to be used by the <c>MapGenerator</c> when generating maps.<summary>
    public class MapGeneratorConfig
    {
        public int Height { get; set; }
        public int Width { get; set; }

        /// <summary>Indicates if dead ends in the corridor mazes should be removed.</summary>
        /// <remarks>
        /// By default, dead ends in the corridors connecting rooms are removed to make the paths between
        /// regions more direct.  This prevents the player from wandering aimlessly from one dead end to another.
        /// Turning this off will result in a full maze being generated around rooms with no dead space.
        /// </remarks>

        public bool CullDeadEnds { get; set; }

        /// <summary>
        /// The max number of collisions with other rooms that shouild occur before stopping room generation.
        /// <summary>
        /// <remarks>
        /// Room generation occurs by trying to place rooms within the map. If it collides with another room,
        /// it is discarded and a new one is generated.  After a number of collisions, the generator will stop
        /// trying to place rooms.  This numebr is that threshold.
        /// </remarks>
        public int RoomCollisionThreshold { get; set; }

        /// <summary>
        /// Determines how much extra space/tiles should be between generated rooms and the edges of the map.
        /// </summary>
        /// <remarks>
        /// This adds padding around rooms that is used during collision detection. A higher number will result
        /// in rooms spaced further apart.
        public int RoomBorderThreshold { get; set; } // TODO: Implement RoomBorderThreshold from configuration.

        /// <summary>
        /// The chance that a corridor will change directions during generation.
        /// </summary>
        /// <remarks>
        /// Assumed to be between 0 and 100. A higher percentage will result in corridors that turn and wind
        /// throughout the map more frequently.  Lower numbers will result in longer, less windy corridors.
        /// </remarks>
        public int CorridorDirectionChangeChance { get; set; }

        /// <summary>
        /// The chance that a wall connecting two different regions will be made into a door.
        /// </summary>
        /// <remarks>
        /// Regions will be connected to other regions with one door initially.  All other tiles that could connect
        /// to different regions are removed.  This setting provides a chance that a connection cell that would
        /// normally be discarded will instead be converted to a door/connection point. Any connection points that are
        /// immediately next to an already existing door/connection point will be discarded regardless of this setting.
        public int ExtraDoorChance { get; set; }

        /// <summary>
        /// The seed to be used by the random number generator when generating the map and it's features.
        /// </summary>
        public int Seed { get; set; }

        public MapGeneratorConfig()
        {
            Width = 125;
            Height = 75;
            CullDeadEnds = true;
            RoomCollisionThreshold = 1000;
            CorridorDirectionChangeChance = 25;
            ExtraDoorChance = 5;
            Seed = (int)DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }
    }
}