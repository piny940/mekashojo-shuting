namespace Controller
{
    /* IDについての設計
     * 各タイプのオブジェクトに対して、IDが何番まで使われたかはModel.IDManagerで管理する。
     * ControllerクラスにはIDを取得・Modelクラスのインスタンスの生成を行うメソッドを用意しておく。
     * 各ビュークラスはStartメソッドで上記のメソッドからIDを取得する
     * コントローラークラスはModelクラスのインスタンスが入ったテーブルに対してforeachをすることで
     * モデルクラスのメソッドをUpdateメソッドで呼ぶ。
     * オブジェクトを削除する際は、ビュークラスにおいて、コントローラークラスのテーブルの
     * 対応する箇所を取り除き、その後自身をDestroyする。
     * ただしテーブルから対応する箇所を取り除く処理は、AddListenerの中に入れると
     * foreachのループの中で「ループに使っているテーブル」に変更を入れてしまい、
     * "Collection was modified; enumeration operation may not execute."
     * と言われるため、isBeingDestroyedなどのブール値を用意しておいて、Updateメソッドの中で行う。
     */
    public static class IDManager
    {
        // IDを何まで使ったか
        private static int _lastEnemyID = 0;
        private static int _lastEnemyBulletID = 0;
        private static int _lastPlayerBulletID = 0;
        private static int _lastMaterialID = 0;

        public static int GetEnemyID()
        {
            _lastEnemyID++;
            return _lastEnemyID;
        }

        public static int GetEnemyBulletID()
        {
            _lastEnemyBulletID++;
            return _lastEnemyBulletID;
        }

        public static int GetPlayerBulletID()
        {
            _lastPlayerBulletID++;
            return _lastPlayerBulletID;
        }

        public static int GetMaterialID()
        {
            _lastMaterialID++;
            return _lastMaterialID;
        }
    }
}
