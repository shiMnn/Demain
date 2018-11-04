namespace game {
    static class GameDefine {
        /// <summary>ブロックのサイズ(横幅)</summary>
        static public float BLOCK_SIZE_WIDTH = 1.0f;
        /// <summary>ブロックのサイズ(高さ)</summary>
        static public float BLOCK_SIZE_HEIGHT = 1.0f;
        /// <summary>ブロックのサイズ(奥行)</summary>
        static public float BLOCK_SIZE_DEPTH = 1.0f;
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
}