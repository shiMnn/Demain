namespace game {
    static class GameDefine {
        /// <summary>ブロックのサイズ(横幅)</summary>
        static public float BLOCK_SIZE_WIDTH = 1.0f;
        /// <summary>ブロックのサイズ(高さ)</summary>
        static public float BLOCK_SIZE_HEIGHT = 1.0f;
        /// <summary>ブロックのサイズ(奥行)</summary>
        static public float BLOCK_SIZE_DEPTH = 1.0f;
        /// <summary>部屋の最小数</summary>
        static public int MIN_ROOM_COUNT = 10;
        /// <summary>部屋の最大数</summary>
        static public int MAX_ROOM_COUTN = 25;
        /// <summary>部屋の最小横幅</summary>
        static public int MIN_ROOM_WIDTH = 8;
        /// <summary>部屋の最大横幅</summary>
        static public int MAX_ROOM_WIDTH = 15;
        /// <summary>部屋の最小縦幅</summary>
        static public int MIN_ROOM_HEIGHT = 8;
        /// <summary>部屋の最大縦幅</summary>
        static public int MAX_ROOM_HEIGHT = 15;
        /// <summary>フロアの最大横幅</summary>
        static public int FLOOR_MAX_WIDTH = 70;
        /// <summary>フロアの最大縦幅</summary>
        static public int FLOOR_MAX_HEIGHT = 70;
        /// <summary>ゲーム中に干渉することができない領域</summary>
        static public int UNTOUCHABLE_AREA = 10;
    }

    public enum Direction {
        None = -1,
        Top,
        Bottom,
        Left,
        Right,
    }

    public enum Power {
        None = -1,          //!< 所属なし
        Neutral,            //!< 中立
        Friendly,           //!< 味方勢力
        Opponent,           //!< 敵勢力
    }

    /// <summary>アクション種別</summary>
    public enum ActionType {
        Move,
    }
}