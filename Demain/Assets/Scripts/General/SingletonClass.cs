public class SingletonClass<T> where T : class, new() {
    private static T m_instance = null;

    public static T Instance
    {
        get
        {
            if(m_instance == null) {
                m_instance = new T();
            }
            return m_instance;
        }
    }

    public static void Release() {
        m_instance = null;
    }
}